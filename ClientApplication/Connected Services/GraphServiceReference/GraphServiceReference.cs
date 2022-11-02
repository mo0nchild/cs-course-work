﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторного создания кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GraphServiceReference
{
    using System.Runtime.Serialization;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.Runtime.Serialization.DataContractAttribute(Name="NodeData", Namespace="http://schemas.datacontract.org/2004/07/ServiceLibrary.ServiceContracts")]
    public partial class NodeData : object
    {
        
        private int NodeIDField;
        
        private int[] NodeInboxsIDField;
        
        private int[] NodeLinksIDField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int NodeID
        {
            get
            {
                return this.NodeIDField;
            }
            set
            {
                this.NodeIDField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int[] NodeInboxsID
        {
            get
            {
                return this.NodeInboxsIDField;
            }
            set
            {
                this.NodeInboxsIDField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int[] NodeLinksID
        {
            get
            {
                return this.NodeLinksIDField;
            }
            set
            {
                this.NodeLinksIDField = value;
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="GraphServiceReference.GraphCalculator")]
    public interface GraphCalculator
    {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GraphCalculator/FindPathByBFS", ReplyAction="http://tempuri.org/GraphCalculator/FindPathByBFSResponse")]
        int[] FindPathByBFS(int origin_id, int target_id, GraphServiceReference.NodeData[] node_list);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GraphCalculator/FindPathByBFS", ReplyAction="http://tempuri.org/GraphCalculator/FindPathByBFSResponse")]
        System.Threading.Tasks.Task<int[]> FindPathByBFSAsync(int origin_id, int target_id, GraphServiceReference.NodeData[] node_list);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    public interface GraphCalculatorChannel : GraphServiceReference.GraphCalculator, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    public partial class GraphCalculatorClient : System.ServiceModel.ClientBase<GraphServiceReference.GraphCalculator>, GraphServiceReference.GraphCalculator
    {
        
        /// <summary>
        /// Реализуйте этот разделяемый метод для настройки конечной точки службы.
        /// </summary>
        /// <param name="serviceEndpoint">Настраиваемая конечная точка</param>
        /// <param name="clientCredentials">Учетные данные клиента.</param>
        static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
        
        public GraphCalculatorClient() : 
                base(GraphCalculatorClient.GetDefaultBinding(), GraphCalculatorClient.GetDefaultEndpointAddress())
        {
            this.Endpoint.Name = EndpointConfiguration.NetTcpBinding_GraphCalculator.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public GraphCalculatorClient(EndpointConfiguration endpointConfiguration) : 
                base(GraphCalculatorClient.GetBindingForEndpoint(endpointConfiguration), GraphCalculatorClient.GetEndpointAddress(endpointConfiguration))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public GraphCalculatorClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(GraphCalculatorClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public GraphCalculatorClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(GraphCalculatorClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress)
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public GraphCalculatorClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        public int[] FindPathByBFS(int origin_id, int target_id, GraphServiceReference.NodeData[] node_list)
        {
            return base.Channel.FindPathByBFS(origin_id, target_id, node_list);
        }
        
        public System.Threading.Tasks.Task<int[]> FindPathByBFSAsync(int origin_id, int target_id, GraphServiceReference.NodeData[] node_list)
        {
            return base.Channel.FindPathByBFSAsync(origin_id, target_id, node_list);
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
        
        public virtual System.Threading.Tasks.Task CloseAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginClose(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndClose));
        }
        
        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.NetTcpBinding_GraphCalculator))
            {
                System.ServiceModel.NetTcpBinding result = new System.ServiceModel.NetTcpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                return result;
            }
            throw new System.InvalidOperationException(string.Format("Не удалось найти конечную точку с именем \"{0}\".", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.NetTcpBinding_GraphCalculator))
            {
                return new System.ServiceModel.EndpointAddress("net.tcp://localhost:8733/GraphService");
            }
            throw new System.InvalidOperationException(string.Format("Не удалось найти конечную точку с именем \"{0}\".", endpointConfiguration));
        }
        
        private static System.ServiceModel.Channels.Binding GetDefaultBinding()
        {
            return GraphCalculatorClient.GetBindingForEndpoint(EndpointConfiguration.NetTcpBinding_GraphCalculator);
        }
        
        private static System.ServiceModel.EndpointAddress GetDefaultEndpointAddress()
        {
            return GraphCalculatorClient.GetEndpointAddress(EndpointConfiguration.NetTcpBinding_GraphCalculator);
        }
        
        public enum EndpointConfiguration
        {
            
            NetTcpBinding_GraphCalculator,
        }
    }
}