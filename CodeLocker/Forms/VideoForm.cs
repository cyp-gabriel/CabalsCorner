using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CabalsCorner;
using CabalsCorner.UIUtilities;
using CabalsCorner.ErrorHandling;
using CabalsCorner.CodeLocker;

namespace CabalsCorner.CodeLocker.Forms
{
	public partial class VideoForm : Form
	{
		public VideoForm()
		{
			InitializeComponent();
		}

		private void VideoForm_Load(object sender, EventArgs e)
		{
			try
			{
				wb.Navigate("https://youtu.be/Ad8Toz1kgYw");
			}
			catch (Exception ex)
			{

				HandleException(ex);
			}
		}

		private void HandleException(Exception ex)
		{
			string msg = new ExceptionMessageMaker().MakeExceptionChainMessage(ex);
			MessageBox.Show(msg, "It Seems We Have An Issue", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
		}
	}
}
