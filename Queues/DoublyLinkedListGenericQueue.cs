using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queues
{
    public class DoublyLinkedListGenericQueue<T> : GenericQueue<T>
    {
        Node Head;
        Node Tail;
        public class Node
        {
            public T value;
            public Node Next;
            public Node Prev;

            public Node(T val)
            {
                value = val;
                Prev = null;
                Next = null;
            }
        }

        public DoublyLinkedListGenericQueue()
        {
            Head = null;
            Tail = null;
        }
        public object pop()
        {
            if (Head != null)
            {
                T value = Head.value;
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

        public object peek()
        {
            if (Head != null)
            {
                return Head.value;
            }
            else
            {
                return Head;
            }
            
        }

        public void push(T a)
        {
            if (Head == null && Tail == null)
            {
                Node newNode = new Node(a);
                Head = Tail = newNode;
            }
            else
            {
                Node newNode = new Node(a);
                Tail.Next = newNode;
                newNode.Prev = Tail;
                Tail = newNode;
            }
        }

        public void display()
        {
            Node current = Head;
            int i = 0;
            while (current != null)
            {
                Console.WriteLine("Element at Index " + i + " : " + current.value);
                current = current.Next;
            }
        }
    }
}
