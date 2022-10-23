using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CSCourseWork.NodesControllers;

namespace CSCourseWork.EditorComponents
{
    public interface IEditorComponentBuilder<TNodesController>
        where TNodesController : NodesControllers.INodesControllerWithConnectors, new()
    {
        public IEditorComponentBuilder<TNodesController> AddEditorNodeSize(int node_size, int node_border);
        public IEditorComponentBuilder<TNodesController> AddEditorGeometry(Point position, Size size);
        public IEditorComponentBuilder<TNodesController> AddEditorMovingSpeed(int speed);
        public IEditorComponentBuilder<TNodesController> AddEditorNodeColor(
            Color @default, Color selected, Color background);

        public void SetLinkToNodeController(TNodesController controller);
        public void SetLinkToForm(Form form_link);

        public IEditorComponent<TNodesController> BuildEditorFromConfiguration();
        public IEditorComponent<TNodesController> BuildEditor();
    }

    public class EditorComponentBuilder : System.Object, IEditorComponentBuilder<NodesController>
    {
        private const int DefaultNodeBorder = 3;
        private const int DefaultNodeSize = 40;

        public NodesController Controller { get; private set; }
        public Form FormLink { get; private set; }

        private System.Int32 NodeSize = EditorComponentBuilder.DefaultNodeSize;
        private System.Int32 NodeBorderSize = EditorComponentBuilder.DefaultNodeBorder;

        private System.Drawing.Point EditorPosition = default(Point);
        private System.Drawing.Size EditorSize = default(Size);

        private System.Int32 NodeMovingSpeed = default(int) + 1;
        private System.Drawing.Color NodeColor, NodeSelectColor, BackGround;
        public EditorComponentBuilder(Form form_link, NodesController controller) : base() 
        {
            this.Controller = controller;
            this.FormLink = form_link;
        }

        public void SetLinkToForm(Form form_link) => this.FormLink = form_link;
        public void SetLinkToNodeController(NodesController controller) => this.Controller = controller;

        public IEditorComponentBuilder<NodesController> AddEditorGeometry(Point position, Size size)
        {
            (this.EditorPosition, this.EditorSize) = (position, size);
            return this;
        }

        public IEditorComponentBuilder<NodesController> AddEditorNodeSize(int node_size, int node_border)
        {
            (this.NodeSize, this.NodeBorderSize) = (node_size, node_border); 
            return this;
        }

        public IEditorComponentBuilder<NodesController> AddEditorNodeColor(Color @default, 
            Color selected, Color background)
        {
            this.NodeColor = @default;
            this.BackGround = background;
            this.NodeSelectColor = selected;
            return this;
        }

        public IEditorComponentBuilder<NodesController> AddEditorMovingSpeed(int speed)
        {
            this.NodeMovingSpeed = speed;
            return this;
        }
        public IEditorComponent<NodesController> BuildEditorFromConfiguration() => throw new NotImplementedException();

        public IEditorComponent<NodesController> BuildEditor()
        {
            this.Controller.NodeSize = this.NodeSize;
            return new EditorComponentBase(this.FormLink, this.Controller)
            {
                NodeColor = this.NodeColor,
                NodeSelectColor = this.NodeSelectColor,
                NodeMovingSpeed = this.NodeMovingSpeed,
                NodeBorderWidth = this.NodeBorderSize,
                BackColor = this.BackGround,
                Location = this.EditorPosition,
                Size = this.EditorSize
            };
        }
    }
}
