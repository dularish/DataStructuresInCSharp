using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queues
{
    public class DoublyLinkedListGenericPriorityQueue<T> : GenericQueue<T>
    {
        Node Head;
        Node Tail;
        public class Node
        {
            public T value;
            public int PriorityValue;
            public Node Next;
            public Node Prev;

            public Node(T val, int priority)
            {
                value = val;
                PriorityValue = priority;
                Prev = null;
                Next = null;
            }
        }

        public DoublyLinkedListGenericPriorityQueue()
        {
            Head = null;
            Tail = null;
        }
        public Node pop()
        {
            if (Head != null)
            {
                Node value = Head;
                if (Head.Next != null)
                {
                    Head.Next.Prev = null;
                }
                else
                {
                    Tail = null;
                }
                Head = Head.Next;
                return value;
            }
            else
            {
                return null;
            }
        }

        public Node peek()
        {
            return Head;
        }

        public void push(T a, int priority)
        {
            if (Head == null && Tail == null)
            {
                Node newNode = new Node(a, priority);
                Head = Tail = newNode;
            }
            else
            {
                Node newNode = new Node(a, priority);

                if (Head.PriorityValue > priority)
                {
                    newNode.Next = Head;
                    Head.Prev = newNode;
                    Head = newNode;
                    return;
                }

                Node currentNode = Head;
                while (currentNode != null)
                {
                    if (currentNode.PriorityValue <= priority && (currentNode.Next == null || currentNode.Next.PriorityValue > priority))
                    {
                        newNode.Prev = currentNode;
                        newNode.Next = currentNode.Next;
                        if (currentNode.Next != null)
                        {
                            currentNode.Next.Prev = newNode;
                        }
                        currentNode.Next = newNode;
                        break;
                    }
                    currentNode = currentNode.Next;
                }
            }
        }

        public void display()
        {
            Node current = Head;
            int i = 0;
            while (current != null)
            {
                Console.WriteLine("Element at Index " + i + " : " + current.value + " Priority Value : " + current.PriorityValue);
                current = current.Next;
            }
        }
    }
}
