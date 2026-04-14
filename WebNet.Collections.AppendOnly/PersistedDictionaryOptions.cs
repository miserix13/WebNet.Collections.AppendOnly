namespace WebNet.Collections.AppendOnly
{
    public record PersistedDictionaryOptions
    {
        public string DatabaseName { get; set; } = "PersistedKV";
        public string BaseDirectory { get; set; } = Environment.CurrentDirectory;
    }
}
