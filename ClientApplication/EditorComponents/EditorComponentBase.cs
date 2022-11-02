using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using CSCourseWork.NodesControllers;

namespace CSCourseWork.EditorComponents
{
    public enum EditorModes : System.SByte { AddNode, RemoveNode, SelectNode };

    public sealed class EditorActionEventArgs : System.EventArgs
    {
        public EditorModes NodeAction { get; private set; } = default(EditorModes);
        public Point ActionPosition { get; private set; } = default(Point);
        public NodeModel? NodeInstance { get; set; } = null;
        public System.Int32? NodeScale { get; set; } = null;

        public EditorActionEventArgs(EditorModes action, Point position)
            : base() { this.NodeAction = action; this.ActionPosition = position; }
    }

    public delegate void EditorActionEventHandler(object? sender, EditorActionEventArgs args);

    public record struct EditorScale(int Min, int Max);
    public record struct EditorColor(int R, int G, int B) { public Color ConvertToColor() => Color.FromArgb(R, G, B); }

    public abstract class EditorComponentBase<TNodesController> : System.Windows.Forms.Panel
        where TNodesController : NodesControllers.INodesControllerWithConnectors, new()
    {
        public virtual event EditorComponents.EditorActionEventHandler? NodeClicked;
        public virtual event EditorComponents.EditorActionEventHandler? FieldClicked;

        public virtual event EditorComponents.EditorActionEventHandler? NodeScaled;

        public virtual EditorComponents.EditorModes Mode { get; set; } = default;
        public abstract Nullable<System.Int32> SelectedNodeID { get; set; }
        public TNodesController Controller { get; private set; }

        public virtual System.Int32 NodeMovingSpeed { get; set; } = default(int) + 1;
        public virtual EditorComponents.EditorScale NodeScaleRange { get; set; } = new(10, 100);
        public virtual System.String NodeFontFamily { get; set; } = new("Arial");
        public virtual System.Int32 NodeBorderWidth { get; set; } = default(int);

        public virtual EditorComponents.EditorColor NodeColor { get; set; } = default(EditorColor);
        public virtual EditorComponents.EditorColor EditorGridColor { get; set; } = default(EditorColor);
        public virtual EditorComponents.EditorColor NodeSelectColor { get; set; } = default(EditorColor);
        public virtual EditorComponents.EditorColor EditorBackColor { get; set; } = default(EditorColor);
        public virtual System.Int32 NodeSize { get; protected set; }

        public EditorComponentBase(TNodesController controller, Form parent_form) : base()
        { this.Controller = controller; parent_form.Controls.Add(this); }

        public abstract void BuildGraphPath(List<NodesConnectorInfo> node_paths);
        public abstract void PaintEdgeWithArrow(Graphics graphics, NodesConnectorInfo connector, Color color);
        public abstract void PaintNodeInstance(Graphics graphic, NodeModel node_info, Brush node_brush);
        public abstract void ScalingGraphView(int scale_value);
    }

    public sealed class EditorComponentException : System.Exception 
    {
        public EditorComponentException(string message) : base(message) { }
    }
}
