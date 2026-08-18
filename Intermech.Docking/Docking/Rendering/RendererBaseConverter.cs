
// Type: Intermech.Docking.Rendering.RendererBaseConverter
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

public class RendererBaseConverter : TypeConverter
{
  public override TypeConverter.StandardValuesCollection GetStandardValues(
    ITypeDescriptorContext context)
  {
    ArrayList values = new ArrayList();
    if (context != null && context.Instance is DockContainer)
      values.Add((object) "(default)");
    values.Add((object) "Everett");
    values.Add((object) "Office 2003");
    values.Add((object) "Whidbey");
    return new TypeConverter.StandardValuesCollection((ICollection) values);
  }

  public override bool CanConvertFrom(ITypeDescriptorContext context, Type type)
  {
    return type == typeof (string) || base.CanConvertFrom(context, type);
  }

  public override object ConvertFrom(
    ITypeDescriptorContext context,
    CultureInfo cultureInfo,
    object obj)
  {
    if (obj == null)
      return (object) new WhidbeyRenderer();
    if (!(obj is string))
      return base.ConvertFrom(context, cultureInfo, obj);
    string str;
    if ((str = (string) obj) != null)
    {
      if (!(str != "Everett"))
        return (object) new EverettRenderer();
      switch (str)
      {
        case "Office 2003":
          return (object) new Office2003Renderer();
        case "Whidbey":
          return (object) new WhidbeyRenderer();
      }
    }
    return (object) null;
  }

  public override object ConvertTo(
    ITypeDescriptorContext A_0,
    CultureInfo A_1,
    object A_2,
    Type A_3)
  {
    if (A_3 == typeof (string))
    {
      switch (A_2)
      {
        case string _:
          return A_2;
        case RendererBase _:
          return (object) A_2.ToString();
        default:
          return (object) "(default)";
      }
    }
    else
      return A_3 == typeof (InstanceDescriptor) && A_2 is RendererBase ? (object) new InstanceDescriptor((MemberInfo) A_2.GetType().GetConstructor(Type.EmptyTypes), (ICollection) new object[0], true) : base.ConvertTo(A_0, A_1, A_2, A_3);
  }

  public override bool GetStandardValuesSupported(ITypeDescriptorContext context) => true;

  public override bool CanConvertTo(ITypeDescriptorContext A_0, Type A_1)
  {
    return A_1 == typeof (string) || A_1 == typeof (InstanceDescriptor) || base.CanConvertTo(A_0, A_1);
  }

  public override bool GetCreateInstanceSupported(ITypeDescriptorContext context) => true;
}
