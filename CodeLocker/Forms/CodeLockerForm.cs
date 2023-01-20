using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Collections.Specialized;
using System.Reflection;
using System.Diagnostics;
using System.Security.Principal;

using CabalsCorner;
using CabalsCorner.Utilities;
using CabalsCorner.CodeLocker;
using CabalsCorner.UIUtilities;
using CabalsCorner.ErrorHandling;
//using CabalsCorner.UIUtilities.DialogBoxes;
using CabalsCorner.CodeLocker.UserControls;
using CabalsCorner.CodeLocker.Classes;
using System.Collections.Concurrent;

namespace CabalsCorner.CodeLocker.Forms
{
	internal partial class CodeLockerForm : Form
	{
		#region Constructor(s)

		public CodeLockerForm()
		{
			try
			{
				InitializeComponent();

				if (DesignMode)
					return;

				App.Instance.CreateDocumentsCodeLockerFolder();
				string s = App.Instance.BackupFolder;

//				dump = new DebugDialog();
//				dump.Hide();

				ExpirationDate expDate = App.Instance.ExpirationDate;
				expDate.Activated += OnExpirationDateActiveStatusChanged;
				expDate.Deactivated += OnExpirationDateActiveStatusChanged;

				cmbCodeType.SelectedIndex = 0;
				var ss = ConfigurationManager.AppSettings;
				int i = ss.Count;
				CodeLockerAppSettings settings = App.Instance.Settings;

				cmbCodeLength.SelectedItem = settings.CodeLength.ToString();
				cmbDurationType.SelectedItem = App.Instance.Settings.DurationType.ToString();

				chkUseMSTime.Checked = settings.UseMicrosoftTime;

				getTimeOp = new GetTimeOp();
				getTimeOp.ExecuteCompleted += getTimeOp_ExecuteCompleted;
				getTimeOp.ExecuteProgressChanged += getTimeOp_ExecuteProgressChanged;



				CodePersistanceMgr codePersistMgr = new CodePersistanceMgr();
				if (!codePersistMgr.CodeIsLocked(App.Instance.DefaultCodeLockFileName) && File.Exists(App.Instance.BackupCodeLockFilePath))
				{
					codePersistMgr.Import(App.Instance.DefaultCodeLockFileName, App.Instance.BackupCodeLockFilePath);
				}
				else if (!codePersistMgr.CodeIsLocked(App.Instance.DefaultCodeLockFileName) && !File.Exists(App.Instance.BackupCodeLockFilePath))
				{
					codePersistMgr.SaveCodeLockRecordIfNotExists(App.Instance.DefaultCodeLockFileName);
				}

				codePersistMgr.SaveTimeServersFileIfNotExists();

				SetTitleWithLastUnlockedCode();
			}
			catch (Exception ex)
			{
				HandleException(ex);
			}
		}

		#endregion

		#region Message-Handlers

		private void CaretakerMainForm_Load(object sender, EventArgs e)
		{
			CodeLockerAppSettings settings = null;

			try
			{
				if (DesignMode)
					return;

				saveFileDialog.InitialDirectory = App.Instance.MyDocuments;
				openFileDialog.InitialDirectory = App.Instance.MyDocuments;

				_nowTimer.Start();
				mainMenu.Enabled = true;

				//SingleTickStatusMsg("Program restarted with necessary security permissions.", Color.Red, true);
				RefreshDurationTextBoxToolTip(false);

				settings = App.Instance.Settings;
				bool appFailed = settings.AppFailedWithInsufficientSecurityCredentials;
				if (appFailed)
				{
					SingleTickStatusMsg("Program restarted with necessary security permissions.", Color.Red, true);
					settings.AppFailedWithInsufficientSecurityCredentials = false;
				}

				InitView(true);
			}
			catch (System.Security.SecurityException)
			{
				if (settings != null)
					settings.AppFailedWithInsufficientSecurityCredentials = true;

				Elevate();
				Close();
			}
			catch (System.Security.Cryptography.CryptographicException)
			{
				try
				{
					File.Delete(App.Instance.DefaultCodeLockFileName);
					File.Delete(App.Instance.BackupCodeLockFilePath);
					InitView(true);
					SingleTickStatusMsg("Deleted corrupted code lock.", Color.Red, true);
				}
				catch (Exception ex2)
				{
					HandleException(ex2);
				}
			}
			catch (Exception ex3)
			{
				HandleException(ex3);
			}
		}
		private void CaretakerMainForm_DoubleClick(object sender, EventArgs e)
		{
			try
			{
				//AcceptButton = btnError;
				//((Button)AcceptButton).Focus();
			}
			catch (Exception ex)
			{
				HandleException(ex);
			}
		}

