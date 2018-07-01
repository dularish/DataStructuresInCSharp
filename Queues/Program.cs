using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queues
{
    class Program
    {
        static void Main(string[] args)
        {
            int choice;
            do
            {
                Console.Clear();
                Console.WriteLine("1.Demo GenericDoublyLinkedListQueue\n2.Demo GenericDoublyLinkedListPriorityQueue\n3.Exit\nPlease enter a choice");
                choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        GenericDoublyLinkedListQueueDemo();
                        break;
                    case 2:
                        GenericDoublyLinkedListPriorityQueueDemo();
                        break;
                    case 3:
                        break;
                    default:
                        Console.WriteLine("Please enter a valid choice");
                        Console.ReadKey();
                        break;
                }
            } while (choice != 3);
        }

        private static void GenericDoublyLinkedListPriorityQueueDemo()
        {
            DoublyLinkedListGenericPriorityQueue<int> linkedListStack = new DoublyLinkedListGenericPriorityQueue<int>();
            int choice = 1;
            do
            {
                Console.Clear();
                Console.WriteLine("1.Display the array\n2.Push An Element\n3.Pop an element\n4.Peek an element\n5.Exit\nPlease enter a choice");
                choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        linkedListStack.display();
                        Console.ReadKey();
                        break;
                    case 2:
                        Console.WriteLine("Enter the element to push\n");
                        int value = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter the priority of the element to push\n");
                        int priority = Convert.ToInt32(Console.ReadLine());
                        linkedListStack.push(value, priority);
                        Console.WriteLine("Pushed successfully");
                        Console.ReadKey();
                        break;
                    case 3:
                        var poppedvalue = linkedListStack.pop();
                        if (poppedvalue == null)
                        {
                            Console.WriteLine("Stack Underflow");
                        }
                        else
                        {
                            Console.WriteLine("The element popped is " + poppedvalue.value);
                        }
                        Console.ReadKey();
                        break;
                    case 4:
                        var peekedvalue = linkedListStack.peek();
                        if (peekedvalue == null)
                        {
                            Console.WriteLine("Stack Underflow");
                        }
                        else
                        {
                            Console.WriteLine("The element peeked is " + peekedvalue.value);
                        }
                        Console.ReadKey();
                        break;
                    case 5:
                        break;
                    default:
                        Console.WriteLine("Please enter a valid choice");
                        Console.ReadKey();
                        break;
                }
            } while (choice != 5);
        }

        private static void GenericDoublyLinkedListQueueDemo()
        {
            DoublyLinkedListGenericQueue<int> linkedListStack = new DoublyLinkedListGenericQueue<int>();
            int choice = 1;
            do
            {
                Console.Clear();
                Console.WriteLine("1.Display the array\n2.Push An Element\n3.Pop an element\n4.Peek an element\n5.Exit\nPlease enter a choice");
                choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        linkedListStack.display();
                        Console.ReadKey();
                        break;
                    case 2:
                        Console.WriteLine("Enter the element to push\n");
                        int value = Convert.ToInt32(Console.ReadLine());
                        linkedListStack.push(value);
                        Console.WriteLine("Pushed successfully");
                        Console.ReadKey();
                        break;
                    case 3:
                        var poppedvalue = linkedListStack.pop();
                        if (poppedvalue == null)
                        {
                            Console.WriteLine("Stack Underflow");
                        }
                        else
                        {
                            Console.WriteLine("The element popped is " + (int)poppedvalue);
                        }
                        Console.ReadKey();
                        break;
                    case 4:
                        var peekedvalue = linkedListStack.peek();
                        if (peekedvalue == null)
                        {
                            Console.WriteLine("Stack Underflow");
                        }
                        else
                        {
                            Console.WriteLine("The element peeked is " + peekedvalue);
                        }
                        Console.ReadKey();
                        break;
                    case 5:
                        break;
                    default:
                        Console.WriteLine("Please enter a valid choice");
                        Console.ReadKey();
                        break;
                }
            } while (choice != 5);
        }
    }
}
