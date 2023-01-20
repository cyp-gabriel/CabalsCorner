using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Globalization;
using System.ComponentModel.Design.Serialization;
using System.Reflection;

namespace CabalsCorner.Utilities.CustomDataSource
{
	 [Serializable()]
	 public class NotCopied
	 {
		  static NotCopied _Value = new NotCopied();

		  public static NotCopied Value
		  {
				get { return _Value; }
		  }
	 }
}
