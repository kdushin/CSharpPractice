using System;

namespace AlgorithmBasics.DataStructures.Tree
{
    public class BstNode
    {
        public BstNode Parent { get; set; }
        
        public BstNode Left { get; set; }
        
        public BstNode Right { get; set; }
        
        public int Key { get; }

        public BstNode(int key)
        {
            Key = key;
        }

        public void Insert(BinarySearchTree tree, BstNode insertionNode)
        {
            BstNode previousNode = null;
            BstNode currentNode = tree.Root;

            while (currentNode != null)
            {
                previousNode = currentNode;
                currentNode = insertionNode.Key < currentNode.Key ? currentNode.Left : currentNode.Right;
            }

            insertionNode.Parent = previousNode;

            if (previousNode == null)
            {
                tree.Root = insertionNode;
            }
            else if (insertionNode.Key < previousNode.Key)
            {
                previousNode.Left = insertionNode;
            }
            else
            {
                previousNode.Right = insertionNode;
            }
        }

        public static void Print(BstNode treeNode)
        {
            if (treeNode != null)
            {
                Print(treeNode.Left);
                Console.WriteLine(treeNode.Key);
                Print(treeNode.Right);
            }
        }

        public static BstNode Search(BstNode start, int key)
        {
            while (start != null && key != start.Key)
            {
                if (key < start.Key)
                {
                    start = start.Left;
                }
                else
                {
                    start = start.Right;
                }
            }

            return start;
        }

        public static BstNode Min(BstNode searchNode)
        {
            while (searchNode.Left != null)
            {
                searchNode = searchNode.Left;
            }
            return searchNode;
        }
        
        public static BstNode Max(BstNode searchNode)
        {
            while (searchNode.Right != null)
            {
                searchNode = searchNode.Right;
            }
            return searchNode;
        }

        public static BstNode Successor(BstNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException();
            }
            
            if (node.Right != null)
            {
                return Min(node.Right);
            }
            else
            {
                BstNode parent = node.Parent;
                while (parent != null && node == parent.Right)
                {
                    node = parent;
                    parent = parent.Parent;
                }
                return parent;
            }
        }

        public static BstNode Predecessor(BstNode node)
        {
            if (node == null)
            {
                throw new ArgumentException();
            }

            if (node.Left != null)
            {
                return Max(node.Left);
            }
            else
            {
                BstNode parent = node.Parent;
                while (parent != null && node == parent.Left)
                {
                    node = parent;
                    parent = parent.Parent;
                }
                return parent;
            }
        }

        public static void Delete(BinarySearchTree t, BstNode z)
        {
            if (z.Left == null)
            {
                Transplant(t, z, z.Right);
            }
            else if (z.Right == null)
            {
                Transplant(t, z, z.Left);
            }
            else
            {
                BstNode y = Min(z.Right);
                if (y.Parent != z)
                {
                    Transplant(t, y, y.Right);
                    y.Right = z.Right;
                    y.Right.Parent = y;
                }
                Transplant(t, z, y);
                y.Left = z.Left;
                y.Left.Parent = y;
            }
        }

        private static void Transplant(BinarySearchTree t,
                                       BstNode u,
                                       BstNode v)
        {
            if (u.Parent == null)
            {
                t.Root = v;
            }
            else if (u == u.Parent.Left)
            {
                u.Parent.Left = v;
            }
            else
            {
                u.Parent.Right = v;
            }

            if (v != null)
            {
                v.Parent = u.Parent;
            }
        }
    }

    public class BinarySearchTree
    {
        public BstNode Root { get; set; }

        public BinarySearchTree(BstNode root)
        {
            Root = root;
        }
    }

    public static class BstHelper
    {
        
    }

}