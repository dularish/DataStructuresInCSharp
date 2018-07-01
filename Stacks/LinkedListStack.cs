using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stacks
{
    public class LinkedListStack : CustomStack<int>
    {
        Node Head;
        Node Top;
        public class Node
        {
            public int value;
            public Node Next;

            public Node(int val)
            {
                value = val;
                Next = null;
            }
        }

        public LinkedListStack()
        {
            Head = null;
            Top = null;
        }


        public void push(int value)
        {
            if (Head == null)
            {
                Head = new Node(value);
                Top = Head;
            }
            else
            {
                Node newNode = new Node(value);
                Top.Next = newNode;
                Top = newNode;
            }
        }

        public object pop()
        {
            if (Top == null)
            {
                return null;
            }
            if (Top == Head)
            {
                int headvalue = Head.value;
                Top = null;
                Head = null;
                return headvalue;
            }
            int value = Top.value;
            Node current = Head;
            while (current != null)
            {
                if (current.Next == Top)
                {
                    current.Next = null;
                    Top = current;

                    return value;
                }

                current = current.Next;
            }


            return value;
        }

        public object peek()
        {
            if (Top == null)
            {
                return null;
            }
            int value = Top.value;
            return value;
        }

        public void display()
        {
            int count = 0;
            Node current = Head;
            if (current == null)
            {
                Console.WriteLine("No elements exist in the list");
            }
            while (current != null)
            {
                Console.WriteLine("Element at index " + count++ + " : " + current.value);
                current = current.Next;
            }
        }
    }
}
