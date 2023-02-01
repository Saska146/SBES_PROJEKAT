using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [ServiceContract]
    public interface IMetode
    {
        [OperationContract]
        [FaultContract(typeof(SecurityException))]
        void CreateFile(string fileName, string folderName, string text);

        [OperationContract]
        [FaultContract(typeof(SecurityException))]
        void CreateFolder(string folderName);

        [OperationContract]
        [FaultContract(typeof(SecurityException))]
        (List<string> Files, List<string> Directories) ShowFolderContent(string folderName);

        [OperationContract]
        [FaultContract(typeof(SecurityException))]
        string ReadFile(string fileName);

        [OperationContract]
        [FaultContract(typeof(SecurityException))]
        void Delete(string fileName);

        [OperationContract]
        [FaultContract(typeof(SecurityException))]
        bool Rename(string currentFileName, string newFileName);

        [OperationContract]
        [FaultContract(typeof(SecurityException))]
        void MoveTo(string fileName, string folderName);
    }
}
