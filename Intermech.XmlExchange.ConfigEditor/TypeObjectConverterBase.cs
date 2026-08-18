// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.TypeObjectConverterBase
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

internal class TypeObjectConverterBase : TypeConverter
{
  private Dictionary<Guid, IMSObjectType> _objectTypeDictionary = new Dictionary<Guid, IMSObjectType>();

  public TypeObjectConverterBase(Type type)
  {
  }

  public TypeObjectConverterBase()
  {
  }

  public override object ConvertTo(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value,
    Type destinationType)
  {
    if (value.ToString() == Guid.Empty.ToString())
      return (object) string.Empty;
    Guid result;
    if (!Guid.TryParse(value.ToString(), out result))
      return value;
    IMSObjectType objectType;
    if (this._objectTypeDictionary.TryGetValue(result, out objectType))
      return (object) objectType.ObjectTypeName;
    objectType = MetaDataHelper.GetObjectType(result);
    if (objectType == null)
      return (object) value.ToString();
    this._objectTypeDictionary.Add(result, objectType);
    return (object) objectType.ObjectTypeName;
  }
}
