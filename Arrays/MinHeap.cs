using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arrays
{
    public class MinHeap
    {
        public int[] HeapArray;

        public int maxSize;
        public int bottomIndex;

        public MinHeap(int size)
        {
            HeapArray = new int[size];
            maxSize = size;
        }

        public void Insert(int num)
        {
            int travIndex = bottomIndex;
            if (bottomIndex < maxSize)
            {
                HeapArray[bottomIndex] = num;
                bottomIndex++;
            }
            if (bottomIndex == 1)
            {
                return;
            }
            Validate(travIndex);
        }

        private void Validate(int travIndex)
        {
            if (travIndex == 0)
            {
                return;
            }
            int parentt = parent(travIndex);
            if (HeapArray[parentt] > HeapArray[travIndex])
            {
                int temp = HeapArray[travIndex];
                HeapArray[travIndex] = HeapArray[parentt];
                HeapArray[parentt] = temp;
                Validate(parentt);
            }
        }

        public int Search(int num, int index)
        {
            if (index < maxSize)
            {
                if (HeapArray[index] == num)
                {
                    return index;
                }
                else if (HeapArray[index] < num)
                {
                    int leftSearch = Search(num, left(index));
                    if ( leftSearch == -1)
                    {
                        return Search(num, right(index));
                    }
                    else
                    {
                        return leftSearch;
                    }
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                return -1;
            }
        }

        public int parent(int index)
        {
            return ((index + 1) / 2) - 1;
        }

        public int left(int index)
        {
            return (index * 2) + 1;
        }

        public int right(int index)
        {
            return (index * 2) + 2;
        }

        public void deleteKey(int i)
        {
            decreaseKeyValue(i, int.MinValue);
            extractMin();
        }

        private void decreaseKeyValue(int index, int new_value)
        {
            HeapArray[index] = new_value;

            while (index != 0 && HeapArray[parent(index)] > HeapArray[index])
            {
                int temp = HeapArray[index];
                HeapArray[index] = HeapArray[parent(index)];
                HeapArray[parent(index)] = temp;

                index = parent(index);
            }
        }

        public int extractMin()
        {
            if (bottomIndex == 0)
            {
                return int.MaxValue;
            }
            else
            {
                if (bottomIndex == 1)
                {
                    bottomIndex--;
                    return HeapArray[0];
                }
                else
                {
                    int max = HeapArray[0];
                    HeapArray[0] = HeapArray[bottomIndex - 1];
                    bottomIndex--;
                    MinHeapify(0);
                    return max;
                }
            }
        }

        private void MinHeapify(int index)
        {
            int MinIndex = index;

            if ((left(index)) < bottomIndex && HeapArray[left(index)] < HeapArray[MinIndex])
            {
                MinIndex = left(index);
            }
            if (right(index) < bottomIndex && HeapArray[right(index)] < HeapArray[MinIndex])
            {
                MinIndex = right(index);
            }

            if (MinIndex != index)
            {
                int temp = HeapArray[index];
                HeapArray[index] = HeapArray[MinIndex];
                HeapArray[MinIndex] = temp;

                MinHeapify(MinIndex);
            }
        }

        internal void display(int index)
        {
            if (index < bottomIndex)
            {
                display(right(index));
                int level = getLevel(index);
                Console.WriteLine();
                for (int i = 0; i < level + 1; i++)
                {
                    Console.Write("\t");
                }
                Console.Write(HeapArray[index]);
                Console.WriteLine();
                display(left(index));
            }
        }

        private int getLevel(int index)
        {
            int level = 0;
            while (index >= 0)
            {
                int numElementsInLevel = pow(2, level);
                if (index < numElementsInLevel)
                {
                    return level;
                }
                else
                {
                    index -= numElementsInLevel;
                }
                level++;
            }

            Console.WriteLine("20.20 Unexpected block");
            return int.MinValue;
        }

        private int pow(int baseNum, int index)
        {
            if(index == 0)
            {
                return 1;
            }
            else
            {
                return baseNum * pow(baseNum, index - 1);
            }
        }

        internal bool Delete(int valueToDelete)
        {
            int indexToDelete = Search(valueToDelete, 0);
            if (indexToDelete == -1)
            {
                return false;

            }
            else
            {
                deleteKey(indexToDelete);
                return true;
            }
        }
    }
}
