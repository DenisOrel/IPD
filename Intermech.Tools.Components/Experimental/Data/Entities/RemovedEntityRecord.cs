// Decompiled with JetBrains decompiler
// Type: Experimental.Data.Entities.RemovedEntityRecord
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using System.Collections.Generic;

#nullable disable
namespace Experimental.Data.Entities;

public class RemovedEntityRecord : EntityChangeTrackerLogRecord
{
  public RemovedEntityRecord(object entity, bool isRootEntity)
    : base(entity, isRootEntity)
  {
    this.InitiallyReferencedBy = new List<ParentEntityPropertyInfo>();
  }

  /// <summary>
  /// Возвращает коллекцию непосредственных ссылок на доменный объект, которые существовали на момент начала отслеживания изменений.
  /// </summary>
  public List<ParentEntityPropertyInfo> InitiallyReferencedBy { get; private set; }
}
