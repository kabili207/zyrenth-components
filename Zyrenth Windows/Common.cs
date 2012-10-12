using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Reflection;


namespace Zyrenth
{
	public static class Common
	{
		private static bool? _useCompatibleTextRendering = null;
		private static bool? _visualStylesEnabled = null;

		/// <summary>
		/// 
		/// </summary>
		public static bool UseCompatibleTextRendering
		{
			get
			{
				try
				{
					FieldInfo info = typeof(Control).GetField("UseCompatibleTextRenderingDefault", BindingFlags.Static | BindingFlags.NonPublic);
					if (info == null) // Not using .Net, try Mono
						info = typeof(Application).GetField("use_compatible_text_rendering", BindingFlags.Static | BindingFlags.NonPublic);
					object o = null;
					if (info != null)
						o = info.GetValue(null); // Parameter is ignored on static fields, just pass null.
					return o.Equals(true);
				}
				catch
				{
					return false;
				}
			}
		}
		
		/// <summary>
		/// 
		/// </summary>
		public static bool VisualStylesEnabled
		{
			get
			{
				try
				{
					FieldInfo info = typeof(Application).GetField("useVisualStyles", BindingFlags.Static | BindingFlags.NonPublic);
					if (info == null) // Not using .Net, try Mono
						info = typeof(Application).GetField("visual_styles_enabled", BindingFlags.Static | BindingFlags.NonPublic);
					object o = null;
					if (info != null)
						o = info.GetValue(null); // Parameter is ignored on static fields, just pass null.
					return o.Equals(true);
				}
				catch
				{
					return true;
				}
			}
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="box">A Rectangle</param>
		/// <param name="baseColor">A Color</param>
		/// <param name="mode">A LinearGradientMode</param>
		/// <returns>
		/// A <see cref="LinearGradientBrush"/>
		/// </returns>
		public static LinearGradientBrush CreateGradient(Rectangle box, Color baseColor, LinearGradientMode mode) {
			
			// Create new colors that are slightly lighter and
			// darker than the system base color
			Color colorL = Color.FromArgb(
				Extentions.Clamp(baseColor.R + 35, 0, 255),
				Extentions.Clamp(baseColor.G + 35, 0, 255),
				Extentions.Clamp(baseColor.B + 35, 0, 255));
			Color colorD = Color.FromArgb(
				Extentions.Clamp(baseColor.R - 35, 0, 255),
				Extentions.Clamp(baseColor.G - 35, 0, 255),
				Extentions.Clamp(baseColor.B - 35, 0, 255));
			return new LinearGradientBrush(box, colorL, colorD, mode);	
		}
	}
}

