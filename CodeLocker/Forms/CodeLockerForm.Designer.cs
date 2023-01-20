namespace CabalsCorner.CodeLocker.Forms
{
	partial class CodeLockerForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CodeLockerForm));
			this.opTimer = new System.Windows.Forms.Timer(this.components);
			this._errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
			this._ttUseMSTime = new System.Windows.Forms.ToolTip(this.components);
			this.btnDeleteLock = new System.Windows.Forms.Button();
			this.btnReset = new System.Windows.Forms.Button();
			this.chkUseMSTime = new System.Windows.Forms.CheckBox();
			this.txtDurationValue = new CabalsCorner.Controls.NumberTextbox();
			this.cmbDurationType = new System.Windows.Forms.ComboBox();
			this.cmbCodeType = new System.Windows.Forms.ComboBox();
			this.btnCustomDurationDialog = new System.Windows.Forms.Button();
			this.statusTimer = new System.Windows.Forms.Timer(this.components);
			this._nowTimer = new System.Windows.Forms.Timer(this.components);
			this.lblTime = new System.Windows.Forms.Label();
			this.gotTimeTimer = new System.Windows.Forms.Timer(this.components);
			this.singleTickTimer = new System.Windows.Forms.Timer(this.components);
			this.tt = new System.Windows.Forms.ToolTip(this.components);
			this.txtManualCode = new System.Windows.Forms.TextBox();
			this.btnCancelManualExpirationDate = new System.Windows.Forms.Button();
			this.btnError = new System.Windows.Forms.Button();
			this.cmbTimeout = new System.Windows.Forms.ComboBox();
			this.cmbCodeLength = new System.Windows.Forms.ComboBox();
			this.mainMenu = new System.Windows.Forms.MenuStrip();
			this.mnuProgramMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuActionsMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuCreateCodeLock = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuUnlockCode = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuDeleteCodeLock = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuSetManualExpirationDatetimeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuClearManualExpiration = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuClearLastUnlockedCode = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.otherSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuExportCodeLock = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuImportCodeLock = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuDisableTTs = new System.Windows.Forms.ToolStripMenuItem();
			this.pLeasureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuRandomVideos = new System.Windows.Forms.ToolStripMenuItem();
			this.pnlBody = new System.Windows.Forms.Panel();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnUnlockCode = new System.Windows.Forms.Button();
			this.btnLockCode = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.lblTimeout = new System.Windows.Forms.Label();
			this.grpOutput = new System.Windows.Forms.GroupBox();
			this.txtOutput = new System.Windows.Forms.TextBox();
			this.lblOutput = new System.Windows.Forms.Label();
			this.grpCodeLockDetails = new System.Windows.Forms.GroupBox();
			this.lblManualCode = new System.Windows.Forms.Label();
			this.grpDuration = new System.Windows.Forms.GroupBox();
			this.lblDurationValue = new System.Windows.Forms.Label();
			this.lblDurationType = new System.Windows.Forms.Label();
			this.lblCodeLength = new System.Windows.Forms.Label();
			this.lblCodeType = new System.Windows.Forms.Label();
			this.status = new System.Windows.Forms.ToolStripStatusLabel();
			this.opTime = new System.Windows.Forms.ToolStripStatusLabel();
			this.ss = new System.Windows.Forms.StatusStrip();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.folderBrowser = new System.Windows.Forms.FolderBrowserDialog();
			((System.ComponentModel.ISupportInitialize)(this._errorProvider)).BeginInit();
			this.mainMenu.SuspendLayout();
			this.pnlBody.SuspendLayout();
			this.grpOutput.SuspendLayout();
			this.grpCodeLockDetails.SuspendLayout();
			this.grpDuration.SuspendLayout();
			this.ss.SuspendLayout();
			this.SuspendLayout();
			// 
			// opTimer
			// 
			this.opTimer.Interval = 1000;
			this.opTimer.Tick += new System.EventHandler(this.opTimer_Tick);
			// 
			// _errorProvider
			// 
			this._errorProvider.ContainerControl = this;
			// 
			// _ttUseMSTime
			// 
			this._ttUseMSTime.IsBalloon = true;
			// 
			// btnDeleteLock
			// 
			this.btnDeleteLock.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteLock.Image")));
			this.btnDeleteLock.Location = new System.Drawing.Point(5, 264);
			this.btnDeleteLock.Margin = new System.Windows.Forms.Padding(2);
			this.btnDeleteLock.Name = "btnDeleteLock";
			this.btnDeleteLock.Size = new System.Drawing.Size(40, 37);
			this.btnDeleteLock.TabIndex = 28;
			this._ttUseMSTime.SetToolTip(this.btnDeleteLock, "Delete existing code lock");
			this.btnDeleteLock.UseVisualStyleBackColor = true;
			this.btnDeleteLock.Click += new System.EventHandler(this.btnDeleteLock_Click);
			// 
			// btnReset
			// 
			this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnReset.BackColor = System.Drawing.SystemColors.Control;
			this.btnReset.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnReset.Image = ((System.Drawing.Image)(resources.GetObject("btnReset.Image")));
			this.btnReset.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnReset.Location = new System.Drawing.Point(40, 326);
			this.btnReset.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.btnReset.Name = "btnReset";
			this.btnReset.Size = new System.Drawing.Size(114, 44);
			this.btnReset.TabIndex = 26;
			this.btnReset.Text = "Re&set";
			this._ttUseMSTime.SetToolTip(this.btnReset, "Reset form.");
			this.btnReset.UseVisualStyleBackColor = false;
			this.btnReset.Visible = false;
			this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
			// 
			// chkUseMSTime
			// 
			this.chkUseMSTime.AutoSize = true;
			this.chkUseMSTime.Location = new System.Drawing.Point(505, 270);
			this.chkUseMSTime.Margin = new System.Windows.Forms.Padding(2);
			this.chkUseMSTime.Name = "chkUseMSTime";
			this.chkUseMSTime.Size = new System.Drawing.Size(128, 19);
			this.chkUseMSTime.TabIndex = 19;
			this.chkUseMSTime.Text = "Use Microsoft Time";
			this.tt.SetToolTip(this.chkUseMSTime, "Get time from time.windows.com.");
			this.chkUseMSTime.UseVisualStyleBackColor = true;
			this.chkUseMSTime.CheckedChanged += new System.EventHandler(this.chkUseMSTime_CheckedChanged);
			// 
			// txtDurationValue
			// 
			this.txtDurationValue.BackColor = System.Drawing.SystemColors.Window;
			this.txtDurationValue.Font = new System.Drawing.Font("Courier New", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.txtDurationValue.Location = new System.Drawing.Point(220, 23);
			this.txtDurationValue.Margin = new System.Windows.Forms.Padding(5);
			this.txtDurationValue.MaxNumberValue = 922337203685.478D;
			this.txtDurationValue.Name = "txtDurationValue";
			this.txtDurationValue.OriginalBackColor = System.Drawing.SystemColors.Window;
			this.txtDurationValue.Size = new System.Drawing.Size(224, 23);
			this.txtDurationValue.TabIndex = 10;
			this.txtDurationValue.Text = "9";
			this.tt.SetToolTip(this.txtDurationValue, "Duration of code lock");
			this.txtDurationValue.ValidationSuccessful = true;
			this.txtDurationValue.NumberValidated += new System.EventHandler(this.txtDurationValue_NumberValidated);
			this.txtDurationValue.TextAlignChanged += new System.EventHandler(this.txtDurationValue_TextChanged);
			this.txtDurationValue.TextChanged += new System.EventHandler(this.txtDurationValue_TextChanged);
			this.txtDurationValue.Enter += new System.EventHandler(this.txtDurationValue_Enter);
			this.txtDurationValue.Leave += new System.EventHandler(this.txtDurationValue_Leave);
			// 
			// cmbDurationType
			// 
			this.cmbDurationType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbDurationType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.cmbDurationType.Font = new System.Drawing.Font("Courier New", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.cmbDurationType.FormattingEnabled = true;
			this.cmbDurationType.Items.AddRange(new object[] {
            "Days",
            "Hours",
            "Minutes",
            "Seconds"});
			this.cmbDurationType.Location = new System.Drawing.Point(58, 22);
			this.cmbDurationType.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.cmbDurationType.Name = "cmbDurationType";
			this.cmbDurationType.Size = new System.Drawing.Size(96, 24);
			this.cmbDurationType.TabIndex = 9;
			this.tt.SetToolTip(this.cmbDurationType, "Choose units to use for code lock duration");
			this.cmbDurationType.SelectedIndexChanged += new System.EventHandler(this.cmbDurationType_SelectedIndexChanged);
			// 
			// cmbCodeType
			// 
			this.cmbCodeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbCodeType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.cmbCodeType.Font = new System.Drawing.Font("Courier New", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.cmbCodeType.FormattingEnabled = true;
			this.cmbCodeType.Items.AddRange(new object[] {
            "CreateRandomNumericCode",
            "CreateRandomNumbersAndLettersCode",
            "ManuallyEnterCode"});
			this.cmbCodeType.Location = new System.Drawing.Point(108, 27);
			this.cmbCodeType.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.cmbCodeType.Name = "cmbCodeType";
			this.cmbCodeType.Size = new System.Drawing.Size(366, 24);
			this.cmbCodeType.TabIndex = 6;
			this.tt.SetToolTip(this.cmbCodeType, "Choose whether you want CodeLocker to generate a random code, or if you want to i" +
        "nput your own code");
			this.cmbCodeType.SelectedIndexChanged += new System.EventHandler(this.cmbCodeType_SelectedIndexChanged);
			// 
			// btnCustomDurationDialog
			// 
			this.btnCustomDurationDialog.Image = ((System.Drawing.Image)(resources.GetObject("btnCustomDurationDialog.Image")));
			this.btnCustomDurationDialog.Location = new System.Drawing.Point(598, 120);
			this.btnCustomDurationDialog.Margin = new System.Windows.Forms.Padding(2);
			this.btnCustomDurationDialog.Name = "btnCustomDurationDialog";
			this.btnCustomDurationDialog.Size = new System.Drawing.Size(40, 37);
			this.btnCustomDurationDialog.TabIndex = 13;
			this.tt.SetToolTip(this.btnCustomDurationDialog, "Choose date/time for code lock");
			this.btnCustomDurationDialog.UseVisualStyleBackColor = true;
			this.btnCustomDurationDialog.Click += new System.EventHandler(this.btnCustomDurationDialog_Click);
			// 
			// statusTimer
			// 
			this.statusTimer.Interval = 5000;
			this.statusTimer.Tick += new System.EventHandler(this.statusTimer_Tick);
			// 
			// _nowTimer
			// 
			this._nowTimer.Tick += new System.EventHandler(this._nowTimer_Tick);
			// 
			// lblTime
			// 
			this.lblTime.Font = new System.Drawing.Font("Courier New", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblTime.ForeColor = System.Drawing.Color.DarkBlue;
			this.lblTime.Location = new System.Drawing.Point(387, 343);
			this.lblTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblTime.Name = "lblTime";
			this.lblTime.Size = new System.Drawing.Size(113, 44);
			this.lblTime.TabIndex = 16;
			this.lblTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// gotTimeTimer
			// 
			this.gotTimeTimer.Interval = 2500;
			this.gotTimeTimer.Tick += new System.EventHandler(this.gotTimeTimer_Tick);
			// 
			// singleTickTimer
			// 
			this.singleTickTimer.Interval = 4000;
			this.singleTickTimer.Tick += new System.EventHandler(this.singleTickTimer_Tick);
			// 
			// tt
			// 
			this.tt.AutomaticDelay = 100;
			this.tt.AutoPopDelay = 20000;
			this.tt.InitialDelay = 100;
			this.tt.IsBalloon = true;
			this.tt.ReshowDelay = 20;
			this.tt.ShowAlways = true;
			this.tt.Popup += new System.Windows.Forms.PopupEventHandler(this.tt_Popup);
			// 
			// txtManualCode
			// 
			this.txtManualCode.Font = new System.Drawing.Font("Courier New", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.txtManualCode.Location = new System.Drawing.Point(108, 70);
			this.txtManualCode.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.txtManualCode.Name = "txtManualCode";
			this.txtManualCode.Size = new System.Drawing.Size(528, 23);
			this.txtManualCode.TabIndex = 8;
			this.tt.SetToolTip(this.txtManualCode, "Enter any keyboard character except \"space\".  Length cannot exceed 35 characters." +
        "");
			this.txtManualCode.TextChanged += new System.EventHandler(this.txtManualCode_TextChanged);
			// 
			// btnCancelManualExpirationDate
			// 
			this.btnCancelManualExpirationDate.Location = new System.Drawing.Point(597, 122);
			this.btnCancelManualExpirationDate.Margin = new System.Windows.Forms.Padding(2);
			this.btnCancelManualExpirationDate.Name = "btnCancelManualExpirationDate";
			this.btnCancelManualExpirationDate.Size = new System.Drawing.Size(40, 37);
			this.btnCancelManualExpirationDate.TabIndex = 14;
			this.tt.SetToolTip(this.btnCancelManualExpirationDate, "Delete custom code-lock expiration date/time.");
			this.btnCancelManualExpirationDate.UseVisualStyleBackColor = true;
			this.btnCancelManualExpirationDate.Visible = false;
			this.btnCancelManualExpirationDate.Click += new System.EventHandler(this.btnCancelManualExpirationDate_Click);
			// 
			// btnError
			// 
			this.btnError.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnError.BackColor = System.Drawing.SystemColors.Control;
			this.btnError.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnError.Location = new System.Drawing.Point(40, 327);
			this.btnError.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.btnError.Name = "btnError";
			this.btnError.Size = new System.Drawing.Size(114, 44);
			this.btnError.TabIndex = 25;
			this.btnError.Text = "&Error...";
			this.tt.SetToolTip(this.btnError, "View error details...");
			this.btnError.UseVisualStyleBackColor = false;
			this.btnError.Visible = false;
			this.btnError.Click += new System.EventHandler(this.btnError_Click);
			// 
			// cmbTimeout
			// 
			this.cmbTimeout.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbTimeout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.cmbTimeout.Font = new System.Drawing.Font("Courier New", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.cmbTimeout.FormattingEnabled = true;
			this.cmbTimeout.Items.AddRange(new object[] {
            "3000",
            "4000",
            "5000",
            "6000",
            "7000",
            "8000",
            "9000",
            "10000",
            "11000",
            "12000",
            "13000",
            "14000",
            "15000"});
			this.cmbTimeout.Location = new System.Drawing.Point(405, 265);
			this.cmbTimeout.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.cmbTimeout.Name = "cmbTimeout";
			this.cmbTimeout.Size = new System.Drawing.Size(84, 24);
			this.cmbTimeout.TabIndex = 18;
			this.tt.SetToolTip(this.cmbTimeout, "Choose how long CodeLocker tries to fetch time before aborting");
			this.cmbTimeout.SelectedIndexChanged += new System.EventHandler(this.cmbTimeout_SelectedIndexChanged);
			// 
			// cmbCodeLength
			// 
			this.cmbCodeLength.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbCodeLength.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.cmbCodeLength.Font = new System.Drawing.Font("Courier New", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.cmbCodeLength.FormattingEnabled = true;
			this.cmbCodeLength.Items.AddRange(new object[] {
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
			this.cmbCodeLength.Location = new System.Drawing.Point(583, 27);
			this.cmbCodeLength.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.cmbCodeLength.Name = "cmbCodeLength";
			this.cmbCodeLength.Size = new System.Drawing.Size(53, 24);
			this.cmbCodeLength.TabIndex = 7;
			this.tt.SetToolTip(this.cmbCodeLength, "Enter length of randomly generated code");
			this.cmbCodeLength.SelectedIndexChanged += new System.EventHandler(this.cmbCodeLength_SelectedIndexChanged);
			// 
			// mainMenu
			// 
			this.mainMenu.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
			this.mainMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuProgramMenu,
            this.mnuActionsMenu,
            this.pLeasureToolStripMenuItem});
			this.mainMenu.Location = new System.Drawing.Point(0, 0);
			this.mainMenu.Name = "mainMenu";
			this.mainMenu.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
			this.mainMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
			this.mainMenu.Size = new System.Drawing.Size(660, 24);
			this.mainMenu.TabIndex = 18;
			this.mainMenu.Text = "menuStrip1";
			// 
			// mnuProgramMenu
			// 
			this.mnuProgramMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem,
            this.toolStripSeparator1,
            this.mnuExit});
			this.mnuProgramMenu.Name = "mnuProgramMenu";
			this.mnuProgramMenu.Size = new System.Drawing.Size(65, 20);
			this.mnuProgramMenu.Text = "&Program";
			// 
			// aboutToolStripMenuItem
			// 
			this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			this.aboutToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
			this.aboutToolStripMenuItem.Text = "&About...";
			this.aboutToolStripMenuItem.Click += new System.EventHandler(this.mnuAbout_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(133, 6);
			// 
			// mnuExit
			// 
			this.mnuExit.Image = ((System.Drawing.Image)(resources.GetObject("mnuExit.Image")));
			this.mnuExit.Name = "mnuExit";
			this.mnuExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
			this.mnuExit.Size = new System.Drawing.Size(136, 22);
			this.mnuExit.Text = "&Exit";
			this.mnuExit.Click += new System.EventHandler(this.mnuExit_Click);
			// 
			// mnuActionsMenu
			// 
			this.mnuActionsMenu.Checked = true;
			this.mnuActionsMenu.CheckState = System.Windows.Forms.CheckState.Checked;
			this.mnuActionsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCreateCodeLock,
            this.mnuUnlockCode,
            this.mnuDeleteCodeLock,
            this.toolStripSeparator3,
            this.mnuSetManualExpirationDatetimeToolStripMenuItem,
            this.mnuClearManualExpiration,
            this.toolStripSeparator2,
            this.mnuClearLastUnlockedCode,
            this.toolStripSeparator4,
            this.otherSettingsToolStripMenuItem,
            this.mnuExportCodeLock,
            this.mnuImportCodeLock,
            this.toolStripSeparator5,
            this.mnuDisableTTs});
			this.mnuActionsMenu.Name = "mnuActionsMenu";
			this.mnuActionsMenu.Size = new System.Drawing.Size(59, 20);
			this.mnuActionsMenu.Text = "&Actions";
			// 
			// mnuCreateCodeLock
			// 
			this.mnuCreateCodeLock.Image = ((System.Drawing.Image)(resources.GetObject("mnuCreateCodeLock.Image")));
			this.mnuCreateCodeLock.Name = "mnuCreateCodeLock";
			this.mnuCreateCodeLock.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
			this.mnuCreateCodeLock.Size = new System.Drawing.Size(302, 26);
			this.mnuCreateCodeLock.Text = "Create code-&lock";
			this.mnuCreateCodeLock.Click += new System.EventHandler(this.mnuCreateCodeLock_Click);
			// 
			// mnuUnlockCode
			// 
			this.mnuUnlockCode.Image = ((System.Drawing.Image)(resources.GetObject("mnuUnlockCode.Image")));
			this.mnuUnlockCode.Name = "mnuUnlockCode";
			this.mnuUnlockCode.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.U)));
			this.mnuUnlockCode.Size = new System.Drawing.Size(302, 26);
			this.mnuUnlockCode.Text = "&Unlock existing code-lock";
			this.mnuUnlockCode.Click += new System.EventHandler(this.mnuUnlockCode_Click);
			// 
			// mnuDeleteCodeLock
			// 
			this.mnuDeleteCodeLock.Name = "mnuDeleteCodeLock";
			this.mnuDeleteCodeLock.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
			this.mnuDeleteCodeLock.Size = new System.Drawing.Size(302, 26);
			this.mnuDeleteCodeLock.Text = "&Delete code-lock";
			this.mnuDeleteCodeLock.Click += new System.EventHandler(this.mnuDeleteCodeLock_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(299, 6);
			// 
			// mnuSetManualExpirationDatetimeToolStripMenuItem
			// 
			this.mnuSetManualExpirationDatetimeToolStripMenuItem.Name = "mnuSetManualExpirationDatetimeToolStripMenuItem";
			this.mnuSetManualExpirationDatetimeToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
			this.mnuSetManualExpirationDatetimeToolStripMenuItem.Size = new System.Drawing.Size(302, 26);
			this.mnuSetManualExpirationDatetimeToolStripMenuItem.Text = "&Set manual expiration date/time...";
			this.mnuSetManualExpirationDatetimeToolStripMenuItem.Click += new System.EventHandler(this.mnuSetManualExpirationDatetimeToolStripMenuItem_Click);
			// 
			// mnuClearManualExpiration
			// 
			this.mnuClearManualExpiration.Name = "mnuClearManualExpiration";
			this.mnuClearManualExpiration.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H)));
			this.mnuClearManualExpiration.Size = new System.Drawing.Size(302, 26);
			this.mnuClearManualExpiration.Text = "&Clear manual expiration date/time";
			this.mnuClearManualExpiration.Click += new System.EventHandler(this.mnuClearManualExpiration_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(299, 6);
			// 
			// mnuClearLastUnlockedCode
			// 
			this.mnuClearLastUnlockedCode.Name = "mnuClearLastUnlockedCode";
			this.mnuClearLastUnlockedCode.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
			this.mnuClearLastUnlockedCode.Size = new System.Drawing.Size(302, 26);
			this.mnuClearLastUnlockedCode.Text = "Clea&r last unlocked code-lock";
			this.mnuClearLastUnlockedCode.Click += new System.EventHandler(this.mnuClearLastUnlockedCode_Click);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(299, 6);
			// 
			// otherSettingsToolStripMenuItem
			// 
			this.otherSettingsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("otherSettingsToolStripMenuItem.Image")));
			this.otherSettingsToolStripMenuItem.Name = "otherSettingsToolStripMenuItem";
			this.otherSettingsToolStripMenuItem.Size = new System.Drawing.Size(302, 26);
			this.otherSettingsToolStripMenuItem.Text = "&Other settings...";
			this.otherSettingsToolStripMenuItem.Visible = false;
			this.otherSettingsToolStripMenuItem.Click += new System.EventHandler(this.otherSettingsToolStripMenuItem_Click);
			// 
			// mnuExportCodeLock
			// 
			this.mnuExportCodeLock.Image = ((System.Drawing.Image)(resources.GetObject("mnuExportCodeLock.Image")));
			this.mnuExportCodeLock.Name = "mnuExportCodeLock";
			this.mnuExportCodeLock.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
			this.mnuExportCodeLock.Size = new System.Drawing.Size(302, 26);
			this.mnuExportCodeLock.Text = "E&xport code lock";
			this.mnuExportCodeLock.Click += new System.EventHandler(this.mnuExportCodeLock_Click);
			// 
			// mnuImportCodeLock
			// 
			this.mnuImportCodeLock.Image = ((System.Drawing.Image)(resources.GetObject("mnuImportCodeLock.Image")));
			this.mnuImportCodeLock.Name = "mnuImportCodeLock";
			this.mnuImportCodeLock.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
			this.mnuImportCodeLock.Size = new System.Drawing.Size(302, 26);
			this.mnuImportCodeLock.Text = "&Import code lock";
			this.mnuImportCodeLock.Click += new System.EventHandler(this.mnuImportCodeLock_Click);
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(299, 6);
			// 
			// mnuDisableTTs
			// 
			this.mnuDisableTTs.Checked = true;
			this.mnuDisableTTs.CheckState = System.Windows.Forms.CheckState.Checked;
			this.mnuDisableTTs.Name = "mnuDisableTTs";
			this.mnuDisableTTs.Size = new System.Drawing.Size(302, 26);
			this.mnuDisableTTs.Text = "Toolti&ps";
			this.mnuDisableTTs.Click += new System.EventHandler(this.mnuDisableTTs_Click);
			// 
			// pLeasureToolStripMenuItem
			// 
			this.pLeasureToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuRandomVideos});
			this.pLeasureToolStripMenuItem.Name = "pLeasureToolStripMenuItem";
			this.pLeasureToolStripMenuItem.Size = new System.Drawing.Size(63, 20);
			this.pLeasureToolStripMenuItem.Text = "P&leasure";
			this.pLeasureToolStripMenuItem.Visible = false;
			// 
			// mnuRandomVideos
			// 
			this.mnuRandomVideos.Name = "mnuRandomVideos";
			this.mnuRandomVideos.Size = new System.Drawing.Size(156, 22);
			this.mnuRandomVideos.Text = "&Random videos";
			this.mnuRandomVideos.Click += new System.EventHandler(this.mnuRandomVideos_Click);
			// 
			// pnlBody
			// 
			this.pnlBody.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pnlBody.Controls.Add(this.btnDeleteLock);
			this.pnlBody.Controls.Add(this.btnCancel);
			this.pnlBody.Controls.Add(this.btnUnlockCode);
			this.pnlBody.Controls.Add(this.btnLockCode);
			this.pnlBody.Controls.Add(this.btnClose);
			this.pnlBody.Controls.Add(this.lblTimeout);
			this.pnlBody.Controls.Add(this.cmbTimeout);
			this.pnlBody.Controls.Add(this.chkUseMSTime);
			this.pnlBody.Controls.Add(this.grpOutput);
			this.pnlBody.Controls.Add(this.grpCodeLockDetails);
			this.pnlBody.Controls.Add(this.btnReset);
			this.pnlBody.Controls.Add(this.btnError);
			this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlBody.Location = new System.Drawing.Point(0, 24);
			this.pnlBody.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.pnlBody.Name = "pnlBody";
			this.pnlBody.Size = new System.Drawing.Size(660, 380);
			this.pnlBody.TabIndex = 19;
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.BackColor = System.Drawing.SystemColors.Control;
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
			this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnCancel.Location = new System.Drawing.Point(411, 327);
			this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(114, 44);
			this.btnCancel.TabIndex = 24;
			this.btnCancel.Text = "Ca&ncel";
			this.btnCancel.UseVisualStyleBackColor = false;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnUnlockCode
			// 
			this.btnUnlockCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnUnlockCode.BackColor = System.Drawing.SystemColors.Control;
			this.btnUnlockCode.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnUnlockCode.Image = ((System.Drawing.Image)(resources.GetObject("btnUnlockCode.Image")));
			this.btnUnlockCode.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnUnlockCode.Location = new System.Drawing.Point(163, 327);
			this.btnUnlockCode.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.btnUnlockCode.Name = "btnUnlockCode";
			this.btnUnlockCode.Size = new System.Drawing.Size(114, 44);
			this.btnUnlockCode.TabIndex = 22;
			this.btnUnlockCode.Text = "&Unlock Code ";
			this.btnUnlockCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnUnlockCode.UseVisualStyleBackColor = false;
			this.btnUnlockCode.Click += new System.EventHandler(this.btnUnlockCode_Click);
			// 
			// btnLockCode
			// 
			this.btnLockCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnLockCode.BackColor = System.Drawing.SystemColors.Control;
			this.btnLockCode.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnLockCode.Image = ((System.Drawing.Image)(resources.GetObject("btnLockCode.Image")));
			this.btnLockCode.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnLockCode.Location = new System.Drawing.Point(287, 327);
			this.btnLockCode.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.btnLockCode.Name = "btnLockCode";
			this.btnLockCode.Size = new System.Drawing.Size(114, 44);
			this.btnLockCode.TabIndex = 23;
			this.btnLockCode.Text = "&Lock Code   ";
			this.btnLockCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnLockCode.UseVisualStyleBackColor = false;
			this.btnLockCode.Click += new System.EventHandler(this.btnLockCode_Click);
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.BackColor = System.Drawing.SystemColors.Control;
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
			this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnClose.Location = new System.Drawing.Point(534, 327);
			this.btnClose.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(114, 44);
			this.btnClose.TabIndex = 27;
			this.btnClose.Text = "&Close         ";
			this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnClose.UseVisualStyleBackColor = false;
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// lblTimeout
			// 
			this.lblTimeout.AutoSize = true;
			this.lblTimeout.Location = new System.Drawing.Point(317, 272);
			this.lblTimeout.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblTimeout.Name = "lblTimeout";
			this.lblTimeout.Size = new System.Drawing.Size(81, 15);
			this.lblTimeout.TabIndex = 20;
			this.lblTimeout.Text = "Timeout (ms):";
			// 
			// grpOutput
			// 
			this.grpOutput.Controls.Add(this.txtOutput);
			this.grpOutput.Controls.Add(this.lblOutput);
			this.grpOutput.Location = new System.Drawing.Point(5, 190);
			this.grpOutput.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.grpOutput.Name = "grpOutput";
			this.grpOutput.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.grpOutput.Size = new System.Drawing.Size(644, 68);
			this.grpOutput.TabIndex = 17;
			this.grpOutput.TabStop = false;
			this.grpOutput.Text = "Output";
			// 
			// txtOutput
			// 
			this.txtOutput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtOutput.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.txtOutput.Location = new System.Drawing.Point(108, 22);
			this.txtOutput.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.txtOutput.Name = "txtOutput";
			this.txtOutput.ReadOnly = true;
			this.txtOutput.Size = new System.Drawing.Size(528, 29);
			this.txtOutput.TabIndex = 0;
			this.txtOutput.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// lblOutput
			// 
			this.lblOutput.AutoSize = true;
			this.lblOutput.Location = new System.Drawing.Point(14, 31);
			this.lblOutput.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblOutput.Name = "lblOutput";
			this.lblOutput.Size = new System.Drawing.Size(58, 15);
			this.lblOutput.TabIndex = 0;
			this.lblOutput.Text = "lblOutput";
			// 
			// grpCodeLockDetails
			// 
			this.grpCodeLockDetails.Controls.Add(this.txtManualCode);
			this.grpCodeLockDetails.Controls.Add(this.lblManualCode);
			this.grpCodeLockDetails.Controls.Add(this.grpDuration);
			this.grpCodeLockDetails.Controls.Add(this.cmbCodeLength);
			this.grpCodeLockDetails.Controls.Add(this.lblCodeLength);
			this.grpCodeLockDetails.Controls.Add(this.cmbCodeType);
			this.grpCodeLockDetails.Controls.Add(this.lblCodeType);
			this.grpCodeLockDetails.Controls.Add(this.btnCustomDurationDialog);
			this.grpCodeLockDetails.Controls.Add(this.btnCancelManualExpirationDate);
			this.grpCodeLockDetails.Location = new System.Drawing.Point(5, 3);
			this.grpCodeLockDetails.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.grpCodeLockDetails.Name = "grpCodeLockDetails";
			this.grpCodeLockDetails.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.grpCodeLockDetails.Size = new System.Drawing.Size(644, 179);
			this.grpCodeLockDetails.TabIndex = 16;
			this.grpCodeLockDetails.TabStop = false;
			this.grpCodeLockDetails.Text = "Code Lock Details";
			// 
			// lblManualCode
			// 
			this.lblManualCode.AutoSize = true;
			this.lblManualCode.Location = new System.Drawing.Point(14, 76);
			this.lblManualCode.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblManualCode.Name = "lblManualCode";
			this.lblManualCode.Size = new System.Drawing.Size(79, 15);
			this.lblManualCode.TabIndex = 12;
			this.lblManualCode.Text = "Manual code:";
			// 
			// grpDuration
			// 
			this.grpDuration.Controls.Add(this.txtDurationValue);
			this.grpDuration.Controls.Add(this.lblDurationValue);
			this.grpDuration.Controls.Add(this.cmbDurationType);
			this.grpDuration.Controls.Add(this.lblDurationType);
			this.grpDuration.Location = new System.Drawing.Point(108, 105);
			this.grpDuration.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.grpDuration.Name = "grpDuration";
			this.grpDuration.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.grpDuration.Size = new System.Drawing.Size(468, 61);
			this.grpDuration.TabIndex = 11;
			this.grpDuration.TabStop = false;
			this.grpDuration.Text = "Duration";
			// 
			// lblDurationValue
			// 
			this.lblDurationValue.AutoSize = true;
			this.lblDurationValue.Location = new System.Drawing.Point(162, 29);
			this.lblDurationValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblDurationValue.Name = "lblDurationValue";
			this.lblDurationValue.Size = new System.Drawing.Size(56, 15);
			this.lblDurationValue.TabIndex = 5;
			this.lblDurationValue.Text = "Duration:";
			// 
			// lblDurationType
			// 
			this.lblDurationType.AutoSize = true;
			this.lblDurationType.Location = new System.Drawing.Point(14, 29);
			this.lblDurationType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblDurationType.Name = "lblDurationType";
			this.lblDurationType.Size = new System.Drawing.Size(34, 15);
			this.lblDurationType.TabIndex = 3;
			this.lblDurationType.Text = "Type:";
			// 
			// lblCodeLength
			// 
			this.lblCodeLength.AutoSize = true;
			this.lblCodeLength.Location = new System.Drawing.Point(498, 31);
			this.lblCodeLength.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblCodeLength.Name = "lblCodeLength";
			this.lblCodeLength.Size = new System.Drawing.Size(75, 15);
			this.lblCodeLength.TabIndex = 9;
			this.lblCodeLength.Text = "Code length:";
			this.lblCodeLength.DoubleClick += new System.EventHandler(this.lblCodeLength_DoubleClick);
			// 
			// lblCodeType
			// 
			this.lblCodeType.AutoSize = true;
			this.lblCodeType.Location = new System.Drawing.Point(14, 33);
			this.lblCodeType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblCodeType.Name = "lblCodeType";
			this.lblCodeType.Size = new System.Drawing.Size(64, 15);
			this.lblCodeType.TabIndex = 6;
			this.lblCodeType.Text = "Code type:";
			// 
			// status
			// 
			this.status.BorderStyle = System.Windows.Forms.Border3DStyle.RaisedOuter;
			this.status.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.status.ForeColor = System.Drawing.Color.Black;
			this.status.Name = "status";
			this.status.Size = new System.Drawing.Size(643, 17);
			this.status.Spring = true;
			this.status.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// opTime
			// 
			this.opTime.BorderStyle = System.Windows.Forms.Border3DStyle.RaisedOuter;
			this.opTime.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.opTime.ForeColor = System.Drawing.SystemColors.ControlText;
			this.opTime.Name = "opTime";
			this.opTime.Size = new System.Drawing.Size(0, 17);
			this.opTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// ss
			// 
			this.ss.Font = new System.Drawing.Font("Courier New", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.ss.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.ss.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.status,
            this.opTime});
			this.ss.Location = new System.Drawing.Point(0, 404);
			this.ss.Name = "ss";
			this.ss.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
			this.ss.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
			this.ss.Size = new System.Drawing.Size(660, 22);
			this.ss.SizingGrip = false;
			this.ss.TabIndex = 21;
			this.ss.Text = "ss";
			this.ss.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ss_ItemClicked);
			// 
			// openFileDialog
			// 
			this.openFileDialog.DefaultExt = "clk";
			this.openFileDialog.Filter = "CodeLocker files|*.clk";
			// 
			// saveFileDialog
			// 
			this.saveFileDialog.DefaultExt = "clk";
			this.saveFileDialog.FileName = "codelock";
			this.saveFileDialog.Filter = "CodeLocker files|*.clk";
			this.saveFileDialog.Title = "Export code lock file";
			// 
			// folderBrowser
			// 
			this.folderBrowser.Description = "Export code lock to folder";
			this.folderBrowser.SelectedPath = "C:\\Users\\cabal\\OneDrive\\Documents";
			this.folderBrowser.ShowNewFolderButton = false;
			// 
			// CodeLockerForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(660, 426);
			this.Controls.Add(this.pnlBody);
			this.Controls.Add(this.ss);
			this.Controls.Add(this.mainMenu);
			this.Controls.Add(this.lblTime);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.mainMenu;
			this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.MaximizeBox = false;
			this.Name = "CodeLockerForm";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "CodeLocker";
			this.Load += new System.EventHandler(this.CaretakerMainForm_Load);
			this.DoubleClick += new System.EventHandler(this.CaretakerMainForm_DoubleClick);
			((System.ComponentModel.ISupportInitialize)(this._errorProvider)).EndInit();
			this.mainMenu.ResumeLayout(false);
			this.mainMenu.PerformLayout();
			this.pnlBody.ResumeLayout(false);
			this.pnlBody.PerformLayout();
			this.grpOutput.ResumeLayout(false);
			this.grpOutput.PerformLayout();
			this.grpCodeLockDetails.ResumeLayout(false);
			this.grpCodeLockDetails.PerformLayout();
			this.grpDuration.ResumeLayout(false);
			this.grpDuration.PerformLayout();
			this.ss.ResumeLayout(false);
			this.ss.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Timer opTimer;
		private System.Windows.Forms.ErrorProvider _errorProvider;
		private System.Windows.Forms.ToolTip _ttUseMSTime;
		private System.Windows.Forms.Timer statusTimer;
		private System.Windows.Forms.Timer _nowTimer;
		private System.Windows.Forms.Label lblTime;
		private System.Windows.Forms.Timer gotTimeTimer;
		private System.Windows.Forms.Timer singleTickTimer;
		private System.Windows.Forms.ToolTip tt;
		private System.Windows.Forms.MenuStrip mainMenu;
		private System.Windows.Forms.ToolStripMenuItem mnuProgramMenu;
		private System.Windows.Forms.ToolStripMenuItem mnuExit;
		private System.Windows.Forms.ToolStripMenuItem mnuActionsMenu;
		private System.Windows.Forms.ToolStripMenuItem mnuSetManualExpirationDatetimeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem mnuDeleteCodeLock;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem mnuClearManualExpiration;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem mnuUnlockCode;
		private System.Windows.Forms.ToolStripMenuItem mnuCreateCodeLock;
		private System.Windows.Forms.ToolStripMenuItem mnuClearLastUnlockedCode;
		private System.Windows.Forms.Panel pnlBody;
		private System.Windows.Forms.Button btnReset;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnUnlockCode;
		private System.Windows.Forms.Button btnLockCode;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Button btnError;
		private System.Windows.Forms.Label lblTimeout;
		private System.Windows.Forms.ComboBox cmbTimeout;
		private System.Windows.Forms.GroupBox grpCodeLockDetails;
		private System.Windows.Forms.TextBox txtManualCode;
		private System.Windows.Forms.Label lblManualCode;
		private System.Windows.Forms.GroupBox grpDuration;
		private Controls.NumberTextbox txtDurationValue;
		private System.Windows.Forms.Label lblDurationValue;
		private System.Windows.Forms.Label lblDurationType;
		private System.Windows.Forms.ComboBox cmbCodeLength;
		private System.Windows.Forms.Label lblCodeLength;
		private System.Windows.Forms.ComboBox cmbCodeType;
		private System.Windows.Forms.Label lblCodeType;
		private System.Windows.Forms.Button btnCustomDurationDialog;
		private System.Windows.Forms.Button btnCancelManualExpirationDate;
		private System.Windows.Forms.ComboBox cmbDurationType;
		private System.Windows.Forms.GroupBox grpOutput;
		private System.Windows.Forms.TextBox txtOutput;
		private System.Windows.Forms.Label lblOutput;
		private System.Windows.Forms.CheckBox chkUseMSTime;
		private System.Windows.Forms.StatusStrip ss;
		private System.Windows.Forms.ToolStripStatusLabel status;
		private System.Windows.Forms.ToolStripStatusLabel opTime;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.SaveFileDialog saveFileDialog;
		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.Button btnDeleteLock;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripMenuItem otherSettingsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem mnuExportCodeLock;
		private System.Windows.Forms.FolderBrowserDialog folderBrowser;
		private System.Windows.Forms.ToolStripMenuItem mnuImportCodeLock;
		private System.Windows.Forms.ToolStripMenuItem pLeasureToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem mnuRandomVideos;
		private ToolStripSeparator toolStripSeparator5;
		private ToolStripMenuItem mnuDisableTTs;
	}
}