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
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Input;
using CalDavSynchronizer.Contracts;
using CalDavSynchronizer.Ui.Options.ViewModels.Mapping;
using log4net;
using Microsoft.Office.Interop.Outlook;

namespace CalDavSynchronizer.Ui.Options.ViewModels
{
    public class OptionsCollectionViewModel : IOptionsViewModelParent, ISynchronizationProfilesViewModel
    {
        private static readonly ILog s_logger = LogManager.GetLogger(MethodInfo.GetCurrentMethod().DeclaringType);

        private readonly ObservableCollection<IOptionsViewModel> _options = new ObservableCollection<IOptionsViewModel>();
        private readonly IOptionsViewModelFactory _optionsViewModelFactory;
        private readonly GeneralOptions _generalOptions;
        private readonly IUiService _uiService;
        public event EventHandler<CloseEventArgs> CloseRequested;
        private readonly Func<Guid, string> _profileDataDirectoryFactory;
        private readonly IOptionTasks _optionTasks;

        public event EventHandler RequestBringIntoView;

        public OptionsCollectionViewModel(
          GeneralOptions generalOptions,
          IOutlookAccountPasswordProvider outlookAccountPasswordProvider,
          IReadOnlyList<string> availableEventCategories,
          Func<Guid, string> profileDataDirectoryFactory,
          IUiService uiService,
          IOptionTasks optionTasks)
        {
            _optionTasks = optionTasks;
            _profileDataDirectoryFactory = profileDataDirectoryFactory;
            _uiService = uiService;
            if (generalOptions == null)
                throw new ArgumentNullException(nameof(generalOptions));
            if (profileDataDirectoryFactory == null)
                throw new ArgumentNullException(nameof(profileDataDirectoryFactory));
            if (optionTasks == null) throw new ArgumentNullException(nameof(optionTasks));

            _generalOptions = generalOptions;


            _optionsViewModelFactory = new OptionsViewModelFactory(
              this,
              outlookAccountPasswordProvider,
              availableEventCategories,
              optionTasks);
            AddCommand = new DelegateCommand(_ => Add());
            AddMultipleCommand = new DelegateCommand(_ => AddMultiple());
            CloseCommand = new DelegateCommand(shouldSaveNewOptions => Close((bool)shouldSaveNewOptions));
            DeleteSelectedCommand = new DelegateCommandHandlingRequerySuggested(_ => DeleteSelected(), _ => CanDeleteSelected);
            CopySelectedCommand = new DelegateCommandHandlingRequerySuggested(_ => CopySelected(), _ => CanCopySelected);
            MoveSelectedUpCommand = new DelegateCommandHandlingRequerySuggested(_ => MoveSelectedUp(), _ => CanMoveSelectedUp);
            MoveSelectedDownCommand = new DelegateCommandHandlingRequerySuggested(_ => MoveSelectedDown(), _ => CanMoveSelectedDown);
            OpenProfileDataDirectoryCommand = new DelegateCommandHandlingRequerySuggested(_ => OpenProfileDataDirectory(), _ => CanOpenProfileDataDirectory);
            ExpandAllCommand = new DelegateCommandHandlingRequerySuggested(_ => ExpandAll(), _ => _options.Count > 0);
            CollapseAllCommand = new DelegateCommandHandlingRequerySuggested(_ => CollapseAll(), _ => _options.Count > 0);
            ExportAllCommand = new DelegateCommandHandlingRequerySuggested(_ => ExportAll(), _ => _options.Count > 0);
            ImportCommand = new DelegateCommandHandlingRequerySuggested(_ => Import(), _ => true);
        }

        private void Import()
        {
            var fileName = _uiService.ShowOpenDialog("Import Profiles");
            if (fileName == null)
                return;

            var reportBuilder = new StringBuilder();
            var newOptions = _optionTasks.LoadOptions(fileName);
            var mergedOptions = _optionTasks.ProfileExportProcessor.PrepareAndMergeForImport(GetOptionsCollection(), newOptions, s => reportBuilder.AppendLine(s));

            SetOptionsCollection(mergedOptions);

            reportBuilder.AppendLine($"Sucessfully imported {newOptions.Length} profile(s) from '{fileName}'.");

            _uiService.ShowReport("Export profiles", reportBuilder.ToString());
        }

        private void ExportAll()
        {
            var reportBuilder = new StringBuilder();

            var profiles = GetOptionsCollection();
            _optionTasks.ProfileExportProcessor.PrepareForExport(profiles, s => reportBuilder.AppendLine(s));

            var fileName = _uiService.ShowSaveDialog("Export Profiles");
            if (fileName != null)
            {
                _optionTasks.SaveOptions(profiles, fileName);
                reportBuilder.AppendLine($"Sucessfully exported {profiles.Length} profile(s) to '{fileName}'.");
            }
            else
            {
                reportBuilder.AppendLine("Export cancelled by user.");
            }

            _uiService.ShowReport("Export profiles", reportBuilder.ToString());
        }

