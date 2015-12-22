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
// along with this program.  If not, see <http://www.gnu.org/licenses/>.using System;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using CalDavSynchronizer.DataAccess;
using Microsoft.Win32;

namespace CalDavSynchronizer.Ui.Reports
{
  public class ReportsViewModel : ViewModelBase
  {
    private readonly ObservableCollection<ReportViewModel> _reports = new ObservableCollection<ReportViewModel>();
    private readonly DelegateCommand _deleteSelectedCommand;
    private readonly DelegateCommand _saveSelectedCommand;
    private ISynchronizationReportRepository _reportRepository;

    public static ReportsViewModel Create (Dictionary<Guid, string> currentProfileNamesById, ISynchronizationReportRepository reportRepository)
    {
      var reportNames = reportRepository.GetAvailableReports();

      var reports = new ObservableCollection<ReportViewModel>();
      foreach (var reportName in reportNames)
      {
        string profileName;
        if (!currentProfileNamesById.TryGetValue (reportName.SyncronizationProfileId, out profileName))
          profileName = "<Not existing anymore>";

        var reportNameClosureLocal = reportName;
        var reportProxy = new ReportProxy (reportName, () => reportRepository.GetReport (reportNameClosureLocal), profileName);
        var reportViewModel = new ReportViewModel (reportProxy, reportRepository);
        reports.Add (reportViewModel);
      }

      return new ReportsViewModel (reports, reportRepository);
    }

    private ReportsViewModel (ObservableCollection<ReportViewModel> reports, ISynchronizationReportRepository reportRepository)
    {
      _reports = reports;
      this._reportRepository = reportRepository;
      _deleteSelectedCommand = new DelegateCommand (DeleteSelected, _ => Reports.Any (r => r.IsSelected));
      _saveSelectedCommand = new DelegateCommand (SaveSelected, _ => Reports.Any (r => r.IsSelected));
    }


    private void SaveSelected (object parameter)
    {
      SaveFileDialog dialog = new SaveFileDialog();
      dialog.Filter = "Zip archives|*.zip";
      dialog.FileName = "SynchronizationReports.zip";
      dialog.Title = "Save selected reports";
      if (dialog.ShowDialog() ?? false)
      {
        using (var fileStream = new FileStream (dialog.FileName, FileMode.Create))
        {
          using (var archive = new ZipArchive (fileStream,ZipArchiveMode.Create))
          {
            foreach (var report in Reports.Where (r => r.IsSelected))
            {
              var entry = archive.CreateEntry (report.ReportName.ToString(), CompressionLevel.Optimal);
              using (var entryStream = entry.Open())
              {
                using (var reportStream = _reportRepository.GetReportStream (report.ReportName))
                {
                  reportStream.CopyTo (entryStream);
                }
              }
            }
          }
        }
      }
    }

    private void DeleteSelected (object parameter)
    {
      for (int i = Reports.Count - 1; i >= 0; i--)
      {
        var report = Reports[i];
        if (report.IsSelected)
        {
          report.Delete();
          Reports.RemoveAt (i);
        }
      }
    }

    public ObservableCollection<ReportViewModel> Reports
    {
      get { return _reports; }
    }

    public DelegateCommand DeleteSelectedCommand
    {
      get { return _deleteSelectedCommand; }
    }

    public DelegateCommand SaveSelectedCommand
    {
      get { return _saveSelectedCommand; }
    }

    public static readonly ReportsViewModel DesignInstance;

    static ReportsViewModel ()
    {
      DesignInstance = new ReportsViewModel (
          new ObservableCollection<ReportViewModel>
          {
              ReportViewModel.CreateDesignInstance(),
              ReportViewModel.CreateDesignInstance (true),
              ReportViewModel.CreateDesignInstance (false, true),
              ReportViewModel.CreateDesignInstance (true, true),
          },
          NullSynchronizationReportRepository.Instance);
    }
  }
}