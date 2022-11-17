using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Runtime.Serialization;

namespace TransferDataPackage.DataSerializations
{
    public sealed class VertexInfo : System.Object
    {
        public System.Int32 ID { get; set; } = default;
        public System.Int32 PositionX { get; set; } = default;
        public System.Int32 PositionY { get; set; } = default;
    }

    public sealed class EdgeInfo : System.Object
    {
        public System.Int32 RightNodeID { get; set; } = default;
        public System.Int32 LeftNodeID { get; set; } = default;
    }

    public class TransferDataInteraction<TData> : System.Object
    {
        protected List<TData> TransferData { get; private set; } = default;
        public TransferDataInteraction(List<TData> transfer_data) => this.TransferData = transfer_data;
    }

    public class TransferDataInstaller<TData> : TransferDataInteraction<TData>
    {
        public TransferDataInstaller(List<TData> transfer_data) : base(transfer_data) { }
        public void InstallData(TData transfer_data) => this.TransferData.Add(transfer_data);
    }

    public class TransferDataExtractor<TData> : TransferDataInteraction<TData>, IEnumerable<TData>
    {
        public TransferDataExtractor(List<TData> data) : base(data) { }
        public IEnumerable<TData> ExtractData()
        { foreach (TData transfer_data in base.TransferData) yield return transfer_data; }

        public IEnumerator<TData> GetEnumerator() => base.TransferData.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }

    [SerializableAttribute, DataContractAttribute(Name = "TransferData", Namespace = "http://coolapp.com")]
    public class DataSerializationBase : System.Object
    {
        [DataMemberAttribute(Name = "VertexList", Order = 0)]
        private List<VertexInfo> VertexList { get; set; } = new List<VertexInfo>();

        [DataMemberAttribute(Name = "EdgeList", Order = 1)]
        private List<EdgeInfo> EdgeList { set; get; } = new List<EdgeInfo>();

        public DataSerializationBase() : base() { }

        public DataSerializationBase StateInstaller()
        {
            var vertexs_installer = new TransferDataInstaller<VertexInfo>(this.VertexList);
            var edges_installer = new TransferDataInstaller<EdgeInfo>(this.EdgeList);

            this.SetContextData(vertexs_installer, edges_installer);
            return new DataSerializationBase { EdgeList = this.EdgeList, VertexList = this.VertexList };
        }
        public TData StateExtraction<TData>() where TData : DataSerializationBase
        {
            var vertexs_extractor = new TransferDataExtractor<VertexInfo>(this.VertexList);
            var edges_extractor = new TransferDataExtractor<EdgeInfo>(this.EdgeList);

            var data_serialization = (TData)Activator.CreateInstance(typeof(TData));
            data_serialization.GetContextData(vertexs_extractor, edges_extractor);

            return data_serialization;
        }
        protected virtual void SetContextData(TransferDataInstaller<VertexInfo> vertexs,
            TransferDataInstaller<EdgeInfo> edges)
        { }
        protected virtual void GetContextData(TransferDataExtractor<VertexInfo> vertexs,
            TransferDataExtractor<EdgeInfo> edges)
        { }
    }
}
