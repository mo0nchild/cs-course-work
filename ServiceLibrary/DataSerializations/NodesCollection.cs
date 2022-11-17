using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel.Configuration;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

using ServiceLibrary.ServiceContracts;
using TransferDataPackage.DataSerializations;

namespace ServiceLibrary.DataSerializations
{
    [type: System.SerializableAttribute, DataContractAttribute]
    public class NodesCollection : System.Object, IEnumerable<ServiceContracts.NodeData>
    {
        [DataMemberAttribute] public List<ServiceContracts.NodeData> NodesData { get; set; }
        public NodesCollection(List<NodeData> datalist) : base() => this.NodesData = datalist;
        public NodesCollection() : this(new List<ServiceContracts.NodeData>()) { }

        public IEnumerator<ServiceContracts.NodeData> GetEnumerator() => this.NodesData.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }

    [SerializableAttribute]
    public sealed class DataSerializationAdapter : DataSerializationBase
    {
        [NonSerializedAttribute] private DataSerializations.NodesCollection nodes_list = default;
        public DataSerializations.NodesCollection NodesList { get => this.nodes_list; }

        public DataSerializationAdapter() : this(new NodesCollection()) { }

        public DataSerializationAdapter(NodesCollection nodes_list) : base()
        { this.nodes_list = nodes_list; }

        protected override void SetContextData(TransferDataInstaller<VertexInfo> vertexs,
            TransferDataInstaller<EdgeInfo> edges)
        {
            this.nodes_list.NodesData.ForEach(delegate (NodeData node_info) 
            {
                vertexs.InstallData(new VertexInfo { ID = node_info.NodeID,
                    PositionX = (int)node_info.PositionX, PositionY = (int)node_info.PositionY,
                });
                foreach (var links in node_info.NodeLinksID)
                { edges.InstallData(new EdgeInfo { LeftNodeID = node_info.NodeID, RightNodeID = links }); }
            });
        }

        protected override void GetContextData(TransferDataExtractor<VertexInfo> vertexs,
            TransferDataExtractor<EdgeInfo> edges)
        {
            this.nodes_list = new DataSerializations.NodesCollection();

            Console.WriteLine("GET CONTEXT DATA");

            foreach (VertexInfo vertex in vertexs.ExtractData())
            {
                Console.WriteLine($"{vertex.ID}, {vertex.PositionX}, {vertex.PositionY}");

                var node_data = new NodeData() { 
                    NodeID = vertex.ID, PositionX = vertex.PositionX, PositionY = vertex.PositionY,
                };
                IList<int> node_lisks = new List<int>(), node_inboxs = new List<int>();
                foreach (EdgeInfo edge in edges.ExtractData())
                {
                    if (edge.LeftNodeID == node_data.NodeID) node_lisks.Add(edge.RightNodeID);
                    if (edge.RightNodeID == node_data.NodeID) node_inboxs.Add(edge.LeftNodeID);
                }
                node_data.NodeLinksID = node_lisks.ToArray(); node_data.NodeInboxsID = node_inboxs.ToArray();

                this.nodes_list.NodesData.Add(node_data);
            }
        }
    }
}
