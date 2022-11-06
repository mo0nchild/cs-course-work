using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.LinkLabel;

namespace CSCourseWork.NodesControllers
{
    public class NodesController : System.Object, INodesControllerWithConnectors
    {
        public SortedSet<NodeModel> NodesList { get; set; }
        public int NodeSize { get; set; } = default;

        public NodesController() : base() => this.NodesList = new SortedSet<NodeModel>(new NodesComparer());

        public NodeModel? this[int node_id]
        {
            get {
                if (node_id <= 0 || node_id > this.NodesList.Count) return null;
                return this.NodesList.ElementAt(node_id - 1);
            }
        }

        public bool NodeCollisionCheck(Point position, int node_id)
        {
            NodeModel? select_node = default;
            try {
                select_node = this.NodesList.Where(delegate(NodeModel node_info) 
                    { return node_info.NodeID == node_id; }).ToList()[0];
            }
            catch (Exception error) { MessageBox.Show(error.Message, "Ошибка"); return false; }
            var node_position = select_node.Position;

            double delta_x = node_position.X - position.X, delta_y = node_position.Y - position.Y;
            bool check = Math.Sqrt(Math.Pow(delta_x, 2) + Math.Pow(delta_y, 2)) < NodeSize;

            if (check == true) return true;
            return false;
        }

        public void AddNewNode(int pos_x, int pos_y)
        {
            this.NodesList.ToList().ForEach(delegate (NodesControllers.NodeModel node_info)
            {
                if (this.NodeCollisionCheck(new Point(pos_x, pos_y), node_info.NodeID))
                { throw new NodesControllerException("Произошло наложение вершин", node_info); }
            });

            var node_builded = new NodeModel(this.NodesList.Count + 1) { Position = new Point(pos_x, pos_y) };
            this.NodesList.Add(node_builded);
        }

        public void RemoveNode(int node_id)
        {
            //foreach (var node_info in this.NodesList
            //    .Where((NodeModel node_info) => node_info.NodeLinksID.Contains(node_id)))
            //{
            //    this.RemoveNodeLinks(node_info.NodeID, node_id);
            //    for (int id = 0; id < node_info.NodeLinksID.Count; id++)
            //    {
            //        if (node_info.NodeLinksID[id] >= node_id) node_info.NodeLinksID[id]--;
            //    }
            //}
            foreach (NodesControllers.NodeModel node_info in this.NodesList)
            {
                if (node_info.NodeLinksID.Contains(node_id)) this.RemoveNodeLinks(node_info.NodeID, node_id);
                for (int id = 0; id < node_info.NodeLinksID.Count; id++)
                {
                    if (node_info.NodeLinksID[id] >= node_id) node_info.NodeLinksID[id]--;
                }
            }
            this.NodesList.RemoveWhere((node_info) => node_info.NodeID == node_id);

            try { for (int id = node_id; id <= this.NodesList.Count; id++) this[id]!.NodeID--; }
            catch (Exception error) { MessageBox.Show(error.Message, "Ошибка"); }
        }

        public void SetNodeLinks(int node_id, int required_link_id)
        {
            NodesControllers.NodeModel? selectednode_info = this[node_id];

            if (selectednode_info != null && node_id != required_link_id) 
            {
                if (LinkCheck(selectednode_info, required_link_id)) selectednode_info?.NodeLinksID.Add(required_link_id);
            }

            bool LinkCheck(NodesControllers.NodeModel node_info, int required_id)
            { return node_info.NodeLinksID.Where((id) => id == required_id).ToList().Count == 0; }
        }

        public void RemoveNodeLinks(int node_id, int required_links_id)
        {
            try {
                this[node_id]?.NodeLinksID.RemoveAll((id) => id == required_links_id);
                this[required_links_id]?.NodeLinksID.RemoveAll((id) => id == node_id);
            }
            catch (System.Exception error) { MessageBox.Show(error.Message, "Ошибка"); }
        }

        public List<NodesConnectorInfo> BuildNodeСonnectors()
        {
            var result_list = new List<NodesConnectorInfo>();
            var edge_id = default(int);

            for (int node_id = 1; node_id <= this.NodesList.Count; node_id++)
            {
                var node_links = this.NodesList.Where((node_info) => node_info.NodeID != node_id);
                node_links.ToList().ForEach(delegate (NodeModel link)
                {
                    foreach (var item in result_list)
                    {
                        if (this[node_id]!.NodeLinksID == item.RightNode.NodeLinksID && 
                            item.LeftNode.NodeLinksID == link.NodeLinksID) return;
                    }

                    if (link.NodeLinksID.Contains(node_id)) 
                        result_list.Add(new NodesConnectorInfo(edge_id++, link, this[node_id]!));
                });
            }
            return result_list;
        }

        public IEnumerator<NodeModel> GetEnumerator()
        { foreach (NodeModel item in this.NodesList) yield return item; }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        void IDisposable.Dispose() => this.NodesList.Clear();
    }
}
