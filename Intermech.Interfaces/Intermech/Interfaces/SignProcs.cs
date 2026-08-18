
// Type: Intermech.Interfaces.SignProcs
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Globalization;


namespace Intermech.Interfaces
{
    public class SignProcs
    {
      /// <summary>
      /// Перевод даты в строку для подписей.
      /// Изменение на выходе приведет к другим результатам при расчете хэшей, то есть к невалидности существующих хэшей.
      /// </summary>
      /// <param name="dateTime"></param>
      /// <returns></returns>
      public static string DateTimeToString(DateTime dateTime)
      {
        return dateTime.ToString("G", (IFormatProvider) CultureInfo.InvariantCulture);
      }
    }
}
