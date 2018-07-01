using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stacks
{
    public class DoublyLinkedListGenericStack<T> : CustomStack<T>
    {
        public Node Head;
        public Node Top;
        public class Node
        {
            public T value;
            public Node Next;
            public Node Prev;
            public Node(T val)
            {
                value = val;
                Next = null;
                Prev = null;
            }
        }

        public DoublyLinkedListGenericStack()
        {
            Head = null;
            Top = null;
        }


        public void push(T value)
        {
            if (Head == null)
            {
                Head = new Node(value);
                Top = Head;
            }
            else
            {
                Node newNode = new Node(value);
                newNode.Prev = Top;
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
                T headvalue = Head.value;
                Top = null;
                Head = null;
                return headvalue;
            }
            T value = Top.value;
            Node newTop = Top.Prev;
            newTop.Next = null;
            Top = newTop;
            return value;
        }

        public object peek()
        {
            if (Top == null)
            {
                return null;
            }
            T value = Top.value;
            return value;
        }

        public void display()
        {
            int count = getCountOfItems();
            Node current = Top;
            if (current == null)
            {
                Console.WriteLine("No elements exist in the list");
            }
            while (current != null)
            {
                Console.WriteLine("Element at index " + --count + " : " + current.value);
                current = current.Prev;
            }
        }

        private int getCountOfItems()
        {
            int count = 0;
            if (Head == null)
            {
                return 0;
            }
            Node current = Head;
            while (current != null)
            {
                count++;
                current = current.Next;
            }
            return count;
        }
    }
}
