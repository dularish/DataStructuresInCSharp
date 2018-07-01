using Arrays;
using System;

namespace ArrayAlgorithms
{
    public class Program
    {
        public static int[] arrayToSortRandom = GenerateRandomNumbers(100);
        static void Main(string[] args)
        {
            int choice;
            do
            {
                Console.Clear();
                Console.WriteLine("1.Demo InsertionSort\n2.Demo SelectionSort\n3.MergeSort\n4.SortWithoutLoop\n5.HeapSort\n6.QuickSort\n7.CountingSort\n8.RadixSort\n9.BucketSort\n10.Exit\nPlease enter a choice");
                choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        InsertionSort();
                        break;
                    case 2:
                        SelectionSort();
                        break;
                    case 3:
                        MergeSort();
                        break;
                    case 4:
                        SortWithoutLoop();
                        break;
                    case 5:
                        HeapSort();
                        break;
                    case 6:
                        QuickSort();
                        break;
                    case 7:
                        CountingSort();
                        break;
                    case 8:
                        RadixSort();
                        break;
                    case 9:
                        BucketSort();
                        break;
                    default:
                        Console.WriteLine("Please enter a valid choice");
                        Console.ReadKey();
                        break;
                }
            } while (choice != 10);
            MergeSort();

            Console.ReadKey();
        }

        private static void BucketSort()
        {
            int n = 1000;
            arrayToSortRandom = GenerateRandomNumbers(100, n);
            Console.WriteLine("Array before sorting :");
            for (int i = 0; i < arrayToSortRandom.Length; i++)
            {
                Console.WriteLine(arrayToSortRandom[i]);
            }
            int[] sortedArray = BucketSort(arrayToSortRandom,n);
            Console.WriteLine("Output after sorting :");
            for (int i = 0; i < sortedArray.Length; i++)
            {
                Console.WriteLine(sortedArray[i]);
            }
            Console.ReadKey();
        }

        private static int[] BucketSort(int[] arrayToSortRandom, int maxNumInArray)
        {
            CustomLinkedList<int>[] linkedListArray = new CustomLinkedList<int>[arrayToSortRandom.Length];
            int[] targetArray = new int[arrayToSortRandom.Length];

            for (int i = 0; i < arrayToSortRandom.Length; i++)
            {
                linkedListArray[i] = new CustomLinkedList<int>();
            }
            //Putting in buckets
            for (int i = 0; i < arrayToSortRandom.Length; i++)
            {
                linkedListArray[arrayToSortRandom[i] / ((maxNumInArray + 1) / arrayToSortRandom.Length)].Insert(arrayToSortRandom[i]);
            }

            //Insertion sort each bucket
            for (int i = 0; i < linkedListArray.Length; i++)
            {
                InsertionSortBucket(ref linkedListArray[i]);
            }

            int runningIndexForTargetArray = 0;
            //Putting data from each bucket in order to the target array
            for (int i = 0; i < linkedListArray.Length; i++)
            {
                CustomLinkedList<int>.Node current = linkedListArray[i].Root;
                while (current != null)
                {
                    targetArray[runningIndexForTargetArray] = current.Data;
                    current = current.Next;
                    runningIndexForTargetArray++;
                }
            }

            return targetArray;
        }

        private static void InsertionSortBucket(ref CustomLinkedList<int> linkedList)
        {
            CustomLinkedList<int>.Node current = linkedList.Root;

            while (current != null)
            {
                CustomLinkedList<int>.Node reverseCurrent = current;
                while (reverseCurrent.Prev != null && reverseCurrent.Data < reverseCurrent.Prev.Data)
                {
                    int tempData = reverseCurrent.Data;
                    reverseCurrent.Data = reverseCurrent.Prev.Data;
                    reverseCurrent.Prev.Data = tempData;

                    reverseCurrent = reverseCurrent.Prev;
                }
                current = current.Next;
            }
        }