		private void mnuExit_Click(object sender, EventArgs e)
		{
			try
			{
				Close();
			}
			catch (Exception ex)
			{
				HandleException(ex);
			}
		}
		private void mnuSetManualExpirationDatetimeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				btnCustomDurationDialog_Click(sender, e);
			}
			catch (Exception ex)
			{
				HandleException(ex);
			}
		}
		private void mnuAbout_Click(object sender, EventArgs e)
		{
			try
			{
				AboutDialog aboutDlg = new AboutDialog();
				aboutDlg.ShowDialog(this);
				FocusActiveButton();
				FocusTextBox();
			}
			catch (Exception ex)
			{
				HandleException(ex);
			}
		}
		private void mnuDeleteCodeLock_Click(object sender, EventArgs e)
		{
			try
			{
				btnDeleteLock_Click(sender, e);
			}
			catch (Exception ex)
			{
				HandleException(ex);
			}
		}
		private void mnuClearManualExpiration_Click(object sender, EventArgs e)
		{
			try
			{
				btnCancelManualExpirationDate_Click(this, e);
			}
			catch (Exception ex)
			{
				HandleException(ex);
			}
		}
		private void mnuUnlockCode_Click(object sender, EventArgs e)
		{

			try
			{
				//FocusActiveButton();
				FocusTextBox();
				btnUnlockCode_Click(this, e);
			}
			catch (Exception ex)
			{
				HandleException(ex);
			}
		}
		private void mnuCreateCodeLock_Click(object sender, EventArgs e)
		{
			try
			{
				FocusTextBox();
				//btnLockCode_ClickAsync(this, e);
			}
			catch (Exception ex)
			{
				HandleException(ex);
			}

		}
		private void mnuClearLastUnlockedCode_Click(object sender, EventArgs e)
		{
			try
			{
				ClearLastUnlockedCode();
				mnuClearLastUnlockedCode.Enabled = !Text.Equals("CodeLocker");
			}
			catch (Exception ex)
			{
				HandleException(ex);
			}
		}
		private void mnuImport_Click(object sender, EventArgs e)
		{
			try
			{
				openFileDialog.Title = "Import code-lock from file.";
				if (openFileDialog.ShowDialog(this) == DialogResult.OK)
				{
					CodePersistanceMgr c = new CodePersistanceMgr();
					c.Import(App.Instance.DefaultCodeLockFileName, openFileDialog.FileName);
					InitView();

					SingleTickStatusMsg(string.Format("Imported code-lock from file '{0}'.", Path.GetFileName(openFileDialog.FileName)), Color.Blue, false);
				}
			}
			catch (Exception ex)
			{
				HandleException(ex);
			}
		}
		private void mnuExport_Click(object sender, EventArgs e)
		{
			try
			{
				saveFileDialog.Title = "Export active code-lock to file.";
				if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
				{
					CodePersistanceMgr c = new CodePersistanceMgr();
					c.Export(App.Instance.DefaultCodeLockFileName, saveFileDialog.FileName);
					//InitView();

					SingleTickStatusMsg(string.Format("Exported active code-lock to file '{0}'.", Path.GetFileName(saveFileDialog.FileName)), Color.Blue, false);
				}
			}
			catch (Exception ex)
			{
				HandleException(ex);
			}
		}

		private void txtManualCode_TextChanged(object sender, EventArgs e)
		{
			try
			{
				CodeType codeType = (CodeType)Enum.Parse(typeof(CodeType), cmbCodeType.SelectedItem.ToString());

				if (codeType == CodeType.ManuallyEnterCode)
				{
					bool v = ValidateManualCode();
					Button b = (Button)AcceptButton;
					b.Enabled = v && txtDurationValue.ValidationSuccessful;
				}
			}
			catch (Exception ex)
			{
				HandleException(ex);
			}
		}
		private void cmbCodeLength_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				App.Instance.Settings.CodeLength = int.Parse(cmbCodeLength.SelectedItem.ToString());
				OnComboValueChanged();
			}
			catch (Exception ex)
			{
				HandleException(ex);
			}
		}
		private void txtDurationValue_NumberValidated(object sender, EventArgs e)
		{
			try
			{
				RefreshCreateManualExpirationDateTimeButtons();

				mnuCreateCodeLock.Enabled = btnLockCode.Enabled;
			}
			catch (Exception ex)
			{
				HandleException(ex);
			}
		}
		private void txtDurationValue_TextChanged(object sender, EventArgs e)
		{
			try
			{
				if (!App.Instance.ExpirationDate.Active)
				{
					bool success = txtDurationValue.ValidationSuccessful;
					btnCustomDurationDialog.Enabled = mnuSetManualExpirationDatetimeToolStripMenuItem.Enabled = success;
				}
				RefreshDurationTextBoxToolTip(false);
			}
			catch (Exception ex)
			{
				HandleException(ex);
			}
		}
		private void txtDurationValue_Leave(object sender, EventArgs e)
		{
			try
			{
				tt.SetToolTip(txtDurationValue, string.Empty);
			}
			catch (Exception ex)
			{
				HandleException(ex);
			}
		}
		private void txtDurationValue_Enter(object sender, EventArgs e)
		{
			try
			{
				RefreshDurationTextBoxToolTip(true);
			}
			catch (Exception ex)
			{
				HandleException(ex);
			}
		}
		private void btnClose_Click(object sender, EventArgs e)
		{
			try
			{
				File.SetAttributes("config.xml", FileAttributes.Normal);
				File.Copy("config.xml", App.Instance.BackupSettingsFilePath, true);

				Close();
			}
			catch (UnauthorizedAccessException ex)
			{
				HandleException(ex);
			}
			catch (DirectoryNotFoundException ex)
			{
				HandleException(ex);
			}
			catch (FileNotFoundException ex)
			{
				HandleException(ex);
			}
			catch (IOException ex)
			{
				HandleException(ex);
			}
			catch (Exception ex)
			{
				HandleException(ex);
			}
		}
		private void cmbCodeType_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				CodeType codeType = (CodeType)Enum.Parse(typeof(CodeType), cmbCodeType.SelectedItem.ToString());
				if (codeType == CodeType.CreateRandomNumbersAndLettersCode || codeType == CodeType.CreateRandomNumericCode)
				{
					lblCodeLength.Enabled = true;
					cmbCodeLength.Enabled = true;

					txtManualCode.Enabled = false;

					if (AcceptButton != null)
					{
						((Button)AcceptButton).Enabled = true;
					}

					_errorProvider.SetError(txtManualCode, string.Empty);
					txtManualCode.BackColor = SystemColors.Window;
				}
				else if (codeType == CodeType.ManuallyEnterCode)
				{
					bool v = ValidateManualCode();
					if (AcceptButton != null)
					{
						((Button)AcceptButton).Enabled = v;
					}

					lblCodeLength.Enabled = false;
					cmbCodeLength.Enabled = false;

					cmbCodeLength.Focus();

					txtManualCode.Enabled = true;
					txtManualCode.ReadOnly = false;
				}

				OnComboValueChanged();
			}
			catch (Exception ex)
			{
				HandleException(ex);
			}
		}
		private void btnLockCode_Click(object sender, EventArgs e)
		{
			if (InvokeRequired)
			{
				Invoke(new Action<object, EventArgs>(btnLockCode_Click), sender, e);
				return;
			}

			lblTime.Text = string.Empty;
			btnUnlockCode.Enabled = false;

			EncrypterDecrypter.DurationType durationType =
				(EncrypterDecrypter.DurationType)Enum.Parse(
			 typeof(EncrypterDecrypter.DurationType)
				, cmbDurationType.SelectedItem.ToString()
				);
			TimeSpan durationTs = App.Instance.GetRemainingDuration(DateTime.Now, durationType, txtDurationValue.DoubleValue);
			App.Instance.ExpirationDate.Deactivate();
			if (durationTs.TotalDays > 9250)
			{
				MessageBox.Show(this, "Duration cannot be greater than 9250 days.", "Attention on Location", MessageBoxButtons.OK, MessageBoxIcon.Error);
				FocusTextBox();
				return;
			}

			//throw new ArgumentException();
			_lockCode = true;

			Cursor = Cursors.WaitCursor;
			opTime.Text = string.Empty;

			CodeType codeType = (CodeType)Enum.Parse(
			  typeof(CodeType)
			, cmbCodeType.SelectedItem.ToString()
			);
			if (codeType == CodeType.ManuallyEnterCode && txtManualCode.Text.Trim() == string.Empty)
			{
				StatusMsg("You must enter a code.", SystemColors.ControlText, false);
				return;
			}

			btnCancelManualExpirationDate.Visible = false;
			btnCustomDurationDialog.Visible = true;
			mnuSetManualExpirationDatetimeToolStripMenuItem.Enabled = btnCustomDurationDialog.Visible && btnCustomDurationDialog.Enabled;

			EnableAllControls(false);
			if (App.Instance.Settings.UseMicrosoftTime)
			{
				btnCancel.Cursor = Cursors.Default;
				btnCancel.Visible = chkUseMSTime.Checked;
				btnCancel.Enabled = chkUseMSTime.Checked;
			}
			btnDeleteLock.Enabled = false;
			txtDurationValue.ReadOnly = false;

			CodeLockerAppSettings settings = App.Instance.Settings;
			if (!settings.UseMicrosoftTime)
			{
				count = settings.TimeoutMS / 1000;
				opTimer.Start();
			}

			FocusActiveButton();

			BackgroundWorker bw = new();
			bw.DoWork += (obj, ea) => LockCodeAsync().ConfigureAwait(true);
			bw.RunWorkerAsync();
			getTimeOp_ExecuteCompleted(this, new AsyncCompletedEventArgs(null, false, getTimeOp.Result));
		}

		private async Task LockCodeAsync()
		{
			try
			{


				var tokenSource = new CancellationTokenSource();
				var token = tokenSource.Token;
				Task t;
				var tasks = new ConcurrentBag<Task>();

				t = Task.Run(() => getTimeOp.ExecuteAsync(token), token);
				tasks.Add(t);
				try
				{
					await Task.WhenAll(tasks.ToArray());
				}
				catch (OperationCanceledException)
				{
					Console.WriteLine($"\n{nameof(OperationCanceledException)} thrown\n");
				}
				finally
				{
					tokenSource.Dispose();
				}
				getTimeOp_ExecuteCompleted(this, new AsyncCompletedEventArgs(null, false, getTimeOp.Result));
			}
			catch (Exception ex)
			{
				HandleException(ex);
			}
		}

		private void btnUnlockCode_Click(object sender, EventArgs e)
		{
			try
			{

				if (InvokeRequired)
				{
					Invoke(new Action<object, EventArgs>(btnUnlockCode_Click), sender, e);
					return;
				}

				btnUnlockCode.Enabled = false;

				lblTime.Text = string.Empty;
				_lockCode = false;
				opTime.Text = string.Empty;

				Cursor = Cursors.WaitCursor;

				EnableAllControls(false);
				btnCancel.Visible = chkUseMSTime.Checked;
				btnCancel.Enabled = chkUseMSTime.Checked;
				btnCancel.Cursor = Cursors.Default;
				txtOutput.Text = string.Empty;
				btnDeleteLock.Enabled = false;

				CodeLockerAppSettings settings = App.Instance.Settings;
				if (!settings.UseMicrosoftTime)
				{
					count = settings.TimeoutMS / 1000;
					opTimer.Start();
				}

				FocusActiveButton();

				BackgroundWorker bw = new();
				bw.DoWork += (obj, ea) => LockCodeAsync().ConfigureAwait(true);
				bw.RunWorkerAsync();
			}
			catch (Exception ex)
			{
				HandleException(ex);
			}
		}
		private void btnReset_Click(object sender, EventArgs e)
		{
			try
			{
				RefreshView();
				RefreshCreateManualExpirationDateTimeButtons();

				btnCancel.Visible = btnCancel.Enabled = false;

				btnReset.Visible = false;

				mainMenu.Enabled = true;
				mnuActionsMenu.Enabled = true;

				//status.Text = string.Empty;
			}
			catch (Exception ex)
			{
				HandleException(ex);
			}
		}
		private void btnCancel_Click(object sender, EventArgs e)
		{
			try
			{
				btnCancel.Cursor = Cursors.Default;
				btnCancel.Enabled = false;
				getTimeOp.CancelAsync();
			}
			catch (Exception ex)
			{
				HandleException(ex);
			}
		}
		private void chkUseMSTime_CheckedChanged(object sender, EventArgs e)
		{
			try
			{
				App.Instance.Settings.UseMicrosoftTime = chkUseMSTime.Checked;
				lblTimeout.Enabled = !chkUseMSTime.Checked;
				cmbTimeout.Enabled = !chkUseMSTime.Checked;
				FocusActiveButton();
				FocusTextBox();
			}
			catch (Exception ex)
			{
				HandleException(ex);
			}
		}
		private void cmbTimeout_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				App.Instance.Settings.TimeoutMS = int.Parse(cmbTimeout.SelectedItem.ToString());
				OnComboValueChanged();
			}
			catch (Exception ex)
			{
				HandleException(ex);
			}
		}
		private void ss_DoubleClick(object sender, EventArgs e)
		{
			try
			{
				//if (dump.ErrorText != string.Empty && dump.ErrorText != null)
				//{
				//	dump.Show();
				//}
			}
			catch (Exception ex)
			{
				HandleException(ex);
			}
		}
		private void cmbDurationType_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				EncrypterDecrypter.DurationType durationType =
					 (EncrypterDecrypter.DurationType)Enum.Parse(
							 typeof(EncrypterDecrypter.DurationType)
						  , cmbDurationType.SelectedItem.ToString()
					 );
				if (!App.Instance.Settings.DurationType.Equals(durationType))
				{
					App.Instance.Settings.DurationType = durationType;
				}

				OnComboValueChanged();
			}
			catch (Exception ex)
			{
				HandleException(ex);
			}
		}
		private void btnError_Click(object sender, EventArgs e)
		{
			try
			{
				/*
				if (dump.ErrorText != string.Empty && dump.ErrorText != null)
				{
					btnError.Visible = false;

					dump.Show();
				}
				*/
				//throw new Exception(_las)
				throw _lastError;
			}
			catch (Exception ex)
			{
				HandleException(ex);
			}
		}
		private void lblCodeLength_DoubleClick(object sender, EventArgs e)
		{
			try
			{
				throw new ArgumentException();
			}
			catch (Exception ex)
			{
				HandleException(ex);
			}
		}
		private void btnDeleteLock_Click(object sender, EventArgs e)
		{
			try
			{
				DialogResult result = MessageBox.Show(
				  "Are you sure you wan't to delete the current locked code?  This action cannot be undone."
				, "Attention on Location"
				, MessageBoxButtons.YesNo
				, MessageBoxIcon.Exclamation
				);
				if (result == DialogResult.Yes)
				{
					File.Delete(App.Instance.DefaultCodeLockFileName);
					File.Delete(App.Instance.BackupCodeLockFilePath);
					InitView(false);

					StatusMsg("Code lock deleted.", Color.Blue, true);

					App.Instance.ExpirationDate.Deactivate();

					mnuDeleteCodeLock.Enabled = btnDeleteLock.Enabled;

					FocusActiveButton();

					txtDurationValue.ValidateText();
					FocusTextBox();
					((Button)AcceptButton).Enabled = txtDurationValue.ValidationSuccessful;
				}
				else
				{

					FocusActiveButton();
					//FocusTextBox();
					txtDurationValue.ValidateText();
					FocusTextBox();
				}
			}
			catch (Exception ex)
			{
				HandleException(ex);
			}
		}
		private void btnCustomDurationDialog_Click(object sender, EventArgs e)
		{
			try
			{
				tt.SetToolTip(txtDurationValue, string.Empty);
				_nowTimer.Stop();

				EncrypterDecrypter.DurationType durationType =
					 (EncrypterDecrypter.DurationType)Enum.Parse(
							 typeof(EncrypterDecrypter.DurationType)
						  , cmbDurationType.SelectedItem.ToString()
					 );
				TimeSpan durationTs = App.Instance.GetRemainingDuration(DateTime.Now, durationType, txtDurationValue.DoubleValue);
				if (durationTs.TotalDays > 9250)
				{
					MessageBox.Show(this, "Duration cannot be greater than 9250 days.", "Attention on Location", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}

				double duration = txtDurationValue.DoubleValue;

				btnCustomDurationDialog.Visible = false;
				//mnuSetManualExpirationDatetimeToolStripMenuItem.Enabled = false;

				//if (! App.Instance.ExpirationDate.Active)
				//{
				//	App.Instance.ExpirationDate.Set(cmbDurationType.SelectedIndex, duration);
				//}

				TimeDurationSelectorDialog dlg = new TimeDurationSelectorDialog();
				dlg.DurationControl.Configure(DateTime.Now, cmbDurationType.SelectedIndex, duration);
				string orgDuration = string.Copy(txtDurationValue.Text);
				if (dlg.ShowDialog() == DialogResult.OK)
				{
					txtDurationValue.Text = dlg.DurationControl.DurationUnits;

					foreach (string item in cmbDurationType.Items)
					{
						if (item.Equals(dlg.DurationControl.DurationType))
						{
							cmbDurationType.SelectedItem = item;
						}
					}

					StatusMsg("Using manual expiration date-time.", Color.Blue, true);

					FocusActiveButton();
					RefreshManualExpirationDateButtons();
					FocusTextBox();

					txtDurationValue.ReadOnly = true;
				}
				else
				{
					CancelManualExpirationDate();

					// restore original Duration value before dialog was opened
					txtDurationValue.Text = orgDuration;

					FocusActiveButton();
					RefreshManualExpirationDateButtons();
					FocusTextBox();
				}

				dlg.Dispose();

				_nowTimer.Start();
			}
			catch (Exception ex)
			{
				HandleException(ex);
			}
		}
		private void _statusTimer_Tick(object sender, EventArgs e)
		{
			status.Text = string.Empty;
		}
		private void btnCancelManualExpirationDate_Click(object sender, EventArgs e)
		{
			try
			{
				CancelManualExpirationDate();
			}
			catch (Exception ex)
			{
				HandleException(ex);
			}
		}

		private void tt_Popup(object sender, PopupEventArgs e)
		{
			try
			{
				RefreshDurationTextBoxToolTip(true);
			}
			catch (Exception ex)
			{
				HandleException(ex);
			}
		}
		private void ss_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
			try
			{
			}
			catch (Exception ex)
			{
				HandleException(ex);
			}
		}

		private void gotTimeTimer_Tick(object sender, EventArgs e)
		{
			try
			{
				gotTimeTimer.Stop();
				opTime.ForeColor = SystemColors.ControlText;
				opTime.Font = new System.Drawing.Font(
					"Courier New"
					, 10F
					, FontStyle.Regular
					, System.Drawing.GraphicsUnit.Point
					, ((byte)(0))
					);
			}
			catch (Exception ex)
			{
				HandleException(ex);
			}
		}
		private void opTimer_Tick(object sender, EventArgs e)
		{
			if (InvokeRequired)
			{
				Invoke(new Action<object, EventArgs>(opTimer_Tick), sender, e);
				return;
			}

			CodeLockerAppSettings settings = App.Instance.Settings;
			if (settings.UseMicrosoftTime)
			{
				return;
			}
			try
			{
				string countStr = " " + Convert.ToString(--count) + " ";
				if (count < 0)
				{
					opTime.Text = " * ";
					count = settings.TimeoutMS / 1000;
					opTimer.Stop();
				}
				else
				{
					opTime.Text = countStr;
				}
				Refresh();
			}
			catch (Exception ex)
			{
				HandleException(ex);
			}
		}
		private void statusTimer_Tick(object sender, EventArgs e)
		{
			try
			{
				statusTimer.Stop();
				status.Text = string.Empty;
			}
			catch (Exception ex)
			{
				HandleException(ex);
			}
		}
		private void _nowTimer_Tick(object sender, EventArgs e)
		{
			try
			{
				if (!getTimeOp.IsBusy && !singleTickTimer.Enabled)
				{
					opTime.Text = DateTime.Now.ToShortDateString() + ", " + DateTime.Now.ToLongTimeString();
				}
				if (App.Instance.ExpirationDate.Active && App.Instance.ExpirationDate.Expired)
				{
					OnExpirationDateExpired();
				}
				else if (App.Instance.ExpirationDate.Active && !App.Instance.ExpirationDate.Expired)
				{
					txtDurationValue.Text = App.Instance.ExpirationDate.GetRemainingDurationUnits(cmbDurationType.SelectedIndex).ToString();
				}
			}
			catch (Exception ex)
			{
				HandleException(ex);
			}
		}
		private void singleTickTimer_Tick(object sender, EventArgs e)
		{
			try
			{
				singleTickTimer.Stop();

				status.Text = string.Empty;
			}
			catch (Exception ex)
			{
				HandleException(ex);
			}
		}

		private void OnComboValueChanged()
		{
			try
			{
				//FocusActiveButton();
				FocusTextBox();
			}
			catch (Exception ex)
			{
				HandleException(ex);
			}
		}

		protected override void WndProc(ref Message m)
		{
			try
			{
				if (m.Msg == WM_NCLBUTTONDBLCLK)
				{
					ClearLastUnlockedCode();

					m.Result = IntPtr.Zero;
					return;
				}
				base.WndProc(ref m);
			}
			catch (Exception ex)
			{
				HandleException(ex);
			}
		}

		#endregion

		#region Object Event-Handlers

		private void getTimeOp_ExecuteProgressChanged(object sender, ExecuteProgressChangedEventArgs e)
		{
			try
			{
				if (InvokeRequired)
				{
					Invoke(new Action<object, ExecuteProgressChangedEventArgs>(getTimeOp_ExecuteProgressChanged), sender, e);
					return;
				}

				if (e.StatusMsg.StartsWith("Getting time"))
				{
					StatusMsg(e.StatusMsg, SystemColors.ControlText, false, 15000);
				}
				else if (e.StatusMsg.Equals("Retrying..."))
				{
					opTimer.Stop();
					count = App.Instance.Settings.TimeoutMS / 1000;
					StatusMsg(count.ToString(), SystemColors.ControlText, false, count * 1000);
					opTimer.Start();
				}
				if (!e.StatusMsg.StartsWith("Getting time"))
				{
					StatusMsg(e.StatusMsg, SystemColors.ControlText, false);
				}
			}
			catch (Exception ex)
			{
				HandleException(ex);
			}
		}
		private void getTimeOp_ExecuteCompleted(object sender, AsyncCompletedEventArgs e)
		{
			try
			{
				if (InvokeRequired)
				{
					Invoke(new Action<object, AsyncCompletedEventArgs>(getTimeOp_ExecuteCompleted), sender, e);
					return;
				}

				opTimer.Stop();
				count = App.Instance.Settings.TimeoutMS / 1000;

				mainMenu.Enabled = true;

				btnCustomDurationDialog.Enabled = true;

            if (getTimeOp.Error != null && !getTimeOp.TimedOut)
				{
					StatusMsg("Failed to open socket.", SystemColors.ControlText, false);
				}
				else if (getTimeOp.TimedOut)
				{
					StatusMsg("Connection timed out.", SystemColors.ControlText, false);
				}
				else if (e.Cancelled)
				{
					StatusMsg("Operation cancelled.", SystemColors.ControlText, false);
				}

				bool failed = (getTimeOp.Error != null && !getTimeOp.TimedOut)
					|| getTimeOp.TimedOut
					|| e.Cancelled;
				if (failed)
				{
					Cursor = Cursors.Default;
					RefreshView();
					return;
				}

				//
				// Adjust server "now" datetime.
				//
				TimeSpan delta = DateTime.Now - getTimeOp.Result;
				if (delta.TotalSeconds > 0 && delta.TotalSeconds <= 30)
				{
					getTimeOp.Result = getTimeOp.Result + delta;
				}

				if (_lockCode)
				{
					CodeType codeType = (CodeType)Enum.Parse(
						 typeof(CodeType)
					 , cmbCodeType.SelectedItem.ToString()
					 );
					EncrypterDecrypter.DurationType durationType =
						 (EncrypterDecrypter.DurationType)Enum.Parse(
								 typeof(EncrypterDecrypter.DurationType)
							  , cmbDurationType.SelectedItem.ToString()
						 );

					EncryptActionResult result = App.Instance.EncryptCodeDurationAndStartingTime(
						 codeType
					 , getTimeOp.Result
					 , durationType
					 , txtManualCode.Text
					 , txtDurationValue.Text
					 , App.Instance.Settings.CodeLength.ToString()
					 );
					string encryptedCode = result.EncryptedString;

					/////////////////////////
					////////////////////////
					/*
					NetUtility nu = new NetUtility();
					CodeLockerAppSettings _s = new CodeLockerAppSettings(ConfigurationManager.AppSettings["CodePersistanceMgr.DefaultAppSettingsFilePath"]);

					nu.SendEmail(587, 1, "smtp.gmail.com", "boone.cabal@gmail.com", "Bogh0001", _s.UserEmail, "Your CodeLock", result.EncryptedString);

					*/
					///////////////////////
					/////////////////////////

					RefreshManualExpirationDateButtons();

					StatusMsg("Code has been encrypted and locked.", Color.Blue, true);

					EnableOnlyOutputControls(true);

					btnLockCode.Enabled = mnuCreateCodeLock.Enabled = false;
					btnDeleteLock.Enabled = mnuDeleteCodeLock.Enabled = true;

					btnCancel.Visible = false;

					lblOutput.Text = "Generated:";
					txtOutput.Text = result.OriginalUnencryptedString;
					txtOutput.Focus();
					txtOutput.Select();

					// persist encrypted code lock record
					CodePersistanceMgr.State state = new CodePersistanceMgr.State(encryptedCode, App.Instance.EncryptedDuration, App.Instance.EncryptedStartingTime);
					CodePersistanceMgr codePersistMgr = new CodePersistanceMgr();
					codePersistMgr.Save(App.Instance.DefaultCodeLockFileName, state);

					bool lockHasExpired = App.Instance.LockHasExpired(state, getTimeOp.Result);
					if (lockHasExpired)
					{
						throw new ApplicationException("lock has expired too soon.");
					}

					btnReset.Visible = true;
					mnuActionsMenu.Enabled = false;

					AcceptButton = btnReset;
				}
				else // unlock
				{
					status.Text = "Retrieving code...";

					CodePersistanceMgr codePersistMgr = new CodePersistanceMgr();
					CodePersistanceMgr.State state = codePersistMgr.Load(App.Instance.DefaultCodeLockFileName);

					string s = App.Instance.GetDecryptedCode(state);
					bool lockHasExpired = App.Instance.LockHasExpired(state, getTimeOp.Result);
					if (lockHasExpired)
					{
						//
						// show the decrypted code
						//
						lblOutput.Text = "Decrypted code:";
						lblTime.Text = string.Empty;

						string decryptedCode = App.Instance.GetDecryptedCode(state);
						txtOutput.Text = decryptedCode;
						btnUnlockCode.Enabled = false;
						btnDeleteLock.Enabled = btnDeleteLock.Visible = false;
						btnClose.Enabled = true;

						codePersistMgr.SaveLastUnlockedCodeIfNotExists(decryptedCode);

						KeyMgr keyMgr = new KeyMgr();
						keyMgr.ResetKey();
						App.Instance.Key = null;

						SetTitleWithLastUnlockedCode();

						// reset the code lock record in persistent storage
						codePersistMgr.Reset();

						StatusMsg("The code has been unlocked.", Color.Blue, true);

						EnableOnlyOutputControls(true);

						txtOutput.Focus();
						txtOutput.Select();
						btnUnlockCode.Enabled = mnuUnlockCode.Enabled = false;

						btnReset.Visible = true;

						mnuActionsMenu.Enabled = false;

						File.Delete(App.Instance.BackupCodeLockFilePath);

						AcceptButton = btnReset;
					}
					else // lock is still active
					{
						//
						// show the lock expiration time
						///
						lblOutput.Text = "Expiration:";
						btnCancel.Enabled = false;
						DateTime expirationTime = App.Instance.GetLockExpirationTime(state);

						btnUnlockCode.Enabled = true;
						btnDeleteLock.Enabled = true;

						StatusMsg("The code is still locked.", SystemColors.ControlText, false);

						EnableOnlyOutputControls(true);

						txtOutput.Text = expirationTime.ToLongDateString() + " " + expirationTime.ToLongTimeString();
						txtOutput.Focus();
						txtOutput.Select();

						AcceptButton = btnUnlockCode;
					}
					btnCancel.Visible = false;
				}

				lblTimeout.Enabled = !chkUseMSTime.Checked;
				cmbTimeout.Enabled = !chkUseMSTime.Checked;
			}
			catch (Exception ex)
			{
				HandleException(ex);
			}
			finally
			{
				Cursor = Cursors.Default;
			}
		}

		void OnExpirationDateExpired()
		{
			try
			{
				StatusMsg("Manual expiration date expired.", Color.Blue, true);

				App.Instance.ExpirationDate.Deactivate();

				RefreshView();
				RefreshManualExpirationDateButtons();

				txtDurationValue.Clear();
				txtDurationValue.Focus();
				txtDurationValue.ValidateText();
			}
			catch (Exception ex)
			{
				HandleException(ex);
			}
		}
		private void OnExpirationDateActiveStatusChanged(object sender, EventArgs e)
		{
			try
			{
				RefreshView();
			}
			catch (Exception ex)
			{
				HandleException(ex);
			}
		}

		#endregion

		#region Utilities: UI

		private void TimerMsg(bool highlight)
		{
			DateTime time = getTimeOp.Result;
			if (highlight)
			{
				opTime.ForeColor = Color.DarkBlue;
				opTime.Font = new System.Drawing.Font(
					"Courier New"
					, 10F
					, FontStyle.Bold
					, System.Drawing.GraphicsUnit.Point
					, ((byte)(0))
					);
			}
			else
			{
				opTime.ForeColor = SystemColors.ControlText;
				opTime.Font = new System.Drawing.Font(
					"Courier New"
					, 10F
					, FontStyle.Regular
					, System.Drawing.GraphicsUnit.Point
					, ((byte)(0))
					);
			}
			opTime.Text = time.ToShortDateString() + ", " + time.ToLongTimeString();
		}
		private void StatusMsg(string msg, Color statusColor, bool bold)
		{
			StatusMsg(msg, statusColor, bold, 4000);
		}
		private void StatusMsg(string msg, Color statusColor, bool bold, int timeout)
		{
			statusTimer.Stop();
			singleTickTimer.Stop();
			SetStatusFont(statusColor, bold);

			status.Text = msg;
			statusTimer.Interval = timeout;
			statusTimer.Start();
		}
		private void SingleTickStatusMsg(string msg, Color statusColor, bool bold)
		{
			singleTickTimer.Stop();
			statusTimer.Stop();
			opTime.Text = string.Empty;

			SetStatusFont(statusColor, bold);
			status.Text = msg;

			singleTickTimer.Start();
		}
		private void SetStatusFont(Color statusColor, bool bold)
		{

				status.ForeColor = statusColor;
				FontStyle fontStyle = bold ? FontStyle.Bold : FontStyle.Regular;
				status.Font = new Font("Courier New", 10F, fontStyle);

		}

		private void RefreshView()
		{
			MethodInfo method = typeof(ToolStrip).GetMethod("ClearAllSelections", BindingFlags.NonPublic | BindingFlags.Instance);
			method.Invoke(mainMenu, null);

			RefreshManualExpirationDateButtons();

			if (CodeIsLocked(App.Instance.DefaultCodeLockFileName))
			{
				EnableAllControls(false);

				mainMenu.Enabled = true;

				btnUnlockCode.Enabled = mnuUnlockCode.Enabled = btnUnlockCode.Enabled = true;
				btnDeleteLock.Enabled = btnDeleteLock.Visible = mnuDeleteCodeLock.Enabled = true;

				mnuCreateCodeLock.Enabled = btnLockCode.Enabled;

				mnuSetManualExpirationDatetimeToolStripMenuItem.Enabled = btnCustomDurationDialog.Enabled && btnCustomDurationDialog.Visible;
				mnuClearManualExpiration.Enabled = btnCancelManualExpirationDate.Visible;

				btnCancel.Visible = false;
				btnClose.Enabled = true;
				AcceptButton = btnUnlockCode;
				btnUnlockCode.Focus();

				mnuExportCodeLock.Enabled = true;
				mnuImportCodeLock.Enabled = false;
			}
			else // code is not locked
			{
				btnUnlockCode.Enabled = mnuUnlockCode.Enabled = false;
				btnDeleteLock.Enabled = btnDeleteLock.Visible = mnuDeleteCodeLock.Enabled = false;

				mnuSetManualExpirationDatetimeToolStripMenuItem.Enabled = btnCustomDurationDialog.Enabled && btnCustomDurationDialog.Visible;
				mnuClearManualExpiration.Enabled = btnCancelManualExpirationDate.Visible;

				btnCancel.Visible = chkUseMSTime.Checked;
				txtManualCode.Enabled = cmbCodeType.SelectedIndex == 2;
				txtManualCode.ReadOnly = !txtManualCode.Enabled;

				grpCodeLockDetails.Enabled = true;
				btnLockCode.Enabled = mnuCreateCodeLock.Enabled = true;
				EnableOnlyCodeLockControls(true);

				RefreshManualCodeView();
				AcceptButton = btnLockCode;

				mnuExportCodeLock.Enabled = false;
				mnuImportCodeLock.Enabled = true;
			}
			if (btnCancelManualExpirationDate.Visible)
			{
				tt.ToolTipTitle = string.Empty;
			}
			txtDurationValue.ReadOnly = App.Instance.ExpirationDate.Active;

			btnCancel.Enabled = false;
			btnCancel.Visible = false;
			chkUseMSTime.Enabled = true;
			lblOutput.Text = string.Empty;
			btnClose.Enabled = true;
			grpOutput.Enabled = false;

			txtOutput.Text = string.Empty;

			mnuClearLastUnlockedCode.Enabled = !Text.Equals("CodeLocker");

			txtDurationValue.Focus();
			txtDurationValue.Select();

			lblTimeout.Enabled = !chkUseMSTime.Checked;
			cmbTimeout.Enabled = !chkUseMSTime.Checked;
		}
		private void RefreshManualExpirationDateButtons()
		{
			bool manualExpDtActive = App.Instance.ExpirationDate.Active;
         if (manualExpDtActive)
			{
				btnCustomDurationDialog.Visible = mnuSetManualExpirationDatetimeToolStripMenuItem.Enabled = false;
				btnCancelManualExpirationDate.Visible = mnuClearManualExpiration.Enabled = true;
			}
			else if (!App.Instance.ExpirationDate.Active)
			{
				btnCustomDurationDialog.Visible = mnuSetManualExpirationDatetimeToolStripMenuItem.Enabled = true;
				btnCancelManualExpirationDate.Visible = mnuClearManualExpiration.Enabled = false;
			}
		}
		private void InitView(bool initDurationType)
		{
			CodeLockerAppSettings settings = App.Instance.Settings;
			cmbTimeout.SelectedItem = settings.TimeoutMS.ToString();

			if (initDurationType)
			{
				cmbDurationType.SelectedItem = settings.DurationType.ToString();
			}

			RefreshView();

			btnReset.Visible = false;
			mnuActionsMenu.Enabled = true;
		}
		private void InitView()
		{
			InitView(true);
		}
		private void RefreshManualCodeView()
		{
			if (GetActiveCodeType() == CodeType.CreateRandomNumbersAndLettersCode || GetActiveCodeType() == CodeType.CreateRandomNumericCode)
			{
				lblCodeLength.Enabled = true;
				cmbCodeLength.Enabled = true;

				txtManualCode.Enabled = false;
				txtManualCode.Focus();
			}
			else if (GetActiveCodeType() == CodeType.ManuallyEnterCode)
			{
				lblCodeLength.Enabled = false;
				cmbCodeLength.Enabled = false;

				cmbCodeLength.Focus();

				txtManualCode.Enabled = true;
				txtManualCode.ReadOnly = false;
			}
		}
		private void RefreshDurationTextBoxToolTip(bool title)
		{
			//if (!refresh)
			//{
			//	return;
			//}
			//if (!txtDurationValue.ValidationSuccessful || App.Instance.ExpirationDate.Active)
			//{
			//	tt.SetToolTip(txtDurationValue, string.Empty);
			//	return;
			//}

			//ExpirationDate exp = new ExpirationDate();
			//exp.Activate(cmbDurationType.SelectedIndex, double.Parse(txtDurationValue.Text));
			//StringBuilder sb = new StringBuilder();
			//if (title)
			//{
			//	sb.AppendLine();
			//}
			//sb.AppendFormat("{0}  (Now)", App.Instance.MakeTimestamp(DateTime.Now));
			//sb.AppendLine();
			//sb.AppendFormat("{0}  (Calculated)", App.Instance.MakeTimestamp(exp.Value));

			//refresh = false;
			//tt.SetToolTip(txtDurationValue, sb.ToString());
			//tt.ToolTipTitle = string.Empty;
			//tt.ToolTipTitle = title ? "Code-Lock Expiration Date/Time" : string.Empty;
			//refresh = true;
		}
		private void SetTitleWithLastUnlockedCode()
		{
			string lastUnlockedCode = App.Instance.Settings.LastUnlockedCode;
			if (lastUnlockedCode != string.Empty)
			{
				Text = string.Format("CodeLocker - {0}", lastUnlockedCode);
			}
			else
			{
				Text = "CodeLocker";
			}
			mnuClearLastUnlockedCode.Enabled = !Text.Equals("CodeLocker");
		}
		private void FocusTextBox()
		{
			MethodInfo method = typeof(ToolStrip).GetMethod("ClearAllSelections", BindingFlags.NonPublic | BindingFlags.Instance);
			method.Invoke(mainMenu, null);

			if (grpCodeLockDetails.Enabled == false)
			{
				if (!App.Instance.ExpirationDate.Active)
				{
					txtOutput.SelectAll();
					txtOutput.Focus();
				}
				return;
			}
			//
			// set focus to either manual code or duration value textbox
			//
			CodeType codeType = (CodeType)Enum.Parse(typeof(CodeType), cmbCodeType.SelectedItem.ToString());
			if (codeType == CodeType.CreateRandomNumbersAndLettersCode || codeType == CodeType.CreateRandomNumericCode)
			{
				if (!App.Instance.ExpirationDate.Active)
				{
					// focus duration value textbox
					txtDurationValue.SelectAll();
					txtDurationValue.Focus();
				}
			}
			else if (codeType == CodeType.ManuallyEnterCode)
			{
				if (ValidateManualCode() && txtDurationValue.ValidationSuccessful)
				{
					// focus manual value textbox
					//txtManualCode.SelectAll();
					//txtManualCode.Focus();
					// focus duration value textbox
					txtDurationValue.SelectAll();
					txtDurationValue.Focus();
				}
				else if (!ValidateManualCode())
				{
					// focus manual value textbox
					txtManualCode.SelectAll();
					txtManualCode.Focus();
				}
				else if (!txtDurationValue.ValidationSuccessful)
				{
					// focus duration value textbox
					txtDurationValue.SelectAll();
					txtDurationValue.Focus();
				}
			}
		}
		private void FocusActiveButton()
		{
			if (btnReset.Visible)
			{
				AcceptButton = btnReset;
			}
			else if (btnError.Visible && btnError.Enabled)
			{
				AcceptButton = btnError;
			}
			else if (btnError.Visible && !btnError.Enabled && AcceptButton.Equals(btnClose))
			{
				AcceptButton = btnClose;
			}
			else if (btnCancel.Visible)
			{
				AcceptButton = btnCancel;
			}
			else if (CodeIsLocked(App.Instance.DefaultCodeLockFileName))
			{
				AcceptButton = btnUnlockCode;
			}
			else if (!CodeIsLocked(App.Instance.DefaultCodeLockFileName))
			{
				AcceptButton = btnLockCode;
			}
			((Button)AcceptButton).Focus();
		}
		private void EnableOnlyOutputControls(bool enable)
		{
			txtOutput.Enabled = enable;
			txtOutput.ReadOnly = enable;

			lblOutput.Enabled = enable;

			grpOutput.Enabled = enable;

			btnClose.Enabled = enable;
			btnCancel.Enabled = enable;

			chkUseMSTime.Enabled = enable;

			lblTimeout.Enabled = enable;
			cmbTimeout.Enabled = enable;
		}
		private void EnableOnlyCodeLockControls(bool enable)
		{
			btnLockCode.Enabled = enable;
			cmbCodeLength.Enabled = enable;
			cmbCodeType.Enabled = enable;
			cmbDurationType.Enabled = enable;

			txtManualCode.Enabled = enable;
			txtDurationValue.Enabled = enable;

			lblCodeLength.Enabled = enable;
			lblManualCode.Enabled = enable;
			lblCodeType.Enabled = enable;
			lblDurationValue.Enabled = enable;
			lblDurationType.Enabled = enable;

			lblTimeout.Enabled = enable;
			cmbTimeout.Enabled = enable;

			grpCodeLockDetails.Enabled = enable;

			chkUseMSTime.Enabled = enable;
		}
		private void EnableAllControls(bool enable)
		{
			EnableOnlyCodeLockControls(enable);
			EnableOnlyOutputControls(enable);
			mainMenu.Enabled = enable;

			btnLockCode.Enabled = enable;
			btnClose.Enabled = enable;
			btnUnlockCode.Enabled = enable;
			btnCancel.Enabled = enable;

			lblTimeout.Enabled = enable;
			cmbTimeout.Enabled = enable;

			chkUseMSTime.Enabled = enable;
		}
		private void CancelManualExpirationDate()
		{
			StatusMsg("Cancelled manual expiration date-time.", Color.Blue, true);

			App.Instance.ExpirationDate.Deactivate();

			RefreshManualExpirationDateButtons();

			txtDurationValue.ReadOnly = false;
			txtDurationValue.Clear();
			txtDurationValue.Focus();
			txtDurationValue.ValidateText();
		}

		private void HandleException(Exception ex)
		{
			_lastError = ex;

			string msg = new ExceptionMessageMaker().MakeExceptionChainMessage(ex);
			Console.WriteLine(msg);
			//MessageBox.Show(msg, "It Seems We Have An Issue", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

			//			dump.ErrorText = msg;
			StatusMsg("Error occured!", Color.Red, true);

			btnError.Visible = true;

			grpCodeLockDetails.Enabled = false;
			grpDuration.Enabled = false;
			grpOutput.Enabled = false;
			btnLockCode.Visible = false;
			btnUnlockCode.Visible = false;
			btnClose.Visible = true;
			btnClose.Enabled = true;
			cmbTimeout.Enabled = false;
			chkUseMSTime.Enabled = false;
			lblTimeout.Enabled = false;
			btnCancel.Visible = false;

			btnReset.Visible = false;
			mnuActionsMenu.Enabled = false;

			AcceptButton = btnError;
		}
		private bool ValidateManualCode()
		{
			bool bStatus = true;
			if (txtManualCode.Text == null || txtManualCode.Text.Equals(string.Empty))
			{
				bStatus = false;
				_errorProvider.SetError(txtManualCode, "Value is reqired.");
				txtManualCode.BackColor = Color.Yellow;
			}
			else if (txtManualCode.Text.Trim().Length > 35)
			{
				bStatus = false;
				_errorProvider.SetError(txtManualCode, "Code length cannot exceed 35 characters.");
				txtManualCode.BackColor = Color.Yellow;
			}
			else if (txtManualCode.Text.Contains(" "))
			{
				bStatus = false;
				_errorProvider.SetError(txtManualCode, "Code cannot contain whitespace characters.");
				txtManualCode.BackColor = Color.Yellow;
			}
			else
			{
				_errorProvider.SetError(txtManualCode, string.Empty);
				txtManualCode.BackColor = SystemColors.Window;
			}
			return bStatus;
		}
		private CodeType GetActiveCodeType()
		{
			CodeType codeType = (CodeType)Enum.Parse(typeof(CodeType), cmbCodeType.SelectedItem.ToString());
			return codeType;
		}
		private bool CodeIsLocked(string codeLockFile)
		{
			return new CodePersistanceMgr().CodeIsLocked(codeLockFile);
		}
		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams cp = base.CreateParams;
				cp.Style &= ~WS_SYSMENU;
				return cp;
			}
		}
		private void Elevate()
		{
			ProcessStartInfo processInfo = new ProcessStartInfo();
			processInfo.Verb = "runas";
			processInfo.FileName = Application.ExecutablePath;
			try
			{
				Process.Start(processInfo);
			}
			catch (Win32Exception)
			{
				//Do nothing. Probably the user canceled the UAC window
			}
		}
		private void ClearLastUnlockedCode()
		{
			CodePersistanceMgr mgr = new CodePersistanceMgr();
			mgr.DeleteLastUnlockedCode();
			Text = "CodeLocker";
			mnuClearLastUnlockedCode.Enabled = !Text.Equals("CodeLocker");
		}
		private void RefreshCreateManualExpirationDateTimeButtons()
		{
			if (!App.Instance.ExpirationDate.Active)
			{
				bool success = txtDurationValue.ValidationSuccessful;
				((Button)AcceptButton).Enabled = success;
				btnCustomDurationDialog.Enabled = success;
				mnuSetManualExpirationDatetimeToolStripMenuItem.Enabled = btnCustomDurationDialog.Enabled;
				mnuCreateCodeLock.Enabled = btnCustomDurationDialog.Enabled;
			}
		}
		private Button GetActiveButton()
		{
			if (btnError.Visible && btnError.Enabled)
			{
				return btnError;
			}
			else if (!btnError.Visible && btnError.Enabled && !btnLockCode.Visible && !btnUnlockCode.Visible)
			{
				return btnClose;
			}
			else if (btnCancel.Visible)
			{
				return btnCancel;
			}
			else if (btnUnlockCode.Enabled)
			{
				return btnUnlockCode;
			}
			else if (btnLockCode.Enabled)
			{
				return btnLockCode;
			}
			else if (btnReset.Enabled)
			{
				return btnReset;
			}
			else
			{
				return null;
			}
		}
		#endregion

		#region Private Fields

		private const int WM_NCLBUTTONDBLCLK = 0x00A3; //double click on a title bar a.k.a. non-client area of the form
		private const int WS_SYSMENU = 0x80000;

		private GetTimeOp getTimeOp = null;
		private bool _lockCode = true;
		private int count;
		private Exception _lastError = null;
		private Thread _lockCodeThread = null;

		#endregion

		private void otherSettingsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				OtherSettingsForm f = new OtherSettingsForm();
				f.ShowDialog();
			}
			catch (Exception ex)
			{
				HandleException(ex);
			}
		}

		private void mnuExportCodeLock_Click(object sender, EventArgs e)
		{
			try
			{
				DialogResult r = folderBrowser.ShowDialog(this);
				if (r != DialogResult.OK)
					return;

				string targetCodeLockFile = Path.Combine(folderBrowser.SelectedPath, App.Instance.DefaultCodeLockFileName);
				File.Copy(App.Instance.DefaultCodeLockFilePath, targetCodeLockFile, true);


				StatusMsg("Exported code lock.", Color.Blue, false, 5500);
			}
			catch (ArgumentException ex)
			{
				HandleException(ex);
			}
			catch (UnauthorizedAccessException ex)
			{
				HandleException(ex);
			}
			catch (DirectoryNotFoundException ex)
			{
				HandleException(ex);
			}
			catch (FileNotFoundException ex)
			{
				HandleException(ex);
			}
			catch (PathTooLongException ex)
			{
				HandleException(ex);
			}
			catch (IOException ex)
			{
				HandleException(ex);
			}
			catch (Exception ex)
			{
				HandleException(ex);
			}
		}

		private void mnuImportCodeLock_Click(object sender, EventArgs e)
		{
			try
			{
				openFileDialog.InitialDirectory = App.Instance.MyDocuments;
				DialogResult r = openFileDialog.ShowDialog(this);
				if (r != DialogResult.OK)
					return;

				CodePersistanceMgr m = new CodePersistanceMgr();
				//m.Reset();
				m.Import(App.Instance.DefaultCodeLockFileName, openFileDialog.FileName);

				StatusMsg("Imported code lock.", Color.Blue, false, 5500);

				RefreshView();
			}
			catch (Exception ex)
			{
				HandleException(ex);
			}
		}

		private void mnuRandomVideos_Click(object sender, EventArgs e)
		{
			try
			{
				VideoForm vf = new VideoForm();
				vf.Show(this);
			}
			catch (Exception ex)
			{
				HandleException(ex);
			}
		}

		private void mnuDisableTTs_Click(object sender, EventArgs e)
		{
			mnuDisableTTs.Text = mnuDisableTTs.Checked ? "Enable toolti&ps" : "Toolti&ps";
			mnuDisableTTs.Checked = !mnuDisableTTs.Checked;
			tt.Active = mnuDisableTTs.Checked;
		}
	} // class CaretakerMainForm

} // namespace CabalsCorner.CodeLocker.Forms