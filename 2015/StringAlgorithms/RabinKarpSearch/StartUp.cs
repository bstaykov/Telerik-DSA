namespace RabinKarpSearch
{
    using System;    

    public class StartUp
    {
        public static void Main()
        {
            string text = "dwadfrg fefe abc123456 e  da abc123456 e dawd abc123456 efrfgr";
            string pattern = "abc123456";

            int textLength = text.Length;
            int patternLength = pattern.Length;

            if (patternLength > textLength)
            {
                return;
            }

            Hash.ComputePowers(patternLength);

            Hash hpattern = new Hash(pattern);
            Hash hwindow = new Hash(text.Substring(0, patternLength));

            if (hpattern.Value == hwindow.Value)
            {
                Console.WriteLine("Math at 0");
            }

            for (int i = 1; i <= textLength - patternLength; i++)
            {
                hwindow.Add(text[i + patternLength - 1]);
                hwindow.Remove(text[i - 1], patternLength);

                if (hpattern.Value == hwindow.Value)
                {
                    Console.WriteLine("Math at {0}", i);
                }
            }
        }
    }
}
