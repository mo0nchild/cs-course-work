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
    public record struct EditorConfigProperty(string Name, object Value, System.Type Type);

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = false)]
    public sealed class EditorConfigTypeAttribute : System.Attribute { }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class EditorConfigPropertyAttribute : System.Attribute
    {
        public System.String Name { get; set; } = string.Empty;
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
                var result_property_type = GetPropertyType(property.Type);
                if (result_property_type == null) continue;

                if (property.Building.Value != String.Empty) 
                {
                    var result_property_value = Convert.ChangeType(property.Building.Value, result_property_type);
                    result.Add(new EditorConfigProperty(property.Name, result_property_value, result_property_type));
                    continue;
                }

                object? property_instance = Activator.CreateInstance(result_property_type, new object[] { });
                if (property_instance == null) continue;

                foreach (EditorPropertyBuilding.PropertyParams param in property.Building) 
                {
                    var param_type = GetPropertyType(param.Type);
                    if (param_type == null) continue;


                    if (!param_type.IsPrimitive && param_type != typeof(string) && param_type != typeof(DateTime)) 
                    {
                        // обработка сложных типов
                    }

                    var param_value = Convert.ChangeType(param.Value, param_type);
                    PropertyInfo? property_iteminfo = default;

                    foreach (var property_item in result_property_type.GetProperties()) 
                    {
                        var property_attribute = property_item.GetCustomAttribute<EditorConfigPropertyAttribute>();
                        if(property_attribute != null && property_attribute.Name == param.Name) 
                        { 
                            property_iteminfo = property_item; break; 
                        }
                    }
                    if (property_iteminfo != null) property_iteminfo.SetValue(property_instance, param_value);
                }
                result.Add(new EditorConfigProperty(property.Name, property_instance, result_property_type));
            }
            return result;
        }

        public IEnumerator<EditorConfigProperty> GetEnumerator() => this.TakeConfig().GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}
