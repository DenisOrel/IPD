
// Type: Intermech.Search.MeasuredValueConverter
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces;
using System;
using System.ComponentModel;
using System.Globalization;


namespace Intermech.Search
{
    public sealed class MeasuredValueConverter : TypeConverter
    {
      public override object ConvertTo(
        ITypeDescriptorContext context,
        CultureInfo culture,
        object value,
        Type destinationType)
      {
        return value is MeasuredValue && destinationType == typeof (string) ? (object) ((MeasuredValue) value).Caption : base.ConvertTo(context, culture, value, destinationType);
      }
    }
}
