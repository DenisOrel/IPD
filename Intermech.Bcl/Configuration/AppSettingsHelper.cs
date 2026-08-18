
// Type: Intermech.Configuration.AppSettingsHelper
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Collections.Generic;
using System.Configuration;


namespace Intermech.Configuration
{
    public static class AppSettingsHelper
    {
      /// <summary>
      /// Содержит значения bool-параметров, конвертируемые в значение true.
      /// Массив обязательно должен быть отсортирован по возрастанию.
      /// </summary>
      private static readonly string[] trueValues = new string[5]
      {
        "1",
        "enabled",
        "on",
        "true",
        "yes"
      };

      public static bool GetBoolean(string key, bool defaultValue)
      {
        if (key == null)
          throw new ArgumentNullException(nameof (key));
        return AppSettingsHelper.ParseBoolean(ConfigurationManager.AppSettings[key], defaultValue);
      }

      public static int GetInt32(string key, int defaultValue)
      {
        if (key == null)
          throw new ArgumentNullException(nameof (key));
        return AppSettingsHelper.ParseInt32(ConfigurationManager.AppSettings[key], defaultValue);
      }

      public static string GetString(string key, string defaultValue)
      {
        if (key == null)
          throw new ArgumentNullException(nameof (key));
        return AppSettingsHelper.ParseString(ConfigurationManager.AppSettings[key], defaultValue);
      }

      public static int ParseInt32(string value, int defaultValue)
      {
        int result;
        return !int.TryParse(value, out result) ? defaultValue : result;
      }

      public static string ParseString(string value, string defaultValue)
      {
        if (value != null)
          value = value.Trim();
        return !string.IsNullOrEmpty(value) ? value : defaultValue;
      }

      public static bool ParseBoolean(string value, bool defaultValue)
      {
        return string.IsNullOrEmpty(value) ? defaultValue : Array.BinarySearch(AppSettingsHelper.trueValues, value, (IComparer<string>) StringComparer.CurrentCultureIgnoreCase) >= 0;
      }
    }
}
