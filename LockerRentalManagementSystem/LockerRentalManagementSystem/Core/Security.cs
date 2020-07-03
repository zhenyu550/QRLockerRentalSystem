using System.Security.Cryptography;
using System.Text;

namespace LockerRentalManagementSystem.Core
{
    public static class Security
    {
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
