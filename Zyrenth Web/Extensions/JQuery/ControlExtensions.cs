using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Zyrenth.Web.Extensions.JQuery
{
	public static class ControlExtensions
	{
		/// <summary>
		/// Opens the specified control as a modal popup.
		/// </summary>
		/// <param name="control">The control to open as a modal popup</param>
		internal static void OpenPopupInternal(this Control control)
		{
			Page oPage = control.Page;
			IEnumerable<Control> oControls = control.Controls.Flatten();
			List<string> buttons = new List<string>();
			StringBuilder sbOnOpen = new StringBuilder();

			foreach (Control oControl in oControls)
			{
				if (oControl is Button)
				{
					Button oButton = (Button)oControl;

					// Because we have to use the jQuery hide() below, we get the added
					// functionality of allowing the developer to selectively hide buttons.
					if (!oButton.Visible)
						continue;

					string sIsModal = oButton.Attributes["isModal"];
					if (sIsModal != null && sIsModal.ToLower() == "true")
					{
						string sButtonText = string.Format(" \"{0}\" : function() {{ {1}; }} ", oButton.Text,
							oPage.ClientScript.GetPostBackEventReference(oButton, string.Empty));
						buttons.Add(sButtonText);

						// We have to use jQuery hide otherwise ASP throws a server error.
						sbOnOpen.Append("$('#" + oButton.ClientID + "').hide();");
					}
				}
			}
			string sButtonJs = "{" + string.Join(", ", buttons.ToArray()) + "}";
			string sOpenEvent = string.Format("function() {{ {0} }}", sbOnOpen);
			oPage.RegisterModalScripts();
			ScriptManager.RegisterStartupScript(oPage, control.GetType(), "ShowPopup" + control.ID, "openModalDiv('" + control.ClientID + "', " + sButtonJs + ", " + sOpenEvent + ");", true);
		}


		/// <summary>
		/// Closed the currently open modal popup
		/// </summary>
		/// <param name="control">The control to close</param>
		internal static void ClosePopupInternal(this Control control)
		{
			var oPage = control.Page;
			oPage.RegisterModalScripts();
			ScriptManager.RegisterStartupScript(oPage, control.GetType(), "ShowPopup" + control.ID, "closeModalDiv('" + control.ClientID + "');", true);
		}

	}
}
