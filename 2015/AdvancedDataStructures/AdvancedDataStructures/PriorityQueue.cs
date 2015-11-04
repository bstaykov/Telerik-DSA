namespace AdvancedDataStructures
{
    using System;

    public class PriorityQueue<T> where T : IComparable
    {
        private const int InitialCapacity = 16;
        private T[] data;
        private int capacity;
        private int count;

        public PriorityQueue()
            : this(InitialCapacity)
        {
        }

        public PriorityQueue(int capacity)
        {
            this.capacity = capacity;
            this.data = new T[capacity];
            this.count = 0;
        }

        public void Enqueue(T item)
        {
            this.data[this.count] = item;

            if (this.count != 0)
            {
                this.CompareWithParrent(this.count);
            }

            this.count++;
            if (this.count == this.capacity - 1)
            {
                this.IncreaseCapacity();
            }
        }

        public override string ToString()
        {
            return string.Join(", ", this.data);
        }

        private void CompareWithParrent(int index)
        {
            var parrentIndex = (index - 1) / 2;
            if (this.data[parrentIndex].CompareTo(this.data[index]) == 1)
            {
                var tempItem = this.data[parrentIndex];
                this.data[parrentIndex] = this.data[index];
                this.data[index] = tempItem;

                if (parrentIndex != 0)
                {
                    this.CompareWithParrent(parrentIndex);
                }
            }
        }

        private void IncreaseCapacity()
        {
            var newCapacity = this.capacity * 2;
            var newData = new T[newCapacity];
            Array.Copy(this.data, newData, this.count);
            this.data = newData;
            this.capacity = newCapacity;
        }
    }
}
