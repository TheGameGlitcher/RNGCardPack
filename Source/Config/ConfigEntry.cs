namespace RNGCardPack.Source.Config
{
    public struct CustomConfigEntry<T>
    {
        public CustomConfigEntry(T value, string section, string key, string description)
        {
            Value = value;
            Section = section;
            Key = key;
            Description = description;
        }

        public T Value { get; set; }
        public string Section { get; set; }
        public string Key { get; set; }
        public string Description { get; set; }
    }
}