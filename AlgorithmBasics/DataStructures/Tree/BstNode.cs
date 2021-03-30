using System;

namespace AlgorithmBasics.DataStructures.Tree
{
    public class BinarySearchTree
    {
        public BstNode Root { get; set; }

        public BinarySearchTree(BstNode root)
        {
            Root = root;
        }
    }


    public class BstNode
    {
        public BstNode Parent { get; set; }

        public BstNode Left { get; set; }

        public BstNode Right { get; set; }

        public int Size { get; set; }

        public int Key { get; }

        public BstNode(int key)
        {
            Key = key;
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
            while (start != null && key == start.Key)
            {
                if (key > start.Key)
                {
                    start = start.Right;
                }
                else
                {
                    start = start.Left;
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
                throw new ArgumentNullException();
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

        public void Insert(BinarySearchTree tree, BstNode insertionNode)
        {
            BstNode currentNode = tree.Root;
            BstNode previousNode = null;

            while (currentNode != null)
            {
                currentNode.Size++;
                previousNode = currentNode;
                currentNode = insertionNode.Key < currentNode.Key ? currentNode.Left : currentNode.Right;
            }

            insertionNode.Parent = previousNode;
            if (previousNode == null)
            {
                tree.Root = insertionNode;
            }
            else if (insertionNode.Key >= previousNode.Key)
            {
                previousNode.Right = insertionNode;
            }
            else
            {
                previousNode.Left = insertionNode;
            }
        }

        public static void Delete(BinarySearchTree t, BstNode z)
        {
            if (z.Left == null)
            {
                TransplantParents(t, z, z.Right);
            }
            else if (z.Right == null)
            {
                TransplantParents(t, z, z.Left);
            }
            else
            {
                BstNode y = Min(z.Right);
                if (y.Parent != z)
                {
                    TransplantParents(t, y, y.Right);
                    y.Right = z.Right;
                    y.Right.Parent = y;
                }

                TransplantParents(t, z, y);
                y.Left = z.Left;
                y.Left.Parent = y;
            }
        }

        // Replaces the subtree rooted at node u with the subtree rooted at node v, node u’s parent becomes node
        // v’s parent, and u’s parent ends up having v as its appropriate child
        private static void TransplantParents(BinarySearchTree t, BstNode u, BstNode v)
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
                BstNode someNode = v.Parent;
                while (someNode?.Parent != null)
                {
                    someNode.Size--;
                    someNode = someNode.Parent;
                }
            }
        }

        public static BstNode SelectKthItem(BinarySearchTree t, int k)
        {
            BstNode item = t.Root;

            while (k != 0 && item != null)
            {
                int leftSize = item.Left?.Size ?? 0;
                if (leftSize >= k)
                {
                    item = item.Left;
                }
                else
                {
                    k = k - leftSize - 1;
                    item = item.Right;
                }
            }

            return item;
        }
    }
}