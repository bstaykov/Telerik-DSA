namespace SearchingAlgorithmsTests
{
    using System;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using SortingAlgorithms;
    
    [TestClass]
    public class SearchingTests
    {
        private static SortableCollection<int> collection;

        [TestInitialize]
        public void InitMethod()
        {
            collection = new SortableCollection<int>(new List<int>() { 4, 6, 1, 9, 21, 34, 54, 5, 23 });
        }

        [TestMethod]
        public void Test_LinearSearch_ShouldReturnTrue_WhenItemIsContainedInTheCollection()
        {
            var isContained = collection.LinearSearch(1);
            Assert.IsTrue(isContained, "LinearSearch should return true when element is contained in the collection.");
        }

        [TestMethod]
        public void Test_LinearSearch_ShouldReturnFalse_WhenItemIsNotContainedInTheCollection()
        {
            var isContained = collection.LinearSearch(0);
            Assert.IsFalse(isContained, "LinearSearch should return false when element is not contained in the collection.");
        }

        [TestMethod]
        public void Test_BinarySearch_ShouldReturnTrue_WhenItemIsContainedInTheCollection()
        {
            var isContained = collection.BinarySearch(1);
            Assert.IsTrue(isContained, "BinarySearch should return true when element is contained in the collection.");
        }

        [TestMethod]
        public void Test_BinarySearch_ShouldReturnFalse_WhenItemIsNotContainedInTheCollection()
        {
            var isContained = collection.BinarySearch(0);
            Assert.IsFalse(isContained, "BinarySearch should return false when element is not contained in the collection.");
        }

        [TestMethod]
        public void Test_BinarySearch_ShouldSortCollection_OnSearch()
        {
            collection.BinarySearch(0);
            bool isSortedCorrectly = true;
            for (int i = 0; i < collection.Items.Count - 1; i++)
            {
                if (collection.Items[i].CompareTo(collection.Items[i + 1]) > 0)
                {
                    isSortedCorrectly = false;
                    break;
                }
            }

            var col = string.Join(", ", collection.Items);

            Assert.IsTrue(isSortedCorrectly, "BinarySearch should sort collection on search.");
        }
    }
}
