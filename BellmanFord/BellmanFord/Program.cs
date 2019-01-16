using System;
using System.Collections.Generic;
using System.IO;

namespace BellmanFord
{
    class Program
    {
        static void Main(string[] args)
        {
            //TestBellman();
            Graph g = new Graph();
            g.ReadGraph("graph.txt");
            g.BellmanFord(0);
        }

        private static void TestBellman()
        {
            Graph g = new Graph(5);
            g.AddDirectedEdge(0, 1, 6);
            g.AddDirectedEdge(0, 3, 7);
            g.AddDirectedEdge(4, 0, 2);
            g.AddDirectedEdge(1, 2, 5);
            g.AddDirectedEdge(2, 1, -2);
            g.AddDirectedEdge(1, 3, 8);
            g.AddDirectedEdge(3, 2, -3);
            g.AddDirectedEdge(1, 4, -4);
            g.AddDirectedEdge(3, 4, 9);
            g.AddDirectedEdge(4, 2, 7);

            g.BellmanFord(0);
        }
    }

    public class Graph
    {
        bool isInitialized = false; // to check wether graph initialized or not
        int numberOfVerticies;
        int numberOfEdges;
        private int[,] adjMatrix;

        // allowing to create the graph without providing number of edges
        public Graph()
        {

        }

        public Graph(int numOfVertices)
        {
            this.numberOfVerticies = numOfVertices;
            Initialize();
        }

        private void Initialize()
        {
            isInitialized = true;
            adjMatrix = new int[numberOfVerticies, numberOfVerticies];
            this.numberOfEdges = 0;
        }

        //  This method is just to demonstrate the reading of a graph from a text file
        public void ReadGraph(string filename)
        {
            StreamReader sr = new StreamReader(filename);
            string[] allLines = sr.ReadToEnd().Split('\n');
            sr.Close(); // no more need the file

            int[,] allEdges = new int[allLines.Length, 3];
            int maxVertId = 0;

            for (int i = 0; i < allLines.Length; i++)
            {
                string s = allLines[i];
                string[] data = s.Split(' ');
                allEdges[i, 0] = Convert.ToInt32(data[0]);
                allEdges[i, 1] = Convert.ToInt32(data[1]);
                allEdges[i, 2] = Convert.ToInt32(data[2]);

                if (maxVertId < allEdges[i, 0])
                    maxVertId = allEdges[i, 0];
                if (maxVertId < allEdges[i, 1])
                    maxVertId = allEdges[i, 1];
            }

            // our indexes starts from 0
            this.numberOfVerticies = maxVertId + 1;
            Initialize(); // to reset adjacency matrix and edge count

            // creating the graph
            for (int i = 0; i < allEdges.GetUpperBound(0); i++)
            {
                this.AddDirectedEdge(allEdges[i, 0], allEdges[i, 1], allEdges[i, 2]);
            }

        }

        public void AddDirectedEdge(int source, int destination, int weight = 0)
        {
            CheckInitialization();

            adjMatrix[source, destination] = weight;
            this.numberOfEdges++;
        }

        private void CheckInitialization()
        {
            if (!isInitialized)
                throw new Exception("Graph is not initialized.");
        }

        public int[,] GetAllEdges()
        {
            CheckInitialization();

            int[,] edges = new int[numberOfEdges, 3];

            int count = 0;
            for (int i = 0; i < numberOfVerticies; i++)
            {
                for (int j = 0; j < numberOfVerticies; j++)
                {
                    if (adjMatrix[i, j] != 0)
                    {
                        edges[count, 0] = i;
                        edges[count, 1] = j;
                        edges[count, 2] = adjMatrix[i, j];
                        count++;
                    }

                }
            }

            return edges;
        }

        public int[] GetDirectedNeighbours(int source)
        {
            CheckInitialization();

            List<int> neg = new List<int>();

            for (int i = 0; i < numberOfVerticies; i++)
            {
                if (adjMatrix[source, i] > 0)
                    neg.Add(i);
            }

            return neg.ToArray();
        }

        public bool BellmanFord(int source)
        {
            CheckInitialization();

            int[] distance = new int[numberOfVerticies];

            // initialize all vertices to infinite
            for (int i = 0; i < numberOfVerticies; i++)
                distance[i] = int.MaxValue;
            // set source to 0
            distance[source] = 0;

            // for the simplicity, we are using the following method
            // however, this method cause O(n^2) for Bellman Ford
            // but can be avoided by simply storing each edge at time of insertion
            int[,] edges = GetAllEdges();

            // actual algo
            for (int i = 0; i < numberOfVerticies - 1; i++)
            {
                int[] lastDistance = distance;
                for (int e = 0; e < numberOfEdges; e++)
                {
                    int s = edges[e, 0];
                    int d = edges[e, 1];
                    int w = edges[e, 2];
                    if (distance[s] != int.MaxValue && lastDistance[d] > distance[s] + w)
                        distance[d] = distance[s] + w;
                }
            }

            for (int e = 0; e < numberOfEdges; e++)
            {
                int s = edges[e, 0];
                int d = edges[e, 1];
                int w = edges[e, 2];
                if (distance[d] > distance[s] + w)
                    return false;
            }

            for (int i = 0; i < distance.Length; i++)
                Console.WriteLine("Vertex:{0} Distance:{1}", i, distance[i]);

            return true;
        }
    }
}
