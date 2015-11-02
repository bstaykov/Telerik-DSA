namespace HashTablesAndSets
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Hashset<K>
    {
        private const int InitialCapacity = 16;
        private HashTable<K, K> data;

        public Hashset(int capacity = InitialCapacity)
        {
            this.data = new HashTable<K, K>(capacity);
        }

        public int Count
        {
            get
            {
                return this.data.Count;
            }
        }

        public int Capacity
        {
            get
            {
                return this.data.Capacity;
            }
        }

        public void Add(K key)
        {
            this.data.Add(key, key);
        }

        public bool Remove(K key)
        {
            return this.data.Remove(key);
        }

        public K Find(K key)
        {
            KeyValuePair<K,K>? keyValuePair = this.data.Find(key);
            return keyValuePair.GetValueOrDefault().Key;
        }

        public void Clear()
        {
            this.data.Clear();
        }

        public bool Containes(K key)
        {
            return this.data.ContainesKey(key);
        }

        public Hashset<K> Union(Hashset<K> hashset)
        {
            throw new NotImplementedException("NOT IMPLEMENTED");
        }

        public Hashset<K> Intersect(Hashset<K> hashset)
        {
            throw new NotImplementedException("NOT IMPLEMENTED");
        }
    }
}
