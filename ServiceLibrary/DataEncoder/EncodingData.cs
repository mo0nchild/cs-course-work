using ServiceLibrary.ServiceLocatorTool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLibrary.DataEncoder
{
    public interface IServiceDataEncoder<TServiceData> : IServiceContract where TServiceData : class
    {
        System.String EncodeData(TServiceData encoding_data);
        TServiceData DecodeData(System.String decoding_data);
    }

    public sealed class ServiceDataEncoderFactory : ServiceLocatorTool.ServiceFactoryBase
    {
        public override ServiceLocatorTool.ServiceBase BuildService() => new ServiceDataEncoder();
    }

    public sealed class ServiceDataEncoder : ServiceLocatorTool.ServiceBase, IServiceDataEncoder<ServiceContracts.ProjectInfo>
    {
        public System.Type FactoryType { get => typeof(ServiceDataEncoderFactory); }
        public ServiceDataEncoder() : base(typeof(ServiceDataEncoder).Name) { }

        public string EncodeData(ServiceContracts.ProjectInfo encoding_data)
        {
            var encoding_text = $"{encoding_data.ProjectName};{encoding_data.FileName};" +
                $"{encoding_data.CreateTime}";

            var encoded_data = string.Empty;
            foreach (var data in Encoding.UTF8.GetBytes(encoding_text)) encoded_data += string.Format("{0:X} ", data);

            return encoded_data;
        }

        public ServiceContracts.ProjectInfo DecodeData(string decoding_data)
        {
            var filtered_data = decoding_data.Split(' ').Where((string data) => data != String.Empty);

            var encoded_data = filtered_data.Select((string @string) 
                => byte.Parse(@string, System.Globalization.NumberStyles.HexNumber)).ToArray();

            var properties_strings = Encoding.UTF8.GetString(encoded_data).Split(';');
            return new ServiceContracts.ProjectInfo()
            {
                ProjectName = properties_strings[0], FileName = properties_strings[1],
                CreateTime = DateTime.Parse(properties_strings[2])
            };
        }
    }
}
