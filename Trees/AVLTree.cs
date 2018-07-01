using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trees
{
    class AVLTree
    {
        public Tree Root;

        public AVLTree()
        {
            Root = null;
        }

        public class Tree
        {
            public int value;
            public int Balance;
            public Tree Left;
            public Tree Right;

            public Tree(int val)
            {
                value = val;
                Left = null;
                Right = null;
                Balance = 0;
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

        public Tree getParentRef(Tree tree, Tree parentToReturn)
        {
            if (Root == tree)
            {
                return null;
            }
            else
            {
                if(parentToReturn == null)
                {
                    return null;
                }
                else if (tree == parentToReturn.Left || tree == parentToReturn.Right)
                {
                    return parentToReturn;
                }
                else
                {
                    Tree leftBranchResult = getParentRef(tree, tree.Left);
                    Tree rightBranchResult = getParentRef(tree, tree.Right);

                    return leftBranchResult == null ? rightBranchResult : leftBranchResult;
                }
            }
        }

        public bool Delete(ref Tree tree, int val)
        {
            throw new UnauthorizedAccessException();
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

        public bool Delete(ref Tree tree, int val, ref bool ht_inc)
        {
            if (tree == null)
            {
                return false;
            }
            else if (tree == Root && tree.value == val)
            {
                tree = Root = null;
                return true;
            }
            else if (val > tree.value)
            {
                bool valueToReturn =  Delete(ref tree.Right, val, ref ht_inc);

                if (ht_inc)
                {
                    switch (tree.Balance)
                    {
                        case -1:
                            tree.Balance = 0;
                            break;
                        case 0:
                            tree.Balance = 1;
                            ht_inc = false;
                            break;
                        case 1:
                            if (tree.Left.Balance == 0)
                            {
                                RightRotate(tree);
                            }
                            else if (tree.Left.Balance == 1)
                            {
                                RightRotate(tree);
                            }
                            else if (tree.Left.Balance == -1)
                            {
                                LeftRotate(tree.Left);
                                RightRotate(tree);
                            }
                            else
                            {
                                Console.WriteLine("10.10 Unexpected execution block");
                            }
                            break;
                    }
                }


                return valueToReturn;
            }
            else if (val < tree.value)
            {
                bool valueToReturn = Delete(ref tree.Left, val,ref ht_inc);

                if (ht_inc)
                {
                    switch (tree.Balance)
                    {
                        case 1:
                            tree.Balance = 0;
                            break;
                        case 0:
                            tree.Balance = -1;
                            ht_inc = false;
                            break;
                        case -1:
                            if (tree.Left.Balance == 0)
                            {
                                LeftRotate(tree);
                            }
                            else if (tree.Left.Balance == 1)
                            {
                                LeftRotate(tree);
                            }
                            else if (tree.Left.Balance == -1)
                            {
                                RightRotate(tree.Left);
                                LeftRotate(tree);
                            }
                            else
                            {
                                Console.WriteLine("10.20 Unexpected execution block");
                            }
                            break;
                    }
                }


                return valueToReturn;
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
                    Delete(ref tree, valueToReplace,ref ht_inc);
                    tree.value = valueToReplace;
                }
                ht_inc = true;
                return true;
            }
        }


        private void RightRotate(Tree tree)
        {
            Tree A = tree.Left;
            Tree B = tree.Right;
            if (A == null)
            {
                throw new Exception("Cannot right rotate on a tree where Left is null");
            }
            Tree parent = getParentRef(tree, Root);
            if (parent == null)
            {
                Root = A;
            }
            else if (parent.Right == tree)
            {
                parent.Right = A;
            }
            else if (parent.Left == tree)
            {
                parent.Left = A;
            }
            else
            {
                Console.WriteLine("10.30 Unexpected execution block");
            }
            tree.Left = A.Right;

            if (tree == Root)
            {
                Root = A;
            }
            A.Right = tree;

            tree.Balance = FindHeightOfTree(ref tree.Left) - FindHeightOfTree(ref tree.Right);
            A.Balance = FindHeightOfTree(ref A.Left) - FindHeightOfTree(ref A.Right);
        }

        private void LeftRotate(Tree tree)
        {
            Tree A = tree.Left;
            Tree B = tree.Right;
            if (B == null)
            {
                throw new Exception("Cannot left rotate on a tree where Left is null");
            }
            Tree parent = getParentRef(tree, Root);
            if (parent == null)
            {
                Root = A;
            }
            else if (parent.Right == tree)
            {
                parent.Right = B;
            }
            else if (parent.Left == tree)
            {
                parent.Left = B;
            }
            else
            {
                Console.WriteLine("10.40 Unexpected execution block");
            }
            tree.Right = B.Left;

            if (tree == Root)
            {
                Root = B;
            }
            B.Left = tree;

            tree.Balance = FindHeightOfTree(ref tree.Left) - FindHeightOfTree(ref tree.Right);
            B.Balance = FindHeightOfTree(ref B.Left) - FindHeightOfTree(ref B.Right);
        }

        public int findLargestValueInSubTree(Tree tree)
        {
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

        public bool Insert(ref Tree tree, int val)
        {
            throw new UnauthorizedAccessException();
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

        public bool Insert(ref Tree tree, int val,ref bool ht_inc)
        {
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
                ht_inc = true;
                return true;
            }
            else if (val > tree.value)
            {
                bool valueToReturn = Insert(ref tree.Right, val, ref ht_inc);
                Tree A;//Conflict Node
                Tree B;//Lower to conflict
                Tree C;//Lower to B
                if (ht_inc)
                {
                    switch (tree.Balance)
                    {
                        case 1:
                            tree.Balance = 0;
                            ht_inc = false;
                            break;
                        case 0:
                            tree.Balance = -1;
                            break;
                        case -1:
                            A = tree;
                            B = tree.Right;
                            if (B.Balance == -1)
                            {
                                A.Right = B.Left;//RR Rotation
                                A.Balance = FindHeightOfTree(ref A.Left) - FindHeightOfTree(ref A.Right);
                                B.Left = A;
                                tree = B;
                            }
                            else
                            {
                                C = B.Left;//RL Rotation
                                A.Right = C.Left;
                                B.Left = C.Right;
                                A.Balance = FindHeightOfTree(ref A.Left) - FindHeightOfTree(ref A.Right);
                                B.Balance = FindHeightOfTree(ref B.Left) - FindHeightOfTree(ref B.Right);
                                C.Left = A;
                                C.Right = B;
                                tree = C;
                            }
                            tree.Balance = 0;
                            break;
                        default:
                            break;
                    }
                }



                return valueToReturn;
            }
            else if (val < tree.value)
            {
                bool valueToReturn = Insert(ref tree.Left, val, ref ht_inc);
                Tree A;//Conflict Node
                Tree B;//Lower to conflict
                Tree C;//Lower to B

                if (ht_inc)
                {
                    switch (tree.Balance)
                    {
                        case -1:
                            tree.Balance = 0;
                            ht_inc = false;
                            break;
                        case 0:
                            tree.Balance = 1;
                            break;
                        case 1:
                            A = tree;
                            B = tree.Left;
                            if (B.Balance == 1)
                            {
                                A.Left = B.Right;//LL Rotation
                                A.Balance = FindHeightOfTree(ref A.Left) - FindHeightOfTree(ref A.Right);
                                B.Right = A;
                                tree = B;
                            }
                            else
                            {
                                C = B.Right;//LR Rotation
                                A.Left = C.Right;
                                B.Right = C.Left;
                                A.Balance = FindHeightOfTree(ref A.Left) - FindHeightOfTree(ref A.Right);
                                B.Balance = FindHeightOfTree(ref B.Left) - FindHeightOfTree(ref B.Right);
                                C.Left = B;
                                C.Right = A;
                                tree = C;
                            }
                            tree.Balance = 0;
                            break;
                        default:
                            break;
                    }
                }


                return valueToReturn;
            }
            else
            {
                return false;
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
                Console.Write(tree.value);
                display(tree.Left, level + 1);
            }
        }
    }
}
