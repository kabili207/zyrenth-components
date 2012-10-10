using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.ComponentModel;
using System.Web.UI.Design;
using System.IO;
using System.ComponentModel.Design;
using System.Drawing;

namespace Zyrenth.Web
{


	
	[
	DefaultProperty("Title"),
	ToolboxData("<{0}:ModalPopup runat=\"server\"> </{0}:ModalPopup>"),
	Designer(typeof(ModalPopupDesigner)),
	ToolboxBitmap(typeof(ColorSwatch), "Zyrenth.Web.Icons.ModalPopup.bmp"),
	]
	public class ModalPopup : CompositeControl, IPostBackEventHandler
	{
		private ITemplate templateValue;
		private TemplateOwner ownerValue;

		#region Properties

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
		//PopupButtonCollection _buttons = new PopupButtonCollection();

		[
		PersistenceMode(PersistenceMode.InnerProperty),
		]
		public PopupButtonCollection Buttons
		{
			get
			{
				object o = ViewState["Buttons"];
				if (o == null)
				{
					o = new PopupButtonCollection();
					ViewState["Buttons"] = o;
				}
				return (PopupButtonCollection)o;
			}
			//set
			//{
			//    ViewState["Buttons"] = value;
			//}
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



		public override ControlCollection Controls
		{
			get
			{
				this.EnsureChildControls();
				return base.Controls;
			}
		}

		#endregion // Properties

		#region Events

		#region "ButtonClicked event"

		private static readonly string EventButtonClicked = "ModalPopupButtonClick";

		public event ModalButtonEventHandler ButtonClicked
		{
			add
			{
				Events.AddHandler(EventButtonClicked, value);
			}
			remove
			{
				Events.RemoveHandler(EventButtonClicked, value);
			}
		}

		protected virtual void OnButtonClicked(ModalButtonEventArgs e)
		{
			ModalButtonEventHandler ev = Events[EventButtonClicked] as ModalButtonEventHandler;
			if (ev != null)
				ev(this, e);
		}

		#endregion

		#endregion // Events

		public ModalPopup()
		{

		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			string popupJs = @"
				function openModalDiv(divname, closeFnc, btnArray) {
					var width = $('#' + divname).width();
					var height = $('#' + divname).height();
					$('#' + divname).dialog({ autoOpen: false, bgiframe: true, modal: true,
						resizable: false, width: 'auto', close: function(ev, ui) { closeFnc(); },
						autoResize: true /*height: height, width: width*/, buttons: btnArray });
					$('#' + divname).dialog('open');
					$('#' + divname).parent().appendTo($('form:first'));
				}

				function closeModalDiv(divname) {
					$('#' + divname).dialog('close');
				}
			";

			Page.ClientScript.RegisterClientScriptBlock(typeof(ModalPopup), "ModalFunctionPopupJs", popupJs, true);


		}


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
			ownerValue.Attributes.Add(
			DesignerRegion.DesignerRegionAttributeName, "Content");

			this.Controls.Add(ownerValue);
		}
		public override void DataBind()
		{
			CreateChildControls();
			ChildControlsCreated = true;
			base.DataBind();
		}

		internal void RenderContentWindow(HtmlTextWriter writer)
		{
			RenderContents(writer);
		}

		internal void RenderButtons(HtmlTextWriter writer)
		{

		}

