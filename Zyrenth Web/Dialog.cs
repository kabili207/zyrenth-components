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
	ToolboxData("<{0}:Dialog Title=\"\" runat=\"server\"></{0}:Dialog>"),
	Designer(typeof(DialogDesigner)),
	ToolboxBitmap(typeof(Dialog), "Icons.Dialog.ico"),
	ParseChildren(true, "Content"),
	]
	public class Dialog : CompositeControl, IPostBackEventHandler
	{
		private TaglessPanel _content;
		private ITemplate _templateContent;

		#region Properties

		[
		Bindable(true),
		Category("Appearance"),
		DefaultValue(""),
		Description("The title of the dialog"),
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
		Bindable(true),
		Category("Behavior"),
		DefaultValue(true),
		Description("A value indicating if is a modal popup or not"),
		]
		public virtual bool IsModal
		{
			get
			{
				object t = ViewState["IsModal"];
				if (t == null)
					return true;
				return (bool)t;
			}
			set
			{
				ViewState["IsModal"] = value;
			}
		}

		[
		PersistenceMode(PersistenceMode.InnerProperty),
		NotifyParentProperty(true),
		]
		public DialogButtonCollection Buttons
		{
			get
			{
				object o = ViewState["Buttons"];
				if (o == null)
				{
					o = new DialogButtonCollection();
					ViewState["Buttons"] = o;
				}
				return (DialogButtonCollection)o;
			}
		}

		[
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		// Set this to InnerDefaultProperty if we don't want the requirement of
		// using the <Content></Content> tags in the aspx file
		PersistenceMode(PersistenceMode.InnerProperty),
		// This is EXTREMELY important! This tells VS to generate objects in the
		// *.Designer.cs code-behind file!
		TemplateInstance(TemplateInstance.Single)
		]
		public ITemplate Content
		{
			get
			{
				return _templateContent;
			}
			set
			{
				_templateContent = value;
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

		#region ButtonClicked event

		public delegate void DialogButtonEventHandler(object sender, DialogButtonEventArgs e);
		private static readonly string EventButtonClicked = "DialogButtonClick";

		public event DialogButtonEventHandler ButtonClicked
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

		protected virtual void OnButtonClicked(DialogButtonEventArgs e)
		{
			DialogButtonEventHandler ev = Events[EventButtonClicked] as DialogButtonEventHandler;
			if (ev != null)
				ev(this, e);
		}

		#endregion

		#endregion // Events

		public Dialog()
		{

		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			this.EnsureChildControls();
			ScriptHelper.RegisterJQuery(this.Page.ClientScript);

		}


		protected override void CreateChildControls()
		{
			Controls.Clear();

			_content = new TaglessPanel();

			ITemplate temp = _templateContent;
			if (temp == null)
			{
				temp = new DefaultTemplate();
			}

			temp.InstantiateIn(_content);

			this.Controls.Add(_content);
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
				Dialog container = (Dialog)(source.NamingContainer);
				source.Text = container.Title;
			}
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

		protected override void Render(HtmlTextWriter writer)
		{
			EnsureChildControls();
			string designerExtra = "";
			if (this.DesignMode)
				designerExtra = "class='ui-dialog-content ui-widget-content'";
			writer.Write("<div id=\"{0}\" style=\"\" title=\"{1}\" {2} >", ClientID, Title, designerExtra);
			if (this.DesignMode)
			{
				string sCloseIcon = "";
				if (ShowCloseButton)
				{
					sCloseIcon = "<a href='#' class='ui-dialog-titlebar-close' style='position: absolute; right: 0px'>" +
						"<span class=\"ui-icon ui-icon-closethick\">close</span></a>";
				}
				writer.Write("<div class=\"ui-dialog-titlebar ui-widget-header\">" +
				"<span class=\"ui-dialog-title\">{0}</span>{1}</div>", Title, sCloseIcon);
			}

			RenderContents(writer);

			if (this.DesignMode)
			{
				writer.Write("<hr />");
				foreach (DialogButton button in Buttons)
				{
					string sButtonIcon = button.Icon == JQueryIcon.None ? "" :
						string.Format("ui-icon-{0}", button.Icon.ToString().Replace('_', '-'));
					if (!string.IsNullOrWhiteSpace(sButtonIcon))
					{
						sButtonIcon = string.Format("<span class=\"ui-button-icon-primary ui-icon {0}\" style='position: absolute; top: 15px'></span>", sButtonIcon);
					}
					string sClass = button.Icon == JQueryIcon.None ? "ui-button-text-only" :
						button.IconOnly ? "ui-button-icon-only" : "ui-button-text-icon-primary";
					writer.Write("<a href='#' class=\"ui-button ui-widget ui-state-default {0}\" style='position: relative;'>" +
					"{1}<span class=\"ui-button-text\">{2}</span></a>", sClass, sButtonIcon, button.Text);
				}
			}
			writer.Write("</div>");

			if (!this.DesignMode)
			{

				var buttons = Buttons.OfType<DialogButton>().Select(x =>
					{
						string buttonPostBack = Page.ClientScript.GetPostBackEventReference(this, "Button+" + x.CommandName);

						StringBuilder sbOpenJs = new StringBuilder();
						if (x.Icon != JQueryIcon.None)
						{
							//TODO: Extend this to allow secondary buttons.
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

				string buttonsJs = "null";
				if (buttons.Count() > 0)
				{
					buttonsJs = "[" + string.Join(", ", buttons) + "]";
				}

				//string openJs = string.Format("openModalDiv('{0}', function() {{ {1}; }}, {2} );",
				string closePostBack = Page.ClientScript.GetPostBackEventReference(this, "close");
				dialogJs = string.Format(dialogJs, ClientID, closePostBack,
					buttonsJs, sbCloseJs.ToString(), Resizable ? "true" : "false",
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
				OnButtonClicked(new DialogButtonEventArgs(btnId));
			}
		}

		public void Show()
		{
			this.Visible = true;
		}

		public void Hide()
		{
			this.Visible = false;
		}

		#region Nested Classes

		public class DialogButtonEventArgs : EventArgs
		{
			public string CommandName { get; set; }
			public DialogButtonEventArgs(string commandName)
			{
				this.CommandName = commandName;
			}
		}


		public class DialogDesigner : CompositeControlDesigner
		{
			private const string CONTENT = "CONTENT";
			private const string TITLE = "TITLE";
			private Dialog myControl;

			public override bool AllowResize
			{
				get { return false; }
			}

			public override void Initialize(IComponent Component)
			{
				base.Initialize(Component);
				myControl = (Dialog)Component;
				SetViewFlags(ViewFlags.TemplateEditing, true);
			}

			protected override void CreateChildControls()
			{
				base.CreateChildControls();
				// Add design time markers for each of the three regions 
				myControl._content.Attributes.Add(DesignerRegion.DesignerRegionAttributeName, CONTENT);

			}

			public override String GetDesignTimeHtml(DesignerRegionCollection regions)
			{
				Dialog control = (Dialog)Component;
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

						ITemplate template = myControl.Content;
						if (template != null)
							return ControlPersister.PersistTemplate(template, host);
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
						ITemplate template = ControlParser.ParseTemplate(host, content);

						myControl.Content = template;
					}
				}
			}
		}

		#endregion
	}

}
