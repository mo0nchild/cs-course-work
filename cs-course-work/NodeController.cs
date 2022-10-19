using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static CSCourseWork.EditorComponent;
using static CSCourseWork.NodesController;
using static System.Math;

namespace CSCourseWork
{
    internal sealed class NodeControllerException : System.Exception
    {
        public NodesController.NodeInfo Node { get; private set; }
        public NodeControllerException(string message, NodesController.NodeInfo node)
            : base(message) => this.Node = node;
    }

    internal class NodesController: System.Object, IEnumerable<NodesController.NodeInfo>
    {
        public class NodeComparer : IComparer<NodesController.NodeInfo>
        {
            int IComparer<NodeInfo>.Compare(NodeInfo? x, NodeInfo? y)
            {
                if (x == null || y == null) throw new NullReferenceException("Argument X | Y is null");
                return x.NodeID.CompareTo(y.NodeID);
            }
        }

        public record NodeConnectorInfo(NodeInfo LeftNode, NodeInfo RightNode) : System.Object;

        public sealed class NodeInfo : System.Object, IComparable<NodeInfo>, ICloneable
        {
            public Point Position { get; set; } = new(0, 0);
            public List<NodeInfo> NodeLinks { get; set; } = new();
            public int NodeID { get; set; } = default;

            public NodeInfo(int node_id) : base() => this.NodeID = node_id;

            int IComparable<NodeInfo>.CompareTo(NodeInfo? node_other)
            {
                if (node_other == null) throw new NodeControllerException("Comparable node is null", this);
                return this.NodeID.CompareTo(node_other.NodeID);
            }

            public System.Object Clone() => this.MemberwiseClone();
        }

        public SortedSet<NodeInfo> NodesList { get; set; }
        public int NodeSize { get; set; } = default(int);

        public NodesController(): base() => this.NodesList = new SortedSet<NodeInfo>(new NodeComparer());


        // переделать чтоб  нормально выглядело 
        public NodeInfo? this[int ID] 
        {
            get 
            {
                foreach (var node_info in this.NodesList) 
                {
                    if (node_info.NodeID == ID) return node_info;
                }
                return null;
            }
        }

        public bool NodeCollisionCheck(Point position, int node_id)
        {
            var select_node = this.NodesList.Where((NodeInfo node_info) => node_info.NodeID == node_id)
                .Select((NodeInfo node_info) => node_info).ToList<NodeInfo>()[0];
            var node_position = select_node.Position;

            double delta_x = node_position.X - position.X, delta_y = node_position.Y - position.Y;
            if (Sqrt(Pow(delta_x, 2) + Pow(delta_y, 2)) < this.NodeSize * 2) return true;
            
            return false;
        }

        public void AddNewNode(int pos_x, int pos_y) 
        {
            this.NodesList.ToList().ForEach(delegate (NodeInfo node_info) 
            {
                if (this.NodeCollisionCheck(new Point(pos_x, pos_y), node_info.NodeID))
                { throw new NodeControllerException("Произошло наложение вершин", node_info); }
            });
            this.NodesList.Add(new NodeInfo(this.NodesList.Count + 1) { Position = new Point(pos_x, pos_y) });
        }

        public void RemoveNode(int node_id) 
        {
            this.NodesList.RemoveWhere(delegate(NodeInfo x) { return x.NodeID == node_id; });

            foreach (var item in this.NodesList) 
            {
                if(item.NodeID > node_id ) --item.NodeID;
            }
        }

        // переделать
        public void SetNodeLinks(int node_id, int required_links_id)
        {
            var selectednode_info = this.NodesList.ElementAt(node_id - 1);
            var requirednode_info = this.NodesList.ElementAt(required_links_id - 1);

            selectednode_info.NodeLinks.Add(requirednode_info);
            requirednode_info.NodeLinks.Add(selectednode_info);
        }
        // переделать
        public void RemoveNodeLinks(int node_id, int required_links_id)
        {
            NodesController.NodeInfo selected_node = this.NodesList.ElementAt(node_id);
            selected_node.NodeLinks.ForEach(delegate (NodeInfo selected_node_link) 
            {
                selected_node_link.NodeLinks.RemoveAll(delegate (NodeInfo link) { return link.NodeID == node_id; });
            });
            selected_node.NodeLinks.RemoveAll(delegate (NodeInfo link) { return link.NodeID == required_links_id; });
        }
        // переделать
        public List<NodeConnectorInfo> BuildNodeСonnectors() 
        {
            var result_list = new List<NodeConnectorInfo>();

            foreach (var node_info in this.NodesList) 
            {
                foreach (var linknode_info in node_info.NodeLinks) 
                {
                    if (result_list.Contains(new NodeConnectorInfo(node_info, linknode_info))
                        || result_list.Contains(new NodeConnectorInfo(linknode_info, node_info))) continue;
                    result_list.Add(new NodeConnectorInfo(node_info, linknode_info));
                }
            }

            return result_list;
        }

        public IEnumerator<NodeInfo> GetEnumerator()
        {
            foreach (NodesController.NodeInfo item in this.NodesList) yield return item;
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}
