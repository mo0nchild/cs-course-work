using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

using CSCourseWork.NodesControllers;
using static System.Math;

namespace CSCourseWork.EditorComponents
{
    public class EditorComponent : EditorComponents.EditorComponentBase<NodesController>
    {
        public override event EditorComponents.EditorActionEventHandler? NodeClicked;
        public override event EditorComponents.EditorActionEventHandler? FieldClicked;

        public override event EditorComponents.EditorActionEventHandler? NodeScaled;

        private const int DefaultGridDelta = 5, MinimunScaleValue = 10, NodeDefaultSize = 10;
        private bool movingbutton_hold = default(bool);
        private Point movingposition_buffer = default(Point);

        public override EditorColor NodeSelectColor { get; set; } = new EditorColor(220, 20, 60);

        public override EditorColor EditorBackColor 
        {
            get => new EditorColor(BackColor.R, BackColor.G, BackColor.B);
            set => this.BackColor = value.ConvertToColor();
        }

        public override int NodeSize 
        {
            protected set { 
                this.Controller.NodeSize = (value >= node_scale.Min && value <= node_scale.Max) 
                    ? value : this.Controller.NodeSize; 
            }
            get { return this.Controller.NodeSize; }
        }

        private EditorModes editor_mode = default(EditorModes);
        public override EditorModes Mode 
        {
            set {
                this.editor_mode = value;
                switch (value) 
                {
                    case EditorModes.AddNode: this.Cursor = Cursors.Cross; break;
                    case EditorModes.RemoveNode: this.Cursor = Cursors.No; break;
                    case EditorModes.SelectNode: this.Cursor = Cursors.Hand; break;
                }
            }
            get => this.editor_mode;
        }

        private int? selected_nodeid = default(int?);
        public override int? SelectedNodeID
        {
            set => this.selected_nodeid =
                (value > 0 || value <= this.Controller.NodesList.Count) ? value : null;

            get => this.selected_nodeid;
        }

        private EditorComponents.EditorScale node_scale = new(MinimunScaleValue, 100);
        public override EditorComponents.EditorScale NodeScaleRange
        {
            get { return this.node_scale; }
            set {
                if (value.Min < MinimunScaleValue || value.Max < MinimunScaleValue
                    || value.Max - value.Min < MinimunScaleValue) 
                {
                    throw new EditorComponentException("Неверные значение масштаба");
                }
                this.NodeSize = (value.Max - value.Min) / 2 + value.Min;
                this.node_scale = value;
            }
        }

        public EditorComponent(Form parent_form, NodesController controller) : base(controller, parent_form)
        {
            (this.DoubleBuffered, this.BorderStyle, this.NodeSize) = (true, BorderStyle.FixedSingle, NodeDefaultSize);

            this.MouseDown += new MouseEventHandler(this.EditorComponentMouseDown);
            this.MouseMove += new MouseEventHandler(this.EditorComponentMouseMove);
            this.Paint += new PaintEventHandler(this.EditorComponentPaint);

            this.NodeClicked += new EditorActionEventHandler(this.EditorComponentNodeClicked);
            this.FieldClicked += new EditorActionEventHandler(this.EditorComponentFieldClicked);

            this.MouseWheel += new MouseEventHandler(this.EditorComponent_MouseWheel);
            this.MouseUp += new MouseEventHandler(delegate (object? sender, MouseEventArgs args)
                { if (args.Button == MouseButtons.Right) this.movingbutton_hold = false; });
        }
            
        public EditorComponent(Form parent_form) : this(parent_form, new NodesController()) { }

        public override void BuildGraphPath(List<NodesConnectorInfo> node_paths)
        {
            using (var graphic = this.CreateGraphics())
            {
                this.EditorComponentPaint(this, new PaintEventArgs(graphic, new Rectangle(this.Location, this.Size)));
                for (var index = 0; index < node_paths.Count; index++)
                {
                    using var node_brush = new SolidBrush(this.NodeSelectColor.ConvertToColor());

                    this.PaintEdgeWithArrow(graphic, node_paths[index], this.NodeSelectColor.ConvertToColor());
                    this.PaintNodeInstance(graphic, node_paths[index].LeftNode, node_brush);

                    if (index == node_paths.Count - 1)
                    {
                        this.PaintNodeInstance(graphic, node_paths[index].RightNode, node_brush);
                    }
                }
            }
        }

