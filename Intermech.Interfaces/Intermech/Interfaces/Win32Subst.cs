
// Type: Intermech.Interfaces.Win32Subst
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>Переписанные из Win32 функции</summary>
    public class Win32Subst
    {
      /// <summary>
      /// Converts a numeric value into a string that represents the number expressed as a size value in bytes, kilobytes, megabytes, or gigabytes, depending on the size.
      /// </summary>
      /// <param name="l">число для перевода</param>
      /// <returns>переведенная строка</returns>
      public static string StrFormatByteSize(long l)
      {
        double num = (double) l;
        string empty = string.Empty;
        string str;
        if (num < 1024.0)
        {
          str = " байт";
        }
        else
        {
          num /= 1024.0;
          if (num < 1024.0)
          {
            str = "KB";
          }
          else
          {
            num /= 1024.0;
            if (num < 1024.0)
            {
              str = "MB";
            }
            else
            {
              num /= 1024.0;
              if (num < 1024.0)
              {
                str = "GB";
              }
              else
              {
                num /= 1024.0;
                str = "TB";
              }
            }
          }
        }
        return $"{num.ToString("G4")} {str}";
      }

      /// <summary>
      /// Перевести в соответствующий формат количество подаваемых на вход байт,
      /// для строкового отображения
      /// </summary>
      /// <param name="val">Размер в байтах</param>
      /// <param name="Precision">Кол-во знаков после запятой</param>
      /// <returns>Размер в соответствующем формате</returns>
      public static string StrFormatByteSize(long val, int Precision)
      {
        long num1 = 1024 /*0x0400*/;
        long num2 = num1 * num1;
        long num3 = num1 * num2;
        long num4 = num1 * num3;
        long num5 = num1 * num4;
        if (val >= num5)
          return Math.Round((double) val / (double) num5, 1).ToString() + " PB";
        if (val >= num4)
          return Math.Round((double) val / (double) num4, 1).ToString() + " TB";
        if (val >= num3)
          return Math.Round((double) val / (double) num3, 1).ToString() + " GB";
        if (val >= num2)
          return Math.Round((double) val / (double) num2, 1).ToString() + " MB";
        return val >= num1 ? Math.Round((double) val / (double) num1, 1).ToString() + " KB" : val.ToString() + " байт";
      }
    }
}
