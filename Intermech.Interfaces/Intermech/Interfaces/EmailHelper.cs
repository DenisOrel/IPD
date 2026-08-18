
// Type: Intermech.Interfaces.EmailHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Diagnostics;
using System.Text.RegularExpressions;


namespace Intermech.Interfaces
{
    public class EmailHelper
    {
      /// <summary>Проверка e-mail на правильность.</summary>
      public static bool IsEmail([NotNull] string emailAddress)
      {
        return new Regex("^(([^<>()[\\]\\\\.,;:\\s@\\\"]+(\\.[^<>()[\\]\\\\.,;:\\s@\\\"]+)*)|(\\\".+\\\"))@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\])|(([a-zA-Z\\-0-9]+\\.)+[a-zA-Z]{2,}))$").IsMatch(emailAddress);
      }

      /// <summary>Получить адрес из произвольной строки.</summary>
      [NotNull]
      public static string GetEmail([NotNull] string customString)
      {
        return new Regex("(([^<>()[\\]\\\\.,;:\\s@\\\"]+(\\.[^<>()[\\]\\\\.,;:\\s@\\\"]+)*)|(\\\".+\\\"))@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\])|(([a-zA-Z\\-0-9]+\\.)+[a-zA-Z]{2,}))").Match(customString).Value;
      }
    }
}
