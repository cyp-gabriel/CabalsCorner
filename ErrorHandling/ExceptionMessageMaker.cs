using System;
using System.Diagnostics;
using System.Text;
using System.Configuration;
using System.Data.SqlTypes;
using Microsoft.Data.SqlClient;

namespace CabalsCorner.ErrorHandling
{
	public class ExceptionMessageMaker: IExceptionMessageMaker
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
					sb.AppendFormat("{0}[InnerException encountered...]{1}", Environment.NewLine, Environment.NewLine);
					sb.Append(MakeExceptionChainMessage(ex.InnerException));
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
				sb.Append("\nSystem.Exception Details\n======================================================\n");
				sb.AppendFormat("{0,12}\t{1,-256}\n", "Source:", ex.Source);
				sb.AppendFormat("{0,12}\t{1,-256}\n\n", "Message:", ex.Message);

				if (ex.TargetSite != null)
				{
					if (ex.TargetSite.Name != string.Empty)
					{
						sb.AppendFormat("{0,12}\t{1,-256}\n", "TargetSite:", ex.TargetSite);
					}
				}
				if (ex.HelpLink != null)
				{
					if (ex.HelpLink != string.Empty)
					{
						sb.AppendFormat("{0,12}\t{1,-256}\n", "HelpLink:", ex.HelpLink);
					}
				}

				sb.Append("\nOther Info\n--------------------------------------------------------------------------------------\n");
				sb.AppendFormat("{0,12}\t{1,-256}\n", "MachineName:", Environment.MachineName);
				sb.AppendFormat("{0,12}\t{1,-256}\n", "UserName:", Environment.UserName);

				sb.Append("\nCall Stack\n--------------------------------------------------------------------------------------\n");
				if (ex.StackTrace != null && ex.StackTrace != string.Empty)
				{
					sb.AppendFormat("{0,-256}\n", ex.StackTrace);
				}

				return sb.ToString();
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}
		public string MakeExceptionMessage(SqlException ex)
		{
			try
			{
				StringBuilder sb = new StringBuilder();

				for (int i = 0; i < ex.Errors.Count; i++)
				{
					sb.Append("\nSystem.Data.SqlException Details\n=============================================================\n");
					sb.AppendFormat("{0,12}\t{1,-256}\n", "Index #:", i.ToString());
					sb.AppendFormat("{0,12}\t{1,-256}\n", "Message:", ex.Message);
					sb.AppendFormat("{0,12}\t{1,-256}\n", "Source:", ex.Source);
					sb.AppendFormat("{0,12}\t{1,-256}\n", "Procedure:", ex.Procedure);
					sb.AppendFormat("{0,12}\t{1,-256}\n\n", "Line number:", ex.Errors[i].LineNumber);
				}

				if (ex.TargetSite != null)
				{
					if (ex.TargetSite.Name != string.Empty)
					{
						sb.AppendFormat("{0,12}\t{1,-256}\n", "TargetSite:", ex.TargetSite);
					}
				}
				if (ex.HelpLink != null)
				{
					if (ex.HelpLink != string.Empty)
					{
						sb.AppendFormat("{0,12}\t{1,-256}\n", "HelpLink:", ex.HelpLink);
					}
				}

				sb.Append("\nOther Info\n--------------------------------------------------------------------------------------\n");
				sb.AppendFormat("{0,12}\t{1,-256}\n", "MachineName:", Environment.MachineName);
				sb.AppendFormat("{0,12}\t{1,-256}\n", "UserName:", Environment.UserName);

				sb.Append("\nCall Stack\n--------------------------------------------------------------------------------------\n");
				if (ex.StackTrace != null && ex.StackTrace != string.Empty)
				{
					sb.AppendFormat("{0,-256}\n", ex.StackTrace);
				}

				return sb.ToString();
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}
		public string MakeInvalidFormatMessage(string srcMemberName, string argName, string argValue)
		{
			try
			{
				return string.Format(InvalidFormatMessageFormat, srcMemberName, argName, argValue);
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}
		public string MakeUserExceptionMessage(Exception ex)
		{
			try
			{
				StringBuilder sb = new StringBuilder();
				Exception baseEx = (ex.InnerException == null) ? ex : ex.GetBaseException();
				sb.AppendFormat("{0,-18}\t{1,-128}\n", "Error Message:", baseEx.Message);
				sb.AppendFormat("{0,-18}\t{1,-128}\n", "Source Assembly:", baseEx.Source);
				return sb.ToString();
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		#endregion

		#region Private Fields

		private const string InvalidFormatMessageFormat = "{0}: argument '{1}' has invalid format: \"{2}\"\n";

		#endregion
	}
}
