using System.Configuration;

namespace parsing.Configuration
{
    internal class CurrencyInfoElement : ConfigurationElement
    {

        [ConfigurationProperty("Key", IsRequired = true)]
        public uint Key { get => (uint)this["Key"]; set => this["Key"] = value; }

        [ConfigurationProperty("Symbol", IsRequired = true)]
        public char Symbol { get => (char)this["Symbol"]; set => this["Symbol"] = value; }

        [ConfigurationProperty("Link", IsRequired = true)]
        public string Link { get => (string)this["Link"]; set => this["Link"] = value; }
    }
}
