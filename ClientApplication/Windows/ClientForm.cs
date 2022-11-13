using System;
using System.Collections.Generic;
using System.Drawing;
using System.Configuration;
using System.Windows.Forms;
using System.Net.Http.Headers;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;

using CSCourseWork.EditorComponents;
using CSCourseWork.NodesControllers;
using CSCourseWork.Connected_Services.GraphServiceReference;
using CSCourseWork.Windows;
using CSCourseWork.EditorConfiguration;

namespace CSCourseWork
{
    internal partial class ClientForm : Form
    {
        private EditorComponentBase<NodesController> EditorInstance { get; set; } = default!;

        private System.Guid? profile_id = default;
        private System.Guid? ProfileID 
        {
            set { this.profileid_toolstrip_label.Text = (this.profile_id = value).ToString(); }
            get { return this.profile_id; }
        } 

        private System.String? project_name = default;
        private System.String? ProjectName
        {
            set { this.projectname_toolstrip_label.Text = (this.project_name = value); }
            get { return this.project_name; }
        }

        public ClientForm(System.Guid? profile_id) : base()
        {
            this.InitializeComponent(); this.InstallEditorComponent("editor_panel");
            this.ProfileID = profile_id;

            this.addop_button.Click += new EventHandler(AddOperationButtonClick);
            this.deleteop_button.Click += new EventHandler(DeleteOperationButtonClick);
            this.selectop_button.Click += new EventHandler(SelectOperationButtonClick);

            this.findpath_button.Click += new EventHandler(FindPathButtonClick);
            this.reset_button.Click += new EventHandler(ResetButtonClick);

            this.edges_listview.MouseClick += new MouseEventHandler(EdgesListViewMouseClick);
            this.nodes_treeview.AfterSelect += new TreeViewEventHandler(NodesTreeViewAfterSelect);
            this.nodes_treeview.MouseClick += new MouseEventHandler(NodesTreeViewMouseClick);

            this.app_propertygrid.PropertyValueChanged += AppPropertyGridPropertyValueChanged;
            this.editor_trackbar.ValueChanged += EditorTrackbarValueChanged;

            this.editorconf_toolstrip_menuitem.Click += delegate (object? sender, EventArgs args)
            {
                var settings = new EditorSettings<NodesController>(this.EditorInstance!,
                    new EditorConfigProvider(null));

                if (settings.ShowDialog() == DialogResult.OK) this.InstallEditorComponent("editor_panel");
            };
            this.info_toolstrip_button.Click += delegate (object? sender, EventArgs args)
            {
                MessageBox.Show("БИСТ-214 Тюленев Данил; " +
                    "Тема: Определение пути с минимальным числом дуг на основе поиска в ширину.");
            };
            this.accountconf_toolstrip_menuitem.Click += new EventHandler(AccountConfigurationClick);

            this.open_toolstrip_menuitem.Click += new EventHandler(OpenProjectClick);
            this.save_toolstrip_menuitem.Click += new EventHandler(SaveProjectClick);

            this.AddOperationButtonClick(this.addop_button, EventArgs.Empty);
        }

        private void LoggerPrintMessage(string message) => this.status_toolstrip_label.Text = message;

        

        private void EditProjectClick(object? sender, EventArgs args)
        {
            if (!this.ProfileID.HasValue) { MessageBox.Show("Необходимо авторизироваться", "Ошибка"); return; }
        }

        private void SaveProjectClick(object? sender, EventArgs args)
        {
            if (!this.ProfileID.HasValue) { MessageBox.Show("Необходимо авторизироваться", "Ошибка"); return; }
            var project_save = new ProjectSave(this.ProfileID.Value, this.EditorInstance.Controller)
            { ProjectName = this.ProjectName };

            if (project_save.ShowDialog() == DialogResult.Cancel) return;
            this.ProjectName = project_save.ProjectName;

            this.LoggerPrintMessage("Проект сохранён");
        }

