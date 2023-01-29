using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class ClientProxy : ChannelFactory<IMetode>, IMetode, IDisposable
    {
        IMetode factory;

        public ClientProxy(NetTcpBinding binding, string address) : base(binding, address)
        {
            factory = this.CreateChannel();
        }

        public ClientProxy(NetTcpBinding binding, EndpointAddress address) : base(binding, address)
        {
            factory = this.CreateChannel();
            //Credentials.Windows.AllowNtlm = false;
        }



        public void CreateFile(string fileName)
        {
            try
            {
                factory.CreateFile(fileName);
            }
            catch (FaultException<SecurityException> e)
            {
                Console.WriteLine("Error: {0}", e.Detail.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
            }
        }

        public void CreateFolder(string folderName)
        {
            try
            {
                factory.CreateFolder(folderName);
            }
            catch (FaultException<SecurityException> e)
            {
                Console.WriteLine("Error: {0}", e.Detail.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
            }
        }

        public void Delete(string fileName)
        {
            try
            {
                factory.Delete(fileName);
            }
            catch (FaultException<SecurityException> e)
            {
                Console.WriteLine("Error: {0}", e.Detail.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
            }
        }

        public bool MoveTo(string fileName, string folderName)
        {
            try
            {
                factory.MoveTo(fileName, folderName);
            }
            catch (FaultException<SecurityException> e)
            {
                Console.WriteLine("Error: {0}", e.Detail.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
            }
            return false;
        }

        public string ReadFile(string fileName)
        {
            try
            {
                factory.ReadFile(fileName);
            }
            catch (FaultException<SecurityException> e)
            {
                Console.WriteLine("Error: {0}", e.Detail.Message);
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
                return null;
            }
            return factory.ReadFile(fileName);
        }

        public bool Rename(string currentFileName, string newFileName)
        {
            try
            {
                factory.Rename(currentFileName, newFileName);
            }
            catch (FaultException<SecurityException> e)
            {
                Console.WriteLine("Error: {0}", e.Detail.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
            }
            return false;
        }

        public (List<string> Files, List<string> Directories) ShowFolderContent(string folderName)
        {
            try
            {
                return factory.ShowFolderContent(folderName);
            }
            catch (FaultException<SecurityException> e)
            {
                Console.WriteLine("Error: {0}", e.Detail.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
            }
            return (new List<string>(), new List<string>());
        }
    }
}
