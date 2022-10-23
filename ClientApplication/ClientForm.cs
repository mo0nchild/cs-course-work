using CSCourseWork.EditorComponent;
using CSCourseWork.NodeController;

namespace CSCourseWork
{
    internal partial class ClientForm : Form
    {
        public EditorComponentBase EditorInstance { get; private set; }

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

            this.EditorInstance.NodeClicked += new EditorActionEventHandler(EditorComponentNodeSelected);
            this.EditorInstance.FieldClicked += new EditorActionEventHandler(EditorComponentFieldClicked);

            this.addoperation_button.Click += (sender, args) => this.EditorInstance.Mode = EditorModes.AddNode;
            this.deleteoperation_button.Click += (sender, args) => this.EditorInstance.Mode = EditorModes.RemoveNode;
            this.connectoperation_button.Click += (sender, args) => this.EditorInstance.Mode = EditorModes.SelectNode;


            this.test.Click += Test_Click;
        }

        private void Test_Click(object? sender, EventArgs e)
        {
            //using (var client = new GraphServiceReference.GraphCalculatorClient()) 
            //{
            //    var answer = client.FindPathByBFSAsync(new GraphServiceReference.NodeData[0]);
            //    Console.WriteLine(answer.Result.Length);
            //}

            var connectors = this.EditorInstance.Controller.BuildNode—onnectors();
            if (connectors.Count > 0) this.EditorInstance.BuildGraphPath(new List<NodeConnectorInfo>() { connectors[0] });
        }

        #region Test shit
        //private void ListUpdate()
        //{
        //    this.nodelinks_listview.Items.Clear();
        //    foreach (var item in this.EditorInstance.Controller) 
        //    {
        //        string nodelinks = "";
        //        foreach (var link in item.NodeLinksID) nodelinks += $"{link}, ";
        //        this.nodelinks_listview.Items.Add(new ListViewItem(new String[] { item.NodeID.ToString(), nodelinks }));
        //    }

        //    this.connectors_listbox.Items.Clear();
        //    foreach (var item in this.EditorInstance.Controller.BuildNode—onnectors())
        //    {
        //        this.connectors_listbox.Items.Add($"{item.LeftNode.NodeID} - {item.RightNode.NodeID}");

        //    }
        //}
        #endregion

        private void EditorComponentFieldClicked(object? sender, EditorActionEventArgs args)
        {
            // MessageBox.Show("Field Click", "");
            //ListUpdate();
        }

        private void EditorComponentNodeSelected(object? sender, EditorActionEventArgs args)
        {
            // MessageBox.Show("Node Clicked", "");
            //ListUpdate();
        }
    }
}