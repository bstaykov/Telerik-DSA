namespace _08.Majorant
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Program
    {
        public static void Main(string[] args)
        {
            int[] numbers = new int[] { 2, 2, 3, 3, 2, 3, 4, 3, 3 };
            FindMajorant(numbers);
        }

        private static void FindMajorant(int[] numbers)
        {
            var majorants = new List<int>();
            int length = numbers.Length;
            int minLengthForMajorant = (length / 2) + 1;

            for (int i = 0; i < length; i++)
            {
                var number = numbers[i];
                if (majorants.Contains(number) == false)
                {
                    var occurence = numbers.Where(n => n == number).Count();
                    majorants.Add(number);
                    if (occurence >= minLengthForMajorant)
                    {
                        Console.WriteLine("Majorant: {0} Occurence: {1}", number, occurence);
                    }
                }
            }
        }
    }
}
