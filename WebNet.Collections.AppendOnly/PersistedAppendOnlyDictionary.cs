using Stellar.Collections;
using System.Collections;

namespace WebNet.Collections.AppendOnly
{
    public class PersistedAppendOnlyDictionary<TKey, TValue> : IAppendOnlyDictionary<TKey, TValue>, IAsyncDisposable
        where TKey : struct
    {
        private readonly PersistedDictionaryOptions options;
        private readonly FastDB dB;
        private readonly IFastDBCollection<TKey, TValue> values;
        private readonly AppendOnlyDictionary<TKey, TValue> pairs;

        public PersistedAppendOnlyDictionary() :
            base()
        {
            this.options = new();
            this.dB = new(new FastDBOptions() { AddDuplicateKeyBehavior = DuplicateKeyBehaviorType.Upsert, BaseDirectory = this.options.BaseDirectory, BufferMode = BufferModeType.WriteParallelEnabled, BulkAddDuplicateKeyBehavior = DuplicateKeyBehaviorType.Upsert, DatabaseName = this.options.DatabaseName, DeserializationFailureBehavior = ErrorBehaviorType.ReturnFalse, FileExtension = "fdb", IsBufferedWritesEnabled = true, MaxDegreeOfParallelism = 4, RemoveKeyNotFoundBehavior = ErrorBehaviorType.ReturnFalse, SerializationFailureBehavior = ErrorBehaviorType.ReturnFalse, Serializer = SerializerType.MessagePack_Contractless, StorageFailureBehavior = ErrorBehaviorType.ReturnFalse, UpdateKeyNotFoundBehavior = ErrorBehaviorType.ReturnFalse });
            this.values = this.dB.GetCollection<TKey, TValue>("dictionary");
            this.pairs = [];
        }

        public TValue this[TKey key] => this.pairs[key];

        public int Count => this.pairs.Count;

        public void Add(TKey key, TValue value)
        {
            this.values.AddOrUpdate(key, value);
            this.pairs.Add(key, value);
        }

        public void Clear()
        {
            this.pairs.Clear();
        }

        public async Task FlushAsync()
        {
            foreach (var item in this.pairs)
            {
                await this.values.AddOrUpdateAsync(item.Key, item.Value);
            }
        }

        public async ValueTask DisposeAsync()
        {
            GC.SuppressFinalize(this);
            await this.values.CloseAsync();
            await this.dB.DisposeAsync();
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
