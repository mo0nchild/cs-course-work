using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CSCourseWork.EditorConfiguration
{
    //public record class EditorConfigProperty(string Name, object Value, System.Type Type);
    public sealed class EditorConfigProperty : System.Object
    {
        public System.String Name { get; private set; } = String.Empty;
        public System.Object Value { get; set; } = default(object)!;

        public EditorConfigProperty(string name, object value) : base() => (this.Name, this.Value) = (name, value);
        public System.Type Type { get => this.Value!.GetType(); }
    }


    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = true)]
    public sealed class EditorConfigTypeAttribute : System.Attribute { }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class EditorConfigTargetAttribute : System.Attribute { }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class EditorConfigPropertyAttribute : System.Attribute
    {
        public System.String Name { get; private set; } = string.Empty;
        public EditorConfigPropertyAttribute(string name) : base() => this.Name = name;
    }

    public sealed class EditorConfigException : System.Exception
    {
        public System.String PropertyName { get; private set; } = string.Empty;
        public EditorConfigException(string message, string name) : base(message) => this.PropertyName = name; 
    }

    public interface IEditorConfigProvider : IEnumerable<EditorConfigProperty>
    {
        public List<System.String> UsingNamespaces { get; }
        public System.String? ConfigFilePath { get; set; }

        public List<EditorConfiguration.EditorConfigProperty> TakeConfig();
        public IEditorConfigProvider PutConfigProperty(System.String name, System.Object property);
        public IEditorConfigProvider PutConfigObject(System.Object @object);

        protected static Configuration GetConfiguration(string? filepath)
        {
            if (filepath == null) return ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            var fileMap = new ExeConfigurationFileMap() { ExeConfigFilename = filepath };
            return ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
        }
    }

    public class EditorConfigProvider : System.Object, IEditorConfigProvider
    {
        public System.String? ConfigFilePath { get; set; } = null;
        public List<System.String> UsingNamespaces { get; private set; } = new();

        public EditorConfigProvider(string? filepath) : base() => this.ConfigFilePath = filepath;

        public IEditorConfigProvider PutConfigObject(object @object)
        {
            foreach (var property in @object.GetType().GetProperties())
            {
                var property_type = property.PropertyType;

                if (property.GetCustomAttribute<EditorConfigTargetAttribute>(true) == null &&
                    property_type.GetCustomAttribute<EditorConfigTypeAttribute>(true) == null) continue;
                
                this.PutConfigProperty(property.Name, property.GetValue(@object)!);
            }
            return this;
        }

        public IEditorConfigProvider PutConfigProperty(string name, object property)
        {
            var config = IEditorConfigProvider.GetConfiguration(this.ConfigFilePath);
            var section = (EditorConfigSection)config.GetSection("editor.settings");

            var property_namespace = property.GetType().Namespace!;
            var config_has_namespace = default(bool);

            foreach (EditorNamespacesCollection.EditorNamespace config_namespace in section.UsingNamespaces)
            {
                if (config_namespace.Value == property_namespace) { config_has_namespace = true; break; }
            }
            if (config_has_namespace == default(bool))
            {
                section.UsingNamespaces.AddNamespace(new EditorNamespacesCollection.EditorNamespace() 
                { Value = property_namespace });
            }

            var config_has_property = default(bool);
            foreach (EditorConfiguration.EditorProperty config_property in section.EditorProperties) 
            {
                if (config_property.Name == name) { config_has_property = true;  break; }
            }
            if (config_has_property)
            {
                section.EditorProperties[name].Type = property.GetType().Name;
                var changeable_property = section.EditorProperties[name];

                this.PutPropertyInConfig(ref changeable_property, name, property);
            } else
            {
                var new_property = new EditorConfiguration.EditorProperty();
                this.PutPropertyInConfig(ref new_property, name, property);

                section.EditorProperties.AddProperty(new_property);
            }
            config.Save(ConfigurationSaveMode.Full, true); return this;
        }

        public List<EditorConfigProperty> TakeConfig()
        {
            var config = IEditorConfigProvider.GetConfiguration(this.ConfigFilePath);
            var section = (EditorConfigSection)config.GetSection("editor.settings");

            var result_list = new List<EditorConfigProperty>();
            this.UsingNamespaces.Clear();

            foreach (EditorNamespacesCollection.EditorNamespace @namespace in section.UsingNamespaces)
            { this.UsingNamespaces.Add(@namespace.Value); }

            foreach (EditorConfiguration.EditorProperty property in section.EditorProperties)
            {
                var property_instance = this.GetPropertyInstance(property, section);
                if (property_instance == null) continue;

                result_list.Add(new EditorConfigProperty(property.Name, property_instance));
            }
            return result_list;
        }

        private System.Type? GetPropertyType(string type_string)
        {
            Type? property_type = default;

            try { property_type = Type.GetType(type_string, true); }
            catch (System.Exception)
            {
                for (int index = 0; index < this.UsingNamespaces.Count; index++)
                {
                    var selected_namespace = this.UsingNamespaces[index];

                    try { property_type = Type.GetType($"{selected_namespace}.{type_string}", true); break; }
                    catch { continue; }
                }
            }
            return property_type;
        }

        private void PutPropertyInConfig(ref EditorProperty config_property, string name, object property)
        {
            var property_type = property.GetType();
            config_property.Building.ClearParams();

            if (property_type.IsPrimitive || property_type == typeof(string))
            {
                config_property.Building.Value = property.ToString()!; return;
            }
            else if (property_type.GetCustomAttribute<EditorConfigTypeAttribute>(true) == null)
            { throw new EditorConfigException("Невозможно обработать свойство", name); }

            foreach (var property_item in property_type.GetProperties())
            {
                var property_item_type = property_item.PropertyType;
                var property_item_attribute = property_item
                    .GetCustomAttribute<EditorConfigPropertyAttribute>(true);

                if (property_item_attribute == null) continue;
                EditorPropertyBuilding.PropertyParams @param = default!;

                if (property_item_type.IsPrimitive || property_item_type == typeof(string))
                {
                    @param = new EditorPropertyBuilding.PropertyParams() { Name = property_item_attribute.Name,
                        Value = property_item.GetValue(property)?.ToString()!, Type = property_item_type.Name };
                }
                else if (property_item_type.GetCustomAttribute<EditorConfigTypeAttribute>(true) != null)
                {
                    var reference_name = Guid.NewGuid();
                    this.PutConfigProperty(reference_name.ToString(), property_item.GetValue(property)!);

                    @param = new EditorPropertyBuilding.PropertyParams() { Name = property_item_attribute.Name,
                        Type = property_item_type.Name, Reference = reference_name.ToString() };
                }
                else continue;
                config_property.Building.AddParam(@param);
            }
        }

        private object? GetPropertyInstance(EditorProperty property, EditorConfigSection section) 
        {
            var result_property_type = this.GetPropertyType(property.Type);
            if (result_property_type == null) return null;

            if (property.Building.Value != String.Empty)
            {
                return Convert.ChangeType(property.Building.Value, result_property_type);
            }

            object? property_instance = Activator.CreateInstance(result_property_type, new object[] { });
            if (property_instance == null) return null;

            foreach (EditorPropertyBuilding.PropertyParams param in property.Building)
            {
                var param_type = GetPropertyType(param.Type);
                if (param_type == null) continue;

                object? param_value = default(object);

                if (!param_type.IsPrimitive && param_type != typeof(string))
                {
                    if (param_type.GetCustomAttribute<EditorConfigTypeAttribute>(true) == null 
                        || param.Reference == string.Empty) continue;

                    param_value = this.GetPropertyInstance(section.EditorProperties[param.Reference], section);
                }
                else param_value = Convert.ChangeType(param.Value, param_type);
                PropertyInfo? property_iteminfo = default;

                foreach (var property_item in result_property_type.GetProperties())
                {
                    var property_attribute = property_item.GetCustomAttribute<EditorConfigPropertyAttribute>(true);
                    if (property_attribute != null && property_attribute.Name == param.Name)
                    {
                        property_iteminfo = property_item; break;
                    }
                }
                if (property_iteminfo != null) property_iteminfo.SetValue(property_instance, param_value);
            }
            return property_instance;
        }

        public IEnumerator<EditorConfigProperty> GetEnumerator() => this.TakeConfig().GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}
