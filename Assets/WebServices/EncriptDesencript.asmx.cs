using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Services;

namespace ProjectoFinal_Cinel_2024.Assets.WebServices
{
    /// <summary>
    /// Summary description for EncriptDesencript
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class EncriptDesencript : System.Web.Services.WebService
    {
        [WebMethod]
        public string Encriptar(string Message)
        {
            string Passphrase = ConfigurationManager.AppSettings["Passphrase"];
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();

            using (SHA256CryptoServiceProvider HashProvider = new SHA256CryptoServiceProvider())
            using (AesCryptoServiceProvider AESAlgorithm = new AesCryptoServiceProvider())
            {
                byte[] AESKey = HashProvider.ComputeHash(UTF8.GetBytes(Passphrase));
                AESAlgorithm.Key = AESKey;
                AESAlgorithm.Mode = CipherMode.ECB;
                AESAlgorithm.Padding = PaddingMode.PKCS7;

                byte[] DataToEncrypt = UTF8.GetBytes(Message);

                ICryptoTransform Encryptor = AESAlgorithm.CreateEncryptor();
                Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);

                string enc = Convert.ToBase64String(Results);
                enc = System.Web.HttpUtility.UrlEncode(enc);
                return enc;
            }
        }

        [WebMethod]
        public string Desencriptar(string enc)
        {
            string Passphrase = ConfigurationManager.AppSettings["Passphrase"];
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();

            using (SHA256CryptoServiceProvider HashProvider = new SHA256CryptoServiceProvider())
            using (AesCryptoServiceProvider AESAlgorithm = new AesCryptoServiceProvider())
            {
                byte[] AESKey = HashProvider.ComputeHash(UTF8.GetBytes(Passphrase));
                AESAlgorithm.Key = AESKey;
                AESAlgorithm.Mode = CipherMode.ECB;
                AESAlgorithm.Padding = PaddingMode.PKCS7;

                enc = System.Web.HttpUtility.UrlDecode(enc);
                byte[] DataToDecrypt = Convert.FromBase64String(enc);

                ICryptoTransform Decryptor = AESAlgorithm.CreateDecryptor();
                Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);
            }

            return UTF8.GetString(Results);
        }
    }
}
