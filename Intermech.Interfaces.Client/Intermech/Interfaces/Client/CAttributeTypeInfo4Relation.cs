// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.CAttributeTypeInfo4Relation
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Data;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Класс для атрибута применительно к типу связей</summary>
internal class CAttributeTypeInfo4Relation(
  MetadataInfoParentContext serviceContext,
  DataRow attr_row,
  DataRow attr4type_row) : 
  CAttributeTypeInfo4(serviceContext, attr_row, attr4type_row),
  IDBAttributeTypeInfo4Relation,
  IDBAttributeTypeInfo4,
  IDBAttributeTypeInfo
{
  public override string ObjectName
  {
    [DebuggerStepThrough] get
    {
      return $"Атрибут '{this.Name}' для типа связей {MetaDataHelper.GetRelationTypeName(this.TypeID)}";
    }
  }

  public override int TypeID => Convert.ToInt32(this.Attr4TypeRow["F_RELATION_TYPE"]);

  public Attribute4RelationTypeProperties Attribute4RelationPropertiesStructure
  {
    [DebuggerStepThrough] get
    {
      return new Attribute4RelationTypeProperties(this.AttributeID, this.TypeID, this.Required, this.ValidationRule, this.Computed, this.Formula, this.DefaultValue, this.OptimizationMode, this.IsContent, this.Options, this.Mask, this.MasterAttributeID, this.SourceAttributeID)
      {
        FieldType = this.AttributeType
      };
    }
  }
}
