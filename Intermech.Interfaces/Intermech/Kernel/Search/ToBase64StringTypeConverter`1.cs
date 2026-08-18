
// Type: Intermech.Kernel.Search.ToBase64StringTypeConverter`1
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace Intermech.Kernel.Search
{
    public class ToBase64StringTypeConverter<T> : TypeConverter
    {
      public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
      {
        return sourceType == typeof (string) || base.CanConvertFrom(context, sourceType);
      }

      public override object ConvertFrom(
        ITypeDescriptorContext context,
        CultureInfo culture,
        object value)
      {
        if (value is string)
        {
          BinaryFormatter binaryFormatter = new BinaryFormatter();
          byte[] buffer = Convert.FromBase64String((string) value);
          if (buffer != null && buffer.Length != 0)
          {
            using (MemoryStream serializationStream = new MemoryStream(buffer))
              return (object) (T) binaryFormatter.Deserialize((Stream) serializationStream);
          }
        }
        return base.ConvertFrom(context, culture, value);
      }

      public override object ConvertTo(
        ITypeDescriptorContext context,
        CultureInfo culture,
        object value,
        Type destinationType)
      {
        if (!(destinationType == typeof (string)))
          return base.ConvertTo(context, culture, value, destinationType);
        using (MemoryStream serializationStream = new MemoryStream())
        {
          new BinaryFormatter().Serialize((Stream) serializationStream, (object) (T) value);
          return (object) Convert.ToBase64String(serializationStream.ToArray());
        }
      }
    }
}
