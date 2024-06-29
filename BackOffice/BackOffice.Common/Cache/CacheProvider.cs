using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice.Common.Cache
{
    public class CacheProvider
    {
        #region Fields
        private string appDataPath;
        private static readonly byte[] cKey = { 9, 5, 7, 3, 1, 66, 14, 5 };
        private static readonly byte[] cIV = { 4, 255, 56, 61, 43, 33,23, 12 };
        private BinaryFormatter binaryFormatter;
        #endregion
        #region Ctor
        public CacheProvider(string appDataPath)
        {
            this.appDataPath = appDataPath;
            this.binaryFormatter = new BinaryFormatter();
        }
        #endregion
        #region Methods
        public bool Clear()
        {
            var folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BackOffice");
            if (Directory.Exists(folder))
            {
                Directory.Delete(folder, true);
                return true;
            }
            return false;
        }
        public bool ContainsKey(string key)
        {
            return File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BackOffice"));
        }
        public string CreateKey(params string[] subkeys)
        {
            var folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BackOffice");
            if(!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
            var fileName = subkeys[subkeys.Length - 1] + ".cache";

            return Path.Combine(folder, fileName);

        }
        public void Set<T>(string fileName, T data)
        {
            try
            {
                using (DES des = new DESCryptoServiceProvider())
                {
                    using (FileStream fileStream = File.Create(fileName))
                    {
                        var transformer = des.CreateEncryptor(cKey, cIV);
                        using (CryptoStream cryptoStream = new CryptoStream(fileStream, transformer, CryptoStreamMode.Write))
                        {
                            binaryFormatter.Serialize(cryptoStream, data);
                            Debug.WriteLine($"{fileName}.cache has been created.");
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"Error during write cache. Message: {ex.Message}");
            }
        }

        public T Get<T>(string fileName)
        {
            T data = default(T);
            try
            {
                using (DES des = new DESCryptoServiceProvider())
                {
                    using (FileStream fileStream = new FileStream($"{fileName}", FileMode.OpenOrCreate))
                    {
                        var transformer = des.CreateDecryptor(cKey, cIV);
                        Stream cryptoStream = new CryptoStream(fileStream, transformer, CryptoStreamMode.Read);
                        try
                        {
                            data = (T)binaryFormatter.Deserialize(cryptoStream);
                            Debug.WriteLine($"{fileName}.cache has been read.");
                        }
                        finally
                        {
                            cryptoStream.Dispose();
                        }
                        return data;
                    }
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"Error during read cache. Message: {ex.Message}");
            }
            return data;
        }
        #endregion
    }
}
