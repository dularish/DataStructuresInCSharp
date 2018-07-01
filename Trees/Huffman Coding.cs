using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Queues;
using System.Collections;
using System.IO;

namespace Trees
{
    class Huffman_Coding
    {
        public Tree Root;

        //public DoublyLinkedListGenericPriorityQueue<string> StringPriorityQueue;
        //public DoublyLinkedListGenericPriorityQueue<char> CharPriorityQueue;
        public DoublyLinkedListGenericPriorityQueue<object> PriorityQueue;

        public DoublyLinkedListGenericPriorityQueue<object> outputPriorityQueue;

        public PriorityQueueType PriorityQueuePreference = PriorityQueueType.CharacterQueue;

        Dictionary<char, string> charBitsMapping = new Dictionary<char, string>();

        public enum PriorityQueueType
        {
            CharacterQueue, WordQueue
        }

        public enum NodeType
        {
            Internal, External
        }

        public Huffman_Coding()
        {
            Root = null;
            PriorityQueue = null;
        }

        public class Tree
        {
            public int Weight;
            public object Data;
            public Tree Left;
            public Tree Right;
            public NodeType NodeType;


            public Tree(int val)
            {
                Weight = val;
                Left = null;
                Right = null;
                NodeType = Huffman_Coding.NodeType.Internal;
            }
            public Tree(int weight, object data)
            {
                Weight = weight;
                Left = null;
                Right = null;
                NodeType = Huffman_Coding.NodeType.External;
                Data = data;
            }
        }

