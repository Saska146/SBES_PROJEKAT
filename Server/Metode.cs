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
using SecurityManager;
using Formatter = SecurityManager.Formatter;

namespace Server
{
    public class Metode : IMetode
    {
        private static readonly string baseRoute = "C:\\Users\\Saska\\OneDrive\\Desktop\\SBES_PROJEKAT\\SBES_PROJEKAT\\Server\\ApplicationData";

       
        public void CreateFile(string fileName, string folderName, byte[] encryptedArray)
        {

            // server kreira fajl u ime klijenta (implicitna impersonifikacija)
            IIdentity identity = Thread.CurrentPrincipal.Identity;
            WindowsIdentity windowsIdentity = identity as WindowsIdentity;

            CustomPrincipal principal = Thread.CurrentPrincipal as CustomPrincipal;
            string userName = Formatter.ParseName(principal.Identity.Name);
            if (Thread.CurrentPrincipal.IsInRole("Change"))
            {
                using (windowsIdentity.Impersonate())
                {
                    Console.WriteLine($"Process Identity :{WindowsIdentity.GetCurrent().Name}");
                    try
                    {
                        string folderRoute = String.Format("{0}\\{1}", baseRoute, folderName);

                        var fileRoute = Path.Combine(folderRoute, fileName);

                        var decryptedArray = EncryptionMethods.DecrytpedText(encryptedArray);

                        var buffer = Encoding.UTF8.GetBytes(decryptedArray); //pretvaramo string u byte[] zbog write metode 

                        using (FileStream fOutput = new FileStream(fileRoute, FileMode.OpenOrCreate, FileAccess.Write))
                        {
                            fOutput.Write(buffer, 0, buffer.Length); // upisujemo tekst u fajl
                        }

                        Audit.CreateFileSuccess(fileName, userName);
                    }
                    catch (Exception e)
                    {
                        throw new FaultException<SecurityException>(new SecurityException(e.Message));
                    }

                }

                Console.WriteLine($"Process Identity :{WindowsIdentity.GetCurrent().Name}");
            }
            else
            {
                Console.WriteLine("User does not have permission!");
            }
        }

    
        public void CreateFolder(string folderName)
        {
            IIdentity identity = Thread.CurrentPrincipal.Identity;
            WindowsIdentity windowsIdentity = identity as WindowsIdentity;

            CustomPrincipal principal = Thread.CurrentPrincipal as CustomPrincipal;
            string userName = Formatter.ParseName(principal.Identity.Name);
            if (Thread.CurrentPrincipal.IsInRole("Change"))
            {
                using (windowsIdentity.Impersonate())
                {
                    Console.WriteLine($"Process Identity :{WindowsIdentity.GetCurrent().Name}");

                    try
                    {
                        string folderRoute = String.Format("{0}\\{1}", baseRoute, folderName);

                        System.IO.Directory.CreateDirectory(folderRoute);

                        Audit.CreateFolderSuccess(folderName, userName);

                    }
                    catch (Exception e)
                    {
                        throw new FaultException<SecurityException>(new SecurityException(e.Message));
                    }

                }

                Console.WriteLine($"Process Identity :{WindowsIdentity.GetCurrent().Name}");

            }
            else
            {
                Console.WriteLine("User does not have permission!");
            }
        }


