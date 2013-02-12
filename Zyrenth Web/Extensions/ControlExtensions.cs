using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace Zyrenth.Web.Extensions
{
	public static class ControlExtensions
	{
		/// <summary>
		/// Returns true if the child control is a descendant
		/// </summary>
		/// <param name="Parent">the parent control</param>
		/// <param name="Child">the child control</param>
		/// <returns>true if the child is a descendant, false otherwise</returns>
		public static bool IsDescendant(this Control Parent, Control Child)
		{
			Control oCtrl = Child;
			Control oParnt = Child.Parent;
			while (oParnt != null)
			{
				if (oParnt == Parent)
					return true;
				oParnt = oParnt.Parent;
			}
			return false;
		}

		/// <summary>
		/// Finds a descendant of the the parent control by the child's unique id.
		/// </summary>
		/// <param name="Control"></param>
		/// <param name="ControlUniqueId"></param>
		/// <returns></returns>
		public static Control FindDescendantByUniqueId(this Control Control, String ControlUniqueId)
		{
			return (from oDescendant in Control.Controls.Flatten()
					where oDescendant.UniqueID == ControlUniqueId
					select oDescendant).SingleOrDefault();
		}

		/// <summary>
		/// Returns true if the control is in the UpdatePanel currently updating.
		/// </summary>
		/// <param name="Control"></param>
		/// <returns></returns>
		/// <remarks>
		/// Assumes that only one panel is updating per trigger
		/// </remarks>
		public static bool IsInUpdatingPanel(this Control Control)
		{
			if (!Control.Page.IsAsyncPostBack()) return false;
			UpdatePanel oPanel = Control.Page.Controls.FindUpdatingPanel();
			if (oPanel == null)
				return false;
			return oPanel.IsDescendant(Control);
		}

	}
}
