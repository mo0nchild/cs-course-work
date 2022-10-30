using System;
using System.Collections.Generic;
using System.Drawing;
using System.Configuration;
using System.Windows.Forms;
using System.Net.Http.Headers;
using System.ComponentModel;
using System.Diagnostics;

using CSCourseWork.EditorComponents;
using CSCourseWork.NodesControllers;
using CSCourseWork.Connected_Services.GraphServiceReference;
using System.ServiceModel;

namespace CSCourseWork
{
    internal partial class ClientForm : Form
    {
        public IEditorComponent<NodesController> EditorInstance { get; private set; }

        public ClientForm()
        {
            this.InitializeComponent();

            var editor_builder = new EditorComponentBuilder(this, new NodesController())
                .AddEditorGeometry(this.pointer_panel.Location, this.pointer_panel.Size)
                .AddEditorNodeColor(Color.Black, Color.Crimson, Color.White)
                .AddEditorNodeSize(40, 2) 
                .AddEditorMovingSpeed(2);
            this.EditorInstance = editor_builder.BuildEditor();
            this.pointer_panel.Hide();

            this.EditorInstance.NodeClicked += new EditorActionEventHandler(EditorComponentNodeSelected);
            this.EditorInstance.FieldClicked += new EditorActionEventHandler(EditorComponentFieldClicked);

            this.addop_button.Click += new EventHandler(AddOperationButtonClick);
            this.deleteop_button.Click += new EventHandler(DeleteOperationButtonClick);
            this.selectop_button.Click += new EventHandler(SelectOperationButtonClick);

            this.findpath_button.Click += new EventHandler(FindPathButtonClick);
            this.reset_button.Click += new EventHandler(ResetButtonClick);

            this.edges_listview.MouseClick += new MouseEventHandler(EdgesListViewMouseClick);
            this.nodes_treeview.AfterSelect += new TreeViewEventHandler(NodesTreeViewAfterSelect);
            this.nodes_treeview.MouseClick += new MouseEventHandler(NodesTreeViewMouseClick);

            this.app_propertygrid.PropertyValueChanged += AppPropertyGridPropertyValueChanged;
            this.AddOperationButtonClick(this.addop_button, EventArgs.Empty);
        }

        private void AppPropertyGridPropertyValueChanged(object? s, PropertyValueChangedEventArgs args) 
            => (this.EditorInstance as Panel)?.Invalidate();

        private void BuildContextMenu(Action<ToolStripItemClickedEventArgs> delete_action, Point position, string[] items) 
        {
            var context_menu = new ContextMenuStrip() { AutoSize = true };
            foreach (var item_text in items) 
            {
                context_menu.Items.Add(new ToolStripButton(item_text, Resourses.DeleteIcon) { Width = 100 });
            }
            context_menu.ItemClicked += delegate (object? sender, ToolStripItemClickedEventArgs args)
            {
                delete_action(args);
            };
            context_menu.Show(this, position);
        }

        private void EdgesListViewMouseClick(object? sender, MouseEventArgs args)
        {
            if (this.edges_listview.SelectedItems.Count <= 0) return;

            var selected_edge = this.edges_listview.SelectedIndices[0];
            var edge_info = this.EditorInstance.Controller.BuildNodeÑonnectors()[selected_edge];

            this.EditorInstance.BuildGraphPath(new List<NodesConnectorInfo>() { edge_info });

            if (args.Button == MouseButtons.Right)
            {
                this.BuildContextMenu(delegate (ToolStripItemClickedEventArgs args)
                {
                    if (args.ClickedItem.Text != "Óäàëèòü äóãó") return;
                    this.EditorInstance.Controller.RemoveNodeLinks(edge_info.LeftNode.NodeID, edge_info.RightNode.NodeID);

                    (this.EditorInstance as Panel)?.Invalidate();
                    this.NodeInfoListUpdate();

                }, args.Location, new string[] { "Óäàëèòü äóãó" });
            }
        }

        private void NodesTreeViewAfterSelect(object? sender, TreeViewEventArgs args)
        {
            if (args.Node == null) return;
            switch (args.Node.Level)
            {
                case 1:
                    this.EditorInstance.SelectedNodeID = args.Node.Index + 1;
                    this.app_propertygrid.SelectedObject = this.EditorInstance.Controller[args.Node.Index + 1];
                    break;
                case 2:
                    var parent_node_info = this.EditorInstance.Controller[args.Node.Parent.Index + 1]!;
                    this.EditorInstance.SelectedNodeID = parent_node_info.NodeLinksID[args.Node.Index];
                    break;
                default: this.app_propertygrid.SelectedObject = this.EditorInstance.SelectedNodeID = null; break;
            }
            (this.EditorInstance as Panel)?.Invalidate();
        }

