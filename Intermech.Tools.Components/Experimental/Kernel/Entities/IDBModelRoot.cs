// Decompiled with JetBrains decompiler
// Type: Experimental.Kernel.Entities.IDBModelRoot
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Experimental.Data.Entities;

#nullable disable
namespace Experimental.Kernel.Entities;

public interface IDBModelRoot
{
  IEntityLocalCache EntityLocalCache { get; }

  IEntityChangeTracker ChangeTracker { get; }

  IEntityBatchUpdateService BatchUpdateService { get; }

  DBMetadataInfoService MetadataInfoService { get; }

  IEntityBatchUpdateScope StartBatchUpdate();
}
