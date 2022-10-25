using ServiceLibrary.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ServiceLibrary
{
    [ServiceBehaviorAttribute(InstanceContextMode = InstanceContextMode.PerSession, 
        IncludeExceptionDetailInFaults = true)]
    public class GraphService : System.Object, ServiceContracts.IGraphCalculator
    {
        private List<NodeData> TestList { get; set; } = new List<NodeData>()
        {
            new NodeData() { NodeID = 1, NodeLinksID = new int[] { 3 }, NodeInboxsID = new int[] { 2 } },
            new NodeData() { NodeID = 2, NodeLinksID = new int[] { 1, 3 }, NodeInboxsID = new int[] { 4 } },
            new NodeData() { NodeID = 3, NodeLinksID = new int[] { 4 }, NodeInboxsID = new int[] { 1, 2, 5 } },
            new NodeData() { NodeID = 4, NodeLinksID = new int[] { 2, 6 }, NodeInboxsID = new int[] { 3 } },
            new NodeData() { NodeID = 5, NodeLinksID = new int[] { 3 }, NodeInboxsID = new int[] { 6 } },
            new NodeData() { NodeID = 6, NodeLinksID = new int[] { 5 }, NodeInboxsID = new int[] { 4 } },
        };

        public int[] FindPathByBFS(int origin_id, int target_id, List<NodeData> node_list)
        {
            if (origin_id == target_id) throw new Exception("Совпадение начального и целевого узла ");

            var node_queue = new Queue<NodeData>(new NodeData[] { node_list[origin_id - 1] });
            var node_visited = new List<int>() { origin_id };

            while (node_queue.Count > 0) 
            {
                var current_node = node_queue.Dequeue();
                if (current_node.NodeID == target_id) 
                {
                    var result = new int[current_node.NodePathLevel + 1];
                    for (var index = 0; index < current_node.NodeInboxsID.Length; index++) 
                    {
                        var inbox = current_node.NodeInboxsID[index];
                        if (node_list[inbox - 1].NodePathLevel == current_node.NodePathLevel - 1) 
                        {
                            result[current_node.NodePathLevel] = current_node.NodeID;
                            current_node = node_list[inbox - 1];

                            index = -1; continue;
                        }
                    }   
                    result[0] = origin_id;
                    return result;
                }

                foreach (var link_id in current_node.NodeLinksID) 
                {
                    if (node_visited.Contains(link_id)) continue;
                    node_list[link_id - 1].NodePathLevel = current_node.NodePathLevel + 1;

                    node_queue.Enqueue(node_list[link_id - 1]);
                    node_visited.Add(link_id);
                }
            }

            return new int[] { };
        }
    }
}
