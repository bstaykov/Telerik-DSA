namespace Palindromize
{
    using System;
    using System.Linq;

    public class Program
    {
        public static void Main(string[] args)
        {
            string input = Console.ReadLine();
            string reversed = string.Join(string.Empty, input.Reverse());

            if (IsPalindrom(input))
            {
                Console.WriteLine(input);
                return;
            }

            for (int i = 0; i < input.Length - 1; i++)
            {
                var palindromeCandidate = input + reversed.Substring(input.Length - 1 - i, 1 + i);
                if (IsPalindrom(palindromeCandidate))
                {
                    Console.WriteLine(palindromeCandidate);
                    return;
                }
            }
        }

        private static bool IsPalindrom(string palindromeCandidate)
        {
            for (int i = 0; i < palindromeCandidate.Length / 2; i++)
            {
                if (palindromeCandidate[i] != palindromeCandidate[palindromeCandidate.Length - 1 - i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
