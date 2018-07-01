using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arrays
{
    public class MaxHeap
    {
        public int[] HeapArray;

        public int maxSize;
        public int bottomIndex;

        public MaxHeap(int size)
        {
            HeapArray = new int[size];
            maxSize = size;
        }

        public MaxHeap(int[] arrayInput)
        {
            HeapArray = arrayInput;
            maxSize = arrayInput.Length;
            bottomIndex = maxSize;
            BuildMaxHeap(ref HeapArray);
        }

        private void BuildMaxHeap(ref int[] HeapArray)
        {
            for (int i = HeapArray.Length; i >= 0; i--)
            {
                MaxHeapify(i);
            }
        }

        public int[] HeapSort()
        {
            int[] arrayCopied = HeapArray;
            int bottomIndexCopy = bottomIndex;
            for (int i = bottomIndex - 1; i >= 1; i--)
            {
                int largest = HeapArray[0];
                HeapArray[0] = HeapArray[bottomIndex - 1];
                HeapArray[bottomIndex - 1] = largest;
                bottomIndex--;
                MaxHeapify(0);
            }
            int[] arrayToReturn = HeapArray;
            bottomIndex = bottomIndexCopy;
            HeapArray = arrayCopied;
            return arrayToReturn;
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
            if (HeapArray[parentt] < HeapArray[travIndex])
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
                else if (HeapArray[index] > num)
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
            increaseKeyValue(i, int.MaxValue);
            extractMax();
        }

        private void increaseKeyValue(int index, int new_value)
        {
            HeapArray[index] = new_value;

            while (index != 0 && HeapArray[parent(index)] < HeapArray[index])
            {
                int temp = HeapArray[index];
                HeapArray[index] = HeapArray[parent(index)];
                HeapArray[parent(index)] = temp;

                index = parent(index);
            }
        }

        public int extractMax()
        {
            if (bottomIndex == 0)
            {
                return int.MinValue;
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
                    MaxHeapify(0);
                    return max;
                }
            }
        }

        private void MaxHeapify(int index)
        {
            int MaxIndex = index;

            if ((left(index)) < bottomIndex && HeapArray[left(index)] > HeapArray[MaxIndex])
            {
                MaxIndex = left(index);
            }
            if (right(index) < bottomIndex && HeapArray[right(index)] > HeapArray[MaxIndex])
            {
                MaxIndex = right(index);
            }

            if (MaxIndex != index)
            {
                int temp = HeapArray[index];
                HeapArray[index] = HeapArray[MaxIndex];
                HeapArray[MaxIndex] = temp;

                MaxHeapify(MaxIndex);
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

            Console.WriteLine("20.10 Unexpected block");
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
