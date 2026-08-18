// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.CAttributeType4Relation
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Data;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.Client;

internal class CAttributeType4Relation(ClientSession session, DataRow row) : 
  CAttributeType4Category(session, row, "F_RELATION_TYPE"),
  IDBAttributeType4Relation,
  IDBAttributeType4,
  IDBAttributeType
{
  protected override IDBAttributeType4 attribute4
  {
    [DebuggerStepThrough] get
    {
      return this._clientSession.Session.GetRelationType(this.typeID).Attributes.GetAttributeByID(this.attributeID);
    }
  }

  public override UniqueValueModes UniqueMode
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return UniqueValueModes.NotUnique;
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      base.UniqueMode = value;
    }
  }

  public int[] GetRelatedFormulaAttributes()
  {
    this._clientSession.Guard.ValidateCall();
    DataRow[] dataRowArray = this._clientSession.ClientCache.GetTable("IMS_FORMULA_ATTRS").Select(string.Format("F_FORMULA_ID = {0} AND F_OBJECT_TYPE = -1 AND F_RELATION_TYPE = {1} AND F_MODE_ID = " + Consts.Attribute4Formula.ToString(), (object) this.attributeID, (object) this.typeID));
    int[] formulaAttributes = new int[dataRowArray.Length];
    for (int index = 0; index < dataRowArray.Length; ++index)
      formulaAttributes[index] = Convert.ToInt32(dataRowArray[index]["F_ATTRIBUTE_ID"]);
    return formulaAttributes;
  }

  public RequiredModes Required
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return (RequiredModes) Convert.ToInt32(this.paramsTable[0]["F_REQUIRED"]);
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      IDBAttributeType4Relation attribute4 = this.attribute4 as IDBAttributeType4Relation;
      if (attribute4.Required == value)
        return;
      attribute4.Required = value;
      this.paramsTable[0]["F_REQUIRED"] = (object) Convert.ToInt32((object) value);
      this.ReloadCache();
    }
  }

  public Attribute4RelationTypeProperties Attribute4RelationPropertiesStructure
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return new Attribute4RelationTypeProperties(this.AttributeID, this.typeID, this.Required, this.ValidationRule, this.Computed, this.Formula, this.DefaultValue, this.OptimizationMode, this.IsContent, this.Options, this.Mask, this.MasterAttributeID, this.SourceAttributeID)
      {
        FieldType = this.AttributeType
      };
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      IDBAttributeType4Relation attribute4 = this.attribute4 as IDBAttributeType4Relation;
      if (attribute4.Attribute4RelationPropertiesStructure.Equals((object) value))
        return;
      attribute4.Attribute4RelationPropertiesStructure = value;
      this.ReloadCache();
    }
  }
}
