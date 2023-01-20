using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;

namespace CabalsCorner.UIUtilities
{
	public static class IconUtility
	{
		#region Class Operations

		/// <summary>
		/// Saves argument Icon 'ico' to a file named 'filename'
		/// </summary>
		/// <param name="ico">Icon to save to file.</param>
		/// <param name="filename">Name of icon filename to be created.</param>
		public static void CreateIconFile(Icon ico, string filename)
		{
			using (StreamWriter iconWriter = new StreamWriter(filename))
			{
				ico.Save(iconWriter.BaseStream);
			}

		} 

		#endregion
	}
}
