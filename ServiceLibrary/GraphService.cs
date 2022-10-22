using ServiceLibrary.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ServiceLibrary
{
    [ServiceBehaviorAttribute(InstanceContextMode = InstanceContextMode.PerSession)]
    public class GraphService : System.Object, ServiceContracts.IGraphCalculator
    {
        private List<NodeData> GenerateNodeList() 
        {
            return new List<NodeData>()
            {
                new NodeData(){ NodeID = 1, NodeLinksID = new int[] { 2, 3, 4 } },
                new NodeData(){ NodeID = 2, NodeLinksID = new int[] { 1, 3, 6 } },
                new NodeData(){ NodeID = 3, NodeLinksID = new int[] { 1, 2, 4, 5 } },
                new NodeData(){ NodeID = 4, NodeLinksID = new int[] { 1, 3, 5, 6, 7 } },
                new NodeData(){ NodeID = 5, NodeLinksID = new int[] { 3, 4, 7 } },
                new NodeData(){ NodeID = 6, NodeLinksID = new int[] { 2, 4 } },
                new NodeData(){ NodeID = 7, NodeLinksID = new int[] { 4, 5 } },
            };
        }

        public int[] FindPathByBFS(int origin_id, int target_id, List<NodeData> node_list)
        {
            node_list = this.GenerateNodeList();

            var node_queue = new Queue<NodeData>(new NodeData[] { node_list[origin_id - 1] });
            var node_visited = new List<int>() { origin_id };

            while (node_queue.Count > 0) 
            {
                var current_node = node_queue.Dequeue();

                if (current_node.NodeID == target_id) 
                {
                    var result = new int[current_node.NodePathLevel];

                    shit:
                    foreach (var link in current_node.NodeLinksID) 
                    {
                        if (node_list[link - 1].NodePathLevel == current_node.NodePathLevel - 1)
                        {
                            result[current_node.NodePathLevel - 1] = current_node.NodeID;
                            current_node = node_list[link - 1];
                            goto shit;
                        }
                    }
                    result[0] = origin_id;

                    return result;
                }

                foreach (var link_id in current_node.NodeLinksID) 
                {
                    if (node_visited.Contains(link_id)) 
                    {
                        continue;
                    }

                    var link_node = node_list[link_id - 1];
                    link_node.NodePathLevel = current_node.NodePathLevel + 1;

                    node_queue.Enqueue(link_node);
                    node_visited.Add(link_id);
                }
            }

            return new int[] { 0 };
        }
    }
}
