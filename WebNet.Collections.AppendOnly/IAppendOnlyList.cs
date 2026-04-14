namespace WebNet.Collections.AppendOnly
{
    public interface IAppendOnlyList<TItem> : IEnumerable<TItem>
    {
        int Count { get; }
        TItem this[int ix] { get; }
        void Add(TItem item);
        void Clear();
    }
}
