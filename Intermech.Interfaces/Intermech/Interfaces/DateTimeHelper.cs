
// Type: Intermech.Interfaces.DateTimeHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Globalization;


namespace Intermech.Interfaces
{
    /// <summary>Класс с функциями для работы с датой и временем</summary>
    public class DateTimeHelper
    {
      /// <summary>
      /// проверить, является ли введённая строка корректной датой
      /// (в соответствии с настройками винды)
      /// </summary>
      /// <param name="dateTime"></param>
      /// <returns></returns>
      public static bool IsDateValid(string dateTime)
      {
        DateTime result = DateTime.MinValue;
        DateTime.TryParse(dateTime, out result);
        return result != DateTime.MinValue;
      }

      /// <summary>
      /// Функция формирует строку формата даты исходя из переданной ей даты
      /// (используется для сохранения значения в бд в нужном формате)
      /// </summary>
      /// <param name="value"></param>
      /// <returns></returns>
      public static string GenerateDisplayFormat(string value)
      {
        try
        {
          DateTime dateTime = Convert.ToDateTime(value);
          if (value.Length > 17)
          {
            if (dateTime.Hour == 0 && dateTime.Minute == 0 && dateTime.Second == 0)
              return "dd.MM.yyyy";
            return dateTime.Second == 0 ? "dd.MM.yyyy HH:mm" : "dd.MM.yyyy HH:mm:ss";
          }
          return value.Length > 11 && (dateTime.Hour != 0 || dateTime.Minute != 0) ? "dd.MM.yyyy HH:mm" : "dd.MM.yyyy";
        }
        catch
        {
          return "dd.MM.yyyy";
        }
      }

      /// <summary>
      /// Функция формирует строку формата даты исходя из переданной ей даты.
      /// Формат даты берётся из настроек винды пользователя
      /// Если время не указано - d
      /// Время указано, но без секунд - f
      /// Если указаны секунды - F
      /// Используется для отображения даты пользователю
      /// </summary>
      /// <param name="value"></param>
      /// <returns></returns>
      public static string GenerateVisibleDateFormat(string value)
      {
        DateTime dateTime = Convert.ToDateTime(value);
        if (dateTime.Second != 0)
          return "F";
        return dateTime.Hour != 0 || dateTime.Minute != 0 ? "f" : "d";
      }

      /// <summary>Формирует строку из DateTime</summary>
      /// <param name="dateTime">Исходные данные</param>
      /// <returns></returns>
      public static string ToString(DateTime dateTime)
      {
        return dateTime.ToString("O", (IFormatProvider) CultureInfo.InvariantCulture);
      }

      /// <summary>Формирует DateTime из строки (без проверок)</summary>
      /// <param name="dateTime">Исходные данные</param>
      /// <returns></returns>
      public static DateTime ToDateTime(string dateTime)
      {
        return Convert.ToDateTime(dateTime, (IFormatProvider) CultureInfo.InvariantCulture);
      }

      /// <summary>
      /// Сравнивает даты на равенство, игнорируя миллисекунды и мельче
      /// </summary>
      /// <param name="dt1"></param>
      /// <param name="dt2"></param>
      /// <returns></returns>
      public static bool EqualsTruncateToSeconds(DateTime dt1, DateTime dt2)
      {
        return dt1.TruncateToSecond().Equals(dt2.TruncateToSecond());
      }
    }
}
