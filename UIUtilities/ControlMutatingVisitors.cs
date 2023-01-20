using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CabalsCorner.Utilities;

namespace CabalsCorner.UIUtilities
{
	/// <summary>
	/// ControlsEnumeratingMutator makes things well
	/// </summary>
	public class ControlsEnumeratingMutator
	{
		#region Class Operations: Recursive Validation

		public static Control[] GetAllControls(Control topControl)
		{
			Debug.Assert(topControl != null);

			ArrayList allControls = new ArrayList();
			Queue queue = new Queue();
			queue.Enqueue(topControl.Controls);

			while (queue.Count > 0)
			{
				Control.ControlCollection controls = (Control.ControlCollection)queue.Dequeue();
				if (controls == null || controls.Count == 0)
					continue;

				foreach (Control control in controls)
				{
					allControls.Add(control);
					queue.Enqueue(control.Controls);
				}
			}

			return (Control[])allControls.ToArray(typeof(Control));
		}
		public static void SetPropertyAllControls(Control topControl, string propertyName, object value)
		{
			Control[] cs = ControlsEnumeratingMutator.GetAllControls(topControl);
			foreach (Control c in cs)
			{
				if (Reflector.ObjectHasProperty(c, propertyName))
				{
					Reflector.SetPropertyValue(c, propertyName, value);
				}
			}
		}
		public static void ValidateAllControls(Form form)
		{
			Control[] controls = ControlsEnumeratingMutator.GetAllControls(form as Control);
			foreach (Control control in controls)
			{
				control.Focus();
				form.Validate();
			}
		}

		#endregion
	}
}
