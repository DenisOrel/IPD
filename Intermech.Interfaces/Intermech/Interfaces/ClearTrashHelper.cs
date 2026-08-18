
// Type: Intermech.Interfaces.ClearTrashHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>Статические функции для процедуры очистки мусора.</summary>
    public class ClearTrashHelper
    {
      /// <summary>Параметр "Имя компьютера" в конфигурации</summary>
      public static string COMPUTER_NAME = nameof (COMPUTER_NAME);
      /// <summary>Параметр "Режим очистки" в конфигурации</summary>
      public static string CLEARING_MODE = nameof (CLEARING_MODE);
      /// <summary>Параметр "Время очистки" в конфигурации</summary>
      public static string MODE_TIME = nameof (MODE_TIME);

      /// <summary>Получить "Режим очистки" по названию</summary>
      /// <param name="name"></param>
      /// <returns></returns>
      public static ClearingMode GetClearingMode(string name)
      {
        ClearingMode[] values = (ClearingMode[]) Enum.GetValues(typeof (ClearingMode));
        for (int index = 0; index < values.Length; ++index)
        {
          if (values[index].ToString() == name)
            return values[index];
        }
        return ClearingMode.SeveralPerWeek;
      }

      /// <summary>Получить "Время очистки" из строки</summary>
      /// <param name="val"></param>
      /// <returns></returns>
      public static TimeTableValue GetTimeTableValue(string val)
      {
        int length = val.IndexOf('=', 0);
        if (length > 0)
        {
          try
          {
            string day = val.Substring(0, length);
            string str = val.Substring(length + 1, val.Length - length - 1);
            if (day != string.Empty)
            {
              if (str != string.Empty)
                return new TimeTableValue(day, Convert.ToDateTime(str));
            }
          }
          catch
          {
            return (TimeTableValue) null;
          }
        }
        return (TimeTableValue) null;
      }
    }
}
