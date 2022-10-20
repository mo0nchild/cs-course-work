using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ServiceLibrary
{
    [ServiceContractAttribute(Name = "NodeProcess")]
    public interface INodeCalculator
    {
        [OperationContract]
        int Process(NodeData composite);
    }

    [DataContract]
    public class NodeData
    {
        [DataMember]
        public int[] NodeLinksID { get; set; }

        [DataMember]
        public int NodeID { get; set; }
    }
}