        private bool CanMoveSelectedDown => SelectedOrNull != null;
        private bool CanMoveSelectedUp => SelectedOrNull != null;
        private bool CanCopySelected => SelectedOrNull != null;
        private bool CanDeleteSelected => SelectedOrNull != null;
        private bool CanOpenProfileDataDirectory => SelectedOrNull != null;

        private void MoveSelectedDown()
        {
            var selected = SelectedOrNull;
            if (selected != null)
            {
                var index = _options.IndexOf(selected);
                var newIndex = Math.Min(index + 1, _options.Count - 1);
                System.Diagnostics.Debug.WriteLine($"{index} => {newIndex}");
                _options.Move(index, newIndex);
                selected.IsSelected = true;
            }
        }

        private void MoveSelectedUp()
        {
            var selected = SelectedOrNull;
            if (selected != null)
            {
                var index = _options.IndexOf(selected);
                var newIndex = Math.Max(index - 1, 0);
                System.Diagnostics.Debug.WriteLine($"{index} => {newIndex}");
                _options.Move(index, newIndex);
                selected.IsSelected = true;
            }
        }

        private void CopySelected()
        {
            var selected = SelectedOrNull;
            if (selected != null)
                Copy(selected);
        }

        private void DeleteSelected()
        {
            var selected = SelectedOrNull;
            if (selected != null)
                Delete(selected);
        }

        private void OpenProfileDataDirectory()
        {
            var selected = SelectedOrNull;
            if (selected != null)
            {
                var profileDataDirectory = _profileDataDirectoryFactory(selected.Id);
                if (Directory.Exists(profileDataDirectory))
                    System.Diagnostics.Process.Start(profileDataDirectory);
                else
                    MessageBox.Show("The selected profile has no data directory.", "Operation aborted", MessageBoxButton.OK);
            }
        }

        private void CollapseAll()
        {
            ExpandCollapseAll(_options, false);
        }

        private void ExpandAll()
        {
            ExpandCollapseAll(_options, true);
        }

        private void ExpandCollapseAll(IEnumerable<ITreeNodeViewModel> nodes, bool isExpanded)
        {
            foreach (var node in nodes)
            {
                ExpandCollapseAll(node.Items, isExpanded);
                node.IsExpanded = isExpanded;
            }
        }

        IOptionsViewModel SelectedOrNull => _options.FirstOrDefault(o => o.IsSelected);

        private void Close(bool shouldSaveNewOptions)
        {
            if (shouldSaveNewOptions)
            {
                IOptionsViewModel firstViewModelWithError;
                string errorMessage;
                if (!Validate(out errorMessage, out firstViewModelWithError))
                {
                    _uiService.ShowErrorDialog(errorMessage, "Some Options contain invalid Values");
                    if (firstViewModelWithError != null)
                        firstViewModelWithError.IsSelected = true;
                    return;
                }
            }

            CloseRequested?.Invoke(this, new CloseEventArgs(shouldSaveNewOptions));
        }

        private bool Validate(out string errorMessage, out IOptionsViewModel firstViewModelWithError)
        {
            StringBuilder errorMessageBuilder = new StringBuilder();
            bool isValid = true;
            firstViewModelWithError = null;

            foreach (var viewModel in _options)
            {
                StringBuilder currentControlErrorMessageBuilder = new StringBuilder();

                if (!viewModel.Validate(currentControlErrorMessageBuilder))
                {
                    if (errorMessageBuilder.Length > 0)
                        errorMessageBuilder.AppendLine();

                    errorMessageBuilder.AppendFormat("Profile '{0}'", viewModel.Name);
                    errorMessageBuilder.AppendLine();
                    errorMessageBuilder.Append(currentControlErrorMessageBuilder);

                    isValid = false;
                    if (firstViewModelWithError == null)
                        firstViewModelWithError = viewModel;
                }
            }

            errorMessage = errorMessageBuilder.ToString();
            return isValid;
        }

        private void Add()
        {
            var options = CreateNewSynchronizationProfileOrNull();
            if (options != null)
            {
                foreach (var vm in _optionsViewModelFactory.Create(new[] { options }, _generalOptions))
                    _options.Add(vm);
                ShowProfile(options.Id);
            }
        }

        private void AddMultiple()
        {
            ProfileType? profileType;
            var options = CreateNewSynchronizationProfileOrNull(out profileType);
            if (options != null)
            {
                // ReSharper disable once PossibleInvalidOperationException
                var optionsViewModel = _optionsViewModelFactory.CreateTemplate(options, _generalOptions, profileType.Value);
                _options.Add(optionsViewModel);
                ShowProfile(options.Id);
            }
        }


        private Contracts.Options CreateNewSynchronizationProfileOrNull()
        {
            ProfileType? type;
            return CreateNewSynchronizationProfileOrNull(out type);
        }

