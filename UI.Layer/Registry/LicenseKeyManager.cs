using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace UI.Layer.Registry
{
    internal static class LicenseKeyManager
    {
        static string Password = "12345";
        static string ProductKey = "845821B1-1198-4658-B7BA-712425DA8047";
        static byte[] SaltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

        static LicenseKeyManager()
        {

        }

        public static bool IsRegistred()
        {
            bool isValid = false;
            try
            {
                string content = Decrypt();
                byte[] contentBytes = Encoding.UTF8.GetBytes(content);
                using (MemoryStream memoryStream = new MemoryStream(contentBytes))
                {
                    using (StreamReader streamReader = new StreamReader(memoryStream))
                    {
                        string productKey = streamReader.ReadLine().Replace("PRODUCTKEY=", "");
                        string dateText = streamReader.ReadLine().Replace("EXPIRYDATE=", "");
                        DateTime expiryDate = Convert.ToDateTime(dateText);
                        isValid = (productKey == ProductKey && expiryDate > DateTime.Now);
                    }
                }
            }
            catch
            {
                throw;
            }
            return isValid;
        }

        private static string Decrypt()
        {
            byte[] decryptedBytes = null;

            try
            {
                string path = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "LicenseKey.key";
                bool exist = File.Exists(path);
                if (!exist)
                    throw new LicenseKeyException("Clave de producto no encontrada");
                string encryptedText = File.ReadAllText(path);
                byte[] bytesToBeDecrypted = Convert.FromBase64String(encryptedText);
                byte[] passwordBytes = Encoding.UTF8.GetBytes(Password);
                passwordBytes = SHA256.Create().ComputeHash(passwordBytes);
                try
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (RijndaelManaged AES = new RijndaelManaged())
                        {
                            var key = new Rfc2898DeriveBytes(passwordBytes, SaltBytes, 1000);

                            AES.KeySize = 256;
                            AES.BlockSize = 128;
                            AES.Key = key.GetBytes(AES.KeySize / 8);
                            AES.IV = key.GetBytes(AES.BlockSize / 8);
                            AES.Mode = CipherMode.CBC;

                            using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                            {
                                cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                                cs.Close();
                            }

                            decryptedBytes = ms.ToArray();
                        }
                    }
                }
                catch
                {
                    throw new LicenseKeyException("Clave de producto inválida");
                }
            }
            catch
            {
                throw;
            }

            return Encoding.UTF8.GetString(decryptedBytes);
        }
    }
}
