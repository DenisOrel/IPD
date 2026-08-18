// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.DupRemover`1
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using System.Collections.Generic;

#nullable disable
namespace Intermech.Workflow;

public class DupRemover<T>
{
  private List<T> _items = new List<T>();

  public bool Predicate(T obj)
  {
    if (this._items.Contains(obj))
      return true;
    this._items.Add(obj);
    return false;
  }
}
