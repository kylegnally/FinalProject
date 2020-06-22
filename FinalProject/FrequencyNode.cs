using System;


namespace FinalProject
{
    public class FrequencyNode<T>
    {
        private T el;
        private FrequencyNode<T> left;
        private FrequencyNode<T> right;

        public FrequencyNode(T element)
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

        public FrequencyNode<T> Left
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

        public FrequencyNode<T> Right
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