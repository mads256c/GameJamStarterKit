
/**
* Author: Julius Bendt
* Author URI: www.juto.dk
* Company: JutoGames
* Company URI: www.juto.dk
* Copyright 2016
**/



using System.Security.Cryptography;
using System.IO;
using System;
using System.Collections;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using UnityEngine;


namespace juto.api.cryption
{
    class Crypto : MonoBehaviour
    {
        /// <summary>
        /// The password which is used when crypting.
        /// </summary>
        public string password = "k9230k23k289j";

        /// <summary>
        /// The salt which is used when crypting.
        /// </summary>
        public byte[] salt = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };


        public static string version = "1.0.1";


        #region private functions
        /// <summary>
        /// encrypts a string.
        /// </summary>
        /// <param name="text">The text to encrypt</param>
        /// <returns>TextCrypt class with infomation.</returns>
        private byte[] AES_Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
        {
            byte[] encryptedBytes = null;

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(passwordBytes, salt, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                        cs.Close();
                    }
                    encryptedBytes = ms.ToArray();
                }
            }

            return encryptedBytes;
        }

        private byte[] AES_Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
        {
            byte[] decryptedBytes = null;

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(passwordBytes, salt, 1000);
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

            return decryptedBytes;
        }

        private byte[] MD5_Hash(byte[] bytesToBeEncrypted)
        {
            MD5 md5 = MD5.Create();

            md5.ComputeHash(bytesToBeEncrypted);


            return md5.Hash;
        }

        private byte[] SHA256_Hash(byte[] bytesToBeEncrypted)
        {
            return SHA256.Create().ComputeHash(bytesToBeEncrypted);
        }

        private string AES_EncryptText(string input)
        {
            // Get the bytes of the string
            byte[] bytesToBeEncrypted = System.Text.Encoding.UTF8.GetBytes(input);
            byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);

            // Hash the password with SHA256
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

            byte[] bytesEncrypted = AES_Encrypt(bytesToBeEncrypted, passwordBytes);

            string result = System.Convert.ToBase64String(bytesEncrypted);


            return result;
        }

        #endregion

        #region Call functions

        public string EncryptText(string input)
        {
            string res = "";

            input = input.Replace(" ", "_");

            res = AES_EncryptText(input);

            return res;
        }

        public string DecryptText(string input)
        {
            string res = "";

            input = input.Replace("_", " ");
            res = AES_DecryptText(input);


            return res;
        }

        public void EncryptFile(string target)
        {
            byte[] bytesToBeEncrypted = File.ReadAllBytes(target);
            byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);

            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

            byte[] bytesEncrypted = AES_Encrypt(bytesToBeEncrypted, passwordBytes);


            File.WriteAllBytes(target, bytesEncrypted);

        }

        public void DecryptFile(string target)
        {

            byte[] bytesToBeDecrypted = File.ReadAllBytes(target);
            byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

            byte[] bytesDecrypted = AES_Decrypt(bytesToBeDecrypted, passwordBytes);
            File.WriteAllBytes(target, bytesDecrypted);

        }

        public string AES_DecryptText(string input)
        {
            try
            {
                // Get the bytes of the string
                byte[] bytesToBeDecrypted = System.Convert.FromBase64String(input);
                byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);
                passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

                byte[] bytesDecrypted = AES_Decrypt(bytesToBeDecrypted, passwordBytes);

                string result = System.Text.Encoding.UTF8.GetString(bytesDecrypted);

                return result;
            }
            catch
            {
                return input;
            }

        }

        /// <summary>
        /// Generate a random string, based on keys and length
        /// </summary>
        /// <param name="keys">Chraters used in the string. leave blank for random charaters</param>
        /// <param name="length">length of the string</param>
        /// <returns></returns>
        public string SecretKey(string[] keys,int length)
        {
            string r = "";

            using (MemoryStream ms = new MemoryStream())
            {
                

                for (int i = 0; i < length; i++)
                {
                    r += keys[UnityEngine.Random.Range(0, keys.Length)];
                }


                ms.Flush();
                ms.Close();
            }

           

            return r;
        }

        public string SecretKey(int length)
        {
            string[] keys = new string[] { "a", "b","c","d","e","f","g","h","i","j","k","l","m","n","o","p","q","r","s","t","u","v","x","y","z",
                    "A","B","C","D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","X","Y","Z","1","2","3","4","5","6","7","8","9","0"};

            return SecretKey(keys, length);
        }

        #endregion





        /// <summary>
        /// 
        /// </summary>
        /// <param name="_password">The password.</param>
        /// <param name="_salt">The salt, Needs to be atleast 12 charaters. Needs to be times 2</param>
        public Crypto(string _password = "34taefvwaf4", string _salt = "Pmy01Ks92lJT")
        {
            password = _password;


            salt = Convert.FromBase64String(_salt);
        }

    }

}


