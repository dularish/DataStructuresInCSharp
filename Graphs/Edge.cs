using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Graphs
{
    public class Edge
    {
        public int FirstVertex;
        public int SecondVertex;
        public bool Directed;
        public int Length;

        public Edge(int firstvertex, int secondvertex, int length, bool directed = false)
        {
            if (firstvertex == secondvertex || length == 0)
            {
                throw new Exception("Invalid edge");
            }

            FirstVertex = firstvertex;
            SecondVertex = secondvertex;
            Length = length;
            Directed = directed;
        }
    }
}