        private static void RadixSort()
        {
            int n = 1000;
            arrayToSortRandom = GenerateRandomNumbers(100, n);
            Console.WriteLine("Array before sorting :");
            for (int i = 0; i < arrayToSortRandom.Length; i++)
            {
                Console.WriteLine(arrayToSortRandom[i]);
            }
            int maxNumCopy = n;
            int digits = 0;
            while (maxNumCopy != 0)
            {
                maxNumCopy /= 10;
                digits++;
            }
            digits = digits == 0 ? 1 : digits;
            for (int i = 0; i < digits; i++)
            {
                CountSortBasedOnPosition(ref arrayToSortRandom, i);
            }
            Console.WriteLine("Output after sorting :");
            for (int i = 0; i < arrayToSortRandom.Length; i++)
            {
                Console.WriteLine(arrayToSortRandom[i]);
            }
            Console.ReadKey();
        }

        private static void CountingSort()
        {
            int n = 1000;
            arrayToSortRandom = GenerateRandomNumbers(100,n);
            Console.WriteLine("Array before sorting :");
            for (int i = 0; i < arrayToSortRandom.Length; i++)
            {
                Console.WriteLine(arrayToSortRandom[i]);
            }
            int[] sortedArray = CountingSort(arrayToSortRandom,n);
            Console.WriteLine("Output after sorting :");
            for (int i = 0; i < sortedArray.Length; i++)
            {
                Console.WriteLine(sortedArray[i]);
            }
            Console.ReadKey();
        }

        private static void CountSortBasedOnPosition(ref int[] arrayToSort, int pos)
        {
            int decimalDigits = 10;
            int[] targetArray = new int[arrayToSort.Length];
            int[] countArray = new int[decimalDigits];
            for (int i = 0; i < arrayToSortRandom.Length; i++)
            {
                int digitAtPos = GetDigitAtPos(arrayToSort[i], pos);
                countArray[digitAtPos]++;
            }

            for (int i = 1; i < decimalDigits; i++)
            {
                countArray[i] += countArray[i - 1];
            }

            for (int i = arrayToSortRandom.Length - 1; i >= 0; i--)
            {
                int digitAtPos = GetDigitAtPos(arrayToSort[i], pos);
                int indexToPut = countArray[digitAtPos]--;
                targetArray[indexToPut - 1] = arrayToSortRandom[i];
            }

            arrayToSort = targetArray;
        }

        private static int GetDigitAtPos(int number, int pos)
        {
            // 0 pos means one's position, 1 pos means ten's position
            for (int i = 0; i < pos; i++)
            {
                number /= 10;
            }

            return number % 10;
        }

        private static int[] CountingSort(int[] arrayToSortRandom, int maxNumInArray)
        {
            maxNumInArray++;
            int[] targetArray = new int[arrayToSortRandom.Length];
            int[] countArray = new int[maxNumInArray];
            for (int i = 0; i < arrayToSortRandom.Length; i++)
            {
                countArray[arrayToSortRandom[i]]++;
            }

            for (int i = 1; i < maxNumInArray; i++)
            {
                countArray[i] += countArray[i - 1];
            }

            for (int i = arrayToSortRandom.Length - 1; i >= 0; i--)
            {
                int indexToPut = countArray[arrayToSortRandom[i]]--;
                targetArray[indexToPut - 1] = arrayToSortRandom[i];
            }

            return targetArray;
        }

        private static void HeapSort()
        {
            Console.WriteLine("Array before sorting :");
            for (int i = 0; i < arrayToSortRandom.Length; i++)
            {
                Console.WriteLine(arrayToSortRandom[i]);
            }

            MaxHeap maxHeap = new MaxHeap(arrayToSortRandom);
            int[] sortedArray = maxHeap.HeapSort();
            Console.WriteLine("Output after sorting :");
            for (int i = 0; i < arrayToSortRandom.Length; i++)
            {
                Console.WriteLine(arrayToSortRandom[i]);
            }
            Console.ReadKey();
            
        }

