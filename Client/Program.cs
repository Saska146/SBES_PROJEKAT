using Common;
using Server;
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
            string address = "net.tcp://localhost:9999/Metode";

            binding.Security.Mode = SecurityMode.Transport;
            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;
            binding.Security.Transport.ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign;

            Console.WriteLine("Korisnik koji je pokrenuo klijenta :" + WindowsIdentity.GetCurrent().Name);

         
            EndpointAddress endpointAddress = new EndpointAddress(new Uri(address),
                EndpointIdentity.CreateUpnIdentity("wcfServer"));

            using (ClientProxy proxy = new ClientProxy(binding, endpointAddress))
            {

                //proxy.CreateFolder("Folder5");
                proxy.CreateFile("test11.txt", "Folder5", "text1");
                //proxy.Delete("test.txt");
                // proxy.MoveTo("test.txt", "Folder5");
                // proxy.Rename("test.txt", "test3.txt");
                //Console.WriteLine(proxy.ReadFile("test3.txt"));
                /*
                var met = new Metode();
                var names = met.ShowFolderContent("Folder5");
                foreach (var name in names)
                    Console.WriteLine(name);
                */
                //Console.WriteLine(proxy.ReadFileText("test3.txt"));
            }
            //ChannelFactory<IMetode> factory = new ChannelFactory<IMetode>(binding, address);
            //IMetode channel = factory.CreateChannel();


            Console.ReadLine();
        }
    }
}
