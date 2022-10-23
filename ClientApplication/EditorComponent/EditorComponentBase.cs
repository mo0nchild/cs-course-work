﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CSCourseWork.NodeController;
using static System.Math;

namespace CSCourseWork.EditorComponent
{
    public class EditorComponentBase : System.Windows.Forms.Panel, IEditorComponent
    {
        public event EditorActionEventHandler? NodeClicked;
        public event EditorActionEventHandler? FieldClicked;

        public INodesControllerWithConnectors Controller { get; private set; }
        public EditorModes Mode { get; set; } = default(EditorModes);
        public int? SelectedNodeID { get; private set; } = null;

        private bool movingbutton_hold = default(bool);
        private Point movingposition_buffer = default(Point);

        private const int DefaultNodeBorder = 3;
        private const int DefaultNodeSize = 40;
        private const int DefaultGridDelta = 5;

        public int NodeBorderWidth { get; set; } = default(int);
        public int NodeMovingSpeed { get; set; } = default(int) + 1;
        public Color NodeColor { get; set; } = default(Color);
        public Color NodeSelectColor { get; set; } = Color.Crimson;

        public int NodeSize { get => this.Controller.NodeSize; }

        public EditorComponentBase(Form parent_form, INodesControllerWithConnectors controller) : base()
        {
            (this.Controller, this.DoubleBuffered) = (controller, true);
            parent_form.Controls.Add(this);

            this.MouseDown += new MouseEventHandler(this.EditorComponentMouseDown);
            this.MouseMove += new MouseEventHandler(this.EditorComponentMouseMove);
            this.Paint += new PaintEventHandler(this.EditorComponentPaint);

            this.NodeClicked += new EditorActionEventHandler(this.EditorComponentNodeClicked);
            this.FieldClicked += new EditorActionEventHandler(this.EditorComponentFieldClicked);

            this.MouseWheel += new MouseEventHandler(this.EditorComponent_MouseWheel);
            this.MouseUp += new MouseEventHandler(delegate (object? sender, MouseEventArgs args)
                { if (args.Button == MouseButtons.Right) this.movingbutton_hold = false; });
        }
            
        public EditorComponentBase(Form parent_form) : this(parent_form, ) { }

        public void BuildGraphPath(List<NodeConnectorInfo> node_paths)
        {
            using (var graphic = this.CreateGraphics())
            {
                for (var index = 0; index < node_paths.Count; index++)
                {
                    using var node_brush = new SolidBrush(this.NodeSelectColor);

                    this.PaintEdgeWithArrow(graphic, node_paths[index], this.NodeSelectColor);
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

        public void PaintEdgeWithArrow(Graphics graphics, NodeConnectorInfo connector, Color color) 
        {
            int delta_x = connector.RightNode.Position.X - connector.LeftNode.Position.X, // x1 - x0
                delta_y = connector.RightNode.Position.Y - connector.LeftNode.Position.Y; // y1 - y0

            var S = Math.Sqrt(Math.Pow(delta_x, 2) + Math.Pow(delta_y, 2)) - (this.NodeSize / 2);
            var c = (double)delta_y / delta_x;

            var x = connector.LeftNode.Position.X + Math.Sign(delta_x) * (S / Math.Sqrt(1 + Math.Pow(c, 2)));
            var y = connector.LeftNode.Position.Y + Math.Sign(delta_x) * (S * c / Math.Sqrt(1 + Math.Pow(c, 2)));

            var line_pen = new Pen(color, this.NodeSize / 6) { CustomEndCap = new AdjustableArrowCap(3, 4, true) };
            graphics.DrawLine(line_pen, connector.LeftNode.Position, new Point((int)x, (int)y));
        }

        public void PaintNodeInstance(Graphics graphic, NodeModel node_info, Brush node_brush) 
        {
            var node_position = new Point(node_info.Position.X - this.NodeSize / 2, node_info.Position.Y - this.NodeSize / 2);
            var node_geometry = new Rectangle(node_position, new Size(this.NodeSize, this.NodeSize));

            graphic.FillEllipse(node_brush, node_geometry);
            graphic.DrawEllipse(new Pen(Brushes.DimGray, this.NodeBorderWidth), node_geometry);

            using (var node_font = new Font(FontFamily.GenericSansSerif, this.NodeSize / 2)) 
            {
                graphic.DrawString(node_info.NodeID.ToString(), node_font, Brushes.White, node_position);
            } 
        }

        private void EditorComponentPaint(object? sender, PaintEventArgs args)
        {
            using (var grid_image = this.BuildEditorGrid(this.Size)) args.Graphics.DrawImage(grid_image, new Point(0, 0));

            foreach (var connector in this.Controller.BuildNodeСonnectors())
            { this.PaintEdgeWithArrow(args.Graphics, connector, this.NodeColor); }

            this.Controller.ToList().ForEach(delegate (NodeModel node_info)
            {
                var node_color = (this.SelectedNodeID.HasValue && node_info.NodeID == this.SelectedNodeID.Value)
                    ? this.NodeSelectColor : this.NodeColor;

                this.PaintNodeInstance(args.Graphics, node_info, new SolidBrush(node_color));
            });
        }

        private void EditorComponent_MouseWheel(object? sender, MouseEventArgs args)
        {
            var buffer_delta = this.Controller.NodeSize + Math.Sign(args.Delta) * DefaultGridDelta;
            if (buffer_delta > 0)
            {
                foreach (var item in this.Controller)
                {
                    int position_x = item.Position.X / Controller.NodeSize * buffer_delta,
                        position_y = item.Position.Y / Controller.NodeSize * buffer_delta;
                    item.Position = new Point(position_x, position_y);
                }
                this.Controller.NodeSize = buffer_delta;
            }
            this.Invalidate();
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
                catch (NodeControllerException node_error)
                {
                    MessageBox.Show($"{node_error.Message} - Node: {node_error.Node.NodeID}", "Ошибка"); return;
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
