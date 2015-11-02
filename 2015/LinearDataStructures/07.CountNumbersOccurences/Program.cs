namespace _07.CountNumbersOccurences
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Program
    {
        public static void Main(string[] args)
        {
            List<int> numbers = new List<int>() { 3, 4, 4, 2, 3, 3, 4, 3, 2 };
            CountNumberOccurences(numbers);
        }

        private static void CountNumberOccurences(List<int> numbers)
        {
            int[] occurencess = new int[1000];
            for (int i = 0; i < numbers.Count; i++)
            {
                int currentNumber = numbers[i];
                if (occurencess[currentNumber] == 0)
                {
                    int occurenceCount = numbers.Where(n => n == currentNumber).Count();
                    occurencess[currentNumber] = occurenceCount;
                }
            }

            for (int i = 0; i < occurencess.Length; i++)
            {
                if (occurencess[i] != 0)
                {
                    Console.WriteLine("{0} → {1} times", i, occurencess[i]);
                }
            }
        }
    }
}
