namespace SortingAlgorithms
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class Program
    {
        private static ISorter<int> sorter;

        internal static void Main(string[] args)
        {
            var collection = new SortableCollection<int>(new[] { 1, 2, 3, 4 });
            Console.WriteLine("All items before sorting:");
            collection.PrintAllItemsOnConsole();
            Console.WriteLine();

            var copy = collection.GetCopy();
            sorter = new SelectionSorter<int>();
            Console.WriteLine("========================SelectionSorter result=================================");
            GeneratePermutationsAndSort(copy, 0);

            copy = collection.GetCopy();
            sorter = new UpgradedSelectionSorter<int>();
            Console.WriteLine("========================UpgradedSelectionSorter result=========================");
            GeneratePermutationsAndSort(copy, 0);

            copy = collection.GetCopy();
            sorter = new Quicksorter<int>();
            Console.WriteLine("========================Quicksorter result=========================");
            GeneratePermutationsAndSort(copy, 0);

            copy = collection.GetCopy();
            sorter = new MergeSorter<int>();
            Console.WriteLine("========================MergeSorter result=========================");
            GeneratePermutationsAndSort(copy, 0);

            Console.WriteLine("Linear search 101:");
            Console.WriteLine(collection.LinearSearch(101));
            Console.WriteLine();

            Console.WriteLine("Binary search 101:");
            Console.WriteLine(collection.BinarySearch(101));
            Console.WriteLine();

            Console.WriteLine("Shuffle:");
            collection.Shuffle();
            collection.PrintAllItemsOnConsole();
            Console.WriteLine();

            Console.WriteLine("Shuffle again:");
            collection.Shuffle();
            collection.PrintAllItemsOnConsole();
        }

        private static void GeneratePermutationsAndSort(SortableCollection<int> collection, int k)
        {
            if (k >= collection.Items.Count)
            {
                var copy = collection.GetCopy();
                Console.Write("Before Sort: ");
                copy.PrintAllItemsOnConsole();
                copy.Sort(sorter);
                Console.Write("After  Sort: ");
                copy.PrintAllItemsOnConsole();
                Console.WriteLine();
            }
            else
            {
                GeneratePermutationsAndSort(collection, k + 1);
                for (int i = k + 1; i < collection.Items.Count; i++)
                {
                    Swap(collection.Items, k, i);
                    GeneratePermutationsAndSort(collection, k + 1);
                    Swap(collection.Items, k, i);
                }
            }
        }

        private static void Swap<T>(IList<T> arr, int first, int second)
        {
            T oldFirst = arr[first];
            arr[first] = arr[second];
            arr[second] = oldFirst;
        }
    }
}
