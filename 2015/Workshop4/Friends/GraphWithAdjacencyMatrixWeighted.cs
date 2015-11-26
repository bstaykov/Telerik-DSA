//namespace Friends
//{
//    using System;
//    using System.Collections.Generic;
//    using System.Linq;

//    public class GraphWithAdjacencyMatrixWeighted
//    {
//        private char[] edgesSeparators = new char[] { '\n', '\r' };
//        private char[] separators = new char[] { ' ' };
//        private int n;
//        private int[,] matrixStartTown1;
//        private int[,] matrixStartTown2;
//        private int[,] matrixEndTown2;
//        private int[,] matrixEndTown1;
//        private int[,] matrixTown1Town2;
//        private int startTown;
//        private int endTown;
//        private int town1;
//        private int town2;


//        public GraphWithAdjacencyMatrixWeighted()
//        {
//            this.ParseMatrix();
//        }

//        public int CalculateShortestPath()
//        {
//            var pathFromTown1Town2 = DistanceBetweenNodes(this.matrixTown1Town2, this.town1, this.town2);
//            var pathStartTown1PlusTown2End = DistanceBetweenNodes(this.matrixStartTown1, this.startTown, this.town1) + DistanceBetweenNodes(this.matrixEndTown2, this.town2, this.endTown);
//            var pathStartTown2PlusTown1End = DistanceBetweenNodes(this.matrixStartTown2, this.startTown, this.town2) + DistanceBetweenNodes(this.matrixEndTown1, this.town1, this.endTown);
//            int minPath = pathFromTown1Town2;
//            if (pathStartTown1PlusTown2End < pathStartTown2PlusTown1End)
//            {
//                minPath += pathStartTown1PlusTown2End;
//            }
//            else
//            {
//                minPath += pathStartTown2PlusTown1End;
//            }

//            return minPath;
//        }

//        // Dijkstra
//        private int DistanceBetweenNodes(int[,] matrix, int startNode, int endNode)
//        {
//            int[] distance = new int[matrix.GetLength(0)];
//            HashSet<int> nodes = new HashSet<int>();

//            for (int i = 0; i < matrix.GetLength(0); i++)
//            {
//                distance[i] = int.MaxValue;
//                nodes.Add(i);
//            }

//            distance[startNode] = 0;

//            while (nodes.Count != 0)
//            {
//                int minNode = int.MaxValue;

//                foreach (var node in nodes)
//                {
//                    if (minNode > distance[node])
//                    {
//                        minNode = node;
//                    }
//                }

//                nodes.Remove(minNode);

//                if (minNode == int.MaxValue)
//                {
//                    break;
//                }

//                for (int i = 0; i < matrix.GetLength(0); i++)
//                {
//                    if (matrix[minNode, i] > 0)
//                    {
//                        int potentialDistance = distance[minNode] + matrix[minNode, i];
//                        if (potentialDistance < distance[i])
//                        {
//                            distance[i] = potentialDistance;
//                        }
//                    }
//                }
//            }

//            return distance[endNode];
//        }

//        public void Print(int[,] matrix)
//        {
//            Console.Write("\t");
//            for (int i = 0; i < this.n; i++)
//            {
//                Console.Write("{0}\t", i + 1);
//            }

//            Console.WriteLine();

//            for (int v1 = 0; v1 < matrix.GetLength(0); v1++)
//            {
//                Console.Write("{0}|\t", v1 + 1);
//                for (int v2 = 0; v2 < matrix.GetLength(1); v2++)
//                {
//                    Console.Write("{0}\t", matrix[v1, v2]);
//                }

//                Console.WriteLine();
//            }
//        }

//        private void ParseMatrix()
//        {
//            var townsAndEdgesCount = Console.ReadLine().Split(new char[]{' '}, StringSplitOptions.RemoveEmptyEntries);
//            n = int.Parse(townsAndEdgesCount[0]);
//            var m = int.Parse(townsAndEdgesCount[1]);
//            matrixStartTown1 = new int[n, n];
//            matrixStartTown2 = new int[n, n];
//            matrixEndTown2 = new int[n, n];
//            matrixEndTown1 = new int[n, n];
//            matrixTown1Town2 = new int[n, n];

//            var startEndTown = Console.ReadLine().Split(this.separators, StringSplitOptions.RemoveEmptyEntries);
//            this.startTown = int.Parse(startEndTown[0]) - 1;
//            this.endTown = int.Parse(startEndTown[1]) - 1;

//            var towns = Console.ReadLine().Split(this.separators, StringSplitOptions.RemoveEmptyEntries);
//            this.town1 = int.Parse(towns[0]) - 1;
//            this.town2 = int.Parse(towns[1]) - 1;

//            for (int i = 0; i < m; i++)
//            {
//                var edgeString = Console.ReadLine().Split(this.separators, StringSplitOptions.RemoveEmptyEntries);
                
//                var v1 = int.Parse(edgeString[0]) - 1;
//                var v2 = int.Parse(edgeString[1]) - 1;
//                var weight = int.Parse(edgeString[2]);

//                if (v1 != town2 && v1 != endTown && v2 != town2 && v2 != endTown)
//                {
//                    // start - town1
//                    this.matrixStartTown1[v1, v2] = weight;
//                    this.matrixStartTown1[v2, v1] = weight;
//                }

//                if (v1 != town1 && v1 != endTown && v2 != town1 && v2 != endTown)
//                {
//                    // start - town2
//                    this.matrixStartTown2[v1, v2] = weight;
//                    this.matrixStartTown2[v2, v1] = weight;
//                }

//                if (v1 != town2 && v1 != startTown && v2 != town2 && v2 != startTown)
//                {
//                    // end - town1
//                    this.matrixEndTown1[v1, v2] = weight;
//                    this.matrixEndTown1[v2, v1] = weight;
//                }

//                if (v1 != town1 && v1 != startTown && v2 != town1 && v2 != startTown)
//                {
//                    // end - town2
//                    this.matrixEndTown2[v1, v2] = weight;
//                    this.matrixEndTown2[v2, v1] = weight;
//                }

//                if (v1 != startTown && v1 != endTown && v2 != startTown && v2 != endTown)
//                {
//                    // town1 - town2
//                    this.matrixTown1Town2[v1, v2] = weight;
//                    this.matrixTown1Town2[v2, v1] = weight;
//                }

//                //this.matrixTown1Town2[v1, v2] = weight;
//                //this.matrixTown1Town2[v2, v1] = weight;
//            }
//        }
//    }
//}