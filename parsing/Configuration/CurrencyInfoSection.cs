using System.Configuration;

namespace parsing.Configuration
{
    internal class CurrencyInfoSection : ConfigurationSection
    {
        [ConfigurationProperty("", IsDefaultCollection = true)]
        public CurrencyInfoCollection CurrenciesInfo => (CurrencyInfoCollection)base[""];
    }
}
