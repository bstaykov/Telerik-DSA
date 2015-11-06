using System;
namespace _03.BiDictionary
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BiDictionary<int, string, string> biDictionary = new BiDictionary<int, string, string>();
            var key1 = 1;
            var key2 = "2";
            biDictionary.Add(key1, key2, "default value");
            Console.WriteLine("==========Find(key1)==========");
            Console.WriteLine(biDictionary.Find(key1));
            Console.WriteLine("==========Find(key2)==========");
            Console.WriteLine(biDictionary.Find(key2));
            Console.WriteLine("==========Find(key1, key2)==========");
            Console.WriteLine(biDictionary.Find(key1, key2));
        }
    }
}
