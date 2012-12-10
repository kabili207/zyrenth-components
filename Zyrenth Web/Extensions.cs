using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace Zyrenth.Web
{
	public static class Extensions
	{

		#region UpdatePanel
		/// <summary>
		/// Returns true if the UpdatePanel is related to the control that triggered the asynchronous postback.
		/// </summary>
		/// <param name="panel"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public static bool IsUpdating(this UpdatePanel panel)
		{
			string sourceUniqueId = ScriptManager.GetCurrent(panel.Page).AsyncPostBackSourceElementID;
			foreach (UpdatePanelTrigger trigger in panel.Triggers)
			{
				PostBackTrigger pt = trigger as PostBackTrigger;
				if (pt != null && panel.NamingContainer.FindControl(pt.ControlID).UniqueID == sourceUniqueId)
					return true;
				AsyncPostBackTrigger at = trigger as AsyncPostBackTrigger;
				if (at != null &&
					panel.NamingContainer.FindControl(at.ControlID).UniqueID == sourceUniqueId)
					return true;
			}
			if (panel.FindDescendantByUniqueId(sourceUniqueId) != null)
				return true;
			return false;
		}
		#endregion


		#region Control
		/// <summary>
		/// Returns true if the child control is a descendant
		/// </summary>
		/// <param name="parent"></param>
		/// <param name="child"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public static bool IsDescendant(this Control parent, Control child)
		{
			Control ctrl = child;
			Control parnt = child.Parent;
			while (parnt != null)
			{
				if (parnt == parent)
					return true;
				parnt = parnt.Parent;
			}
			return false;
		}


		/// <summary>
		/// Finds a descendant of the the parent control by the child's unique id.
		/// </summary>
		/// <param name="control"></param>
		/// <param name="controlUniqueId"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public static Control FindDescendantByUniqueId(this Control control, String controlUniqueId)
		{
			return (from descendant in control.Controls.Flatten()
					where descendant.UniqueID == controlUniqueId
					select descendant).SingleOrDefault();
		}


		/// <summary>
		/// Returns true if the control is in the UpdatePanel currently updating.
		/// </summary>
		/// <param name="control"></param>
		/// <returns></returns>
		/// <remarks>Assumes that only one panel is updating per trigger</remarks>
		public static bool IsInUpdatingPanel(this Control control)
		{
			if (!control.Page.IsAsyncPostBack()) return false;
			UpdatePanel panel = control.Page.Controls.FindUpdatingPanel();
			if (panel == null)
				return false;
			return panel.IsDescendant(control);
		}


		#endregion


		#region "ControlCollection"
		/// <summary>
		/// Returns an IEnumerable(of Control) that iterates over all controls in the control hierarchy
		/// </summary>
		/// <param name="controls"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public static IEnumerable<Control> Flatten(this ControlCollection controls)
		{
			if (controls == null)
				throw new ArgumentNullException("controls");
			return controls.Cast<Control>().Flatten();
		}


		/// <summary>
		/// Returns an IEnumerable(of Control) that iterates over all controls in the control hierarchy
		/// </summary>
		/// <param name="controls"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public static IEnumerable<Control> Flatten(this IEnumerable<Control> controls)
		{
			return controls.Union(controls.SelectMany(c => c.Controls.Flatten()));
		}


		/// <summary>
		/// Returns the first panel found that is related to an asynchronous postback.
		/// </summary>
		/// <param name="controls"></param>
		/// <returns></returns>
		/// <remarks>Assumes that there is only one update panel for the event.
		/// Triggers can be associated with multiple update panels so another method should be used
		/// if you need to find the multiple panels associated with a triggered asynchronous postback.</remarks>
		public static UpdatePanel FindUpdatingPanel(this ControlCollection controls)
		{
			return (from control in Flatten(controls)
					let p = control as UpdatePanel
					where p != null && IsUpdating(p)
					select p).SingleOrDefault();
		}
		#endregion


		#region "Page"
		/// <summary>
		/// Returns true if the page is loaded as a result of an asynchronous postback.
		/// </summary>
		/// <param name="page"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public static bool IsAsyncPostBack(this Page page)
		{
			if (!page.IsPostBack) return false;
			ScriptManager manager = ScriptManager.GetCurrent(page);
			if (manager == null) return false;
			if (manager.IsInAsyncPostBack) return true;
			return false;
		}


		#endregion

	}
}