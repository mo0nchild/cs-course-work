using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using CSCourseWork.NodesControllers;
using GraphServiceReference;

namespace CSCourseWork.Connected_Services.GraphServiceReference
{
    public static class GraphServiceDataAdapter : System.Object
    {
        public static NodeData[] ConvertToServiceData(this IEnumerable<NodeModel> nodemodel_list) 
        {
            var nodedata_result = new NodeData[nodemodel_list.Count()];
            for (var index = 0; index < nodemodel_list.Count(); ++index)
            {
                var current_nodemodel = nodemodel_list.ElementAt<NodeModel>(index);
                nodedata_result[index] = new NodeData()
                {
                    NodeLinksID = current_nodemodel.NodeLinksID.ToArray(),
                    NodeID = current_nodemodel.NodeID
                };

                var inbox_nodes = new List<int>();
                foreach (var node_info in nodemodel_list) 
                {
                    if(node_info.NodeLinksID.Contains(current_nodemodel.NodeID)) inbox_nodes.Add(node_info.NodeID);
                }
                nodedata_result[index].NodeInboxsID = inbox_nodes.ToArray();
            }
            return nodedata_result;
        }

        public static List<NodesConnectorInfo> ConvertToPath(this INodesControllerWithConnectors controller, int[] path_list)
        {
            var result = new List<NodesConnectorInfo>();

            for (var index = 0; index < path_list.Length - 1; ++index) 
            {
                result.Add(new NodesConnectorInfo(index + 1, controller[path_list[index]]!, controller[path_list[index + 1]]!));
            }
            result.Add(new NodesConnectorInfo(path_list.Length, 
                controller[path_list[path_list.Length - 2]]!, 
                controller[path_list[path_list.Length - 1]]!));

            return result;
        }
    }
}
