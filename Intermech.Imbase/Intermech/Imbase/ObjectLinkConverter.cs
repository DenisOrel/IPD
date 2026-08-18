// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.ObjectLinkConverter
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Interfaces;
using Intermech.Localization;
using System;
using System.ComponentModel;
using System.Globalization;

#nullable disable
namespace Intermech.Imbase;

internal class ObjectLinkConverter : TypeConverter
{
  public override object ConvertFrom(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value)
  {
    return value;
  }

  public override object ConvertTo(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value,
    Type destinationType)
  {
    if (value == null || value == DBNull.Value || string.IsNullOrEmpty(value.ToString()) || context == null)
      return (object) null;
    string str1 = value.ToString();
    if (!GuidHelper.IsGuid(str1))
      return value;
    Guid objectGUID = new Guid(str1);
    if (objectGUID == Guid.Empty)
      return (object) null;
    if (!(context.Instance is StructureEditorPropGridDescriptor))
      return value;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(objectGUID);
      if (!objectInfo.Empty && !string.IsNullOrEmpty(objectInfo.Caption))
        return (object) objectInfo.Caption;
      IDBObject dbObject = sessionKeeper.Session.GetObject(objectGUID, false);
      string str2 = string.Empty;
      if (dbObject != null)
        str2 = dbObject.Caption;
      return (object) $"{LocalizationHolder.rm.GetString("Client.Core_1132")} №{str2}";
    }
  }
}
