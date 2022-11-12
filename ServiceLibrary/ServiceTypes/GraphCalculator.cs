using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

using ServiceLibrary.ServiceContracts;

namespace ServiceLibrary.ServiceTypes
{
    [ServiceBehaviorAttribute(InstanceContextMode = InstanceContextMode.PerSession)]
    public class GraphCalculator : ServiceTypes.ProjectDispatcher, ServiceContracts.IGraphCalculator
    {
        public int[] FindPathByBFS(int origin_id, int target_id, List<NodeData> node_list)
        {
            if (origin_id == target_id)
            {
                var exception_instance = new GraphCalculatorException("Совпадение начального и целевого узла");
                throw new FaultException<GraphCalculatorException>(exception_instance); 
            }

            var node_queue = new Queue<ServiceContracts.NodeData>(new NodeData[] { node_list[origin_id - 1] });
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
                    result[0] = origin_id; return result;
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
