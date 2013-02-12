using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace Zyrenth.Web.Extensions.JQuery
{
	public static class PanelExtensions
	{
		/// <summary>
		/// Opens the specified panel as a modal popup.
		/// To show jQuery UI modal popup buttons, add isModal=&quot;true&quot;
		/// to the tag.
		/// </summary>
		/// <param name="panel">The Panel to open as a modal popup</param>
		public static void OpenPopup(this Panel panel)
		{
			panel.OpenPopupInternal();
		}

		/// <summary>
		/// Closed the currently open modal popup
		/// </summary>
		/// <param name="panel">The Panel to close</param>
		public static void ClosePopup(this Panel panel)
		{
			panel.ClosePopupInternal();
		}
	}
}
