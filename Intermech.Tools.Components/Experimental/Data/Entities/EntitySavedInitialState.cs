// Decompiled with JetBrains decompiler
// Type: Experimental.Data.Entities.EntitySavedInitialState
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

#nullable disable
namespace Experimental.Data.Entities;

/// <summary>
/// Содержит состояние доменного объекта на момент начала отслеживания изменений в доменных объектах.
/// </summary>
internal sealed class EntitySavedInitialState : IEntityStateRecord
{
  private static readonly IList<DataPropertySnapshot> emptyDataProperties = (IList<DataPropertySnapshot>) new ReadOnlyCollection<DataPropertySnapshot>((IList<DataPropertySnapshot>) new DataPropertySnapshot[0]);
  private static readonly IList<NavigationPropertySnapshot> emptyNavigationProperties = (IList<NavigationPropertySnapshot>) new ReadOnlyCollection<NavigationPropertySnapshot>((IList<NavigationPropertySnapshot>) new NavigationPropertySnapshot[0]);
  private static readonly IList<ParentEntityPropertyInfo> emptyReferencedBy = (IList<ParentEntityPropertyInfo>) new ReadOnlyCollection<ParentEntityPropertyInfo>((IList<ParentEntityPropertyInfo>) new ParentEntityPropertyInfo[0]);
  private IList<DataPropertySnapshot> dataProperties;
  private IList<NavigationPropertySnapshot> navigationProperties;
  private IList<ParentEntityPropertyInfo> referencedBy;

  public EntitySavedInitialState(object entity, bool isRootEntity)
  {
    this.Entity = entity;
    this.IsRootEntity = isRootEntity;
    this.dataProperties = EntitySavedInitialState.emptyDataProperties;
    this.navigationProperties = EntitySavedInitialState.emptyNavigationProperties;
    this.referencedBy = EntitySavedInitialState.emptyReferencedBy;
  }

  public object Entity { get; private set; }

  public bool IsRootEntity { get; set; }

  public IList<DataPropertySnapshot> DataProperties
  {
    [DebuggerStepThrough] get => this.dataProperties;
    [DebuggerStepThrough] set
    {
      this.dataProperties = value != null ? value : throw new ArgumentNullException(nameof (value));
    }
  }

  public IList<NavigationPropertySnapshot> NavigationProperties
  {
    [DebuggerStepThrough] get => this.navigationProperties;
    [DebuggerStepThrough] set
    {
      this.navigationProperties = value != null ? value : throw new ArgumentNullException(nameof (value));
    }
  }

  public IList<ParentEntityPropertyInfo> ReferencedBy
  {
    [DebuggerStepThrough] get => this.referencedBy;
    [DebuggerStepThrough] set
    {
      this.referencedBy = value != null ? value : throw new ArgumentNullException(nameof (value));
    }
  }
}
