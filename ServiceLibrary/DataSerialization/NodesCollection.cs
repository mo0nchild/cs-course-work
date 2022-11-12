using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

using ServiceLibrary.ServiceContracts;

namespace ServiceLibrary.DataSerialization
{
    [type: System.SerializableAttribute, XmlRootAttribute(Namespace = "http://www.nodesfield.com")]
    public sealed class NodesCollection : System.Object/*, ISerializable*/
    {
        [XmlArrayAttribute(ElementName = "NodesList"), XmlArrayItemAttribute(typeof(NodeData))]
        public List<ServiceContracts.NodeData> NodesData { get; set; }

        public NodesCollection(List<NodeData> datalist) : base() => this.NodesData = datalist;
        public NodesCollection() : this(new List<ServiceContracts.NodeData>()) { }
    }
}
