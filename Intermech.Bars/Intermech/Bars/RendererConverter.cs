
// Type: Intermech.Bars.RendererConverter
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;


namespace Intermech.Bars
{
    internal class RendererConverter : TypeConverter
    {
      public override TypeConverter.StandardValuesCollection GetStandardValues(
        ITypeDescriptorContext A_0)
      {
        return new TypeConverter.StandardValuesCollection((ICollection) new ArrayList()
        {
          (object) "Office 2002",
          (object) "Office 2003",
          (object) "Whidbey"
        });
      }

      public override bool CanConvertFrom(ITypeDescriptorContext tdc, Type type)
      {
        return type == typeof (string) || base.CanConvertFrom(tdc, type);
      }

      public override object ConvertFrom(ITypeDescriptorContext A_0, CultureInfo cinfo, object obj)
      {
        if (!(obj is string))
          return base.ConvertFrom(A_0, cinfo, obj);
        string str;
        if ((str = (string) obj) != null)
        {
          switch (string.IsInterned(str))
          {
            case "Office 2003":
              return (object) new Office2003Renderer();
            case "Whidbey":
              return (object) new WhidbeyRenderer();
            case "Black":
              return (object) new BlackRenderer();
          }
        }
        return (object) new Office2002Renderer();
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
            case IToolBarRenderer _:
              return (object) A_2.ToString();
            default:
              return (object) "(default)";
          }
        }
        else
          return A_3 == typeof (InstanceDescriptor) && A_2 is IToolBarRenderer ? (object) new InstanceDescriptor((MemberInfo) A_2.GetType().GetConstructor(Type.EmptyTypes), (ICollection) new object[0], true) : base.ConvertTo(A_0, A_1, A_2, A_3);
      }

      public override bool GetStandardValuesExclusive(ITypeDescriptorContext A_0) => true;

      public override bool CanConvertTo(ITypeDescriptorContext A_0, Type A_1)
      {
        return A_1 == typeof (string) || A_1 == typeof (InstanceDescriptor) || base.CanConvertTo(A_0, A_1);
      }

      public override bool GetStandardValuesSupported(ITypeDescriptorContext A_0) => true;
    }
}
