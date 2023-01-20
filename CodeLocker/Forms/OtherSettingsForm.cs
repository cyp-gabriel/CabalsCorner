using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

using CabalsCorner.CodeLocker.Classes;

namespace CabalsCorner.CodeLocker.Forms
{
	public partial class OtherSettingsForm : Form
	{
		public OtherSettingsForm()
		{
			InitializeComponent();

			_settings = new CodeLockerAppSettings(ConfigurationManager.AppSettings["CodePersistanceMgr.DefaultAppSettingsFilePath"]);

		}

		private void OtherSettingsForm_Load(object sender, EventArgs e)
		{
			this.txtEmail.Text = _settings.UserEmail != "NOEMAIL" ? _settings.UserEmail : string.Empty;

			chkEmailCodeLock.Checked = _settings.EmailCodeLockToUser;
		}

		private void btnSaveAndClose_Click(object sender, EventArgs e)
		{
			if (! IsValidEmail(txtEmail.Text))
			{
				MessageBox.Show("Invalid email, friend.", "Attention on Location", MessageBoxButtons.OK, MessageBoxIcon.Error);

				txtEmail.Focus();
				txtEmail.SelectAll();
			}
			else if (txtEmail.Text != string.Empty)
			{
				_settings.UserEmail = txtEmail.Text;
				_settings.EmailCodeLockToUser = chkEmailCodeLock.Checked;

				Close();
			}

		}

		bool IsValidEmail(string email)
		{
			try
			{
				var addr = new System.Net.Mail.MailAddress(email);
				return addr.Address == email;
			}
			catch
			{
				return false;
			}
		}

		private CodeLockerAppSettings _settings = null;

		private void txtEmail_Validating(object sender, CancelEventArgs e)
		{
		
		}
	}
}
