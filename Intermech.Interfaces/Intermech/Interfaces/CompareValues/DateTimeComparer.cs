
// Type: Intermech.Interfaces.CompareValues.DateTimeComparer
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Globalization;


namespace Intermech.Interfaces.CompareValues
{
    internal sealed class DateTimeComparer : Comparer<object>
    {
      protected override object ConvertTo(object value)
      {
        if (value is DateTime dateTime)
          return (object) dateTime;
        if (value is string)
          return value.Equals((object) Consts.CurrentDateFunction) ? (object) DateTime.Now.Date : (object) (string) value;
        DateTime result;
        if (!DateTime.TryParse(Convert.ToString(value, (IFormatProvider) CultureInfo.InvariantCulture), (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
          throw new Exception($"Не удалось привести \"{value}\" к типу DateTime");
        return (object) result;
      }
    }
}
