namespace _03.CountWordsFromTextFile
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Program
    {
        public static void Main(string[] args)
        {
            var separators = new char[] { ' ', ',', '!', '-', '.', '–', '?' };
            var dictionary = new Dictionary<string, int>();

            using (StreamReader streamReader = new StreamReader(@"..\..\words.txt"))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    var words = line.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var word in words)
                    {
                        var wordToLower = word.ToLower();
                        if (!dictionary.ContainsKey(wordToLower))
                        {
                            dictionary[wordToLower] = 0;
                        }

                        dictionary[wordToLower] += 1;
                    }
                }
            }

            var wordsSortedByapearence = dictionary.OrderBy(i => i.Value);
            foreach (var word in wordsSortedByapearence)
            {
                Console.WriteLine("{0} -> {1}", word.Key, word.Value);
            }
        }
    }
}
