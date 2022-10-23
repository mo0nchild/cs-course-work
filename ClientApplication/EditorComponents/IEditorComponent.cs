using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CSCourseWork.NodesControllers;

namespace CSCourseWork.EditorComponents
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

    public interface IEditorComponent<TNodesController> : System.IDisposable
        where TNodesController : NodesControllers.INodesControllerWithConnectors
    {
        public event EditorComponents.EditorActionEventHandler? NodeClicked;
        public event EditorComponents.EditorActionEventHandler? FieldClicked;

        public Nullable<System.Int32> SelectedNodeID { get; set; }
        public EditorComponents.EditorModes Mode { get; set; }
        public TNodesController Controller { get; }

        public System.Int32 NodeMovingSpeed { get; set; }
        public System.Drawing.Color NodeColor { get; set; }
        public System.Drawing.Color NodeSelectColor { get; set; }
        public System.Int32 NodeSize { get; }

        public void BuildGraphPath(List<NodesConnectorInfo> node_paths);
        public void PaintEdgeWithArrow(Graphics graphics, NodesConnectorInfo connector, Color color);
        public void PaintNodeInstance(Graphics graphic, NodeModel node_info, Brush node_brush);
    }

    public sealed class EditorComponentException : System.Exception 
    {
        public EditorComponentException(string message) : base(message) { }
    }
}
