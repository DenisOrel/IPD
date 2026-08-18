// Decompiled with JetBrains decompiler
// Type: Experimental.Data.Entities.ParentEntityPropertyInfo
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using System;

#nullable disable
namespace Experimental.Data.Entities;

public sealed class ParentEntityPropertyInfo
{
  public ParentEntityPropertyInfo(object entity, string propertyName)
  {
    if (entity == null)
      throw new ArgumentNullException(nameof (entity));
    if (propertyName == null)
      throw new ArgumentNullException(nameof (propertyName));
    this.Entity = entity;
    this.PropertyName = propertyName;
  }

  public object Entity { get; private set; }

  public string PropertyName { get; private set; }
}
