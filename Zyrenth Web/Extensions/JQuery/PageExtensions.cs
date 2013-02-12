using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace Zyrenth.Web.Extensions.JQuery
{
	public static class PageExtensions
	{
		/// <summary>
		/// Registers the scripts required to use jQuery modal popups
		/// </summary>
		/// <param name="page">The page to register the scripts with</param>
		public static void RegisterModalScripts(this Page page)
		{
			string sScript = @"function openModalDiv(divname, buttons, openEvent) {
					var width = $('#' + divname).width();
					var height = $('#' + divname).height();
					$('#' + divname).dialog({ autoOpen: false, bgiframe: true, modal: true, resizable: false, width: 'auto', autoResize: true, buttons: buttons, open: openEvent });
					$('#' + divname).dialog('open');
					$('#' + divname).parent().appendTo($('form:first'));
				}

				function closeModalDiv(divname) {
					$('#' + divname).dialog('close');
				}";

			ScriptManager.RegisterClientScriptBlock(page, typeof(PageExtensions), "ModalPopupCode", sScript, true);
		}
	}
}
