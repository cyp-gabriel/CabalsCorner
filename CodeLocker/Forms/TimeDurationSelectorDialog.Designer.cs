namespace CabalsCorner.CodeLocker.Forms
{
	 partial class TimeDurationSelectorDialog
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
			this.btnAcceptDuration = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.timeDurationSelectorControl1 = new CabalsCorner.CodeLocker.UserControls.TimeDurationSelectorControl();
			this.SuspendLayout();
			// 
			// btnAcceptDuration
			// 
			this.btnAcceptDuration.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAcceptDuration.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnAcceptDuration.Location = new System.Drawing.Point(312, 234);
			this.btnAcceptDuration.Margin = new System.Windows.Forms.Padding(2);
			this.btnAcceptDuration.Name = "btnAcceptDuration";
			this.btnAcceptDuration.Size = new System.Drawing.Size(75, 32);
			this.btnAcceptDuration.TabIndex = 0;
			this.btnAcceptDuration.Text = "&Select";
			this.btnAcceptDuration.UseVisualStyleBackColor = true;
			this.btnAcceptDuration.Click += new System.EventHandler(this.btnAcceptDuration_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(233, 234);
			this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 32);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "&Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// timeDurationSelectorControl1
			// 
			this.timeDurationSelectorControl1.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.timeDurationSelectorControl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.timeDurationSelectorControl1.DurationUnits = null;
			this.timeDurationSelectorControl1.ExpirationDatetime = null;
			this.timeDurationSelectorControl1.Location = new System.Drawing.Point(11, 11);
			this.timeDurationSelectorControl1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.timeDurationSelectorControl1.Name = "timeDurationSelectorControl1";
			this.timeDurationSelectorControl1.Size = new System.Drawing.Size(376, 216);
			this.timeDurationSelectorControl1.TabIndex = 2;
			// 
			// TimeDurationSelectorDialog
			// 
			this.AcceptButton = this.btnAcceptDuration;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(396, 271);
			this.Controls.Add(this.timeDurationSelectorControl1);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnAcceptDuration);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(2);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "TimeDurationSelectorDialog";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Select Code-Lock Expiration Date/Time";
			this.Load += new System.EventHandler(this.TimeDurationSelectorDialog_Load);
			this.ResumeLayout(false);

		  }

		  #endregion
		  private System.Windows.Forms.Button btnAcceptDuration;
		  private System.Windows.Forms.Button btnCancel;
		  private UserControls.TimeDurationSelectorControl timeDurationSelectorControl1;
	}
}

