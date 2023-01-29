using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            NetTcpBinding binding = new NetTcpBinding();
            string address = "net.tcp://localhost:9999/Server";

            ChannelFactory<IMetode> factory = new ChannelFactory<IMetode>(binding, address);
            IMetode channel = factory.CreateChannel();

            Console.ReadLine();
        }
    }
}
