// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.CAttributeTypeInfo4Object
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Data;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Класс для атрибута применительно к типу объектов</summary>
internal class CAttributeTypeInfo4Object : 
  CAttributeTypeInfo4,
  IDBAttributeTypeInfo4Object,
  IDBAttributeTypeInfo4,
  IDBAttributeTypeInfo
{
  public CAttributeTypeInfo4Object(
    MetadataInfoParentContext serviceContext,
    DataRow attr_row,
    DataRow attr4type_row)
    : base(serviceContext, attr_row, attr4type_row)
  {
    this.paramsTable[0]["F_LEVEL_ID"] = attr4type_row["F_LEVEL_ID"];
    this.paramsTable[0]["F_UNIQUE"] = attr4type_row["F_UNIQUE"];
  }

  public override string ObjectName
  {
    [DebuggerStepThrough] get
    {
      return $"Атрибут '{this.Name}' для типа объектов {MetaDataHelper.GetObjectTypeName(this.TypeID)}";
    }
  }

  public override int TypeID
  {
    [DebuggerStepThrough] get => Convert.ToInt32(this.Attr4TypeRow["F_OBJECT_TYPE"]);
  }

  public InheritModes InheritMode
  {
    [DebuggerStepThrough] get => (InheritModes) Convert.ToInt32(this.Attr4TypeRow["F_PUBLIC"]);
  }

  public Attribute4ObjectTypeProperties Attribute4ObjectPropertiesStructure
  {
    [DebuggerStepThrough] get
    {
      return new Attribute4ObjectTypeProperties(this.AttributeID, this.TypeID, this.InheritMode, this.Required, this.ValidationRule, this.Computed, this.Formula, this.UniqueMode, this.LevelID, this.DefaultValue, this.OptimizationMode, this.IsContent, this.Options, this.Mask, this.MasterAttributeID, this.SourceAttributeID)
      {
        FieldType = this.AttributeType
      };
    }
  }
}
