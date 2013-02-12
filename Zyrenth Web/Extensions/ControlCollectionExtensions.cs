using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace Zyrenth.Web.Extensions
{
	public static class ControlCollectionExtensions
	{
		/// <summary>
		/// Returns an IEnumerable(of Control) that iterates over all Controls in the control hierarchy
		/// </summary>
		/// <param name="Controls"></param>
		/// <returns></returns>
		public static IEnumerable<Control> Flatten(this ControlCollection Controls)
		{
			if (Controls == null)
				throw new ArgumentNullException("Controls");
			return Controls.Cast<Control>().Flatten();
		}


		/// <summary>
		/// Returns an IEnumerable(of Control) that iterates over all Controls in the control hierarchy
		/// </summary>
		/// <param name="Controls"></param>
		/// <returns></returns>
		public static IEnumerable<Control> Flatten(this IEnumerable<Control> Controls)
		{
			return Controls.Union(Controls.SelectMany(c => c.Controls.Flatten()));
		}


		/// <summary>
		/// Returns the first panel found that is related to an asynchronous post-back.
		/// </summary>
		/// <param name="Controls"></param>
		/// <returns></returns>
		/// <remarks>
		/// Assumes that there is only one update panel for the event.
		/// Triggers can be associated with multiple update panels so another method should be used
		/// if you need to find the multiple panels associated with a triggered asynchronous post-back.
		/// </remarks>
		public static UpdatePanel FindUpdatingPanel(this ControlCollection Controls)
		{
			return (from oControl in Flatten(Controls)
					let oP = oControl as UpdatePanel
					where oP != null && oP.IsUpdating()
					select oP).SingleOrDefault();
		}
	}
}
