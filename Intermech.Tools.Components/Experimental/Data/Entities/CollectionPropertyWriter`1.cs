// Decompiled with JetBrains decompiler
// Type: Experimental.Data.Entities.CollectionPropertyWriter`1
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Experimental.Data.Entities;

internal sealed class CollectionPropertyWriter<T> : INavigationPropertyWriter
{
  private NavigationPropertyDescriptor descriptor;

  public CollectionPropertyWriter(NavigationPropertyDescriptor descriptor)
  {
    this.descriptor = descriptor;
  }

  public void AssignValueFromCollection(object entity, IEnumerable<object> items)
  {
    object propertyValue = this.descriptor.GetValue(entity).PropertyValue;
    if (propertyValue == null)
    {
      propertyValue = Activator.CreateInstance(typeof (List<>).MakeGenericType(this.descriptor.Definition.ContainerItemType));
      this.descriptor.SetValue(entity, propertyValue);
    }
    ICollection<T> objs = (ICollection<T>) propertyValue;
    objs.Clear();
    foreach (object obj in items)
      objs.Add((T) obj);
  }
}
