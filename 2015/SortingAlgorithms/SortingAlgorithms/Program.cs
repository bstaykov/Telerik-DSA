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
        private static Stopwatch stopWatch = new Stopwatch();
        private static Random random = new Random();

        internal static void Main(string[] args)
        {
            TestSortersTime(10000);

            var length = 5;
            var collection = new SortableCollection<int>();
            for (int i = 0; i < length; i++)
            {
                collection.Items.Add(random.Next(0, 10000));
            }

            Console.WriteLine("All items before sorting:");
            collection.PrintAllItemsOnConsole();
            Console.WriteLine();

            Console.WriteLine("Calculating time (Permutation without repetitions for {0} numbers) ...", length);

            var copy = collection.GetCopy();
            stopWatch.Reset();
            sorter = new SelectionSorter<int>();
            Console.WriteLine("========================SelectionSorter result=================================");
            GeneratePermutationsAndSort(copy, 0);
            Console.WriteLine("SelectionSorter:    " + stopWatch.Elapsed);
            Console.WriteLine();

            copy = collection.GetCopy();
            stopWatch.Reset();
            sorter = new UpgradedSelectionSorter<int>();
            Console.WriteLine("========================UpgradedSelectionSorter result=========================");
            GeneratePermutationsAndSort(copy, 0);
            Console.WriteLine("UpgradedSelection  :" + stopWatch.Elapsed);
            Console.WriteLine();

            copy = collection.GetCopy();
            stopWatch.Reset();
            sorter = new QuickSorter<int>();
            Console.WriteLine("========================Quicksorter result=========================");
            GeneratePermutationsAndSort(copy, 0);
            Console.WriteLine("Quicksorter:        " + stopWatch.Elapsed);
            Console.WriteLine();

            copy = collection.GetCopy();
            stopWatch.Reset();
            sorter = new MergeSorter<int>();
            Console.WriteLine("========================MergeSorter result=========================");
            GeneratePermutationsAndSort(copy, 0);
            Console.WriteLine("MergeSorter:        " + stopWatch.Elapsed);
            Console.WriteLine();

            copy = collection.GetCopy();
            stopWatch.Reset();
            sorter = new MergeInsertSorter<int>();
            Console.WriteLine("========================MergeInsertSorter result=========================");
            GeneratePermutationsAndSort(copy, 0);
            Console.WriteLine("MergeInsertSorter:  " + stopWatch.Elapsed);
            Console.WriteLine();

            copy = collection.GetCopy();
            stopWatch.Reset();
            sorter = new InsertionSorter<int>();
            Console.WriteLine("========================Insertion result=========================");
            GeneratePermutationsAndSort(copy, 0);
            Console.WriteLine("Insertion:          " + stopWatch.Elapsed);
            Console.WriteLine();

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

        private static void TestSortersTime(int length)
        {
            var sorters = new ISorter<int>[] 
            { 
                new SelectionSorter<int>(),
                new UpgradedSelectionSorter<int>(),
                new QuickSorter<int>(),
                new MergeSorter<int>(),
                new MergeInsertSorter<int>(),
                new InsertionSorter<int>(),
            };

            var collection = new SortableCollection<int>();
            for (int i = 0; i < length; i++)
            {
                collection.Items.Add(random.Next(0, 10000));
            }

            var sw = new Stopwatch();
            foreach (var currentSorter in sorters)
            {
                var copy = collection.GetCopy();
                sw.Reset();
                sw.Start();
                copy.Sort(currentSorter);
                sw.Stop();
                Console.WriteLine(currentSorter.GetType().Name + ": " + sw.Elapsed);
            }
        }

        private static void GeneratePermutationsAndSort(SortableCollection<int> collection, int k)
        {
            if (k >= collection.Items.Count)
            {
                var copy = collection.GetCopy();

                // Console.Write("Before Sort: ");
                // copy.PrintAllItemsOnConsole();
                stopWatch.Start();
                copy.Sort(sorter);
                stopWatch.Stop();

                // Console.Write("After  Sort: ");
                // copy.PrintAllItemsOnConsole();
                // Console.WriteLine();
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
