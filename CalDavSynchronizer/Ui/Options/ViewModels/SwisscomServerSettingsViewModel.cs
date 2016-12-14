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
using System.Reflection;
using System.Security;
using System.Text;
using System.Windows;
using System.Windows.Input;
using CalDavSynchronizer.Contracts;
using CalDavSynchronizer.Utilities;
using log4net;
using Microsoft.Office.Interop.Outlook;
using Exception = System.Exception;
using DotNetOpenAuth.OAuth2;
using CalDavSynchronizer.OAuth.Swisscom;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace CalDavSynchronizer.Ui.Options.ViewModels
{
    internal class SwisscomServerSettingsViewModel : ViewModelBase, IServerSettingsViewModel
    {
        private static readonly ILog s_logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private string _calenderUrl;
        private string _emailAddress;
        private SecureString _password;
        private bool _useAccountPassword;
        private string _userName;
        private readonly ISettingsFaultFinder _settingsFaultFinder;
        private readonly ICurrentOptions _currentOptions;
        private readonly IOutlookAccountPasswordProvider _outlookAccountPasswordProvider;
        private readonly DelegateCommandWithoutCanExecuteDelegation _testConnectionCommand;
        private readonly DelegateCommandWithoutCanExecuteDelegation _getAccountSettingsCommand;
        private readonly DelegateCommandWithoutCanExecuteDelegation _createDavResourceCommand;

        public SwisscomServerSettingsViewModel(ISettingsFaultFinder settingsFaultFinder, ICurrentOptions currentOptions, IOutlookAccountPasswordProvider outlookAccountPasswordProvider)
        {
            if (settingsFaultFinder == null)
                throw new ArgumentNullException(nameof(settingsFaultFinder));
            if (currentOptions == null)
                throw new ArgumentNullException(nameof(currentOptions));

            _settingsFaultFinder = settingsFaultFinder;
            _currentOptions = currentOptions;
            _outlookAccountPasswordProvider = outlookAccountPasswordProvider;

            _testConnectionCommand = new DelegateCommandWithoutCanExecuteDelegation(_ =>
            {
                ComponentContainer.EnsureSynchronizationContext();
                TestConnectionAsync();
            });
            _getAccountSettingsCommand = new DelegateCommandWithoutCanExecuteDelegation(_ =>
            {
                ComponentContainer.EnsureSynchronizationContext();
                GetAccountSettings();
            });
            _createDavResourceCommand = new DelegateCommandWithoutCanExecuteDelegation(_ =>
            {
                ComponentContainer.EnsureSynchronizationContext();
                CreateDavResource();
            });
        }

        public ICommand TestConnectionCommand => _testConnectionCommand;
        public ICommand GetAccountSettingsCommand => _getAccountSettingsCommand;
        public ICommand CreateDavResourceCommand => _createDavResourceCommand;

        public string CalenderUrl
        {
            get { return _calenderUrl; }
            set
            {
                CheckedPropertyChange(ref _calenderUrl, value);
            }
        }

        public string UserName
        {
            get { return _userName; }
            set
            {
                CheckedPropertyChange(ref _userName, value);
            }
        }

        public SecureString Password
        {
            get { return _password; }
            set
            {
                CheckedPropertyChange(ref _password, value);
            }
        }

        public string EmailAddress
        {
            get { return _emailAddress; }
            set
            {
                CheckedPropertyChange(ref _emailAddress, value);
            }
        }

        public bool UseAccountPassword
        {
            get { return _useAccountPassword; }
            set
            {
                CheckedPropertyChange(ref _useAccountPassword, value);
            }
        }

        public static SwisscomServerSettingsViewModel DesignInstance => new SwisscomServerSettingsViewModel(NullSettingsFaultFinder.Instance, new DesignCurrentOptions(), NullOutlookAccountPasswordProvider.Instance)
        {
            CalenderUrl = "http://calendar.url",
            Password = SecureStringUtility.ToSecureString("password"),
            UserName = "username"
        };

        public void SetOptions(Contracts.Options options)
        {
            CalenderUrl = options.CalenderUrl;
            UserName = options.UserName;
            Password = options.Password;
        }

        public void FillOptions(Contracts.Options options)
        {
            options.CalenderUrl = _calenderUrl;
            options.UserName = _userName;
            options.Password = _password;
            options.EmailAddress = _emailAddress;
            options.UseAccountPassword = _useAccountPassword;
            options.ServerAdapterType = ServerAdapterType.WebDavHttpClientBasedWithSwisscomOAuth;
        }

        public ServerAdapterType ServerAdapterType { get; } = ServerAdapterType.WebDavHttpClientBasedWithSwisscomOAuth;

        public bool IsGoogle { get; } = false;

        public bool Validate(StringBuilder errorMessageBuilder)
        {
            var result = OptionTasks.ValidateWebDavUrl(CalenderUrl, errorMessageBuilder, true);
            return result;
        }

        private void GetAccountSettings()
        {
            _getAccountSettingsCommand.SetCanExecute(false);
            try
            {
                var authServer = new AuthorizationServerDescription()
                {
                    AuthorizationEndpoint = new Uri("https://consent.sirius.test1.swisscom.com/c/oauth2/auth"),
                    TokenEndpoint = new Uri("https://apigw-test.it.bwns.ch/oauth2/token")
                };
                var client = new UserAgentClient(authServer, "SInWLXPnP8AADGSYSB0OdUKDxYvI6quy", "0JRbtFLcgKCxQup5");
                var authorizePopup = new Authorize(client);
                //authorizePopup.Owner = this;
                bool? result = authorizePopup.ShowDialog();
                if (result.HasValue && result.Value)
                {
                    var request = (HttpWebRequest)WebRequest.Create("https://apigw-test.it.bwns.ch/v2/carddav");
                    client.AuthorizeRequest(request, authorizePopup.Authorization);
                    using (var resourceResponse = request.GetResponse())
                    {
                        using (var responseStream = new StreamReader(resourceResponse.GetResponseStream()))
                        {
                            var json = responseStream.ReadToEnd();
                            var credentials = JsonConvert.DeserializeObject<Credentials>(json);
                            UserName = credentials.Username;
                            Password = SecureStringUtility.ToSecureString(credentials.Password);
                            CalenderUrl = credentials.Url;
                        }
                    }
                }
                //UserName = "blah";
                //Password = SecureStringUtility.ToSecureString("blah");
                //CalenderUrl = "url";
                /*
                var serverAccountSettings = _outlookAccountPasswordProvider.GetAccountServerSettings(_currentOptions.FolderAccountName);
                string path = !string.IsNullOrEmpty(CalenderUrl) ? new Uri(CalenderUrl).AbsolutePath : string.Empty;
                bool success;
                var dnsDiscoveredUrl = OptionTasks.DoSrvLookup(EmailAddress, OlItemType.olAppointmentItem, out success);
                CalenderUrl = success ? dnsDiscoveredUrl : "https://" + serverAccountSettings.ServerString + path;
                UserName = serverAccountSettings.UserName;
                */
            }
            catch (Exception x)
            {
                s_logger.Error("Exception while getting account settings.", x);
                string message = null;
                for (Exception ex = x; ex != null; ex = ex.InnerException)
                    message += ex.Message + Environment.NewLine;
                MessageBox.Show(message, "Account settings");
            }
            finally
            {
                _getAccountSettingsCommand.SetCanExecute(true);
            }
        }

        private async void TestConnectionAsync()
        {
            _testConnectionCommand.SetCanExecute(false);
            try
            {
                CalenderUrl = await OptionTasks.TestWebDavConnection(_currentOptions, _settingsFaultFinder, CalenderUrl, UserName);
            }
            catch (Exception x)
            {
                s_logger.Error("Exception while testing the connection.", x);
                string message = null;
                for (Exception ex = x; ex != null; ex = ex.InnerException)
                    message += ex.Message + Environment.NewLine;
                MessageBox.Show(message, OptionTasks.ConnectionTestCaption);
            }
            finally
            {
                _testConnectionCommand.SetCanExecute(true);
            }
        }

        private async void CreateDavResource()
        {
            _testConnectionCommand.SetCanExecute(false);
            _createDavResourceCommand.SetCanExecute(false);
            try
            {
                CalenderUrl = await OptionTasks.CreateDavResource(_currentOptions, CalenderUrl);
            }
            catch (Exception x)
            {
                s_logger.Error("Exception while adding a DAV resource.", x);
                string message = null;
                for (Exception ex = x; ex != null; ex = ex.InnerException)
                    message += ex.Message + Environment.NewLine;
                MessageBox.Show(message, OptionTasks.CreateDavResourceCaption);
            }
            finally
            {
                _testConnectionCommand.SetCanExecute(true);
                _createDavResourceCommand.SetCanExecute(true);
            }
        }
    }
}