        public bool GenerateHuffmanCodingTree(string inputString)
        {
            GeneratePriorityQueue(inputString);


            if(PriorityQueue.peek() != null)
            {
                Queues.DoublyLinkedListGenericPriorityQueue<object>.Node LastPoppedValue = PriorityQueue.pop();
                int LastPoppedValuePriority = -1;
                object LastPoppedValueObject = null;
                if (LastPoppedValue.GetType() != typeof(Tree))
                {
                    LastPoppedValuePriority = ((Queues.DoublyLinkedListGenericPriorityQueue<object>.Node)LastPoppedValue).PriorityValue;
                    LastPoppedValueObject = ((Queues.DoublyLinkedListGenericPriorityQueue<object>.Node)LastPoppedValue).value;
                }

                while (PriorityQueue.peek() != null)
                {
                    Tree firstTree, secondTree;
                    if (LastPoppedValue.value.GetType() == typeof(Tree) && PriorityQueue.peek().value.GetType() == typeof(Tree))
                    {
                        firstTree = ((Tree)LastPoppedValue.value);
                        secondTree = ((Tree)PriorityQueue.peek().value);
                        
                    }
                    else if (LastPoppedValue.value.GetType() == typeof(Tree))
                    {
                        firstTree = ((Tree)LastPoppedValue.value);
                        secondTree = new Tree(PriorityQueue.peek().PriorityValue, PriorityQueue.peek().value);
                        
                    }
                    else if (PriorityQueue.peek().value.GetType() == typeof(Tree))
                    {
                        firstTree = new Tree(LastPoppedValuePriority, LastPoppedValueObject);
                        secondTree = ((Tree)PriorityQueue.peek().value);
                    }
                    else
                    {
                        firstTree = new Tree(LastPoppedValuePriority, LastPoppedValueObject);
                        secondTree = new Tree(PriorityQueue.peek().PriorityValue, PriorityQueue.peek().value);
                    }
                    Tree newTree = new Tree(firstTree.Weight + secondTree.Weight);
                    newTree.Left = firstTree.Weight < secondTree.Weight ? firstTree : secondTree;
                    newTree.Right = firstTree.Weight >= secondTree.Weight ? firstTree : secondTree;
                    PriorityQueue.push(newTree, newTree.Weight);
                    PriorityQueue.pop();
                    LastPoppedValue = PriorityQueue.pop();
                    if (LastPoppedValue != null && LastPoppedValue.GetType() != typeof(Tree))
                    {
                        LastPoppedValuePriority = ((Queues.DoublyLinkedListGenericPriorityQueue<object>.Node)LastPoppedValue).PriorityValue;
                        LastPoppedValueObject = ((Queues.DoublyLinkedListGenericPriorityQueue<object>.Node)LastPoppedValue).value;
                    }
                }
                if (LastPoppedValue.value.GetType() == typeof(Tree))
                {
                    Root = (Tree)LastPoppedValue.value;
                }
                else
                {
                    Tree newTree = new Tree(LastPoppedValuePriority, LastPoppedValueObject);
                    Root = newTree;
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        private void GeneratePriorityQueue(string inputString)
        {
            if (PriorityQueuePreference == PriorityQueueType.CharacterQueue)
            {
                //CharPriorityQueue = new DoublyLinkedListGenericPriorityQueue<char>();
                PriorityQueue = new DoublyLinkedListGenericPriorityQueue<object>();
                Dictionary<char,int> dict = GenerateCharDictionaryForInput(inputString);

                foreach (KeyValuePair<char,int> item in dict)
                {
                    PriorityQueue.push(item.Key, item.Value);
                }
            }
            else
            {
                //StringPriorityQueue = new DoublyLinkedListGenericPriorityQueue<string>();
                PriorityQueue = new DoublyLinkedListGenericPriorityQueue<object>();
                Dictionary<string, int> dict = GenerateStringDictionaryForInput(inputString);

                foreach (KeyValuePair<string,int> item in dict)
                {
                    PriorityQueue.push(item.Key, item.Value);
                }
            }
        }

        private Dictionary<string, int> GenerateStringDictionaryForInput(string inputString)
        {
            Dictionary<string, int> outputDict = new Dictionary<string, int>();
            int startIndex = -1;
            outputDict.Add(" ", FindMatchCount(inputString, " "));
            for (int i = 0; i < inputString.Length; i++)
            {
                if (startIndex == -1)
                {
                    startIndex = i;
                }
                else if (inputString[i] == ' ')
                {
                    string word = GetStringBetweenIndices(inputString, startIndex, i - 1);
                    if (outputDict.ContainsKey(word))
                    {
                        continue;
                    }
                    int matchCount = FindMatchCount(inputString, word);
                    outputDict.Add(word, matchCount);
                }
            }
            return outputDict;
        }

        private int FindMatchCount(string inputString, string word)
        {
            int matchCount = 0;
            for (int i = 0; i < inputString.Length;i++ )
            {
                if (inputString[i] == word[0])
                {
                    int k = i;
                    bool Match = true;
                    bool IsTraverseComplete = false;
                    for (int j = 0; j < word.Length && k < inputString.Length; j++,k++)
                    {
                        if (inputString[k] != word[j])
                        {
                            Match = false;
                        }
                        if (j == word.Length - 1)
                        {
                            IsTraverseComplete = true;
                        }
                    }
                    if (Match && IsTraverseComplete)
                    {
                        matchCount++;
                    }
                }
            }
            return matchCount;
        }

        private string GetStringBetweenIndices(string inputString, int startIndex, int endIndex)
        {
            string output = "";
            for (int i = startIndex; i <= endIndex; i++)
            {
                output += inputString[i];
            }

            return output;
        }



        private Dictionary<char, int> GenerateCharDictionaryForInput(string inputString)
        {
            Dictionary<char, int> outputDict = new Dictionary<char, int>();
            for (int i = 0; i < inputString.Length; i++)
            {
                if (outputDict.ContainsKey(inputString[i]))
                {
                    continue;
                }
                string letter = "";
                letter += inputString[i];
                int matchCount = FindMatchCount(inputString, letter);
                outputDict.Add(inputString[i], matchCount);
            }
            return outputDict;
        }


        public bool Search(ref Tree tree, int val)
        {
            if (tree == null)
            {
                return false;
            }
            else if (val > tree.Weight)
            {
                return Search(ref tree.Right, val);
            }
            else if (val < tree.Weight)
            {
                return Search(ref tree.Left, val);
            }
            else
            {
                return true;
            }
        }

        public void display(Tree tree, int level)
        {
            if (tree != null)
            {
                display(tree.Right, level + 1);
                Console.WriteLine();
                for (int i = 0; i < level; i++)
                {
                    Console.Write("\t");
                }
                if (tree.NodeType == NodeType.Internal)
                {
                    Console.Write(tree.Weight);
                }
                else
                {
                    Console.Write(tree.Weight + "(" + tree.Data + ")");
                }

                display(tree.Left, level + 1);
            }
        }

        internal string Decode(string valueToDecode)
        {
            string outputString = "";
            Tree trav = Root;
            for (int i = 0; i < valueToDecode.Length; i++)
            {
                if (valueToDecode[i] == '0')
                {
                    trav = trav.Left;
                    if (trav == null)
                    {
                        Console.WriteLine("Decoding failed. Invalid Code");
                    }
                    if (trav.NodeType == NodeType.External)
                    {
                        outputString += trav.Data;
                        trav = Root;
                    }
                }
                else if (valueToDecode[i] == '1')
                {
                    trav = trav.Right;
                    if (trav == null)
                    {
                        Console.WriteLine("Decoding failed. Invalid Code");
                    }
                    if (trav.NodeType == NodeType.External)
                    {
                        outputString += trav.Data;
                        trav = Root;
                    }
                }
                else
                {
                    outputString = "";
                    return outputString;
                }
            }
            return outputString;
        }

        internal string Encode(string value)
        {
            string output = "";
            PrepareBitsTableForCharacter();
            for (int i = 0; i < value.Length; i++)
            {
                output += charBitsMapping[value[i]];
            }
            if (Decode(output) == value)
            {
                return output;
            }
            else
            {
                Console.WriteLine("Encoding-Decoding match failed.");
                Console.WriteLine("Input To Encode : " + value + " Encoded : " + output );
                Console.WriteLine("Decoded for the code : " + Decode(output));
                return output;
            }
        }

        public void SaveEncodedString(string inputStringToEncode, string path)
        {
            if (Root == null)
            {
                GenerateHuffmanCodingTree(inputStringToEncode);
            }
            string encodedOutput = Encode(inputStringToEncode);
            BitArray bitArray = new BitArray(encodedOutput.Length);
            for (int i = 0; i < encodedOutput.Length; i++)
            {
                if (encodedOutput[i] == '0')
                {
                    bitArray[i] = false;
                }
                else
                {
                    bitArray[i] = true;
                }
            }

            byte[] bytes = new byte[bitArray.Length / 8 + (bitArray.Length % 8 == 0 ? 0 : 1)];
            bitArray.CopyTo(bytes, 0);
            string finalFileNameSaved = path + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day + "__" + DateTime.Now.Hour + "_" + DateTime.Now.Minute;
            File.WriteAllBytes( finalFileNameSaved + ".bin", bytes);

            using (StreamWriter sw = new StreamWriter(finalFileNameSaved + ".txt"))
            {
                foreach (KeyValuePair<char,string> item in charBitsMapping)
                {
                    sw.WriteLine(item.Key + "|" + item.Value);
                }
            }

            string readString = "";
            //Testing with reading
            //BinaryReader reader = new BinaryReader(new FileStream(finalFileNameSaved + ".bin", FileMode.Open));
            //while (reader.Read() != null)
            //{
            //    bool data = reader.ReadBoolean();
            //    if (data)
            //    {
            //        readString += "1";
            //    }
            //    else
            //    {
            //        readString += "0";
            //    }
            //}
           byte[] bytesRead =  File.ReadAllBytes(finalFileNameSaved + ".bin");
           BitArray bitArrayRead = new BitArray(bytesRead);
           string bitArraySaved = "";
           foreach (bool item in bitArray)
           {
               if (item)
               {
                   bitArraySaved += "1";
               }
               else
               {
                   bitArraySaved += "0";
               }
           }
           foreach (bool item in bitArrayRead)
           {
               if (item)
               {
                   readString += "1";
               }
               else
               {
                   readString += "0";
               }
           }
            Console.WriteLine("Encoded Output - " + encodedOutput);
            Console.WriteLine("Saved BitArray - " + bitArraySaved);
            Console.WriteLine("Retrieved Data - " + readString);
        }

        private void PrepareBitsTableForCharacter()
        {
            charBitsMapping.Clear();
            TraverseWithPathAndAddBitsToTable(Root.Left, "0");
            TraverseWithPathAndAddBitsToTable(Root.Right, "1");
        }

        private void TraverseWithPathAndAddBitsToTable(Tree tree, string path)
        {
            if (tree != null)
            {
                if (tree.NodeType == NodeType.Internal)
                {
                    TraverseWithPathAndAddBitsToTable(tree.Left, path + "0");
                    TraverseWithPathAndAddBitsToTable(tree.Right,path + "1");
                }
                else if (tree.NodeType == NodeType.External)
                {
                    if (!charBitsMapping.ContainsKey((char)tree.Data))
                    {
                        charBitsMapping.Add((char)tree.Data, path);
                    }
                    else
                    {
                        Console.WriteLine("12.20 Unexpected block");
                    }
                }
                else
                {
                    Console.WriteLine("12.10 Unexpected block");
                }
            }
            
        }
    }
}
