using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CSCourseWork.EditorConfiguration
{
    public record class EditorConfigProperty(string Name, object Value, System.Type Type);

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

    public interface IEditorConfigProvider : IEnumerable<EditorConfigProperty>
    {
        public List<System.String> UsingNamespaces { get; }
        public System.String? ConfigFilePath { get; set; }

        public List<EditorConfiguration.EditorConfigProperty> TakeConfig();
        public IEditorConfigProvider PutConfig(System.Object property);

        protected static Configuration GetConfiguration(string? filepath)
        {
            if (filepath != null) return ConfigurationManager.OpenExeConfiguration(filepath);
            return ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        }
    }

    public class EditorConfigProvider : System.Object, IEditorConfigProvider
    {
        public System.String? ConfigFilePath { get; set; } = null;
        public List<System.String> UsingNamespaces { get; private set; } = new();

        public EditorConfigProvider(string? filepath) : base() => this.ConfigFilePath = filepath;

        public IEditorConfigProvider PutConfig(object property)
        {

            return this;
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

                if (!param_type.IsPrimitive && param_type != typeof(string) && param_type != typeof(DateTime))
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

        public List<EditorConfigProperty> TakeConfig()
        {
            var config = IEditorConfigProvider.GetConfiguration(this.ConfigFilePath);
            var section = (EditorConfigSection)config.GetSection("editor.settings");

            var result = new List<EditorConfigProperty>();
            this.UsingNamespaces.Clear();

            foreach (EditorNamespacesCollection.EditorNamespace @namespace in section.UsingNamespaces) 
            { this.UsingNamespaces.Add(@namespace.Value); }

            foreach (EditorProperty property in section.EditorProperties) 
            {
                var property_instance = this.GetPropertyInstance(property, section);
                if (property_instance == null) continue;

                result.Add(new EditorConfigProperty(property.Name, property_instance, property_instance.GetType()));
            }
            return result;
        }

        public IEnumerator<EditorConfigProperty> GetEnumerator() => this.TakeConfig().GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}
