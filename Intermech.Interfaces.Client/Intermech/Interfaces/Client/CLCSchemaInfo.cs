// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.CLCSchemaInfo
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Interfaces.LifeCycles;
using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Класс с инфой о схеме ЖЦ</summary>
internal class CLCSchemaInfo(MetadataInfoParentContext serviceContext, int metadataID) : 
  MetadataInfoObject(serviceContext, metadataID),
  IDBLCSchemaInfo
{
  protected override string DBTableName => "IMS_LC_SCHEMAS";

  protected override string MetadataNotFoundMessage
  {
    get => $"Схема ЖЦ номер {this.MetadataID} не найден.";
  }

  public override string ObjectName => $"Схема ЖЦ '{this.Name}'";

  public int SchemaID
  {
    [DebuggerStepThrough] get => this.MetadataID;
  }

  public string Name
  {
    [DebuggerStepThrough] get => this.paramsTable[0]["F_NAME"].ToString();
  }

  public string Note
  {
    [DebuggerStepThrough] get => this.paramsTable[0]["F_NOTE"].ToString();
  }

  public bool IsDefaultSchema
  {
    [DebuggerStepThrough] get => Convert.ToInt32(this.paramsTable[0]["F_DEFAULT"]) != 0;
  }

  public byte[] DrawData
  {
    [DebuggerStepThrough] get
    {
      return this.paramsTable[0]["F_DRAW_DATA"] == DBNull.Value ? new byte[0] : (byte[]) this.paramsTable[0]["F_DRAW_DATA"];
    }
  }

  public DBLCSchemaProperties SchemaProperties
  {
    [DebuggerStepThrough] get
    {
      return new DBLCSchemaProperties(this.SchemaID, this.Name, this.Note, this.GUID, this.IsDefaultSchema, this.SubjectAreas, this.Options);
    }
  }

  public string SubjectAreas
  {
    [DebuggerStepThrough] get => this.paramsTable[0]["F_AREA_ID"].ToString();
  }

  public LCSchemaOptions Options
  {
    [DebuggerStepThrough] get
    {
      return (LCSchemaOptions) Convert.ToInt32(this.paramsTable[0]["F_OPTIONS"]);
    }
  }
}
