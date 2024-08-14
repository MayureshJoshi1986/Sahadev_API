using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
using Serilog;
using SahadevUtilities.Common;

namespace SahadevUtilities.Encryption
{
    public class StringEncryption
    {
        static string _className = "SahadevUtilities.Encryption.StringEncryption";
        public static readonly string RCEDKey = "r3pHW86uWys2p4SB"; //rope 3 park HULU WALMART 8 6 usa WALMART yelp skype 2 park 4 SKYPE BESTBUY  

        #region GetCryptKey
        /// <summary>
        /// This methos is used to get key
        /// </summary>
        /// <returns>Return key</returns>
        public static string GetCryptKey()
        {
            string sReturn = string.Empty;
            try
            {
                string strCryptKey = GeneralUtility.GetSettingKeyValue("LMS_CryptKey");
                if (!string.IsNullOrEmpty(strCryptKey))
                {
                    sReturn = DecryptKey(strCryptKey, Rota.KeyType.SW);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, _className, "GetCryptKey");
            }
            return sReturn;
        }
        #endregion

        #region EncryptWithKey
        /// <summary>
        /// This method encrypts the input string 
        /// </summary>
        /// <param name="stringToEncrypt">String to encrypt</param>
        /// <param name="encryptionKey">Key to be appled for encrypting</param>
        /// <returns>Encrypted string</returns>
        public string Encrypt(string stringToEncrypt, string encryptionKey)
        {
            // Create byte array list to encrypt key
            byte[] key = { };
            byte[] IV = { 18, 52, 86, 120, 144, 171, 205, 239 };
            byte[] inputByteArray;

            try
            {
                encryptionKey = string.IsNullOrEmpty(encryptionKey) ? GetCryptKey() : encryptionKey;
                //Get key byte to be apply
                key = Encoding.UTF8.GetBytes(encryptionKey.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                // Get byte for input string 
                inputByteArray = Encoding.UTF8.GetBytes(stringToEncrypt);
                // Get memory stream
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(key, IV), CryptoStreamMode.Write);
                // Get stream of crypto
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                // Return string to base 64 format
                return Convert.ToBase64String(ms.ToArray());
            }
            catch (ArgumentNullException ecArguNull)
            {
                Log.Error(ecArguNull, _className, "Encrypt - ArgumentNullException");
                return "0";
            }
            catch (NotSupportedException ecNotSupport)
            {
                Log.Error(ecNotSupport, _className, "Encrypt - NotSupportedException");
                return "0";
            }
            catch (ArgumentOutOfRangeException ecOutofRange)
            {
                Log.Error(ecOutofRange, _className, "Encrypt - ArgumentOutOfRangeException");
                return "0";
            }
            catch (EncoderFallbackException exFallback)
            {
                Log.Error(exFallback, _className, "Encrypt - EncoderFallbackException");
                return "0";
            }
            catch (FormatException exFormate)
            {
                Log.Error(exFormate, _className, "Encrypt - FormatException");
                return "0";
            }
            catch (CryptographicException exCrypto)
            {
                Log.Error(exCrypto, _className, "Encrypt - CryptographicException");
                return "0";
            }
            catch (ArgumentException ecArgu)
            {
                Log.Error(ecArgu, _className, "Encrypt - ArgumentException");
                return "0";
            }
            catch (Exception ex)
            {
                Log.Error(ex, _className, "Encrypt");
                return "0";
            }
        }
        #endregion

        #region Encrypt
        /// <summary>
        /// This method encrypts the  given string 
        /// </summary>
        /// <param name="stringToEncrypt">String to encrypt</param>
        /// <returns>Encrypted string</returns>
        public string Encrypt(string stringToEncrypt)
        {
            // Create byte array list to encrypt key
            byte[] key = { };
            byte[] IV = { 18, 52, 86, 120, 144, 171, 205, 239 };
            byte[] inputByteArray;
            try
            {
                //Get key byte to be apply
                key = Encoding.UTF8.GetBytes("!#$b91?8".Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                // Get byte for input string 
                inputByteArray = Encoding.UTF8.GetBytes(stringToEncrypt);
                // Get memory stream
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(key, IV), CryptoStreamMode.Write);
                // Get stream of crypto
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                // Return string to base 64 format
                return Convert.ToBase64String(ms.ToArray());
            }
            catch (ArgumentNullException ecArguNull)
            {
                Log.Error(ecArguNull, _className, "Encrypt - ArgumentNullException");
                return "0";
            }
            catch (NotSupportedException ecNotSupport)
            {
                Log.Error(ecNotSupport, _className, "Encrypt - NotSupportedException");
                return "0";
            }
            catch (ArgumentOutOfRangeException ecOutofRange)
            {
                Log.Error(ecOutofRange, _className, "Encrypt - ArgumentOutOfRangeException");
                return "0";
            }
            catch (EncoderFallbackException exFallback)
            {
                Log.Error(exFallback, _className, "Encrypt - EncoderFallbackException");
                return "0";
            }
            catch (FormatException exFormate)
            {
                Log.Error(exFormate, _className, "Encrypt - FormatException");
                return "0";
            }
            catch (CryptographicException exCrypto)
            {
                Log.Error(exCrypto, _className, "Encrypt - CryptographicException");
                return "0";
            }
            catch (ArgumentException ecArgu)
            {
                Log.Error(ecArgu, _className, "Encrypt - ArgumentException");
                return "0";
            }
            catch (Exception ex)
            {
                Log.Error(ex, _className, "Encrypt");
                return "0";
            }
        }

        #endregion

