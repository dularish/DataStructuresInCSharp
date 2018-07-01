using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Stacks;

namespace Graphs
{
    public class GraphWithAdjacencyList
    {
        public Node[] AdjacencyList;
        public string[] NodeValues;

        public int[][] adjMatrix;

        public GraphWithAdjacencyList(int numOfNodes)
        {
            NodeValues = new string[numOfNodes];
            AdjacencyList = new Node[numOfNodes];
        }

        public GraphWithAdjacencyList()
        {
            GenerateAdjListFromFile();
        }
        public class Node
        {
            public string NodeValue { get; set; }
            public Node Next { get; set; }

            public int Distance { get; set; }

            public Node(string datavalue, int length = 1)
            {
                NodeValue = datavalue;
                Next = null;
                Distance = length;
            }
        }

        internal void InsertNodesWithNeighboursInfo()
        {
            //System.Configuration.AppSettingsReader reader = new System.Configuration.AppSettingsReader();
            

            //string value1 = System.Configuration.ConfigurationManager.AppSettings["AdjListPath"];
            //string value = reader.GetValue("AdjListPath", typeof(string)).ToString();
            Console.Clear();
            for (int i = 0; i < NodeValues.Length; i++)
            {
                Console.WriteLine("Enter the value of the node present at index : " + i);
                string data = Console.ReadLine();

                if (!NodeValues.Contains(data))
                {
                    NodeValues[i] = data;
                }
            }

            

            for (int i = 0; i < NodeValues.Length; i++)
            {
                Console.Clear();
                Console.WriteLine("Enter the number of neighbours for the node at " + i);
                int neighboursCount = Convert.ToInt32(Console.ReadLine());
                Node last = null;
                for (int j = 0; j < neighboursCount; j++)
                {
                    Console.Clear();
                    Console.WriteLine("Enter the neighbour index " + j + " for the Node : " + NodeValues[i]);
                    string data = Console.ReadLine();
                    Node nodeToAdd = new Node(data.ToUpper());
                    
                    if (AdjacencyList[i] == null)
                    {
                        AdjacencyList[i] = nodeToAdd;
                        last = nodeToAdd;
                    }
                    else
                    {
                        last.Next = nodeToAdd;
                        last = nodeToAdd;
                    }
                }
            }

            adjMatrix = GenerateAdjMatrix();
        }

