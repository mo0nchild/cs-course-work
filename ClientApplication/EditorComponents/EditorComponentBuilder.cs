using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
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

        public EditorComponentBase<TController> BuildEditor();
    }

    public sealed class EditorComponentBuilder : System.Object, IEditorComponentBuilder<NodesController>
    {
        public System.Type ControllerType { get; private set; }
        public Form FormLink { get; private set; }
        public List<EditorConfigProperty> Properties { get; private set; } = new();

        private System.Drawing.Point EditorPosition = default(Point);
        private System.Drawing.Size EditorSize = default(Size);

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
            var adding_property = new EditorConfigProperty(name, value, value.GetType());
            var contains_index = default(int?);

            for (int index = 0; index < this.Properties.Count; index++)
            {
                if (this.Properties[index].Name == name) { contains_index = index; break; }
            }
            if (contains_index.HasValue) { this.Properties[contains_index.Value] = adding_property; }
            else this.Properties.Add(adding_property);

            return this;
        }

        public IEditorComponentBuilder<NodesController> AddEditorConfiguration(IEditorConfigProvider provider) 
        {
            foreach (var property in provider.TakeConfig()) { this.AddEditorProperty(property.Name, property.Value); }
            return this;
        }

        public EditorComponentBase<NodesController> BuildEditor()
        {
            var editor_instance = new EditorComponent(this.FormLink,
                (Activator.CreateInstance(this.ControllerType) as NodesController)!)
            {
                Location = this.EditorPosition,
                Size = this.EditorSize
            };

            foreach (var item in this.Properties) 
            {
                var property_info = editor_instance.GetType().GetProperty(item.Name);
                if (property_info != null)
                {
                    try { property_info.SetValue(editor_instance, item.Value); }
                    catch (System.Exception) { }
                }
            }
            return editor_instance;
        }

        public IEnumerator<EditorConfigProperty> GetEnumerator()
        { foreach (EditorConfigProperty property in this.Properties) yield return property; }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}
