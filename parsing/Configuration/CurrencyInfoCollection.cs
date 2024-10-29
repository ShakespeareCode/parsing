using System.Configuration;

namespace parsing.Configuration
{
    internal class CurrencyInfoCollection : ConfigurationElementCollection
    {
        private const string CollectionKeyName = "CurrencyInfo";

        protected override ConfigurationElement CreateNewElement()
            => new CurrencyInfoElement();

        protected override object GetElementKey(ConfigurationElement element)
            => ((CurrencyInfoElement)element).Key;
        protected override string ElementName => CollectionKeyName;

        public override ConfigurationElementCollectionType CollectionType
            => ConfigurationElementCollectionType.BasicMap;
        public CurrencyInfoElement this[int index]
        {
            get => (CurrencyInfoElement)BaseGet(index);
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }

        new public CurrencyInfoElement this[string name] => (CurrencyInfoElement)BaseGet(name);

        public int IndexOf(CurrencyInfoElement details) => BaseIndexOf(details);

        public void Add(CurrencyInfoElement details) => BaseAdd(details);

        protected override void BaseAdd(ConfigurationElement element) 
            => BaseAdd(element, false);

        public void Remove(CurrencyInfoElement details)
        {
            if (BaseIndexOf(details) >= 0)
                BaseRemove(details.Key);
        }

        public void RemoveAt(int index) => BaseRemoveAt(index);

        public void Remove(string name)
        {
            BaseRemove(name);
        }

        public void Clear()
        {
            BaseClear();
        }
    }
}
