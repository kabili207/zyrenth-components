using System.Web.UI;

namespace Zyrenth.Web.Extensions
{
	public static class PageExtensions
	{

		/// <summary>
		/// Returns true if the page is loaded as a result of an asynchronous post-back.
		/// </summary>
		/// <param name="Page"></param>
		/// <returns></returns>
		public static bool IsAsyncPostBack(this Page Page)
		{
			if (!Page.IsPostBack) return false;
			ScriptManager oManager = ScriptManager.GetCurrent(Page);
			if (oManager == null) return false;
			if (oManager.IsInAsyncPostBack) return true;
			return false;
		}

	}
}