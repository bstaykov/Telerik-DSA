﻿namespace Friends
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Program
    {
        public static void Main(string[] args)
        {
            GraphWithAdjacencyMatrixWeighted graphWithAdjacencyMatrixWeighted = new GraphWithAdjacencyMatrixWeighted();
            Console.WriteLine(graphWithAdjacencyMatrixWeighted.CalculateShortestPath());
        }
    }
    public class GraphWithAdjacencyMatrixWeighted
    {
        private char[] edgesSeparators = new char[] { '\n', '\r' };
        private char[] separators = new char[] { ' ' };
        private int n;
        private bool[,] matrixStartTown1;
        private bool[,] matrixStartTown2;
        private bool[,] matrixEndTown2;
        private bool[,] matrixEndTown1;
        private bool[,] matrixTown1Town2;
        private int[,] weights;
        private int startTown;
        private int endTown;
        private int town1;
        private int town2;


        public GraphWithAdjacencyMatrixWeighted()
        {
            this.ParseMatrix();
        }

        public long CalculateShortestPath()
        {
            var pathFromTown1Town2 = DistanceBetweenNodes(this.matrixTown1Town2, this.town1, this.town2);
            var pathStartTown1PlusTown2End = DistanceBetweenNodes(this.matrixStartTown1, this.startTown, this.town1) + DistanceBetweenNodes(this.matrixEndTown2, this.town2, this.endTown);
            var pathStartTown2PlusTown1End = DistanceBetweenNodes(this.matrixStartTown2, this.startTown, this.town2) + DistanceBetweenNodes(this.matrixEndTown1, this.town1, this.endTown);
            long minPath = pathFromTown1Town2;
            if (pathStartTown1PlusTown2End < pathStartTown2PlusTown1End)
            {
                minPath += pathStartTown1PlusTown2End;
            }
            else
            {
                minPath += pathStartTown2PlusTown1End;
            }

            return minPath;
        }

        // Dijkstra
        private long DistanceBetweenNodes(bool[,] matrix, int startNode, int endNode)
        {
            long[] distance = new long[matrix.GetLength(0)];
            HashSet<long> nodes = new HashSet<long>();

            for (long i = 0; i < matrix.GetLength(0); i++)
            {
                distance[i] = long.MaxValue;
                nodes.Add(i);
            }

            distance[startNode] = 0;

            while (nodes.Count != 0)
            {
                long minNode = long.MaxValue;

                foreach (var node in nodes)
                {
                    if (minNode > distance[node])
                    {
                        minNode = node;
                    }
                }

                nodes.Remove(minNode);

                if (minNode == long.MaxValue)
                {
                    break;
                }

                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    if (matrix[minNode, i] && weights[minNode, i] > 0)
                    {
                        long potentialDistance = distance[minNode] + weights[minNode, i];
                        if (potentialDistance < distance[i])
                        {
                            distance[i] = potentialDistance;
                        }
                    }
                }
            }

            return distance[endNode];
        }

        public void Print(int[,] matrix)
        {
            Console.Write("\t");
            for (int i = 0; i < this.n; i++)
            {
                Console.Write("{0}\t", i + 1);
            }

            Console.WriteLine();

            for (int v1 = 0; v1 < matrix.GetLength(0); v1++)
            {
                Console.Write("{0}|\t", v1 + 1);
                for (int v2 = 0; v2 < matrix.GetLength(1); v2++)
                {
                    Console.Write("{0}\t", matrix[v1, v2]);
                }

                Console.WriteLine();
            }
        }

        private void ParseMatrix()
        {
            var townsAndEdgesCount = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            n = int.Parse(townsAndEdgesCount[0]);
            var m = int.Parse(townsAndEdgesCount[1]);
            matrixStartTown1 = new bool[n, n];
            matrixStartTown2 = new bool[n, n];
            matrixEndTown2 = new bool[n, n];
            matrixEndTown1 = new bool[n, n];
            matrixTown1Town2 = new bool[n, n];
            weights = new int[n, n];

            var startEndTown = Console.ReadLine().Split(this.separators, StringSplitOptions.RemoveEmptyEntries);
            this.startTown = int.Parse(startEndTown[0]) - 1;
            this.endTown = int.Parse(startEndTown[1]) - 1;

            var towns = Console.ReadLine().Split(this.separators, StringSplitOptions.RemoveEmptyEntries);
            this.town1 = int.Parse(towns[0]) - 1;
            this.town2 = int.Parse(towns[1]) - 1;

            for (int i = 0; i < m; i++)
            {
                var edgeString = Console.ReadLine().Split(this.separators, StringSplitOptions.RemoveEmptyEntries);

                var v1 = int.Parse(edgeString[0]) - 1;
                var v2 = int.Parse(edgeString[1]) - 1;
                var weight = int.Parse(edgeString[2]);

                if (v1 != town2 && v1 != endTown && v2 != town2 && v2 != endTown)
                {
                    // start - town1
                    this.matrixStartTown1[v1, v2] = true;
                    this.matrixStartTown1[v2, v1] = true;
                }

                if (v1 != town1 && v1 != endTown && v2 != town1 && v2 != endTown)
                {
                    // start - town2
                    this.matrixStartTown2[v1, v2] = true;
                    this.matrixStartTown2[v2, v1] = true;
                }

                if (v1 != town2 && v1 != startTown && v2 != town2 && v2 != startTown)
                {
                    // end - town1
                    this.matrixEndTown1[v1, v2] = true;
                    this.matrixEndTown1[v2, v1] = true;
                }

                if (v1 != town1 && v1 != startTown && v2 != town1 && v2 != startTown)
                {
                    // end - town2
                    this.matrixEndTown2[v1, v2] = true;
                    this.matrixEndTown2[v2, v1] = true;
                }

                if (v1 != startTown && v1 != endTown && v2 != startTown && v2 != endTown)
                {
                    // town1 - town2
                    this.matrixTown1Town2[v1, v2] = true;
                    this.matrixTown1Town2[v2, v1] = true;
                }

                //this.matrixTown1Town2[v1, v2] = true;
                //this.matrixTown1Town2[v2, v1] = true;

                this.weights[v1, v2] = weight;
                this.weights[v2, v1] = weight;
            }
        }
    }
}
