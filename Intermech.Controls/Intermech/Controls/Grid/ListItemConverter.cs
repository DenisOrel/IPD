
// Type: Intermech.Controls.Grid.ListItemConverter
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;


namespace Intermech.Controls.Grid;

public class ListItemConverter : TypeConverter
{
  public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
  {
    return destinationType == typeof (InstanceDescriptor) || base.CanConvertTo(context, destinationType);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="context"></param>
  /// <param name="culture"></param>
  /// <param name="value"></param>
  /// <param name="destinationType"></param>
  /// <returns></returns>
  public override object ConvertTo(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value,
    Type destinationType)
  {
    if (destinationType == typeof (InstanceDescriptor) && value is ListItem)
    {
      ConstructorInfo constructor = typeof (ListItem).GetConstructor(Type.EmptyTypes);
      if (constructor != (ConstructorInfo) null)
        return (object) new InstanceDescriptor((MemberInfo) constructor, (ICollection) null, false);
    }
    return base.ConvertTo(context, culture, value, destinationType);
  }
}
