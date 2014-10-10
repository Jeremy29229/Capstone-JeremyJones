using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace D_Quester
{
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="T"></typeparam>
	class Node<T>
	{
		/// <summary>
		/// 
		/// </summary>
		public T info;

		/// <summary>
		/// 
		/// </summary>
		public List<Node<T>> parents{ get; private set; }

		/// <summary>
		/// 
		/// </summary>
		public List<Node<T>> children{ get; private set; }
	 
		/// <summary>
		/// 
		/// </summary>
		/// <param name="value"></param>
		public Node(T value)
		{
			parents = new List<Node<T>>();
			children = new List<Node<T>>();
			info = value;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="child"></param>
		public void AddChild(Node<T> child)
		{
			children.Add(child);
			if (!child.parents.Contains(this))
			{
				child.AddParent(this);
			}
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="parent"></param>
		public void AddParent(Node<T> parent)
		{
			parents.Add(parent);
			if(!parent.children.Contains(this))
			{
				parent.AddChild(this);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="child"></param>
		public void RemoveChild(Node<T> child)
		{
			children.Remove(child);
			if (child.parents.Contains(this))
			{
				child.RemoveParent(this);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="parent"></param>
		public void RemoveParent (Node<T> parent)
		{
			parents.Remove(parent);
			if (parent.children.Contains(this))
			{
				parent.RemoveChild(this);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public void RemoveAllChildren()
		{
			foreach (var v in children)
			{
				RemoveChild(v);
			}
		}

		/// <summary>
		/// 
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