        #region DecryptWithKey
        /// <summary>
        /// This method decrypts the encrypted string
        /// </summary>
        /// <param name="stringToDecrypt">String to decrypt</param>
        /// <param name="decriptionKey">Key to be apply for decrypt</param>
        /// <returns>Decrypted string</returns>
        /// <remarks> Limitation : it will work with UTF8 only </remarks>
        public string Decrypt(string stringToDecrypt, string decriptionKey)
        {
            // Create byte array list to decrypt key
            byte[] key = { };
            byte[] IV = { 18, 52, 86, 120, 144, 171, 205, 239 };
            // Get byte for input string 
            byte[] inputByteArray = new byte[stringToDecrypt.Length];

            try
            {
                decriptionKey = string.IsNullOrEmpty(decriptionKey) ? GetCryptKey() : decriptionKey;
                //Get key byte to be apply
                key = Encoding.UTF8.GetBytes(decriptionKey.Substring(0, 8));

                // Get crypto provider and stream object
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(key, IV), CryptoStreamMode.Write);

                // Get string to base 64 format
                inputByteArray = Convert.FromBase64String(stringToDecrypt);
                // Get stream of crypto for inup string
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                // Return in UTF8 format
                Encoding encoding = Encoding.UTF8;
                return encoding.GetString(ms.ToArray());
            }
            catch (ArgumentNullException ecArguNull)
            {
                Log.Error(ecArguNull, _className, "Decrypt - ArgumentNullException");
                return "0";
            }
            catch (NotSupportedException ecNotSupport)
            {
                Log.Error(ecNotSupport, _className, "Decrypt - NotSupportedException");
                return "0";
            }
            catch (ArgumentOutOfRangeException ecOutofRange)
            {
                Log.Error(ecOutofRange, _className, "Decrypt - ArgumentOutOfRangeException");
                return "0";
            }
            catch (EncoderFallbackException exFallback)
            {
                Log.Error(exFallback, _className, "Decrypt - EncoderFallbackException");
                return "0";
            }
            catch (FormatException exFormate)
            {
                Log.Error(exFormate, _className, "Decrypt - FormatException");
                return "0";
            }
            catch (CryptographicException exCrypto)
            {
                Log.Error(exCrypto, _className, "Decrypt - CryptographicException");
                return "0";
            }
            catch (ArgumentException ecArgu)
            {
                Log.Error(ecArgu, _className, "Decrypt - ArgumentException");
                return "0";
            }
            catch (Exception ex)
            {
                Log.Error(ex, _className, "Decrypt");
                return "0";
            }

        }
        #endregion

        #region Decrypt
        /// <summary>
        /// This method decrypts the string 
        /// </summary>
        /// <param name="stringToDecrypt">String to be decrypt</param>
        /// <returns>Decrypted string</returns>
        public string Decrypt(string stringToDecrypt)
        {
            // Create byte array list to decrypt key
            byte[] key = { };
            byte[] IV = { 18, 52, 86, 120, 144, 171, 205, 239 };
            // Get byte for input string 
            byte[] inputByteArray = new byte[stringToDecrypt.Length];
            try
            {
                //Get key byte to be appled
                key = Encoding.UTF8.GetBytes("!#$b91?8".Substring(0, 8));
                // Get  crypto provider
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                // Get string to base 64 format
                inputByteArray = Convert.FromBase64String(stringToDecrypt);
                // Get memory stream
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(key, IV), CryptoStreamMode.Write);
                // Get stream of crypt
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                // Return in UTF8 format
                Encoding encoding = Encoding.UTF8;
                return encoding.GetString(ms.ToArray());
            }
            catch (ArgumentNullException ecArguNull)
            {
                Log.Error(ecArguNull, _className, "Decrypt - ArgumentNullException");
                return "0";
            }
            catch (NotSupportedException ecNotSupport)
            {
                Log.Error(ecNotSupport, _className, "Decrypt - NotSupportedException");
                return "0";
            }
            catch (ArgumentOutOfRangeException ecOutofRange)
            {
                Log.Error(ecOutofRange, _className, "Decrypt - ArgumentOutOfRangeException");
                return "0";
            }
            catch (EncoderFallbackException exFallback)
            {
                Log.Error(exFallback, _className, "Decrypt - EncoderFallbackException");
                return "0";
            }
            catch (FormatException exFormate)
            {
                Log.Error(exFormate, _className, "Decrypt - FormatException");
                return "0";
            }
            catch (CryptographicException exCrypto)
            {
                Log.Error(exCrypto, _className, "Decrypt - CryptographicException");
                return "0";
            }
            catch (ArgumentException ecArgu)
            {
                Log.Error(ecArgu, _className, "Decrypt - ArgumentException");
                return "0";
            }
            catch (Exception ex)
            {
                Log.Error(ex, _className, "Decrypt");
                return "0";
            }
        }
        #endregion

        #region EncryptKey
        /// <summary>
        /// Encrypt the given plain string for alphabets and numbers
        /// </summary>
        /// <param name="keyValues">input string to encrypt</param>
        /// <returns>returns encrypted string</returns>
        public static string EncryptKey(string keyValues, Rota.KeyType type)
        {
            string sReturn = string.Empty;
            sReturn = Rota.Encrypt(keyValues, type);
            return sReturn;
        }
        #endregion

        #region DecryptKey
        /// <summary>
        /// Decrypt the given encrypted string for alphabets and numbers
        /// </summary>
        /// <param name="keyValues">input string to decrypt</param>
        /// <returns>returns decrypted string</returns>
        public static string DecryptKey(string keyValues, Rota.KeyType type)
        {
            string sReturn = string.Empty;
            sReturn = Rota.Decrypt(keyValues, type);
            return sReturn;
        }
        #endregion
    }
}
