using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    public class Tree<T>
    {
        public enum Relative
        {
            LeftChild, RightChild, Parent, Root
        }

        private Node<T> root;
        private Node<T> current;
        private int size;

        public Tree()
        {
            root = null;
            current = null;
            size = 0;
        }

        public int Size
        {
            get
            {
                return size;
            }
        }

        public Node<T> Current
        {
            get
            {
                return current;
            }
        }

        public Node<T> Root
        {
            get
            {
                return root;
            }
        }

        public Boolean isEmpty
        {
            get
            {
                return root == null;
            }
        }

        /// <summary>
        /// Method to traverse to a child node, a root node, or a parent node.
        /// </summary>
        /// <param name="relative"></param>
        /// <returns>bool</returns>
        public Boolean moveTo(Relative relative)
        {
            Boolean found = false;

            switch (relative)
            {
                case Relative.LeftChild:
                    if (current.Left != null)
                    {
                        current = current.Left;
                        found = true;
                    }

                    break;

                case Relative.RightChild:
                    if (current.Right != null)
                    {
                        current = current.Right;
                        found = true;
                    }
                    break;

                case Relative.Root:
                    if (root != null)
                    {
                        current = root;
                        found = true;
                    }
                    break;

                case Relative.Parent:
                    if (current != root)
                    {
                        current = findParent(current);
                        found = true;
                    }
                    break;
            }

            return found;
        }

        /// <summary>
        /// Method to find the parent of a node when given a node.
        /// </summary>
        /// <param name="node"></param>
        /// <returns>Returns a TreeNode<T>.</returns>
        public Node<T> findParent(Node<T> node)
        {
            Stack<Node<T>> s = new Stack<Node<T>>();
            Node<T> n = root;

            while (n.Left != node && n.Right != node)
            {
                if (n.Right != null)
                    s.Push(n.Right);

                if (n.Left != null)
                    n = n.Left;
                else
                    n = s.Pop();
            }
            return n;
        }

        /// <summary>
        /// Add method. Takes a T value and an enum named Relative. 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="relative"></param>
        /// <returns>bool</returns>
        public Boolean Add(T value, Relative relative)
        {
            Boolean inserted = true;
            Node<T> newNode = new Node<T>(value);

            if ((relative == Relative.LeftChild && current.Left != null) ||
                relative == Relative.RightChild && current.Right != null)
            {
                inserted = false;
            }
            else
            {
                if (relative == Relative.LeftChild)
                {
                    current.Left = newNode;
                }
                else if (relative == Relative.RightChild)
                {
                    current.Right = newNode;
                }
                else if (relative == Relative.Root)
                {
                    if (root == null)
                    {
                        root = newNode;
                    }

                    current = root;
                }
            }

            if (inserted)
                size++;

            return inserted;
        }

        public int GetTreeSize(Tree<T> aTree)
        {
            int size = aTree.Size;
            return size;
        }

        //public bool FindNode(T value, Node<T> node)
        //{
        //    // I couldn't make anything I tried here actually work
        //}

        /// <summary>
        /// InOrder traversal method. 
        /// </summary>
        /// <param name="node"></param>
        public void InOrder(Node<T> node)
        {
            if (node != null)
            {
                InOrder(node.Left);
                Console.Write(node.Element.ToString());
                InOrder(node.Right);
            }
        }

        /// <summary>
        /// Method for PreOrder traversal.
        /// </summary>
        /// <param name="node"></param>
        public void PreOrder(Node<T> node)
        {
            if (node != null)
            {
                Console.Write(node.Element.ToString());
                PreOrder(node.Left);
                PreOrder(node.Right);
            }
        }

        /// <summary>
        /// Method for PostOrder traversal.
        /// </summary>
        /// <param name="node"></param>
        public void PostOrder(Node<T> node)
        {
            if (node != null)
            {
                PostOrder(node.Left);
                PostOrder(node.Right);
                Console.Write(node.Element.ToString());
            }
        }
    }
}
