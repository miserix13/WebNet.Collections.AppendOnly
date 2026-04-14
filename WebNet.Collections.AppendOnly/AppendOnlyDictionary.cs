using System.Collections;

namespace WebNet.Collections.AppendOnly
{
    public class AppendOnlyDictionary<TKey, TValue> : IAppendOnlyDictionary<TKey, TValue>
    {
        private readonly Dictionary<TKey, TValue> pairs;

        public AppendOnlyDictionary() :
            base()
        {
            this.pairs = [];
        }

        public TValue this[TKey key] => this.pairs[key];

        public int Count => this.pairs.Count;

        public void Add(TKey key, TValue value)
        {
            this.pairs.Add(key, value);
        }

        public void Clear()
        {
            this.Clear();
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return this.pairs.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
