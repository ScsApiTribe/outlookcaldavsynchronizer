﻿// This file is Part of CalDavSynchronizer (http://outlookcaldavsynchronizer.sourceforge.net/)
// Copyright (c) 2015 Gerhard Zehetbauer
// Copyright (c) 2015 Alexander Nimmervoll
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as
// published by the Free Software Foundation, either version 3 of the
// License, or (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Affero General Public License for more details.
// 
// You should have received a copy of the GNU Affero General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using CalDavSynchronizer.DataAccess;
using CalDavSynchronizer.Implementation.ComWrappers;
using DDay.iCal;
using GenSync.EntityRelationManagement;
using GenSync.EntityRepositories;
using GenSync.Logging;
using log4net;
using Microsoft.Office.Interop.Outlook;

namespace CalDavSynchronizer.Implementation.Events
{
  public class DuplicateEventCleaner : IEventSynchronizationContext
  {
    private static readonly ILog s_logger = LogManager.GetLogger (System.Reflection.MethodInfo.GetCurrentMethod ().DeclaringType);

    private readonly Dictionary<AppointmentId, int> _hashesById;
    private readonly OutlookEventRepository _outlookRepository;
    private readonly IEntityRepository<WebResourceName, string, IICalendar, IEventSynchronizationContext> _btypeRepository;
    private readonly IEntityRelationDataAccess<AppointmentId, DateTime, WebResourceName, string> _entityRelationDataAccess;

    public DuplicateEventCleaner (
      OutlookEventRepository outlookRepository, 
      IEntityRepository<WebResourceName, string, IICalendar, IEventSynchronizationContext> btypeRepository, 
      IEntityRelationDataAccess<AppointmentId, DateTime, WebResourceName, string> entityRelationDataAccess,
      IEqualityComparer<AppointmentId> idComparer)
    {
      if (outlookRepository == null)
        throw new ArgumentNullException (nameof (outlookRepository));
      if (btypeRepository == null)
        throw new ArgumentNullException (nameof (btypeRepository));
      if (entityRelationDataAccess == null)
        throw new ArgumentNullException (nameof (entityRelationDataAccess));
      if (idComparer == null) throw new ArgumentNullException(nameof(idComparer));

      _outlookRepository = outlookRepository;
      _btypeRepository = btypeRepository;
      _entityRelationDataAccess = entityRelationDataAccess;
      _hashesById = new Dictionary<AppointmentId, int>(idComparer);
    }

    public async Task NotifySynchronizationFinished ()
    {
      await DeleteDuplicates();
    }

    public void AnnounceAppointment (AppointmentItem appointment)
    {
      _hashesById[new AppointmentId(appointment.EntryID, appointment.GlobalAppointmentID)] = GetHashCode (appointment);
    }

    public void AnnounceAppointmentDeleted (AppointmentId id)
    {
      _hashesById.Remove (id);
    }

    public async Task<IEnumerable<AppointmentId>> DeleteAnnouncedEventsIfDuplicates(Predicate<AppointmentId> canBeDeleted)
    {
      var appointmentIdsWithIdenticalHashCode = GetAppointmentIdsWithIdenticalHashCode();

      if (appointmentIdsWithIdenticalHashCode.Length == 0)
        return new AppointmentId[0];

      var deletedEntityIds = new List<AppointmentId>();

      foreach (var ids in appointmentIdsWithIdenticalHashCode)
      {
        var appointments = await GetAppointmentsWithId(ids);
        try
        {
          if (appointments.Length > 1)
          {
            var appointmentsToDelete = (
              from appointmentWithId in appointments
              let data = GetDuplicationRelevantData(appointmentWithId.Item2.Inner)
              group new {Appointment = appointmentWithId, CanBeDeleted = canBeDeleted(appointmentWithId.Item1)} by data // group by duplication relevant data (hashes can collide)
              into groupedByData
              where groupedByData.Count() > 1 // take only the groups containing duplicates found by data comparison
              let appointmentToKeep = groupedByData.FirstOrDefault(a => !a.CanBeDeleted) ?? groupedByData.First() // if all can be deleted, one has to be left untouched
              from appointmentPair in groupedByData.Where(a => a.CanBeDeleted && a != appointmentToKeep).Select(a => new {Keep = appointmentToKeep, Delete = a})
              select appointmentPair
              ).ToArray();

            foreach (var appointmentPair in appointmentsToDelete)
            {
              s_logger.Info($"Deleting duplicate of '{appointmentPair.Keep.Appointment.Item2.Inner.EntryID}'");

              var appointmentToDelete = appointmentPair.Delete.Appointment;
              appointmentToDelete.Item2.Inner.Delete();
              deletedEntityIds.Add(appointmentToDelete.Item1);
              _hashesById.Remove(appointmentToDelete.Item1);
            }
          }

        }
        finally
        {
          _outlookRepository.Cleanup(appointments.Select(a => a.Item2));
        }
      }
      return deletedEntityIds;
    }

