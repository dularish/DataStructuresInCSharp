using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trees
{
    class MaxHeapTree
    {
        public Tree Root;

        public MaxHeapTree()
        {
            Root = null;
        }

        public class Tree
        {
            public int value;
            public Tree Left;
            public Tree Right;

            public Tree(int val)
            {
                value = val;
                Left = null;
                Right = null;
            }
        }

        public bool Search(ref Tree tree, int val)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
                if (tree.Left == null && tree.Right == null)
                {
                    tree = null;
                }
                else if (tree.Left == null)
                {
                    tree = tree.Right;
                }
                else if (tree.Right == null)
                {
                    tree = tree.Left;
                }
                else
                {
                    int valueToReplace = findLargestValueInSubTree(tree.Left);
                    Delete(ref tree, valueToReplace);
                    tree.value = valueToReplace;
                }
                return true;
            }
        }

        public int findLargestValueInSubTree(Tree tree)
        {
            throw new NotImplementedException();
            if (tree == null)
            {
                return Int32.MinValue;
            }
            else if (tree.Right == null)
            {
                return tree.value;
            }
            else
            {
                return findLargestValueInSubTree(tree.Right);
            }
        }

        public int findSmallestValueInTree(Tree tree)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
            if (tree != null)
            {
                InOrderTraverse(tree.Left);
                Console.WriteLine(tree.value + "\t");
                InOrderTraverse(tree.Right);
            }
        }

        public void PreOrderTraverse(Tree tree)
        {
            throw new NotImplementedException();
            if (tree != null)
            {
                Console.WriteLine(tree.value + "\t");
                PreOrderTraverse(tree.Left);
                PreOrderTraverse(tree.Right);
            }
        }

        public void PostOrderTraverse(Tree tree)
        {
            throw new NotImplementedException();
            if (tree != null)
            {
                PostOrderTraverse(tree.Left);
                PostOrderTraverse(tree.Right);
                Console.WriteLine(tree.value + "\t");
            }
        }

        public bool Insert(ref Tree tree, int val)
        {
            throw new NotImplementedException();
            if (val == Int32.MinValue)
            {
                return false;
            }
            else if (tree == null)
            {
                tree = new Tree(val);
                if (Root == null)
                {
                    Root = tree;
                }
                return true;
            }
            else if (val > tree.value)
            {
                return Insert(ref tree.Right, val);
            }
            else if (val < tree.value)
            {
                return Insert(ref tree.Left, val);
            }
            else
            {
                return false;
            }
        }

        public int FindNumberOfElements(ref Tree tree)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
            int height = FindHeightOfTree(ref tree);
            for (int i = 1; i <= height; i++)
            {
                DisplayElementsAtHeight(ref tree, i);
                Console.WriteLine();
            }
        }

        public void display(Tree tree, int level)
        {
            throw new NotImplementedException();
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
