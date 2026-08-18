// Decompiled with JetBrains decompiler
// Type: Intermech.CADInterface.Proxies.NamedTagCollection
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.CADInterface.Proxies;

public class NamedTagCollection
{
  private Dictionary<string, object> tags;

  public NamedTagCollection() => this.tags = new Dictionary<string, object>();

  public void Clear() => this.tags.Clear();

  public object TryGet(string name)
  {
    if (name == null)
      throw new ArgumentNullException(nameof (name));
    object obj;
    return this.tags.TryGetValue(name, out obj) ? obj : (object) null;
  }

  public void Set(string name, object value)
  {
    if (name == null)
      throw new ArgumentNullException(nameof (name));
    if (value != null)
      this.tags[name] = value;
    else
      this.tags.Remove(name);
  }

  public void Remove(string name)
  {
    if (name == null)
      throw new ArgumentNullException(nameof (name));
    this.tags.Remove(name);
  }
}
