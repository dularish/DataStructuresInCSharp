using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stacks
{
    public class ArrayStack<T> : CustomStack<T>
    {
        T[] stackArray;

        int top;

        public ArrayStack(int size)
        {
            stackArray = new T[size];
            top = -1;
        }

        public int getCountOfStackElements()
        {
            return (top + 1);
        }

        public void push(T elementToPush)
        {
            if (top == stackArray.Length - 1)
            {
                Console.WriteLine("Stack Overflow");
            }
            else
            {
                stackArray[++top] = elementToPush;
            }
            
        }

        public object peek()
        {
            if (top != -1)
            {
                return stackArray[top];
            }
            else
            {
                return null;
            }
        }

        public object pop()
        {
            if (top != -1)
            {
                T value = stackArray[top];
                top--;
                return value;
            }
            else
            {
                return null;
            }
            
        }

        public void display()
        {
            for (int i = top; i >= 0; i--)
            {
                Console.WriteLine("Element - " + i + " Value : " + stackArray[i]);
            }

            if (top == -1)
            {
                Console.WriteLine("No elements to display");
            }
        }

    }
}
