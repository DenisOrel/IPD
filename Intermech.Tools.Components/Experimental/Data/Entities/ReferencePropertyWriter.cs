// Decompiled with JetBrains decompiler
// Type: Experimental.Data.Entities.ReferencePropertyWriter
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using System.Collections.Generic;

#nullable disable
namespace Experimental.Data.Entities;

internal sealed class ReferencePropertyWriter : INavigationPropertyWriter
{
  private NavigationPropertyDescriptor descriptor;

  public ReferencePropertyWriter(NavigationPropertyDescriptor descriptor)
  {
    this.descriptor = descriptor;
  }

  public void AssignValueFromCollection(object entity, IEnumerable<object> items)
  {
    using (IEnumerator<object> enumerator = items.GetEnumerator())
    {
      if (enumerator.MoveNext())
      {
        object current = enumerator.Current;
        this.descriptor.SetValue(entity, current);
        return;
      }
    }
    this.descriptor.SetValue(entity, (object) null);
  }
}
