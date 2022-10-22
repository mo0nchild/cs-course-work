using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using CSCourseWork.NodeController;
using static System.Math;

namespace CSCourseWork.EditorComponent
{
    internal class EditorComponent : Panel
    {
        public enum EditorModes : byte { AddNode, RemoveNode, SelectNode };
        public class EditorActionEventArgs : EventArgs
        {
            public EditorModes NodeAction { get; private set; } = default;
            public Point ActionPosition { get; private set; } = default;
            public NodeModel? NodeInstance { get; set; } = default;
            public EditorActionEventArgs(EditorModes action, Point position)
                : base() { NodeAction = action; ActionPosition = position; }
        }

        public delegate void EditorActionEventHandler(object? sender, EditorActionEventArgs args);
        public event EditorActionEventHandler? NodeClicked;
        public event EditorActionEventHandler? FieldClicked;

        public EditorModes Mode { get; set; } = EditorModes.AddNode;
        public INodesControllerWithConnectors Controller { get; private set; } = new NodesController();
        public int? SelectedNodeID { get; private set; } = null;

        private bool movingbutton_hold = default;
        private Point movingposition_buffer = default;

        private const int DefaultNodeBorder = 3;
        private const int DefaultNodeSize = 50;
        private const int DefaultGridDelta = 5;

        public int NodeBorderWidth { get; set; } = default;
        public int NodeMovingSpeed { get; set; } = 2;
        public Color NodeColor { get; set; } = default;
        public Color NodeSelectColor { get; set; } = Color.Crimson;
        public Color NodeConnectorColor { get; set; } = Color.Black;

        public int NodeSize { get => Controller.NodeSize; }

        public EditorComponent(Form parent_form, int node_size, int node_border, Color node_color) : base()
        {
            (Controller.NodeSize, NodeBorderWidth, NodeColor) = (40, node_border, node_color);
            parent_form.Controls.Add(this);
            DoubleBuffered = true;

            MouseDown += new MouseEventHandler(EditorComponentMouseDown);
            MouseMove += new MouseEventHandler(EditorComponentMouseMove);
            Paint += new PaintEventHandler(EditorComponentPaint);

            NodeClicked += new EditorActionEventHandler(EditorComponentNodeClicked);
            FieldClicked += new EditorActionEventHandler(EditorComponentFieldClicked);

            MouseWheel += EditorComponent_MouseWheel;

            MouseUp += new MouseEventHandler(delegate (object? sender, MouseEventArgs args)
                { if (args.Button == MouseButtons.Right) movingbutton_hold = false; });
        }

        public EditorComponent(Form parent_form)
            : this(parent_form, DefaultNodeSize, DefaultNodeBorder, Color.Black) { }

        private void EditorComponent_MouseWheel(object? sender, MouseEventArgs args)
        {
            var buffer_delta = Controller.NodeSize + Sign(args.Delta) * DefaultGridDelta;
            if (buffer_delta > 0)
            {
                foreach (var item in Controller)
                {
                    int position_x = item.Position.X / Controller.NodeSize * buffer_delta,
                        position_y = item.Position.Y / Controller.NodeSize * buffer_delta;
                    item.Position = new Point(position_x, position_y);
                }
                Controller.NodeSize = buffer_delta;
            }
            Invalidate();
        }

        private void EditorComponentNodeClicked(object? sender, EditorActionEventArgs args)
        {
            if (args.NodeInstance == null) return;
            switch (args.NodeAction)
            {
                case EditorModes.RemoveNode: Controller.RemoveNode(args.NodeInstance.NodeID); break;
                case EditorModes.SelectNode when SelectedNodeID.HasValue:
                    Controller.SetNodeLinks(SelectedNodeID.Value, args.NodeInstance.NodeID); break;
                default: return;
            }
            Invalidate();
        }

        private void EditorComponentMouseMove(object? sender, MouseEventArgs args)
        {
            if (movingbutton_hold != true) return;

            var current_position = movingposition_buffer;
            int moving_direction_x = Sign(args.X - current_position.X),
                moving_direction_y = Sign(args.Y - current_position.Y);

            foreach (var node_info in Controller.NodesList)
            {
                node_info.Position = new Point(node_info.Position.X + moving_direction_x * NodeMovingSpeed,
                    node_info.Position.Y + moving_direction_y * NodeMovingSpeed);
            }
            movingposition_buffer = args.Location;
            Invalidate();
        }

