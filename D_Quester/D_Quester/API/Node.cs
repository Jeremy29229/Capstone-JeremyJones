using System.Collections.Generic;


namespace D_Quester
{
	/// <summary>
	/// Represents a node with can have one or more parent nodes and one or more child nodes.
	/// </summary>
	/// <typeparam name="T">Object stored within node</typeparam>
	public class Node<T>
	{
		/// <summary>
		/// Object being stored.
		/// </summary>
		public T info;

		/// <summary>
		/// Every parent of this node. Will list this node in their own children list.
		/// </summary>
		public List<Node<T>> parents{ get; private set; }

		/// <summary>
		/// Every child of this node. Will list this node in their own parent list.
		/// </summary>
		public List<Node<T>> children{ get; private set; }
	 
		/// <summary>
		/// Adds the generic object to node and initializes lists.
		/// </summary>
		/// <param name="value">Object stored within node</param>
		public Node(T value)
		{
			parents = new List<Node<T>>();
			children = new List<Node<T>>();
			info = value;
		}

		/// <summary>
		/// Adds node to this nodes child list. Automatically adds this node to the node-passed-in's parent list.
		/// </summary>
		/// <param name="child">Node to be added to this nodes child list.</param>
		public void AddChild(Node<T> child)
		{
			children.Add(child);
			if (!child.parents.Contains(this))
			{
				child.AddParent(this);
			}
		}
		
		/// <summary>
		/// Adds node to this nodes parents list. Automatically adds this node to the node-passed-in's child list.
		/// </summary>
		/// <param name="parent">Node to be added to this node's parent list.</param>
		public void AddParent(Node<T> parent)
		{
			parents.Add(parent);
			if(!parent.children.Contains(this))
			{
				parent.AddChild(this);
			}
		}

		/// <summary>
		/// Removes node from this nodes child list. Automatically removes this node from the node-passed-in's parent list.
		/// </summary>
		/// <param name="child">Node to be removed from this node's child list.</param>
		public void RemoveChild(Node<T> child)
		{
			children.Remove(child);
			if (child.parents.Contains(this))
			{
				child.RemoveParent(this);
			}
		}

		/// <summary>
		/// Removes node from this nodes parent list. Automatically removes this node from the node-passed-in's child list.
		/// </summary>
		/// <param name="parent">Node to be removed from this node's parent list.</param>
		public void RemoveParent (Node<T> parent)
		{
			parents.Remove(parent);
			if (parent.children.Contains(this))
			{
				parent.RemoveChild(this);
			}
		}

		/// <summary>
		/// Removes all child nodes and removes this node of their parent list.
		/// </summary>
		public void RemoveAllChildren()
		{
			foreach (var v in children)
			{
				RemoveChild(v);
			}
		}

		/// <summary>
		/// Removes all parent nodes and removes this node from their child list.
		/// </summary>
		public void RemoveAllParents()
		{
			foreach (var v in parents)
			{
				RemoveParent(v);
			}
		}
	}
}
