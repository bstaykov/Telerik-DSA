namespace CardGame
{
    using System;
    using System.Linq;
    using System.Numerics;

    public class Program
    {
        private static int n;
        private static Node leftNode;

        public static void Main(string[] args)
        {
            ReadCards();
            CalculateMaximumPointsReachable();
        }

        private static void ReadCards()
        {
            n = int.Parse(Console.ReadLine());
            var nodeValues = Console.ReadLine()
                .Split(new char[]{ ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(i => BigInteger.Parse(i))
                .ToArray();

            leftNode = new Node(nodeValues[0]);
            var middleNode = new Node(nodeValues[1]);
            leftNode.Right = middleNode;
            middleNode.Left = leftNode;
            var rightNode = new Node(nodeValues[2]);
            middleNode.Right = rightNode;
            rightNode.Left = middleNode;

            var lastNode = rightNode;

            for (int i = 3; i < n; i++)
            {
                var newNode = new Node(nodeValues[i]);
                lastNode.Right = newNode;
                newNode.Left = lastNode;
                lastNode = newNode;
            }

            //while (leftNode.Right != null)
            //{
            //    Console.Write(leftNode.Value + " ");
            //    leftNode = leftNode.Right;
            //}
        }

        private static void CalculateMaximumPointsReachable()
        {
            BigInteger maxPoints = 0;
            while (n > 2)
            {
                var toRemoveNode = leftNode.Right;
                var minNodeVal = toRemoveNode.Value;
                // var max = toRemoveNode.Value * (toRemoveNode.Left.Value + toRemoveNode.Right.Value);
                var currentNode = leftNode.Right;

                if (n > 3)
                {
                    while (currentNode.Right != null && currentNode.Right.Right != null)
                    {
                        // && currentNode.Right.Right != null
                        if (currentNode.Right.Value < minNodeVal)
                        {
                            toRemoveNode = currentNode.Right;
                            minNodeVal = toRemoveNode.Value;
                        }
                        else if (currentNode.Right.Value == minNodeVal)
                        {
                            var left = toRemoveNode.Value * (toRemoveNode.Left.Value + toRemoveNode.Right.Value);
                            var right = currentNode.Right.Value * (currentNode.Value + currentNode.Right.Right.Value);
                            if (right > left)
                            {
                                toRemoveNode = currentNode.Right;
                                // currentNode = currentNode.Right;
                            }
                        }

                        currentNode = currentNode.Right;
                    }
                }

                maxPoints += toRemoveNode.Value * (toRemoveNode.Left.Value + toRemoveNode.Right.Value); ;
                toRemoveNode.Left.Right = toRemoveNode.Right;
                toRemoveNode.Right.Left = toRemoveNode.Left;
                n--;
            }

            Console.WriteLine(maxPoints);
        }
    }

    public class Node
    {
        public Node(BigInteger value)
        {
            this.Value = value;
        }

        public BigInteger Value { get; set; }

        public Node Left { get; set; }

        public Node Right { get; set; }
    }
}
