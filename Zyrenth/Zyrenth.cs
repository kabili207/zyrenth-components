using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Zyrenth
{
	public static partial class Zyrenth
	{
		
		private static Random random = new Random((int)DateTime.Now.Ticks);

		/// <summary>
		/// Gets a random number generator
		/// </summary>
		public static Random RandomNumberGen
		{
			get { return random; }
		}
		
		/// <summary>
		/// Restricts a value to a certain range
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value">The initial value</param>
		/// <param name="min">The minimum value</param>
		/// <param name="max">The maximum value</param>
		/// <returns>A clamped value that lies between min and max</returns>
		public static T Clamp<T>(T value, T min, T max)
			where T : System.IComparable<T>
		{
			T result = value;
			if (value.CompareTo(max) > 0)
				result = max;
			if (value.CompareTo(min) < 0)
				result = min;
			return result;
		}
		
		/// <summary>
		/// Indicates whether a specified string is null, empty, or consists only of white-space characters.
		/// </summary>
		/// <param name="text">The string to test.</param>
		/// <returns>
		/// true if the value parameter is null or String.Empty, or if value consists exclusively of white-space characters.
		/// </returns>
		/// <remarks>This method is comparable in speed to the Mono 2.8 implementation. It does not, however, even
		/// come close to Microsoft's implementation. I'll figure out how they did it...</remarks>
		public static bool IsNullOrWhiteSpace(string text)
		{
			if (text == null || text.Length == 0)
			{
				return true;
			}
			else
			{
				for (int i = 0; i < text.Length; i++)
				{
					if (Char.IsWhiteSpace(text[i])) { continue; }
					else { return false; }
				}
			}
			return false;
		}
		
		/// <summary>
		/// Indicates whether a specified string is null, empty, or consists only of white-space characters.
		/// </summary>
		/// <param name="text">The string to test.</param>
		/// <returns>
		/// true if the value parameter is null or String.Empty, or if value consists exclusively of white-space characters.
		/// </returns>
		/// <remarks>This method is comparable in speed to the Mono 2.8 implementation. It does not, however, even
		/// come close to Microsoft's implementation. I'll figure out how they did it...</remarks>
		public static bool IsNullOrWhiteSpace2(string text)
		{
			if (text != null)
			{
				for (int i = 0; i < text.Length; i++)
				{
					if (!char.IsWhiteSpace(text[i]))
						return false;
				}
			}
			return true;
		}
		
		/// <summary>
		/// Replaces any variation on line endings (CRLF, CR, LF) and 
		/// replaces them with the operating system's line ending.
		/// </summary>
		/// <param name="text">The text to be sanitized</param>
		/// <returns>A string with normalized line endings</returns>
		public static string CleanLineEndings(string text)
		{
			return Regex.Replace(text, @"(?:\n|\r\n?)", Environment.NewLine, RegexOptions.Compiled);
		}

		/// <summary>
		/// Truncates a string to the maximum length specified
		/// </summary>
		/// <param name="source">The input string</param>
		/// <param name="length">The maximum length</param>
		/// <returns>A string truncated to the length specified</returns>
		public static string Truncate(string source, int length)
		{
			if (source.Length > length)
			{
				source = source.Substring(0, length);
			}
			return source;
		}
		
		/// <summary>
		/// Generates a random alpha-numeric string of the specifed length.
		/// </summary>
		/// <param name="size">The length of string to generate</param>
		/// <returns>A randomly generated string</returns>
		public static string RandomString(int length)
		{
			StringBuilder builder = new StringBuilder();
			char[] pool = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
			for (int i = 0; i < length; i++)
			{
				builder.Append(pool[random.Next(pool.Length)]);
			}

			return builder.ToString();
		}

		internal static byte[] AESCreateKey(byte[] key, int keyLength)
		{
			// Create the Real Key with the given Key Length 
			byte[] rkey = new byte[keyLength];

			// XOR each byte of the Key given with the Real Key 
			// until there's nothing left 
			// This allows for keys longer than our Key Length 
			// and pads short keys to the required length 
			for (int i = 0; i < key.Length; i++)
			{
				rkey[i % keyLength] ^= key[i];
			}

			return rkey;
		}

		/// <summary>
		/// Encrypts the string using MySQL's AES encryption technique and then
		/// converts it to a Base64 encoded string.
		/// </summary>
		/// <param name="clearText">The string to be encrypted</param>
		/// <param name="password">The password that should be used to encrypt the string</param>
		/// <returns>An encrypted version of the specified string</returns>
		public static string MysqlAesEncrypt(string clearText, string password)
		{
			byte[] clearData = Encoding.ASCII.GetBytes(clearText);
			byte[] key = Encoding.ASCII.GetBytes(password);
			MemoryStream ms = new MemoryStream();

			Aes alg = Aes.Create();
			alg.KeySize = 128;
			alg.Mode = CipherMode.ECB;
			alg.Padding = PaddingMode.PKCS7;
			alg.Key = AESCreateKey(key, alg.KeySize / 8);
			CryptoStream cs = new CryptoStream(ms, alg.CreateEncryptor(), CryptoStreamMode.Write);

			cs.Write(clearData, 0, clearData.Length);

			cs.Close();
			return Convert.ToBase64String(ms.ToArray());
		}

		/// <summary>
		/// Takes a Base64 encoded string and decrypts it using MySQL's AES
		/// decryption technique.
		/// </summary>
		/// <param name="cipherText">An encrypted base64 string</param>
		/// <param name="password">The password</param>
		/// <returns>The specified byte array decrypted</returns>
		public static string MysqlAesDecrypt(string cipherText, string password)
		{
			byte[] cipherBytes = Convert.FromBase64String(cipherText);
			byte[] key = Encoding.ASCII.GetBytes(password);
			MemoryStream ms = new MemoryStream();

			Aes alg = Aes.Create();
			alg.KeySize = 128;
			alg.Mode = CipherMode.ECB;
			alg.Padding = PaddingMode.PKCS7;
			alg.Key = AESCreateKey(key, alg.KeySize / 8);
			CryptoStream cs = new CryptoStream(ms, alg.CreateDecryptor(), CryptoStreamMode.Write);

			cs.Write(cipherBytes, 0, cipherBytes.Length);

			cs.Close();

			byte[] newBytes = ms.ToArray();
			return Encoding.ASCII.GetString(newBytes);
		}

	}
}

