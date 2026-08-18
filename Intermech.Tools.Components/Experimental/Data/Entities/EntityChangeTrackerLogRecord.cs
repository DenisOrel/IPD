// Decompiled with JetBrains decompiler
// Type: Experimental.Data.Entities.EntityChangeTrackerLogRecord
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Experimental.Data.Entities;

public abstract class EntityChangeTrackerLogRecord
{
  protected EntityChangeTrackerLogRecord(object entity, bool isRootEntity)
  {
    this.Entity = entity != null ? entity : throw new ArgumentNullException(nameof (entity));
    this.IsRootEntity = isRootEntity;
    this.ModifiedNavigationProperties = new List<ModifiedNavigationPropertyRecord>();
  }

  public object Entity { get; private set; }

  public bool IsRootEntity { get; private set; }

  public List<ModifiedNavigationPropertyRecord> ModifiedNavigationProperties { get; private set; }
}
