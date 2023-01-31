using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Security.Principal;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    public class Metode : IMetode
    {
   
         [PrincipalPermission(SecurityAction.Demand, Role = "Change")]

        public void CreateFile(string fileName)
        {

            // server kreira fajl u ime klijenta (implicitna impersonifikacija)
            IIdentity identity = Thread.CurrentPrincipal.Identity;
            WindowsIdentity windowsIdentity = identity as WindowsIdentity;

            using (windowsIdentity.Impersonate())
            {
                Console.WriteLine($"Process Identity :{WindowsIdentity.GetCurrent().Name}");
                try
                {
                    StreamWriter sw = File.CreateText(fileName + 1);
                    sw.Close();
                }
                catch (Exception e)
                {
                    throw new FaultException<SecurityException>(new SecurityException(e.Message));
                }

            }

            Console.WriteLine($"Process Identity :{WindowsIdentity.GetCurrent().Name}");
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Change")]
        public void CreateFolder(string folderName)
        {
            throw new NotImplementedException();
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Delete")]
        public void Delete(string fileName)
        {
            throw new NotImplementedException();
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Change")]
        public bool MoveTo(string fileName, string folderName)
        {
            throw new NotImplementedException();
        }

     
        public string ReadFile(string fileName)
        {
            throw new NotImplementedException();
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Change")]
        public bool Rename(string currentFileName, string newFileName)
        {
            throw new NotImplementedException();
        }

      
        public (List<string> Files, List<string> Directories) ShowFolderContent(string folderName)
        {
            throw new NotImplementedException();
        }
    }
}
