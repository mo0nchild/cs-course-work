namespace CSCourseWork
{
    internal partial class ClientForm : Form
    {
        public EditorComponent EditorInstance { get; private set; }

        public ClientForm()
        {
            this.InitializeComponent();

            this.EditorInstance = new(this)
            {
                // BackColor = System.Drawing.Color.FromArgb(255, 56, 62, 80),
                BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle,
                NodeBorderWidth = 3,
                Location = new System.Drawing.Point(305, 38),
                Name = "editor_panel",
                Size = new System.Drawing.Size(506, 438),
                TabIndex = 0
            };

            this.EditorInstance.NodeClicked += new EditorComponent.EditorActionEventHandler(EditorComponentNodeSelected);
            this.EditorInstance.FieldClicked += new EditorComponent.EditorActionEventHandler(EditorComponentFieldClicked);

            this.addoperation_button.Click += (sender, args) => this.EditorInstance.Mode = EditorComponent.EditorModes.AddNode;
            this.deleteoperation_button.Click += (sender, args) => this.EditorInstance.Mode = EditorComponent.EditorModes.RemoveNode;
            this.connectoperation_button.Click += (sender, args) => this.EditorInstance.Mode = EditorComponent.EditorModes.SelectNode;
        }

        #region Test shit
        private void ListUpdate()
        {
            this.nodelinks_listview.Items.Clear();
            foreach (var item in this.EditorInstance.Controller) 
            {
                string nodelinks = "";
                foreach (var link in item.NodeLinksID) nodelinks += $"{link}, ";
                this.nodelinks_listview.Items.Add(new ListViewItem(new String[] { item.NodeID.ToString(), nodelinks }));
            }

            this.connectors_listbox.Items.Clear();
            foreach (var item in this.EditorInstance.Controller.BuildNode—onnectors())
            {
                this.connectors_listbox.Items.Add($"{item.LeftNode.NodeID} - {item.RightNode.NodeID}");

            }
        }
        #endregion

        private void EditorComponentFieldClicked(object? sender, EditorComponent.EditorActionEventArgs args)
        {
            // MessageBox.Show("Field Click", "");
            ListUpdate();
        }

        private void EditorComponentNodeSelected(object? sender, EditorComponent.EditorActionEventArgs args)
        {
            // MessageBox.Show("Node Clicked", "");
            ListUpdate();
        }
    }
}