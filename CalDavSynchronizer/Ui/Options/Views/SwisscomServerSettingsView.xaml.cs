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
using CalDavSynchronizer.Ui.Options.ViewModels;
using CalDavSynchronizer.Utilities;
using System;
using System.Security;
using System.Windows.Controls;

namespace CalDavSynchronizer.Ui.Options.Views
{
  /// <summary>
  ///   Interaction logic for ReportView.xaml
  /// </summary>
  public partial class SwisscomServerSettingsView : UserControl
  {
    private SwisscomServerSettingsViewModel _viewModel;

    public SwisscomServerSettingsView ()
    {
      InitializeComponent();
      DataContextChanged += ServerSettingsView_DataContextChanged;
      _passwordBox.PasswordChanged += PasswordBox_PasswordChanged;
    }

    private void PasswordBox_PasswordChanged (object sender, System.Windows.RoutedEventArgs e)
    {
        if (null != _viewModel)
        {
            _viewModel.Password = _passwordBox.SecurePassword;
        }
    }

    private void ServerSettingsView_DataContextChanged (object sender, System.Windows.DependencyPropertyChangedEventArgs e)
    {
      _viewModel = e.NewValue as SwisscomServerSettingsViewModel;
      if (_viewModel != null)
      {
        // Password is just a OneWayBinding. Therefore just set the initial value
        _passwordBox.Password = SecureStringUtility.ToUnsecureString (_viewModel.Password);
      }
    }

    }
}