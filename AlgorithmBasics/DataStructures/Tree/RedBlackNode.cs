namespace AlgorithmBasics.DataStructures.Tree
{
    public class RedBlackTree
    {
        /// <summary>
        /// A dummy object that represents null and the following options:
        /// 1) linked to root of the tree
        /// 2) Root.Parent linked to sentinel
        /// 3) if any node.Left == null OR node.Right == null then it will be linked to sentinel
        /// 4) sentinel.Parent == root
        /// 5) sentinel.Left == null AND sentinel.Right == null
        /// </summary>
        public RedBlackNode Sentinel { get; set; }
        public RedBlackNode Root { get; set; }

        public RedBlackTree(RedBlackNode root)
        {
            Root = root;
        }
    }

    public class RedBlackNode
    {
        public NodeColor Color { get; set; }

        public RedBlackNode Parent { get; set; }

        public RedBlackNode Left { get; set; }

        public RedBlackNode Right { get; set; }

        public int Size { get; set; }

        public int Key { get; }

        public RedBlackNode(int key,
                            NodeColor color = NodeColor.Black,
                            int size = 0,
                            RedBlackNode parent = null,
                            RedBlackNode left = null,
                            RedBlackNode right = null)
        {
            Color = color;
            Parent = parent;
            Left = left;
            Right = right;
            Size = size;
            Key = key;
        }

        public static void Insert(RedBlackTree T, RedBlackNode z)
        {
            RedBlackNode y = T.Sentinel;
            RedBlackNode x = T.Root;
            while (x != T.Sentinel)
            {
                y = x;
                if (z.Key < x.Key)
                {
                    x = x.Left;
                }
                else
                {
                    x = x.Right;
                }
            }
            z.Parent = y;
            if (y == T.Sentinel)
            {
                T.Root = z;
            }
            else if (z.Key < y.Key)
            {
                y.Left = z;
            }
            else
            {
                y.Right = z;
            }
            z.Left = T.Sentinel;
            z.Right = T.Sentinel;
            z.Color = NodeColor.Red;
            FixRedBlackProperty(T, z);
        }

        private static void FixRedBlackProperty(RedBlackTree T, RedBlackNode z)
        {
            while (z.Parent.Color == NodeColor.Red)
            {
                bool isParentLeft = z.Parent == z.Parent.Parent.Left;
                RedBlackNode uncle = isParentLeft ? z.Parent.Parent.Right : z.Parent.Parent.Left;
                
                if (uncle.Color == NodeColor.Red)
                {
                    uncle.Color = NodeColor.Black;
                    z.Parent.Color = NodeColor.Black;
                    z.Parent.Parent.Color = NodeColor.Red;
                    z = z.Parent.Parent;
                }
                else
                {
                    if (z == z.Parent.Right)
                    {
                        z = z.Parent;
                        // change?
                        LeftRotate(T, z);
                    }
                    z.Parent.Color = NodeColor.Black;
                    z.Parent.Parent.Color = NodeColor.Red;
                    // change?
                    RightRotate(T, z.Parent.Parent);
                }
            }

            z.Color = NodeColor.Black;
        }

        private static RedBlackNode Foo(RedBlackTree T, RedBlackNode uncle)
        {
            
        }

        public static void LeftRotate(RedBlackTree tree, RedBlackNode x)
        {
            RedBlackNode y = x.Right;
            x.Right = y.Left;
            if (y.Left != tree.Sentinel)
            {
                y.Left.Parent = x;
            }
            y.Parent = x.Parent;
            if (x.Parent == tree.Sentinel)
            {
                tree.Root = y;
            }
            else if (x == x.Parent.Left)
            {
                x.Parent.Left = y;
            }
            else
            {
                x.Parent.Right = y;
            }
            y.Left = x;
            x.Parent = y;
        }

        public static void RightRotate(RedBlackTree tree, RedBlackNode y)
        {
            RedBlackNode x = y.Left;
            x.Right = y.Left;
            if (x.Right != tree.Sentinel)
            {
                x.Right.Parent = y;
            }
            x.Parent = y.Parent;
            if (y.Parent == tree.Sentinel)
            {
                tree.Root = x;
            }
            else if (y == y.Parent.Left)
            {
                y.Parent.Left = x;
            }
            else
            {
                y.Parent.Right = x;
            }
            x.Right = y;
            y.Parent = x;
        }
    }

    public enum NodeColor
    {
        Black,
        Red
    }
}