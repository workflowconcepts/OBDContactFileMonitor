using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace com.workflowconcepts.applications.filemonitor
{
    [Serializable]
    public class AESSymmetricEncryption
    {
        AesCryptoServiceProvider _csp = null;

        private String _Password = string.Empty;
        private String _Salt = string.Empty;
        private String _InitializationVector = string.Empty;

        public String Password
        {
            set { _Password = value; }
        }

        public String Salt
        {
            set { _Salt = value; }
        }

        public String InitializationVector
        {
            set { _InitializationVector = value; }
        }

        public AESSymmetricEncryption(String Password, String Salt, String InitializationVector)
        {
            _Password = Password;
            _Salt = Salt;
            _InitializationVector = InitializationVector;

            InitializeCryptoServiceProvider();
        }

        public String Encrypt(string raw)
        {
            try
            {
                ICryptoTransform e = _csp.CreateEncryptor();

                byte[] inputBuffer = Encoding.UTF8.GetBytes(raw);
                byte[] output = e.TransformFinalBlock(inputBuffer, 0, inputBuffer.Length);

                string encrypted = Convert.ToBase64String(output);

                output = null;
                inputBuffer = null;
                e = null;

                return encrypted;
            }
            catch (Exception ex)
            {
                throw new AESSymmetricEncryptionException(ex.Message, ex.StackTrace);
            }
        }

        public String Decrypt(string encrypted)
        {
            try
            {
                ICryptoTransform d = _csp.CreateDecryptor();
                byte[] output = Convert.FromBase64String(encrypted);
                byte[] decryptedOutput = d.TransformFinalBlock(output, 0, output.Length);

                string decypted = Encoding.UTF8.GetString(decryptedOutput);

                decryptedOutput = null;
                output = null;
                d = null;

                return decypted;
            }
            catch (Exception ex)
            {
                throw new AESSymmetricEncryptionException(ex.Message, ex.StackTrace);
            }
        }

        private void InitializeCryptoServiceProvider()
        {
            if (string.IsNullOrEmpty(_Password))
            {
                throw new AESSymmetricEncryptionException("_Password is empty or null", nameof(InitializeCryptoServiceProvider));
            }

            if (string.IsNullOrEmpty(_Salt))
            {
                throw new AESSymmetricEncryptionException("_Salt is empty or null", nameof(InitializeCryptoServiceProvider));
            }

            if (string.IsNullOrEmpty(_InitializationVector))
            {
                throw new AESSymmetricEncryptionException("_InitializationVector is empty or null", nameof(InitializeCryptoServiceProvider));
            }

            _csp = new AesCryptoServiceProvider();

            _csp.BlockSize = 128;
            _csp.KeySize = 256;
            _csp.Mode = CipherMode.CBC;
            _csp.Padding = PaddingMode.PKCS7;
            _csp.IV = Encoding.UTF8.GetBytes(_InitializationVector);

            var spec = new Rfc2898DeriveBytes(Encoding.UTF8.GetBytes(_Password), Encoding.UTF8.GetBytes(_Salt), 65536);
            byte[] key = spec.GetBytes(_InitializationVector.Length);
            _csp.Key = key;

            spec = null;
            key = null;
        }
    }
}
