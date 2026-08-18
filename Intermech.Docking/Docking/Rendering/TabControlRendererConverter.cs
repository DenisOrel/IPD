
// Type: Intermech.Docking.Rendering.TabControlRendererConverter
// Assembly: Intermech.Docking, Version=4.0.25.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F97F850-2D29-46D1-A3D7-6B2A02E86D46
:\IPS\Client\Intermech.Docking.dll

using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;


namespace Intermech.Docking.Rendering;

internal class TabControlRendererConverter : ExpandableObjectConverter
{
  public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
  {
    return sourceType == typeof (string) || base.CanConvertFrom(context, sourceType);
  }

  public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
  {
    return destinationType == typeof (string) || destinationType == typeof (InstanceDescriptor) || base.CanConvertTo(context, destinationType);
  }

  public override object ConvertFrom(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value)
  {
    if (!(value is string))
      return base.ConvertFrom(context, culture, value);
    if ((string) value != null)
    {
      switch ((string) value)
      {
        case "Tab":
          return (object) new TabControlRenderer();
        case "SmallTab":
          return (object) new SmallTabControlRenderer();
      }
    }
    return (object) null;
  }

  public override object ConvertTo(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value,
    Type destinationType)
  {
    return destinationType == typeof (string) ? (value is string ? value : (object) value.ToString()) : (destinationType == typeof (InstanceDescriptor) ? (object) new InstanceDescriptor((MemberInfo) value.GetType().GetConstructor(Type.EmptyTypes), (ICollection) new object[0], true) : base.ConvertTo(context, culture, value, destinationType));
  }

  public override TypeConverter.StandardValuesCollection GetStandardValues(
    ITypeDescriptorContext context)
  {
    return new TypeConverter.StandardValuesCollection((ICollection) new ArrayList()
    {
      (object) "Tab",
      (object) "SmallTab"
    });
  }

  public override bool GetStandardValuesExclusive(ITypeDescriptorContext context) => true;

  public override bool GetStandardValuesSupported(ITypeDescriptorContext context) => true;
}
