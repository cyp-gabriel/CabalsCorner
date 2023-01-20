using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CabalsCorner.ErrorHandling;

namespace CabalsCorner.ErrorHandling
{
	/// <summary>
	/// 
	/// </summary>
	public class HtmlExceptionMessageMaker : IExceptionMessageMaker
	{
		#region Interface: IExceptionMessageMaker

		public string MakeExceptionChainMessage(Exception ex)
		{
			try
			{
				StringBuilder sb = new StringBuilder();
				sb.Append(MakeExceptionMessage(ex));

				if (ex.InnerException != null)
				{
					sb.AppendLine("<H1>Inner Exception Details **********************************</H1>");
					sb.Append(this.MakeExceptionMessage(ex.InnerException));
				}

				return sb.ToString();
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		#endregion

		#region Operations

		public string MakeExceptionMessage(Exception ex)
		{
			try
			{
				StringBuilder sb = new StringBuilder();
				sb.AppendLine("<H2>System.Exception Details ================================</H2>");
				string src = String.Format("{0,12}\t{1,-256}\n", "Source:", ex.Source);
				sb.AppendFormat("<P>{0}</P>\n", src);
				//string msg = String.Format("{0,12}\t{1,-256}\n\n", "Message:", ex.Message);
				//sb.AppendFormat("<P>{0}</P>\n", msg);
				sb.AppendLine("<P>Message:</P>");
				sb.AppendFormat("<TEXTAREA STYLE='width: 95%; margin-left: 1em; height: 140px;'>{0}</TEXTAREA>", ex.Message);

				if (ex.TargetSite != null)
				{
					if (ex.TargetSite.Name != string.Empty)
					{
						string targetSite = String.Format("{0,12}\t{1,-256}\n", "TargetSite:", ex.TargetSite);
						sb.AppendFormat("<P>{0}</P>\n", targetSite);
					}
				}
				if (ex.HelpLink != null)
				{
					if (ex.HelpLink != string.Empty)
					{
						string helpLink = String.Format("{0,12}\t{1,-256}\n", "HelpLink:", ex.HelpLink);
						sb.AppendFormat("<P>{0}</P>\n", helpLink);
					}
				}
				sb.AppendLine("<H3>Other Info --------------------------------------------------------------------------------------------</H3>");

				sb.AppendFormat("<P>{0,12}\t{1,-256}</P>\n", "MachineName:", Environment.MachineName);
				sb.AppendFormat("<P>{0,12}\t{1,-256}\n</P>\n", "UserName:", Environment.UserName);
				sb.AppendLine("<H3>Call Stack---------------------------------------------------------------------------------------------</H3>");
				if (ex.StackTrace != null && ex.StackTrace != string.Empty)
				{
					sb.AppendFormat("<P>{0,-256}</P>\n", ex.StackTrace);
				}

				return sb.ToString();
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		#endregion
	}
}
