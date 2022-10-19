using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

namespace CSCourseWork
{
    internal class EditorComponent : System.Windows.Forms.Panel
    {
        public enum EditorModes : System.Byte { AddNode, RemoveNode, SelectNode };
        public class EditorActionEventArgs : System.EventArgs 
        {
            public EditorModes NodeAction { get; private set; }
            public Point ActionPosition { get; private set; }
            public NodesController.NodeInfo? NodeInstance { get; set; } = default;
            public EditorActionEventArgs(EditorModes action, Point position) 
                : base() { this.NodeAction = action; this.ActionPosition = position; }
        }

        public delegate void EditorActionEventHandler(object? sender, EditorActionEventArgs args);
        public event EditorActionEventHandler? NodeClicked;
        public event EditorActionEventHandler? FieldClicked;

        public EditorModes Mode { get; set; } = EditorModes.AddNode;
        public NodesController Controller { get; private set; } = new();
        public int? SelectedNodeID { get; private set; } = null;

        private const int DefaultNodeBorder = 3;
        private const int DefaultNodeSize = 50;

        public int NodeBorderWidth { get; private set; } = default(int);
        public Color NodeColor { get; private set; } = default(Color);
        public int NodeSize { get => this.Controller.NodeSize; }

        public EditorComponent(Form parent_form, int node_size, int node_border, Color node_color) : base()
        {
            (this.Controller.NodeSize, this.NodeBorderWidth, this.NodeColor) = (node_size, node_border, node_color);
            parent_form.Controls.Add(this);

            this.MouseClick += new MouseEventHandler(this.EditorComponentClick);
            this.Paint += new PaintEventHandler(this.EditorComponentPaint);

            this.NodeClicked += new EditorActionEventHandler(this.EditorComponentNodeClicked);
            this.FieldClicked += new EditorActionEventHandler(this.EditorComponentFieldClicked);
        }

        public EditorComponent(Form parent_form) 
            : this(parent_form, DefaultNodeSize, DefaultNodeBorder, Color.Black) { }

        private void EditorComponentNodeClicked(object? sender, EditorActionEventArgs args) 
        {
            if (args.NodeInstance == null) return;

            switch (args.NodeAction) 
            {
                case EditorModes.SelectNode:
                    if (this.SelectedNodeID.HasValue) 
                    {
                        this.Controller.SetNodeLinks(this.SelectedNodeID.Value, args.NodeInstance.NodeID);
                    }
                    break;
                case EditorModes.RemoveNode:
                    args.NodeInstance.NodeLinks.ForEach(delegate (NodesController.NodeInfo link_info) 
                    {
                        link_info.NodeLinks.Remove(args.NodeInstance);
                    });
                    this.Controller.RemoveNode(args.NodeInstance.NodeID); 
                    break;
                default: return;
            }
            this.Invalidate();
        }

        private void EditorComponentFieldClicked(object? sender, EditorActionEventArgs args)
        {
            if (args.NodeAction == EditorModes.AddNode)
            {
                try { this.Controller.AddNewNode(args.ActionPosition.X, args.ActionPosition.Y); }
                catch (NodeControllerException node_error)
                {
                    MessageBox.Show($"{node_error.Message} - Node: {node_error.Node.NodeID}", "Ошибка");
                }
            }
            else if (args.NodeAction == EditorModes.SelectNode && this.SelectedNodeID.HasValue )
            {
                this.Controller.NodesList.ElementAt(this.SelectedNodeID.Value - 1).Position = args.ActionPosition;
            }
            else return; 
            this.Invalidate();
        }

        private void EditorComponentPaint(object? sender, PaintEventArgs e)
        {
            using (var graphics = this.CreateGraphics())
            {
                var node_font = new Font(FontFamily.GenericSansSerif, this.NodeSize / 2);
                var node_brush = new SolidBrush(this.NodeColor);

                foreach (var connector in this.Controller.BuildNodeСonnectors()) 
                {
                    graphics.DrawLine(new Pen(Brushes.Crimson), connector.LeftNode.Position, connector.RightNode.Position);
                }

                this.Controller.ToList().ForEach(delegate (NodesController.NodeInfo node_info)
                {
                    if (SelectedNodeID.HasValue && node_info.NodeID == SelectedNodeID.Value)
                    {
                        
                    }

                    var node_geometry = new Rectangle(node_info.Position, new Size(this.NodeSize, this.NodeSize));

                    graphics.FillEllipse(node_brush, node_geometry);
                    graphics.DrawString(node_info.NodeID.ToString(), node_font, Brushes.White, node_info.Position);
                });
                node_brush.Dispose();
            }
        }

        //private void EditorComponentClick(object? sender, MouseEventArgs args)
        //{
        //    NodesController.NodeInfo? node_info = default;
        //    foreach (NodesController.NodeInfo node_item in this.Controller) 
        //    {
        //        var node_pos = node_item.Position;
        //        if (Sqrt(Pow(node_pos.X - args.X, 2) + Pow(node_pos.Y - args.Y, 2)) < NodeSize && node_info == null) 
        //        {
        //            node_info = node_item;
        //        }
        //    }
        //    if (node_info == null) this.FieldClicked?.Invoke(this, new EditorActionEventArgs(this.Mode, args.Location));
        //    else 
        //    {
        //        this.NodeClicked?.Invoke(this, new EditorActionEventArgs(this.Mode, args.Location)
        //        {
        //            NodeInstance = node_info
        //        });
        //    }
        //}

        // обновить версию метода EditorComponentClick до нижней
        //      |
        //      V

        private void EditorComponentClick(object? sender, MouseEventArgs args)
        {
            NodesController.NodeInfo? selected_node = default;
            foreach (NodesController.NodeInfo node_item in this.Controller)
            {
                var collision_check = this.Controller.NodeCollisionCheck(args.Location, node_item.NodeID);
                if (collision_check) { selected_node = node_item; break; }
            }
            if (selected_node != null)
            {
                
                if(!this.SelectedNodeID.HasValue) this.SelectedNodeID = selected_node.NodeID;

                this.NodeClicked?.Invoke(this, new EditorActionEventArgs(this.Mode, args.Location)
                { NodeInstance = selected_node });
            }
            else 
            {
                this.FieldClicked?.Invoke(this, new EditorActionEventArgs(this.Mode, args.Location));
            }

            if (this.SelectedNodeID.HasValue) this.SelectedNodeID = null;
        }
    }
}
