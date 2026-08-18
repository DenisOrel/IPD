
// Type: SuperTooltips.SuperTooltipInfoConverter
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Globalization;
using System.Reflection;


namespace SuperTooltips
{
    public class SuperTooltipInfoConverter : TypeConverter
    {
      public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
      {
        return destinationType == typeof (InstanceDescriptor) || base.CanConvertTo(context, destinationType);
      }

      public override object ConvertTo(
        ITypeDescriptorContext context,
        CultureInfo culture,
        object value,
        Type destinationType)
      {
        if (destinationType == (Type) null)
          throw new ArgumentNullException(nameof (destinationType));
        if (destinationType == typeof (InstanceDescriptor) && value is SuperTooltipInfo)
        {
          SuperTooltipInfo superTooltipInfo = (SuperTooltipInfo) value;
          MemberInfo constructor;
          object[] arguments;
          if (superTooltipInfo.HeaderVisible && superTooltipInfo.FooterVisible && superTooltipInfo.CustomSize.IsEmpty)
          {
            constructor = (MemberInfo) typeof (SuperTooltipInfo).GetConstructor(new Type[6]
            {
              typeof (string),
              typeof (string),
              typeof (string),
              typeof (Image),
              typeof (Image),
              typeof (TooltipColorScheme)
            });
            arguments = new object[6]
            {
              (object) superTooltipInfo.HeaderText,
              (object) superTooltipInfo.FooterText,
              (object) superTooltipInfo.BodyText,
              (object) superTooltipInfo.BodyImage,
              (object) superTooltipInfo.FooterImage,
              (object) superTooltipInfo.Color
            };
          }
          else
          {
            constructor = (MemberInfo) typeof (SuperTooltipInfo).GetConstructor(new Type[9]
            {
              typeof (string),
              typeof (string),
              typeof (string),
              typeof (Image),
              typeof (Image),
              typeof (TooltipColorScheme),
              typeof (bool),
              typeof (bool),
              typeof (Size)
            });
            arguments = new object[9]
            {
              (object) superTooltipInfo.HeaderText,
              (object) superTooltipInfo.FooterText,
              (object) superTooltipInfo.BodyText,
              (object) superTooltipInfo.BodyImage,
              (object) superTooltipInfo.FooterImage,
              (object) superTooltipInfo.Color,
              (object) superTooltipInfo.HeaderVisible,
              (object) superTooltipInfo.FooterVisible,
              (object) superTooltipInfo.CustomSize
            };
          }
          if (constructor != (MemberInfo) null)
            return (object) new InstanceDescriptor(constructor, (ICollection) arguments);
        }
        return base.ConvertTo(context, culture, value, destinationType);
      }
    }
}
