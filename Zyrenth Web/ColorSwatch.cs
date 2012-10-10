using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.ComponentModel;
using System.Drawing;

[assembly: WebResource("Zyrenth.Web.CSS.ColorSwatch.css", "text/css")]
[assembly: WebResource("Zyrenth.Web.JS.ColorSwatch.js", "text/javascript")]

namespace Zyrenth.Web
{
	/// <summary>
	/// Summary description for ColorSwatch
	/// </summary>
	[ToolboxBitmap(typeof(ColorSwatch),"Zyrenth.Web.Icons.ColorSwatch.bmp")]
	public class ColorSwatch : Control
	{

		[
		Bindable(true),
		Category("Appearance"),
		DefaultValue("FFFFFF"),
		Description("The hex value of the color.")
		]
		public virtual string HexValue
		{

			get
			{
				object t = ViewState["HexValue"];
				return t as string;
			}
			set
			{
				ViewState["HexValue"] = value;
			}

		}

		[
		Bindable(true),
		Category("Appearance"),
		DefaultValue(""),
		Description("The displayed color name"),
		]
		public virtual string Text
		{
			get
			{
				object t = ViewState["Text"];
				return t as string;
			}
			set
			{
				ViewState["Text"] = value;
			}
		}

		[
		Bindable(false),
		Category("Appearance"),
		Description("The color of the swatch."),
		Browsable(true),
		Editor(typeof(System.Drawing.Design.ColorEditor), typeof(System.Drawing.Design.UITypeEditor)),
		]
		public virtual System.Drawing.Color Color
		{
			get
			{
				return System.Drawing.ColorTranslator.FromHtml("#" + HexValue);
			}
			set
			{
				if (value != null)
					HexValue = string.Format("{0:X2}{1:X2}{2:X2}", value.R, value.G, value.B);
				else
					HexValue = "FFFFFF";
			}
		}


		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design",
			"System.Drawing.Design.UITypeEditor, System.Drawing")]
		public List<string> SecondaryColors
		{
			get
			{
				List<string> t = ViewState["SecondaryColors"] as List<string>;
				if (t == null)
					ViewState["SecondaryColors"] = t = new List<string>();
				return t;
			}
		}

		public ColorSwatch()
		{
		}

		public ColorSwatch(string colorName, string hexValue)
		{
			Text = colorName;
			HexValue = hexValue;
		}

		protected override void Render(HtmlTextWriter writer)
		{
			writer.Write(BuildColorSwatchText(Text, HexValue, SecondaryColors));
			if (this.DesignMode)
			{

			}
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			ScriptHelper.RegisterJQuery(this.Page.ClientScript);
			ScriptHelper.RegisterCss(this.Page.ClientScript, typeof(ColorSwatch), "SwatchCssFile", "ColorSwatch");
			ScriptHelper.RegisterJavascript(this.Page.ClientScript, typeof(ColorSwatch), "SwatchJsFile", "ColorSwatch");

			string js = "<script>$(document).ready(function () { SetupColorSwatch(); });</script>";
			
			this.Page.ClientScript.RegisterStartupScript(typeof(ColorSwatch), "SwatchJsStartup", js, false);


		}

		public static string BuildColorSwatchText(string ColorText, string HexValue)
		{
			return BuildColorSwatchText(ColorText, HexValue, null);
		}
		public static string BuildColorSwatchText(string ColorText, string HexValue, List<string> secondaryColors)
		{
			System.Drawing.Color fore;
			System.Drawing.Color back;
			GetForeAndBackColors(HexValue, out fore, out back);
			string sBack = string.Format("{0:X2}{1:X2}{2:X2}", back.R, back.G, back.B);
			string sFore = string.Format("{0:X2}{1:X2}{2:X2}", fore.R, fore.G, fore.B);
			string s = @"<span class=""color_swatch"" style=""color: #{0}; background-color: #{1}; "">{3}<span class=""swatch_hover"" style=""color: #{0}; background-color: #{1}; "">{2}</span></span>";
			string colors = "";
			if (secondaryColors != null)
			{
				foreach (string color in secondaryColors)
				{
					try
					{
						System.Drawing.Color c = System.Drawing.ColorTranslator.FromHtml("#" + color);
						string sColor = string.Format("{0:X2}{1:X2}{2:X2}", c.R, c.G, c.B);
						colors += string.Format(@"<span class=""color_secondary"" style=""background-color: #{0};""></span>", sColor);
					}
					catch { }
				}
			}
			return string.Format(s, sFore, sBack, ColorText, colors);
		}

		public static void GetForeAndBackColors(string hexValue, out System.Drawing.Color fore, out System.Drawing.Color back)
		{
			try
			{
				if (!string.IsNullOrEmpty(hexValue) && hexValue.Length >= 6)
				{
					back = System.Drawing.ColorTranslator.FromHtml("#" + hexValue);
					int first = back.R;
					int second = back.G;
					int third = back.B;
					if (second > 144)
						fore = System.Drawing.Color.Black;
					else if ((first + second + third) > 480)
						fore = System.Drawing.Color.Black;
					else
						fore = System.Drawing.Color.White;
					return;
				}
			}
			catch { }
			fore = System.Drawing.Color.Black;
			back = System.Drawing.Color.White;
		}
	}
}