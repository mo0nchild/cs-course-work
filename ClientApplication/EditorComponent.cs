﻿using System;
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

namespace CSCourseWork
{
    internal class EditorComponent : System.Windows.Forms.Panel
    {
        public enum EditorModes : System.Byte { AddNode, RemoveNode, SelectNode };
        public class EditorActionEventArgs : System.EventArgs 
        {
            public EditorModes NodeAction { get; private set; } = default;
            public Point ActionPosition { get; private set; } = default;
            public NodeModel? NodeInstance { get; set; } = default;
            public EditorActionEventArgs(EditorModes action, Point position) 
                : base() { this.NodeAction = action; this.ActionPosition = position; }
        }

        public delegate void EditorActionEventHandler(object? sender, EditorActionEventArgs args);
        public event EditorActionEventHandler? NodeClicked;
        public event EditorActionEventHandler? FieldClicked;

        public EditorModes Mode { get; set; } = EditorModes.AddNode;
        public NodesController Controller { get; private set; } = new();
        public int? SelectedNodeID { get; private set; } = null;

        private bool movingbutton_hold = default(bool);
        private Point movingposition_buffer = default(Point);

        private const int DefaultNodeBorder = 3;
        private const int DefaultNodeSize = 50;
        private const int DefaultGridDelta = 5;

        public int NodeBorderWidth { get; set; } = default(int);
        public int NodeMovingSpeed { get; set; } = 2;
        public Color NodeColor { get; set; } = default(Color);
        public Color NodeSelectColor { get; set; } = Color.Crimson;
        public Color NodeConnectorColor { get; set; } = Color.Black;
        
        public int NodeSize { get => this.Controller.NodeSize; }

        public EditorComponent(Form parent_form, int node_size, int node_border, Color node_color) : base()
        {
            (this.Controller.NodeSize, this.NodeBorderWidth, this.NodeColor) = (40, node_border, node_color);
            parent_form.Controls.Add(this);
            this.DoubleBuffered = true;
            
            this.MouseDown += new MouseEventHandler(this.EditorComponentMouseDown);
            this.MouseMove += new MouseEventHandler(EditorComponentMouseMove);
            this.Paint += new PaintEventHandler(this.EditorComponentPaint);

            this.NodeClicked += new EditorActionEventHandler(this.EditorComponentNodeClicked);
            this.FieldClicked += new EditorActionEventHandler(this.EditorComponentFieldClicked);

            this.MouseWheel += EditorComponent_MouseWheel;

            this.MouseUp += new MouseEventHandler(delegate (object? sender, MouseEventArgs args)
                { if (args.Button == MouseButtons.Right) this.movingbutton_hold = false; });
        }

        public EditorComponent(Form parent_form) 
            : this(parent_form, DefaultNodeSize, DefaultNodeBorder, Color.Black) { }

        private void EditorComponent_MouseWheel(object? sender, MouseEventArgs args)
        {
            var buffer_delta = this.Controller.NodeSize + Math.Sign(args.Delta) * EditorComponent.DefaultGridDelta;
            if (buffer_delta > 0)
            {
                foreach (var item in this.Controller)
                {
                    int position_x = (int)(item.Position.X / this.Controller.NodeSize * buffer_delta),
                        position_y = (int)(item.Position.Y / this.Controller.NodeSize * buffer_delta);
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

            var current_position = this.movingposition_buffer;
            int moving_direction_x = Math.Sign(args.X - current_position.X),
                moving_direction_y = Math.Sign(args.Y - current_position.Y);

            foreach (var node_info in this.Controller.NodesList)
            {
                node_info.Position = new Point(node_info.Position.X + (moving_direction_x * this.NodeMovingSpeed),
                    node_info.Position.Y + (moving_direction_y * this.NodeMovingSpeed));
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
            else if (args.NodeAction == EditorModes.SelectNode && this.SelectedNodeID.HasValue )
            {
                this.Controller[this.SelectedNodeID.Value]!.Position = args.ActionPosition;
            }
            else return; 
            this.Invalidate();
        }

        private System.Drawing.Image BuildEditorGrid(System.Drawing.Size size) 
        {
            var grid_bitmap = new Bitmap(size.Width, size.Height);
            using (var grid_graphic = Graphics.FromImage(grid_bitmap))
            {
                var shift_x = movingposition_buffer.X * this.NodeSize / size.Width * this.NodeMovingSpeed * 2;
                var shift_y = movingposition_buffer.Y * this.NodeSize / size.Height * this.NodeMovingSpeed * 2;

                for (int i = 0; i < size.Width; i += this.NodeSize)
                {
                    grid_graphic.DrawLine(new Pen(Brushes.Gray), new Point(i + shift_x, 0), new Point(i + shift_x, size.Height));
                    grid_graphic.DrawLine(new Pen(Brushes.Gray), new Point(0, i + shift_y), new Point(size.Width, i + shift_y));
                }
            }
            return grid_bitmap;
        }

        private void EditorComponentPaint(object? sender, PaintEventArgs args)
        {
            using (var grid_image = this.BuildEditorGrid(this.Size)) args.Graphics.DrawImage(grid_image, new Point(0, 0));

            var node_font = new Font(FontFamily.GenericSansSerif, this.NodeSize / 2);
            foreach (var connector in this.Controller.BuildNodeСonnectors())
            {
                args.Graphics.DrawLine(new Pen(this.NodeConnectorColor, this.NodeSize / 4), connector.LeftNode.Position, connector.RightNode.Position);
            }
            this.Controller.ToList().ForEach(delegate (NodeModel node_info)
            {
                var node_brush = new SolidBrush(this.NodeColor);

                var node_position = new Point(node_info.Position.X - this.NodeSize / 2, node_info.Position.Y - this.NodeSize / 2);
                var node_geometry = new Rectangle(node_position, new Size(this.NodeSize, this.NodeSize));

                if (SelectedNodeID.HasValue && node_info.NodeID == SelectedNodeID.Value)
                {
                    node_brush = new SolidBrush(this.NodeSelectColor);
                }
                args.Graphics.FillEllipse(node_brush, node_geometry);
                args.Graphics.DrawEllipse(new Pen(Brushes.DimGray, this.NodeBorderWidth), node_geometry);

                args.Graphics.DrawString(node_info.NodeID.ToString(), node_font, Brushes.White, node_position);
            });
        }

        private void EditorComponentMouseDown(object? sender, MouseEventArgs args)
        {
            if (args.Button == MouseButtons.Right) { movingbutton_hold = true; return; }

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