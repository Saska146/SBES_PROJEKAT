using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
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

            binding.Security.Mode = SecurityMode.Transport;
            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;
            binding.Security.Transport.ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign;

            Console.WriteLine("Korisnik koji je pokrenuo servera :" + WindowsIdentity.GetCurrent().Name);

            EndpointAddress endpointAddress = new EndpointAddress(new Uri(address),
                EndpointIdentity.CreateUpnIdentity("wcfServer"));

            using (ClientProxy proxy = new ClientProxy(binding, endpointAddress))
            {
                proxy.CreateFile("File");
                
            }

            ChannelFactory<IMetode> factory = new ChannelFactory<IMetode>(binding, address);
            IMetode channel = factory.CreateChannel();

            Console.ReadLine();
        }
    }
}
