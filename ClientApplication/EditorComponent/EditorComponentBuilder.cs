using CSCourseWork.NodeController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCourseWork.EditorComponent
{
    public interface IEditorComponentBuilder : System.IDisposable
    {
        public IEditorComponentBuilder AddLinkToForm(Form form_link);
        public IEditorComponentBuilder AddEditorNodeSize(int node_size, int node_border);
        public IEditorComponentBuilder AddEditorNodeColor(Color @default, Color selected);
        public IEditorComponentBuilder AddEditorMovingSpeed(int speed);

        public void AddLinkToNodeController(INodesControllerWithConnectors controller);
        public IEditorComponent BuildEditorFromConfiguration();
        public IEditorComponent BuildEditor();
    }

    public class EditorComponentBuilder<TEditorComponent> : System.Object, IEditorComponentBuilder
        where TEditorComponent : IEditorComponent
    {
        //public TEditorComponent EditorComponent { get; private set; }

        public IEditorComponentBuilder AddEditorMovingSpeed(int speed)
        {
            throw new NotImplementedException();
        }

        public IEditorComponentBuilder AddEditorNodeColor(Color @default, Color selected)
        {
            throw new NotImplementedException();
        }

        public IEditorComponentBuilder AddEditorNodeSize(int node_size, int node_border)
        {
            throw new NotImplementedException();
        }

        public IEditorComponentBuilder AddLinkToForm(Form form_link)
        {
            throw new NotImplementedException();
        }

        public void AddLinkToNodeController(INodesControllerWithConnectors controller)
        {
            throw new NotImplementedException();
        }

        public IEditorComponent BuildEditor()
        {
            throw new NotImplementedException();
        }

        public IEditorComponent BuildEditorFromConfiguration()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
