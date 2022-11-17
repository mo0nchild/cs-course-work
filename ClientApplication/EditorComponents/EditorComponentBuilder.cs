using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using CSCourseWork.EditorConfiguration;
using CSCourseWork.NodesControllers;

namespace CSCourseWork.EditorComponents
{
    public interface IEditorComponentBuilder<TController> : IEnumerable<EditorConfigProperty>
        where TController : NodesControllers.INodesControllerWithConnectors, new()
    {
        public IEditorComponentBuilder<TController> AddEditorGeometry(Point position, Size size);
        public IEditorComponentBuilder<TController> AddEditorProperty(string name, object value);
        public IEditorComponentBuilder<TController> AddEditorConfiguration(IEditorConfigProvider provider);

        public void SetLinkToNodeController(System.Type controller_type);
        public void SetLinkToForm(Form form_link);
        public EditorComponentBase<TController> BuildEditor(System.String name);
    }

    public sealed class EditorComponentBuilder : System.Object, IEditorComponentBuilder<NodesController>
    {
        public System.Type ControllerType { get; private set; }
        public Form FormLink { get; private set; }
        public Dictionary<string, object> Properties { get; private set; } = new();

        private System.Drawing.Point EditorPosition = default(Point);
        private System.Drawing.Size EditorSize = default(Size);

        private NodesControllers.NodesController? controller_instance = default(NodesController);
        public NodesController? ControllerInstance { set => controller_instance = value; }

        public EditorComponentBuilder(Form form_link, Type controller_type) : base() 
        {
            try { this.SetLinkToNodeController(controller_type); }
            catch (EditorComponents.EditorComponentException error) 
            { 
                throw new System.Exception("Невозможно создать Builder", error); 
            }
            this.FormLink = form_link;
        }

        public void SetLinkToForm(Form form_link) => this.FormLink = form_link;
        public void SetLinkToNodeController(System.Type controller_type) 
        {
            if (!typeof(NodesController).IsAssignableFrom(controller_type)) 
            {
                throw new EditorComponentException($"Тип не совместим с {typeof(NodesController).Name}");
            }
            this.ControllerType = controller_type;
        }

        public IEditorComponentBuilder<NodesController> AddEditorGeometry(Point position, Size size) 
        { (this.EditorPosition, this.EditorSize) = (position, size); return this; }

        public IEditorComponentBuilder<NodesController> AddEditorProperty(string name, object value) 
        {
            if (this.Properties.ContainsKey(name)) { this.Properties[name] = value; }
            else { this.Properties.Add(name, value); } return this;
        }

        public IEditorComponentBuilder<NodesController> AddEditorConfiguration(IEditorConfigProvider provider) 
        {
            try { foreach (var property in provider.TakeConfig()) this.AddEditorProperty(property.Name, property.Value); }
            catch (EditorConfiguration.EditorConfigException error) { Console.WriteLine(error.Message); } return this;

            //foreach (var property in provider.TakeConfig()) 
            //{
            //this.AddEditorProperty(property.Name, property.Value);
            //if(property.Name == "TestType")
            //{
            //    var testtype = (EditorTestType)property.Value;
            //    Console.WriteLine($"\n\tTestType - Text: {testtype.Text}, Range: [min={testtype.Range.Min}],
            //      [max={testtype.Range.Max}]\n");
            //}
            //}
        }

        public EditorComponentBase<NodesController> BuildEditor(string name)
        {
            if (this.controller_instance == null)
            { this.controller_instance = (NodesController)(Activator.CreateInstance(this.ControllerType)!); }

            var editor_instance = new EditorComponent(this.controller_instance)
            {
                Location = this.EditorPosition, Size = this.EditorSize, Name = name
            };
            this.FormLink.Controls.Add(editor_instance);

            foreach (KeyValuePair<string, object> item in this.Properties)
            {
                var property_info = editor_instance.GetType().GetProperty(item.Key);
                if (property_info != null)
                {
                    try { property_info.SetValue(editor_instance, item.Value); }
                    catch (System.Exception) { continue; }
                }
            }
            return editor_instance;
        }

        public IEnumerator<EditorConfigProperty> GetEnumerator()
        {
            foreach (KeyValuePair<string, object> property in this.Properties)
            { yield return new EditorConfigProperty(property.Key, property.Value); }
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}
