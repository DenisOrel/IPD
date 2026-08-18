
// Type: Intermech.ButtonsPanel.PanelButtonConverter
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;


namespace Intermech.ButtonsPanel
{
    internal class PanelButtonConverter : TypeConverter
    {
      public override bool CanConvertTo(ITypeDescriptorContext context, Type destType)
      {
        return destType == typeof (InstanceDescriptor) || base.CanConvertTo(context, destType);
      }

      public override object ConvertTo(
        ITypeDescriptorContext context,
        CultureInfo culture,
        object value,
        Type destType)
      {
        return destType == typeof (InstanceDescriptor) ? (object) new InstanceDescriptor((MemberInfo) typeof (PanelButton).GetConstructor(Type.EmptyTypes), (ICollection) null, false) : base.ConvertTo(context, culture, value, destType);
      }
    }
}
