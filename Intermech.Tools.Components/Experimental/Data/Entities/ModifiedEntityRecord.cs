// Decompiled with JetBrains decompiler
// Type: Experimental.Data.Entities.ModifiedEntityRecord
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using System.Collections.Generic;

#nullable disable
namespace Experimental.Data.Entities;

public class ModifiedEntityRecord : EntityChangeTrackerLogRecord
{
  public ModifiedEntityRecord(object entity, bool isRootEntity)
    : base(entity, isRootEntity)
  {
    this.ModifiedDataProperties = new List<string>();
    this.ReferencedBy = new List<ParentEntityPropertyInfo>();
  }

  public List<string> ModifiedDataProperties { get; private set; }

  public List<ParentEntityPropertyInfo> ReferencedBy { get; private set; }
}
