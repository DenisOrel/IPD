// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.Converters_Editors.AttributeTypeConverter
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using Intermech.Interfaces;
using System;
using System.ComponentModel;
using System.Globalization;

#nullable disable
namespace Intermech.AutoSelection.Client.Converters_Editors;

public class AttributeTypeConverter : GuidConverter
{
  public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
  {
    return !(sourceType == typeof (string)) && base.CanConvertFrom(context, sourceType);
  }

  public override object ConvertTo(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value,
    Type destinationType)
  {
    return destinationType == typeof (string) && value is Guid attrTypeGuid ? (object) (attrTypeGuid != Guid.Empty ? MetaDataHelper.GetAttributeType(attrTypeGuid) : (IMSAttributeType) null)?.Name ?? (object) "" : base.ConvertTo(context, culture, value, destinationType);
  }
}
