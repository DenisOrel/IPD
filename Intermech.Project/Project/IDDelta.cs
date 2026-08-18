// Decompiled with JetBrains decompiler
// Type: Intermech.Project.IDDelta
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using Intermech.Diagnostics;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Project;

internal class IDDelta
{
  [NotNull]
  public List<long> Added { get; } = new List<long>();

  [NotNull]
  public List<long> Deleted { get; } = new List<long>();

  public void Add(long id, bool added) => (added ? this.Added : this.Deleted).Add(id);

  public void Clear()
  {
    this.Added.Clear();
    this.Deleted.Clear();
  }
}
