using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ServiceLibrary.ServiceContracts
{
    [ServiceContractAttribute(Name = "GraphCalculator")]
    public interface IGraphCalculator
    {
        [FaultContract(typeof(System.Exception))]
        [OperationContractAttribute]
        System.Int32[] FindPathByBFS(int origin_id, int target_id, List<NodeData> node_list);
    }

    [DataContractAttribute]
    public sealed class NodeData : System.Object
    {
        [DataMemberAttribute]
        public System.Int32[] NodeLinksID { get; set; } = new System.Int32[0];

        [DataMemberAttribute]
        public System.Int32[] NodeInboxsID { get; set; } = new System.Int32[0];

        [DataMemberAttribute]
        public System.Int32 NodeID { get; set; } = default(System.Int32);
        public System.Int32 NodePathLevel { get; set; } = default(System.Int32);
    }
}
