using System;
using System.Collections.Generic;

namespace Zyrenth.Collections
{

	/**
	 * Implements a red-black tree.
	 * Note that all "matching" is based on the compareTo method.
	 * @author Mark Allen Weiss
	 */
	public class RedBlackTree<T> where T : System.IComparable<T>
	{

		internal enum RedBlackNodeColor
		{
			Red,
			Black
		};

		internal class RedBlackNode
		{

			public T Item { get; set; }

			public RedBlackNode Left { get; set; }

			public RedBlackNode Right { get; set; }

			public RedBlackNodeColor Color { get; set; }

			public RedBlackNode()
				: this(default(T))
			{
			}

			// Constructors
			public RedBlackNode(T theElement)
				: this(theElement, nullNode, nullNode)
			{

			}

			public RedBlackNode(T theElement, RedBlackNode lt, RedBlackNode rt)
			{
				Item = theElement;
				Left = lt;
				Right = rt;
				Color = RedBlackNodeColor.Black;
			}
		}

		private RedBlackNode header;
		private static RedBlackNode nullNode;
		// Used in insert routine and its helpers
		private static RedBlackNode current, parent, grand, great;

		static RedBlackTree()
		{
			nullNode = new RedBlackNode();
			nullNode.Left = nullNode.Right = nullNode;
		}

		/**
		 * Construct the tree.
		 */
		public RedBlackTree()
		{
			header = new RedBlackNode();
		}

		/**
		 * Compare item and t.element, using compareTo, with
		 * caveat that if t is header, then item is always larger.
		 * This routine is called if is possible that t is header.
		 * If it is not possible for t to be header, use compareTo directly.
		 */
		private int compare(T item, RedBlackNode t)
		{
			if (t == header)
				return 1;
			else
				return item.CompareTo(t.Item);

		}

		/// <summary>
		/// Insert into the tree.
		/// </summary>
		/// <param name="item">the item to insert.</param>
		/// <exception cref="ArgumentOutOfRangeException">if item is already present.</exception>
		public void Add(T item)
		{
			current = parent = grand = header;
			nullNode.Item = item;

			while (compare(item, current) != 0)
			{
				great = grand;
				grand = parent;
				parent = current;
				current = compare(item, current) < 0 ?
					current.Left : current.Right;

				// Check if two red children; fix if so
				if (current.Left.Color == RedBlackNodeColor.Red && current.Right.Color == RedBlackNodeColor.Red)
					ReorientTree(item);
			}

			// Insertion fails if already present
			if (current != nullNode)
				//    throw new DuplicateItemException( item.toString( ) );
				throw new ArgumentOutOfRangeException();
			current = new RedBlackNode(item);

			// Attach to parent
			if (compare(item, parent) < 0)
				parent.Left = current;
			else
				parent.Right = current;
			ReorientTree(item);
		}

		/**
		 * Remove from the tree.
		 * @param x the item to remove.
		 * @throws UnsupportedOperationException if called.
		 */
		public void Remove(T x)
		{
			throw new NotImplementedException();
		}



		/// <summary>
		/// Find the smallest item  the tree.
		/// </summary>
		/// <returns>the smallest item or null if empty.</returns>
		public T findMin()
		{
			if (isEmpty())
				return default(T);

			RedBlackNode itr = header.Right;

			while (itr.Left != nullNode)
				itr = itr.Left;

			return itr.Item;
		}

		/**
		 * Find the largest item in the tree.
		 * @return the largest item or null if empty.
		 */
		public T findMax()
		{
			if (isEmpty())
				return default(T);

			RedBlackNode itr = header.Right;

			while (itr.Right != nullNode)
				itr = itr.Right;

			return itr.Item;
		}

		/**
		 * Find an item in the tree.
		 * @param x the item to search for.
		 * @return the matching item or null if not found.
		 */
		public T find(T x)
		{
			nullNode.Item = x;
			current = header.Right;

			for (; ;)
			{
				if (x.CompareTo(current.Item) < 0)
					current = current.Left;
				else if (x.CompareTo(current.Item) > 0)
					current = current.Right;
				else if (current != nullNode)
					return current.Item;
				else
					return default(T);
			}
		}

		/**
		 * Make the tree logically empty.
		 */
		public void Clear()
		{
			header = nullNode;
		}

		/**
		 * Internal method to print a subtree in sorted order.
		 * @param t the node that roots the tree.
		 */
		private void printTree(RedBlackNode t)
		{
			if (t != nullNode)
			{
				printTree(t.Left);
				Console.WriteLine(t.Item);
				printTree(t.Right);
			}
		}

		/**
		 * Test if the tree is logically empty.
		 * @return true if empty, false otherwise.
		 */
		public bool isEmpty()
		{
			return header.Right == nullNode;
		}

		/**
		 * Internal routine that is called during an insertion
		 * if a node has two red children. Performs flip and rotations.
		 * @param item the item being inserted.
		 */
		internal void ReorientTree(T item)
		{
			// Do the color flip
			current.Color = RedBlackNodeColor.Red;
			current.Left.Color = RedBlackNodeColor.Black;
			current.Right.Color = RedBlackNodeColor.Black;

			if (parent.Color == RedBlackNodeColor.Red)   // Have to rotate
			{
				grand.Color = RedBlackNodeColor.Red;
				if ((compare(item, grand) < 0) !=
						(compare(item, parent) < 0))
					parent = rotate(item, grand);  // Start dbl rotate
				current = rotate(item, great);
				current.Color = RedBlackNodeColor.Black;
			}
			header.Right.Color = RedBlackNodeColor.Black; // Make root black
		}

		/**
		 * Internal routine that performs a single or double rotation.
		 * Because the result is attached to the parent, there are four cases.
		 * Called by handleReorient.
		 * @param item the item in handleReorient.
		 * @param parent the parent of the root of the rotated subtree.
		 * @return the root of the rotated subtree.
		 */
		internal RedBlackNode rotate(T item, RedBlackNode parent)
		{
			if (compare(item, parent) < 0)
				return parent.Left = compare(item, parent.Left) < 0 ?
					rotateWithLeftChild(parent.Left) : // LL
					rotateWithRightChild(parent.Left);  // LR
			else
				return parent.Right = compare(item, parent.Right) < 0 ?
					rotateWithLeftChild(parent.Right) : // RL
					rotateWithRightChild(parent.Right);  // RR
		}

		/**
		 * Rotate binary tree node with left child.
		 */
		private static RedBlackNode rotateWithLeftChild(RedBlackNode k2)
		{
			RedBlackNode k1 = k2.Left;
			k2.Left = k1.Right;
			k1.Right = k2;
			return k1;
		}

		/**
		 * Rotate binary tree node with right child.
		 */
		private static RedBlackNode rotateWithRightChild(RedBlackNode k1)
		{
			RedBlackNode k2 = k1.Right;
			k1.Right = k2.Left;
			k2.Left = k1;
			return k2;
		}
	}

}
