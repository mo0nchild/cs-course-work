using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCourseWork.NodesControllers
{
    public sealed class NodesControllerException : System.Exception
    {
        public NodeModel? Node { get; private set; } = default;
        public NodesControllerException(string message, NodeModel node) : base(message) => this.Node = node;
    }

    public interface INodesController : IEnumerable<NodeModel>, IDisposable
    {
        public SortedSet<NodeModel> NodesList { get; set; }
        public NodeModel? this[int node_id] { get; }
        public System.Int32 NodeSize { get; set; }
        public void AddNewNode(int position_x, int position_y);
        public void RemoveNode(int node_id);
        public bool NodeCollisionCheck(Point position, int node_id);
    }

    public interface INodesControllerWithConnectors : INodesController
    {
        public void SetNodeLinks(int node_id, int required_links_id);
        public void RemoveNodeLinks(int node_id, int required_links_id);
        public List<NodesConnectorInfo> BuildNodeСonnectors();
    }
}
