using System;
using System.Text;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;

namespace Zyrenth.Web
{

	// TODO: Add support for message types (error, warning, etc.)
	public class WebMsgBox
	{
		protected static Dictionary<IHttpHandler, Queue<MsgBox>> handlerPages;

		static WebMsgBox()
		{
			handlerPages = new Dictionary<IHttpHandler, Queue<MsgBox>>();
		}

		private WebMsgBox()
		{
		}

		protected class MsgBox
		{
			public string Message { get; set; }

			public string Title { get; set; }

			public MsgBox()
			{
			}
		}

		// TODO: Add overload to accept an exception

		public static void Show(string message, string title)
		{
			if (!(handlerPages.ContainsKey(HttpContext.Current.Handler)))
			{
				Page currentPage = (Page)HttpContext.Current.Handler;
				if (!((currentPage == null)))
				{
					Queue<MsgBox> messageQueue = new Queue<MsgBox>();
					messageQueue.Enqueue(new MsgBox() { Message = message, Title = title });
					handlerPages.Add(HttpContext.Current.Handler, messageQueue);
					currentPage.PreRender += new EventHandler(CurrentPageUnload);
				}
			}
			else
			{
				Queue<MsgBox> queue = handlerPages[HttpContext.Current.Handler];
				queue.Enqueue(new MsgBox() { Message = message, Title = title });
			}
		}

		private static void CurrentPageUnload(object sender, EventArgs e)
		{
			Queue<MsgBox> queue = handlerPages[HttpContext.Current.Handler];
			if (queue != null)
			{
				Page p = (Page)HttpContext.Current.Handler;

				StringBuilder builder = new StringBuilder();
				StringBuilder divs = new StringBuilder();
				int iMsgCount = queue.Count;
				//builder.Append("<script language='javascript' src='" + p.ResolveUrl("~/Script/jquery-1.8.0.min.js") + "'></script>");
				//builder.Append("<script language='javascript' src='" + p.ResolveUrl("~/Script/jquery-ui-1.8.23.custom.min.js") + "'></script>");
				//builder.Append("<link href='" + p.ResolveUrl("~/Styles/jquery-ui-1.8.22.custom.css") +"' rel='stylesheet' type='text/css' />");

				builder.AppendLine("<script type='text/javascript'>");
				//builder.AppendLine("$(document).ready(function(){");
				MsgBox sMsg;
				while ((iMsgCount > 0))
				{
					iMsgCount = iMsgCount - 1;
					sMsg = queue.Dequeue();
					if (p.Request.Browser.IsMobileDevice)
					{
						builder.Append("alert( \"" + sMsg.Message + "\" );");
					}
					else
					{
						divs.AppendLine("<div id='webMsgBox" + iMsgCount + "' title='" + sMsg.Title + "' style='/*height=100px;*/'>" + sMsg.Message + "</div>");
						builder.AppendLine("var height = $('#webMsgBox" + iMsgCount + "').height();");
						builder.AppendLine(@"$('#webMsgBox" + iMsgCount + @"').dialog({
						bgiframe: true, modal: true, resizable: false,
						width: 400, autoResize: true , /*height: height,*/ draggable: true,
						buttons: {
							Ok: function() {
								$( this ).dialog( 'close' );
							}
						} });");
						//builder.AppendLine("$('webMsgBox" + iMsgCount + "').dialog('option', 'position', 'center');");
						//builder.AppendLine("$('#webMsgBox" + iMsgCount + "').dialog('open');");
						//builder.AppendLine("$('#webMsgBox" + iMsgCount + "').parent().appendTo($('form:first'));");
					}
				}
				//builder.AppendLine("});");
				builder.AppendLine("</script>");
				handlerPages.Remove(HttpContext.Current.Handler);
				p.Form.Controls.Add(new LiteralControl(divs.ToString() + builder.ToString()));
				//p.Header.Controls.Add(new LiteralControl("<link href='" + p.ResolveUrl("~/Styles/jquery-ui-1.8.22.custom.css") + "' rel='stylesheet' type='text/css' />"));
				//HttpContext.Current.Response.Write(builder.ToString());
			}
		}
	}
}