using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.ComponentModel;

namespace Zyrenth.Web
{
	public class DialogButton : IStateManager
	{
		private bool _isTrackingViewState;
		private StateBag _viewState;

		public DialogButton()
		{
			// TODO: Have the ModalPopup class manage this
			((IStateManager)this).TrackViewState();
		}

		#region Properties

		[
		Category("Appearance"),
		DefaultValue(""),
		Description("The text to display on the button"),
		NotifyParentProperty(true)
		]
		public virtual String Text
		{
			get
			{
				string s = ViewState["Text"] as string;
				return s ?? "";
			}
			set
			{
				ViewState["Text"] = value;
			}
		}

		[
		Category("Data"),
		DefaultValue(""),
		Description("The text to display on the button"),
		NotifyParentProperty(true)
		]
		public virtual String CommandName
		{
			get
			{
				string s = ViewState["CommandName"] as string;
				return s ?? "";
			}
			set
			{
				ViewState["CommandName"] = value;
			}
		}

		[
		Category("Appearance"),
		DefaultValue(""),
		Description("Extra CSS classes to add"),
		NotifyParentProperty(true)
		]
		public virtual String CssClass
		{
			get
			{
				string s = ViewState["CssClass"] as string;
				return s ?? "";
			}
			set
			{
				ViewState["CssClass"] = value;
			}
		}

		[
		Category("Appearance"),
		DefaultValue(JQueryIcon.None),
		Description("Sets an optional icon to display on the button"),
		NotifyParentProperty(true)
		]
		public virtual JQueryIcon Icon
		{
			get
			{
				object s = ViewState["Icon"];
				if (s == null || !(s is JQueryIcon))
					return JQueryIcon.None;
				return (JQueryIcon)s;
			}
			set
			{
				ViewState["Icon"] = value;
			}
		}

		[
		Bindable(true),
		Category("Appearance"),
		DefaultValue(false),
		Description("Determines whether or not to show text when the button contains an icon"),
		]
		public virtual bool IconOnly
		{
			get
			{
				object t = ViewState["ShowCloseButton"];
				if (t == null)
					return false;
				return (bool)t;
			}
			set
			{
				ViewState["ShowCloseButton"] = value;
			}
		}

		protected virtual StateBag ViewState
		{
			get
			{
				if (_viewState == null)
				{
					_viewState = new StateBag(false);

					if (_isTrackingViewState)
					{
						((IStateManager)_viewState).TrackViewState();
					}
				}
				return _viewState;
			}
		}

		#endregion // Properties

		#region IStateManager implementation

		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return _isTrackingViewState;
			}
		}

		void IStateManager.LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				((IStateManager)ViewState).LoadViewState(savedState);
			}
		}

		object IStateManager.SaveViewState()
		{
			object savedState = null;

			if (_viewState != null)
			{
				savedState =
                   ((IStateManager)_viewState).SaveViewState();
			}
			return savedState;
		}

		void IStateManager.TrackViewState()
		{
			_isTrackingViewState = true;

			if (_viewState != null)
			{
				((IStateManager)_viewState).TrackViewState();
			}
		}

        #endregion

		internal void SetDirty()
		{
			_viewState.SetDirty(true);
		}
	}

	[ParseChildren(false)]
	public class DialogButtonCollection : List<DialogButton>
	{
		//internal PopupButtonCollection(Control c) : base(c) { }
	}
}
