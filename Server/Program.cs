using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            NetTcpBinding binding = new NetTcpBinding();
            string address = "net.tcp://localhost:9999/Server";

            ServiceHost host = new ServiceHost(typeof(Metode));
            host.AddServiceEndpoint(typeof(IMetode), binding, address);

            host.Open();
            Console.WriteLine("Servis je pokrenut.");

            Console.ReadLine();

            host.Close();
        }
    }
}
