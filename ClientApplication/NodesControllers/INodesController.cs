using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCourseWork.NodesControllers
{
    public sealed class NodesControllerException : System.Exception
    {
        public NodesControllers.NodeModel? Node { get; private set; } = default;
        public NodesControllerException(string message, NodeModel node) : base(message) => this.Node = node;
    }

    public interface INodesController : IEnumerable<NodesControllers.NodeModel>, IDisposable
    {
        public SortedSet<NodesControllers.NodeModel> NodesList { get; set; }
        public NodesControllers.NodeModel? this[System.Int32 node_id] { get; }
        public System.Int32 NodeSize { get; set; }

        public void AddNewNode(System.Int32 position_x, System.Int32 position_y);
        public void RemoveNode(System.Int32 node_id);
        public bool NodeCollisionCheck(Point position, System.Int32 node_id);
    }

    public interface INodesControllerWithConnectors : INodesController
    {
        public void SetNodeLinks(System.Int32 node_id, System.Int32 required_links_id);
        public void RemoveNodeLinks(System.Int32 node_id, System.Int32 required_links_id);
        public List<NodesControllers.NodesConnectorInfo> BuildNodeСonnectors();
    }
}
