using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_Quester
{
    class Node<T>
    {
        T info;

        public List<Node<T>> parents{ get; private set; }

        public List<Node<T>> children{ get; private set; }
     
        public Node(T value)
        {
            parents = new List<Node<T>>();
            children = new List<Node<T>>();
            info = value;
        }

        public void AddChild(Node<T> child)
        {
            children.Add(child);
            if (!child.parents.Contains(this))
            {
                child.AddParent(this);
            }
        }

        public void AddParent(Node<T> parent)
        {
            parents.Add(parent);
            if(!parent.children.Contains(this))
            {
                parent.AddChild(this);
            }
        }

        public void RemoveChild(Node<T> child)
        {
            children.Remove(child);
            if (child.parents.Contains(this))
            {
                child.RemoveParent(this);
            }
        }

        public void RemoveParent (Node<T> parent)
        {
            parents.Remove(parent);
            if (parent.children.Contains(this))
            {
                parent.RemoveChild(this);
            }
        }

        public void RemoveAllChildren()
        {
            foreach (var v in children)
            {
                RemoveChild(v);
            }
        }

        public void RemoveAllParents()
        {
            foreach (var v in parents)
            {
                RemoveParent(v);
            }
        }
    }
}
