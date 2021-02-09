using System;
using System.Collections.Generic;
using System.Text;

namespace App.Framework
{
    public class Node
    {
        public object Data { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }


        public Node() : this(null, null, null)
        {
        }

        public Node(Node left, Node right, object data)
        {
            Data = data;
            Right = right;
            Left = left;
        }

    }

    public class ExpressionTree
    {
        public Node Root { get; set; }

        public ExpressionTree(Node root)
        {
            Root = root;
        }
    }
}
