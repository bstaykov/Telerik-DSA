namespace Task5
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;

    public class Program
    {
        private static int n;
        private static char[] directions;
        private static ulong k;
        private static int[] password;
        private static ulong counter = 0;

        public static void Main(string[] args)
        {
            ReadInput();
            FindPossiblePasswords();
        }

        private static void FindPossiblePasswords()
        {
            for (int i = 0; i < 10; i++)
            {
                password[0] = i;
                EvalPossibleCombinations(i, 0);
            }
        }

        private static void EvalPossibleCombinations(int currentDigit, int directionIndex)
        {
            if (counter == k)
            {
                return;
            }

            if (directionIndex >= n)
            {
                return;
            }

            password[directionIndex] = currentDigit;
            if (directionIndex == n - 1)
            {
                counter++;
                if (counter == k)
                {
                    Console.WriteLine(BigInteger.Parse(string.Join(string.Empty, password)).ToString().PadLeft(n, '0'));
                }

                return;
            }

            var direction = directions[directionIndex];

            if (direction == '=')
            {
                EvalPossibleCombinations(currentDigit, directionIndex + 1);
            }
            else if (direction == '>')
            {
                if (currentDigit == 0)
                {
                    return;
                }

                EvalPossibleCombinations(0, directionIndex + 1);

                for (int i = currentDigit + 1; i < 10; i++)
                {
                    EvalPossibleCombinations(i, directionIndex + 1);
                }
            }
            else if (direction == '<')
            {
                if (currentDigit == 0)
                {
                    for (int i = 1; i <= 9; i++)
                    {
                        EvalPossibleCombinations(i, directionIndex + 1);
                    }
                }
                else
                {
                    for (int i = 1; i < currentDigit; i++)
                    {
                        EvalPossibleCombinations(i, directionIndex + 1);
                    }
                }
            }
        }

        private static void TestPrintInput()
        {
            Console.WriteLine(n);

            Console.WriteLine(string.Join(" ", directions));

            Console.WriteLine(k);
        }

        private static void ReadInput()
        {
            n = int.Parse(Console.ReadLine());
            directions = new char[n - 1];
            password = new int[n];
            directions = Console.ReadLine().ToArray();
            k = ulong.Parse(Console.ReadLine());
        }
    }
}