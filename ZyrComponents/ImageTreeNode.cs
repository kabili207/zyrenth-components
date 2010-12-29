using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace HRScreening.Components
{

	/// <summary>
	/// Represents a node of a <see cref="HRScreening.Components.ImageTreeView"/> that contains
	/// special information about a location.
	/// </summary>
	public partial class ImageTreeNode : TreeNode
	{

		private bool _active = true;

		/// <summary>
		/// Gets or sets the state of the location.
		/// </summary>
		public bool Active
		{
			get { return _active; }
			set { _active = value; }
		}

		private Bitmap _image = null;

		/// <summary>
		/// Gets or sets a value indicating if this is a primary location.
		/// </summary>
		public Bitmap Image
		{
			get { return _image; }
			set { _image = value; }
		}

		public String FallbackText { get; set; }

		public Color FallbackTextColor { get; set; }

		public void ResetFallbackTextColor()
		{
			FallbackTextColor = this.ForeColor;
		}

		public bool ShouldSerializeFallbackTextColor()
		{
			return FallbackTextColor != this.ForeColor;
		}

		#region LocationTreeNode constructors
		// These constructors aren't really needed anymore as they only
		// call base TreeNode constructors, however they have been left
		// here just in case.

		/// <summary>
		/// Initializes a new instance of the <see cref="HRScreening.Components.ImageTreeNode"/> class.
		/// </summary>
		public ImageTreeNode()
			: base()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="HRScreening.Components.ImageTreeNode"/>
		/// class with the specified label text.
		/// </summary>
		/// <param name="text">The label <see cref="HRScreening.Components.ImageTreeNode"/>.Text
		/// of the new tree node.</param>
		public ImageTreeNode(string text)
			: base(text)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="HRScreening.Components.ImageTreeNode"/>
		/// class with the specified label text and child tree nodes.
		/// </summary>
		/// <param name="text">The label <see cref="HRScreening.Components.ImageTreeNode"/>.Text
		/// of the new tree node.</param>
		/// <param name="children">An array of child <see cref="System.Windows.Forms.TreeNode"/> objects.</param>
		public ImageTreeNode(string text, TreeNode[] children)
			: base(text, children)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="HRScreening.Components.ImageTreeNode"/>
		/// class with the specified label text and images to display when the tree node is in a
		/// selected and unselected state.
		/// </summary>
		/// <param name="text">The label <see cref="HRScreening.Components.ImageTreeNode"/>.Text
		/// of the new tree node.</param>
		/// <param name="imageIndex">The index value of <see cref="System.Drawing.Image"/> to
		/// display when the tree node is unselected.</param>
		/// <param name="selectedImageIndex">The index value of <see cref="System.Drawing.Image"/>
		/// to display when the tree node is selected.</param>
		public ImageTreeNode(string text, int imageIndex, int selectedImageIndex)
			: base(text, imageIndex, selectedImageIndex)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="HRScreening.Components.ImageTreeNode"/>
		/// class with the specified label text, child tree nodes, and images to display when the
		/// tree node is in a selected and unselected state.
		/// </summary>
		/// <param name="text">The label <see cref="HRScreening.Components.ImageTreeNode"/>.Text
		/// of the new tree node.</param>
		/// <param name="imageIndex">The index value of <see cref="System.Drawing.Image"/> to
		/// display when the tree node is unselected.</param>
		/// <param name="selectedImageIndex">The index value of <see cref="System.Drawing.Image"/>
		/// to display when the tree node is selected.</param>
		/// <param name="children">An array of child <see cref="System.Windows.Forms.TreeNode"/>  objects.</param>
		public ImageTreeNode(string text, int imageIndex, int selectedImageIndex, TreeNode[] children)
			: base(text, imageIndex, selectedImageIndex, children)
		{
		}

		#endregion

	}
}
