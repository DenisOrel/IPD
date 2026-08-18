// Decompiled with JetBrains decompiler
// Type: Experimental.Data.Entities.EntityCaptureChangesState
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
/// Содержит состояние доменного объекта на момент определения изменений.
/// </summary>
internal sealed class EntityCaptureChangesState : IEntityStateRecord
{
  private static readonly IList<NavigationPropertySnapshot> emptyNavigationProperties = (IList<NavigationPropertySnapshot>) new ReadOnlyCollection<NavigationPropertySnapshot>((IList<NavigationPropertySnapshot>) new NavigationPropertySnapshot[0]);
  private static readonly IList<ParentEntityPropertyInfo> emptyReferencedBy = (IList<ParentEntityPropertyInfo>) new ReadOnlyCollection<ParentEntityPropertyInfo>((IList<ParentEntityPropertyInfo>) new ParentEntityPropertyInfo[0]);
  private IList<NavigationPropertySnapshot> navigationProperties;
  private IList<ParentEntityPropertyInfo> referencedBy;

  public EntityCaptureChangesState(object entity, bool isRootEntity)
  {
    this.Entity = entity;
    this.IsRootEntity = isRootEntity;
    this.navigationProperties = EntityCaptureChangesState.emptyNavigationProperties;
    this.referencedBy = EntityCaptureChangesState.emptyReferencedBy;
  }

  public object Entity { get; private set; }

  public bool IsRootEntity { get; set; }

  /// <summary>
  /// Возвращает или задает состояние доменного объекта на момент начала отслеживания изменений.
  /// Значение свойства может быть не задано и равно null, если доменный объект не существовал на момент начала отслеживания изменений.
  /// </summary>
  public EntitySavedInitialState InitialState { get; set; }

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
