// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.CLifecycleLevelInfoCollection
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Коллекция с информацией об уровнях продвижения</summary>
internal class CLifecycleLevelInfoCollection(
  MetadataInfoParentContext serviceContext,
  object parentID,
  bool filtering) : MetadataInfoCollection(serviceContext, parentID, filtering), IDBLifecycleLevelInfoCollection, IDBCollection
{
  protected override string DBKeyField => "F_LEVEL_ID";

  protected override string DBTableName => "IMS_LEVELS";
}
