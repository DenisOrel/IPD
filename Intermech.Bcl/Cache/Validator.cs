
// Type: Intermech.Cache.Validator
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.Cache
{
    /// <summary>
    /// Содержит часто используемые методы проверки корректности аргументов
    /// методов.
    /// </summary>
    internal sealed class Validator
    {
      public static void CheckKey(object key)
      {
        if (key == null)
          throw new ArgumentNullException(nameof (key), Resources.GetString("E_KeyIsNull"));
      }

      public static void CheckData(object data)
      {
        if (data == null)
          throw new ArgumentNullException(nameof (data), Resources.GetString("E_DataIsNull"));
      }

      public static void CheckExpirations(IExpiration[] expirations)
      {
        if (expirations == null)
          return;
        for (int index = 0; index < expirations.Length; ++index)
        {
          if (expirations[index] == null)
            throw new ArgumentNullException("expirations[i]", Resources.GetString("E_ExpirationIsNull"));
        }
      }
    }
}
