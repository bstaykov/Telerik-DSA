namespace _03.BiDictionary
{
    using System;
    using System.Collections.Generic;
    using Wintellect.PowerCollections;

    public class BiDictionary<K1, K2, V>
    {
        private Dictionary<K1, Guid> key1Identifiers;
        private Dictionary<K2, Guid> key2Identifiers;
        private MultiDictionary<Guid, V> values;

        public BiDictionary()
        {
            this.key1Identifiers = new Dictionary<K1, Guid>();
            this.key2Identifiers = new Dictionary<K2, Guid>();
            this.values = new MultiDictionary<Guid, V>(true);
        }

        public void Add(K1 key1, K2 key2, V value)
        {
            var id = this.GenerateId();
            this.key1Identifiers.Add(key1, id);
            this.key2Identifiers.Add(key2, id);
            this.values.Add(id, value);
        }

        public ICollection<V> Find(K1 key)
        {
            if (key1Identifiers.ContainsKey(key))
            {
                var id = key1Identifiers[key];

                return this.values[id];
            }
            else 
            {
                return null;
            }
        }

        public ICollection<V> Find(K2 key)
        {
            if (key2Identifiers.ContainsKey(key))
            {
                var id = key2Identifiers[key];

                return this.values[id];
            }
            else
            {
                return null;
            }
        }

        public ICollection<V> Find(K1 key1, K2 key2)
        {
            if (key1Identifiers.ContainsKey(key1) && key2Identifiers.ContainsKey(key2))
            {
                var id1 = key1Identifiers[key1];
                var id2 = key2Identifiers[key2];
                if (id1 == id2)
                {
                    return this.values[id1];
                }

            }

            return null;
        }

        private Guid GenerateId()
        {
            var guid = Guid.NewGuid();
            while (values.ContainsKey(guid))
            {
                guid = Guid.NewGuid();
            }

            return guid;
        }
    }
}
