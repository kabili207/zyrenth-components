using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

[assembly: WebResource("Zyrenth.Web.JS.jquery-1.4.1.min.js", "text/javascript")]
[assembly: WebResource("Zyrenth.Web.JS.jquery-1.7.2.min.js", "text/javascript")]

namespace Zyrenth.Web
{

	internal class ScriptHelper
	{
		/// <summary>
		/// Registers the jQuery client script to the page.
		/// </summary>
		/// <param name="cs">The ClientScriptManager to assign the script to.</param>
		internal static void RegisterJQuery(ClientScriptManager cs)
		{
			cs.RegisterClientScriptResource(typeof(ScriptHelper), "Zyrenth.Web.JS.jquery-1.7.2.min.js");
		}

		internal static void RegisterCss(ClientScriptManager cs, Type type, string key, string cssName)
		{
			string css = "<link href=\"" + cs.GetWebResourceUrl(type, "Zyrenth.Web.CSS." + cssName + ".css") +
				"\" type=\"text/css\" rel=\"stylesheet\" />";
			cs.RegisterClientScriptBlock(type, key, css, false);
		}

		internal static void RegisterJavascript(ClientScriptManager cs, Type type, string key, string jsName)
		{
			string js = "<script src=\"" + cs.GetWebResourceUrl(type, "Zyrenth.Web.JS." + jsName + ".js") +
				"\" type=\"text/javascript\"></script>";

			cs.RegisterStartupScript(type, key, js, false);
		}
	}
}
