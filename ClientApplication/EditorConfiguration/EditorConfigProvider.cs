using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCourseWork.EditorConfiguration
{
    public interface IEditorConfigProvider : IEnumerable<EditorConfigData.EditorConfigProperties>
    {
        public void PutConfig(EditorConfiguration.EditorConfigData config_data);
        public EditorConfiguration.EditorConfigData TakeConfig();

        protected static Configuration GetConfiguration(string? filepath)
        {
            if (filepath != null) return ConfigurationManager.OpenExeConfiguration(filepath);
            return ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        }
    }

    public class EditorConfigProvider : System.Object, IEditorConfigProvider
    {
        public string? ConfigFilePath { get; private set; } = default(string);
        public EditorConfigProvider(string? filepath) => this.ConfigFilePath = filepath;

        public void PutConfig(EditorConfigData config_data)
        {
            
        }

        public EditorConfigData TakeConfig()
        {
            var config = IEditorConfigProvider.GetConfiguration(this.ConfigFilePath);
            var section = (EditorConfigSection)config.GetSection("editor.settings");

            var result = new EditorConfigData();
            foreach (EditorNamespacesCollection.EditorNamespace @namespace in section.UsingNamespaces) 
            {
                result.UsingNamespaces.Add(@namespace.Value);
            }

            foreach (EditorProperty property in section.EditorProperties) 
            {
                var result_property_type = GetPropertyType(property.Type);
                if (result_property_type == null) continue;

                if (property.Building.Value != String.Empty) 
                {
                    var result_property_value = Convert.ChangeType(property.Building.Value, result_property_type);
                    result.Properties.Add(
                        new EditorConfigData.EditorConfigProperties( result_property_type, result_property_value));
                    continue;
                }

                foreach (EditorPropertyBuilding.PropertyParams param in property.Building) 
                {
                    var param_type = GetPropertyType(param.Type);
                    if (param_type == null) continue;

                    var param_value = Convert.ChangeType(param.Value, param_type);

                }
            }
            return result;

            System.Type? GetPropertyType(string type_string) 
            {
                Type? property_type = default;

                try { property_type = Type.GetType(type_string); }
                catch (System.Exception)
                {
                    for (int index = 0; index < result?.UsingNamespaces.Count; index++)
                    {
                        var selected_namespace = result.UsingNamespaces[index];

                        try { property_type = Type.GetType($"{selected_namespace}.{type_string}"); }
                        catch { continue; }
                    }
                }
                return property_type;
            }
        }

        public IEnumerator<EditorConfigData.EditorConfigProperties> GetEnumerator()
            => this.TakeConfig().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => throw new NotImplementedException();
    }
}
