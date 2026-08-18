// Decompiled with JetBrains decompiler
// Type: Experimental.Data.Entities.EntityChangeTrackerConfiguration
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Experimental.Data.Entities;

public class EntityChangeTrackerConfiguration : ModelConfiguration, IEntityChangeTrackerConfiguration
{
  public EntityChangeTrackerConfiguration(
    IEnumerable<EntityChangeTrackerDescriptor> descriptors)
  {
    if (descriptors == null)
      throw new ArgumentNullException(nameof (descriptors));
    foreach (EntityTypeDescriptor descriptor in descriptors)
      this.AddDescriptor(descriptor);
  }

  public IBasicEntityTypeDescriptor GetEntityDescriptor(object entity)
  {
    return entity != null ? this.GetEntityDescriptor(entity.GetType()) : throw new ArgumentNullException(nameof (entity));
  }

  private IBasicEntityTypeDescriptor GetEntityDescriptor(Type entityType)
  {
    return !(entityType == (Type) null) ? (IBasicEntityTypeDescriptor) this.GetDescriptorInternal(entityType) : throw new ArgumentNullException(nameof (entityType));
  }
}
