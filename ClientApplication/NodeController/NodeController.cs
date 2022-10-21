using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CSCourseWork.NodeController
{
    internal sealed class NodeControllerException : System.Exception
    {
        public NodeModel Node { get; private set; }
        public NodeControllerException(string message, NodeModel node) : base(message) => Node = node;
    }

    internal interface INodesController : IEnumerable<NodeModel>, IDisposable
    {
        public SortedSet<NodeModel> NodesList { get; set; }
        public NodeModel? this[int node_id] { get; }
        void AddNewNode(int position_x, int position_y);
        void RemoveNode(int node_id);
        
    }

    internal interface INodeConnectorsController : INodesController 
    {
        void SetNodeLinks(int node_id, int required_links_id);
        void RemoveNodeLinks(int node_id, int required_links_id);
        List<NodeConnectorInfo> BuildNodeСonnectors();
    }

    internal class NodesController : System.Object, INodeConnectorsController
    {
        public SortedSet<NodeModel> NodesList { get; set; }
        public int NodeSize { get; set; } = default;

        public NodesController() : base() => NodesList = new SortedSet<NodeModel>(new NodeComparer());

        public NodeModel? this[int node_id]
        {
            get
            {
                if (node_id <= 0 || node_id > NodesList.Count) return null;
                return NodesList.ElementAt(node_id - 1);
            }
        }

        public bool NodeCollisionCheck(Point position, int node_id)
        {
            NodeModel? select_node = default;
            try
            {
                select_node = NodesList.Where((node_info) => node_info.NodeID == node_id)
                    .ToList()[0];
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
            NodesList.ToList().ForEach(delegate (NodeModel node_info)
            {
                if (NodeCollisionCheck(new Point(pos_x, pos_y), node_info.NodeID))
                { throw new NodeControllerException("Произошло наложение вершин", node_info); }
            });
            NodesList.Add(new NodeModel(NodesList.Count + 1) { Position = new Point(pos_x, pos_y) });
        }

        public void RemoveNode(int node_id)
        {
            foreach (var node_info in NodesList
                .Where((node_info) => node_info.NodeLinksID.Contains(node_id)))
            {
                RemoveNodeLinks(node_info.NodeID, node_id);
            }
            NodesList.RemoveWhere((node_info) => node_info.NodeID == node_id);

            try { for (int id = node_id; id <= NodesList.Count; id++) this[id]!.NodeID--; }
            catch (Exception error) { MessageBox.Show(error.Message, "Ошибка"); }
        }

        public void SetNodeLinks(int node_id, int required_links_id)
        {
            NodeModel? selectednode_info = this[node_id], requirednode_info = this[required_links_id];
            if (selectednode_info == null || requirednode_info == null || node_id == required_links_id) return;

            if (LinkCheck(selectednode_info, required_links_id) && LinkCheck(requirednode_info, node_id))
            {
                selectednode_info?.NodeLinksID.Add(required_links_id);
                requirednode_info?.NodeLinksID.Add(node_id);
            }

            bool LinkCheck(NodeModel node_info, int required_id)
            { return node_info.NodeLinksID.Where((id) => id == required_id).ToList().Count == 0; }
        }

        public void RemoveNodeLinks(int node_id, int required_links_id)
        {
            this[node_id]?.NodeLinksID.RemoveAll((id) => id == required_links_id);
            this[required_links_id]?.NodeLinksID.RemoveAll((id) => id == node_id);
        }
        // переделать
        public List<NodeConnectorInfo> BuildNodeСonnectors()
        {
            var result_list = new List<NodeConnectorInfo>();

            for (int node_id = 1; node_id <= NodesList.Count; node_id++)
            {
                var node_links = NodesList.Where((node_info) => node_info.NodeID != node_id);
                var edge_id = default(int) + 1;

                node_links.ToList().ForEach(delegate (NodeModel link)
                {
                    foreach (var item in result_list)
                    {
                        if (item.LeftNode == this[node_id]! && item.RightNode == link || item.LeftNode == link
                            && item.RightNode == this[node_id]!) return;
                    }

                    if (link.NodeLinksID.Contains(node_id)) result_list.Add(
                        new NodeConnectorInfo(edge_id++, this[node_id]!, link));
                });
            }
            return result_list;
        }

        public IEnumerator<NodeModel> GetEnumerator()
        { foreach (NodeModel item in NodesList) yield return item; }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        void IDisposable.Dispose() => NodesList.Clear();
    }
}
