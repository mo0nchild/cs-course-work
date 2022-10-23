using System;
using System.Collections.Generic;
using System.Drawing;
using System.Configuration;
using System.Windows.Forms;

using CSCourseWork.EditorComponents;
using CSCourseWork.NodesControllers;
using System.ComponentModel;
using System.Net.Http.Headers;

namespace CSCourseWork
{
    internal partial class ClientForm : Form
    {
        public IEditorComponent<NodesController> EditorInstance { get; private set; }

        public ClientForm()
        {
            this.InitializeComponent();

            var editor_builder = new EditorComponentBuilder(this, new NodesController())
                .AddEditorNodeColor(Color.Black, Color.Crimson, Color.White)
                .AddEditorNodeSize(40, 2)
                .AddEditorGeometry(new Point(284, 60), new Size(568, 426))
                .AddEditorMovingSpeed(2);
            this.EditorInstance = editor_builder.BuildEditor();

            this.EditorInstance.NodeClicked += new EditorActionEventHandler(EditorComponentNodeSelected);
            this.EditorInstance.FieldClicked += new EditorActionEventHandler(EditorComponentFieldClicked);

            this.addop_button.Click += new EventHandler(AddOperationButtonClick);
            this.deleteop_button.Click += new EventHandler(DeleteOperationButtonClick);
            this.selectop_button.Click += new EventHandler(SelectOperationButtonClick);

            this.findpath_button.Click += new EventHandler(FindPathButtonClick);
            this.reset_button.Click += new EventHandler(ResetButtonClick);

            this.edges_listview.DoubleClick += new EventHandler(EdgesListViewDoubleClick);
            this.edges_listview.Click += new EventHandler(EdgesListViewClick);

            this.nodes_treeview.AfterSelect += new TreeViewEventHandler(NodesTreeViewAfterSelect);
            this.nodes_treeview.DoubleClick += new EventHandler(NodesTreeViewDoubleClick);

            this.app_propertygrid.PropertyValueChanged += AppPropertyGridPropertyValueChanged;
            this.AddOperationButtonClick(this.addop_button, EventArgs.Empty);
        }

        private void AppPropertyGridPropertyValueChanged(object? s, PropertyValueChangedEventArgs args) 
            => (this.EditorInstance as Panel)?.Invalidate();

        private void EdgesListViewClick(object? sender, EventArgs args)
        {
            if (this.edges_listview.SelectedItems.Count <= 0) 
            { 
                (this.EditorInstance as Panel)?.Invalidate();
                Console.WriteLine("asdasdasd");
                return; 
            }

            var selected_edge = this.edges_listview.SelectedIndices[0];
            var edge_list = this.EditorInstance.Controller.BuildNodeÑonnectors();

            this.EditorInstance.BuildGraphPath(
                new List<NodesConnectorInfo>() { edge_list[selected_edge] });
        }
        private void EdgesListViewDoubleClick(object? sender, EventArgs args)
        {
            if (this.edges_listview.SelectedItems.Count <= 0) return;

            var selected_edge = this.edges_listview.SelectedIndices[0];
            var edge_info = this.EditorInstance.Controller.BuildNodeÑonnectors()[selected_edge];

            this.EditorInstance.Controller.RemoveNodeLinks(
                edge_info.LeftNode.NodeID, edge_info.RightNode.NodeID);

            (this.EditorInstance as Panel)?.Invalidate();
            this.NodeInfoListUpdate();
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

        private void NodesTreeViewDoubleClick(object? sender, EventArgs args)
        {
            var treeview_node = this.nodes_treeview.SelectedNode;

            if (treeview_node.Level != 1) return;
            this.EditorInstance.Controller.RemoveNode(treeview_node.Index + 1);

            (this.EditorInstance as Panel)?.Invalidate();
            this.NodeInfoListUpdate();
        }

        private void ResetButtonClick(object? sender, EventArgs args)
        {
            var controller = this.EditorInstance.Controller;
            while (controller.NodesList.Count > 0) 
            {
                controller.RemoveNode(1);
                (this.EditorInstance as Panel)?.Invalidate();
            }
            this.NodeInfoListUpdate();
        }

        private void LoggerPrintMessage(string message) => this.info_textbox.Text = message;

        private void FindPathButtonClick(object? sender, EventArgs args)
        {
            int nodeid_origin = default, nodeid_target = default;
            try
            {
                nodeid_origin = int.Parse(this.nodeorigin_combobox.SelectedText.Split(' ')[1]);
                nodeid_target = int.Parse(this.nodetarget_combobox.SelectedText.Split(' ')[1]);
            }
            catch (System.Exception error) { this.LoggerPrintMessage(error.Message); return; }

            //using (var client = new GraphServiceReference.GraphCalculatorClient())
            //{
            //    client.FindPathByBFSAsync(nodeid_origin, nodeid_target, new GraphServiceReference.NodeData[] {})
            //        .ContinueWith(delegate (Task<int[]> value) 
            //    {
            //        this.EditorInstance.BuildGraphPath();
            //    });
            //}
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

            for (var index = 0; index < this.EditorInstance.Controller.NodesList.Count; index++)
            {
                var node_info = this.EditorInstance.Controller[index + 1]!;
                treeview_root.Nodes.Add($"Âåðøèíà ID: {node_info.NodeID}");

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
            this.NodeInfoListUpdate();
        }

    }
}