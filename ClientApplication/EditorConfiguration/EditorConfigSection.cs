using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCourseWork.EditorConfiguration
{
    public sealed class EditorConfigSection : ConfigurationSection
    {
        [ConfigurationProperty("namespaces", IsRequired = true)]
        public EditorNamespacesCollection UsingNamespaces => (EditorNamespacesCollection)base["namespaces"];

        [ConfigurationProperty("properties", IsRequired = true)]
        public EditorPropertiesCollection EditorProperties => (EditorPropertiesCollection)base["properties"];
    }


    [ConfigurationCollection(typeof(EditorProperty), AddItemName = "item")]
    public sealed class EditorNamespacesCollection : ConfigurationElementCollection
    {
        public class EditorNamespace : ConfigurationElement
        {
            [ConfigurationProperty("value", DefaultValue = default(string), IsKey = true, IsRequired = true)]
            public string Value { get => (string)base["value"]; set => base["value"] = value; }
        }

        protected override ConfigurationElement CreateNewElement() => new EditorNamespace();

        protected override object GetElementKey(ConfigurationElement element)
        { return ((EditorNamespace)element).Value; }
    }


    [ConfigurationCollection(typeof(EditorProperty), AddItemName = "property")]
    public sealed class EditorPropertiesCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement() => new EditorProperty();

        protected override object GetElementKey(ConfigurationElement element)
        { return ((EditorProperty)element).Name; }
    }

    public sealed class EditorProperty : ConfigurationElement
    {
        [ConfigurationProperty("name", DefaultValue = default(string), IsKey = true, IsRequired = true)]
        public string Name { get => (string)base["name"]; set => base["name"] = value; }

        [ConfigurationProperty("type", DefaultValue = default(string), IsRequired = true)]
        public string Type { get => (string)base["type"]; set => base["type"] = value; }

        [ConfigurationProperty("building", IsRequired = true)]
        public EditorPropertyBuilding Building
        {
            get { return (EditorPropertyBuilding)base["building"]; }
            set { base["building"] = value; }
        }
    }

    [ConfigurationCollection(typeof(EditorPropertyBuilding.PropertyParams))]
    public sealed class EditorPropertyBuilding : ConfigurationElementCollection
    {
        [ConfigurationProperty("value", DefaultValue = default(string))]
        public string Value { get => (string)base["value"]; set => base["value"] = value; }

        public class PropertyParams : ConfigurationElement
        {
            [ConfigurationProperty("value", IsRequired = true)]
            public string Value { get => (string)base["value"]; set => base["value"] = value; }

            [ConfigurationProperty("type", IsRequired = true)]
            public string Type { get => (string)base["type"]; set => base["type"] = value; }
        }

        protected override ConfigurationElement CreateNewElement() => new PropertyParams();

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((PropertyParams)element).Value;
        }
    }

}
