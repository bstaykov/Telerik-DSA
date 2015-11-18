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
        private static int minRange = 0;
        private static int maxRange = 1000;
        private static int searches = 5;
        private static int shuffles = 10;

        public static void Main(string[] args)
        {
            TestLargeCollections(1000);
            SortableCollection<int> collection = GenerateList(5);
            TestSortersAllPermutations(collection);
            collection = GenerateList(1000);
            TestSearchAlgorithms(collection, searches);

            collection = GenerateList(10);
            TestShuffle(collection, shuffles);
        }

        private static void TestShuffle(SortableCollection<int> collection, int shuffles)
        {
            Console.Write("\nOriginal collection: ");
            collection.PrintAllItemsOnConsole();
            for (int i = 0; i < shuffles; i++)
            {
                var copy = collection.GetCopy();
                Console.WriteLine("\nShuffle...");
                copy.Shuffle();
                copy.PrintAllItemsOnConsole();
            }
        }

        private static void TestSearchAlgorithms(SortableCollection<int> collection, int searches)
        {
            collection.Sort(new QuickSorter<int>());
            for (int i = 0; i < searches; i++)
            {
                var searchedNumber = GetRandomNumber();
                Console.WriteLine("Linear search: {0} Found: {1}", searchedNumber, collection.LinearSearch(searchedNumber));
                Console.WriteLine("Binary search: {0} Found: {1}", searchedNumber, collection.BinarySearch(searchedNumber));
            }
        }

        private static void TestSortersAllPermutations(SortableCollection<int> collection)
        {
            Console.WriteLine("All items before sorting:");
            collection.PrintAllItemsOnConsole();
            Console.WriteLine();
            Console.WriteLine("Calculating time (Permutation without repetitions) ...");

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
            sorter = new MergeInsertionSorter<int>();
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
        }

        private static SortableCollection<int> GenerateList(int length)
        {
            var collection = new SortableCollection<int>();
            for (int i = 0; i < length; i++)
            {
                collection.Items.Add(GetRandomNumber());
            }

            return collection;
        }

        private static int GetRandomNumber()
        {
            return random.Next(minRange, maxRange);
        }

        private static void TestLargeCollections(int length)
        {
            var sorters = new ISorter<int>[] 
            { 
                new SelectionSorter<int>(),
                new UpgradedSelectionSorter<int>(),
                new QuickSorter<int>(),
                new MergeSorter<int>(),
                new MergeInsertionSorter<int>(),
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
