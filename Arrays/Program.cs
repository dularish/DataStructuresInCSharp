using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arrays
{
    class Program
    {
        static void Main(string[] args)
        {
            int choice;
            do
            {
                Console.Clear();
                Console.WriteLine("1.Demo Max Heap\n2.Demo Min Heap\n6.Exit\nPlease enter a choice");
                choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Enter the size of the heap :");
                        int size_maxHeap = Convert.ToInt32(Console.ReadLine());
                        SimpleMaxHeapArrayDemo(size_maxHeap);
                        break;
                    case 2:
                        Console.WriteLine("Enter the size of the heap :");
                        int size_minHeap = Convert.ToInt32(Console.ReadLine());
                        SimpleMinHeapArrayDemo(size_minHeap);
                        break;
                    case 3:
                        //RedBlackTreeDemo();
                        break;
                    case 4:
                        //SplayTreeDemo();
                        break;
                    case 5:
                        //HuffManCodingDemo();
                        break;
                    case 6:
                        break;
                    default:
                        Console.WriteLine("Please enter a valid choice");
                        Console.ReadKey();
                        break;
                }
            } while (choice != 6);
        }

        private static void SimpleMaxHeapArrayDemo(int size)
        {
            MaxHeap heap = new MaxHeap(size);
            int choice = 1;
            do
            {
                Console.Clear();
                Console.WriteLine("1.Display the Array\n2.Insert a node\n3.Delete a node\n4.Search a node\n11.Exit\nPlease enter a choice");
                choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        heap.display(0);
                        Console.ReadKey();
                        break;
                    case 2:
                        Console.WriteLine("Enter the element to add\n");
                        int value = Convert.ToInt32(Console.ReadLine());
                        if (heap.Search(value, 0) == -1)
                        {
                            heap.Insert(value);
                            Console.WriteLine("Added successfully");
                        }
                        else
                        {
                            Console.WriteLine("Could not add the element");
                        }
                        Console.ReadKey();
                        break;
                    case 3:
                        Console.WriteLine("Enter the element to delete\n");
                        int valueToDelete = Convert.ToInt32(Console.ReadLine());
                        bool delResult = heap.Delete(valueToDelete);
                        if (delResult)
                        {
                            Console.WriteLine("Deleted successfully");
                        }
                        else
                        {
                            Console.WriteLine("Could not delete the element");
                        }
                        Console.ReadKey();
                        break;
                    case 4:
                        Console.WriteLine("Enter the element to search\n");
                        int valueToSearch = Convert.ToInt32(Console.ReadLine());
                        int searchResult = heap.Search(valueToSearch, 0);
                        if (searchResult != -1)
                        {
                            Console.WriteLine("The element exists");
                        }
                        else
                        {
                            Console.WriteLine("The element doesn't exists");
                        }
                        Console.ReadKey();
                        break;
                    //case 5:
                    //    int numberOfElements = tree.FindNumberOfElements(ref tree.Root);
                    //    Console.WriteLine("The total number of elements in the tree is " + numberOfElements);
                    //    Console.ReadKey();
                    //    break;
                    //case 6:
                    //    int heightOfTree = tree.FindHeightOfTree(ref tree.Root);
                    //    Console.WriteLine("The total height of the tree is " + heightOfTree);
                    //    Console.ReadKey();
                    //    break;
                    //case 7:
                    //    tree.InOrderTraverse(tree.Root);
                    //    Console.ReadKey();
                    //    break;
                    //case 8:
                    //    tree.PreOrderTraverse(tree.Root);
                    //    Console.ReadKey();
                    //    break;
                    //case 9:
                    //    tree.PostOrderTraverse(tree.Root);
                    //    Console.ReadKey();
                    //    break;
                    //case 10:
                    //    tree.LevelOrderTraverse(tree.Root);
                    //    Console.ReadKey();
                    //    break;
                    case 11:
                        break;
                    default:
                        Console.WriteLine("Please enter a valid choice");
                        Console.ReadKey();
                        break;
                }
            } while (choice != 11);
        }

        private static void SimpleMinHeapArrayDemo(int size)
        {
            MinHeap heap = new MinHeap(size);
            int choice = 1;
            do
            {
                Console.Clear();
                Console.WriteLine("1.Display the Array\n2.Insert a node\n3.Delete a node\n4.Search a node\n11.Exit\nPlease enter a choice");
                choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        heap.display(0);
                        Console.ReadKey();
                        break;
                    case 2:
                        Console.WriteLine("Enter the element to add\n");
                        int value = Convert.ToInt32(Console.ReadLine());
                        if (heap.Search(value, 0) == -1)
                        {
                            heap.Insert(value);
                            Console.WriteLine("Added successfully");
                        }
                        else
                        {
                            Console.WriteLine("Could not add the element");
                        }
                        Console.ReadKey();
                        break;
                    case 3:
                        Console.WriteLine("Enter the element to delete\n");
                        int valueToDelete = Convert.ToInt32(Console.ReadLine());
                        bool delResult = heap.Delete(valueToDelete);
                        if (delResult)
                        {
                            Console.WriteLine("Deleted successfully");
                        }
                        else
                        {
                            Console.WriteLine("Could not delete the element");
                        }
                        Console.ReadKey();
                        break;
                    case 4:
                        Console.WriteLine("Enter the element to search\n");
                        int valueToSearch = Convert.ToInt32(Console.ReadLine());
                        int searchResult = heap.Search(valueToSearch, 0);
                        if (searchResult != -1)
                        {
                            Console.WriteLine("The element exists");
                        }
                        else
                        {
                            Console.WriteLine("The element doesn't exists");
                        }
                        Console.ReadKey();
                        break;
                    //case 5:
                    //    int numberOfElements = tree.FindNumberOfElements(ref tree.Root);
                    //    Console.WriteLine("The total number of elements in the tree is " + numberOfElements);
                    //    Console.ReadKey();
                    //    break;
                    //case 6:
                    //    int heightOfTree = tree.FindHeightOfTree(ref tree.Root);
                    //    Console.WriteLine("The total height of the tree is " + heightOfTree);
                    //    Console.ReadKey();
                    //    break;
                    //case 7:
                    //    tree.InOrderTraverse(tree.Root);
                    //    Console.ReadKey();
                    //    break;
                    //case 8:
                    //    tree.PreOrderTraverse(tree.Root);
                    //    Console.ReadKey();
                    //    break;
                    //case 9:
                    //    tree.PostOrderTraverse(tree.Root);
                    //    Console.ReadKey();
                    //    break;
                    //case 10:
                    //    tree.LevelOrderTraverse(tree.Root);
                    //    Console.ReadKey();
                    //    break;
                    case 11:
                        break;
                    default:
                        Console.WriteLine("Please enter a valid choice");
                        Console.ReadKey();
                        break;
                }
            } while (choice != 11);
        }
    }
}
