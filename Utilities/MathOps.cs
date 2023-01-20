using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CabalsCorner.Utilities
{
	class MathOps
	{
		/// <summary>
		/// Usage: long r = LongRandom(100000000000000000, 100000000000000050, new Random());
		/// </summary>
		/// <param name="min"></param>
		/// <param name="max"></param>
		/// <param name="rand"></param>
		/// <returns></returns>
		public static long LongRandom(long min, long max, Random rand)
		{
			byte[] buf = new byte[8];
			rand.NextBytes(buf);
			long longRand = BitConverter.ToInt64(buf, 0);

			return (Math.Abs(longRand % (max - min)) + min);
		}
	}
}
