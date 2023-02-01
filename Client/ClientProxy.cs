using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using SecurityException = Common.SecurityException;

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
            Credentials.Windows.AllowedImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.Impersonation;

            factory = this.CreateChannel();
            //Credentials.Windows.AllowNtlm = false;
        }



        public void CreateFile(string fileName, string folderName, string text)
        {
            try
            {
                factory.CreateFile(fileName, folderName, text);
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

        public void MoveTo(string fileName, string folderName)
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

        public void Rename(string currentFileName, string newFileName)
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
            
        }

        public List<string> ShowFolderContent(string folderName)
        {
            try
            {
                 factory.ShowFolderContent(folderName);
            }
            catch (FaultException<SecurityException> e)
            {
                Console.WriteLine("Error: {0}", e.Detail.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
            }
               return (new List<string>());
        }
    }
}