        private static void QuickSort()
        {
            Console.WriteLine("Array before sorting :");
            for (int i = 0; i < arrayToSortRandom.Length; i++)
            {
                Console.WriteLine(arrayToSortRandom[i]);
            }

            QuickSort(ref arrayToSortRandom, 0, arrayToSortRandom.Length - 1);
            Console.WriteLine("Output after sorting :");
            for (int i = 0; i < arrayToSortRandom.Length; i++)
            {
                Console.WriteLine(arrayToSortRandom[i]);
            }
            Console.ReadKey();

        }

        private static void QuickSort(ref int[] arrayToSortRandom, int startIndex, int endIndex)
        {
            if (startIndex < endIndex)
            {
                int partitionedIndex = Partition(ref arrayToSortRandom, startIndex, endIndex);
                QuickSort(ref arrayToSortRandom, startIndex, partitionedIndex - 1);
                QuickSort(ref arrayToSortRandom, partitionedIndex + 1, endIndex);
            }
        }

        public static int Partition(ref int[] arrayToSortRandom, int startIndex, int endIndex)
        {
            int pivotElement = endIndex;//assigning for readability
            int partition = startIndex;// this partition index would move on progress

            for (int j = partition; j < pivotElement; j++)//Not considering pivot element for looping
            {
                //Always maintaining that all the values below partition position has values less than pivotelement value
                if (arrayToSortRandom[j] < arrayToSortRandom[pivotElement])
                {
                    int temp = arrayToSortRandom[partition];
                    arrayToSortRandom[partition] = arrayToSortRandom[j];
                    arrayToSortRandom[j] = temp;
                    partition++;
                }
            }
            //shifting pivot element's position to the partition
            int temp2 = arrayToSortRandom[partition];
            arrayToSortRandom[partition] = arrayToSortRandom[pivotElement];
            arrayToSortRandom[pivotElement] = temp2;
            partition++;

            return partition - 1;
        }

        private static void SortWithoutLoop()
        {
            Console.WriteLine("Array before sorting :");
            for (int i = 0; i < arrayToSortRandom.Length; i++)
            {
                Console.WriteLine(arrayToSortRandom[i]);
            }

            SortWithoutLoop(ref arrayToSortRandom, 0, 1,0); //sortMergeTwoArrays(arrayToSortRandomA,0,(arrayToSortRandomA.Length - 1)/2, arrayToSortRandomA.Length - 1);
            Console.WriteLine("Output after sorting :");
            for (int i = 0; i < arrayToSortRandom.Length; i++)
            {
                Console.WriteLine(arrayToSortRandom[i]);
            }
            Console.ReadKey();
        }

        private static void SortWithoutLoop(ref int[] arrayToSortRandom, int indexForComparison, int currentIndex, int minNIndex)
        {
            if (!(indexForComparison == arrayToSortRandom.Length))
            {
                if (currentIndex == arrayToSortRandom.Length)
                {
                    int temp = arrayToSortRandom[minNIndex];
                    arrayToSortRandom[minNIndex] = arrayToSortRandom[indexForComparison];
                    arrayToSortRandom[indexForComparison] = temp;

                    SortWithoutLoop(ref arrayToSortRandom, ++indexForComparison, indexForComparison + 1, indexForComparison);
                }
                else 
                {
                    if (arrayToSortRandom[currentIndex] < arrayToSortRandom[minNIndex])
                    {
                        minNIndex = currentIndex;
                    }
                    SortWithoutLoop(ref arrayToSortRandom, indexForComparison, currentIndex + 1, minNIndex);
                }
                
            }
        }

        private static void MergeSort()
        {
            Console.WriteLine("Array before sorting :");
            for (int i = 0; i < arrayToSortRandom.Length; i++)
            {
                Console.WriteLine(arrayToSortRandom[i]);
            }

            MergeSort(ref arrayToSortRandom, 0, arrayToSortRandom.Length - 1); //sortMergeTwoArrays(arrayToSortRandomA,0,(arrayToSortRandomA.Length - 1)/2, arrayToSortRandomA.Length - 1);
            Console.WriteLine("Output after sorting :");
            for (int i = 0; i < arrayToSortRandom.Length; i++)
            {
                Console.WriteLine(arrayToSortRandom[i]);
            }
        }

