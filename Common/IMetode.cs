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
        void CreateFile(string fileName, string folderName, byte[] encryptedArray);

        [OperationContract]
        [FaultContract(typeof(SecurityException))]
        void CreateFolder(string folderName);

        [OperationContract]
        [FaultContract(typeof(SecurityException))]
        List<string> ShowFolderContent(string folderName);

        [OperationContract]
        [FaultContract(typeof(SecurityException))]
        byte[] ReadFile(string fileName);

        [OperationContract]
        [FaultContract(typeof(SecurityException))]
        void Delete(string fileName);

        [OperationContract]
        [FaultContract(typeof(SecurityException))]
        void Rename(string currentFileName, string newFileName);

        [OperationContract]
        [FaultContract(typeof(SecurityException))]
        void MoveTo(string fileName, string folderName);
    }
}
