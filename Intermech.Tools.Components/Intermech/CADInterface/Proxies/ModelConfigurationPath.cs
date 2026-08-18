// Decompiled with JetBrains decompiler
// Type: Intermech.CADInterface.Proxies.ModelConfigurationPath
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace Intermech.CADInterface.Proxies;

public sealed class ModelConfigurationPath : IEnumerable<string>, IEnumerable, ICloneable
{
  private readonly LinkedList<string> items;

  public ModelConfigurationPath() => this.items = new LinkedList<string>();

  public ModelConfigurationPath(IEnumerable<string> items)
  {
    this.items = items != null ? new LinkedList<string>(items) : throw new ArgumentNullException();
  }

  public void Add(string configurationName)
  {
    if (string.IsNullOrEmpty(configurationName))
      throw new ArgumentException();
    this.items.AddLast(configurationName);
  }

  public ModelConfigurationPath Clone()
  {
    return new ModelConfigurationPath((IEnumerable<string>) this.items);
  }

  object ICloneable.Clone() => (object) this.Clone();

  public IEnumerator<string> GetEnumerator() => (IEnumerator<string>) this.items.GetEnumerator();

  IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.items.GetEnumerator();

  public string RootConfiguration => this.items.First.Value;

  public string TargetConfiguration => this.items.Last.Value;
}
