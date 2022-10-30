using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace CSCourseWork.NodesControllers
{
    public record NodesConnectorInfo(int EdgeId, NodeModel LeftNode, NodeModel RightNode) : System.Object;

    public class NodesComparer : IComparer<NodeModel>
    {
        int IComparer<NodeModel>.Compare(NodeModel? x, NodeModel? y)
        {
            if (x == null || y == null) throw new NullReferenceException("Argument X | Y is null");
            return x.NodeID.CompareTo(y.NodeID);
        }
    }

    [System.SerializableAttribute]
    public sealed class NodeModel : object, IComparable<NodeModel>, ICloneable
    {
        [CategoryAttribute("Настройки вершины"), ReadOnlyAttribute(true)]
        [DescriptionAttribute("Значение идентификатора выбранного узла")]
        public System.Int32 NodeID { get; set; } = default;

        [CategoryAttribute("Настройки вершины"), TypeConverterAttribute(typeof(List<int>))]
        [DescriptionAttribute("Коллекция значений привязанных вершин")]
        public List<System.Int32> NodeLinksID { get; set; } = new();

        [CategoryAttribute("Настройки положения"), DescriptionAttribute("Текущее местоположение вершины")]
        public System.Drawing.Point Position { get; set; } = new(0, 0);

        public NodeModel(int node_id) : base() => this.NodeID = node_id;

        int IComparable<NodeModel>.CompareTo(NodeModel? node_other)
        {
            if (node_other == null) throw new NodesControllerException("Comparable node is null", this);
            return this.NodeID.CompareTo(node_other.NodeID);
        }

        public object Clone() => this.MemberwiseClone();
    }
}