        private Contracts.Options CreateNewSynchronizationProfileOrNull(out ProfileType? type)
        {
            type = _uiService.QueryProfileType();
            if (!type.HasValue)
                return null;

            var options = Contracts.Options.CreateDefault(type.Value);
            switch (type)
            {
                case ProfileType.Google:
                    options.ServerAdapterType = ServerAdapterType.WebDavHttpClientBasedWithGoogleOAuth;
                    break;
                case ProfileType.Swisscom:
                    options.ServerAdapterType = ServerAdapterType.WebDavHttpClientBasedWithSwisscomOAuth;
                    break;
                default:
                    options.ServerAdapterType = ServerAdapterType.WebDavHttpClientBased;
                    break;
            }
            return options;
        }


        public ICommand AddCommand { get; }
        public ICommand AddMultipleCommand { get; }
        public ICommand CloseCommand { get; }
        public ICommand DeleteSelectedCommand { get; }
        public ICommand CopySelectedCommand { get; }
        public ICommand MoveSelectedUpCommand { get; }
        public ICommand MoveSelectedDownCommand { get; }
        public ICommand OpenProfileDataDirectoryCommand { get; }
        public ICommand ExpandAllCommand { get; }
        public ICommand CollapseAllCommand { get; }
        public ICommand ExportAllCommand { get; }
        public ICommand ImportCommand { get; }

        public ObservableCollection<IOptionsViewModel> Options => _options;


        public void SetOptionsCollection(Contracts.Options[] value, Guid? initialSelectedProfileId = null)
        {
            _options.Clear();
            foreach (var vm in _optionsViewModelFactory.Create(value, _generalOptions))
                _options.Add(vm);

            var initialSelectedProfile =
                (initialSelectedProfileId != null ? _options.FirstOrDefault(o => o.Id == initialSelectedProfileId.Value) : null)
                ?? _options.FirstOrDefault(o => o.IsActive)
                ?? _options.FirstOrDefault();

            if (initialSelectedProfile != null)
                initialSelectedProfile.IsSelected = true;

            if (_options.Count > 0 && _generalOptions.ExpandAllSyncProfiles)
                ExpandAll();
        }

        public Contracts.Options[] GetOptionsCollection()
        {
            var optionsCollection = new List<CalDavSynchronizer.Contracts.Options>();
            foreach (var viewModel in _options)
            {
                var options = viewModel.GetOptionsOrNull();
                if (options != null)
                    optionsCollection.Add(options);
            }
            return optionsCollection.ToArray();
        }

        private void Delete(IOptionsViewModel viewModel)
        {
            var index = _options.IndexOf(viewModel);
            _options.Remove(viewModel);
            if (_options.Count > 0)
                _options[Math.Max(0, Math.Min(_options.Count - 1, index))].IsSelected = true;
        }

        private void Copy(IOptionsViewModel viewModel)
        {
            var options = viewModel.GetOptionsOrNull();
            if (options != null)
            {
                options.Id = Guid.NewGuid();
                options.Name += " (Copy)";

                var index = _options.IndexOf(viewModel) + 1;

                foreach (var vm in _optionsViewModelFactory.Create(new[] { options }, _generalOptions))
                    _options.Insert(index, vm);

                ShowProfile(options.Id);
            }
        }

        public void RequestCacheDeletion(IOptionsViewModel viewModel)
        {

            s_logger.InfoFormat("Deleting cache for profile '{0}'", viewModel.Name);

            var profileDataDirectory = _profileDataDirectoryFactory(viewModel.Id);
            if (Directory.Exists(profileDataDirectory))
                Directory.Delete(profileDataDirectory, true);

            MessageBox.Show("A new intial sync will be performed with the next sync run!", "Profile cache deleted", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        public void RequestRemoval(IOptionsViewModel viewModel)
        {
            Delete(viewModel);
        }

        public void RequestAdd(IReadOnlyCollection<Contracts.Options> options)
        {
            foreach (var vm in _optionsViewModelFactory.Create(options, _generalOptions))
                _options.Add(vm);
            if (options.Any())
                ShowProfile(options.First().Id);
        }

        public static OptionsCollectionViewModel DesignInstance
        {
            get
            {
                {
                    var viewModel = new OptionsCollectionViewModel(
                        new GeneralOptions { AcceptInvalidCharsInServerResponse = true },
                        NullOutlookAccountPasswordProvider.Instance,
                        new[] { "Cat1", "Cat2" },
                        _ => string.Empty,
                        NullUiService.Instance, new OptionTasks(new DesignOutlookSession()));
                    var genericOptionsViewModel = GenericOptionsViewModel.DesignInstance;
                    genericOptionsViewModel.IsSelected = true;
                    viewModel.Options.Add(genericOptionsViewModel);
                    viewModel.Options.Add(GenericOptionsViewModel.DesignInstance);
                    return viewModel;
                }
            }
        }

        public void ShowProfile(Guid value)
        {
            var selectedProfile = _options.FirstOrDefault(o => o.Id == value);

            if (selectedProfile != null)
                selectedProfile.IsSelected = true;
        }

        public void BringToFront()
        {
            RequestBringIntoView?.Invoke(this, EventArgs.Empty);
        }
    }
}