using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dijsktra
{
    class Program
    {
        static void Main(string[] args)
        {
            Graph g = new Graph(5);
            g.AddDirectedEdge(0, 1, 10);
            g.AddDirectedEdge(0, 2, 5);
            g.AddDirectedEdge(1, 3, 1);
            g.AddDirectedEdge(1, 2, 2);
            g.AddDirectedEdge(2, 1, 3);
            g.AddDirectedEdge(2, 3, 9);
            g.AddDirectedEdge(2, 4, 2);
            g.AddDirectedEdge(3, 4, 4);
            g.AddDirectedEdge(4, 3, 6);
            g.AddDirectedEdge(4, 0, 7);

            g.DijkstraShortestPath(0);

        }
    }

    public class Vertex
    {
        public int id;
        public int distance;
        public bool visited = false;

        public Vertex(int id, int distance)
        {
            this.id = id;
            this.distance = distance;
        }
    }

    public class PriorityQueue
    {
        List<Vertex> list = new List<Vertex>();

        public void Enqueue(Vertex e)
        {
            list.Add(e);
        }

        public Vertex ExtractMin()
        {
            int minLoc = FindMin();
            Vertex v = list[minLoc];
            list.RemoveAt(minLoc);

            return v;
        }

        private int FindMin()
        {
            if (list.Count < 1)
                return -1;

            Vertex min = list[0];

            int loc = 0;

            for (int i = 1; i < list.Count; i++)
            {
                if (list[i].distance < min.distance)
                {
                    min = list[i];
                    loc = i;
                }
            }

            return loc;
        }

        public bool IsEmpty()
        {
            return list.Count() == 0;
        }
    }

    public class Graph
    {
        int numberOfVerticies;
        private int[,] adjMatrix;

        public Graph(int numOfVertices)
        {
            this.numberOfVerticies = numOfVertices;
            adjMatrix = new int[numberOfVerticies, numberOfVerticies];
        }

        public void AddDirectedEdge(int source, int destination, int weight = 0)
        {
            adjMatrix[source, destination] = weight;
        }

        public int[] GetDirectedNeighbours(int source)
        {
            List<int> neg = new List<int>();

            for (int i = 0; i < numberOfVerticies; i++)
            {
                if (adjMatrix[source, i] > 0)
                    neg.Add(i);
            }

            return neg.ToArray();
        }

        public void DijkstraShortestPath(int source)
        {
            PriorityQueue pq = new PriorityQueue();
            Vertex[] distanceVector = new Vertex[numberOfVerticies];
            int[] visited = new int[numberOfVerticies];

            // intialization
            for (int i = 0; i < numberOfVerticies; i++)
            {
                distanceVector[i] = new Vertex(i, int.MaxValue); 
            }
            distanceVector[source].distance = 0;

            // addign in queue
            for (int i = 0; i < numberOfVerticies; i++)
                pq.Enqueue(distanceVector[i]);

            while (!pq.IsEmpty())
            {
                Vertex v = pq.ExtractMin();
                int[] neg = GetDirectedNeighbours(v.id);

                foreach (int n in neg)
                {
                    if (distanceVector[n].distance > v.distance + adjMatrix[v.id, n])
                        distanceVector[n].distance = v.distance + adjMatrix[v.id, n];
                }
            }

            foreach (Vertex x in distanceVector)
                Console.WriteLine("{0},{1}", x.id, x.distance);
        }
    }
}
