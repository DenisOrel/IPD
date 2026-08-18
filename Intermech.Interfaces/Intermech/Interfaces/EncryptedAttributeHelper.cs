
// Type: Intermech.Interfaces.EncryptedAttributeHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Protection;
using System;


namespace Intermech.Interfaces
{
    /// <summary>Хелпер для работы с паролями</summary>
    public class EncryptedAttributeHelper
    {
      /// <summary>
      /// Проверяет пароль на сложность, если этого требуют настройки
      /// </summary>
      /// <param name="session">Сессия пользователя</param>
      /// <param name="psw">Пароль</param>
      public static void ValidateComplexPassword(IUserSession session, string psw)
      {
        long num1 = session.Configurations.ReadInteger("KERNEL", "SECURITY", "PSW_LEN", 0L, DBConfigMode.GlobalOnly);
        if (!session.Configurations.ReadBool("KERNEL", "SECURITY", "STRONG_PSW", false, DBConfigMode.GlobalOnly))
          return;
        if (psw.Length < 6)
          throw new KernelExceptionID(116, (object) num1);
        long num2 = 0;
        if (psw != psw.ToUpper() || psw != psw.ToLower())
          ++num2;
        foreach (char c in psw)
        {
          if (!char.IsLetterOrDigit(c))
          {
            ++num2;
            break;
          }
        }
        if (num2 < 2L)
          throw new KernelExceptionID(119);
      }

      /// <summary>
      /// Возвращает хэш пароля, используя текущий метод хэширования
      /// </summary>
      /// <param name="session">Сессия</param>
      /// <param name="psw">Пароль</param>
      /// <returns></returns>
      public static string GetPasswordHash(IUserSession session, string psw)
      {
        char cryptMethod = Convert.ToChar(session.Configurations.ReadString("KERNEL", "SECURITY", "CRYPTO_METHOD", Convert.ToString(CryptHelper.SHA1Crypt), DBConfigMode.GlobalOnly));
        return CryptHelper.CryptPassword(psw, cryptMethod);
      }
    }
}
