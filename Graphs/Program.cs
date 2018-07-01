using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    class Program
    {
        public static GraphWithAdjacencyList graphWithAdjList;
        static void Main(string[] args)
        {
            int choice;
            do
            {
                Console.Clear();
                Console.WriteLine("1.Demo Graph Creation " + 
                    "\n2. Demo Breadth-first search" + 
                    "\n3. Read AdjList From File" +
                    //"\n3.ValidateParanthesis\n4.Demo GenericDoublyLinkedListStack\n5.TransformInfixExpressionToPostFixExpression\n6.Tower of Hanoi\n" + 
                    "\n4. Demo Depth-First Search" +
                    "\n5. Demo Topology Sorting" +
                    "\n6. Demo MST By Prim's algorithm" +
                    "\n7. Demo MST By Kruskal's algorithm" +
                    "\n8. Demo Dijkstra's algorithm" +
                    "\n9.Exit\nPlease enter a choice");
                choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        CreateGraphWithAdjList();
                        break;
                    case 2:
                        DemoBreadthFirstSearch();
                        break;
                    case 3:
                        CreateGraphWithAdjListFromFile();
                        break;
                    case 4:
                        DemoDepthFirstSearch();
                        break;
                    case 5:
                        DemoTopologySorting();
                        break;
                    case 6:
                        DemoMSTByPrimAlg();
                        break;
                    case 7:
                        DemoMSTByKruskalAlg();
                        break;
                    case 8:
                        DemoDijkstraAlg();
                        break;
                    case 9:
                        break;
                    default:
                        Console.WriteLine("Please enter a valid choice");
                        Console.ReadKey();
                        break;
                }
            } while (choice != 9);
        }

        private static void DemoDijkstraAlg()
        {
            graphWithAdjList = new GraphWithAdjacencyList();
            Console.WriteLine("Enter the Node to start with");
            char input = Console.ReadLine().Trim().ToUpper()[0];

            graphWithAdjList.GetDjikstraAlgResultOn(input);
        }

        private static void DemoMSTByKruskalAlg()
        {
            graphWithAdjList = new GraphWithAdjacencyList();
            graphWithAdjList.GetMSTByKruskalAlg();
        }

        private static void DemoMSTByPrimAlg()
        {
            graphWithAdjList = new GraphWithAdjacencyList();
            graphWithAdjList.GetMSTByPrimAlg();
        }

        private static void DemoTopologySorting()
        {
            graphWithAdjList = new GraphWithAdjacencyList();

            graphWithAdjList.GetTopologicalSort();
        }

        private static void DemoDepthFirstSearch()
        {
            graphWithAdjList = new GraphWithAdjacencyList();
            Console.WriteLine("Enter the node from which to start with");
            string input = Console.ReadLine();
            int index = input.ToUpper()[0] - 'A';
            graphWithAdjList.DepthFirstSearch(index);
        }

        private static void CreateGraphWithAdjListFromFile()
        {
            graphWithAdjList = new GraphWithAdjacencyList();
        }

        private static void DemoBreadthFirstSearch()
        {
            int n = 9;
            int[][] adjMatrix = new int[n][];
            //Console.WriteLine("Enter the adjacency matrix of the graph");
            //for (int i = 0; i < n; i++)
            //{
            //    adjMatrix[i] = new int[n];
            //    for (int j = 0; j < n; j++)
            //    {
            //        adjMatrix[i][j] = Convert.ToInt32(Console.ReadLine());
            //    }
            //}
            adjMatrix = CreateDummyAdjMatrix(n);
            BreadthFirstSearch(adjMatrix, n, 0, n- 1);
            Console.ReadKey();
        }

        private static int[][] CreateDummyAdjMatrix(int n)
        {
            int[][] adjMatrix = new int[n][];
            for (int i = 0; i < n; i++)
            {
                adjMatrix[i] = new int[n];
            }
            if (n == 5)
            {
                adjMatrix[0] = new int[] { 0, 1, 0, 1, 0 };
                adjMatrix[1] = new int[] { 1, 0, 1, 1, 0 };
                adjMatrix[2] = new int[] { 0, 1, 0, 0, 1 };
                adjMatrix[3] = new int[] { 1, 1, 0, 0, 1 };
                adjMatrix[4] = new int[] { 0, 0, 1, 1, 0 };
            }
            else if (n == 9)
            {
                adjMatrix[0] = new int[] { 0, 1, 1, 1, 0, 0, 0, 0, 0 };
                adjMatrix[1] = new int[] { 0, 0, 0, 0, 1, 0, 0, 0, 0 };
                adjMatrix[2] = new int[] { 0, 1, 0, 0, 0, 0, 1, 0, 0 };
                adjMatrix[3] = new int[] { 0, 0, 1, 0, 0, 0, 1, 0, 0 };
                adjMatrix[4] = new int[] { 0, 0, 1, 0, 0, 1, 0, 0, 0 };
                adjMatrix[5] = new int[] { 0, 0, 1, 0, 0, 0, 0, 1, 0 };
                adjMatrix[6] = new int[] { 0, 0, 0, 0, 0, 1, 0, 1, 1 };
                adjMatrix[7] = new int[] { 0, 0, 0, 0, 1, 0, 0, 0, 1 };
                adjMatrix[8] = new int[] { 0, 0, 0, 0, 0, 1, 0, 0, 0 };
            }
            

            return adjMatrix;
        }

        private static void BreadthFirstSearch(int[][] adjMatrix, int n, int origin, int dest)
        {
            bool[] visited = new bool[n];
            char lastvisitedNode = '0';

            Dictionary<char, char> nodeOriginMapping = new Dictionary<char, char>();

            //Introduced the below array for avoiding Dictionary
            int[] originArray = new int[n];
            for (int z = 0; z < n; z++)
            {
                originArray[z] = -1;
            }

            Queues.DoublyLinkedListGenericQueue<int> queue = new Queues.DoublyLinkedListGenericQueue<int>();
            int i = origin;
            while (true)
            {
                
                for (int j = 0; j < n; j++)
                {
                    if (adjMatrix[i][j] != 0)
                    {
                        if (visited[j] != true)
                        {
                            if (!nodeOriginMapping.ContainsKey((char)(j + 65)))
                            {
                                nodeOriginMapping.Add((char)(j + 65), (char)(i + 65));
                                originArray[j] = i;
                                visited[j] = true;
                                queue.push(j);
                                visited[i] = true;
                                lastvisitedNode = (char)(j + 65);
                            }
                            else
                            {
                                Console.WriteLine("Unexpected Code block BreadthFirstSearch 1");
                            }
                        }
                    }
                }

                if (queue.peek() != null)
                {
                    i = (int)queue.pop();
                }
                else
                {
                    break;
                }
            }


            //while (nodeOriginMapping.ContainsKey(lastvisitedNode))
            //{
            //    Console.Write(lastvisitedNode + "<- ");
            //    lastvisitedNode = nodeOriginMapping[lastvisitedNode];
            //}
            while (originArray[(int)lastvisitedNode - 65] != -1)
            {
                Console.Write(lastvisitedNode + "<- ");
                lastvisitedNode = (char)(originArray[(int)lastvisitedNode - 65] + 65);
            }
            Console.Write((char)(origin + 65));
        }

        private static void CreateGraphWithAdjList()
        {
            Console.WriteLine("Please enter the number of nodes present in the Graph");
            int totalNumOfNodes = Convert.ToInt32(Console.ReadLine());

            graphWithAdjList = new GraphWithAdjacencyList(totalNumOfNodes);

            graphWithAdjList.InsertNodesWithNeighboursInfo();
            Console.WriteLine("All neighbouring nodes have been inserted");
            Console.ReadKey();
        }
    }
}
