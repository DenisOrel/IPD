
// Type: Intermech.Bars.ToolbarItemBaseConverter
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
    internal class ToolbarItemBaseConverter : TypeConverter
    {
      public override bool CanConvertTo(ITypeDescriptorContext A_0, Type A_1)
      {
        return A_1 == typeof (InstanceDescriptor) || base.CanConvertTo(A_0, A_1);
      }

      public override object ConvertTo(
        ITypeDescriptorContext A_0,
        CultureInfo A_1,
        object A_2,
        Type A_3)
      {
        return A_3 == typeof (InstanceDescriptor) ? (object) new InstanceDescriptor((MemberInfo) A_2.GetType().GetConstructor(Type.EmptyTypes), (ICollection) null, false) : base.ConvertTo(A_0, A_1, A_2, A_3);
      }
    }
}
