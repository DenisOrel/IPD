
// Type: Intermech.Interfaces.CompareValues.IntegerComparer
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Globalization;


namespace Intermech.Interfaces.CompareValues
{
    internal sealed class IntegerComparer : Comparer<long>
    {
      protected override long ConvertTo(object value)
      {
        switch (value)
        {
          case long num:
            return num;
          case int _:
          case Decimal _:
            return Convert.ToInt64(value);
          default:
            long result;
            if (!long.TryParse(Convert.ToString(value, (IFormatProvider) CultureInfo.InvariantCulture), NumberStyles.Integer, (IFormatProvider) CultureInfo.InvariantCulture, out result))
              throw new Exception($"Не удалось привести \"{value}\" к типу Int64");
            return result;
        }
      }
    }
}
