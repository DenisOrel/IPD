// Decompiled with JetBrains decompiler
// Type: Experimental.Kernel.Entities.DBEntityChangeTracker
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Experimental.Data.Entities;
using Intermech;
using System;

#nullable disable
namespace Experimental.Kernel.Entities;

internal sealed class DBEntityChangeTracker : DBEntityChangeTrackerBase
{
  private DBModelConfiguration modelConfiguration;

  public DBEntityChangeTracker(
    IEntityChangeTrackerConfiguration configuration,
    DBModelConfiguration modelConfiguration)
    : base(configuration)
  {
    this.modelConfiguration = modelConfiguration != null ? modelConfiguration : throw new ArgumentNullException(nameof (modelConfiguration));
  }

  protected override bool IsNewEntity(object entity)
  {
    IDBEntityTypeDescriptor entityTypeDescriptor = this.modelConfiguration.GetEntityTypeDescriptor(entity);
    switch (entityTypeDescriptor.EntityKind)
    {
      case DBEntityKind.Object:
        return entityTypeDescriptor.AsDBObjectDescriptor().GetKey(entity) == 0L;
      case DBEntityKind.Relation:
        return entityTypeDescriptor.AsDBRelationDescriptor().GetKey(entity) == 0L;
      default:
        throw new NotSupportedEnumException((Enum) entityTypeDescriptor.EntityKind);
    }
  }

  protected override bool IsChildOccurence(object entity)
  {
    return this.modelConfiguration.GetEntityTypeDescriptor(entity).EntityKind == DBEntityKind.Relation;
  }
}
