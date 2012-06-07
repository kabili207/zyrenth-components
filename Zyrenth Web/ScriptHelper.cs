using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

[assembly: WebResource("Zyrenth.Web.JS.jquery-1.4.1.min.js", "text/javascript")]

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
			cs.RegisterClientScriptResource(typeof(ScriptHelper), "Zyrenth.Web.JS.jquery-1.4.1.min.js");
		}
	}
}
