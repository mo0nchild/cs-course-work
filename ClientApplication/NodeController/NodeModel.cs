namespace CSCourseWork.NodeController
{
    internal record NodeConnectorInfo(NodeModel LeftNode, NodeModel RightNode) : object;

    public class NodeComparer : IComparer<NodeModel>
    {
        int IComparer<NodeModel>.Compare(NodeModel? x, NodeModel? y)
        {
            if (x == null || y == null) throw new NullReferenceException("Argument X | Y is null");
            return x.NodeID.CompareTo(y.NodeID);
        }
    }

    internal sealed class NodeModel : object, IComparable<NodeModel>, ICloneable
    {
        public Point Position { get; set; } = new(0, 0);
        public List<int> NodeLinksID { get; set; } = new();
        public int NodeID { get; set; } = default;

        public NodeModel(int node_id) : base() => NodeID = node_id;

        int IComparable<NodeModel>.CompareTo(NodeModel? node_other)
        {
            if (node_other == null) throw new NodeControllerException("Comparable node is null", this);
            return NodeID.CompareTo(node_other.NodeID);
        }

        public object Clone() => MemberwiseClone();
    }
}
