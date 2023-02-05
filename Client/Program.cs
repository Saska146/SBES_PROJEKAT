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

            Console.WriteLine("Korisnik koji je pokrenuo klijenta: " + WindowsIdentity.GetCurrent().Name);


            EndpointAddress endpointAddress = new EndpointAddress(new Uri(address),
                EndpointIdentity.CreateUpnIdentity("wcfServer"));

            using (ClientProxy proxy = new ClientProxy(binding, endpointAddress))
            {
                bool run = true;
                do
                {
                    Console.WriteLine("--------------------------------");
                    Console.WriteLine("Select option: ");
                    Console.WriteLine("1. Create folder");
                    Console.WriteLine("2. Create file");
                    Console.WriteLine("3. Rename file");
                    Console.WriteLine("4. Delete file");
                    Console.WriteLine("5. Move file");
                    Console.WriteLine("6. Read file");
                    Console.WriteLine("7. Show folder content");
                    Console.WriteLine("8. Exit");
                    Console.WriteLine("--------------------------------");

                    int input = int.Parse(Console.ReadLine());
                    Console.WriteLine("---------------------------------");
                    switch (input)
                    {
                        case 1:
                            Console.WriteLine("Enter folder name: ");
                            var folderNameCreate = Console.ReadLine();
                            proxy.CreateFolder(folderNameCreate);
                            break;

                        case 2:
                            Console.WriteLine("Enter file name: ");
                            var fileNameCreate = Console.ReadLine();

                            Console.WriteLine("Enter folder name: ");
                            var folderName = Console.ReadLine();

                            Console.WriteLine("Enter text: ");
                            var text = Console.ReadLine();

                            proxy.CreateFile(fileNameCreate, folderName, text);
                            break;

                        case 3:
                            Console.WriteLine("Enter current file name: ");
                            var currentFileName = Console.ReadLine();

                            Console.WriteLine("Enter new file name: ");
                            var newFileName = Console.ReadLine();

                            proxy.Rename(currentFileName, newFileName);
                            break;

                        case 4:
                            Console.WriteLine("Enter file to delete: ");
                            var fileToDelete = Console.ReadLine();

                            proxy.Delete(fileToDelete);
                            break;

                        case 5:
                            Console.WriteLine("Enter file to move: ");
                            var fileToMove = Console.ReadLine();

                            Console.WriteLine("Enter folder destination: ");
                            var folderDestination = Console.ReadLine();

                            proxy.MoveTo(fileToMove, folderDestination);
                            break;

                        case 6:
                            Console.WriteLine("Enter file to read: ");
                            var fileToRead = Console.ReadLine();

                            var textRead = proxy.ReadFileText(fileToRead);

                            Console.WriteLine("Text content: ");
                            Console.WriteLine(textRead);

                            break;

                        case 7:
                            Console.WriteLine("Enter folder name to show content: ");
                            var folderToShowContent = Console.ReadLine();

                            List<string> folderContent = proxy.ShowFolderContent(folderToShowContent);

                            Console.WriteLine("Folder contents: ");

                            if (folderContent.Count == 0)
                            {
                                Console.WriteLine("Nema fajlova!");
                            }
                            else
                            {
                                foreach (var content in folderContent)
                                {

                                    Console.WriteLine(content);
                                }
                            }
                         
                            break;

                        case 8:
                            Console.WriteLine("Exiting...");
                            break;
                    }
                } while (run);

                Console.ReadLine();
            }
        }
    }
}