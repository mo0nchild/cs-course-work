namespace CSCourseWork
{
    internal partial class ClientForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClientForm));
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Список вершин");
            this.info_toolstrip_button = new System.Windows.Forms.ToolStripButton();
            this.app_toolstrip = new System.Windows.Forms.ToolStrip();
            this.project_toolstrip_button = new System.Windows.Forms.ToolStripDropDownButton();
            this.open_toolstrip_menuitem = new System.Windows.Forms.ToolStripMenuItem();
            this.save_toolstrip_menuitem = new System.Windows.Forms.ToolStripMenuItem();
            this.settings_toolstrip_button = new System.Windows.Forms.ToolStripDropDownButton();
            this.editorconf_toolstrip_menuitem = new System.Windows.Forms.ToolStripMenuItem();
            this.accountconf_toolstrip_menuitem = new System.Windows.Forms.ToolStripMenuItem();
            this.edges_listview = new System.Windows.Forms.ListView();
            this.edgeid_column = new System.Windows.Forms.ColumnHeader();
            this.edgeleftnode_column = new System.Windows.Forms.ColumnHeader();
            this.edgerightnode_column = new System.Windows.Forms.ColumnHeader();
            this.nodestree_tabpage = new System.Windows.Forms.TabPage();
            this.nodes_treeview = new System.Windows.Forms.TreeView();
            this.app_tabcontrol = new System.Windows.Forms.TabControl();
            this.edgeslist_tabpage = new System.Windows.Forms.TabPage();
            this.findpath_button = new System.Windows.Forms.Button();
            this.app_propertygrid = new System.Windows.Forms.PropertyGrid();
            this.selectop_button = new System.Windows.Forms.Button();
            this.deleteop_button = new System.Windows.Forms.Button();
            this.addop_button = new System.Windows.Forms.Button();
            this.app_statusstrip = new System.Windows.Forms.StatusStrip();
            this.progressbar_toolstrip = new System.Windows.Forms.ToolStripProgressBar();
            this.status_toolstrip_label = new System.Windows.Forms.ToolStripStatusLabel();
            this.info_textbox = new System.Windows.Forms.TextBox();
            this.nodeorigin_combobox = new System.Windows.Forms.ComboBox();
            this.nodetarget_combobox = new System.Windows.Forms.ComboBox();
            this.nodeorigin_label = new System.Windows.Forms.Label();
            this.nodetarget_label = new System.Windows.Forms.Label();
            this.reset_button = new System.Windows.Forms.Button();
            this.pointer_panel = new System.Windows.Forms.Panel();
            this.editor_trackbar = new System.Windows.Forms.TrackBar();
            this.app_toolstrip.SuspendLayout();
            this.nodestree_tabpage.SuspendLayout();
            this.app_tabcontrol.SuspendLayout();
            this.edgeslist_tabpage.SuspendLayout();
            this.app_statusstrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.editor_trackbar)).BeginInit();
            this.SuspendLayout();
            // 
            // info_toolstrip_button
            // 
            this.info_toolstrip_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.info_toolstrip_button.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.info_toolstrip_button.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.info_toolstrip_button.Image = ((System.Drawing.Image)(resources.GetObject("info_toolstrip_button.Image")));
            this.info_toolstrip_button.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.info_toolstrip_button.Name = "info_toolstrip_button";
            this.info_toolstrip_button.Size = new System.Drawing.Size(57, 22);
            this.info_toolstrip_button.Text = "Справка";
            // 
            // app_toolstrip
            // 
            this.app_toolstrip.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.app_toolstrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.app_toolstrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.app_toolstrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.project_toolstrip_button,
            this.settings_toolstrip_button,
            this.info_toolstrip_button});
            this.app_toolstrip.Location = new System.Drawing.Point(0, 0);
            this.app_toolstrip.Name = "app_toolstrip";
            this.app_toolstrip.Size = new System.Drawing.Size(864, 25);
            this.app_toolstrip.TabIndex = 23;
            this.app_toolstrip.Text = "Инструменты";
            // 
            // project_toolstrip_button
            // 
            this.project_toolstrip_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.project_toolstrip_button.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.project_toolstrip_button.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.open_toolstrip_menuitem,
            this.save_toolstrip_menuitem});
            this.project_toolstrip_button.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.project_toolstrip_button.Image = ((System.Drawing.Image)(resources.GetObject("project_toolstrip_button.Image")));
            this.project_toolstrip_button.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.project_toolstrip_button.Name = "project_toolstrip_button";
            this.project_toolstrip_button.Size = new System.Drawing.Size(60, 22);
            this.project_toolstrip_button.Text = "Проект";
            // 
            // open_toolstrip_menuitem
            // 
            this.open_toolstrip_menuitem.Name = "open_toolstrip_menuitem";
            this.open_toolstrip_menuitem.Size = new System.Drawing.Size(174, 22);
            this.open_toolstrip_menuitem.Text = "Открыть проект";
            // 
            // save_toolstrip_menuitem
            // 
            this.save_toolstrip_menuitem.Name = "save_toolstrip_menuitem";
            this.save_toolstrip_menuitem.Size = new System.Drawing.Size(174, 22);
            this.save_toolstrip_menuitem.Text = "Сохранить проект";
            // 
            // settings_toolstrip_button
            // 
            this.settings_toolstrip_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.settings_toolstrip_button.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.settings_toolstrip_button.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editorconf_toolstrip_menuitem,
            this.accountconf_toolstrip_menuitem});
            this.settings_toolstrip_button.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.settings_toolstrip_button.Image = ((System.Drawing.Image)(resources.GetObject("settings_toolstrip_button.Image")));
            this.settings_toolstrip_button.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.settings_toolstrip_button.Name = "settings_toolstrip_button";
            this.settings_toolstrip_button.Size = new System.Drawing.Size(80, 22);
            this.settings_toolstrip_button.Text = "Настройки";
            // 
            // editorconf_toolstrip_menuitem
            // 
            this.editorconf_toolstrip_menuitem.Name = "editorconf_toolstrip_menuitem";
            this.editorconf_toolstrip_menuitem.Size = new System.Drawing.Size(227, 22);
            this.editorconf_toolstrip_menuitem.Text = "Настройки редактора";
            // 
            // accountconf_toolstrip_menuitem
            // 
            this.accountconf_toolstrip_menuitem.Name = "accountconf_toolstrip_menuitem";
            this.accountconf_toolstrip_menuitem.Size = new System.Drawing.Size(227, 22);
            this.accountconf_toolstrip_menuitem.Text = "Параметры учетной записи";
            // 
            // edges_listview
            // 
            this.edges_listview.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.edges_listview.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.edgeid_column,
            this.edgeleftnode_column,
            this.edgerightnode_column});
            this.edges_listview.FullRowSelect = true;
            this.edges_listview.GridLines = true;
            this.edges_listview.Location = new System.Drawing.Point(6, 6);
            this.edges_listview.MultiSelect = false;
            this.edges_listview.Name = "edges_listview";
            this.edges_listview.Size = new System.Drawing.Size(233, 162);
            this.edges_listview.TabIndex = 14;
            this.edges_listview.UseCompatibleStateImageBehavior = false;
            this.edges_listview.View = System.Windows.Forms.View.Details;
            // 
            // edgeid_column
            // 
            this.edgeid_column.Text = "ID";
            this.edgeid_column.Width = 40;
            // 
            // edgeleftnode_column
            // 
            this.edgeleftnode_column.Text = "Источник";
            this.edgeleftnode_column.Width = 90;
            // 
            // edgerightnode_column
            // 
            this.edgerightnode_column.Text = "Цель";
            this.edgerightnode_column.Width = 90;
            // 
            // nodestree_tabpage
            // 
            this.nodestree_tabpage.Controls.Add(this.nodes_treeview);
            this.nodestree_tabpage.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.nodestree_tabpage.Location = new System.Drawing.Point(4, 24);
            this.nodestree_tabpage.Name = "nodestree_tabpage";
            this.nodestree_tabpage.Padding = new System.Windows.Forms.Padding(3);
            this.nodestree_tabpage.Size = new System.Drawing.Size(245, 174);
            this.nodestree_tabpage.TabIndex = 0;
            this.nodestree_tabpage.Text = "Список вершин";
            this.nodestree_tabpage.UseVisualStyleBackColor = true;
            // 
            // nodes_treeview
            // 
            this.nodes_treeview.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.nodes_treeview.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.nodes_treeview.FullRowSelect = true;
            this.nodes_treeview.Location = new System.Drawing.Point(6, 6);
            this.nodes_treeview.Name = "nodes_treeview";
            treeNode2.Name = "nodesroot_treeview";
            treeNode2.Text = "Список вершин";
            this.nodes_treeview.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode2});
            this.nodes_treeview.Size = new System.Drawing.Size(233, 162);
            this.nodes_treeview.TabIndex = 11;
            // 
            // app_tabcontrol
            // 
            this.app_tabcontrol.Controls.Add(this.nodestree_tabpage);
            this.app_tabcontrol.Controls.Add(this.edgeslist_tabpage);
            this.app_tabcontrol.Location = new System.Drawing.Point(12, 31);
            this.app_tabcontrol.Name = "app_tabcontrol";
            this.app_tabcontrol.SelectedIndex = 0;
            this.app_tabcontrol.Size = new System.Drawing.Size(253, 202);
            this.app_tabcontrol.TabIndex = 22;
            // 
            // edgeslist_tabpage
            // 
            this.edgeslist_tabpage.Controls.Add(this.edges_listview);
            this.edgeslist_tabpage.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.edgeslist_tabpage.Location = new System.Drawing.Point(4, 24);
            this.edgeslist_tabpage.Name = "edgeslist_tabpage";
            this.edgeslist_tabpage.Padding = new System.Windows.Forms.Padding(3);
            this.edgeslist_tabpage.Size = new System.Drawing.Size(245, 174);
            this.edgeslist_tabpage.TabIndex = 1;
            this.edgeslist_tabpage.Text = "Список дуг (ребер)";
            this.edgeslist_tabpage.UseVisualStyleBackColor = true;
            // 
            // findpath_button
            // 
            this.findpath_button.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.findpath_button.Location = new System.Drawing.Point(12, 283);
            this.findpath_button.Name = "findpath_button";
            this.findpath_button.Size = new System.Drawing.Size(253, 28);
            this.findpath_button.TabIndex = 21;
            this.findpath_button.Text = "Найти путь";
            this.findpath_button.UseVisualStyleBackColor = true;
            // 
            // app_propertygrid
            // 
            this.app_propertygrid.HelpBackColor = System.Drawing.Color.White;
            this.app_propertygrid.Location = new System.Drawing.Point(16, 342);
            this.app_propertygrid.Name = "app_propertygrid";
            this.app_propertygrid.Size = new System.Drawing.Size(253, 206);
            this.app_propertygrid.TabIndex = 20;
            // 
            // selectop_button
            // 
            this.selectop_button.BackgroundImage = global::CSCourseWork.Windows.Resourses.SelectIcon;
            this.selectop_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.selectop_button.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.selectop_button.Location = new System.Drawing.Point(356, 28);
            this.selectop_button.Name = "selectop_button";
            this.selectop_button.Size = new System.Drawing.Size(30, 30);
            this.selectop_button.TabIndex = 18;
            this.selectop_button.UseVisualStyleBackColor = true;
            // 
            // deleteop_button
            // 
            this.deleteop_button.BackgroundImage = global::CSCourseWork.Windows.Resourses.DeleteIcon;
            this.deleteop_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.deleteop_button.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.deleteop_button.Location = new System.Drawing.Point(320, 28);
            this.deleteop_button.Name = "deleteop_button";
            this.deleteop_button.Size = new System.Drawing.Size(30, 30);
            this.deleteop_button.TabIndex = 17;
            this.deleteop_button.UseVisualStyleBackColor = true;
            // 
            // addop_button
            // 
            this.addop_button.BackgroundImage = global::CSCourseWork.Windows.Resourses.AddIcon;
            this.addop_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.addop_button.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.addop_button.Location = new System.Drawing.Point(284, 28);
            this.addop_button.Name = "addop_button";
            this.addop_button.Size = new System.Drawing.Size(30, 30);
            this.addop_button.TabIndex = 16;
            this.addop_button.UseVisualStyleBackColor = true;
            // 
            // app_statusstrip
            // 
            this.app_statusstrip.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.app_statusstrip.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.app_statusstrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.app_statusstrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.progressbar_toolstrip,
            this.status_toolstrip_label});
            this.app_statusstrip.Location = new System.Drawing.Point(0, 555);
            this.app_statusstrip.Name = "app_statusstrip";
            this.app_statusstrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode;
            this.app_statusstrip.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.app_statusstrip.Size = new System.Drawing.Size(864, 24);
            this.app_statusstrip.SizingGrip = false;
            this.app_statusstrip.TabIndex = 25;
            this.app_statusstrip.Text = "Статус";
            // 
            // progressbar_toolstrip
            // 
            this.progressbar_toolstrip.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.progressbar_toolstrip.Name = "progressbar_toolstrip";
            this.progressbar_toolstrip.Size = new System.Drawing.Size(150, 18);
            this.progressbar_toolstrip.Step = 1;
            this.progressbar_toolstrip.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // status_toolstrip_label
            // 
            this.status_toolstrip_label.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.status_toolstrip_label.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.status_toolstrip_label.Name = "status_toolstrip_label";
            this.status_toolstrip_label.Size = new System.Drawing.Size(53, 19);
            this.status_toolstrip_label.Text = "Готово";
            // 
            // info_textbox
            // 
            this.info_textbox.BackColor = System.Drawing.Color.White;
            this.info_textbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.info_textbox.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.info_textbox.Location = new System.Drawing.Point(284, 493);
            this.info_textbox.Multiline = true;
            this.info_textbox.Name = "info_textbox";
            this.info_textbox.ReadOnly = true;
            this.info_textbox.Size = new System.Drawing.Size(568, 56);
            this.info_textbox.TabIndex = 26;
            // 
            // nodeorigin_combobox
            // 
            this.nodeorigin_combobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.nodeorigin_combobox.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.nodeorigin_combobox.FormattingEnabled = true;
            this.nodeorigin_combobox.Location = new System.Drawing.Point(12, 252);
            this.nodeorigin_combobox.Name = "nodeorigin_combobox";
            this.nodeorigin_combobox.Size = new System.Drawing.Size(121, 25);
            this.nodeorigin_combobox.TabIndex = 27;
            // 
            // nodetarget_combobox
            // 
            this.nodetarget_combobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.nodetarget_combobox.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.nodetarget_combobox.FormattingEnabled = true;
            this.nodetarget_combobox.Location = new System.Drawing.Point(144, 252);
            this.nodetarget_combobox.Name = "nodetarget_combobox";
            this.nodetarget_combobox.Size = new System.Drawing.Size(121, 25);
            this.nodetarget_combobox.TabIndex = 28;
            // 
            // nodeorigin_label
            // 
            this.nodeorigin_label.AutoSize = true;
            this.nodeorigin_label.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.nodeorigin_label.Location = new System.Drawing.Point(12, 236);
            this.nodeorigin_label.Name = "nodeorigin_label";
            this.nodeorigin_label.Size = new System.Drawing.Size(102, 13);
            this.nodeorigin_label.TabIndex = 29;
            this.nodeorigin_label.Text = "Начальная точка:";
            // 
            // nodetarget_label
            // 
            this.nodetarget_label.AutoSize = true;
            this.nodetarget_label.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.nodetarget_label.Location = new System.Drawing.Point(144, 236);
            this.nodetarget_label.Name = "nodetarget_label";
            this.nodetarget_label.Size = new System.Drawing.Size(88, 13);
            this.nodetarget_label.TabIndex = 30;
            this.nodetarget_label.Text = "Целевая точка:";
            // 
            // reset_button
            // 
            this.reset_button.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.reset_button.Location = new System.Drawing.Point(746, 28);
            this.reset_button.Name = "reset_button";
            this.reset_button.Size = new System.Drawing.Size(100, 30);
            this.reset_button.TabIndex = 31;
            this.reset_button.Text = "Очистить";
            this.reset_button.UseVisualStyleBackColor = true;
            // 
            // pointer_panel
            // 
            this.pointer_panel.Location = new System.Drawing.Point(284, 61);
            this.pointer_panel.Name = "pointer_panel";
            this.pointer_panel.Size = new System.Drawing.Size(568, 426);
            this.pointer_panel.TabIndex = 32;
            // 
            // editor_trackbar
            // 
            this.editor_trackbar.AutoSize = false;
            this.editor_trackbar.Cursor = System.Windows.Forms.Cursors.SizeWE;
            this.editor_trackbar.Location = new System.Drawing.Point(633, 34);
            this.editor_trackbar.Maximum = 100;
            this.editor_trackbar.Minimum = 1;
            this.editor_trackbar.Name = "editor_trackbar";
            this.editor_trackbar.Size = new System.Drawing.Size(107, 24);
            this.editor_trackbar.TabIndex = 33;
            this.editor_trackbar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.editor_trackbar.Value = 50;
            // 
            // ClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(864, 579);
            this.Controls.Add(this.editor_trackbar);
            this.Controls.Add(this.pointer_panel);
            this.Controls.Add(this.reset_button);
            this.Controls.Add(this.nodetarget_label);
            this.Controls.Add(this.nodeorigin_label);
            this.Controls.Add(this.nodetarget_combobox);
            this.Controls.Add(this.nodeorigin_combobox);
            this.Controls.Add(this.info_textbox);
            this.Controls.Add(this.app_statusstrip);
            this.Controls.Add(this.app_toolstrip);
            this.Controls.Add(this.app_tabcontrol);
            this.Controls.Add(this.findpath_button);
            this.Controls.Add(this.app_propertygrid);
            this.Controls.Add(this.selectop_button);
            this.Controls.Add(this.deleteop_button);
            this.Controls.Add(this.addop_button);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(880, 618);
            this.MinimumSize = new System.Drawing.Size(880, 618);
            this.Name = "ClientForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Редактор карты";
            this.app_toolstrip.ResumeLayout(false);
            this.app_toolstrip.PerformLayout();
            this.nodestree_tabpage.ResumeLayout(false);
            this.app_tabcontrol.ResumeLayout(false);
            this.edgeslist_tabpage.ResumeLayout(false);
            this.app_statusstrip.ResumeLayout(false);
            this.app_statusstrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.editor_trackbar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private ToolStripButton info_toolstrip_button;
        private ToolStrip app_toolstrip;
        private ListView edges_listview;
        private TabPage nodestree_tabpage;
        private TreeView nodes_treeview;
        private TabControl app_tabcontrol;
        private TabPage edgeslist_tabpage;
        private Button findpath_button;
        private PropertyGrid app_propertygrid;
        private Button selectop_button;
        private Button deleteop_button;
        private Button addop_button;
        private ToolStripDropDownButton project_toolstrip_button;
        private ToolStripMenuItem open_toolstrip_menuitem;
        private ToolStripMenuItem save_toolstrip_menuitem;
        private ToolStripDropDownButton settings_toolstrip_button;
        private ToolStripMenuItem editorconf_toolstrip_menuitem;
        private ToolStripMenuItem accountconf_toolstrip_menuitem;
        private StatusStrip app_statusstrip;
        private ToolStripStatusLabel status_toolstrip_label;
        private ToolStripProgressBar progressbar_toolstrip;
        private TextBox info_textbox;
        private ComboBox nodeorigin_combobox;
        private ComboBox nodetarget_combobox;
        private Label nodeorigin_label;
        private Label nodetarget_label;
        private ColumnHeader edgeid_column;
        private ColumnHeader edgeleftnode_column;
        private ColumnHeader edgerightnode_column;
        private Button reset_button;
        private Panel pointer_panel;
        private TrackBar editor_trackbar;
    }
}