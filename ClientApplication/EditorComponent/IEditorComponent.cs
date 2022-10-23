using CSCourseWork.NodeController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCourseWork.EditorComponent
{
    public enum EditorModes : System.SByte { AddNode, RemoveNode, SelectNode };

    public sealed class EditorActionEventArgs : System.EventArgs
    {
        public EditorModes NodeAction { get; private set; } = default(EditorModes);
        public Point ActionPosition { get; private set; } = default(Point);
        public NodeModel? NodeInstance { get; set; } = null;
        public EditorActionEventArgs(EditorModes action, Point position)
            : base() { this.NodeAction = action; this.ActionPosition = position; }
    }

    public delegate void EditorActionEventHandler(object? sender, EditorActionEventArgs args);

    public interface IEditorComponent : System.IDisposable
    {
        public event EditorComponent.EditorActionEventHandler? NodeClicked;
        public event EditorComponent.EditorActionEventHandler? FieldClicked;

        public INodesControllerWithConnectors Controller { get; }
        public Nullable<System.Int32> SelectedNodeID { get; }
        public EditorComponent.EditorModes Mode { get; set; }

        public System.Int32 NodeMovingSpeed { get; set; }
        public System.Drawing.Color NodeColor { get; set; }
        public System.Drawing.Color NodeSelectColor { get; set; }

        public System.Int32 NodeSize { get; }

        public void BuildGraphPath(List<NodeConnectorInfo> node_paths);
        public void PaintEdgeWithArrow(Graphics graphics, NodeConnectorInfo connector, Color color);
        public void PaintNodeInstance(Graphics graphic, NodeModel node_info, Brush node_brush);
    }

    public
}
