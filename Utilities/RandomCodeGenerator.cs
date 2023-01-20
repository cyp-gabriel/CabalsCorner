using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CabalsCorner.Utilities
{
	public static class RandomCodeGenerator
	{
		#region Class Operations

		public static long MakeCombinationCodeMaxValue(long codeLength)
		{
			string max = string.Empty;
			for (long i = 0; i < codeLength; i++)
			{
				max += "9";
			}
			long maxValue = Convert.ToInt64(max);
			return maxValue;
		}
		public static long MakeCombinationCodeMinValue(long codeLength)
		{
			string min = "1";
			bool appendOne = false;
			for (long i = 0; i < codeLength - 2; i++)
			{
				if (appendOne)
				{
					min += "1";
					appendOne = false;
				}
				else
				{
					min += "0";
					appendOne = true;
				}
			}
			long minValue = Convert.ToInt64(min);
			return minValue;
		}
		public static string CreateRandomNumericCode(long minValue, long maxValue)
		{
			int diff = maxValue.ToString().Length - minValue.ToString().Length;
			if (diff > 1)
				throw new ArgumentException("EncrypterDecrypter.CreateRandomNumberString: 'minValue' needs to be either the same length as 'maxValue', or one digit shorter.");

			// generate number between minValue-maxValue
			Random rnd = new Random();
			//int randomNumber = rnd.Next(Convert.ToInt32(minValue), Convert.ToInt32(maxValue));
			long randomNumber = MathOps.LongRandom(minValue, maxValue, rnd);

			// if number is 5 digits, prepend a "0"
			if (randomNumber.ToString().Length == minValue.ToString().Length)
				return "0" + randomNumber.ToString();
			else
				return randomNumber.ToString();
		}
		public static string CreateRandomAlphaNumericString(int stringLength)
		{
			var chars = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
			var random = new Random();
			var result = new string(
				 Enumerable.Repeat(chars, stringLength)
							  .Select(s => s[random.Next(s.Length)])
							  .ToArray());
			return result;
		}
		public static string CreateRandomAlphaNumericStringWithSymbols(int stringLength)
		{
			var chars = "0123456789abcdefghijklmnopqrstuvwxyz`~!@#$%^&*()-_+=\\|/?'\",<.>{}:;";
			var random = new Random();
			var result = new string(
				 Enumerable.Repeat(chars, stringLength)
							  .Select(s => s[random.Next(s.Length)])
							  .ToArray());
			return result;
		} 

		#endregion
	}
}
