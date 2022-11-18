using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Configuration;

using ServiceLibrary;
using System.Windows.Input;
using System.Drawing;
using System.IO;
using System.Text;
using System.Net.Mail;
using System.Net;
using ServiceLibrary.DataSerializations;
using System.Xml.Serialization;
using System.Runtime.Remoting.Messaging;
using ServiceLibrary.ServiceContracts;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using ServiceLibrary.ServiceLocatorTool;
using ServiceLibrary.DataTransfer;
using ServiceLibrary.DataEncoder;

namespace HostApplication 
{
    public static class Host : System.Object
    {
        public static void Main(System.String[] args)
        {
            ServiceLocator.RegistrationService<INetworkTransfer<SmptMessageEnvelope>>(new SmtpTransferFactory());
            ServiceLocator.RegistrationService<IServiceDataEncoder<ProjectInfo>>(new ServiceDataEncoderFactory());

            using (var servicehost_instance = new ServiceHost(typeof(ServiceLibrary.GraphService)))
            {
                servicehost_instance.Open();
                Console.WriteLine($"{typeof(GraphService).FullName} is hosting");

                foreach (var address in servicehost_instance.BaseAddresses)
                {
                    Console.WriteLine($"Resourse address: {address.ToString()}");
                }
                Console.ReadKey();
            }
        }
    }
}