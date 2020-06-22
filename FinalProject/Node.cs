using System;


namespace FinalProject
{
    public class Node<T>
    {
        private T el;
        private Node<T> left;
        private Node<T> right;

        public Node(T element)
        {
            el = element;
            left = null;
            right = null;
        }

        public T Element
        {
            get
            {
                return el;
            }
            set
            {
                el = value;
            }
        }

        public Node<T> Left
        {
            get
            {
                return left;
            }
            set
            {
                left = value;
            }
        }

        public Node<T> Right
        {
            get
            {
                return right;
            }
            set
            {
                right = value;
            }
        }

        public Boolean isLeaf
        {
            get
            {
                return left == null && right == null;
            }
        }
    }
}