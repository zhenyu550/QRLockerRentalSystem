/// <summary>
/// The class containing the Security functions, including encryption, decryption and hashing.
/// </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace LockerDoorControlConsole.Core
{
    class Security
    {
        //  Hash the provided String in SHA256 format
        public static string SHA256Hash(string str)
        {
            using (SHA256 hash = SHA256.Create())
            {
                StringBuilder builder = new StringBuilder();
                foreach (byte b in hash.ComputeHash(Encoding.UTF8.GetBytes(str)))
                {
                    builder.Append(b.ToString("x2").ToLower());
                }
                return builder.ToString();
            }
        }

        // Encrypt and Decrypt the provided String in MD5 format using the provided Key
        public static string EncryptDecrypt(string szPlainText, int szEncryptionKey)
        {
            StringBuilder szInputStringBuild = new StringBuilder(szPlainText);
            StringBuilder szOutStringBuild = new StringBuilder(szPlainText.Length);
            char Textch;
            for (int iCount = 0; iCount < szPlainText.Length; iCount++)
            {
                Textch = szInputStringBuild[iCount];
                Textch = (char)(Textch ^ szEncryptionKey);
                szOutStringBuild.Append(Textch);
            }
            return szOutStringBuild.ToString();
        }
    }

}