        private void OpenProjectClick(object? sender, EventArgs args)
        {
            if (!this.ProfileID.HasValue) { MessageBox.Show("Необходимо авторизироваться", "Ошибка"); return; }

            var project_open = new ProjectOpen(this.ProfileID.Value) { ProjectName = this.ProjectName };
            if (project_open.ShowDialog() == DialogResult.OK) 
            {
                this.EditorInstance.Controller.NodesList = project_open.NodeList!.ConvertToClientData();
                this.EditorInstance.Invalidate(); 
                
                this.NodeInfoListUpdate(); this.LoggerPrintMessage("Проект загружен");
            }
            this.ProjectName = project_open.ProjectName;
        }

        private void AccountConfigurationClick(object? sender, EventArgs e)
        {
            if (!this.ProfileID.HasValue) { MessageBox.Show("Необходимо авторизироваться", "Ошибка"); return; }
            var profile_settings = new ProfileSettings(this.ProfileID.Value);

            if(profile_settings.ShowDialog() == DialogResult.Abort) this.Close();
            this.LoggerPrintMessage("Данные учёной записи изменены");
        }

        private void InstallEditorComponent(string editor_name)
        {
            this.pointer_panel.Hide(); this.Controls.RemoveByKey(editor_name);
            var editor_builder = new EditorComponentBuilder(this, typeof(NodesController));

            editor_builder.AddEditorGeometry(this.pointer_panel.Location, this.pointer_panel.Size)
                .AddEditorConfiguration(new EditorConfigProvider(null));

            if (this.EditorInstance != null) editor_builder.ControllerInstance = this.EditorInstance.Controller;
            this.LoggerPrintMessage("Настройки редактора загружены");

            this.EditorInstance = editor_builder.BuildEditor(editor_name);
            this.EditorInstance.NodeScaled += new EditorActionEventHandler(EditorInstanceNodeScaled);

            this.EditorInstance.FieldClicked += new EditorActionEventHandler(EditorComponentFieldClicked);
            this.EditorInstance.NodeClicked += new EditorActionEventHandler(EditorComponentNodeSelected);
        }

        private void EditorTrackbarValueChanged(object? sender, EventArgs args)
        {
            var min_scale = this.EditorInstance.NodeScaleRange.Min;
            var max_scale = this.EditorInstance.NodeScaleRange.Max;

            var adapted_value = this.editor_trackbar.Value * Math.Abs(max_scale - min_scale) / 100 + min_scale;

            try { this.EditorInstance.ScalingGraphView(adapted_value); }
            catch (EditorComponentException error) { MessageBox.Show(error.Message, "Ошибка"); }
        }

