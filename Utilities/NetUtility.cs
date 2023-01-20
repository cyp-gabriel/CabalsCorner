using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;

namespace CabalsCorner.Utilities
{
	public class NetUtility
	{
		#region Events/Delegates

		public event EventHandler SendEmailStarted;
		public event EventHandler SendEmailCompleted;

		#endregion

		#region Object Event-Handlers

		void client_SendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
		{
			EventDispatcher.SyncExecute(SendEmailCompleted, this, EventArgs.Empty);

		}

		#endregion

		#region Operations

		public async void SendEmail(
		  int port
		, int timeout
		, string host
		, string username
		, string pwd
		, string emailAddress
		, string subject
		, string body
		)
		{
			using (SmtpClient client = new SmtpClient())
			{
				client.UseDefaultCredentials = true;

				client.Port = port;
				client.EnableSsl = true;
				client.Timeout = timeout;
				client.DeliveryMethod = SmtpDeliveryMethod.Network;
				client.Host = host;
				client.Credentials = new System.Net.NetworkCredential(username, pwd);
				client.SendCompleted += client_SendCompleted;
				MailMessage mail = new MailMessage(emailAddress, emailAddress);
				mail.Subject = subject;
				mail.Body = body;
				mail.BodyEncoding = UTF8Encoding.UTF8;
				mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

				Task t = client.SendMailAsync(mail);
				EventDispatcher.SyncExecute(SendEmailStarted, this, EventArgs.Empty);

				await t;

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

		#endregion
	}
}