        private void EditorComponentFieldClicked(object? sender, EditorActionEventArgs args)
        {
            if (args.NodeAction == EditorModes.AddNode)
            {
                try { Controller.AddNewNode(args.ActionPosition.X, args.ActionPosition.Y); }
                catch (NodeControllerException node_error)
                {
                    MessageBox.Show($"{node_error.Message} - Node: {node_error.Node.NodeID}", "Ошибка"); return;
                }
            }
            else if (args.NodeAction == EditorModes.SelectNode && SelectedNodeID.HasValue)
            {
                Controller[SelectedNodeID.Value]!.Position = args.ActionPosition;
            }
            else return;
            Invalidate();
        }

        private Image BuildEditorGrid(Size size)
        {
            var grid_bitmap = new Bitmap(size.Width, size.Height);
            using (var grid_graphic = Graphics.FromImage(grid_bitmap))
            {
                var shift_x = movingposition_buffer.X * NodeSize / size.Width * NodeMovingSpeed * 2;
                var shift_y = movingposition_buffer.Y * NodeSize / size.Height * NodeMovingSpeed * 2;

                for (int i = 0; i < size.Width; i += NodeSize)
                {
                    grid_graphic.DrawLine(new Pen(Brushes.Gray), new Point(i + shift_x, 0), new Point(i + shift_x, size.Height));
                    grid_graphic.DrawLine(new Pen(Brushes.Gray), new Point(0, i + shift_y), new Point(size.Width, i + shift_y));
                }
            }
            return grid_bitmap;
        }

        private void EditorComponentPaint(object? sender, PaintEventArgs args)
        {
            using (var grid_image = BuildEditorGrid(Size)) args.Graphics.DrawImage(grid_image, new Point(0, 0));

            var node_font = new Font(FontFamily.GenericSansSerif, NodeSize / 2);
            foreach (var connector in Controller.BuildNodeСonnectors())
            {
                args.Graphics.DrawLine(new Pen(NodeConnectorColor, NodeSize / 4), connector.LeftNode.Position, connector.RightNode.Position);
            }
            Controller.ToList().ForEach(delegate (NodeModel node_info)
            {
                var node_brush = new SolidBrush(NodeColor);

                var node_position = new Point(node_info.Position.X - NodeSize / 2, node_info.Position.Y - NodeSize / 2);
                var node_geometry = new Rectangle(node_position, new Size(NodeSize, NodeSize));

                if (SelectedNodeID.HasValue && node_info.NodeID == SelectedNodeID.Value)
                {
                    node_brush = new SolidBrush(NodeSelectColor);
                }
                args.Graphics.FillEllipse(node_brush, node_geometry);
                args.Graphics.DrawEllipse(new Pen(Brushes.DimGray, NodeBorderWidth), node_geometry);

                args.Graphics.DrawString(node_info.NodeID.ToString(), node_font, Brushes.White, node_position);
            });
        }

        private void EditorComponentMouseDown(object? sender, MouseEventArgs args)
        {
            if (args.Button == MouseButtons.Right) { movingbutton_hold = true; return; }

            NodeModel? current_node = default;
            foreach (var node_item in Controller)
            {
                var collision_check = (Controller as NodesController)!.NodeCollisionCheck(args.Location, node_item.NodeID);
                if (collision_check) { current_node = node_item; break; }
            }

            var selection_switch = default(bool);
            if (current_node != null)
            {
                if (!SelectedNodeID.HasValue && Mode == EditorModes.SelectNode)
                {
                    SelectedNodeID = current_node.NodeID;
                    selection_switch = true;
                }

                NodeClicked?.Invoke(this, new EditorActionEventArgs(Mode, args.Location)
                { NodeInstance = current_node });
            }
            else FieldClicked?.Invoke(this, new EditorActionEventArgs(Mode, args.Location));

            if (SelectedNodeID.HasValue && !selection_switch) SelectedNodeID = null;
        }
    }
}