        private void EditorInstanceNodeScaled(object? sender, EditorActionEventArgs args)
        {
            if (args.NodeScale is null) return;

            var min_scale = this.EditorInstance.NodeScaleRange.Min;
            var max_scale = this.EditorInstance.NodeScaleRange.Max;

            var adapted_value = (double)Math.Abs(args.NodeScale.Value - min_scale) / (max_scale - min_scale) * 100;
            this.editor_trackbar.Value = ((int)adapted_value <= 0) ? 1 : (int)adapted_value;
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
            var edge_info = this.EditorInstance.Controller.BuildNodeСonnectors()[selected_edge];

            this.EditorInstance.BuildGraphPath(new List<NodesConnectorInfo>() { edge_info });

            if (args.Button == MouseButtons.Right)
            {
                this.BuildContextMenu(delegate (ToolStripItemClickedEventArgs args)
                {
                    if (args.ClickedItem.Text != "Удалить дугу") return;
                    this.EditorInstance.Controller.RemoveNodeLinks(edge_info.LeftNode.NodeID, edge_info.RightNode.NodeID);

                    (this.EditorInstance as Panel)?.Invalidate();
                    this.NodeInfoListUpdate();

                }, args.Location, new string[] { "Удалить дугу" });
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
                    if (args.ClickedItem.Text != "Удалить вершину") return;
                    this.EditorInstance.Controller.RemoveNode(treeview_node!.Index + 1);

                    (this.EditorInstance as Panel)?.Invalidate();
                    this.NodeInfoListUpdate();

                }, args.Location, new string[] { "Удалить вершину" });
            }
        }

        private void ResetButtonClick(object? sender, EventArgs args)
        {
            var controller = this.EditorInstance.Controller;
            while (controller.NodesList.Count > 0) controller.RemoveNode(1);

            this.LoggerPrintMessage("Поверхность редактора очищена");
            (this.EditorInstance as Panel)?.Invalidate();

            this.NodeInfoListUpdate();
        }

        private void FindPathButtonClick(object? sender, EventArgs args)
        {
            int nodeid_origin = default, nodeid_target = default;
            try
            {
                nodeid_origin = int.Parse(this.nodeorigin_combobox.Text.Split(' ')[1]);
                nodeid_target = int.Parse(this.nodetarget_combobox.Text.Split(' ')[1]);
            }
            catch (System.Exception error) { this.LoggerPrintMessage(error.Message); return; }

            try {
                using var client = new GraphServiceReference.GraphCalculatorClient();

                var service_inputdata = this.EditorInstance.Controller.ConvertToServiceData();
                var graph_path = client.FindPathByBFS(nodeid_origin, nodeid_target, service_inputdata);

                if (graph_path.Length > 0)
                { this.EditorInstance.BuildGraphPath(this.EditorInstance.Controller.ConvertToPath(graph_path)); }
            }
            catch (FaultException<GraphServiceReference.GraphCalculatorException> error) 
            {
                MessageBox.Show(error.Detail.Message, "Ошибка"); 
            }
            catch (CommunicationException error) { MessageBox.Show(error.Message, "Ошибка"); }
        }

        private void SelectOperationButtonClick(object? sender, EventArgs args)
        {
            this.EditorInstance.Mode = EditorModes.SelectNode;
            this.LoggerPrintMessage("Выбран инструмент соединения");

            this.addop_button.Enabled = this.deleteop_button.Enabled = true;
            (sender as Button)!.Enabled = false;
        }

        private void DeleteOperationButtonClick(object? sender, EventArgs args)
        {
            this.EditorInstance.Mode = EditorModes.RemoveNode;
            this.LoggerPrintMessage("Выбран инструмент удаления");

            this.addop_button.Enabled = this.selectop_button.Enabled = true;
            (sender as Button)!.Enabled = false;
        }

        private void AddOperationButtonClick(object? sender, EventArgs args)
        {
            this.EditorInstance.Mode = EditorModes.AddNode;
            this.LoggerPrintMessage("Выбран инструмент добавления");

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
                treeview_root.Nodes.Add($"Вершина ID: {node_info.NodeID}");

                this.nodeorigin_combobox.Items.Add($"ID: {node_info.NodeID}");
                this.nodetarget_combobox.Items.Add($"ID: {node_info.NodeID}");

                foreach (var link_id in node_info.NodeLinksID)
                {
                    treeview_root.Nodes[index].Nodes.Add($"Ссылка на Вершину ID: {link_id}");
                }
            }
            treeview_root.Expand();

            var edges_list = this.EditorInstance.Controller.BuildNodeСonnectors();
            this.edges_listview.Items.Clear();

            foreach (var edge_info in edges_list) 
            {
                var row_info = new string[] { (edge_info.EdgeId + 1).ToString(),
                    $"Узел1 ID: {edge_info.LeftNode.NodeID}", 
                    $"Узел2 ID: {edge_info.RightNode.NodeID}"
                };
                this.edges_listview.Items.Add(new ListViewItem(row_info));
            }
        }

        private void EditorComponentFieldClicked(object? sender, EditorActionEventArgs args) => this.NodeInfoListUpdate();

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