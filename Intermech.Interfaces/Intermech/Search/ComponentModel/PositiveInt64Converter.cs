
// Type: Intermech.Search.ComponentModel.PositiveInt64Converter
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.ComponentModel;
using System.Globalization;


namespace Intermech.Search.ComponentModel
{
    public sealed class PositiveInt64Converter : Int64Converter
    {
      public override object ConvertFrom(
        ITypeDescriptorContext context,
        CultureInfo culture,
        object value)
      {
        long num = (long) base.ConvertFrom(context, culture, value);
        return num > 0L ? (object) num : throw new Exception("Значение долно быть положительным");
      }
    }
}
