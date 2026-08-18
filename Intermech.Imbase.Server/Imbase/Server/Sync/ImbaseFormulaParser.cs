// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.Sync.ImbaseFormulaParser
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using Intermech.Imbase.Server.Sync.Records;
using Intermech.ImpExp.Interface;
using Intermech.Interfaces;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Imbase.Server.Sync;

internal class ImbaseFormulaParser : FormulaParser
{
  private FieldRecord[] _fields;
  private IUserSession _session;

  public ImbaseFormulaParser(
    IUserSession session,
    IDBAttributeType fieldItem,
    FieldRecord[] fields)
    : base(fieldItem.GUID, fieldItem.AttributeType)
  {
    this._fields = fields;
    this._session = session;
  }

  protected override FormulaParser.AttributeInfo GetAttributeInfo(Guid attributeGuid)
  {
    IDBAttributeType attributeType = this._session.GetAttributeType(attributeGuid, false);
    return attributeType == null ? (FormulaParser.AttributeInfo) null : new FormulaParser.AttributeInfo(attributeType.GUID, attributeType.AttributeType);
  }

  protected override string GetFieldGuid(string field)
  {
    if (this._fields == null)
      return field;
    foreach (FieldRecord field1 in this._fields)
    {
      if (field1.Field == field)
        return field1.GUID.ToString();
    }
    return field;
  }

  protected override SortedDictionary<string, string> GetFieldsList()
  {
    SortedDictionary<string, string> fieldsList = new SortedDictionary<string, string>((IComparer<string>) new ImbaseFormulaParser.LocComparer());
    foreach (FieldRecord field in this._fields)
      fieldsList.Add(field.Field, field.GUID.ToString());
    for (int index = 0; index < 999; ++index)
    {
      string key = $"F{index}";
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
        return true;
      case FieldTypes.ftSystem:
        IDBAttributeType attributeType = this._session.GetAttributeType(attrGUID, false);
        if (attributeType != null)
          return attributeType.ValueFieldName.Equals("F_INTEGER_VALUE");
        break;
    }
    return false;
  }

  internal class LocComparer : IComparer<string>
  {
    public int Compare(string x, string y) => y.CompareTo(x);
  }
}