        public void Delete(string fileName)
        {
            IIdentity identity = Thread.CurrentPrincipal.Identity;
            WindowsIdentity windowsIdentity = identity as WindowsIdentity;

            CustomPrincipal principal = Thread.CurrentPrincipal as CustomPrincipal;
            string userName = Formatter.ParseName(principal.Identity.Name);

            if (Thread.CurrentPrincipal.IsInRole("Change"))
            {
                using (windowsIdentity.Impersonate())
                {
                    Console.WriteLine($"Process Identity :{WindowsIdentity.GetCurrent().Name}");
                    try
                    {
                        var fileFolderRoute = FindFolderRoute(fileName);

                        var fileRoute = String.Format("{0}\\{1}", fileFolderRoute, fileName);

                        File.Delete(fileRoute);

                        Audit.DeleteFileSuccess(fileName, userName);
                    }
                    catch (Exception e)
                    {
                        throw new FaultException<SecurityException>(new SecurityException(e.Message));
                    }
                }
                Console.WriteLine($"Process Identity :{WindowsIdentity.GetCurrent().Name}");
            }
            else
            {
                Console.WriteLine("User does not have permission!");
            }
        }

        
        public void MoveTo(string fileName, string folderName)
        {
            IIdentity identity = Thread.CurrentPrincipal.Identity;
            WindowsIdentity windowsIdentity = identity as WindowsIdentity;

            CustomPrincipal principal = Thread.CurrentPrincipal as CustomPrincipal;
            string userName = Formatter.ParseName(principal.Identity.Name);
            if (Thread.CurrentPrincipal.IsInRole("Change"))
            {
                using (windowsIdentity.Impersonate())
            {
                try
                {
                    var fileFolderRoute = FindFolderRoute(fileName);

                    var fileRoute = String.Format("{0}\\{1}", fileFolderRoute, fileName);

                    string fileRouteNew = String.Format("{0}\\{1}\\{2}", baseRoute, folderName, fileName);

                    File.Move(fileRoute, fileRouteNew);

                    Audit.MoveToSuccess(fileName, userName, folderName);
                }
                catch (Exception e)
                {
                    throw new FaultException<SecurityException>(new SecurityException(e.Message));
                }

            }
            Console.WriteLine($"Process Identity :{WindowsIdentity.GetCurrent().Name}");
            }
            else
            {
                Console.WriteLine("User does not have permission!");
            }
        }

     
        public byte[] ReadFile(string fileName)
        {
            try
            {
                var fileFolderRoute = FindFolderRoute(fileName);

                var fileRoute = String.Format("{0}\\{1}", fileFolderRoute, fileName);

                var text = File.ReadAllText(fileRoute);

                var encryptedArray = EncryptionMethods.EncryptText(text);
                return encryptedArray;
            }
            catch (Exception e)
            {
                throw new FaultException<SecurityException>(new SecurityException(e.Message));
            }
           
        }

       
        public void Rename(string currentFileName, string newFileName)
        {
            IIdentity identity = Thread.CurrentPrincipal.Identity;
            WindowsIdentity windowsIdentity = identity as WindowsIdentity;

            CustomPrincipal principal = Thread.CurrentPrincipal as CustomPrincipal;
            string userName = Formatter.ParseName(principal.Identity.Name);
            if (Thread.CurrentPrincipal.IsInRole("Change"))
            {
                using (windowsIdentity.Impersonate()) {
                try
                {
                    var fileFolderRoute = FindFolderRoute(currentFileName);

                    var fileRoute = String.Format("{0}\\{1}", fileFolderRoute, currentFileName);

                    var fileRouteNew = String.Format("{0}\\{1}", fileFolderRoute, newFileName);

                    File.Move(fileRoute, fileRouteNew);

                    Audit.RenameFileSuccess(currentFileName, userName, newFileName);
                }
                catch (Exception e)
                {
                    throw new FaultException<SecurityException>(new SecurityException(e.Message));
                }
         
            }
            Console.WriteLine($"Process Identity :{WindowsIdentity.GetCurrent().Name}");
            }
            else
            {
                Console.WriteLine("User does not have permission!");
            }
        }


        public List<string> ShowFolderContent(string folderName)
        {
            try
            {
                string folderRoute = String.Format("{0}\\{1}", baseRoute, folderName); //dobavljamo rutu foldera

                DirectoryInfo di = new DirectoryInfo(folderRoute); //directory info pomocna klasa na koju pozivamo get files

                var files = di.GetFiles().Select(x => x.Name).ToList(); //vraca imena svih faljova u datoteci

                return files;
            }
            catch (Exception e)
            {
                throw new FaultException<SecurityException>(new SecurityException(e.Message));
            }
          
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