        public void GenerateAdjListFromFile()
        {
            var appSettings = ConfigurationManager.AppSettings;
            string adjListPath = appSettings["AdjListPath"];
            List<string> lines = new List<string>();
            using (StreamReader reader = new StreamReader(adjListPath))
            {
                string line = "";
                while ((line = reader.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }


            lines = lines.Where(s => s.Contains(":")).ToList();
            NodeValues = new string[lines.Count];
            AdjacencyList = new Node[lines.Count];
            for (int i = 0; i < NodeValues.Length; i++)
            {
                string NodeName = lines[i].Substring(0, Regex.Match(lines[i], ":").Index);
                NodeName = NodeName.Trim().ToUpper();
                if (!NodeValues.Contains(NodeName))
                {
                    NodeValues[i] = NodeName;
                }

                List<string> neighbours = lines[i].Substring(Regex.Match(lines[i], ":").Index).Trim(':').Trim().Split(',').Select(s => s.Trim()).ToList();
                Node last = null;
                for (int j = 0; j < neighbours.Count; j++)
                {
                    Node nodeToAdd;
                    if(neighbours[j].Contains('-'))
                    {
                        string nodeName = neighbours[j].Substring(0, neighbours[j].LastIndexOf('-')).ToUpper();
                        int length = Convert.ToInt32(neighbours[j].Substring(neighbours[j].LastIndexOf('-') + 1));
                        nodeToAdd = new Node(nodeName, length);
                    }
                    else
                    {
                        nodeToAdd = new Node(neighbours[j].ToUpper());
                    }
                    
                    
                    if (AdjacencyList[i] == null)
                    {
                        AdjacencyList[i] = nodeToAdd;
                        last = nodeToAdd;
                    }
                    else
                    {
                        last.Next = nodeToAdd;
                        last = nodeToAdd;
                    }
                }
            }


            adjMatrix = GenerateAdjMatrix();
        }

        private int[][] GenerateAdjMatrix()
        {
            //Convert AdjList to AdjMatrix - Assuming user enters A,B,C,D
            if (AdjacencyList.All(s => string.IsNullOrEmpty(s.NodeValue) || s.NodeValue.Length == 1) && AdjacencyList.Length > 0)
            {
                //Finding Max char
                char MaxChar = (char)('A' + (AdjacencyList.Length - 1));

                for (int i = 0; i < AdjacencyList.Count(); i++)
                {
                    if (string.IsNullOrEmpty(AdjacencyList[i].NodeValue))
                    {
                        continue;
                    }
                    Node travNode = AdjacencyList[i];

                    while (travNode != null)
                    {
                        if ((int)MaxChar < (int)travNode.NodeValue.ToUpper()[0])
                        {
                            MaxChar = travNode.NodeValue.ToUpper()[0];
                        }
                        travNode = travNode.Next;
                    }
                }

                int[][] adjMatrix = new int[MaxChar - 'A' + 1][];

                for (int i = 0; i < adjMatrix.Length; i++)
                {
                    
                    adjMatrix[i] = new int[MaxChar - 'A' + 1];
                    if (string.IsNullOrEmpty(AdjacencyList[i].NodeValue))
                    {
                        continue;
                    }
                    //int index = -1;
                    //for (int j = 0; j < AdjacencyList.Length; j++)
                    //{
                    //    if (AdjacencyList[j].NodeValue.ToUpper()[0] - 'A' == i)
                    //    {
                    //        index = j;
                    //        break;
                    //    }
                    //}

                    Node travNode = AdjacencyList[i];
                    while (travNode != null)
                    {
                        int indexToMakeOne = travNode.NodeValue.ToUpper()[0] - 'A';
                        adjMatrix[i][indexToMakeOne] = travNode.Distance;
                        travNode = travNode.Next;
                    }
                }

                Console.WriteLine("Adj Matrix :");
                for (int i = 0; i < adjMatrix.Length; i++)
                {
                    for (int j = 0; j < adjMatrix[i].Length; j++)
                    {
                        Console.Write(adjMatrix[i][j] + "\t");
                    }
                    Console.WriteLine();
                }
                return adjMatrix;
            }
            else
            {
                throw new Exception("Conversion to adjMatrix not possible");
                //Console.WriteLine("Conversion to adjMatrix not possible");
            }
        }

        internal void DepthFirstSearch(int index)
        {
            int[] readyStateArray = new int[adjMatrix.Length];

            Stacks.DoublyLinkedListGenericStack<int> stack = new DoublyLinkedListGenericStack<int>();
            Console.WriteLine("Depth First Search results :");
            Console.Write(((char)('A' + index)) + "\t");
            readyStateArray[index] = 1;
            do//while (readyStateArray[index] != 1 && index < adjMatrix.Length)
            {
                for (int i = 0; i < adjMatrix[index].Length; i++)
                {
                    if (adjMatrix[index][i] == 1 && readyStateArray[i] == 0)
                    {
                        stack.push(i);
                        
                        readyStateArray[i] = 1;
                    }
                }

                if (stack.peek() != null)
                {
                    index = (int)stack.pop();
                    Console.Write(((char)('A' + index)) + "\t");
                }
            } while (stack.peek() != null);
            Console.WriteLine("Depth First Search completed");
            Console.ReadKey();
        }



        internal void GetTopologicalSort()
        {
            Queues.DoublyLinkedListGenericQueue<int> queue = new Queues.DoublyLinkedListGenericQueue<int>();
            //bool ZeroIndegNodePresent = false;
            //int indegNodeIndex = -1;
            int[] indegreesForNodes = new int[adjMatrix.Length];
            bool[] nodesAddedToQueue = new bool[adjMatrix.Length];
            //WIP
            for (int i = 0; i < adjMatrix.Length; i++)
            {
                int indeg = GetIndegreeOfNode(i, adjMatrix);
                indegreesForNodes[i] = indeg;
                if (indeg == 0)
                {
                    queue.push(i);
                    nodesAddedToQueue[i] = true;
                }
            }
                
                while (queue.peek() != null)
                {
                    int NodeIndex = (int)queue.pop();
                    Console.Write( ((char)('A' + NodeIndex)) + "\t");
                    //Getting neighbours of the node 
                    for (int i = 0; i < adjMatrix[NodeIndex].Length; i++)
                    {
                        if (adjMatrix[NodeIndex][i] > 0)
                        {
                            indegreesForNodes[i]--;
                            if (indegreesForNodes[i] == 0 && !nodesAddedToQueue[i])
                            {
                                queue.push(i);
                                nodesAddedToQueue[i] = true;
                            }
                        }
                    }
                }
            
        }

        private int GetIndegreeOfNode(int p, int[][] adjMatrix)
        {
            int IndegreeOfNode = 0;
            for (int i = 0; i < adjMatrix.Length; i++)
            {
                if (p < adjMatrix[i].Length)
                {
                    if (adjMatrix[i][p] == 1)
                    {
                        IndegreeOfNode++;
                    }
                }
                else
                {
                    //Unexpected block for debugging
                }
            }
            return IndegreeOfNode;
        }

        internal int[][] GetMSTByPrimAlg()
        {
            int[] verticesAddedToMST = new int[adjMatrix.Length];

            

            int[][] MST = new int[adjMatrix.Length][];

            for (int i = 0; i < MST.Length; i++)
            {
                MST[i] = new int[MST.Length];
            }

            

            for (int i = 0; i < verticesAddedToMST.Length; i++)
            {
                if (verticesAddedToMST[i] == 0)
                {
                    int currentVertex = i;
                    while (true)
                    {
                        int minLength = int.MaxValue;
                        int minLengthVertex = -1;
                        for (int j = 0; j < adjMatrix[currentVertex].Length; j++)
                        {
                            if (verticesAddedToMST[j] != 1 && adjMatrix[currentVertex][j] > 0 && adjMatrix[currentVertex][j] < minLength)
                            {
                                minLength = adjMatrix[currentVertex][j];
                                minLengthVertex = j;
                            }
                        }

                        if (minLengthVertex != -1)
                        {
                            MST[currentVertex][minLengthVertex] = minLength;
                            verticesAddedToMST[minLengthVertex] = 1;
                            verticesAddedToMST[currentVertex] = 1;

                            if (adjMatrix[currentVertex][minLengthVertex] == adjMatrix[minLengthVertex][currentVertex])
                            {
                                MST[minLengthVertex][currentVertex] = minLength;
                            }

                            currentVertex = minLengthVertex;
                            
                            
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }

            for (int i = 0; i < verticesAddedToMST.Length; i++)
            {
                if (verticesAddedToMST[i] == 0)
                {
                    int minLength = int.MaxValue;
                    int minLengthVertex = -1;
                    for (int j = 0; j < adjMatrix[i].Length; j++)
                    {
                        if (verticesAddedToMST[j] == 1 && adjMatrix[i][j] > 0 && adjMatrix[i][j] < minLength)
                        {
                            minLength = adjMatrix[i][j];
                            minLengthVertex = j;
                        }
                    }

                    if (minLengthVertex != -1)
                    {
                        MST[i][minLengthVertex] = minLength;
                        verticesAddedToMST[i] = 1;
                        verticesAddedToMST[i] = 1;

                        if (adjMatrix[i][minLengthVertex] == adjMatrix[minLengthVertex][i])
                        {
                            MST[minLengthVertex][i] = minLength;
                        }
                    }
                    else
                    {
                        //Unexpected block
                    }
                }
            }

            return MST;

        }

        internal void GetMSTByKruskalAlg()
        {
            Node[][] forest = new Node[AdjacencyList.Length][];

            int[][] MST = new int[AdjacencyList.Length][];

            for (int i = 0; i < MST.Length; i++)
            {
                MST[i] = new int[AdjacencyList.Length];
            }

            Queues.DoublyLinkedListGenericPriorityQueue<Edge> queue = new Queues.DoublyLinkedListGenericPriorityQueue<Edge>();

            //Initializing forest
            for (int i = 0; i < forest.Length; i++)
            {
                forest[i] = new Node[AdjacencyList.Length];
                forest[i][i] = new Node("");
            }

            //Populating the queue
            for (int i = 0; i < adjMatrix.Length; i++)
            {
                for (int j = 0; j < adjMatrix[i].Length; j++)
                {
                    if (adjMatrix[i][j] > 0)
                    {
                        if (adjMatrix[i][j] == adjMatrix[j][i])
                        {
                            if (i > j)
                            {
                                queue.push(new Edge(i, j, adjMatrix[i][j]), adjMatrix[i][j]);
                            }
                        }
                        else
                        {
                            queue.push(new Edge(i, j, adjMatrix[i][j], true), adjMatrix[i][j]);
                        }
                    }
                }
            }

            while (queue.peek() != null)
            {
                Queues.DoublyLinkedListGenericPriorityQueue<Edge>.Node poppedNode = queue.pop();

                Edge edgePopped = poppedNode.value;

                bool doesEdgeConnectDiffTrees = true;

                for (int i = 0; i < forest.Length; i++)
                {
                    if (forest[i] != null && forest[i][edgePopped.FirstVertex] != null && forest[i][edgePopped.SecondVertex] != null)
                    {
                        doesEdgeConnectDiffTrees = false;
                        break;
                    }
                }

                if (doesEdgeConnectDiffTrees)
                {
                    int forestIndexForFirstVertexInEdge = -1;

                    int forestIndexForSecondVertexInEdge = -1;

                    for (int i = 0; i < forest.Length; i++)
                    {
                        if (forest[i] != null && forest[i][edgePopped.FirstVertex] != null)
                        {
                            if (forestIndexForFirstVertexInEdge == -1)
                            {
                                forestIndexForFirstVertexInEdge = i;
                            }
                            else
                            {
                                //Unexpected block
                            }
                            
                        }

                        if (forest[i] != null && forest[i][edgePopped.SecondVertex] != null)
                        {
                            if (forestIndexForSecondVertexInEdge == -1)
                            {
                                forestIndexForSecondVertexInEdge = i;
                            }
                            else
                            {
                                //Unexpected block
                            }
                        }
                    }

                    if (forestIndexForFirstVertexInEdge != -1 && forestIndexForSecondVertexInEdge != -1)
                    {
                        for (int i = 0; i < AdjacencyList.Length; i++)
                        {
                            if (forest[forestIndexForFirstVertexInEdge] != null && forest[forestIndexForFirstVertexInEdge][i] != null && forest[forestIndexForSecondVertexInEdge][i] != null)
                            {
                                //Unexpected block
                            }
                            else if (forest[forestIndexForSecondVertexInEdge] != null && forest[forestIndexForSecondVertexInEdge][i] != null)
                            {
                                forest[forestIndexForFirstVertexInEdge][i] = forest[forestIndexForSecondVertexInEdge][i];
                            }
                        }

                        forest[forestIndexForSecondVertexInEdge] = null;
                    }
                    else
                    {
                        //Unexpected block
                    }

                    //Adding new edge
                    if(edgePopped.Directed)
                    {
                        Node treeNode = forest[forestIndexForFirstVertexInEdge][edgePopped.FirstVertex];

                        

                        if (treeNode == null)
                        {
                            treeNode = new Node(((char)('A' + edgePopped.SecondVertex)).ToString(), edgePopped.Length); 
                        }
                        else
                        {
                            Node Last = treeNode;
                            while (Last.Next != null)
                            {
                                Last = Last.Next;
                            }

                            Last.Next = new Node(((char)('A' + edgePopped.SecondVertex)).ToString(), edgePopped.Length); 
                        }

                        MST[edgePopped.FirstVertex][edgePopped.SecondVertex] = edgePopped.Length;
                    }
                    else
                    {
                        Node treeNode = forest[forestIndexForFirstVertexInEdge][edgePopped.FirstVertex];

                        Node treeNodeForSecondVertex = forest[forestIndexForFirstVertexInEdge][edgePopped.SecondVertex];

                        if (treeNode == null)
                        {
                            treeNode = new Node(((char)('A' + edgePopped.SecondVertex)).ToString(), edgePopped.Length);
                        }
                        else
                        {
                            Node Last = treeNode;
                            while (Last.Next != null)
                            {
                                Last = Last.Next;
                            }

                            Last.Next = new Node(((char)('A' + edgePopped.SecondVertex)).ToString(), edgePopped.Length);
                        }

                        if (treeNodeForSecondVertex == null)
                        {
                            treeNodeForSecondVertex = new Node(((char)('A' + edgePopped.FirstVertex)).ToString(), edgePopped.Length);
                        }
                        else
                        {
                            Node Last = treeNodeForSecondVertex;
                            while (Last.Next != null)
                            {
                                Last = Last.Next;
                            }

                            Last.Next = new Node(((char)('A' + edgePopped.FirstVertex)).ToString(), edgePopped.Length);
                        }

                        MST[edgePopped.FirstVertex][edgePopped.SecondVertex] = edgePopped.Length;
                        MST[edgePopped.SecondVertex][edgePopped.FirstVertex] = edgePopped.Length;
                    }
                    
                }
            }


        }

        internal void GetDjikstraAlgResultOn(char input)
        {
            int[][] MST = new int[AdjacencyList.Length][];

            int[] reachable = new int[AdjacencyList.Length];

            for (int i = 0; i < AdjacencyList.Length; i++)
            {
                MST[i] = new int[AdjacencyList.Length];
            }

            int indexToStartWith = input - 'A';

            Queues.DoublyLinkedListGenericPriorityQueue<NodeLabel> tempLabelQueue = new Queues.DoublyLinkedListGenericPriorityQueue<NodeLabel>();

            NodeLabel[] permanentLabel = new NodeLabel[AdjacencyList.Length];
            int[] addedInTempQueue = new int[AdjacencyList.Length];

            permanentLabel[indexToStartWith] = new NodeLabel(indexToStartWith, 0, NodeLabelType.Permanent);
            for (int j = 0; j < adjMatrix[indexToStartWith].Length; j++)
            {
                if (adjMatrix[indexToStartWith][j] > 0)
                {
                    int labelValue = adjMatrix[indexToStartWith][j] + permanentLabel[indexToStartWith].LabelValue;
                    tempLabelQueue.push(new NodeLabel(j, labelValue, NodeLabelType.Temporary, indexToStartWith), labelValue);
                }
            }
            while (tempLabelQueue.peek() != null)
            {
                NodeLabel label = tempLabelQueue.pop().value;

                permanentLabel[label.NodeIndex] = new NodeLabel(label.NodeIndex, label.LabelValue, NodeLabelType.Permanent);
                MST[label.PermanentNodeRef][label.NodeIndex] = adjMatrix[label.PermanentNodeRef][label.NodeIndex];
                reachable[label.NodeIndex] = label.LabelValue;

                tempLabelQueue = new Queues.DoublyLinkedListGenericPriorityQueue<NodeLabel>();
                for (int i = 0; i < permanentLabel.Length; i++)
                {
                    if (permanentLabel[i] != null)
                    {
                        for (int j = 0; j < adjMatrix[i].Length; j++)
                        {
                            if (adjMatrix[i][j] > 0 && permanentLabel[j] == null)
                            {
                                int labelValue = adjMatrix[i][j] + permanentLabel[i].LabelValue;
                                tempLabelQueue.push(new NodeLabel(j, labelValue, NodeLabelType.Temporary, i), labelValue);
                            }
                        }
                    }
                }
            }

        }
    }
}
