using System;
using System.Collections.Generic;
using System.ServiceModel;

using ServiceLibrary;

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
                Console.ReadKey();
            }
        }
    }
}