        private Image BuildEditorGrid(Size size)
        {
            var grid_bitmap = new Bitmap(size.Width, size.Height);
            using (var grid_graphic = Graphics.FromImage(grid_bitmap))
            {
                var shift_x = this.movingposition_buffer.X * this.NodeSize / size.Width * this.NodeMovingSpeed * 2;
                var shift_y = this.movingposition_buffer.Y * this.NodeSize / size.Height * this.NodeMovingSpeed * 2;

                for (int i = 0; i < size.Width; i += this.NodeSize)
                {
                    grid_graphic.DrawLine(new Pen(Brushes.Gray), new Point(i + shift_x, 0), new Point(i + shift_x, size.Height));
                    grid_graphic.DrawLine(new Pen(Brushes.Gray), new Point(0, i + shift_y), new Point(size.Width, i + shift_y));
                }
            }
            return grid_bitmap;
        }

        //public void PaintEdgeWithArrow(Graphics graphics, NodesConnectorInfo connector, Color color) 
        //{
        //    int delta_x = connector.RightNode.Position.X - connector.LeftNode.Position.X, // x1 - x0
        //        delta_y = connector.RightNode.Position.Y - connector.LeftNode.Position.Y; // y1 - y0

        //    var S = Math.Sqrt(Math.Pow(delta_x, 2) + Math.Pow(delta_y, 2)) - (this.NodeSize / 2);
        //    var c = (delta_x == 0) ? 100 : ((double)delta_y / delta_x);

        //    var x = connector.LeftNode.Position.X + Math.Sign(delta_x) * (S / Math.Sqrt(1 + Math.Pow(c, 2)));
        //    var y = connector.LeftNode.Position.Y + Math.Sign(delta_x) * (S * c / Math.Sqrt(1 + Math.Pow(c, 2)));

        //    var line_pen = new Pen(color, this.NodeSize / 6) { CustomEndCap = new AdjustableArrowCap(3, 4, true) };
        //    graphics.DrawLine(line_pen, connector.LeftNode.Position, new Point((int)x, (int)y));
        //}

        public override void PaintEdgeWithArrow(Graphics graphics, NodesConnectorInfo connector, Color color)
        {
            int delta_x = connector.RightNode.Position.X - connector.LeftNode.Position.X, // x1 - x0
                delta_y = connector.RightNode.Position.Y - connector.LeftNode.Position.Y; // y1 - y0

            double D = Math.Sqrt(Math.Pow(delta_x, 2) + Math.Pow(delta_y, 2)), S = D - (this.NodeSize / 2);

            var b = delta_y * (1 - ((double)this.NodeSize / 2 / D));
            var a = Math.Sqrt(Math.Pow(S, 2) - Math.Pow(b, 2));

            var x = connector.LeftNode.Position.X + Math.Sign(delta_x) * a;
            var y = connector.LeftNode.Position.Y + b;

            var line_pen = new Pen(color, this.NodeSize / 6) { CustomEndCap = new AdjustableArrowCap(3, 4, true) };
            graphics.DrawLine(line_pen, connector.LeftNode.Position, new Point((int)x, (int)y));
        }

        public override void PaintNodeInstance(Graphics graphic, NodeModel node_info, Brush node_brush) 
        {
            var node_position = new Point(node_info.Position.X - this.NodeSize / 2, node_info.Position.Y - this.NodeSize / 2);
            var node_geometry = new Rectangle(node_position, new Size(this.NodeSize, this.NodeSize));

            graphic.FillEllipse(node_brush, node_geometry);
            graphic.DrawEllipse(new Pen(Brushes.DimGray, this.NodeBorderWidth), node_geometry);

            using (var node_font = new Font(this.NodeFontFamily, this.NodeSize / 2)) 
            {
                graphic.DrawString(node_info.NodeID.ToString(), node_font, Brushes.White, node_position);
            }
        }

