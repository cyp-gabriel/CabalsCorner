using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

using CabalsCorner.Utilities;

namespace CabalsCorner.CodeLocker.Classes
{
	internal static class AES
	{
		#region Class Operations

		public static byte[] Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
		{
			byte[] encryptedBytes = null;

			// Set your salt here, change it to meet your flavor:
			// The salt bytes must be at least 8 bytes.
			byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

			using (MemoryStream ms = new MemoryStream())
			{
				using (RijndaelManaged AES = new RijndaelManaged())
				{
					AES.KeySize = 256;
					AES.BlockSize = 128;

					var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
					AES.Key = key.GetBytes(AES.KeySize / 8);
					AES.IV = key.GetBytes(AES.BlockSize / 8);

					AES.Mode = CipherMode.CBC;

					using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
					{
						cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
						cs.Close();
					}
					encryptedBytes = ms.ToArray();
				}
			}

			return encryptedBytes;
		}
		public static void EncryptFile(string file, string password, string encFile)
		{
			byte[] bytesToBeEncrypted = File.ReadAllBytes(file);
			byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

			// Hash the password with SHA256
			passwordBytes = System.Security.Cryptography.SHA256.Create().ComputeHash(passwordBytes);

			byte[] bytesEncrypted = AES.Encrypt(bytesToBeEncrypted, passwordBytes);

			string fileEncrypted = encFile;

			File.WriteAllBytes(fileEncrypted, bytesEncrypted);
		}

		public static byte[] Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
		{
			byte[] decryptedBytes = null;

			// Set your salt here, change it to meet your flavor:
			// The salt bytes must be at least 8 bytes.
			byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

			using (MemoryStream ms = new MemoryStream())
			{
				using (RijndaelManaged AES = new RijndaelManaged())
				{
					AES.KeySize = 256;
					AES.BlockSize = 128;

					var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
					AES.Key = key.GetBytes(AES.KeySize / 8);
					AES.IV = key.GetBytes(AES.BlockSize / 8);

					AES.Mode = CipherMode.CBC;

					using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
					{
						cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
						cs.Close();
					}
					decryptedBytes = ms.ToArray();
				}
			}

			return decryptedBytes;
		}

		/// <summary>
		/// Decrypts file 'fileEncrypted' using password 'password'.  Decrypted output is written to file 
		/// 'fileToDecryptTo'.
		/// </summary>
		/// <param name="fileEncrypted">Path to encrypted file.</param>
		/// <param name="password">Encryption key.</param>
		/// <param name="fileToDecryptTo">File that decrypted output is written to.</param>
		public static void DecryptFile(string fileEncrypted, string password, string fileToDecryptTo)
		{
			byte[] bytesToBeDecrypted = File.ReadAllBytes(fileEncrypted);

			byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
			passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

			byte[] bytesDecrypted = Decrypt(bytesToBeDecrypted, passwordBytes);

			File.WriteAllBytes(fileToDecryptTo, bytesDecrypted);
		}

		/// <summary>
		/// Decrypts file 'fileEncrypted' using password 'password'.  Output is returned in StreamReader.
		/// </summary>
		/// <param name="fileEncrypted">Path to encrypted file.</param>
		/// <param name="password">Encryption key.</param>
		/// <returns>StreamReader containing decrypted output.</returns>
		public static StreamReader DecryptFile(string fileEncrypted, string password)
		{
			byte[] bytesToBeDecrypted = File.ReadAllBytes(fileEncrypted);

			byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
			passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

			byte[] bytesDecrypted = AES.Decrypt(bytesToBeDecrypted, passwordBytes);

			return new StreamReader(new MemoryStream(bytesDecrypted));
		} 

		#endregion
	}
}