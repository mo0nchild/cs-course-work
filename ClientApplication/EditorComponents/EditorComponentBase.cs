using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using CSCourseWork.EditorConfiguration;
using CSCourseWork.NodesControllers;
using CSCourseWork.Windows;

namespace CSCourseWork.EditorComponents
{
    public enum EditorModes : System.SByte { AddNode, RemoveNode, SelectNode };

    public sealed class EditorSettingsAttribute : System.Attribute
    {
        public System.String SettingName { get; private set; } = string.Empty;
        public System.String SettingSection { get; set; } = string.Empty;
        public EditorSettingsAttribute(string name) : base() => this.SettingName = name;
    }

    public sealed class EditorActionEventArgs : System.EventArgs
    {
        public EditorModes NodeAction { get; private set; } = default(EditorModes);
        public Point ActionPosition { get; private set; } = default(Point);
        public NodeModel? NodeInstance { get; set; } = null;
        public System.Int32? NodeScale { get; set; } = null;

        public EditorActionEventArgs(EditorModes action, Point position) : base()
        { this.NodeAction = action; this.ActionPosition = position; }
    }

    public delegate void EditorActionEventHandler(object? sender, EditorActionEventArgs args);

    public sealed class EditorComponentException : System.Exception
    { public EditorComponentException(string message) : base(message) { } }

    public abstract class EditorComponentBase<TNodesController> : System.Windows.Forms.Panel
        where TNodesController : NodesControllers.INodesControllerWithConnectors, new()
    {
        public virtual event EditorComponents.EditorActionEventHandler? NodeClicked;
        public virtual event EditorComponents.EditorActionEventHandler? FieldClicked;

        public virtual event EditorComponents.EditorActionEventHandler? NodeScaled;

        public virtual EditorComponents.EditorModes Mode { get; set; } = default;
        public virtual System.Int32 NodeSize { get; protected set; }
        public TNodesController Controller { get; private set; }

        [EditorSettingsAttribute("Диапазон маштабирования", SettingSection = "Камера")]
        public virtual EditorComponents.EditorScale NodeScaleRange { get; set; } = new(10, 100);

        [EditorConfiguration.EditorConfigTargetAttribute]
        [EditorSettingsAttribute("Скорость перемещения", SettingSection = "Камера")]
        public virtual System.Int32 NodeMovingSpeed { get; set; } = default(int) + 1;

        [EditorConfiguration.EditorConfigTargetAttribute]
        [EditorSettingsAttribute("Шрифт номера", SettingSection = "Вершина")]
        public virtual EditorComponents.EditorFontFamily NodeFontFamily { get; set; } = new();

        [EditorConfiguration.EditorConfigTargetAttribute]
        [EditorSettingsAttribute("Ширина границы", SettingSection = "Вершина")]
        public virtual System.Int32 NodeBorderWidth { get; set; } = default(int);

        [EditorSettingsAttribute("Цвет сетки", SettingSection = "Поверхность")]
        public virtual EditorComponents.EditorColor EditorGridColor { get; set; } = default(EditorColor);

        [EditorSettingsAttribute("Цвет выбранной вершины", SettingSection = "Вершина")]
        public virtual EditorComponents.EditorColor NodeSelectColor { get; set; } = default(EditorColor);

        [EditorSettingsAttribute("Цвет поверхности", SettingSection = "Поверхность")]
        public virtual EditorComponents.EditorColor EditorBackColor { get; set; } = default(EditorColor);

        [EditorSettingsAttribute("Цвет вершины", SettingSection = "Вершина")]
        public virtual EditorComponents.EditorColor NodeColor { get; set; } = default(EditorColor);

        [EditorSettingsAttribute("Цвет шрифта", SettingSection = "Вершина")]
        public virtual EditorComponents.EditorColor NodeFontColor { get; set; } = new(255, 255, 255);

        private int? selected_nodeid = default(int?);
        public virtual Nullable<System.Int32> SelectedNodeID
        {
            set => this.selected_nodeid =
                (value > 0 || value <= this.Controller.NodesList.Count) ? value : null;

            get => this.selected_nodeid;
        }

        public EditorComponentBase(TNodesController controller) : base()
        { 
            this.MouseDown += new MouseEventHandler(this.OnEditorComponentClick);
            this.Paint += new PaintEventHandler(this.EditorComponentPaint);

            this.Controller = controller;
        }

        protected void EditorComponentPaint(object? sender, PaintEventArgs args)
        {
            using (var grid_image = this.BuildEditorGrid(this.Size)) args.Graphics.DrawImage(grid_image, new Point(0, 0));

            foreach (var connector in this.Controller.BuildNodeСonnectors())
            { this.PaintEdgeWithArrow(args.Graphics, connector, this.NodeColor.ConvertToColor()); }

            this.Controller.ToList().ForEach(delegate (NodeModel node_info)
            {
                var node_color = (this.SelectedNodeID.HasValue && node_info.NodeID == this.SelectedNodeID.Value)
                    ? this.NodeSelectColor : this.NodeColor;

                this.PaintNodeInstance(args.Graphics, node_info, new SolidBrush(node_color.ConvertToColor()));
            });
        }

        protected virtual void OnEditorComponentClick(object? sender, MouseEventArgs args)
        {
            NodesControllers.NodeModel? current_node = default;
            foreach (var node_item in this.Controller)
            {
                var collision_check = this.Controller.NodeCollisionCheck(args.Location, node_item.NodeID);
                if (collision_check) { current_node = node_item; break; }
            }
            var selection_switch = default(bool);
            if (current_node != null)
            {
                if (!this.SelectedNodeID.HasValue && this.Mode == EditorModes.SelectNode)
                {
                    this.SelectedNodeID = current_node.NodeID; selection_switch = true;
                }
                this.NodeClicked?.Invoke(this, new EditorActionEventArgs(this.Mode, args.Location)
                { NodeInstance = current_node });
            }
            else this.FieldClicked?.Invoke(this, new EditorActionEventArgs(this.Mode, args.Location));

            if (this.SelectedNodeID.HasValue && !selection_switch) this.SelectedNodeID = null;
        }

        protected abstract System.Drawing.Image BuildEditorGrid(Size size);

        public abstract void PaintEdgeWithArrow(Graphics graphics, NodesConnectorInfo connector, Color color);
        public abstract void PaintNodeInstance(Graphics graphic, NodeModel node_info, Brush node_brush);

        public abstract void BuildGraphPath(List<NodesConnectorInfo> node_paths);
        public abstract void ScalingGraphView(int scale_value);
    }
}
