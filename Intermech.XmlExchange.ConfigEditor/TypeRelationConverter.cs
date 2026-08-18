// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.TypeRelationConverter
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor;

internal class TypeRelationConverter : TypeConverter
{
  private Dictionary<Guid, IMSRelationType> _relationTypeDictionary = new Dictionary<Guid, IMSRelationType>();

  public TypeRelationConverter(Type type)
  {
  }

  public override object ConvertTo(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value,
    Type destinationType)
  {
    if (value == null)
      return (object) string.Empty;
    Guid result;
    if (!Guid.TryParse(value.ToString(), out result))
      return value;
    IMSRelationType relationType;
    if (this._relationTypeDictionary.TryGetValue(result, out relationType))
      return (object) relationType.Description;
    relationType = MetaDataHelper.GetRelationType(result);
    if (relationType == null)
      return value;
    this._relationTypeDictionary.Add(result, relationType);
    return (object) relationType.Description;
  }
}
