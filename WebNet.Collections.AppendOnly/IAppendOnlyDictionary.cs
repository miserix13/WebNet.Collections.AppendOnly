namespace WebNet.Collections.AppendOnly
{
    public interface IAppendOnlyDictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
    {
        int Count { get; }
        TValue this[TKey key] { get; }
        void Add(TKey key, TValue value);
        void Clear();
    }
}
