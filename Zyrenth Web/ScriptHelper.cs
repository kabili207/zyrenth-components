using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

[assembly: WebResource("Zyrenth.Web.JS.jquery-1.4.1.min.js", "text/javascript")]
[assembly: WebResource("Zyrenth.Web.JS.jquery-1.7.2.min.js", "text/javascript")]

namespace Zyrenth.Web
{

	public class ScriptHelper
	{
		/// <summary>
		/// Gets or sets a value indicating if the web controls should use the built-in jQuery.
		/// The built-in version of jQuery used is 1.7.2.
		/// </summary>
		public static bool EnableBuiltinJQuery { get; set; }

		/// <summary>
		/// Registers the built-in jQuery client script to the page if it is enabled.
		/// The built-in version of jQuery used is 1.7.2.
		/// </summary>
		/// <param name="cs">The ClientScriptManager to assign the script to.</param>
		internal static void RegisterJQuery(ClientScriptManager cs)
		{
			if(EnableBuiltinJQuery)
				cs.RegisterClientScriptResource(typeof(ScriptHelper), "Zyrenth.Web.JS.jquery-1.7.2.min.js");
		}

		/// <summary>
		/// Registers the specified CSS file on the page.
		/// </summary>
		/// <param name="cs">The ClientScriptManager to assign the script to</param>
		/// <param name="type">The type associated with this CSS file</param>
		/// <param name="key">The key associated with this CSS file</param>
		/// <param name="cssName">The name of the CSS file, without the .css extension</param>
		internal static void RegisterCss(ClientScriptManager cs, Type type, string key, string cssName)
		{
			string css = "<link href=\"" + cs.GetWebResourceUrl(type, "Zyrenth.Web.CSS." + cssName + ".css") +
				"\" type=\"text/css\" rel=\"stylesheet\" />";
			cs.RegisterClientScriptBlock(type, key, css, false);
		}

		/// <summary>
		/// Registers the specified javascript file on the page.
		/// </summary>
		/// <param name="cs">The ClientScriptManager to assign the script to</param>
		/// <param name="type">The type associated with this javascript file</param>
		/// <param name="key">The key associated with this javascript file</param>
		/// <param name="cssName">The name of the javascript file, without the .js extension</param>
		internal static void RegisterJavascript(ClientScriptManager cs, Type type, string key, string jsName)
		{
			string js = "<script src=\"" + cs.GetWebResourceUrl(type, "Zyrenth.Web.JS." + jsName + ".js") +
				"\" type=\"text/javascript\"></script>";

			cs.RegisterStartupScript(type, key, js, false);
		}
	}
}
