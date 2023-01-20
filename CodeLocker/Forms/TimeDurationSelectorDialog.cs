using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CabalsCorner.CodeLocker.UserControls;
using CabalsCorner.CodeLocker.Classes;

namespace CabalsCorner.CodeLocker.Forms
{
	 internal partial class TimeDurationSelectorDialog : Form
	 {
		 #region Ctor(s)

		 public TimeDurationSelectorDialog()
		 {
			 InitializeComponent();
			 timeDurationSelectorControl1.ExpirationDateExpired += timeDurationSelectorControl1_ExpirationDateExpired;
			 timeDurationSelectorControl1.ExpirationDatetimeChanged += timeDurationSelectorControl1_ExpirationDatetimeChanged;

			 Disposed += TimeDurationSelectorDialog_Disposed;
		 } 

		 #endregion

		 #region Message-Handlers

		 private void TimeDurationSelectorDialog_Load(object sender, EventArgs e)
		 {
			 if (DesignMode)
			 {
				 return;
			 }

			 AcceptButton = btnAcceptDuration;
			 CancelButton = btnCancel;
			 btnAcceptDuration.Focus();
		 }

		 private void cmbDurationType_SelectedIndexChanged(object sender, EventArgs e)
		 {
		 }
		 private void btnCancel_Click(object sender, EventArgs e)
		 {
			 App.Instance.ExpirationDate.Deactivate();
			 Close();
		 }
		 private void btnAcceptDuration_Click(object sender, EventArgs e)
		 {
			 App.Instance.ExpirationDate.Active = true;
			 Close();
		 }

		 void timeDurationSelectorControl1_ExpirationDatetimeChanged(object sender, EventArgs e)
		 {
			 AcceptButton = btnAcceptDuration;
			 ((Button)AcceptButton).Focus();
		 }
		 void TimeDurationSelectorDialog_Disposed(object sender, EventArgs e)
		 {
			 timeDurationSelectorControl1.ExpirationDateExpired -= timeDurationSelectorControl1_ExpirationDateExpired;
			 timeDurationSelectorControl1.ExpirationDatetimeChanged -= timeDurationSelectorControl1_ExpirationDatetimeChanged;
		 }
		 void timeDurationSelectorControl1_ExpirationDateExpired(object sender, EventArgs e)
		 {
			 btnAcceptDuration.Enabled = false;
			 AcceptButton = btnCancel;
		 }

		 #endregion

		 #region Properties: Read-Only

		 public TimeDurationSelectorControl DurationControl
		 {
			 get
			 {
				 return this.timeDurationSelectorControl1;
			 }
		 } 

		 #endregion
	 }
}
