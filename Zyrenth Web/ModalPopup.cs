using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.ComponentModel;
using System.Web.UI.Design;

namespace Zyrenth.Web
{

	[
	DefaultProperty("Title"),
	ToolboxData("<{0}:ModalPopup runat=\"server\"> </{0}:ModalPopup>")/*,
	Designer(typeof(ModalPopupDesigner))*/
	]
	public class ModalPopup : CompositeControl, IPostBackEventHandler
	{
		private ITemplate templateValue;
		private TemplateOwner ownerValue;

		public ModalPopup()
		{
		
		}

		[
		Bindable(true),
		Category("Appearance"),
		DefaultValue(""),
		Description("The title of the modal dialog"),
		]
		public virtual string Title
		{
			get
			{
				object t = ViewState["Title"];
				return t as string;
			}
			set
			{
				ViewState["Title"] = value;
			}
		}



		[
		Browsable(false),
		DesignerSerializationVisibility(
			DesignerSerializationVisibility.Hidden)
		]
		public TemplateOwner Owner
		{
			get
			{
				return ownerValue;
			}
		}

		[
		Browsable(false),
		PersistenceMode(PersistenceMode.InnerProperty),
		DefaultValue(typeof(ITemplate), ""),
		Description("Control template"),
		TemplateContainer(typeof(ModalPopup))
		]
		public virtual ITemplate Template
		{
			get
			{
				return templateValue;
			}
			set
			{
				templateValue = value;
			}
		}


		[
		ToolboxItem(false)
		]
		public class TemplateOwner : WebControl
		{
			public override void RenderBeginTag(HtmlTextWriter writer)
			{
			}

			public override void RenderEndTag(HtmlTextWriter writer)
			{
			}
		}

		#region DefaultTemplate
		sealed class DefaultTemplate : ITemplate
		{
			void ITemplate.InstantiateIn(Control owner)
			{
				Label title = new Label();
				title.DataBinding += new EventHandler(title_DataBinding);

				LiteralControl linebreak = new LiteralControl("<br/>");

				owner.Controls.Add(title);
				owner.Controls.Add(linebreak);

			}

			void title_DataBinding(object sender, EventArgs e)
			{
				Label source = (Label)sender;
				ModalPopup container = (ModalPopup)(source.NamingContainer);
				source.Text = container.Title;
			}
		}
		#endregion


		protected override void CreateChildControls()
		{
			Controls.Clear();
			ownerValue = new TemplateOwner();

			ITemplate temp = templateValue;
			if (temp == null)
			{
				temp = new DefaultTemplate();
			}

			temp.InstantiateIn(ownerValue);
			this.Controls.Add(ownerValue);
		}
		public override void DataBind()
		{
			CreateChildControls();
			ChildControlsCreated = true;
			base.DataBind();
		}
		protected override void Render(HtmlTextWriter writer)
		{

			writer.Write("<div id=\"{0}\" style=\"\" title=\"{1}\">", ClientID, Title);
			RenderContents(writer);
			writer.Write("</div>");
			writer.Write("<a href=\"{0}\">Close</a>", Page.ClientScript.GetPostBackClientHyperlink(this, "close"));
			string openJs = string.Format("openModalDiv('{0}', function() {{ {1}; }} );",
				ClientID, Page.ClientScript.GetPostBackEventReference(this, "close"));

			Page.ClientScript.RegisterStartupScript(typeof(ModalPopup), ClientID + "Open", openJs, true); 
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			string popupJs = @"
				function openModalDiv(divname, closeFnc) {
					var width = $('#' + divname).width();
					var height = $('#' + divname).height();
					$('#' + divname).dialog({ autoOpen: false, bgiframe: true, modal: true,
						resizable: false, width: 'auto', close: function(ev, ui) { closeFnc(); },
						autoResize: true /*height: height, width: width*/ });
					$('#' + divname).dialog('open');
					$('#' + divname).parent().appendTo($('form:first'));
				}

				function closeModalDiv(divname) {
					$('#' + divname).dialog('close');
				}
			";

			Page.ClientScript.RegisterClientScriptBlock(typeof(ModalPopup), "ModalFunctionPopupJs", popupJs, true);


		}

		public void RaisePostBackEvent(string eventArgument)
		{
			switch (eventArgument)
			{
				case "close":
					this.Visible = false;
					break;
			}
		}
	}

	/*public class ModalPopupDesigner : ControlDesigner
	{

		public override void Initialize(IComponent Component)
		{
			base.Initialize(Component);
			SetViewFlags(ViewFlags.TemplateEditing, true);
		}

		public override string GetDesignTimeHtml()
		{
			return "<div>This is design-time HTML</div>";
		}

		public override TemplateGroupCollection TemplateGroups
		{
			get
			{
				TemplateGroupCollection collection = new TemplateGroupCollection();
				TemplateGroup group;
				TemplateDefinition template;
				ModalPopup control;

				control = (ModalPopup)Component;
				group = new TemplateGroup("Item");
				template = new TemplateDefinition(this, "Template", control, "Template", true);
				group.AddTemplateDefinition(template);
				collection.Add(group);
				return collection;
			}
		}
	}*/
}
