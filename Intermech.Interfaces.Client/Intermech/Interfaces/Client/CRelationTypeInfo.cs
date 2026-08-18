// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.CRelationTypeInfo
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Получение инфы о типе связей</summary>
internal class CRelationTypeInfo(MetadataInfoParentContext serviceContext, int metadataID) : 
  CAttributableTypeInfo(serviceContext, metadataID),
  IDBRelationTypeInfo,
  IDBAttributableTypeInfo
{
  protected override string DBTableName => "IMS_RELATION_TYPES";

  protected override string MetadataNotFoundMessage
  {
    get => $"Тип связей номер {this.MetadataID} не найден.";
  }

  public override string ObjectName
  {
    [DebuggerStepThrough] get => $"Тип связей '{this.Description}'";
  }

  public override IDBAttribute4TypeInfoCollection Attributes
  {
    [DebuggerStepThrough] get
    {
      if (this._Attributes == null)
        this._Attributes = (IDBAttribute4TypeInfoCollection) new CAttribute4RelationTypeInfoCollection(this.ServiceContext, this.MetadataID, false);
      return this._Attributes;
    }
  }

  public override IDBAttribute4TypeInfoCollection VisibleAttributes
  {
    get
    {
      return (IDBAttribute4TypeInfoCollection) new CAttribute4RelationTypeInfoCollection(this.ServiceContext, this.MetadataID, true);
    }
  }

  public int RelationType
  {
    [DebuggerStepThrough] get => this.MetadataID;
  }

  public string TypeName
  {
    [DebuggerStepThrough] get => this.paramsTable[0]["F_TYPE_NAME"].ToString();
  }

  public string ReverseName
  {
    [DebuggerStepThrough] get => this.paramsTable[0]["F_REVERSE_NAME"].ToString();
  }

  public string Note
  {
    [DebuggerStepThrough] get => this.paramsTable[0]["F_NOTE"].ToString();
  }

  public bool CheckoutFile
  {
    [DebuggerStepThrough] get => Convert.ToBoolean(this.paramsTable[0]["F_CHKOUTFILE"]);
  }

  public byte[] Icon
  {
    [DebuggerStepThrough] get
    {
      return this.paramsTable[0]["F_ICON"] == DBNull.Value ? new byte[0] : (byte[]) this.paramsTable[0]["F_ICON"];
    }
  }

  public bool SaveHistory
  {
    [DebuggerStepThrough] get => Convert.ToBoolean(this.paramsTable[0]["F_SAVE_HISTORY"]);
  }

  public string Description
  {
    [DebuggerStepThrough] get => this.paramsTable[0]["F_DESCRIPTION"].ToString();
  }

  public string ShortName
  {
    [DebuggerStepThrough] get => this.paramsTable[0]["F_SHORT_NAME"].ToString();
  }

  public RelationTypeOptions Options
  {
    [DebuggerStepThrough] get
    {
      return (RelationTypeOptions) Convert.ToInt32(this.paramsTable[0]["F_OPTIONS"]);
    }
  }

  public RelationTypeProperties PropertiesStructure
  {
    [DebuggerStepThrough] get
    {
      return new RelationTypeProperties(this.RelationType, this.TypeName, this.ReverseName, this.Note, this.CheckoutFile, this.SaveHistory, this.Description, this.GUID, this.SubjectAreas, this.AnyAttributes, this.ShortName, this.Options);
    }
  }

  public string SubjectAreas
  {
    [DebuggerStepThrough] get => this.paramsTable[0]["F_AREA_ID"].ToString();
  }
}
