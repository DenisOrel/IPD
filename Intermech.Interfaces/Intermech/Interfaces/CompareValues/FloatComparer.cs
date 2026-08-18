
// Type: Intermech.Interfaces.CompareValues.FloatComparer
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Globalization;


namespace Intermech.Interfaces.CompareValues
{
    internal sealed class FloatComparer : Comparer<double>
    {
      protected override double ConvertTo(object value)
      {
        if (value is double num)
          return num;
        if (value is Decimal)
          return Convert.ToDouble(value);
        double result;
        if (!double.TryParse(Convert.ToString(value, (IFormatProvider) CultureInfo.InvariantCulture), NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture, out result))
          throw new Exception($"Не удалось привести \"{value}\" к типу Double");
        return result;
      }
    }
}
