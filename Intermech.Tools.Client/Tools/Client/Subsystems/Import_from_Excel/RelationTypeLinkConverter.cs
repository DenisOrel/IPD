// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.Subsystems.Import_from_Excel.RelationTypeLinkConverter
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Interfaces;
using Intermech.Localization;
using System;
using System.ComponentModel;
using System.Globalization;

#nullable disable
namespace Intermech.Tools.Client.Subsystems.Import_from_Excel;

public sealed class RelationTypeLinkConverter : TypeConverter
{
  public override object ConvertTo(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value,
    Type destinationType)
  {
    if (!(value is int relTypeID) || !(destinationType == typeof (string)))
      return base.ConvertTo(context, culture, value, destinationType);
    IMSRelationType relationType = MetaDataHelper.GetRelationType(relTypeID);
    return relationType == null ? (object) LocalizationHolder.rm.GetString("Tools.Client_275") : (object) relationType.Description;
  }
}
