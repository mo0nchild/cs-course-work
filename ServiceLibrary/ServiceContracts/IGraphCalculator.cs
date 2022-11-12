using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Xml.Serialization;

namespace ServiceLibrary.ServiceContracts
{
    [ServiceContractAttribute(Name = "GraphCalculator")]
    public interface IGraphCalculator
    {
        [OperationContractAttribute, FaultContractAttribute(typeof(GraphCalculatorException))]
        System.Int32[] FindPathByBFS(int origin_id, int target_id, List<NodeData> node_list);
    }

    [DataContractAttribute, SerializableAttribute]
    public sealed class GraphCalculatorException : System.Object
    {
        [DataMemberAttribute]
        public System.String Message { get; private set; } = default;
        public GraphCalculatorException(string message) : base() => this.Message = message;
    }

    [DataContractAttribute, SerializableAttribute]
    public sealed class NodeData : System.Object
    {
        [DataMemberAttribute, XmlArrayItemAttribute(typeof(int))]
        public System.Int32[] NodeLinksID { get; set; } = new System.Int32[0];

        [DataMemberAttribute, XmlArrayItemAttribute(typeof(int))]
        public System.Int32[] NodeInboxsID { get; set; } = new System.Int32[0];

        [DataMemberAttribute, XmlAttribute]
        public System.Int32 NodeID { get; set; } = default(System.Int32);
        
        [XmlIgnoreAttribute]
        public System.Int32 NodePathLevel { get; set; } = default(System.Int32);

        [DataMemberAttribute, XmlAttribute]
        public System.Double PositionX { get; set; } = default;

        [DataMemberAttribute, XmlAttribute]
        public System.Double PositionY { get; set; } = default;
    }
}
