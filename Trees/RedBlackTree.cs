using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trees
{
    class RedBlackTree
    {
        public Tree Root;

        public RedBlackTree()
        {
            Root = null;
        }

        public enum NodeColor
        {
            Red,Black
        }

        public class Tree
        {
            public int value;
            public NodeColor Color;
            public Tree Left;
            public Tree Right;
            public Tree Parent;

            public Tree(int val, Tree parent)
            {
                value = val;
                Left = null;
                Right = null;
                Parent = parent;
                Color = NodeColor.Red;
            }

            internal Tree Uncle()
            {
                Tree Parent = this.Parent;
                Tree GrandParent = Parent.Parent;
                if (Parent == null || GrandParent == null)
                {
                    return null;
                }
                else
                {
                    if (Parent == GrandParent.Left)
                    {
                        return GrandParent.Right;
                    }
                    else if (Parent == GrandParent.Right)
                    {
                        return GrandParent.Left;
                    }
                    else
                    {
                        return null;
                    }
                }
            }

            internal Tree Sibling()
            {
                Tree Parent = this.Parent;
                if (Parent == null)
                {
                    return null;
                }
                else
                {
                    return (this == Parent.Left) ? Parent.Right : Parent.Left;
                }
            }
        }

        public bool Search(ref Tree tree, int val)
        {
            if (tree == null)
            {
                return false;
            }
            else if (val > tree.value)
            {
                return Search(ref tree.Right, val);
            }
            else if (val < tree.value)
            {
                return Search(ref tree.Left, val);
            }
            else
            {
                return true;
            }
        }

        public bool Delete(ref Tree tree, int val)
        {
            if (tree == null)
            {
                return false;
            }
            else if (val > tree.value)
            {
                return Delete(ref tree.Right, val);
            }
            else if (val < tree.value)
            {
                return Delete(ref tree.Left, val);
            }
            else
            {
                if(tree.Left != null && tree.Right != null)
                {
                    Tree largestSubTree = findLargestValueInSubTree(tree.Left);
                    int valueToReplace = largestSubTree == null ? Int32.MinValue : largestSubTree.value;
                    NodeColor color = largestSubTree.Color;
                    Delete(ref tree, valueToReplace);
                    tree.value = valueToReplace;
                    tree.Color = tree == Root ? NodeColor.Black : color;
                }
                else
                {
                    if (tree.Left == null && tree.Right == null)
                    {
                        DeletionCase1(tree);
                        tree = null;
                    }
                    else if (tree.Left == null)
                    {
                        if (tree.Color != tree.Right.Color)
                        {
                            tree.Right.Parent = tree.Parent;
                            tree = tree.Right;
                            tree.Color = NodeColor.Black;
                        }
                        else
                        {
                            DeletionCase1(tree);
                            //tree.Right.Parent = tree.Parent;
                            //tree = tree.Right;
                            if (tree.Left == null && tree.Right == null)
                            {
                                tree = null;
                            }
                            else
                            {
                                Console.WriteLine("10.10 Unexpected block");
                            }
                        }
                    }
                    else if (tree.Right == null)
                    {
                        if (tree.Color != tree.Left.Color)
                        {
                            tree.Left.Parent = tree.Parent;
                            tree = tree.Left;
                            tree.Color = NodeColor.Black;
                        }
                        else
                        {
                            DeletionCase1(tree);
                            //tree.Left.Parent = tree.Parent;
                            //tree = tree.Left;
                            if (tree.Left == null && tree.Right == null)
                            {
                                tree = null;
                            }
                            else
                            {
                                Console.WriteLine("10.20 Unexpected block");
                            }
                        }
                    }
                }
                return true;
            }
        }

        private void DeletionCase1(Tree tree)
        {
            Tree sibiling = tree.Sibling();
            Tree parent = tree.Parent;
            if (sibiling != null)
            {
                if (sibiling.Color == NodeColor.Black)
                {
                    if (sibiling.Left != null && sibiling.Left.Color == NodeColor.Red)
                    {
                        if (sibiling == parent.Left)
                        {
                            sibiling.Left.Color = NodeColor.Black;
                            RightRotate(parent);
                        }
                        else if(sibiling == parent.Right)
                        {
                            sibiling.Left.Color = NodeColor.Black;
                            RightRotate(sibiling);
                            LeftRotate(parent);
                        }
                        else
                        {
                            Console.WriteLine("10.40 Unexpected block");
                        }
                    }
                    else if(sibiling.Right != null && sibiling.Right.Color == NodeColor.Red)
                    {
                        if (sibiling == parent.Left)
                        {
                            sibiling.Right.Color = NodeColor.Black;
                            LeftRotate(sibiling);
                            RightRotate(parent);
                        }
                        else if (sibiling == parent.Right)
                        {
                            sibiling.Right.Color = NodeColor.Black;
                            LeftRotate(parent);
                        }
                        else
                        {
                            Console.WriteLine("10.50 Unexpected block");
                        }
                    }
                    else
                    {
                        sibiling.Color = NodeColor.Red;

                        if (parent.Color == NodeColor.Red)
                        {
                            parent.Color = NodeColor.Black;
                        }
                        else
                        {
                            DeletionCase1(parent);
                        }
                    }
                }
                else
                {
                    //sibiling is red,so parent must be black
                    if (sibiling == parent.Left)
                    {
                        sibiling.Color = NodeColor.Black;
                        if (parent == Root)
                        {
                            Console.WriteLine("10.60 Unexpected block");
                        }
                        parent.Color = NodeColor.Red;
                        RightRotate(parent);
                    }
                    else if (sibiling == parent.Right)
                    {
                        sibiling.Color = NodeColor.Black;
                        if (parent == Root)
                        {
                            Console.WriteLine("10.70 Unexpected block");
                        }
                        parent.Color = NodeColor.Red;
                        LeftRotate(parent);
                    }
                    else
                    {
                        Console.WriteLine("10.80 Unexpected block");
                    }
                }
            }
            else
            {
                if (tree == Root)
                {
                    tree.Color = NodeColor.Black;
                }
                else
                {
                    Console.WriteLine("10.30 Unexpected block");
                }
            }
        }

        public Tree findLargestValueInSubTree(Tree tree)
        {
            if (tree == null)
            {
                return null;
            }
            else if (tree.Right == null)
            {
                return tree;
            }
            else
            {
                return findLargestValueInSubTree(tree.Right);
            }
        }

        public int findSmallestValueInTree(Tree tree)
        {
            if (tree == null)
            {
                return Int32.MinValue;
            }
            else if (tree.Left == null)
            {
                return tree.value;
            }
            else
            {
                return findSmallestValueInTree(tree.Left);
            }
        }

        public void InOrderTraverse(Tree tree)
        {
            if (tree != null)
            {
                InOrderTraverse(tree.Left);
                Console.WriteLine(tree.value + "\t");
                InOrderTraverse(tree.Right);
            }
        }

        public void PreOrderTraverse(Tree tree)
        {
            if (tree != null)
            {
                Console.WriteLine(tree.value + "\t");
                PreOrderTraverse(tree.Left);
                PreOrderTraverse(tree.Right);
            }
        }

        public void PostOrderTraverse(Tree tree)
        {
            if (tree != null)
            {
                PostOrderTraverse(tree.Left);
                PostOrderTraverse(tree.Right);
                Console.WriteLine(tree.value + "\t");
            }
        }

        public bool Insert(ref Tree tree, int val, Tree Parent = null)
        {
            if (val == Int32.MinValue)
            {
                return false;
            }
            else if (tree == null)
            {
                tree = new Tree(val, Parent);
                if (Root == null)
                {
                    Root = tree;
                }
                InsertionCase1(tree);
                return true;
            }
            else if (val > tree.value)
            {
                bool valueToReturn = Insert(ref tree.Right, val, tree);
                return valueToReturn;
            }
            else if (val < tree.value)
            {
                bool valueToReturn = Insert(ref tree.Left, val, tree);
                return valueToReturn;
            }
            else
            {
                return false;
            }
        }

        private void InsertionCase1(Tree tree)
        {
            if (tree == Root)
            {
                tree.Color = NodeColor.Black;
            }
            else
            {
                InsertionCase2(tree);
            }
        }

        private void InsertionCase2(Tree tree)
        {
            //Tree's Parent will never be null
            if (tree.Parent.Color == NodeColor.Black)
            {
                return;
            }
            else
            {
                InsertionCase3(tree);
            }
        }

        private void InsertionCase3(Tree tree)
        {
            //Parent is Red, GrandParent exists, checking for uncle's color
            Tree Uncle = tree.Uncle();
            if (Uncle == null || Uncle.Color == NodeColor.Black)
            {
                InsertionCase4(tree);
            }
            else
            {
                tree.Parent.Color = NodeColor.Black;
                Uncle.Color = NodeColor.Black;
                tree.Parent.Parent.Color = NodeColor.Red;
                InsertionCase1(tree.Parent.Parent);
            }
        }

        private void InsertionCase4(Tree tree)
        {
            //Uncle is black but parent is red
            Tree Grandparent = tree.Parent.Parent;

            if (tree.Parent == Grandparent.Left)
            {
                if (tree == tree.Parent.Left)
                {
                    RightRotate(Grandparent,true);
                }
                else
                {
                    LeftRotate(tree.Parent);
                    RightRotate(Grandparent, true);
                }
            }
            else
            {
                if (tree == tree.Parent.Right)
                {
                    LeftRotate(Grandparent, true);
                }
                else
                {
                    RightRotate(tree.Parent);
                    LeftRotate(Grandparent, true);
                }
            }

        }

        private void RightRotate(Tree tree, bool IsColorSwappingNeeded = false)
        {
            Tree A = tree.Left;
            Tree B = tree.Right;
            if(A == null)
            {
                throw new Exception("Cannot right rotate on a tree where Left is null");
            }
            if (A.Right != null)
            {
                A.Right.Parent = tree;
            }
            tree.Left = A.Right;

            if (tree.Parent != null)
            {
                if (tree == tree.Parent.Left)
                {
                    tree.Parent.Left = A;
                }
                else
                {
                    tree.Parent.Right = A;
                }
            }
            else
            {
                Root = A;
            }
            A.Parent = tree.Parent;
            tree.Parent = A;
            A.Right = tree;
            if (IsColorSwappingNeeded)
            {
                NodeColor temp = tree.Color;
                tree.Color = A.Color;
                A.Color = temp;
            }
        }

        private void LeftRotate(Tree tree, bool IsColorSwappingNeeded = false)
        {
            Tree A = tree.Left;
            Tree B = tree.Right;
            if (B == null)
            {
                throw new Exception("Cannot left rotate on a tree where Left is null");
            }
            if (B.Left != null)
            {
                B.Left.Parent = tree;
            }
            tree.Right = B.Left;

            if (tree.Parent != null)
            {
                if (tree == tree.Parent.Left)
                {
                    tree.Parent.Left = B;
                }
                else
                {
                    tree.Parent.Right = B;
                }
            }
            else
            {
                Root = B;
            }
            B.Parent = tree.Parent;
            tree.Parent = B;
            B.Left = tree;
            if (IsColorSwappingNeeded)
            {
                NodeColor temp = tree.Color;
                tree.Color = B.Color;
                B.Color = temp;
            }
        }

        public int FindNumberOfElements(ref Tree tree)
        {
            if (tree == null)
            {
                return 0;
            }
            else
            {
                return 1 + FindNumberOfElements(ref tree.Left) + FindNumberOfElements(ref tree.Right);
            }
        }

        public int FindHeightOfTree(ref Tree tree)
        {
            if (tree == null)
            {
                return 0;
            }
            else
            {
                int leftSubTreeHeight = 1 + FindHeightOfTree(ref tree.Left);
                int rightSubTreeHeight = 1 + FindHeightOfTree(ref tree.Right);
                return leftSubTreeHeight > rightSubTreeHeight ? leftSubTreeHeight : rightSubTreeHeight;
            }
        }

        public void DisplayElementsAtHeight(ref Tree tree, int height)
        {
            if (tree != null)
            {
                if (height == 1)
                {
                    Console.Write(tree.value + "\t");
                }
                else if(height > 1)
                {
                    DisplayElementsAtHeight(ref tree.Left, height - 1);
                    DisplayElementsAtHeight(ref tree.Right, height - 1);
                }
            }
        }

        public void LevelOrderTraverse(Tree tree)
        {
            int height = FindHeightOfTree(ref tree);
            for (int i = 1; i <= height; i++)
            {
                DisplayElementsAtHeight(ref tree, i);
                Console.WriteLine();
            }
        }

        public void display(Tree tree, int level)
        {
            if (tree != null)
            {
                display(tree.Right, level + 1);
                Console.WriteLine();
                for (int i = 0; i < level; i++)
                {
                    Console.Write("\t");
                }
                if (tree.Color == NodeColor.Red)
                {
                    Console.Write("R:" + tree.value);
                }
                else
                {
                    Console.Write("B:" + tree.value);
                }
                
                display(tree.Left, level + 1);
            }
        }
    }
}
