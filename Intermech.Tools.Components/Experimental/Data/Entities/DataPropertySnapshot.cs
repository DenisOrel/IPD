// Decompiled with JetBrains decompiler
// Type: Experimental.Data.Entities.DataPropertySnapshot
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using System;

#nullable disable
namespace Experimental.Data.Entities;

internal sealed class DataPropertySnapshot
{
  public DataPropertySnapshot(string propertyName, object propertyValue)
  {
    this.PropertyName = propertyName != null ? propertyName : throw new ArgumentNullException(nameof (propertyName));
    this.PropertyValue = propertyValue;
  }

  public string PropertyName { get; private set; }

  public object PropertyValue { get; private set; }
}
