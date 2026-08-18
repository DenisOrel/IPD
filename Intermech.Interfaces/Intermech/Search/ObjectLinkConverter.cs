
// Type: Intermech.Search.ObjectLinkConverter
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces;
using System;
using System.ComponentModel;
using System.Globalization;


namespace Intermech.Search
{
    public sealed class ObjectLinkConverter : TypeConverter
    {
      public override object ConvertTo(
        ITypeDescriptorContext context,
        CultureInfo culture,
        object value,
        Type destinationType)
      {
        if (value == null)
        {
          if (destinationType == typeof (long))
            return (object) 0L;
          if (destinationType == typeof (string))
            return (object) null;
        }
        else if (value is long objectVersionID && destinationType == typeof (string))
        {
          QuickObjectInfo quickObjectInfo;
          return objectVersionID == 0L || !Session.TryGetObjectInfo(objectVersionID, out quickObjectInfo) ? (object) null : (object) quickObjectInfo.Caption;
        }
        return base.ConvertTo(context, culture, value, destinationType);
      }
    }
}
