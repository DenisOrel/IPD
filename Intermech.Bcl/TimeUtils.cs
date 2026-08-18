
// Type: Intermech.TimeUtils
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech
{
    /// <summary>
    /// Содержит различные утилиты для типов DateTime и TimeSpan.
    /// </summary>
    public static class TimeUtils
    {
      /// <summary>
      /// Возвращает новое значение, у которого отброшены миллисекунды. Этот метод часто используется при работе с временем модификации файлов.
      /// </summary>
      /// <param name="value">Исходное значение</param>
      /// <returns>Значение без миллисекунд</returns>
      public static DateTime TruncateToSecond(this DateTime value)
      {
        long num = value.Ticks % 10000000L;
        return num != 0L ? value - TimeSpan.FromTicks(num) : value;
      }

      /// <summary>
      /// Возвращает новое значение, у которого отброшены микросекунды.
      /// </summary>
      /// <param name="value">Исходное значение</param>
      /// <returns>Значение без микросекунд</returns>
      public static DateTime TruncateToMillisecond(this DateTime value)
      {
        long num = value.Ticks % 10000L;
        return num != 0L ? value - TimeSpan.FromTicks(num) : value;
      }
    }
}
