using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static CSCourseWork.NodesController;

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
            public List<int> NodeLinksID { get; set; } = new();
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

        public NodeInfo? this[int node_id] 
        {
            get 
            {
                if(node_id <= 0 || node_id > this.NodesList.Count) return null;
                return this.NodesList.ElementAt<NodeInfo>(node_id - 1);
            }
        }

        // ПЕРЕДЕЛАТЬ ЧТОБ ЧЕТКО И КРАСИВО БЫЛО ЕМАё
        public bool NodeCollisionCheck(Point position, int node_id)
        {
            NodeInfo? select_node = default(NodeInfo);
            try 
            {
                select_node = this.NodesList.Where((NodeInfo node_info) => node_info.NodeID == node_id)
                    .ToList<NodeInfo>()[0];
            }
            catch (Exception error) { MessageBox.Show(error.Message, "Ошибка"); return false; }
            var node_position = select_node.Position;

            double delta_x = node_position.X - position.X, delta_y = node_position.Y - position.Y;
            bool check = Math.Sqrt(Math.Pow(delta_x, 2) + Math.Pow(delta_y, 2)) < this.NodeSize;
            
            if (check == true) return true;
            return false;
        }

        public void AddNewNode(int pos_x, int pos_y) 
        {
            this.NodesList.ToList<NodeInfo>().ForEach(delegate (NodeInfo node_info) 
            {
                if (this.NodeCollisionCheck(new Point(pos_x, pos_y), node_info.NodeID))
                { throw new NodeControllerException("Произошло наложение вершин", node_info); }
            });
            this.NodesList.Add(new NodeInfo(this.NodesList.Count + 1) { Position = new Point(pos_x, pos_y) });
        }

        public void RemoveNode(int node_id) 
        {
            foreach (var node_info in this.NodesList
                .Where((NodeInfo node_info) => node_info.NodeLinksID.Contains(node_id)))
            { 
                this.RemoveNodeLinks(node_info.NodeID, node_id); 
            }
            this.NodesList.RemoveWhere((NodeInfo node_info) => node_info.NodeID == node_id);

            try { for (int id = node_id; id <= this.NodesList.Count; id++) this[id]!.NodeID--; }
            catch (Exception error) { MessageBox.Show(error.Message, "Ошибка"); }
        }


        public void SetNodeLinks(int node_id, int required_links_id)
        {
            NodeInfo? selectednode_info = this[node_id], requirednode_info = this[required_links_id];
            if (selectednode_info == null || requirednode_info == null || node_id == required_links_id) return;

            if (LinkCheck(selectednode_info, required_links_id) && LinkCheck(requirednode_info, node_id)) 
            {
                selectednode_info?.NodeLinksID.Add(required_links_id);
                requirednode_info?.NodeLinksID.Add(node_id);
            }

            bool LinkCheck(NodeInfo node_info, int required_id)
            { return node_info.NodeLinksID.Where<int>((int id) => id == required_id).ToList().Count == 0; }
        }
        
        public void RemoveNodeLinks(int node_id, int required_links_id)
        {
            this[node_id]?.NodeLinksID.RemoveAll((int id) => id == required_links_id);
            this[required_links_id]?.NodeLinksID.RemoveAll((int id) => id == node_id);
        }
        // переделать
        public List<NodeConnectorInfo> BuildNodeСonnectors() 
        {
            var result_list = new List<NodeConnectorInfo>();

            for (int node_id = 1; node_id <= this.NodesList.Count; node_id++) 
            {
                var node_links = this.NodesList.Where<NodeInfo>((NodeInfo node_info) => node_info.NodeID != node_id);
                node_links.ToList().ForEach(delegate (NodeInfo link)
                {
                    if (link.NodeLinksID.Contains(node_id)) result_list.Add(new NodeConnectorInfo(this[node_id]!, link));
                });
            }
            return result_list;
        }

        public IEnumerator<NodeInfo> GetEnumerator()
        { foreach (NodesController.NodeInfo item in this.NodesList) yield return item; }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}