        private static void InsertionSort()
        {
            Console.WriteLine("Array before sorting :");
            for (int i = 0; i < arrayToSortRandom.Length; i++)
            {
                Console.WriteLine(arrayToSortRandom[i]);
            }
            SortByInsertionSort(ref arrayToSortRandom);
            Console.WriteLine("Array after sorting :");
            for (int i = 0; i < arrayToSortRandom.Length; i++)
            {
                Console.WriteLine(arrayToSortRandom[i]);
            }
        }
        private static void SelectionSort()
        {
            Console.WriteLine("Array before sorting :");
            for (int i = 0; i < arrayToSortRandom.Length; i++)
            {
                Console.WriteLine(arrayToSortRandom[i]);
            }
            SortBySelectionSort(ref arrayToSortRandom);
            Console.WriteLine("Array after sorting :");
            for (int i = 0; i < arrayToSortRandom.Length; i++)
            {
                Console.WriteLine(arrayToSortRandom[i]);
            }
        }


        private static void SortByInsertionSort(ref int[] arrayToSortRandom)
        {
            for (int j = 1; j < arrayToSortRandom.Length; j++)//n
            {
                int key = arrayToSortRandom[j];
                int i = j - 1;//n
                while((i >= 0) && ( arrayToSortRandom[i] > key))
                {
                    arrayToSortRandom[i + 1] = arrayToSortRandom[i];
                    i--;
                }
                arrayToSortRandom[i + 1] = key;
            }
        }

        private static void SortBySelectionSort(ref int[] arrayToSortRandom)
        {
            for (int i = 0; i < arrayToSortRandom.Length; i++)
            {
                int leastNumberIndex = i,temp;//n
                for (int j = i+1; j < arrayToSortRandom.Length; j++)
                {
                    if (arrayToSortRandom[leastNumberIndex] > arrayToSortRandom[j])//~n*n
                    {
                        leastNumberIndex = j;//~n*n
                    }
                }
                temp = arrayToSortRandom[leastNumberIndex];//n
                arrayToSortRandom[leastNumberIndex] = arrayToSortRandom[i];//n
                arrayToSortRandom[i] = temp;//n
            }
        }

        public static void MergeSort(ref int[] array, int p, int r)
        {
            if (p < r)
            {
                int q = (r - p) / 2 + p;
                MergeSort(ref array, p, q);
                MergeSort(ref array, q + 1, r);
                sortMergeTwoArrays(ref array, p, q, r);
            }
        }

        public static int[] sortMergeTwoArrays(ref int[] array, int p, int q, int r)
        {
            if (p < r)
            {
                //Assuming in case of odd number of entries, the majority sorted array would be in the start, adding one each to store Int.Max;
                int n1 = q - p + 2;
                int n2 = r - q + 1;
                int[] firstArray = new int[n1];
                int[] secondArray = new int[n2];
                for (int i = 0; i < (n1 -1); i++)
                {
                    firstArray[i] = array[p + i];
                }
                firstArray[n1 - 1] = int.MaxValue;
                for (int i = 0; i < (n2-1); i++)
                {
                    secondArray[i] = array[q + 1 + i];
                }
                secondArray[n2 - 1] = int.MaxValue;
                int s = 0;
                int t = 0;
                int k = p;
                while (k <= r)
                {
                    if (firstArray[s] < secondArray[t])
                    {
                        array[k] = firstArray[s];
                        s++;
                    }
                    else
                    {
                        array[k] = secondArray[t];
                        t++;
                    }
                    k++;
                }
                return array;
            }
            else
            {
                return array;
            }
        }

        public static int[] GenerateRandomNumbers(int size, int maxNum = 1000)
        {
            int[] arrayToReturn = new int[size];
            Random random = new Random();
            for (int i = 0; i < size; i++)
			{
			    arrayToReturn[i] = random.Next(maxNum);
			}

            return arrayToReturn;
        }
    }
}
