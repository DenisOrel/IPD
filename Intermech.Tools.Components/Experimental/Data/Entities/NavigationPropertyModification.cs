// Decompiled with JetBrains decompiler
// Type: Experimental.Data.Entities.NavigationPropertyModification
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using System;

#nullable disable
namespace Experimental.Data.Entities;

public sealed class NavigationPropertyModification
{
  public NavigationPropertyModification(
    NavigationPropertyModificationType modificationType,
    object propertyValue)
  {
    if (propertyValue == null)
      throw new ArgumentNullException(nameof (propertyValue));
    this.ModificationType = modificationType;
    this.PropertyValue = propertyValue;
  }

  public NavigationPropertyModificationType ModificationType { get; private set; }

  public object PropertyValue { get; private set; }
}
