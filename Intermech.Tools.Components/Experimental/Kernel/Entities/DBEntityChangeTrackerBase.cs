// Decompiled with JetBrains decompiler
// Type: Experimental.Kernel.Entities.DBEntityChangeTrackerBase
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Experimental.Data.Entities;
using System.Collections.Generic;

#nullable disable
namespace Experimental.Kernel.Entities;

internal abstract class DBEntityChangeTrackerBase(IEntityChangeTrackerConfiguration configuration) : 
  EntityChangeTracker(configuration)
{
  protected abstract bool IsChildOccurence(object entity);

  protected override void DoValidateEntityBeforeAttach(object entity)
  {
    base.DoValidateEntityBeforeAttach(entity);
    if (this.IsChildOccurence(entity))
      throw new EntityValidationException(entity, $"Объект-связка '{entity}' не может быть зарегистрирован в трекере в качестве самостоятельного объекта. Вместо этого он должен быть добавлен в навигационное свойство соответствующего доменного объекта.");
  }

  protected override void DoValidateEntityBeforeScan(
    object entity,
    IList<ParentEntityPropertyInfo> referencedBy,
    IList<ParentEntityPropertyInfo> initiallyReferencedBy)
  {
    base.DoValidateEntityBeforeScan(entity, referencedBy, initiallyReferencedBy);
    if (!this.IsChildOccurence(entity))
      return;
    if (referencedBy.Count > 1)
      throw this.ChildOccurenceEntityIsOverreferenced(entity);
    if (referencedBy.Count == 1)
    {
      if (initiallyReferencedBy.Count > 1)
        throw this.ChildOccurenceEntityIsOverreferenced(entity);
      if (initiallyReferencedBy.Count == 1 && initiallyReferencedBy[0].Entity != referencedBy[0].Entity)
        throw this.ChildOccurenceEntityIsTransferred(entity);
    }
    else if (initiallyReferencedBy.Count != 1)
      throw this.ChildOccurenceEntityIsOverreferenced(entity);
  }

  private EntityValidationException ChildOccurenceEntityIsOverreferenced(object entity)
  {
    return new EntityValidationException(entity, $"Объект-связка '{entity}' не может использоваться несколькими доменными объектами одновременно.");
  }

  private EntityValidationException ChildOccurenceEntityIsTransferred(object entity)
  {
    return new EntityValidationException(entity, $"Объект-связка '{entity}' не может передан от одного доменного объекта к другому. Вместо этого нужно использовать новый объект-связку.");
  }

  protected override bool CanAutoRemoveUnreferencedEntity(
    object entity,
    IList<ParentEntityPropertyInfo> referencedBy,
    IList<ParentEntityPropertyInfo> initiallyReferencedBy)
  {
    return this.IsChildOccurence(entity) && (referencedBy.Count == 0 && initiallyReferencedBy.Count == 1 || referencedBy.Count == 1 && initiallyReferencedBy.Count == 1 && referencedBy[0].Entity == initiallyReferencedBy[0].Entity && this.RecycleBin.Contains(referencedBy[0].Entity)) || base.CanAutoRemoveUnreferencedEntity(entity, referencedBy, initiallyReferencedBy);
  }
}
