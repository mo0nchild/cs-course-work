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

namespace HostApplication 
{
    public static class Host : System.Object
    {
        public static void Main(string[] args) 
        {
            using (var service_host = new ServiceHost(typeof(GraphService)))
            {
                service_host.Open();
                Console.WriteLine($"{typeof(GraphService).FullName} is hosting");
                Console.WriteLine($"Resourse address: net.tcp://localhost:8733/GraphService");
                Console.ReadKey();
            }
        }
    }
}