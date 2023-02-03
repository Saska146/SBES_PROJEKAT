using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class EncryptionMethods
    {
        // public static readonly string keyRoute = Path.Combine(Directory.GetParent(Directory.GetParent(Directory.GetParent(Environment.CurrentDirectory).FullName).FullName).FullName, "key.txt");
        public static readonly string keyRoute = "C:\\Users\\danij\\OneDrive\\Radna površina\\SBES-PROJEKAT\\SBES_PROJEKAT\\key.txt";
        private static readonly byte[] secretIv = Convert.FromBase64String("Jcb8G24OjbJ1NwHF47GR9A==");


        private static string GenerateKey()
        {
            SymmetricAlgorithm symmAlgorithm = AesCryptoServiceProvider.Create();

            return symmAlgorithm == null ? String.Empty : ASCIIEncoding.ASCII.GetString(symmAlgorithm.Key);
        }

        public static void StoreKey(string secretKey, string outFile)
        {
            FileStream fOutput = new FileStream(outFile, FileMode.OpenOrCreate, FileAccess.Write);
            byte[] buffer = Encoding.ASCII.GetBytes(secretKey);

            try
            {
                fOutput.Write(buffer, 0, buffer.Length);
            }
            catch (Exception e)
            {
                Console.WriteLine("SecretKeys.StoreKey:: ERROR {0}", e.Message);
            }
            finally
            {
                fOutput.Close();
            }
        }

        public static string LoadKey(string inFile)
        {
            FileStream fInput = new FileStream(inFile, FileMode.Open, FileAccess.Read);
            byte[] buffer = new byte[(int)fInput.Length];

            try
            {
                fInput.Read(buffer, 0, (int)fInput.Length);
            }
            catch (Exception e)
            {
                Console.WriteLine("SecretKeys.LoadKey:: ERROR {0}", e.Message);
            }
            finally
            {
                fInput.Close();
            }

            return ASCIIEncoding.ASCII.GetString(buffer);
        }

        private static string GetKey()
        {
            if (File.Exists(keyRoute))
                return LoadKey(keyRoute);

            var key = GenerateKey();

            StoreKey(key, keyRoute);

            return key;
        }

        public static byte[] EncryptText(string text)
        {
            byte[] data = ASCIIEncoding.UTF8.GetBytes(text);
            var secretKey = Encoding.ASCII.GetBytes(GetKey());

            byte[] encoded;

            using (Aes cipher = Aes.Create())
            {
                cipher.Padding = PaddingMode.ISO10126;
                cipher.Key = secretKey;
                cipher.Mode = CipherMode.CBC;

                ICryptoTransform encryptor = cipher.CreateEncryptor(cipher.Key, secretIv);

                encoded = encryptor.TransformFinalBlock(data, 0, data.Length);
            }

            return encoded;
        }

        public static string DecrytpedText(byte[] data)
        {
            string decoded = null;
            var secretKey = Encoding.ASCII.GetBytes(GetKey());

            using (Aes cipher = Aes.Create())
            {
                cipher.Padding = PaddingMode.ISO10126;
                cipher.Key = secretKey;
                cipher.Mode = CipherMode.CBC;

                ICryptoTransform encryptor = cipher.CreateDecryptor(cipher.Key, secretIv);

                decoded = ASCIIEncoding.UTF8.GetString(encryptor.TransformFinalBlock(data, 0, data.Length));
            }

            return decoded;
        }
    }
}

