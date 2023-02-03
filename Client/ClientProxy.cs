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



        public void CreateFile(string fileName, string folderName, byte[] encryptedText)
        {
            try
            {
                factory.CreateFile(fileName, folderName, encryptedText);
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

        public void CreateFile(string fileName, string folderName, string text )
        {
            try
            {
                var arr = EncryptionMethods.EncryptText(text);
                CreateFile(fileName, folderName, arr);
            }
            catch(Exception e)
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

        public byte[] ReadFile(string fileName)
        {
            byte[] encryptedData;
            try
            {
                encryptedData = factory.ReadFile(fileName);
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
            return encryptedData;
        }

        public string ReadFileText(string fileName)
        {
            var encrypted = ReadFile(fileName);
            var decrypted = EncryptionMethods.DecrytpedText(encrypted);
            return decrypted;
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
