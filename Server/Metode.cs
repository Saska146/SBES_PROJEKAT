using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class Metode : IMetode
    {
        public void CreateFile(string fileName)
        {
            throw new NotImplementedException();
        }

        public void CreateFolder(string folderName)
        {
            throw new NotImplementedException();
        }

        public void Delete(string fileName)
        {
            throw new NotImplementedException();
        }

        public bool MoveTo(string fileName, string folderName)
        {
            throw new NotImplementedException();
        }

        public string ReadFile(string fileName)
        {
            throw new NotImplementedException();
        }

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
