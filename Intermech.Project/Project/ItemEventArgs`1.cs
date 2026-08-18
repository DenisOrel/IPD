// Decompiled with JetBrains decompiler
// Type: Intermech.Project.ItemEventArgs`1
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using Intermech.Diagnostics;
using System;

#nullable disable
namespace Intermech.Project;

public class ItemEventArgs<T> : EventArgs where T : Entity
{
  public readonly int Index = -1;

  public ItemEventArgs([NotNull] T item) => this.Item = item;

  public ItemEventArgs([NotNull] T item, int index)
    : this(item)
  {
    this.Index = index;
  }

  [NotNull]
  public T Item { get; }
}