        private void NodesTreeViewMouseClick(object? sender, MouseEventArgs args)
        {
            var treeview_node = this.nodes_treeview.SelectedNode;
            if (treeview_node != null && treeview_node.Level != 1) return;

            if (args.Button == MouseButtons.Right) 
            {
                this.BuildContextMenu(delegate (ToolStripItemClickedEventArgs args)
                {
                    if (args.ClickedItem.Text != "Óäàëèòü âåðøèíó") return;
                    this.EditorInstance.Controller.RemoveNode(treeview_node!.Index + 1);

                    (this.EditorInstance as Panel)?.Invalidate();
                    this.NodeInfoListUpdate();

                }, args.Location, new string[] { "Óäàëèòü âåðøèíó" });
            }
        }

        private void ResetButtonClick(object? sender, EventArgs args)
        {
            var controller = this.EditorInstance.Controller;
            while (controller.NodesList.Count > 0) controller.RemoveNode(1);

            (this.EditorInstance as Panel)?.Invalidate();
            this.NodeInfoListUpdate();
        }

        private void LoggerPrintMessage(string message) => this.info_textbox.Text = message;

        private void FindPathButtonClick(object? sender, EventArgs args)
        {
            int nodeid_origin = default, nodeid_target = default;
            try
            {
                nodeid_origin = int.Parse(this.nodeorigin_combobox.Text.Split(' ')[1]);
                nodeid_target = int.Parse(this.nodetarget_combobox.Text.Split(' ')[1]);
            }
            catch (System.Exception error) { this.LoggerPrintMessage(error.Message); return; }

            using (var client = new GraphServiceReference.GraphCalculatorClient())
            {
                var service_inputdata = this.EditorInstance.Controller.ConvertToServiceData();
                try
                {
                    var graph_path = client.FindPathByBFS(nodeid_origin, nodeid_target, service_inputdata);
                    if(graph_path.Length > 0) 
                    {
                        this.EditorInstance.BuildGraphPath(this.EditorInstance.Controller.ConvertToPath(graph_path));
                    }
                }
                catch (FaultException error) { MessageBox.Show(error.Message); }
            }
        }

        private void SelectOperationButtonClick(object? sender, EventArgs args)
        {
            this.EditorInstance.Mode = EditorModes.SelectNode;

            this.addop_button.Enabled = this.deleteop_button.Enabled = true;
            (sender as Button)!.Enabled = false;
        }

        private void DeleteOperationButtonClick(object? sender, EventArgs args)
        {
            this.EditorInstance.Mode = EditorModes.RemoveNode;

            this.addop_button.Enabled = this.selectop_button.Enabled = true;
            (sender as Button)!.Enabled = false;
        }

        private void AddOperationButtonClick(object? sender, EventArgs args)
        {
            this.EditorInstance.Mode = EditorModes.AddNode;

            this.selectop_button.Enabled = this.deleteop_button.Enabled = true;
            (sender as Button)!.Enabled = false;
        }

        private void NodeInfoListUpdate() 
        {
            var treeview_root = this.nodes_treeview.Nodes[0];
            treeview_root.Nodes.Clear();

            this.nodeorigin_combobox.Items.Clear();
            this.nodetarget_combobox.Items.Clear();

            for (var index = 0; index < this.EditorInstance.Controller.NodesList.Count; index++)
            {
                var node_info = this.EditorInstance.Controller[index + 1]!;
                treeview_root.Nodes.Add($"Âåðøèíà ID: {node_info.NodeID}");

                this.nodeorigin_combobox.Items.Add($"ID: {node_info.NodeID}");
                this.nodetarget_combobox.Items.Add($"ID: {node_info.NodeID}");

                foreach (var link_id in node_info.NodeLinksID)
                {
                    treeview_root.Nodes[index].Nodes.Add($"Ññûëêà íà Âåðøèíó ID: {link_id}");
                }
            }
            treeview_root.Expand();

            var edges_list = this.EditorInstance.Controller.BuildNodeÑonnectors();
            this.edges_listview.Items.Clear();

            foreach (var edge_info in edges_list) 
            {
                var row_info = new string[] { (edge_info.EdgeId + 1).ToString(),
                    $"Óçåë1 ID: {edge_info.LeftNode.NodeID}", 
                    $"Óçåë2 ID: {edge_info.RightNode.NodeID}"
                };
                this.edges_listview.Items.Add(new ListViewItem(row_info));
            }
        }

        private void EditorComponentFieldClicked(object? sender, EditorActionEventArgs args)
        {
            this.NodeInfoListUpdate();
        }

        private void EditorComponentNodeSelected(object? sender, EditorActionEventArgs args)
        {
            var treeview_rootnode = this.nodes_treeview.Nodes[0];
            var selected_node = this.EditorInstance.SelectedNodeID;

            this.NodeInfoListUpdate();

            if (selected_node.HasValue) 
            {
                this.nodes_treeview.SelectedNode = treeview_rootnode.Nodes[selected_node.Value - 1];
                this.nodes_treeview.Select();
            }
        }
    }
}