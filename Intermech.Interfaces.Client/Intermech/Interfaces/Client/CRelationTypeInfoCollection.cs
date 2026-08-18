// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.CRelationTypeInfoCollection
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Коллекция с информацией о типе связей</summary>
internal class CRelationTypeInfoCollection(
  MetadataInfoParentContext serviceContext,
  object parentID,
  bool filtering) : MetadataInfoCollection(serviceContext, parentID, filtering), IDBRelationTypeInfoCollection, IDBCollection
{
  protected override string DBKeyField
  {
    [DebuggerStepThrough] get => "F_RELATION_TYPE";
  }

  protected override string DBTableName
  {
    [DebuggerStepThrough] get => "IMS_RELATION_TYPES";
  }

  protected override string GetParentSQL() => string.Empty;
}
