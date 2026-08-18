
// Type: Intermech.Interfaces.Cryptor
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;


namespace Intermech.Interfaces
{
    /// <summary>Summary description for Cryptor.</summary>
    public class Cryptor
    {
      private static byte[] Encrypt(byte[] clearData, byte[] Key, byte[] IV)
      {
        using (MemoryStream memoryStream = new MemoryStream())
        {
          Rijndael rijndael = Rijndael.Create();
          rijndael.Key = Key;
          rijndael.IV = IV;
          CryptoStream cryptoStream = new CryptoStream((Stream) memoryStream, rijndael.CreateEncryptor(), CryptoStreamMode.Write);
          cryptoStream.Write(clearData, 0, clearData.Length);
          cryptoStream.Close();
          return memoryStream.ToArray();
        }
      }

      public static byte[] EncryptEx(string clearText, string Password)
      {
        byte[] bytes1 = Encoding.Unicode.GetBytes(clearText);
        PasswordDeriveBytes passwordDeriveBytes = new PasswordDeriveBytes(Password, new Guid("cad00019-306c-11d8-b4e9-00304f19f545").ToByteArray());
        byte[] bytes2 = passwordDeriveBytes.GetBytes(32 /*0x20*/);
        byte[] bytes3 = passwordDeriveBytes.GetBytes(16 /*0x10*/);
        return Cryptor.Encrypt(bytes1, bytes2, bytes3);
      }

      public static string Encrypt(string clearText, string Password)
      {
        return Convert.ToBase64String(Cryptor.EncryptEx(clearText, Password));
      }

      private static byte[] Decrypt(byte[] cipherData, byte[] Key, byte[] IV)
      {
        using (MemoryStream memoryStream = new MemoryStream())
        {
          Rijndael rijndael = Rijndael.Create();
          rijndael.Key = Key;
          rijndael.IV = IV;
          CryptoStream cryptoStream = new CryptoStream((Stream) memoryStream, rijndael.CreateDecryptor(), CryptoStreamMode.Write);
          cryptoStream.Write(cipherData, 0, cipherData.Length);
          cryptoStream.Close();
          return memoryStream.ToArray();
        }
      }

      public static string Decrypt(byte[] cipherBytes, string Password)
      {
        PasswordDeriveBytes passwordDeriveBytes = new PasswordDeriveBytes(Password, new Guid("cad00019-306c-11d8-b4e9-00304f19f545").ToByteArray());
        return Encoding.Unicode.GetString(Cryptor.Decrypt(cipherBytes, passwordDeriveBytes.GetBytes(32 /*0x20*/), passwordDeriveBytes.GetBytes(16 /*0x10*/)));
      }

      public static string Decrypt(string cipherText, string Password)
      {
        return Cryptor.Decrypt(Convert.FromBase64String(cipherText), Password);
      }
    }
}