		protected override void Render(HtmlTextWriter writer)
		{
			writer.Write("<div id=\"{0}\" style=\"\" title=\"{1}\">", ClientID, Title);
			if (this.DesignMode)
			{
				writer.Write("<div>{0}</div>", Title);
			}
			RenderChildren(writer);
			if (this.DesignMode)
			{
				writer.Write("<hr />");
				foreach (ModalPopupButton button in Buttons)
				{
					writer.Write("<button type='button'>{0}</button>", button.Text);
				}
			}
			writer.Write("</div>");

			if (!this.DesignMode)
			{
				// Perhaps we can use this to add icons to the buttons?
				//var buttons = $('.ui-dialog-buttonpane').children('button');
				//buttons.removeClass('ui-button-text-only').addClass('ui-button-text-icon');
       
				//$(buttons[0]).append("<span class='ui-icon ui-icon-check'></span>");
				//$(buttons[1]).append("<span class='ui-icon ui-icon-close'></span>");

				var buttons = Buttons.Select(x =>
					{
						string postBack = Page.ClientScript.GetPostBackEventReference(this, "Button+" + x.CommandName);
						
						StringBuilder sbOpenJs = new StringBuilder();

						if(!string.IsNullOrWhiteSpace(x.CssClass))
							sbOpenJs.AppendFormat(" $(this).addClass('{0}'); ", x.CssClass);

						string buttonJs = @"{{ text: ""{0}"",  click: function() {{ {1} }}, open: function() {{ {2} }} }}";


						return string.Format(buttonJs, x.Text, postBack, sbOpenJs.ToString());
					});

				string openJs = string.Format("openModalDiv('{0}', function() {{ {1}; }}, {2} );",
					ClientID, Page.ClientScript.GetPostBackEventReference(this, "close"), "[" + string.Join(", ", buttons) + "]");

				Page.ClientScript.RegisterStartupScript(typeof(ModalPopup), ClientID + "Open", openJs, true);
			}
		}

		public void RaisePostBackEvent(string eventArgument)
		{
			if (eventArgument == "close")
			{
				this.Visible = false;
			}
			else if (eventArgument.StartsWith("Button+"))
			{
				string btnId = eventArgument.Substring(7);
				OnButtonClicked(new ModalButtonEventArgs(btnId));
			}
		}

		#region Nested Classes

		public class ModalButtonEventArgs : EventArgs
		{
			public string CommandName { get; set; }
			public ModalButtonEventArgs(string commandName)
			{
				this.CommandName = commandName;
			}
		}
		public delegate void ModalButtonEventHandler(object sender, ModalButtonEventArgs e);

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

		public class ModalPopupDesigner : ControlDesigner
		{
			private ModalPopup myControl;

			public override void Initialize(IComponent Component)
			{
				base.Initialize(Component);
				myControl = (ModalPopup)Component;
				SetViewFlags(ViewFlags.TemplateEditing, true);
			}

			// Handler for the Click event, which provides the region in the arguments. 
			protected override void OnClick(DesignerRegionMouseEventArgs e)
			{
				if (e.Region == null)
					return;

				// If the clicked region is not a header, return 
				if (e.Region.Name.IndexOf("Header") != 0)
					return;

				// Switch the current view if required 
				if (e.Region.Name == "Content")
				{
					base.UpdateDesignTimeHtml();
				}
			}

			public override String GetDesignTimeHtml(DesignerRegionCollection regions)
			{
				ModalPopup control = (ModalPopup)Component;
				control.DataBind();
				// Create an editable region and add it to the regions
				EditableDesignerRegion editableRegion =
					new EditableDesignerRegion(this, "Content", false);
				regions.Add(editableRegion);

				// Set the highlight for the selected region
				regions[0].Highlight = true;

				// Use the base class to render the markup 
				return base.GetDesignTimeHtml();
			}

			// Get the content string for the selected region. Called by the designer host? 
			// TODO: This never seems to get called..
			public override string GetEditableDesignerRegionContent(EditableDesignerRegion region)
			{
				// Get a reference to the designer host
				IDesignerHost host = (IDesignerHost)Component.Site.GetService(typeof(IDesignerHost));
				if (host != null)
				{
					ITemplate template = myControl.Template;

					// Persist the template in the design host 
					if (template != null)
						return ControlPersister.PersistTemplate(template, host);
				}

				return String.Empty;
			}

			// Create a template from the content string and put it in the selected view. 
			// TODO: This never seems to get called..
			public override void SetEditableDesignerRegionContent(EditableDesignerRegion region, string content)
			{
				if (content == null)
					return;

				// Get a reference to the designer host
				IDesignerHost host = (IDesignerHost)Component.Site.GetService(typeof(IDesignerHost));
				if (host != null)
				{
					// Create a template from the content string
					ITemplate template = ControlParser.ParseTemplate(host, content);

					myControl.Template = template;
				}
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
					group = new TemplateGroup("Template");
					template = new TemplateDefinition(this, "Template", control, "Template", true);
					group.AddTemplateDefinition(template);
					collection.Add(group);
					return collection;
				}
			}
		}

		#endregion
	}

}
