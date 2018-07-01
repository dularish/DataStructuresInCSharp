using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrayAlgorithms
{
    public class CustomLinkedList<T>
    {
        public Node Root;

        public CustomLinkedList()
        {
            Root = null;
        }

        public void Insert(T data)
        {
            Node current = Root;

            if (Root == null)
            {
                Root = new Node(data);
            }
            else
            {
                while (current.Next != null)
                {
                    current = current.Next;
                }
                current.Next = new Node(data, current);
            }
        }

        public void Remove(T data)
        {
            Node current = Root;

            if (Root == null)
            {
                throw new Exception("No element to delete");
            }
            else
            {
                if (Root.Data.Equals(data))
                {
                    Root = Root.Next;
                }
                else
                {
                    while (current != null)
                    {
                        if (current.Next != null && current.Next.Data.Equals(data))
                        {
                            break;
                        }
                        current = current.Next;
                    }

                    if (current == null)
                    {
                        throw new Exception("Data to be deleted does not exists");
                    }
                    else
                    {
                        current.Next = current.Next.Next;
                    }
                }
            }
        }

        public class Node
        {
            public T Data;
            public Node Next;

            public Node Prev;

            public Node(T data, Node prevNode)
            {
                Data = data;
                Next = null;
                Prev = prevNode;
            }
            public Node(T data)
            {
                Data = data;
                Next = null;
                Prev = null;
            }
        }
    }
}
