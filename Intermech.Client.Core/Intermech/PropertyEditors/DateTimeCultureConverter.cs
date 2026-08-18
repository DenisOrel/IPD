
// Type: Intermech.PropertyEditors.DateTimeCultureConverter
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Globalization;


namespace Intermech.PropertyEditors;

public static class DateTimeCultureConverter
{
  public static string ConvertUniversalDateTimeStringToCurrentDateTimeString(string lDateTime)
  {
    DateTime result;
    if (!string.IsNullOrEmpty(lDateTime) && DateTime.TryParse(lDateTime, (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
      lDateTime = result.ToString();
    return lDateTime;
  }

  public static object ConvertUniversalDateTimeStringToCurrentDateTime(string lDateTime)
  {
    object currentDateTime = (object) lDateTime;
    DateTime result;
    if (!string.IsNullOrEmpty(lDateTime) && DateTime.TryParse(lDateTime, (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
      currentDateTime = (object) result;
    return currentDateTime;
  }
}
