using System;
using ML.Common;
using log4net;

namespace Share.Helper
{
    public interface ICryptoHelper
    {
        bool Encrypt(string source, ref string encrypt);

        bool Decrypt(string encrypt, ref string decrypt);

        string DecryptPassword(string encryptPassword);

        string EncryptPassword(string password);
    }

    public class CryptoHelper : ICryptoHelper
    {
        private readonly string _cryptoKey = std.AppSettings["CryptoKey"];

        private readonly ILog _log = LogManager.GetLogger(typeof(CryptoHelper));

        public bool Encrypt(string source, ref string encrypt)
        {
            try
            {
                encrypt = source;

                if (!string.IsNullOrEmpty(source))
                {
                    encrypt = CryptoByAES.EncryptStringAES(source, _cryptoKey);

                    return true;
                }
            }
            catch (Exception ex)
            {
                _log.Error("Encrypt: " + source, ex);
            }

            return false;
        }

        public bool Decrypt(string encrypt, ref string decrypt)
        {
            try
            {
                if (!string.IsNullOrEmpty(encrypt))
                {
                    decrypt = CryptoByAES.DecryptStringAES(encrypt, _cryptoKey);

                    return true;
                }
            }
            catch (Exception ex)
            {
                _log.Error("Decrypt: " + encrypt, ex);
            }

            return false;
        }

        public string DecryptPassword(string encryptPassword)
        {
            var decryptPwd = encryptPassword;

            if (!string.IsNullOrEmpty(encryptPassword))
            {
                Decrypt(encryptPassword, ref decryptPwd);
            }

            return decryptPwd;
        }

        public string EncryptPassword(string password)
        {
            var encrypt = password;

            if (!string.IsNullOrEmpty(password))
            {
                Encrypt(password, ref encrypt);
            }

            return encrypt;
        }
    }
}
