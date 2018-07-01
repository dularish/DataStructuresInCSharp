using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trees
{
    class Program
    {
        static void Main(string[] args)
        {
            int choice = -1;
            do
            {
                Console.Clear();
                Console.WriteLine("1.Demo BinarySearchTree\n2.Demo AVLTree\n3.Demo Red-BlackTree\n4.Demo SplayTree\n5.HuffmanCoding\n6.Demo B-Tree\n7.Trie Demo\n8.Exit\nPlease enter a choice");
                string input = Console.ReadLine().Trim();
                if (string.IsNullOrEmpty(input))
                {
                    continue;
                }
                choice = Convert.ToInt32(input);
                switch (choice)
                {
                    case 1:
                        BinarySearchTreeDemo();
                        break;
                    case 2:
                        AVLTreeDemo();
                        break;
                    case 3:
                        RedBlackTreeDemo();
                        break;
                    case 4:
                        SplayTreeDemo();
                        break;
                    case 5:
                        HuffManCodingDemo();
                        break;
                    case 6:
                        BTreeDemo();
                        break;
                    case 7:
                        TrieDemo();
                        break;
                    case 8:
                        break;
                    default:
                        Console.WriteLine("Please enter a valid choice");
                        Console.ReadKey();
                        break;
                }
            } while (choice != 8);
            
        }

        private static void HuffManCodingDemo()
        {
            Huffman_Coding tree = new Huffman_Coding();
            int choice = 1;
            do
            {
                Console.Clear();
                Console.WriteLine("1.Display the Tree\n2.Generate HuffManCoding\n3.Clear the data\n4.Decode the string\n5.Encode and save the string\n6.Decode the string from a path\n7.Exit\nPlease enter a choice");
                choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        tree.display(tree.Root, 1);
                        Console.ReadKey();
                        break;
                    case 2:
                        Console.WriteLine("Enter the string to Generate HuffmanCoding\n");
                        string value = Console.ReadLine();
                        bool result = tree.GenerateHuffmanCodingTree(value);
                        if (result)
                        {
                            Console.WriteLine("Generated successfully");
                            Console.WriteLine("The Encoded string is " + tree.Encode(value));
                        }
                        else
                        {
                            Console.WriteLine("Could not Generate");
                        }
                        Console.ReadKey();
                        break;
                    case 3:
                        bool delResult = false;
                        if (tree.Root != null || tree.PriorityQueue != null || tree.outputPriorityQueue != null || tree.PriorityQueuePreference != Huffman_Coding.PriorityQueueType.CharacterQueue)
                        {
                            tree.Root = null;
                            tree.PriorityQueue = null;
                            tree.outputPriorityQueue = null;
                            tree.PriorityQueuePreference = Huffman_Coding.PriorityQueueType.CharacterQueue;
                            delResult = true;
                        }
                        if (delResult)
                        {
                            Console.WriteLine("Deleted successfully");
                        }
                        else
                        {
                            Console.WriteLine("Nothing to clear");
                        }
                        Console.ReadKey();
                        break;
                    case 4:
                        Console.WriteLine("Enter the string to Decode\n");
                        string valueToDecode = Console.ReadLine();
                        string decodedString = tree.Decode(valueToDecode);
                        Console.WriteLine(decodedString);
                        Console.ReadKey();
                        break;
                    case 5:
                        Console.WriteLine("Enter the string to Encode\n");
                        string valueToEncode = Console.ReadLine();
                        bool result1 = tree.GenerateHuffmanCodingTree(valueToEncode);
                        if (result1)
                        {
                            string path = "";
                            Console.WriteLine("Enter the directory to save the file");
                            string directory = Console.ReadLine();
                            if (Directory.Exists(directory))
                            {
                                Console.WriteLine("Enter the filename without extension to save :");
                                string filename = Console.ReadLine();
                                tree.SaveEncodedString(valueToEncode, directory + @"\" + filename);
                                Console.WriteLine("Generated and saved successfully");
                            }
                            else
                            {
                                Console.WriteLine("The directory do not exists");
                                Console.ReadKey();
                                break;
                            }
                            
                        }
                        else
                        {
                            Console.WriteLine("Could not Generate");
                        }
                        Console.ReadKey();
                        break;
                    case 6:
                        Console.WriteLine("Enter the file path without extension which you want to decode");
                        string fileNameWithoutExt = Console.ReadLine();
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

        private static void BinarySearchTreeDemo()
        {
            BinarySearchTree tree = new BinarySearchTree();
            int choice = 1;
            do
            {
                Console.Clear();
                Console.WriteLine("1.Display the Tree\n2.Insert a node\n3.Delete a node\n4.Search a node\n5.Find Number of elements\n6.Find Height of the tree\n7.InOrderTraverse" +
                    "\n8.PreOrderTraverse\n9.PostOrderTraverse\n10.LevelOrderTraverse\n11.Exit\nPlease enter a choice");
                choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        tree.display(tree.Root, 1);
                        Console.ReadKey();
                        break;
                    case 2:
                        Console.WriteLine("Enter the element to add\n");
                        int value = Convert.ToInt32(Console.ReadLine());
                        bool result = tree.Insert(ref tree.Root, value);
                        if (result)
                        {
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
                        bool delResult = tree.Delete(ref tree.Root, valueToDelete);
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
                        bool searchResult = tree.Search(ref tree.Root, valueToSearch);
                        if (searchResult)
                        {
                            Console.WriteLine("The element exists");
                        }
                        else
                        {
                            Console.WriteLine("The element doesn't exists");
                        }
                        Console.ReadKey();
                        break;
                    case 5:
                        int numberOfElements = tree.FindNumberOfElements(ref tree.Root);
                        Console.WriteLine("The total number of elements in the tree is " + numberOfElements);
                        Console.ReadKey();
                        break;
                    case 6:
                        int heightOfTree = tree.FindHeightOfTree(ref tree.Root);
                        Console.WriteLine("The total height of the tree is " + heightOfTree);
                        Console.ReadKey();
                        break;
                    case 7:
                        tree.InOrderTraverse(tree.Root);
                        Console.ReadKey();
                        break;
                    case 8:
                        tree.PreOrderTraverse(tree.Root);
                        Console.ReadKey();
                        break;
                    case 9:
                        tree.PostOrderTraverse(tree.Root);
                        Console.ReadKey();
                        break;
                    case 10:
                        tree.LevelOrderTraverse(tree.Root);
                        Console.ReadKey();
                        break;
                    case 11:
                        break;
                    default:
                        Console.WriteLine("Please enter a valid choice");
                        Console.ReadKey();
                        break;
                }
            } while (choice != 11);
        }

        private static void BTreeDemo()
        {
            B_Tree tree = new B_Tree();
            tree.minNumberOfKeysForTree = 3;
            int choice = 1;
            do
            {
                Console.Clear();
                Console.WriteLine("1.Display the Tree\n2.Insert a node\n3.Delete a node\n4.Search a node\n5.Find Number of elements\n6.Find Height of the tree\n7.InOrderTraverse" +
                    "\n8.PreOrderTraverse\n9.PostOrderTraverse\n10.LevelOrderTraverse\n11.Bulk Insert\n12.Exit\nPlease enter a choice");
                string input = Console.ReadLine().Trim();
                if (string.IsNullOrEmpty(input))
                {
                    continue;
                }
                choice = Convert.ToInt32(input);
                switch (choice)
                {
                    case 1:
                        tree.Display(tree.Root, 1);
                        Console.ReadKey();
                        break;
                    case 2:
                        Console.WriteLine("Enter the element to add\n");
                        int value = Convert.ToInt32(Console.ReadLine());
                        bool result = tree.ProactiveInsert(value, tree.Root);
                        if (result)
                        {
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
                        bool delResult = tree.Delete(ref tree.Root, valueToDelete);
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
                        bool searchResult = tree.Search(valueToSearch, tree.Root);
                        if (searchResult)
                        {
                            Console.WriteLine("The element exists");
                        }
                        else
                        {
                            Console.WriteLine("The element doesn't exists");
                        }
                        Console.ReadKey();
                        break;
                    case 5:
                        int numberOfElements = tree.FindNumberOfElements(ref tree.Root);
                        Console.WriteLine("The total number of elements in the tree is " + numberOfElements);
                        Console.ReadKey();
                        break;
                    case 6:
                        int heightOfTree = tree.FindHeightOfTree(ref tree.Root);
                        Console.WriteLine("The total height of the tree is " + heightOfTree);
                        Console.ReadKey();
                        break;
                    case 7:
                        tree.InOrderTraverse(tree.Root);
                        Console.ReadKey();
                        break;
                    case 8:
                        tree.PreOrderTraverse(tree.Root);
                        Console.ReadKey();
                        break;
                    case 9:
                        tree.PostOrderTraverse(tree.Root);
                        Console.ReadKey();
                        break;
                    case 10:
                        tree.LevelOrderTraverse(tree.Root);
                        Console.ReadKey();
                        break;
                    case 11:
                        for (int i = 1; i < 100; i++)
                        {
                            tree.ProactiveInsert(i * 10, tree.Root);
                        }
                        break;
                    case 12:
                        break;
                    default:
                        Console.WriteLine("Please enter a valid choice");
                        Console.ReadKey();
                        break;
                }
            } while (choice != 12);
        }

        private static void AVLTreeDemo()
        {
            AVLTree tree = new AVLTree();
            int choice = 1;
            do
            {
                Console.Clear();
                Console.WriteLine("1.Display the Tree\n2.Insert a node\n3.Delete a node\n4.Search a node\n5.Find Number of elements\n6.Find Height of the tree\n7.InOrderTraverse" +
                    "\n8.PreOrderTraverse\n9.PostOrderTraverse\n10.LevelOrderTraverse\n11.Exit\nPlease enter a choice");
                choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        tree.display(tree.Root, 1);
                        Console.ReadKey();
                        break;
                    case 2:
                        Console.WriteLine("Enter the element to add\n");
                        int value = Convert.ToInt32(Console.ReadLine());
                        bool ht_inc = false;
                        bool result = tree.Insert(ref tree.Root, value, ref ht_inc);
                        if (result)
                        {
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
                        bool ht_inc1 = false;
                        bool delResult = tree.Delete(ref tree.Root, valueToDelete,ref ht_inc1);
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
                        bool searchResult = tree.Search(ref tree.Root, valueToSearch);
                        if (searchResult)
                        {
                            Console.WriteLine("The element exists");
                        }
                        else
                        {
                            Console.WriteLine("The element doesn't exists");
                        }
                        Console.ReadKey();
                        break;
                    case 5:
                        int numberOfElements = tree.FindNumberOfElements(ref tree.Root);
                        Console.WriteLine("The total number of elements in the tree is " + numberOfElements);
                        Console.ReadKey();
                        break;
                    case 6:
                        int heightOfTree = tree.FindHeightOfTree(ref tree.Root);
                        Console.WriteLine("The total height of the tree is " + heightOfTree);
                        Console.ReadKey();
                        break;
                    case 7:
                        tree.InOrderTraverse(tree.Root);
                        Console.ReadKey();
                        break;
                    case 8:
                        tree.PreOrderTraverse(tree.Root);
                        Console.ReadKey();
                        break;
                    case 9:
                        tree.PostOrderTraverse(tree.Root);
                        Console.ReadKey();
                        break;
                    case 10:
                        tree.LevelOrderTraverse(tree.Root);
                        Console.ReadKey();
                        break;
                    case 11:
                        break;
                    default:
                        Console.WriteLine("Please enter a valid choice");
                        Console.ReadKey();
                        break;
                }
            } while (choice != 11);
        }

        private static void RedBlackTreeDemo()
        {
            RedBlackTree tree = new RedBlackTree();
            int choice = 1;
            do
            {
                Console.Clear();
                Console.WriteLine("1.Display the Tree\n2.Insert a node\n3.Delete a node\n4.Search a node\n5.Find Number of elements\n6.Find Height of the tree\n7.InOrderTraverse" +
                    "\n8.PreOrderTraverse\n9.PostOrderTraverse\n10.LevelOrderTraverse\n11.Exit\nPlease enter a choice");
                choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        tree.display(tree.Root, 1);
                        Console.ReadKey();
                        break;
                    case 2:
                        Console.WriteLine("Enter the element to add\n");
                        int value = Convert.ToInt32(Console.ReadLine());
                        bool result = tree.Insert(ref tree.Root, value);
                        if (result)
                        {
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
                        bool delResult = tree.Delete(ref tree.Root, valueToDelete);
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
                        bool searchResult = tree.Search(ref tree.Root, valueToSearch);
                        if (searchResult)
                        {
                            Console.WriteLine("The element exists");
                        }
                        else
                        {
                            Console.WriteLine("The element doesn't exists");
                        }
                        Console.ReadKey();
                        break;
                    case 5:
                        int numberOfElements = tree.FindNumberOfElements(ref tree.Root);
                        Console.WriteLine("The total number of elements in the tree is " + numberOfElements);
                        Console.ReadKey();
                        break;
                    case 6:
                        int heightOfTree = tree.FindHeightOfTree(ref tree.Root);
                        Console.WriteLine("The total height of the tree is " + heightOfTree);
                        Console.ReadKey();
                        break;
                    case 7:
                        tree.InOrderTraverse(tree.Root);
                        Console.ReadKey();
                        break;
                    case 8:
                        tree.PreOrderTraverse(tree.Root);
                        Console.ReadKey();
                        break;
                    case 9:
                        tree.PostOrderTraverse(tree.Root);
                        Console.ReadKey();
                        break;
                    case 10:
                        tree.LevelOrderTraverse(tree.Root);
                        Console.ReadKey();
                        break;
                    case 11:
                        break;
                    default:
                        Console.WriteLine("Please enter a valid choice");
                        Console.ReadKey();
                        break;
                }
            } while (choice != 11);
        }

        private static void SplayTreeDemo()
        {
            SplayTree tree = new SplayTree();
            int choice = 1;
            do
            {
                Console.Clear();
                Console.WriteLine("1.Display the Tree\n2.Insert a node\n3.Delete a node\n4.Search a node\n5.Find Number of elements\n6.Find Height of the tree\n7.InOrderTraverse" +
                    "\n8.PreOrderTraverse\n9.PostOrderTraverse\n10.LevelOrderTraverse\n11.Exit\nPlease enter a choice");
                choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        tree.display(tree.Root, 1);
                        Console.ReadKey();
                        break;
                    case 2:
                        Console.WriteLine("Enter the element to add\n");
                        int value = Convert.ToInt32(Console.ReadLine());
                        bool result = tree.InsertValueToSplayTree(value);
                        if (result)
                        {
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
                        bool delResult = tree.Delete(ref tree.Root, valueToDelete);
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
                        bool searchResult = tree.Search(ref tree.Root, valueToSearch);
                        if (searchResult)
                        {
                            Console.WriteLine("The element exists");
                        }
                        else
                        {
                            Console.WriteLine("The element doesn't exists");
                        }
                        Console.ReadKey();
                        break;
                    case 5:
                        int numberOfElements = tree.FindNumberOfElements(ref tree.Root);
                        Console.WriteLine("The total number of elements in the tree is " + numberOfElements);
                        Console.ReadKey();
                        break;
                    case 6:
                        int heightOfTree = tree.FindHeightOfTree(ref tree.Root);
                        Console.WriteLine("The total height of the tree is " + heightOfTree);
                        Console.ReadKey();
                        break;
                    case 7:
                        tree.InOrderTraverse(tree.Root);
                        Console.ReadKey();
                        break;
                    case 8:
                        tree.PreOrderTraverse(tree.Root);
                        Console.ReadKey();
                        break;
                    case 9:
                        tree.PostOrderTraverse(tree.Root);
                        Console.ReadKey();
                        break;
                    case 10:
                        tree.LevelOrderTraverse(tree.Root);
                        Console.ReadKey();
                        break;
                    case 11:
                        break;
                    default:
                        Console.WriteLine("Please enter a valid choice");
                        Console.ReadKey();
                        break;
                }
            } while (choice != 11);
        }

        private static void TrieDemo()
        {
            Trie tree = new Trie();
            int choice = 1;
            do
            {
                Console.Clear();
                Console.WriteLine("1.Insert a string\n2.Search a string\n3.Exit\nPlease enter a choice");
                string input = Console.ReadLine().Trim();
                if (string.IsNullOrEmpty(input))
                {
                    continue;
                }
                choice = Convert.ToInt32(input);
                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Enter the string to add\n");
                        string value = Console.ReadLine();
                        tree.Insert(value);
                        Console.ReadKey();
                        break;
                    case 2:
                        Console.WriteLine("Enter the string to search\n");
                        string value1 = Console.ReadLine();
                        bool result = tree.Search(value1);
                        if (result)
                        {
                            Console.WriteLine("The string exists");
                        }
                        else
                        {
                            Console.WriteLine("The string does not exists");
                        }
                        Console.ReadKey();
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
    }
}