        public override void ScalingGraphView(int scale_value)
        {
            if (scale_value >= this.node_scale.Min && scale_value <= this.node_scale.Max)
            {
                foreach (var item in this.Controller)
                {
                    int position_x = item.Position.X / Controller.NodeSize * scale_value,
                        position_y = item.Position.Y / Controller.NodeSize * scale_value;
                    item.Position = new Point(position_x, position_y);
                }
                this.Controller.NodeSize = scale_value;
            }
            else throw new EditorComponentException("Значение вне диапазона");

            this.Invalidate();
        }

        private void EditorComponent_MouseWheel(object? sender, MouseEventArgs args)
        {
            var buffer_delta = this.Controller.NodeSize + Math.Sign(args.Delta) * DefaultGridDelta;
            try { this.ScalingGraphView(buffer_delta); } catch (EditorComponentException) { return; }

            this.NodeScaled?.Invoke(this, new EditorActionEventArgs(this.Mode, default)
            { NodeScale = buffer_delta });
        }

        private void EditorComponentPaint(object? sender, PaintEventArgs args)
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

        private void EditorComponentNodeClicked(object? sender, EditorActionEventArgs args)
        {
            if (args.NodeInstance == null) return;
            switch (args.NodeAction)
            {
                case EditorModes.RemoveNode: this.Controller.RemoveNode(args.NodeInstance.NodeID); break;
                case EditorModes.SelectNode when this.SelectedNodeID.HasValue:
                    this.Controller.SetNodeLinks(this.SelectedNodeID.Value, args.NodeInstance.NodeID); break;
                default: return;
            }
            this.Invalidate();
        }

        private void EditorComponentMouseMove(object? sender, MouseEventArgs args)
        {
            if (this.movingbutton_hold != true) return;

            int moving_direction_x = Math.Sign(args.X - this.movingposition_buffer.X),
                moving_direction_y = Math.Sign(args.Y - this.movingposition_buffer.Y);

            foreach (var node_info in this.Controller.NodesList)
            {
                node_info.Position = new Point(node_info.Position.X + moving_direction_x * this.NodeMovingSpeed,
                    node_info.Position.Y + moving_direction_y * this.NodeMovingSpeed);
            }
            this.movingposition_buffer = args.Location;
            this.Invalidate();
        }

        private void EditorComponentFieldClicked(object? sender, EditorActionEventArgs args)
        {
            if (args.NodeAction == EditorModes.AddNode)
            {
                try { this.Controller.AddNewNode(args.ActionPosition.X, args.ActionPosition.Y); }
                catch (NodesControllerException node_error)
                {
                    MessageBox.Show($"{node_error.Message} - Node: {node_error.Node?.NodeID}", "Ошибка"); return;
                }
            }
            else if (args.NodeAction == EditorModes.SelectNode && this.SelectedNodeID.HasValue)
            {
                this.Controller[this.SelectedNodeID.Value]!.Position = args.ActionPosition;
            }
            else return;
            this.Invalidate();
        }

        private void EditorComponentMouseDown(object? sender, MouseEventArgs args)
        {
            if (args.Button == MouseButtons.Right) { this.movingbutton_hold = true; return; }

            NodeModel? current_node = default;
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
                    this.SelectedNodeID = current_node.NodeID;
                    selection_switch = true;
                }

                this.NodeClicked?.Invoke(this, new EditorActionEventArgs(this.Mode, args.Location)
                { NodeInstance = current_node });
            }
            else this.FieldClicked?.Invoke(this, new EditorActionEventArgs(this.Mode, args.Location));

            if (this.SelectedNodeID.HasValue && !selection_switch) this.SelectedNodeID = null;
        }
    }
}
