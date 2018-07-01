using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trees
{
    class SplayTree
    {
        public Tree Root;

        public SplayTree()
        {
            Root = null;
        }

        public class Tree
        {
            public int value;
            public Tree Left;
            public Tree Right;
            public Tree Parent;

            public Tree(int val, Tree parent)
            {
                value = val;
                Left = null;
                Right = null;
                Parent = parent;
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
                if (tree.Right == null)
                {
                    Splay(tree);
                    return false;
                }
                else
                {
                    return Search(ref tree.Right, val);
                }
                
            }
            else if (val < tree.value)
            {
                if (tree.Left == null)
                {
                    Splay(tree);
                    return false;
                }
                else
                {
                    return Search(ref tree.Left, val);
                }
            }
            else
            {
                Splay(tree);
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
                if (tree.Right == null)
                {
                    Splay(tree);
                    return false;
                }
                else
                {
                    return Delete(ref tree.Right, val);
                }
            }
            else if (val < tree.value)
            {
                if (tree.Left == null)
                {
                    Splay(tree);
                    return false;
                }
                else
                {
                    return Delete(ref tree.Left, val);
                }
            }
            else
            {
                if (tree.Left == null && tree.Right == null)
                {
                    tree = null;
                }
                else if (tree.Left == null)
                {
                    tree.Right.Parent = tree.Parent;
                    tree = tree.Right;
                }
                else if (tree.Right == null)
                {
                    tree.Left.Parent = tree.Parent;
                    tree = tree.Left;
                }
                else
                {
                    int valueToReplace = findLargestValueInSubTree(tree.Left).value;
                    Delete(ref tree, valueToReplace);
                    tree.value = valueToReplace;
                }
                Splay(tree);
                return true;
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

        public bool InsertValueToSplayTree(int val)
        {
            if (!Search(ref Root, val))
            {
                return Insert(ref Root, val);
            }
            else
            {
                return false;
            }
        }

        private bool Insert(ref Tree tree, int val, Tree Parent = null)
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
                Splay(tree);
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

        private void Splay(Tree tree)
        {
            if (tree == Root)
            {
                return;
            }
            else if (tree.Parent == Root)
            {
                if (tree.Parent.Left == tree)
                {
                    RightRotate(tree.Parent);
                }
                else if (tree.Parent.Right == tree)
                {
                    LeftRotate(tree.Parent);
                }
                else
                {
                    Console.WriteLine("11.10 Invalid execution block");
                }
            }
            else if (tree.Parent.Left == tree && tree.Parent.Parent.Left == tree.Parent)
            {
                RightRotate(tree.Parent.Parent);
                RightRotate(tree.Parent);
            }
            else if (tree.Parent.Right == tree && tree.Parent.Parent.Right == tree.Parent)
            {
                LeftRotate(tree.Parent.Parent);
                LeftRotate(tree.Parent);
            }
            else if (tree.Parent.Right == tree && tree.Parent.Parent.Left == tree.Parent)
            {
                LeftRotate(tree.Parent);
                RightRotate(tree.Parent);
            }
            else if (tree.Parent.Left == tree && tree.Parent.Parent.Right == tree.Parent)
            {
                RightRotate(tree.Parent);
                LeftRotate(tree.Parent);
            }
            else
            {
                Console.WriteLine("11.20 Invalid execution block");
            }
            Splay(tree);
        }



        private void RightRotate(Tree tree)
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
        }

        private void LeftRotate(Tree tree)
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
                Console.Write(tree.value);
                
                display(tree.Left, level + 1);
            }
        }
    }
}
