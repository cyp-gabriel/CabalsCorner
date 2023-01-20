using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CabalsCorner.ErrorHandling
{
	/// <summary>
	/// Exception message maker interface.  This interface is used to create formatted exception messages.
	/// </summary>
	public interface IExceptionMessageMaker
	{
		/// <summary>
		/// Makes string message containing all important exception information.
		/// </summary>
		/// <param name="ex">Exception thrown.</param>
		/// <returns>Formatted string message detailing exception information.</returns>
		string MakeExceptionChainMessage(Exception ex);
	}
}
