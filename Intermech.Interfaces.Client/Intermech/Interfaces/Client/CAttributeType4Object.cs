// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.CAttributeType4Object
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Data;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.Client;

internal class CAttributeType4Object(ClientSession session, DataRow row) : 
  CAttributeType4Category(session, row, "F_OBJECT_TYPE"),
  IDBAttributeType4Object,
  IDBAttributeType4,
  IDBAttributeType
{
  protected override IDBAttributeType4 attribute4
  {
    [DebuggerStepThrough] get
    {
      return this._clientSession.Session.GetObjectType(this.typeID, true).Attributes.GetAttributeByID(this.attributeID, true);
    }
  }

  public int[] GetRelatedFormulaAttributes()
  {
    this._clientSession.Guard.ValidateCall();
    DataRow[] dataRowArray = this._clientSession.ClientCache.GetTable("IMS_FORMULA_ATTRS").Select(string.Format("F_FORMULA_ID = {0} AND F_OBJECT_TYPE = {1} AND F_RELATION_TYPE = -1 AND F_MODE_ID = " + Consts.Attribute4Formula.ToString(), (object) this.attributeID, (object) this.typeID));
    int[] formulaAttributes = new int[dataRowArray.Length];
    for (int index = 0; index < dataRowArray.Length; ++index)
      formulaAttributes[index] = Convert.ToInt32(dataRowArray[index]["F_ATTRIBUTE_ID"]);
    return formulaAttributes;
  }

  public InheritModes InheritMode
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return (InheritModes) Convert.ToInt32(this.paramsTable[0]["F_PUBLIC"]);
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      IDBAttributeType4Object attribute4 = this.attribute4 as IDBAttributeType4Object;
      if (attribute4.InheritMode == value)
        return;
      attribute4.InheritMode = value;
      this.paramsTable[0]["F_PUBLIC"] = (object) Convert.ToInt32((object) value);
      this.ReloadCache();
    }
  }

  public Attribute4ObjectTypeProperties Attribute4ObjectPropertiesStructure
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return new Attribute4ObjectTypeProperties(this.AttributeID, this.typeID, this.InheritMode, this.Required, this.ValidationRule, this.Computed, this.Formula, this.UniqueMode, this.LevelID, this.DefaultValue, this.OptimizationMode, this.IsContent, this.Options, this.Mask, this.MasterAttributeID, this.SourceAttributeID)
      {
        FieldType = this.AttributeType
      };
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      IDBAttributeType4Object attribute4 = this.attribute4 as IDBAttributeType4Object;
      if (attribute4.Attribute4ObjectPropertiesStructure.Equals((object) value))
        return;
      attribute4.Attribute4ObjectPropertiesStructure = value;
      this.ReloadCache();
    }
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
      IDBAttributeType4Object attribute4 = this.attribute4 as IDBAttributeType4Object;
      if (attribute4.Required == value)
        return;
      attribute4.Required = value;
      this.paramsTable[0]["F_REQUIRED"] = (object) Convert.ToInt32((object) value);
      this.ReloadCache();
    }
  }
}
