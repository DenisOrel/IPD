// Decompiled with JetBrains decompiler
// Type: Experimental.Data.Entities.NavigationPropertySnapshot
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Collections;
using System;
using System.Collections.Generic;

#nullable disable
namespace Experimental.Data.Entities;

/// <summary>
/// Контейнер для хранения содержимого навигационного свойства доменного объекта.
/// </summary>
internal sealed class NavigationPropertySnapshot
{
  public NavigationPropertySnapshot(
    string propertyName,
    EntityMemberPresenceStatus presenceStatus,
    ICollection<object> propertyValues)
  {
    if (propertyName == null)
      throw new ArgumentNullException(nameof (propertyName));
    if (propertyValues == null)
      throw new ArgumentNullException(nameof (propertyValues));
    this.PropertyName = propertyName;
    this.PresenceStatus = presenceStatus;
    this.PropertyValues = propertyValues.IsReadOnly ? propertyValues : (ICollection<object>) new ReadOnlyCollectionWrapper<object>(propertyValues);
  }

  public string PropertyName { get; private set; }

  public EntityMemberPresenceStatus PresenceStatus { get; private set; }

  public ICollection<object> PropertyValues { get; private set; }
}
