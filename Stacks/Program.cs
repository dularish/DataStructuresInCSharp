using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stacks
{
    class Program
    {
        static void Main(string[] args)
        {
            int choice;
            do
            {
                Console.Clear();
                Console.WriteLine("1.Demo ArrayStack\n2.Demo LinkedListStack\n3.ValidateParanthesis\n4.Demo GenericDoublyLinkedListStack\n5.TransformInfixExpressionToPostFixExpression\n6.Tower of Hanoi\n7.Exit\nPlease enter a choice");
                choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        ArrayStackDemo();
                        break;
                    case 2:
                        LinkedListStackDemo();
                        break;
                    case 3:
                        Console.WriteLine("Enter the algebraic expression to validate");
                        string input = Console.ReadLine();
                        Console.WriteLine("Validation result : " + validateParanthesis(input));
                        Console.ReadKey();
                        break;
                    case 4:
                        GenericDoublyLinkedListStackDemo();
                        break;
                    case 5:
                        Console.WriteLine("Enter the expression to convert to PostFix");
                        string inputstring = Console.ReadLine();
                        Console.WriteLine(getPostFixExpressionForInfix(inputstring));
                        Console.ReadKey();
                        break;
                    case 6:
                        TowerOfHanoiDemo();
                        break;
                    case 7:
                        break;
                    default:
                        Console.WriteLine("Please enter a valid choice");
                        Console.ReadKey();
                        break;
                }
            } while (choice != 7);
        }

        private static void TowerOfHanoiDemo()
        {
            Console.WriteLine("Enter the number of rings to shift");
            int ringsNum = Convert.ToInt32(Console.ReadLine());
            int totalRings = ringsNum;
            DoublyLinkedListGenericStack<int> stackA = new DoublyLinkedListGenericStack<int>();
            DoublyLinkedListGenericStack<int> stackB = new DoublyLinkedListGenericStack<int>();
            DoublyLinkedListGenericStack<int> stackC = new DoublyLinkedListGenericStack<int>();
            while (ringsNum != 0)
            {
                stackA.push(ringsNum--);
            }
            stackA.display();

            SolveTowerOfHanoiRecursively(totalRings,ref stackA, ref stackC, ref stackB);
            Console.WriteLine("Displaying Source Stack :");
            stackA.display();
            Console.WriteLine("Displaying Destination Stack :");
            stackC.display();
            Console.WriteLine("Displaying Spare Stack :");
            stackB.display();
            Console.ReadKey();
        }

        private static void SolveTowerOfHanoiRecursively(int n, ref DoublyLinkedListGenericStack<int> sourceStack,ref DoublyLinkedListGenericStack<int> destinationStack,ref DoublyLinkedListGenericStack<int> spareStack)
        {
            if (sourceStack.Top != null)
            {
                if (/*sourceStack.Top == sourceStack.Head && sourceStack.Top != null &&*/ n == 1)
                {
                    int value = (int)sourceStack.pop();
                    destinationStack.push(value);
                }
                else
                {
                    //while (sourceStack.Top != sourceStack.Head)
                    //{
                        SolveTowerOfHanoiRecursively(n-1 , ref sourceStack, ref spareStack, ref destinationStack);
                    //}
                    //while (sourceStack.Top != null)
                    //{
                        SolveTowerOfHanoiRecursively(1, ref sourceStack, ref destinationStack, ref spareStack);
                    //}
                    //while (spareStack.Top != null)
                    //{
                        SolveTowerOfHanoiRecursively(n -1 , ref spareStack, ref destinationStack, ref sourceStack);
                    //}
                }
            }
        }

        private static string getPostFixExpressionForInfix(string inputString)
        {
            DoublyLinkedListGenericStack<char> operatorStack = new DoublyLinkedListGenericStack<char>();
            string outputString = "";
            for (int i = 0; i < inputString.Length; i++)
            {
                if (inputString[i] == '+' || inputString[i] == '-' || inputString[i] == '*' || inputString[i] == '/' || inputString[i] == '^' || inputString[i] == '(' || inputString[i] == '{' || inputString[i] == '[')
                {
                    operatorStack.push(inputString[i]);
                }
                if ((inputString[i] >= 65 && inputString[i] <= 90) || (inputString[i] >= 97 && inputString[i] <= 122))
                {
                    outputString += inputString[i];
                }
                if (inputString[i] == ')' || inputString[i] == '}' || inputString[i] == ']')
                {
                    while (operatorStack.peek() != null && (char)operatorStack.peek() != '(' && (char)operatorStack.peek() != '{' && (char)operatorStack.peek() != '[')
                    {
                        outputString += operatorStack.pop();
                    }
                    operatorStack.pop();
                }
            }
            return outputString;
        }

        private static bool validateParanthesis(string inputExpression)
        {
            ArrayStack<char> stack = new ArrayStack<char>(inputExpression.Length);

            for (int i = 0; i < inputExpression.Length; i++)
            {
                if (inputExpression[i] == '(' || inputExpression[i] == '{' || inputExpression[i] == '[')
                {
                    stack.push(inputExpression[i]);
                }
                if (inputExpression[i] == ')' )
                {
                    if (((char)(stack.peek() == null ? ' ' : stack.peek())) == '(')
                    {
                        stack.pop();
                    }
                    else
                    {
                        return false;
                    }
                }
                if (inputExpression[i] == '}')
                {
                    if (((char)(stack.peek() == null ? ' ' : stack.peek())) == '{')
                    {
                        stack.pop();
                    }
                    else
                    {
                        return false;
                    }
                }
                if (inputExpression[i] == ']')
                {
                    if (((char)(stack.peek() == null ? ' ' : stack.peek())) == '[')
                    {
                        stack.pop();
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            if (stack.getCountOfStackElements() == 0)
            {
                return true;
            }
            return false;
        }

        private static void LinkedListStackDemo()
        {
            LinkedListStack linkedListStack = new LinkedListStack();
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
                    default:
                        Console.WriteLine("Please enter a valid choice");
                        Console.ReadKey();
                        break;
                }
            } while (choice != 5);
        }

        private static void GenericDoublyLinkedListStackDemo()
        {
            DoublyLinkedListGenericStack<int> linkedListStack = new DoublyLinkedListGenericStack<int>();
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
                    default:
                        Console.WriteLine("Please enter a valid choice");
                        Console.ReadKey();
                        break;
                }
            } while (choice != 5);
        }

        private static void ArrayStackDemo()
        {
            ArrayStack<int> array = new ArrayStack<int>(50);
            int choice = 1;
            do
            {
                Console.Clear();
                Console.WriteLine("1.Display the array\n2.Push An Element\n3.Pop an element\n4.Peek an element\n5.Exit\nPlease enter a choice");
                choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        array.display();
                        Console.ReadKey();
                        break;
                    case 2:
                        Console.WriteLine("Enter the element to push\n");
                        int value = Convert.ToInt32(Console.ReadLine());
                        array.push(value);
                        Console.WriteLine("Pushed successfully");
                        Console.ReadKey();
                        break;
                    case 3:
                        var poppedvalue = array.pop();
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
                        var peekedvalue = array.peek();
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
                    default:
                        Console.WriteLine("Please enter a valid choice");
                        Console.ReadKey();
                        break;
                }
            } while (choice != 5);
        }
    }
}
