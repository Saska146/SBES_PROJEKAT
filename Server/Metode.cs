using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class Metode : IMetode
    {
        [PrincipalPermission(SecurityAction.Demand, Role = "Change")]
        public void CreateFile(string fileName)
        {
            throw new NotImplementedException();
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

        [PrincipalPermission(SecurityAction.Demand, Role = "See")]
        public string ReadFile(string fileName)
        {
            throw new NotImplementedException();
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Change")]
        public bool Rename(string currentFileName, string newFileName)
        {
            throw new NotImplementedException();
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "See")]
        public (List<string> Files, List<string> Directories) ShowFolderContent(string folderName)
        {
            throw new NotImplementedException();
        }
    }
}
