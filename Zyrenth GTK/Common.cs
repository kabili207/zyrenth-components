using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Zyrenth
{
	public static class Common
	{
		
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="box"> A <see cref="Rectangle"/></param>
		/// <param name="baseColor">A <see cref="Color"/></param>
		/// <param name="mode">A <see cref="LinearGradientMode"/></param>
		/// <returns>
		/// A <see cref="LinearGradientBrush"/>
		/// </returns>
		public static LinearGradientBrush CreateGradient(Rectangle box, Color baseColor, LinearGradientMode mode) {
			
			// Create new colors that are slightly lighter and
			// darker than the system base color
			Color colorL = Color.FromArgb(
				Zyrenth.Clamp(baseColor.R + 35, 0, 255),
				Zyrenth.Clamp(baseColor.G + 35, 0, 255),
				Zyrenth.Clamp(baseColor.B + 35, 0, 255));
			Color colorD = Color.FromArgb(
				Zyrenth.Clamp(baseColor.R - 35, 0, 255),
				Zyrenth.Clamp(baseColor.G - 35, 0, 255),
				Zyrenth.Clamp(baseColor.B - 35, 0, 255));
			return new LinearGradientBrush(box, colorL, colorD, mode);	
		}
	}
}

