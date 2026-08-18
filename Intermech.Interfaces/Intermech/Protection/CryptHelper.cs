
// Type: Intermech.Protection.CryptHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces;
using System;
using System.Data;
using System.Security.Cryptography;


namespace Intermech.Protection
{
    /// <summary>Помощник для шифрования паролей</summary>
    public class CryptHelper
    {
      public static readonly char NoneCrypt = '0';
      public static readonly char SHA1Crypt = '1';
      public static readonly char MD5Crypt = '2';
      /// <summary>
      /// используется только для шифрования паролей DVS
      /// метод шифрования  SHA1, перед зашифрованным паролем нет префикса '1'
      /// </summary>
      public static readonly char DVSCrypt = '3';

      /// <summary>
      /// Сравнивает незашифрованный пароль psw с зашифрованным psw_hash
      /// </summary>
      /// <param name="psw"></param>
      /// <param name="psw_hash"></param>
      /// <returns></returns>
      public static bool IsPasswordEqual(string psw, string psw_hash)
      {
        if (psw_hash.Length == 0)
          psw_hash = CryptHelper.NoneCrypt.ToString();
        char cryptMethod = psw_hash[0];
        string str = CryptHelper.CryptPassword(psw, cryptMethod);
        return psw_hash == str;
      }

      /// <summary>Шифрует пароль psw методом cryptMethod</summary>
      /// <param name="psw"></param>
      /// <param name="cryptMethod"></param>
      /// <returns></returns>
      public static string CryptPassword(string psw, char cryptMethod)
      {
        if (psw == null)
          throw new KernelException("Invalid psw value (null).");
        string str;
        switch (cryptMethod)
        {
          case '0':
            str = CryptHelper.NoneCrypt.ToString() + psw;
            break;
          case '1':
          case '3':
            SHA1 shA1 = (SHA1) new SHA1CryptoServiceProvider();
            byte[] buffer1 = new byte[psw.Length * 4];
            for (int index = 0; index < psw.Length; ++index)
            {
              int int32 = Convert.ToInt32(psw[index]);
              buffer1[index * 4] = (byte) (int32 & (int) byte.MaxValue);
              buffer1[index * 4 + 1] = (byte) (int32 >> 8 & (int) byte.MaxValue);
              buffer1[index * 4 + 2] = (byte) (int32 >> 16 /*0x10*/ & (int) byte.MaxValue);
              buffer1[index * 4 + 3] = (byte) (int32 >> 24 & (int) byte.MaxValue);
            }
            str = Convert.ToBase64String(shA1.ComputeHash(buffer1));
            if (cryptMethod == '1')
            {
              str = CryptHelper.SHA1Crypt.ToString() + str;
              break;
            }
            break;
          case '2':
            MD5 md5 = (MD5) new MD5CryptoServiceProvider();
            byte[] buffer2 = new byte[psw.Length * 4];
            for (int index = 0; index < psw.Length; ++index)
            {
              int int32 = Convert.ToInt32(psw[index]);
              buffer2[index * 4] = (byte) (int32 & (int) byte.MaxValue);
              buffer2[index * 4 + 1] = (byte) (int32 >> 8 & (int) byte.MaxValue);
              buffer2[index * 4 + 2] = (byte) (int32 >> 16 /*0x10*/ & (int) byte.MaxValue);
              buffer2[index * 4 + 3] = (byte) (int32 >> 24 & (int) byte.MaxValue);
            }
            byte[] hash = md5.ComputeHash(buffer2);
            str = CryptHelper.MD5Crypt.ToString() + Convert.ToBase64String(hash);
            break;
          default:
            throw new Exception("Unknown crypto method: " + cryptMethod.ToString());
        }
        return str;
      }

      /// <summary>Проверка политики безопасности для паролей</summary>
      /// <param name="session">Сессия юзера, проверяющего пароль</param>
      /// <param name="psw">Пароль в открытом виде</param>
      /// <param name="pswHash">Хэш пароля</param>
      /// <param name="userID">Ид. юзера, чей пароль проверяют</param>
      public static DataTable ValidatePswRules(
        IUserSession session,
        string psw,
        string pswHash,
        long userID)
      {
        DataTable dataTable = (DataTable) null;
        if (!session.IsAdmin && !session.Configurations.ReadBool("KERNEL", "SECURITY", "PSW_USER", true, DBConfigMode.GlobalOnly))
          throw new PasswordModifyException();
        long num = session.Configurations.ReadInteger("KERNEL", "SECURITY", "PSW_LEN", 0L, DBConfigMode.GlobalOnly);
        if ((long) psw.Length < num)
          throw new KernelExceptionID(116, (object) num);
        if (session.Configurations.ReadBool("KERNEL", "SECURITY", "STRONG_PSW", false, DBConfigMode.GlobalOnly))
        {
          if (psw.Length < 6)
            throw new KernelExceptionID(116, (object) num);
          bool flag1 = false;
          bool flag2 = false;
          bool flag3 = psw != psw.ToUpper() && psw != psw.ToLower();
          if (flag3)
          {
            foreach (char c in psw)
            {
              if (char.IsSymbol(c) || char.IsPunctuation(c))
                flag1 = true;
              if (char.IsDigit(c))
                flag2 = true;
            }
          }
          if (!flag3 || !flag1 || !flag2)
            throw new KernelExceptionID(119);
        }
        if (session.Configurations.ReadInteger("KERNEL", "SECURITY", "PSW_MEM", 0L, DBConfigMode.GlobalOnly) > 0L)
        {
          IDBAttribute attributeByGuid = session.GetObject(session.UserID).GetAttributeByGuid(new Guid("cad00019-306c-11d8-b4e9-00304f19f545"));
          if (CryptHelper.IsPasswordEqual(psw, attributeByGuid.AsString))
            throw new KernelExceptionID(118);
          dataTable = session.Configurations.ReadSection("KERNEL", "OLD_PSW", userID);
          foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
          {
            if (row["F_VALUE"].ToString() == pswHash)
              throw new KernelExceptionID(118);
          }
        }
        return dataTable;
      }
    }
}
