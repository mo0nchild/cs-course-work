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
    public sealed class GraphService : ServiceTypes.GraphCalculator
    {
        public GraphService() : base() { }
    }
}
