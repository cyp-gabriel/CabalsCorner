using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CabalsCorner.Utilities
{
	public static class ArrayUtility<T>
	{
		#region Class Operations

		public static void ShuffleArray(ref T[] array)
		{
			for (int index = array.Length - 1; index > -1; index--)
			{
				Random rnd = new Random();
				int randomIndex = rnd.Next(0, index);

				T temporaryValue = array[index];
				array[index] = array[randomIndex];
				array[randomIndex] = temporaryValue;
			}
		} 

		#endregion
	}
}
