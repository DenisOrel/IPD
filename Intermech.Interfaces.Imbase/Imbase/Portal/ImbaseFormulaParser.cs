// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Portal.ImbaseFormulaParser
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

using Intermech.Interfaces;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Imbase.Portal;

internal class ImbaseFormulaParser : FormulaParser
{
  private FieldRecord[] _fields;
  private IUserSession _session;

  public ImbaseFormulaParser(
    IUserSession session,
    IDBAttributeType fieldItem,
    FieldRecord[] fields)
    : base((fieldItem as IDBGuid).GUID, fieldItem.AttributeType)
  {
    this._fields = fields;
    this._session = session;
  }

  protected override FormulaParser.AttributeInfo GetAttributeInfo(Guid attributeGuid)
  {
    IDBAttributeType attributeType = this._session.GetAttributeType(attributeGuid, false);
    return attributeType != null ? new FormulaParser.AttributeInfo((attributeType as IDBGuid).GUID, attributeType.AttributeType) : (FormulaParser.AttributeInfo) null;
  }

  protected override string GetFieldGuid(string field)
  {
    if (this._fields != null)
    {
      for (int index = 0; index < this._fields.Length; ++index)
      {
        if (this._fields[index].Field == field)
          return this._fields[index].GUID.ToString();
      }
    }
    return field;
  }

  protected override SortedDictionary<string, string> GetFieldsList()
  {
    SortedDictionary<string, string> fieldsList = new SortedDictionary<string, string>((IComparer<string>) new locComparer());
    for (int index = 0; index < this._fields.Length; ++index)
      fieldsList.Add(this._fields[index].Field, this._fields[index].GUID.ToString());
    for (int index = 0; index < 999; ++index)
    {
      string key = "F" + index.ToString();
      if (!fieldsList.ContainsKey(key))
        fieldsList.Add(key, string.Empty);
    }
    return fieldsList;
  }

  protected override bool IsNumberAttribute(FieldTypes dataType, Guid attrGUID)
  {
    switch (dataType)
    {
      case FieldTypes.ftInteger:
      case FieldTypes.ftDouble:
      case FieldTypes.ftExternalLink:
      case FieldTypes.ftObjectLink:
      case FieldTypes.ftMeasured:
      case FieldTypes.ftAutoInc:
      case FieldTypes.ftObjectLinkByID:
        return true;
      case FieldTypes.ftSystem:
        IDBAttributeType attributeType = this._session.GetAttributeType(attrGUID, false);
        if (attributeType != null)
          return attributeType.ValueFieldName.Equals("F_INTEGER_VALUE");
        break;
    }
    return false;
  }
}
