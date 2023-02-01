using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
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
        private static readonly string baseRoute = "C:\\Users\\danij\\OneDrive\\Radna površina\\PROJEKAT\\SBES_PROJEKAT\\Server\\ApplicationData";

        [PrincipalPermission(SecurityAction.Demand, Role = "Change")]
        public void CreateFile(string fileName, string folderName, string text)
        {

            // server kreira fajl u ime klijenta (implicitna impersonifikacija)
            IIdentity identity = Thread.CurrentPrincipal.Identity;
            WindowsIdentity windowsIdentity = identity as WindowsIdentity;

            using (windowsIdentity.Impersonate())
            {
                Console.WriteLine($"Process Identity :{WindowsIdentity.GetCurrent().Name}");
                try
                {
                    string folderRoute = String.Format("{0}\\{1}", baseRoute, folderName);

                    var fileRoute = Path.Combine(folderRoute, fileName);

                    var buffer = Encoding.UTF8.GetBytes(text); //pretvaramo string u byte[] zbog write metode 

                    using (FileStream fOutput = new FileStream(fileRoute, FileMode.OpenOrCreate, FileAccess.Write))
                    {
                        fOutput.Write(buffer, 0, buffer.Length); // upisujemo tekst u fajl
                    }
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
            IIdentity identity = Thread.CurrentPrincipal.Identity;
            WindowsIdentity windowsIdentity = identity as WindowsIdentity;

            using (windowsIdentity.Impersonate())
            {
                Console.WriteLine($"Process Identity :{WindowsIdentity.GetCurrent().Name}");
     
                try
                {
                    string folderRoute = String.Format("{0}\\{1}", baseRoute, folderName);

                    System.IO.Directory.CreateDirectory(folderRoute);

                }
                catch(Exception e)
                {
                    throw new FaultException<SecurityException>(new SecurityException(e.Message));
                }

            }
            Console.WriteLine($"Process Identity :{WindowsIdentity.GetCurrent().Name}");
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Delete")]
        public void Delete(string fileName)
        {
            IIdentity identity = Thread.CurrentPrincipal.Identity;
            WindowsIdentity windowsIdentity = identity as WindowsIdentity;

            using (windowsIdentity.Impersonate())
            {
                Console.WriteLine($"Process Identity :{WindowsIdentity.GetCurrent().Name}");
                try
                {
                    var fileFolderRoute = FindFolderRoute(fileName);

                    var fileRoute = String.Format("{0}\\{1}", fileFolderRoute, fileName);

                    File.Delete(fileRoute);
                }
                catch (Exception e)
                {
                    throw new FaultException<SecurityException>(new SecurityException(e.Message));
                }
            }
            Console.WriteLine($"Process Identity :{WindowsIdentity.GetCurrent().Name}");
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Change")]
        public void MoveTo(string fileName, string folderName)
        {
            IIdentity identity = Thread.CurrentPrincipal.Identity;
            WindowsIdentity windowsIdentity = identity as WindowsIdentity;

            using (windowsIdentity.Impersonate())
            {
                try
                {
                    var fileFolderRoute = FindFolderRoute(fileName);

                    var fileRoute = String.Format("{0}\\{1}", fileFolderRoute, fileName);

                    string fileRouteNew = String.Format("{0}\\{1}\\{2}", baseRoute, folderName, fileName);

                    System.IO.File.Move(fileRoute, fileRouteNew);
                }
                catch (Exception e)
                {
                    throw new FaultException<SecurityException>(new SecurityException(e.Message));
                }

            }
            Console.WriteLine($"Process Identity :{WindowsIdentity.GetCurrent().Name}");
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

        private string FindFolderRoute(string fileName)
        {
            DirectoryInfo di = new DirectoryInfo(baseRoute);
            var folderNames = di.GetDirectories().Select(x => x.FullName); //fullname vraca rutu

            foreach (var folderName in folderNames)
            {
                var folderInfo = new DirectoryInfo(folderName);

                var fileNames = folderInfo.GetFiles().Select(x => x.Name); //name vraca samo naziv sa ekstenzijom

                if (fileNames.Contains(fileName))
                    return folderName;
            }

            return "";
        }
    }
}
