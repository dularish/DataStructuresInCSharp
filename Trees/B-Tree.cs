using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trees
{
    class B_Tree
    {
        public BTreeNode Root;
        public int minNumberOfKeysForTree;

        public B_Tree()
        {
            Root = null;
        }

        public class BTreeNode
        {
            public int[] keys;
            public BTreeNode[] children;
            public int totalKeysPresent;
            public bool IsLeaf;
            public int minNumberOfKeys;

            public BTreeNode(int _minNumberOfKeys, bool _isLeaf)
            {
                keys = new int[_minNumberOfKeys * 2 - 1];
                children = new BTreeNode[_minNumberOfKeys * 2];
                IsLeaf = _isLeaf;
                minNumberOfKeys = _minNumberOfKeys;
                totalKeysPresent = 0;
            }

            internal int findKey(int valueToDelete)
            {
                int i = 0;
                for (i = 0; i < totalKeysPresent; i++)
                {
                    if (keys[i] >= valueToDelete )
                    {
                        break;
                    }
                }
                return i;
            }
        }

        public bool ProactiveInsert(int num, BTreeNode node)
        {
            if (node == Root && Root == null)
            {
                BTreeNode newRoot = new BTreeNode(minNumberOfKeysForTree, true);
                newRoot.keys[0] = num;
                newRoot.totalKeysPresent++;
                Root = newRoot;
                return true;
            }
            else if (node == Root && node.totalKeysPresent == (node.minNumberOfKeys * 2 - 1))
            {
                //implement Root splitting
                BTreeNode newRoot = new BTreeNode(node.minNumberOfKeys, false);
                newRoot.children[0] = Root;
                SplitChild(newRoot, 0);
                Root = newRoot;
                BTreeNode childToAddNewNode;

                if (newRoot.keys[0] > num)
                {
                    childToAddNewNode = newRoot.children[0];
                }
                else
                {
                    childToAddNewNode = newRoot.children[1];
                }

                int indexToInsertAt = 0;
                while (indexToInsertAt < childToAddNewNode.totalKeysPresent && childToAddNewNode.keys[indexToInsertAt] < num)
                {
                    indexToInsertAt++;
                }
                if (childToAddNewNode.keys[indexToInsertAt] == num)
                {
                    return false;
                }
                else
                {
                    if (childToAddNewNode.IsLeaf)
                    {
                        //Inserting at leaf
                        for (int i = childToAddNewNode.totalKeysPresent - 1; i >= indexToInsertAt; i--)
                        {
                            childToAddNewNode.keys[i + 1] = childToAddNewNode.keys[i];
                        }
                        childToAddNewNode.keys[indexToInsertAt] = num;
                        childToAddNewNode.totalKeysPresent++;
                        return true;
                    }
                    else
                    {
                        return ProactiveInsert(num, childToAddNewNode.children[indexToInsertAt]);
                    }
                }
                
                
            }
            else
            {
                int indexToInsertAt = 0;
                while (indexToInsertAt < node.totalKeysPresent && node.keys[indexToInsertAt] < num)
                {
                    indexToInsertAt++;
                }
                if (node.keys[indexToInsertAt] == num)
                {
                    return false;
                }
                else
                {
                    if (node.IsLeaf)
                    {
                        //Inserting at leaf
                        for (int i = node.totalKeysPresent - 1; i >= indexToInsertAt; i--)
                        {
                            node.keys[i + 1] = node.keys[i];
                        }
                        node.keys[indexToInsertAt] = num;
                        node.totalKeysPresent++;
                        return true;
                    }
                    else if (node.children[indexToInsertAt].totalKeysPresent == node.children[indexToInsertAt].minNumberOfKeys * 2 - 1)
                    {
                        //Child is full
                        SplitChild(node, indexToInsertAt);
                        if (indexToInsertAt + 1 == node.totalKeysPresent || node.keys[indexToInsertAt + 1] < num)
                        {
                            return ProactiveInsert(num, node.children[indexToInsertAt + 1]);
                        }
                        else
                        {
                            return ProactiveInsert(num, node.children[indexToInsertAt]);
                        }
                    }
                    else
                    {
                        return ProactiveInsert(num, node.children[indexToInsertAt]);
                    }
                }
            }
        }

        private void SplitChild(BTreeNode node, int indexToInsertAt)
        {
            //child is full if SplitChild is called
            BTreeNode child = node.children[indexToInsertAt];
            int childKeysNums = child.totalKeysPresent;

            BTreeNode secondHalfOfchildToCreate = new BTreeNode(child.minNumberOfKeys, child.IsLeaf);
            //First half - 0 to minNumberOfKeys - 1 ; For Eg., if minNumberOfKeys = 3, max is 5, First Half - 0,1 ; second Half - 3,4; if minNumberOfKeys = 4, max is 7, First Half - 0,1,2 ; Second Half - 4,5,6
            
            //Last minNumberOfKeys -1 keys to new child
            for (int i = 0; i < child.minNumberOfKeys - 1; i++)
            {
                secondHalfOfchildToCreate.keys[i] = child.keys[i + child.minNumberOfKeys];
                secondHalfOfchildToCreate.totalKeysPresent++;
            }
            if (!child.IsLeaf)
            {
                //Copying corresponding children
                for (int i = 0; i < child.minNumberOfKeys; i++)
                {
                    secondHalfOfchildToCreate.children[i] = child.children[i + child.minNumberOfKeys];
                }
            }
            
            //Instead of deleting from first child, just limiting the totalKeysPresent
            child.totalKeysPresent = child.minNumberOfKeys - 1;

            for (int j = node.totalKeysPresent; j > indexToInsertAt ; j--)
            {
                node.children[j + 1] = node.children[j];
            }

            for (int j = node.totalKeysPresent - 1; j >= indexToInsertAt; j--)
            {
                node.keys[j + 1] = node.keys[j];
            }

            node.keys[indexToInsertAt] = child.keys[child.minNumberOfKeys - 1];
            node.children[indexToInsertAt + 1] = secondHalfOfchildToCreate;

            node.totalKeysPresent++;
        }

        public bool Search(int num, BTreeNode node)
        {
            int keyIndex = 0;
            while (keyIndex < node.totalKeysPresent && node.keys[keyIndex] < num)
            {
                keyIndex++;
            }
            if (node.keys[keyIndex] == num)
            {
                return true;
            }
            if (node.IsLeaf)
            {
                return false;
            }
            else
            {
                return Search(num, node.children[keyIndex]);
            }
        }

        public void Traverse(BTreeNode node)
        {
            for (int i = 0; i < node.totalKeysPresent; i++)
            {
                if (!node.IsLeaf)
                {
                    Traverse(node.children[i]);
                }
                Console.Write(" " + node.keys[i]);
            }

            //For last child
            Traverse(node.children[node.totalKeysPresent]);
        }

        public void Display(BTreeNode node, int level)
        {
            if (!node.IsLeaf)
            {
                Display(node.children[node.totalKeysPresent], level + 1);
            }
            for (int i = node.totalKeysPresent - 1; i >=0 ; i--)
            {
                //int level = getLevel(node.keys[i], Root);
                for (int j = 0; j < level; j++)
                {
                    Console.Write("\t");
                }
                Console.Write( i + ": " + node.keys[i]);
                Console.WriteLine();
                if (!node.IsLeaf)
                {
                    Display(node.children[i], level + 1);
                }
            }
        }

        private int getLevel(int key, BTreeNode node, int currentLevel = 0)
        {
            int index = 0;
            while (index < node.totalKeysPresent && node.keys[index] < key)
            {
                index++;
            }
            if (node.keys[index] == key)
            {
                return currentLevel;
            }
            else
            {
                if (node.IsLeaf)
                {
                    return int.MinValue;
                }
                else
                {
                    return getLevel(key, node.children[index], currentLevel + 1);
                }
                
            }
        }

        internal bool Delete(ref BTreeNode bTreeNode, int valueToDelete)
        {
            int index = bTreeNode.findKey(valueToDelete);

            if (index < bTreeNode.totalKeysPresent && bTreeNode.keys[index] == valueToDelete)
            {
                if (bTreeNode.IsLeaf)
                {
                    deleteFromLeafNode(ref bTreeNode, valueToDelete, index);
                }
                else
                {
                    deleteFromNonLeafNode(ref bTreeNode, valueToDelete, index);
                }
                return true;
            }
            else
            {
                if (bTreeNode.IsLeaf)
                {
                    return false;
                }
                else
                {
                    bool WasIndexLastChild = index == bTreeNode.totalKeysPresent;

                    if (bTreeNode.children[index].totalKeysPresent < minNumberOfKeysForTree)
                    {
                        fill(ref bTreeNode, index);
                    }

                    if (WasIndexLastChild && index > bTreeNode.totalKeysPresent)
                    {
                        return Delete(ref bTreeNode.children[index - 1], valueToDelete);
                    }
                    else
                    {
                        return Delete(ref bTreeNode.children[index], valueToDelete);
                    }

                }
            }
        }

        private void fill(ref BTreeNode bTreeNode, int index)
        {
            if (index != 0 && bTreeNode.children[index - 1].totalKeysPresent >= minNumberOfKeysForTree)
            {
                borrowFromPrev(ref bTreeNode, index);
            }
            else if (index != bTreeNode.totalKeysPresent && bTreeNode.children[index + 1].totalKeysPresent >= minNumberOfKeysForTree)
            {
                borrowFromNext(ref bTreeNode, index);
            }
            else
            {
                if (index != bTreeNode.totalKeysPresent)
                {
                    merge(ref bTreeNode, index);
                }
                else
                {
                    merge(ref bTreeNode, index - 1);
                }
            }
        }

        private void borrowFromNext(ref BTreeNode bTreeNode, int index)
        {
            BTreeNode childToFill = bTreeNode.children[index];
            BTreeNode nextSibiling = bTreeNode.children[index + 1];

            for (int i = 0; i < nextSibiling.totalKeysPresent - 1; i--)
            {
                nextSibiling.keys[i] = nextSibiling.keys[i + 1];
            }

            if (!nextSibiling.IsLeaf)
            {
                for (int i = 0; i <= nextSibiling.totalKeysPresent - 1; i--)
                {
                    nextSibiling.children[i] = nextSibiling.children[i + 1];
                }
            }

            childToFill.keys[childToFill.totalKeysPresent] = bTreeNode.keys[index];//prevSibiling.keys[prevSibiling.totalKeysPresent - 1];

            if (!childToFill.IsLeaf)
            {
                childToFill.children[childToFill.totalKeysPresent + 1] = nextSibiling.children[0];
            }

            bTreeNode.keys[index] = nextSibiling.keys[0];

            childToFill.totalKeysPresent++;
            nextSibiling.totalKeysPresent--;
        }

        private void borrowFromPrev(ref BTreeNode bTreeNode, int index)
        {
            BTreeNode childToFill = bTreeNode.children[index];
            BTreeNode prevSibiling = bTreeNode.children[index - 1];

            for (int i = childToFill.totalKeysPresent - 1; i >= 0; i--)
            {
                childToFill.keys[i + 1] = childToFill.keys[i];
            }

            if (!childToFill.IsLeaf)
            {
                for (int i = childToFill.totalKeysPresent; i >= 0; i--)
                {
                    childToFill.children[i + 1] = childToFill.children[i];
                }
            }

            childToFill.keys[0] = bTreeNode.keys[index - 1];//prevSibiling.keys[prevSibiling.totalKeysPresent - 1];

            if (!childToFill.IsLeaf)
            {
                childToFill.children[0] = prevSibiling.children[prevSibiling.totalKeysPresent];
            }

            bTreeNode.keys[index - 1] = prevSibiling.keys[prevSibiling.totalKeysPresent - 1];

            childToFill.totalKeysPresent++;
            prevSibiling.totalKeysPresent--;
        }

        private void deleteFromNonLeafNode(ref BTreeNode bTreeNode, int valueToDelete, int index)
        {
            if (bTreeNode.children[index].totalKeysPresent >= minNumberOfKeysForTree)
            {
                BTreeNode leftChild = bTreeNode.children[index];
                int pred = leftChild.keys[leftChild.totalKeysPresent - 1];
                bTreeNode.keys[index] = pred;
                Delete(ref leftChild, pred);
            }
            else if(bTreeNode.children[index + 1].totalKeysPresent >= minNumberOfKeysForTree)
            {
                BTreeNode rightChild = bTreeNode.children[index + 1];
                int succ = rightChild.keys[rightChild.totalKeysPresent - 1];
                bTreeNode.keys[index] = succ;
                Delete(ref rightChild, succ);
            }
            else
            {
                merge(ref bTreeNode, index);
                Delete(ref bTreeNode.children[index], valueToDelete);
            }
        }

        private void merge(ref BTreeNode bTreeNode, int index)
        {
            BTreeNode leftChild = bTreeNode.children[index];
            BTreeNode rightChild = bTreeNode.children[index + 1];

            leftChild.keys[leftChild.totalKeysPresent] = bTreeNode.keys[index];
            leftChild.totalKeysPresent++;

            for (int i = leftChild.totalKeysPresent; i < leftChild.totalKeysPresent + rightChild.totalKeysPresent; i++)
            {
                if (!leftChild.IsLeaf)
                {
                    leftChild.children[i] = rightChild.children[i - leftChild.totalKeysPresent];
                }
                leftChild.keys[i] = rightChild.keys[i - leftChild.totalKeysPresent];
            }
            if (!leftChild.IsLeaf)
            {
                leftChild.children[leftChild.totalKeysPresent + rightChild.totalKeysPresent] = rightChild.children[rightChild.totalKeysPresent];
            }
            leftChild.totalKeysPresent = leftChild.totalKeysPresent + rightChild.totalKeysPresent;

            for (int i = index; i < bTreeNode.totalKeysPresent; i++)
            {
                bTreeNode.keys[index] = bTreeNode.keys[index + 1];
                bTreeNode.children[index + 1] = bTreeNode.children[index + 2];
            }
            
            for (int i = index + 1; i < bTreeNode.totalKeysPresent - 1; i++)
            {
                bTreeNode.keys[i] = bTreeNode.keys[i + 1];
            }
            bTreeNode.totalKeysPresent--;
        }

        private void deleteFromLeafNode(ref BTreeNode bTreeNode, int valueToDelete, int index)
        {
            for (int i = index; i < bTreeNode.totalKeysPresent -1 ; i++)
            {
                bTreeNode.keys[i] = bTreeNode.keys[i + 1];
            }

            bTreeNode.totalKeysPresent--;
        }

        internal int FindNumberOfElements(ref BTreeNode bTreeNode)
        {
            throw new NotImplementedException();
        }

        internal int FindHeightOfTree(ref BTreeNode bTreeNode)
        {
            throw new NotImplementedException();
        }

        internal void InOrderTraverse(BTreeNode bTreeNode)
        {
            throw new NotImplementedException();
        }

        internal void PreOrderTraverse(BTreeNode bTreeNode)
        {
            throw new NotImplementedException();
        }

        internal void PostOrderTraverse(BTreeNode bTreeNode)
        {
            throw new NotImplementedException();
        }

        internal void LevelOrderTraverse(BTreeNode bTreeNode)
        {
            throw new NotImplementedException();
        }
    }
}
