// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.Converters_Editors.ObjectTypeConverter
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using Intermech.AutoSelection.Client.AutoSelectionNode;
using Intermech.Interfaces;
using System;
using System.ComponentModel;
using System.Globalization;

#nullable disable
namespace Intermech.AutoSelection.Client.Converters_Editors;

public class ObjectTypeConverter : GuidConverter
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
    if (!(destinationType == typeof (string)))
      return base.ConvertTo(context, culture, value, destinationType);
    IMSObjectType imsObjectType = (IMSObjectType) null;
    Guid objTypeGuid = Guid.Empty;
    if (value is Guid guid)
      objTypeGuid = guid;
    else if (value is AS_Guid asGuid)
      objTypeGuid = asGuid.Value;
    if (objTypeGuid != Guid.Empty)
      imsObjectType = MetaDataHelper.GetObjectType(objTypeGuid);
    return imsObjectType == null ? (object) "" : (object) imsObjectType.ObjectTypeName;
  }
}