    private int GetHashCode (AppointmentItem item)
    {
      return GetDuplicationRelevantData (item).GetHashCode ();
    }

    private static Tuple<DateTime, DateTime, string> GetDuplicationRelevantData (AppointmentItem item)
    {
      return Tuple
          .Create (
              item.Start,
              item.End,
              item.Subject);
    }

    private async Task DeleteDuplicates()
    {
      var appointmentIdsWithIdenticalHashCode = GetAppointmentIdsWithIdenticalHashCode();

      if (appointmentIdsWithIdenticalHashCode.Length == 0)
        return;


      var relationsById = _entityRelationDataAccess.LoadEntityRelationData().ToDictionary(r => r.AtypeId);

      foreach (var ids in appointmentIdsWithIdenticalHashCode)
      {
        var appointments = await GetAppointments(ids);
        if (appointments.Length > 1)
        {
          try
          {
            var appointmentToKeep = appointments[0];
            var appointmentToKeepData = GetDuplicationRelevantData(appointmentToKeep.Inner);
            foreach (var appointmentToDelete in appointments.Skip(1))
            {
              if (GetDuplicationRelevantData(appointmentToDelete.Inner).Equals(appointmentToKeepData))
              {
                s_logger.Info($"Deleting duplicate of '{appointmentToKeep.Inner.EntryID}'");
                await DeleteAppointment(appointmentToDelete, relationsById);
              }
            }
          }
          finally
          {
            _outlookRepository.Cleanup(appointments);
          }
        }
      }
      _entityRelationDataAccess.SaveEntityRelationData(relationsById.Values.ToList());
    }

    private AppointmentId[][] GetAppointmentIdsWithIdenticalHashCode()
    {
      return _hashesById
        .GroupBy(p => p.Value)
        .Where(g => g.Count() > 1)
        .Select(g => g.Select(p => p.Key).ToArray())
        .ToArray();
    }

    private async Task DeleteAppointment (AppointmentItemWrapper item, Dictionary<AppointmentId, IEntityRelationData<AppointmentId, DateTime, WebResourceName, string>> relations)
    {
      IEntityRelationData<AppointmentId, DateTime, WebResourceName, string> relation;
      var appointmentId = new AppointmentId(item.Inner.EntryID, item.Inner.GlobalAppointmentID);
      if (relations.TryGetValue (appointmentId, out relation))
      {
        await _btypeRepository.TryDelete (relation.BtypeId, relation.BtypeVersion, NullEventSynchronizationContext.Instance);
        relations.Remove (appointmentId);
      }
      item.Inner.Delete();
    }

    private async Task<AppointmentItemWrapper[]> GetAppointments(AppointmentId[] ids)
    {
      return (await Task.WhenAll(ids.Select(GetOrNull).Where(a => a != null))).ToArray();
    }

    private async Task<Tuple<AppointmentId, AppointmentItemWrapper>[]> GetAppointmentsWithId (AppointmentId[] ids)
    {
      return (await Task.WhenAll (ids.Select (async i => Tuple.Create( i, await GetOrNull (i) )))).Where (a => a.Item2 != null).ToArray();
    }

    async Task<AppointmentItemWrapper> GetOrNull (AppointmentId id)
    {
      try
      {
        var itemById =
          await _outlookRepository.Get (
            new[] { id },
            NullLoadEntityLogger.Instance,
            NullEventSynchronizationContext.Instance);

        return itemById.FirstOrDefault ()?.Entity;
      }
      catch (COMException x) when (x.HResult == unchecked((int) 0x8004010F))
      {
        return null;
      }
    }
  }
}