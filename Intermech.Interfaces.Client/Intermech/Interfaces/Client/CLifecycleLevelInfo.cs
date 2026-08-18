// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.CLifecycleLevelInfo
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Получатель инфы об уровне продвижения</summary>
internal class CLifecycleLevelInfo(MetadataInfoParentContext serviceContext, int metadataID) : 
  MetadataInfoObject(serviceContext, metadataID),
  IDBLifecycleLevelInfo
{
  protected override string DBTableName => "IMS_LEVELS";

  protected override string MetadataNotFoundMessage
  {
    get => $"Уровень продвижения номер {this.MetadataID} не найден.";
  }

  public override string ObjectName => $"Уровень продвижение '{this.LevelName}'";

  public int LevelID
  {
    [DebuggerStepThrough] get => this.MetadataID;
  }

  public string LevelName
  {
    [DebuggerStepThrough] get => this.paramsTable[0]["F_LEVEL_NAME"].ToString();
  }

  public string Litera
  {
    [DebuggerStepThrough] get => this.paramsTable[0]["F_LITERA"].ToString();
  }

  public byte[] LevelIcon
  {
    [DebuggerStepThrough] get
    {
      return this.paramsTable[0]["F_ICON"] == DBNull.Value ? new byte[0] : (byte[]) this.paramsTable[0]["F_ICON"];
    }
  }
}
