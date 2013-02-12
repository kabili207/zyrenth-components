using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace Zyrenth.Web.Extensions
{
	public static class UpdatePanelExtensions
	{
		/// <summary>
		/// Returns true if the UpdatePanel is related to the control that triggered the asynchronous post-back.
		/// </summary>
		/// <param name="Panel"></param>
		/// <returns></returns>
		public static bool IsUpdating(this UpdatePanel Panel)
		{
			string sSourceUniqueId = ScriptManager.GetCurrent(Panel.Page).AsyncPostBackSourceElementID;
			foreach (UpdatePanelTrigger oTrigger in Panel.Triggers)
			{
				PostBackTrigger oPt = oTrigger as PostBackTrigger;
				if (oPt != null && Panel.NamingContainer.FindControl(oPt.ControlID).UniqueID == sSourceUniqueId)
					return true;
				AsyncPostBackTrigger oAt = oTrigger as AsyncPostBackTrigger;
				if (oAt != null &&
					Panel.NamingContainer.FindControl(oAt.ControlID).UniqueID == sSourceUniqueId)
					return true;
			}
			if (Panel.FindDescendantByUniqueId(sSourceUniqueId) != null)
				return true;
			return false;
		}
	}
}
