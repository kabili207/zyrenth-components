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
using System.Web.UI.Design.WebControls;

namespace Zyrenth.Web
{

	[
	DefaultProperty("Content"),
	ToolboxData("<{0}:ModalPopup Title=\"\" runat=\"server\"></{0}:ModalPopup>"),
		Designer(typeof(ModalPopupDesigner)),
	ToolboxBitmap(typeof(Zyrenth.Web.Icons.IconHelper), "ModalPopup.bmp"),
		ParseChildren(true),
	//ParseChildren(ChildrenAsProperties = false),
	//ControlBuilder(typeof(ModalPopupControlBuilder)),
	]
	public class ModalPopup : CompositeControl, IPostBackEventHandler
	{
		private TaglessPanel _content;
		private ControlCollection _controls;

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

		[
		Bindable(true),
		Category("Appearance"),
		DefaultValue(true),
		Description("Determines whether or not to show the close button"),
		]
		public virtual bool ShowCloseButton
		{
			get
			{
				object t = ViewState["ShowCloseButton"];
				if (t == null)
					return true;
				return (bool)t;
			}
			set
			{
				ViewState["ShowCloseButton"] = value;
			}
		}

		[
		Bindable(true),
		Category("Behavior"),
		DefaultValue(false),
		Description("A value allowing the dialog to be resized by the user"),
		]
		public virtual bool Resizable
		{
			get
			{
				object t = ViewState["Resizable"];
				if (t == null)
					return false;
				return (bool)t;
			}
			set
			{
				ViewState["Resizable"] = value;
			}
		}

		[
		Bindable(true),
		Category("Behavior"),
		DefaultValue(true),
		Description("A value indicating if the popup can be moved around the screen by the user"),
		]
		public virtual bool Draggable
		{
			get
			{
				object t = ViewState["Draggable"];
				if (t == null)
					return true;
				return (bool)t;
			}
			set
			{
				ViewState["Draggable"] = value;
			}
		}

		[
		PersistenceMode(PersistenceMode.InnerProperty),
		NotifyParentProperty(true),
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
		}

