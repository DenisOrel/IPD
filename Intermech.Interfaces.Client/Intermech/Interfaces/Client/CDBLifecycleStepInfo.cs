// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.CDBLifecycleStepInfo
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Data;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Получатель инфы о шаге ЖЦ</summary>
internal class CDBLifecycleStepInfo(MetadataInfoParentContext serviceContext, int metadataID) : 
  MetadataInfoObject(serviceContext, metadataID),
  IDBLifecycleStepInfo
{
  protected override string DBTableName => "IMS_LC_STEPS";

  protected override string MetadataNotFoundMessage
  {
    get => $"Шаг жизненного цикла номер {this.MetadataID} не найден.";
  }

  public override string ObjectName => $"Шаг жизненного цикла '{this.LCStep}'";

  public int LCStep
  {
    [DebuggerStepThrough] get => this.MetadataID;
  }

  public string LCName
  {
    [DebuggerStepThrough] get => this.paramsTable[0]["F_LC_NAME"].ToString();
  }

  public string Note
  {
    [DebuggerStepThrough] get
    {
      object obj = this.paramsTable[0]["F_NOTE"];
      return obj == DBNull.Value ? string.Empty : obj.ToString();
    }
  }

  public int ObjectTypeID
  {
    [DebuggerStepThrough] get => this._ObjectTypeID;
  }

  public LCAccessTypes AccessType
  {
    [DebuggerStepThrough] get
    {
      return (LCAccessTypes) Convert.ToInt32(this.paramsTable[0]["F_ACCESS_TYPE"]);
    }
  }

  public int LevelID
  {
    [DebuggerStepThrough] get => Convert.ToInt32(this.paramsTable[0]["F_LEVEL_ID"]);
  }

  public DBLifecycleStepProperties Properties
  {
    [DebuggerStepThrough] get
    {
      return new DBLifecycleStepProperties(this.LCStep, this.ObjectTypeID, this.LCName, this.Note, this.AccessType, this.LevelID, this.ObjectModifyMode, this.GUID, this.IsFirstStep, this.Options);
    }
  }

  public ObjectModifyModes ObjectModifyMode
  {
    [DebuggerStepThrough] get
    {
      return (ObjectModifyModes) Convert.ToInt32(this.paramsTable[0]["F_MODIFY_MODE"]);
    }
  }

  public bool IsFirstStep
  {
    [DebuggerStepThrough] get => Convert.ToInt32(this.paramsTable[0]["F_FIRST"]) != 0;
  }

  public bool IsDeleted
  {
    [DebuggerStepThrough] get => Convert.ToInt32(this.paramsTable[0]["F_DELETED"]) != 0;
  }

  public int SchemaID
  {
    [DebuggerStepThrough] get => Convert.ToInt32(this.paramsTable[0]["F_SCHEMA_ID"]);
  }

  public LCStepOptions Options
  {
    [DebuggerStepThrough] get => (LCStepOptions) Convert.ToInt32(this.paramsTable[0]["F_OPTIONS"]);
  }

  public int[] GetNextSteps()
  {
    DataRow[] dataRowArray = this.ServiceContext.ClientCache.GetTable("IMS_LC_LINKS").Select("F_FROM_STEP = " + this.LCStep.ToString());
    int[] nextSteps = new int[dataRowArray.Length];
    for (int index = 0; index < nextSteps.Length; ++index)
      nextSteps[index] = Convert.ToInt32(dataRowArray[index]["F_TO_STEP"]);
    return nextSteps;
  }

  public int GetNextStep(int levelID) => throw new NotImplementedException();

  public int GetDeleteStepID() => throw new NotImplementedException();
}
