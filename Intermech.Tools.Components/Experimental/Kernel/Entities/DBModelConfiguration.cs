// Decompiled with JetBrains decompiler
// Type: Experimental.Kernel.Entities.DBModelConfiguration
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Experimental.Data.Entities;
using System;
using System.Collections.Generic;

#nullable disable
namespace Experimental.Kernel.Entities;

public abstract class DBModelConfiguration : ModelConfiguration
{
  private EntityChangeTrackerConfiguration changeTrackerConfiguration;

  protected virtual void DoBuildModel(DBModelBuilder modelBuilder)
  {
  }

  protected override void DoInitialize()
  {
    DBModelConfigurationBuilder modelBuilder = new DBModelConfigurationBuilder();
    this.DoBuildModel((DBModelBuilder) modelBuilder);
    DBModelConfigurationBuilderResult configurationBuilderResult = modelBuilder.Build();
    this.changeTrackerConfiguration = new EntityChangeTrackerConfiguration((IEnumerable<EntityChangeTrackerDescriptor>) configurationBuilderResult.ChangeTrackerDescriptors);
    this.changeTrackerConfiguration.Initialize();
    foreach (EntityTypeDescriptor internalDescriptor in configurationBuilderResult.InternalDescriptors)
      this.AddDescriptor(internalDescriptor);
    base.DoInitialize();
  }

  internal IDBEntityTypeDescriptor GetEntityTypeDescriptor(Type entityType)
  {
    return !(entityType == (Type) null) ? (IDBEntityTypeDescriptor) this.GetDescriptorInternal(entityType) : throw new ArgumentNullException(nameof (entityType));
  }

  internal IDBEntityTypeDescriptor GetEntityTypeDescriptor(object entity)
  {
    return entity != null ? this.GetEntityTypeDescriptor(entity.GetType()) : throw new ArgumentNullException(nameof (entity));
  }

  internal IEntityChangeTrackerConfiguration ChangeTrackerConfiguration
  {
    get
    {
      this.RequireInitialized();
      return (IEntityChangeTrackerConfiguration) this.changeTrackerConfiguration;
    }
  }
}