		[
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		// Set this to InnerDefaultProperty if we don't want the requirement of
		// using the <Content></Content> tags in the aspx file
		PersistenceMode(PersistenceMode.InnerProperty)
		]
		public ControlCollection Content
		{
			get
			{
				if (_controls == null)
				{
					_controls = new ControlCollection(this);
				}
				return _controls;
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

		public delegate void ModalButtonEventHandler(object sender, ModalButtonEventArgs e);
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

			ScriptHelper.RegisterJQuery(this.Page.ClientScript);

		}


		protected override void CreateChildControls()
		{
			Controls.Clear();

			_content = new TaglessPanel();

			this.Controls.Add(_content);
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

		protected override void RenderContents(HtmlTextWriter writer)
		{
			if (_content != null)
			{
				foreach (Control c in Content)
					_content.Controls.Add(c);
				_content.RenderControl(writer);
			}
		}

		protected override void Render(HtmlTextWriter writer)
		{
			EnsureChildControls();
			writer.Write("<div id=\"{0}\" style=\"\" title=\"{1}\">", ClientID, Title);
			if (this.DesignMode)
			{
				writer.Write("<div>{0}</div>", Title);
			}

			RenderContents(writer);

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

				var buttons = Buttons.OfType<ModalPopupButton>().Select(x =>
					{
						string buttonPostBack = Page.ClientScript.GetPostBackEventReference(this, "Button+" + x.CommandName);

						StringBuilder sbOpenJs = new StringBuilder();
						if (x.Icon != JQueryIcon.None)
						{
							//TODO: Extend this to allow secondary buttons or button only icons.
							sbOpenJs.AppendFormat("$(this).button({{ icons: {{ primary: 'ui-icon-{0}' }}, text: {1} }});",
								x.Icon.ToString().Replace('_', '-'), x.IconOnly ? "false" : "true");
						}

						if (!string.IsNullOrWhiteSpace(x.CssClass))
							sbOpenJs.AppendFormat("$(this).addClass('{0}');", x.CssClass);

						string buttonJs = @"{{ text: ""{0}"", click: function() {{ {1} }}, create: function(ev, ui) {{ {2} }} }}";

						return string.Format(buttonJs, x.Text, buttonPostBack, sbOpenJs.ToString());
					});

				StringBuilder sbCloseJs = new StringBuilder();

				if (!ShowCloseButton)
				{
					sbCloseJs.Append("$('.ui-dialog-titlebar-close', this.parentNode).hide();");
				}

				// TODO: Allow other options?
				string dialogJs = "$('#{0}').dialog({{ autoOpen: true, bgiframe: true, modal: true, " +
						"resizable: {4}, width: 'auto', autoResize: true, buttons: {2}, draggable: {5}, " +
						"close: function(ev, ui) {{ {1}; }}, open: function(ev, ui) {{ {3}; }}}})" +
					//$('#' + divname).dialog('open');
						".parent().appendTo($('form:first'));";

				//string openJs = string.Format("openModalDiv('{0}', function() {{ {1}; }}, {2} );",
				string closePostBack = Page.ClientScript.GetPostBackEventReference(this, "close");
				dialogJs = string.Format(dialogJs, ClientID, closePostBack,
					"[" + string.Join(", ", buttons) + "]", sbCloseJs.ToString(), Resizable ? "true" : "false",
					Draggable ? "true" : "false");

				writer.Write("<script type='text/javascript'>" + dialogJs + "</script>");
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

		
		public class ModalPopupDesigner : CompositeControlDesigner
		{
			private const string CONTENT = "CONTENT";
			private ModalPopup myControl;

			public override bool AllowResize
			{
				get { return false; }
			}

			public override void Initialize(IComponent Component)
			{
				base.Initialize(Component);
				myControl = (ModalPopup)Component;
				SetViewFlags(ViewFlags.TemplateEditing, true);
			}

			protected override void CreateChildControls()
			{
				base.CreateChildControls();
				// Add design time markers for each of the three regions 
				myControl._content.Attributes.Add(DesignerRegion.DesignerRegionAttributeName, CONTENT);
				
			}

			// Handler for the Click event, which provides the region in the arguments. 
			/*protected override void OnClick(DesignerRegionMouseEventArgs e)
			{
				if (e.Region == null)
					return;
				
				// Switch the current view if required 
				if (e.Region.Name == CONTENT)
				{
					base.UpdateDesignTimeHtml();
				}
			}*/

			public override String GetDesignTimeHtml(DesignerRegionCollection regions)
			{
				ModalPopup control = (ModalPopup)Component;
				// Create an editable region and add it to the regions
				EditableDesignerRegion editableRegion =
					new EditableDesignerRegion(this, CONTENT, false);
				regions.Add(editableRegion);

				// Set the highlight for the selected region
				regions[0].Highlight = true;

				// Use the base class to render the markup 
				return base.GetDesignTimeHtml();
			}

			// Get the content string for the selected region. Called by the designer host? 
			public override string GetEditableDesignerRegionContent(EditableDesignerRegion region)
			{
				// Get a reference to the designer host
				IDesignerHost host = (IDesignerHost)Component.Site.GetService(typeof(IDesignerHost));
				if (host != null)
				{
					if (region.Name == CONTENT)
					{
						TaglessPanel owner = myControl._content;
						if (owner != null)
							return ControlPersister.PersistControl(owner, host);
					}
				}

				return String.Empty;
			}

			// Create a template from the content string and put it in the selected view. 
			public override void SetEditableDesignerRegionContent(EditableDesignerRegion region, string content)
			{
				if (content == null)
					return;

				// Get a reference to the designer host
				IDesignerHost host = (IDesignerHost)Component.Site.GetService(typeof(IDesignerHost));
				if (host != null)
				{
					if (region.Name == CONTENT)
					{
						Control c = ControlParser.ParseControl(host, content);

						if (c is TaglessPanel)
						{
							myControl.Content.Clear();
							foreach (Control cc in c.Controls.OfType<Control>().ToList())
								myControl.Content.Add(cc);
							base.UpdateDesignTimeHtml();
						}
					}
				}
			}
		}

		#endregion
	}

}
