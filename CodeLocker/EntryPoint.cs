using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CabalsCorner.CodeLocker.Forms;
using System.Reflection;
using System.Drawing;
using System.Drawing.Imaging;
using System.Security.Principal;
using System.Diagnostics;
using System.ComponentModel;

using CabalsCorner.ErrorHandling;

namespace CabalsCorner.CodeLocker
{
	static class EntryPoint
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{

			AppDomain currentDomain = AppDomain.CurrentDomain;
			currentDomain.UnhandledException += new UnhandledExceptionEventHandler(MyHandler);
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new CodeLockerForm());

		}
		static void MyHandler(object sender, UnhandledExceptionEventArgs args)
		{
			Exception e = (Exception)args.ExceptionObject;
			//Console.WriteLine("MyHandler caught : " + e.Message);
			//Console.WriteLine("Runtime terminating: {0}", args.IsTerminating);
			string msg = new ExceptionMessageMaker().MakeExceptionChainMessage(e);
			Console.WriteLine(msg);
			MessageBox.Show("Handler caught: " + msg + "\nRuntime terminating: " + args.IsTerminating);
		}
	}
}
