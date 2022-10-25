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
        [OperationContractAttribute]
        int[] FindPathByBFS(int origin_id, int target_id, List<NodeData> node_list);
    }

    [DataContractAttribute]
    public sealed class NodeData : System.Object
    {
        [DataMemberAttribute]
        public int[] NodeLinksID { get; set; } = new int[0];

        [DataMemberAttribute]
        public int[] NodeInboxsID { get; set; } = new int[0];

        [DataMemberAttribute]
        public int NodeID { get; set; } = default(int);
        public int NodePathLevel { get; set; } = default(int);
    }
}
