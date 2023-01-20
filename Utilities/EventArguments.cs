using System;
using System.Collections;

namespace CabalsCorner.Utilities
{
	/// <summary>
	/// Convenient wrapper around EventArgs class, which is by-itself...useless.
	/// </summary>
	[Serializable]
	public class EventArguments : EventArgs
	{
		#region Public Fields

		public ArrayList Args = new ArrayList();

		#endregion

		#region Constructors
		public EventArguments(object[] args)
		{
			this.Args = new ArrayList(args);
		}

		#endregion

		#region Class Operations
		public static object[] EmptyArgs()
		{
			ArrayList ra = new ArrayList();
			return (object[])ra.ToArray(typeof(object));
		}

		#endregion

		#region Class Operators

		public static implicit operator object[](EventArguments e)
		{
			return (object[])e.Args.ToArray(typeof(object));
		}

		#endregion

		#region Indexors
		public object this[int index]
		{
			get
			{
				return this.Args[index];
			}
			set
			{
				this.Args[index] = value;
			}
		}

		#endregion
	}
}
