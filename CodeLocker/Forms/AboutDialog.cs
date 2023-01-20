using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CabalsCorner.CodeLocker.Forms
{
	internal partial class AboutDialog : Form
	{
		#region Ctor(s)

		public AboutDialog()
		{
			InitializeComponent();
		}

		#endregion

		#region Message-Handlers

		private void AboutDialog_Load(object sender, EventArgs e)
		{
			btnClose.Focus();
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			Close();
		}
		private void lnkCCS_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start(label2.Text);
			AcceptButton = btnClose;
			btnClose.Focus();
		} 

		#endregion
	}
}
