using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.ComponentModel;
using System.Web.UI.WebControls;
using System.Drawing;

namespace Zyrenth.Web
{
	[
	ToolboxBitmap(typeof(TaglessPanel), "Icons.TaglessPanel.ico"),
	ParseChildren(false),
	ToolboxData("<{0}:TagglessPanel runat=\"server\"></{0}:TagglessPanel>")
	]
	public class TaglessPanel : Panel
	{

		[
		Bindable(true),
		Category("Behavior"),
		DefaultValue(false),
		Description("Determines whether or not to show text when the button contains an icon"),
		]
		public virtual bool RenderOuterDiv
		{
			get
			{
				object t = ViewState["RenderOuterDiv"];
				if (t == null)
					return false;
				return (bool)t;
			}
			set
			{
				ViewState["RenderOuterDiv"] = value;
			}
		}

		public override void RenderBeginTag(HtmlTextWriter writer)
		{
			if (this.DesignMode || RenderOuterDiv)
				base.RenderBeginTag(writer);
		}

		public override void RenderEndTag(HtmlTextWriter writer)
		{
			if (this.DesignMode || RenderOuterDiv)
				base.RenderEndTag(writer);
		}
	}
}
