// Decompiled with JetBrains decompiler
// Type: Experimental.Data.Entities.ModifiedNavigationPropertyRecord
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Experimental.Data.Entities;

public sealed class ModifiedNavigationPropertyRecord
{
  public ModifiedNavigationPropertyRecord(string propertyName)
  {
    this.PropertyName = propertyName != null ? propertyName : throw new ArgumentNullException(nameof (propertyName));
    this.Modifications = new List<NavigationPropertyModification>();
  }

  public string PropertyName { get; private set; }

  public List<NavigationPropertyModification> Modifications { get; private set; }
}
