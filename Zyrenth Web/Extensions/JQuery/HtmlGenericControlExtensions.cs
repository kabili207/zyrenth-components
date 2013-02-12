using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.HtmlControls;

namespace Zyrenth.Web.Extensions.JQuery
{
	public static class HtmlGenericControlExtensions
	{
		/// <summary>
		/// Opens the specified control as a modal popup.
		/// To show jQuery UI modal popup buttons, add isModal=&quot;true&quot;
		/// to the tag.
		/// </summary>
		/// <param name="control">The control to open as a modal popup</param>
		/// <remarks>This only works with DIV tags</remarks>
		public static void OpenPopup(this HtmlGenericControl control)
		{
			if (control.TagName.ToLower() != "div")
			{
				throw new ArgumentException("Control TagName must be of type DIV");
			}
			control.OpenPopupInternal();
		}

		/// <summary>
		/// Closed the currently open modal popup
		/// </summary>
		/// <param name="control">The control to close</param>
		/// <remarks>This only works with DIV tags</remarks>
		public static void ClosePopup(this HtmlGenericControl control)
		{
			if (control.TagName.ToLower() != "div")
			{
				throw new ArgumentException("Control TagName must be of type DIV");
			}
			control.ClosePopupInternal();
		}

	}
}
