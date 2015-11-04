namespace AdvancedDataStructures
{
    using System;

    public class PriorityQueue<T> where T : IComparable
    {
        private const int InitialCapacity = 4;
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

        public int Count
        {
            get
            {
                return this.count;
            }
        }

        public int Capacity
        {
            get
            {
                return this.capacity;
            }
        }

        public void Enqueue(T item)
        {
            this.data[this.count] = item;

            if (this.count != 0)
            {
                this.CompareWithParrent(this.count);
            }

            this.count++;
            if (this.count >= this.capacity - 2)
            {
                this.IncreaseCapacity();
            }
        }

        public T Dequeue()
        {
            var deque = this.data[0];

            if (this.count > 0)
            {
                this.data[0] = this.data[this.count - 1];
                this.count--;
                this.CompareWithChilds(0);
            }

            return deque;
        }

        public override string ToString()
        {
            var copy = new T[this.count];
            Array.Copy(this.data, copy, this.count);
            return string.Format("{0}\nCount: {1}\nCapacity: {2}\n----------------------", string.Join(", ", copy), this.Count, this.Capacity);
        }

        private void CompareWithChilds(int parrentIndex)
        {
            var leftIndex = (parrentIndex * 2) + 1;
            var isLeft = leftIndex < this.count;
            var rigthIndex = (parrentIndex * 2) + 2;
            var isRigth = rigthIndex < this.count;
            var parrent = this.data[parrentIndex];
            if (isLeft && isRigth)
            {
                T left = this.data[leftIndex];
                T rigth = this.data[rigthIndex];
                if (left.CompareTo(rigth) == -1)
                {
                    if (left.CompareTo(parrent) == -1)
                    {
                        this.Swap(leftIndex, parrentIndex);
                        this.CompareWithChilds(leftIndex);
                    }
                }
                else if (rigth.CompareTo(parrent) == -1)
                {
                    this.Swap(rigthIndex, parrentIndex);
                    this.CompareWithChilds(rigthIndex);
                }
            }
            else if (isLeft)
            {
                T left = this.data[leftIndex];
                if (left.CompareTo(parrent) == -1)
                {
                    this.Swap(leftIndex, parrentIndex);
                    this.CompareWithChilds(leftIndex);
                }
            }
        }

        private void Swap(int firstIndex, int secondIndex)
        {
            var temp = this.data[firstIndex];
            this.data[firstIndex] = this.data[secondIndex];
            this.data[secondIndex] = temp;
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
