using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Zyrenth
{
	public static class Common
	{
		
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
		
		public static LinearGradientBrush CreateGradient(Rectangle box, Color baseColor, LinearGradientMode mode) {
			
			// Create new colors that are slightly lighter and
			// darker than the system base color
			Color colorL = Color.FromArgb(
				Common.Clamp(baseColor.R + 35, 0, 255),
				Common.Clamp(baseColor.G + 35, 0, 255),
				Common.Clamp(baseColor.B + 35, 0, 255));
			Color colorD = Color.FromArgb(
				Common.Clamp(baseColor.R - 35, 0, 255),
				Common.Clamp(baseColor.G - 35, 0, 255),
				Common.Clamp(baseColor.B - 35, 0, 255));
			return new LinearGradientBrush(box, colorL, colorD, mode);	
		}
	}
}

