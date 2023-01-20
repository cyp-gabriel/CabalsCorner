namespace CabalsCorner.CodeLocker.UserControls
{
	 partial class TimeDurationSelectorControl
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

		  #region Component Designer generated code

		  /// <summary> 
		  /// Required method for Designer support - do not modify 
		  /// the contents of this method with the code editor.
		  /// </summary>
		  private void InitializeComponent()
		  {
			this.components = new System.ComponentModel.Container();
			this.cmbDurationType = new System.Windows.Forms.ComboBox();
			this.txtDuration = new System.Windows.Forms.TextBox();
			this.dtTime = new System.Windows.Forms.DateTimePicker();
			this.dtDate = new System.Windows.Forms.DateTimePicker();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.lblDuration = new System.Windows.Forms.Label();
			this.btnReset = new System.Windows.Forms.Button();
			this.lblExpiration = new System.Windows.Forms.Label();
			this.txtExpiration = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.txtNow = new System.Windows.Forms.TextBox();
			this._highlightTimer = new System.Windows.Forms.Timer(this.components);
			this._tt = new System.Windows.Forms.ToolTip(this.components);
			this._nowTimer = new System.Windows.Forms.Timer(this.components);
			this.lblStatus = new System.Windows.Forms.Label();
			this._statusTimer = new System.Windows.Forms.Timer(this.components);
			this.SuspendLayout();
			// 
			// cmbDurationType
			// 
			this.cmbDurationType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbDurationType.Font = new System.Drawing.Font("Courier New", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cmbDurationType.FormattingEnabled = true;
			this.cmbDurationType.Items.AddRange(new object[] {
            "Days",
            "Hours",
            "Minutes",
            "Seconds"});
			this.cmbDurationType.Location = new System.Drawing.Point(284, 76);
			this.cmbDurationType.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.cmbDurationType.Name = "cmbDurationType";
			this.cmbDurationType.Size = new System.Drawing.Size(87, 25);
			this.cmbDurationType.TabIndex = 2;
			this.cmbDurationType.SelectedIndexChanged += new System.EventHandler(this.cmbDurationType_SelectedIndexChanged);
			// 
			// txtDuration
			// 
			this.txtDuration.BackColor = System.Drawing.SystemColors.InactiveCaption;
			this.txtDuration.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtDuration.Font = new System.Drawing.Font("Courier New", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtDuration.ForeColor = System.Drawing.Color.DarkRed;
			this.txtDuration.Location = new System.Drawing.Point(73, 76);
			this.txtDuration.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.txtDuration.Name = "txtDuration";
			this.txtDuration.ReadOnly = true;
			this.txtDuration.Size = new System.Drawing.Size(207, 24);
			this.txtDuration.TabIndex = 3;
			this.txtDuration.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// dtTime
			// 
			this.dtTime.AllowDrop = true;
			this.dtTime.Font = new System.Drawing.Font("Courier New", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.dtTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
			this.dtTime.Location = new System.Drawing.Point(73, 40);
			this.dtTime.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.dtTime.Name = "dtTime";
			this.dtTime.ShowUpDown = true;
			this.dtTime.Size = new System.Drawing.Size(144, 24);
			this.dtTime.TabIndex = 1;
			this.dtTime.CloseUp += new System.EventHandler(this.dtTime_CloseUp);
			this.dtTime.ValueChanged += new System.EventHandler(this.dtTime_ValueChanged);
			this.dtTime.DropDown += new System.EventHandler(this.dtTime_DropDown);
			// 
			// dtDate
			// 
			this.dtDate.AllowDrop = true;
			this.dtDate.Font = new System.Drawing.Font("Courier New", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.dtDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dtDate.Location = new System.Drawing.Point(73, 5);
			this.dtDate.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.dtDate.Name = "dtDate";
			this.dtDate.Size = new System.Drawing.Size(144, 24);
			this.dtDate.TabIndex = 0;
			this.dtDate.CloseUp += new System.EventHandler(this.dtDate_CloseUp);
			this.dtDate.ValueChanged += new System.EventHandler(this.dtDate_ValueChanged);
			this.dtDate.DropDown += new System.EventHandler(this.dtDate_DropDown);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(3, 8);
			this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(42, 17);
			this.label1.TabIndex = 4;
			this.label1.Text = "Date:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(3, 43);
			this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(43, 17);
			this.label2.TabIndex = 5;
			this.label2.Text = "Time:";
			// 
			// lblDuration
			// 
			this.lblDuration.AutoSize = true;
			this.lblDuration.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblDuration.Location = new System.Drawing.Point(3, 80);
			this.lblDuration.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.lblDuration.Name = "lblDuration";
			this.lblDuration.Size = new System.Drawing.Size(66, 17);
			this.lblDuration.TabIndex = 6;
			this.lblDuration.Text = "Duration:";
			// 
			// btnReset
			// 
			this.btnReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnReset.Location = new System.Drawing.Point(220, 39);
			this.btnReset.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.btnReset.Name = "btnReset";
			this.btnReset.Size = new System.Drawing.Size(69, 26);
			this.btnReset.TabIndex = 7;
			this.btnReset.Text = "12:00 AM";
			this._tt.SetToolTip(this.btnReset, "Reset time to 12:00 AM.");
			this.btnReset.UseVisualStyleBackColor = true;
			this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
			// 
			// lblExpiration
			// 
			this.lblExpiration.AutoSize = true;
			this.lblExpiration.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblExpiration.Location = new System.Drawing.Point(3, 155);
			this.lblExpiration.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.lblExpiration.Name = "lblExpiration";
			this.lblExpiration.Size = new System.Drawing.Size(74, 17);
			this.lblExpiration.TabIndex = 9;
			this.lblExpiration.Text = "Expiration:";
			// 
			// txtExpiration
			// 
			this.txtExpiration.BackColor = System.Drawing.SystemColors.InactiveCaption;
			this.txtExpiration.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtExpiration.Font = new System.Drawing.Font("Courier New", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtExpiration.ForeColor = System.Drawing.Color.DarkBlue;
			this.txtExpiration.Location = new System.Drawing.Point(90, 152);
			this.txtExpiration.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.txtExpiration.Name = "txtExpiration";
			this.txtExpiration.ReadOnly = true;
			this.txtExpiration.Size = new System.Drawing.Size(281, 24);
			this.txtExpiration.TabIndex = 8;
			this.txtExpiration.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.Location = new System.Drawing.Point(3, 124);
			this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(39, 17);
			this.label5.TabIndex = 11;
			this.label5.Text = "Now:";
			// 
			// txtNow
			// 
			this.txtNow.BackColor = System.Drawing.SystemColors.InactiveCaption;
			this.txtNow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtNow.Font = new System.Drawing.Font("Courier New", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtNow.Location = new System.Drawing.Point(90, 121);
			this.txtNow.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.txtNow.Name = "txtNow";
			this.txtNow.ReadOnly = true;
			this.txtNow.Size = new System.Drawing.Size(281, 24);
			this.txtNow.TabIndex = 10;
			this.txtNow.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// _highlightTimer
			// 
			this._highlightTimer.Interval = 1500;
			// 
			// _nowTimer
			// 
			this._nowTimer.Tick += new System.EventHandler(this._nowTimer_Tick);
			// 
			// lblStatus
			// 
			this.lblStatus.AutoSize = true;
			this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblStatus.ForeColor = System.Drawing.Color.Red;
			this.lblStatus.Location = new System.Drawing.Point(3, 189);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(0, 17);
			this.lblStatus.TabIndex = 12;
			// 
			// _statusTimer
			// 
			this._statusTimer.Interval = 4000;
			this._statusTimer.Tick += new System.EventHandler(this._statusTimer_Tick);
			// 
			// TimeDurationSelectorControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Controls.Add(this.lblStatus);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.txtNow);
			this.Controls.Add(this.lblExpiration);
			this.Controls.Add(this.txtExpiration);
			this.Controls.Add(this.btnReset);
			this.Controls.Add(this.lblDuration);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.cmbDurationType);
			this.Controls.Add(this.txtDuration);
			this.Controls.Add(this.dtTime);
			this.Controls.Add(this.dtDate);
			this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.Name = "TimeDurationSelectorControl";
			this.Size = new System.Drawing.Size(376, 208);
			this.Load += new System.EventHandler(this.TimeDurationSelectorControl_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		  }

		  #endregion

		  private System.Windows.Forms.ComboBox cmbDurationType;
		  private System.Windows.Forms.TextBox txtDuration;
		  private System.Windows.Forms.DateTimePicker dtTime;
		  private System.Windows.Forms.DateTimePicker dtDate;
		  private System.Windows.Forms.Label label1;
		  private System.Windows.Forms.Label label2;
		  private System.Windows.Forms.Label lblDuration;
		  private System.Windows.Forms.Button btnReset;
		  private System.Windows.Forms.Label lblExpiration;
		  private System.Windows.Forms.TextBox txtExpiration;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox txtNow;
		private System.Windows.Forms.Timer _highlightTimer;
		private System.Windows.Forms.ToolTip _tt;
		private System.Windows.Forms.Timer _nowTimer;
		private System.Windows.Forms.Label lblStatus;
		private System.Windows.Forms.Timer _statusTimer;
	}
}
