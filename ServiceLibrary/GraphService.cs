using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ServiceLibrary
{
    [ServiceBehaviorAttribute(InstanceContextMode = InstanceContextMode.PerSession)]
    public class GraphService : IGraphCalculator
    {

        public int Process(List<NodeData> composite)
        {
            return 0;
        }
    }
}
