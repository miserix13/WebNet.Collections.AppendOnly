using System.Collections;

namespace WebNet.Collections.AppendOnly
{
    public class AppendOnlyList<TItem> : IAppendOnlyList<TItem>
    {
        private readonly List<TItem> items;

        public AppendOnlyList() :
            base()
        {
            this.items = [];
        }

        public TItem this[int ix] => this.items[ix];

        public int Count => this.items.Count;

        public void Add(TItem item)
        {
            this.items.Add(item);
        }

        public void Clear()
        {
            this.items.Clear();
        }

        public IEnumerator<TItem> GetEnumerator()
        {
            return this.items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
