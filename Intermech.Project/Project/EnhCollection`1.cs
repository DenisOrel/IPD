// Decompiled with JetBrains decompiler
// Type: Intermech.Project.EnhCollection`1
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using Intermech.Diagnostics;
using System;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable
namespace Intermech.Project;

[Serializable]
public class EnhCollection<T> : Collection<T> where T : Entity
{
  public bool _Modified;
  [NotNull]
  protected List<T> _DeletedItems = new List<T>();

  protected override void OnItemRemoving([NotNull] T item)
  {
    if (!this._DeletedItems.Contains(item))
      this._DeletedItems.Add(item);
    base.OnItemRemoving(item);
  }

  protected override void OnListChanged([NotNull] ListChangedEventArgs e)
  {
    this._Modified = true;
    base.OnListChanged(e);
  }
}
