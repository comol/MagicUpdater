using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace MagicUpdaterCommon.Helpers
{
	public static class MD5Works
	{
		public static string GetMd5Hash(string input)
		{
			using (MD5 md5Hash = MD5.Create())
			{
				// Convert the input string to a byte array and compute the hash.
				byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

				// Create a new Stringbuilder to collect the bytes
				// and create a string.
				StringBuilder sBuilder = new StringBuilder();

				// Loop through each byte of the hashed data 
				// and format each one as a hexadecimal string.
				for (int i = 0; i < data.Length; i++)
				{
					sBuilder.Append(data[i].ToString("x2"));
				}

				// Return the hexadecimal string.

				return sBuilder.ToString();
			}
		}

		// Verify a hash against a string.
		public static bool VerifyMd5Hash(string input, string hash)
		{
			using (MD5 md5Hash = MD5.Create())
			{
				// Hash the input.
				string hashOfInput = GetMd5Hash(input);

				// Create a StringComparer an compare the hashes.
				StringComparer comparer = StringComparer.OrdinalIgnoreCase;

				return (comparer.Compare(hashOfInput, hash) == 0);
			}
		}

		public static bool CompareHahes(string hash1, string hash2)
		{
			if(string.IsNullOrEmpty(hash1)||string.IsNullOrEmpty(hash2))
			{
				return false;
			}

			StringComparer comparer = StringComparer.OrdinalIgnoreCase;

			return (comparer.Compare(hash1, hash2) == 0);
		}

		public static string GetFileHash(string fullPath)
		{
			using (var md5 = MD5.Create())
			{
				using (var stream = File.OpenRead(fullPath))
				{
					byte[] data = md5.ComputeHash(stream);

					StringBuilder sBuilder = new StringBuilder();

					for (int i = 0; i < data.Length; i++)
					{
						sBuilder.Append(data[i].ToString("x2"));
					}

					return sBuilder.ToString();
				}
			}
		}
	}
}
