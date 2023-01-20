
namespace CabalsCorner.CodeLocker.Forms
{
	partial class VideoForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VideoForm));
			this.pnlWB = new System.Windows.Forms.Panel();
			this.button1 = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.wb = new System.Windows.Forms.WebBrowser();
			this.pnlWB.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlWB
			// 
			this.pnlWB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlWB.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pnlWB.Controls.Add(this.wb);
			this.pnlWB.Location = new System.Drawing.Point(13, 13);
			this.pnlWB.Name = "pnlWB";
			this.pnlWB.Size = new System.Drawing.Size(883, 569);
			this.pnlWB.TabIndex = 0;
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button1.Location = new System.Drawing.Point(711, 588);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(100, 35);
			this.button1.TabIndex = 1;
			this.button1.Text = "button1";
			this.button1.UseVisualStyleBackColor = true;
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.Location = new System.Drawing.Point(817, 588);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(79, 35);
			this.btnClose.TabIndex = 2;
			this.btnClose.Text = "Close";
			this.btnClose.UseVisualStyleBackColor = true;
			// 
			// wb
			// 
			this.wb.Dock = System.Windows.Forms.DockStyle.Fill;
			this.wb.Location = new System.Drawing.Point(0, 0);
			this.wb.MinimumSize = new System.Drawing.Size(20, 20);
			this.wb.Name = "wb";
			this.wb.Size = new System.Drawing.Size(879, 565);
			this.wb.TabIndex = 0;
			// 
			// VideoForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(908, 629);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.pnlWB);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "VideoForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Random Video Player";
			this.Load += new System.EventHandler(this.VideoForm_Load);
			this.pnlWB.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel pnlWB;
		private System.Windows.Forms.WebBrowser wb;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button btnClose;
	}
}