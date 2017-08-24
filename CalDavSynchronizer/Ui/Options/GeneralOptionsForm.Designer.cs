﻿using System;

namespace CalDavSynchronizer.Ui.Options
{
  partial class GeneralOptionsForm
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose (bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose ();
      }
      base.Dispose (disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent ()
    {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GeneralOptionsForm));
            this._cancelButton = new System.Windows.Forms.Button();
            this._okButton = new System.Windows.Forms.Button();
            this._checkForNewerVersionsCheckBox = new System.Windows.Forms.CheckBox();
            this._storeDataInRoamingFolderCheckBox = new System.Windows.Forms.CheckBox();
            this._toolTip = new System.Windows.Forms.ToolTip(this.components);
            this._enableTls12Checkbox = new System.Windows.Forms.CheckBox();
            this._disableCertificateValidationCheckbox = new System.Windows.Forms.CheckBox();
            this._enableSsl3Checkbox = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this._enableClientCertificateCheckBox = new System.Windows.Forms.CheckBox();
            this._fixInvalidSettingsCheckBox = new System.Windows.Forms.CheckBox();
            this._checkIfOnlineCheckBox = new System.Windows.Forms.CheckBox();
            this._includeCustomMessageClassesCheckBox = new System.Windows.Forms.CheckBox();
            this._enableTrayIconCheckBox = new System.Windows.Forms.CheckBox();
            this._acceptInvalidCharsInServerResponseCheckBox = new System.Windows.Forms.CheckBox();
            this._triggerSyncAfterSendReceiveCheckBox = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this._expandAllSyncProfilesCheckBox = new System.Windows.Forms.CheckBox();
            this._useUnsafeHeaderParsingCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this._maxReportAgeInDays = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this._reportPopupModeComboBox = new System.Windows.Forms.ComboBox();
            this._reportLogModeComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this._showLogButton = new System.Windows.Forms.Button();
            this._clearLogButton = new System.Windows.Forms.Button();
            this._logLevelComboBox = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this._calDavConnectTimeoutTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // _cancelButton
            // 
            this._cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._cancelButton.Location = new System.Drawing.Point(295, 589);
            this._cancelButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this._cancelButton.Name = "_cancelButton";
            this._cancelButton.Size = new System.Drawing.Size(75, 23);
            this._cancelButton.TabIndex = 0;
            this._cancelButton.Text = "Cancel";
            this._cancelButton.UseVisualStyleBackColor = true;
            // 
            // _okButton
            // 
            this._okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._okButton.Location = new System.Drawing.Point(214, 589);
            this._okButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this._okButton.Name = "_okButton";
            this._okButton.Size = new System.Drawing.Size(75, 23);
            this._okButton.TabIndex = 1;
            this._okButton.Text = "OK";
            this._okButton.UseVisualStyleBackColor = true;
            this._okButton.Click += new System.EventHandler(this._okButton_Click);
            // 
            // _checkForNewerVersionsCheckBox
            // 
            this._checkForNewerVersionsCheckBox.AutoSize = true;
            this._checkForNewerVersionsCheckBox.Enabled = false;
            this._checkForNewerVersionsCheckBox.Location = new System.Drawing.Point(12, 12);
            this._checkForNewerVersionsCheckBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this._checkForNewerVersionsCheckBox.Name = "_checkForNewerVersionsCheckBox";
            this._checkForNewerVersionsCheckBox.Size = new System.Drawing.Size(210, 17);
            this._checkForNewerVersionsCheckBox.TabIndex = 2;
            this._checkForNewerVersionsCheckBox.Text = "Automatically check for newer versions";
            this._checkForNewerVersionsCheckBox.UseVisualStyleBackColor = true;
            // 
            // _storeDataInRoamingFolderCheckBox
            // 
            this._storeDataInRoamingFolderCheckBox.AutoSize = true;
            this._storeDataInRoamingFolderCheckBox.Location = new System.Drawing.Point(12, 53);
            this._storeDataInRoamingFolderCheckBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this._storeDataInRoamingFolderCheckBox.Name = "_storeDataInRoamingFolderCheckBox";
            this._storeDataInRoamingFolderCheckBox.Size = new System.Drawing.Size(155, 17);
            this._storeDataInRoamingFolderCheckBox.TabIndex = 3;
            this._storeDataInRoamingFolderCheckBox.Text = "Store data in roaming folder";
            this._toolTip.SetToolTip(this._storeDataInRoamingFolderCheckBox, "Changing this option requires a restart of Outlook.");
            this._storeDataInRoamingFolderCheckBox.UseVisualStyleBackColor = true;
            // 
            // _toolTip
            // 
            this._toolTip.AutoPopDelay = 30000;
            this._toolTip.InitialDelay = 500;
            this._toolTip.ReshowDelay = 100;
            // 
            // _enableTls12Checkbox
            // 
            this._enableTls12Checkbox.AutoSize = true;
            this._enableTls12Checkbox.Location = new System.Drawing.Point(9, 59);
            this._enableTls12Checkbox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this._enableTls12Checkbox.Name = "_enableTls12Checkbox";
            this._enableTls12Checkbox.Size = new System.Drawing.Size(94, 17);
            this._enableTls12Checkbox.TabIndex = 16;
            this._enableTls12Checkbox.Text = "Enable TLS12";
            this._toolTip.SetToolTip(this._enableTls12Checkbox, "Disabling is a major security risk, not recommended!");
            this._enableTls12Checkbox.UseVisualStyleBackColor = true;
            // 
            // _disableCertificateValidationCheckbox
            // 
            this._disableCertificateValidationCheckbox.AutoSize = true;
            this._disableCertificateValidationCheckbox.Checked = true;
            this._disableCertificateValidationCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this._disableCertificateValidationCheckbox.Location = new System.Drawing.Point(9, 19);
            this._disableCertificateValidationCheckbox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this._disableCertificateValidationCheckbox.Name = "_disableCertificateValidationCheckbox";
            this._disableCertificateValidationCheckbox.Size = new System.Drawing.Size(160, 17);
            this._disableCertificateValidationCheckbox.TabIndex = 14;
            this._disableCertificateValidationCheckbox.Text = "Disable Certificate Validation";
            this._toolTip.SetToolTip(this._disableCertificateValidationCheckbox, "Major security risk, not recommended!");
            this._disableCertificateValidationCheckbox.UseVisualStyleBackColor = true;
            // 
            // _enableSsl3Checkbox
            // 
            this._enableSsl3Checkbox.AutoSize = true;
            this._enableSsl3Checkbox.Location = new System.Drawing.Point(9, 80);
            this._enableSsl3Checkbox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this._enableSsl3Checkbox.Name = "_enableSsl3Checkbox";
            this._enableSsl3Checkbox.Size = new System.Drawing.Size(88, 17);
            this._enableSsl3Checkbox.TabIndex = 17;
            this._enableSsl3Checkbox.Text = "Enable SSL3";
            this._toolTip.SetToolTip(this._enableSsl3Checkbox, "Major security risk, not recommended!");
            this._enableSsl3Checkbox.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this._enableClientCertificateCheckBox);
            this.groupBox1.Controls.Add(this._disableCertificateValidationCheckbox);
            this.groupBox1.Controls.Add(this._enableSsl3Checkbox);
            this.groupBox1.Controls.Add(this._enableTls12Checkbox);
            this.groupBox1.Location = new System.Drawing.Point(3, 251);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Size = new System.Drawing.Size(375, 107);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "SSL/TLS settings";
            this._toolTip.SetToolTip(this.groupBox1, "Changing these options can be a major security risk, not recommended!");
            // 
            // _enableClientCertificateCheckBox
            // 
            this._enableClientCertificateCheckBox.AutoSize = true;
            this._enableClientCertificateCheckBox.Location = new System.Drawing.Point(9, 39);
            this._enableClientCertificateCheckBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this._enableClientCertificateCheckBox.Name = "_enableClientCertificateCheckBox";
            this._enableClientCertificateCheckBox.Size = new System.Drawing.Size(143, 17);
            this._enableClientCertificateCheckBox.TabIndex = 15;
            this._enableClientCertificateCheckBox.Text = "Enable Client Certificates";
            this._toolTip.SetToolTip(this._enableClientCertificateCheckBox, "Enable client certificates with automatic mode.\r\nThe available client certificate" +
        "s from the user store will be automatically provided.");
            this._enableClientCertificateCheckBox.UseVisualStyleBackColor = true;
            // 
            // _fixInvalidSettingsCheckBox
            // 
            this._fixInvalidSettingsCheckBox.AutoSize = true;
            this._fixInvalidSettingsCheckBox.Location = new System.Drawing.Point(12, 73);
            this._fixInvalidSettingsCheckBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this._fixInvalidSettingsCheckBox.Name = "_fixInvalidSettingsCheckBox";
            this._fixInvalidSettingsCheckBox.Size = new System.Drawing.Size(111, 17);
            this._fixInvalidSettingsCheckBox.TabIndex = 5;
            this._fixInvalidSettingsCheckBox.Text = "Fix invalid settings";
            this._toolTip.SetToolTip(this._fixInvalidSettingsCheckBox, resources.GetString("_fixInvalidSettingsCheckBox.ToolTip"));
            this._fixInvalidSettingsCheckBox.UseVisualStyleBackColor = true;
            // 
            // _checkIfOnlineCheckBox
            // 
            this._checkIfOnlineCheckBox.AutoSize = true;
            this._checkIfOnlineCheckBox.Location = new System.Drawing.Point(12, 32);
            this._checkIfOnlineCheckBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this._checkIfOnlineCheckBox.Name = "_checkIfOnlineCheckBox";
            this._checkIfOnlineCheckBox.Size = new System.Drawing.Size(228, 17);
            this._checkIfOnlineCheckBox.TabIndex = 3;
            this._checkIfOnlineCheckBox.Text = "Check Internet connection before sync run";
            this._toolTip.SetToolTip(this._checkIfOnlineCheckBox, resources.GetString("_checkIfOnlineCheckBox.ToolTip"));
            this._checkIfOnlineCheckBox.UseVisualStyleBackColor = true;
            // 
            // _includeCustomMessageClassesCheckBox
            // 
            this._includeCustomMessageClassesCheckBox.AutoSize = true;
            this._includeCustomMessageClassesCheckBox.Location = new System.Drawing.Point(12, 93);
            this._includeCustomMessageClassesCheckBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this._includeCustomMessageClassesCheckBox.Name = "_includeCustomMessageClassesCheckBox";
            this._includeCustomMessageClassesCheckBox.Size = new System.Drawing.Size(254, 17);
            this._includeCustomMessageClassesCheckBox.TabIndex = 6;
            this._includeCustomMessageClassesCheckBox.Text = "Include custom message classes in Outlook filter";
            this._toolTip.SetToolTip(this._includeCustomMessageClassesCheckBox, "Use prefix filter to include also custom message_classes in filter for Outlook fo" +
        "lders. \r\nFor better performance, Windows Search Service shouldn\'t be deactivated" +
        " if this option is enabled.");
            this._includeCustomMessageClassesCheckBox.UseVisualStyleBackColor = true;
            // 
            // _enableTrayIconCheckBox
            // 
            this._enableTrayIconCheckBox.AutoSize = true;
            this._enableTrayIconCheckBox.Location = new System.Drawing.Point(12, 114);
            this._enableTrayIconCheckBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this._enableTrayIconCheckBox.Name = "_enableTrayIconCheckBox";
            this._enableTrayIconCheckBox.Size = new System.Drawing.Size(107, 17);
            this._enableTrayIconCheckBox.TabIndex = 7;
            this._enableTrayIconCheckBox.Text = "Enable Tray Icon";
            this._toolTip.SetToolTip(this._enableTrayIconCheckBox, "Enables the systray icon in the Windows taskbar.");
            this._enableTrayIconCheckBox.UseVisualStyleBackColor = true;
            // 
            // _acceptInvalidCharsInServerResponseCheckBox
            // 
            this._acceptInvalidCharsInServerResponseCheckBox.AutoSize = true;
            this._acceptInvalidCharsInServerResponseCheckBox.Location = new System.Drawing.Point(12, 134);
            this._acceptInvalidCharsInServerResponseCheckBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this._acceptInvalidCharsInServerResponseCheckBox.Name = "_acceptInvalidCharsInServerResponseCheckBox";
            this._acceptInvalidCharsInServerResponseCheckBox.Size = new System.Drawing.Size(214, 17);
            this._acceptInvalidCharsInServerResponseCheckBox.TabIndex = 8;
            this._acceptInvalidCharsInServerResponseCheckBox.Text = "Accept invalid chars in server response.";
            this._toolTip.SetToolTip(this._acceptInvalidCharsInServerResponseCheckBox, "If checked invalid characters in XML server responses are allowed.\r\nA typical inv" +
        "alid char, sent by some servers is \'Form feed\' (0x0C).");
            this._acceptInvalidCharsInServerResponseCheckBox.UseVisualStyleBackColor = true;
            // 
            // _triggerSyncAfterSendReceiveCheckBox
            // 
            this._triggerSyncAfterSendReceiveCheckBox.AutoSize = true;
            this._triggerSyncAfterSendReceiveCheckBox.Location = new System.Drawing.Point(12, 176);
            this._triggerSyncAfterSendReceiveCheckBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this._triggerSyncAfterSendReceiveCheckBox.Name = "_triggerSyncAfterSendReceiveCheckBox";
            this._triggerSyncAfterSendReceiveCheckBox.Size = new System.Drawing.Size(294, 17);
            this._triggerSyncAfterSendReceiveCheckBox.TabIndex = 10;
            this._triggerSyncAfterSendReceiveCheckBox.Text = "Trigger sync after Outlook Send/Receive and on Startup";
            this._toolTip.SetToolTip(this._triggerSyncAfterSendReceiveCheckBox, "If checked a manual sync is always triggered after the Outlook Send/Receive finis" +
        "hes \r\nand on startup of Outlook.");
            this._triggerSyncAfterSendReceiveCheckBox.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(121, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Show reports and notify:";
            this._toolTip.SetToolTip(this.label2, "Show synchronization reports immediately and\r\nnotify in systray icon (if enabled)" +
        "");
            // 
            // _expandAllSyncProfilesCheckBox
            // 
            this._expandAllSyncProfilesCheckBox.AutoSize = true;
            this._expandAllSyncProfilesCheckBox.Location = new System.Drawing.Point(12, 228);
            this._expandAllSyncProfilesCheckBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this._expandAllSyncProfilesCheckBox.Name = "_expandAllSyncProfilesCheckBox";
            this._expandAllSyncProfilesCheckBox.Size = new System.Drawing.Size(233, 17);
            this._expandAllSyncProfilesCheckBox.TabIndex = 12;
            this._expandAllSyncProfilesCheckBox.Text = "Expand all nodes in Synchronization Profiles";
            this._toolTip.SetToolTip(this._expandAllSyncProfilesCheckBox, "Expand all nodes in the treeview of the Synchronization Profiles configuration by" +
        " default if enabled.");
            this._expandAllSyncProfilesCheckBox.UseVisualStyleBackColor = true;
            // 
            // _useUnsafeHeaderParsingCheckBox
            // 
            this._useUnsafeHeaderParsingCheckBox.AutoSize = true;
            this._useUnsafeHeaderParsingCheckBox.Location = new System.Drawing.Point(12, 154);
            this._useUnsafeHeaderParsingCheckBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this._useUnsafeHeaderParsingCheckBox.Name = "_useUnsafeHeaderParsingCheckBox";
            this._useUnsafeHeaderParsingCheckBox.Size = new System.Drawing.Size(183, 17);
            this._useUnsafeHeaderParsingCheckBox.TabIndex = 9;
            this._useUnsafeHeaderParsingCheckBox.Text = "Enable useUnsafeHeaderParsing";
            this._toolTip.SetToolTip(this._useUnsafeHeaderParsingCheckBox, "Enable only if you get the following error: \r\nSystem.Net.WebException: The server" +
        " committed a protocol violation. Section=ResponseStatusLine\r\nNeeded for Yahoo an" +
        "d cPanel Horde servers for example.");
            this._useUnsafeHeaderParsingCheckBox.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this._maxReportAgeInDays);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this._reportPopupModeComboBox);
            this.groupBox2.Controls.Add(this._reportLogModeComboBox);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(3, 363);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(375, 110);
            this.groupBox2.TabIndex = 18;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Synchronization reports";
            // 
            // _maxReportAgeInDays
            // 
            this._maxReportAgeInDays.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._maxReportAgeInDays.Location = new System.Drawing.Point(263, 77);
            this._maxReportAgeInDays.Name = "_maxReportAgeInDays";
            this._maxReportAgeInDays.Size = new System.Drawing.Size(99, 20);
            this._maxReportAgeInDays.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(157, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Delete reports older than (days):";
            // 
            // _reportPopupModeComboBox
            // 
            this._reportPopupModeComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._reportPopupModeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._reportPopupModeComboBox.FormattingEnabled = true;
            this._reportPopupModeComboBox.Location = new System.Drawing.Point(144, 50);
            this._reportPopupModeComboBox.Name = "_reportPopupModeComboBox";
            this._reportPopupModeComboBox.Size = new System.Drawing.Size(218, 21);
            this._reportPopupModeComboBox.TabIndex = 3;
            // 
            // _reportLogModeComboBox
            // 
            this._reportLogModeComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._reportLogModeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._reportLogModeComboBox.FormattingEnabled = true;
            this._reportLogModeComboBox.Location = new System.Drawing.Point(144, 23);
            this._reportLogModeComboBox.Name = "_reportLogModeComboBox";
            this._reportLogModeComboBox.Size = new System.Drawing.Size(218, 21);
            this._reportLogModeComboBox.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Log:";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this._showLogButton);
            this.groupBox3.Controls.Add(this._clearLogButton);
            this.groupBox3.Controls.Add(this._logLevelComboBox);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Location = new System.Drawing.Point(3, 479);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(375, 88);
            this.groupBox3.TabIndex = 19;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "General Logging";
            // 
            // _showLogButton
            // 
            this._showLogButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._showLogButton.Location = new System.Drawing.Point(9, 50);
            this._showLogButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this._showLogButton.Name = "_showLogButton";
            this._showLogButton.Size = new System.Drawing.Size(75, 23);
            this._showLogButton.TabIndex = 3;
            this._showLogButton.Text = "Show Log";
            this._showLogButton.UseVisualStyleBackColor = true;
            this._showLogButton.Click += new System.EventHandler(this._showLogButton_Click);
            // 
            // _clearLogButton
            // 
            this._clearLogButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._clearLogButton.Location = new System.Drawing.Point(144, 50);
            this._clearLogButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this._clearLogButton.Name = "_clearLogButton";
            this._clearLogButton.Size = new System.Drawing.Size(75, 23);
            this._clearLogButton.TabIndex = 4;
            this._clearLogButton.Text = "Clear Log";
            this._clearLogButton.UseVisualStyleBackColor = true;
            this._clearLogButton.Click += new System.EventHandler(this._clearLogButton_Click);
            // 
            // _logLevelComboBox
            // 
            this._logLevelComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._logLevelComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._logLevelComboBox.FormattingEnabled = true;
            this._logLevelComboBox.Location = new System.Drawing.Point(144, 23);
            this._logLevelComboBox.Name = "_logLevelComboBox";
            this._logLevelComboBox.Size = new System.Drawing.Size(218, 21);
            this._logLevelComboBox.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Log Level:";
            // 
            // _calDavConnectTimeoutTextBox
            // 
            this._calDavConnectTimeoutTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._calDavConnectTimeoutTextBox.Location = new System.Drawing.Point(264, 201);
            this._calDavConnectTimeoutTextBox.Name = "_calDavConnectTimeoutTextBox";
            this._calDavConnectTimeoutTextBox.Size = new System.Drawing.Size(99, 20);
            this._calDavConnectTimeoutTextBox.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 203);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(171, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "CalDav Connection Timeout (secs)";
            // 
            // GeneralOptionsForm
            // 
            this.AcceptButton = this._okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this._cancelButton;
            this.ClientSize = new System.Drawing.Size(377, 618);
            this.Controls.Add(this._useUnsafeHeaderParsingCheckBox);
            this.Controls.Add(this._expandAllSyncProfilesCheckBox);
            this.Controls.Add(this._calDavConnectTimeoutTextBox);
            this.Controls.Add(this._triggerSyncAfterSendReceiveCheckBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this._acceptInvalidCharsInServerResponseCheckBox);
            this.Controls.Add(this._enableTrayIconCheckBox);
            this.Controls.Add(this._checkIfOnlineCheckBox);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this._includeCustomMessageClassesCheckBox);
            this.Controls.Add(this._fixInvalidSettingsCheckBox);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this._storeDataInRoamingFolderCheckBox);
            this.Controls.Add(this._checkForNewerVersionsCheckBox);
            this.Controls.Add(this._okButton);
            this.Controls.Add(this._cancelButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "GeneralOptionsForm";
            this.Text = "General Options";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button _cancelButton;
    private System.Windows.Forms.Button _okButton;
    private System.Windows.Forms.CheckBox _checkForNewerVersionsCheckBox;
    private System.Windows.Forms.CheckBox _storeDataInRoamingFolderCheckBox;
    private System.Windows.Forms.ToolTip _toolTip;
    private System.Windows.Forms.CheckBox _enableTls12Checkbox;
    private System.Windows.Forms.CheckBox _disableCertificateValidationCheckbox;
    private System.Windows.Forms.CheckBox _enableSsl3Checkbox;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.CheckBox _fixInvalidSettingsCheckBox;
    private System.Windows.Forms.CheckBox _includeCustomMessageClassesCheckBox;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.ComboBox _reportPopupModeComboBox;
    private System.Windows.Forms.ComboBox _reportLogModeComboBox;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox _maxReportAgeInDays;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.GroupBox groupBox3;
    private System.Windows.Forms.Button _showLogButton;
    private System.Windows.Forms.Button _clearLogButton;
    private System.Windows.Forms.ComboBox _logLevelComboBox;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.CheckBox _checkIfOnlineCheckBox;
    private System.Windows.Forms.CheckBox _enableTrayIconCheckBox;
    private System.Windows.Forms.CheckBox _acceptInvalidCharsInServerResponseCheckBox;
    private System.Windows.Forms.CheckBox _triggerSyncAfterSendReceiveCheckBox;
    private System.Windows.Forms.TextBox _calDavConnectTimeoutTextBox;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.CheckBox _expandAllSyncProfilesCheckBox;
    private System.Windows.Forms.CheckBox _useUnsafeHeaderParsingCheckBox;
    private System.Windows.Forms.CheckBox _enableClientCertificateCheckBox;
  }
}