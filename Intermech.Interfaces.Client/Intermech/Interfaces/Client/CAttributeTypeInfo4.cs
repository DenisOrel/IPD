// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.CAttributeTypeInfo4
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Data;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Базовый класс для атрибута применительно к типу</summary>
internal abstract class CAttributeTypeInfo4 : CAttributeTypeInfo
{
  protected DataRow Attr4TypeRow;

  public CAttributeTypeInfo4(
    MetadataInfoParentContext serviceContext,
    DataRow attr_row,
    DataRow attr4type_row)
    : base(serviceContext, Convert.ToInt32(attr_row["F_ATTRIBUTE_ID"]))
  {
    this._AttributeTypeID = Convert.ToInt32(attr_row["F_ATTRIBUTE_ID"]);
    this.Attr4TypeRow = attr4type_row;
    this.paramsTable[0]["F_COMPUTED"] = attr4type_row["F_COMPUTED"];
    this.paramsTable[0]["F_FORMULA"] = attr4type_row["F_FORMULA"];
    this.paramsTable[0]["F_DEFAULT_VALUE"] = attr4type_row["F_DEFAULT_VALUE"];
    this.paramsTable[0]["F_INVIEW"] = attr4type_row["F_INVIEW"];
    this.paramsTable[0]["F_CONTENT"] = attr4type_row["F_CONTENT"];
    this.paramsTable[0]["F_OPTIONS"] = attr4type_row["F_OPTIONS"];
    this.paramsTable[0]["F_MASK"] = attr4type_row["F_MASK"];
    this.paramsTable[0]["F_MASTER_ID"] = attr4type_row["F_MASTER_ID"];
    this.paramsTable[0]["F_SOURCE_ID"] = attr4type_row["F_SOURCE_ID"];
  }

  public RequiredModes Required
  {
    [DebuggerStepThrough] get => (RequiredModes) Convert.ToInt32(this.Attr4TypeRow["F_REQUIRED"]);
  }

  public override string ValidationRule
  {
    [DebuggerStepThrough] get => this.Attr4TypeRow["F_VALIDATION_RULE"].ToString();
  }

  /// <summary>Тип объекта/связи</summary>
  public abstract int TypeID { get; }
}
