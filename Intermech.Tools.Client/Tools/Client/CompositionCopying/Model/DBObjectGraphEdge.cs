// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Model.DBObjectGraphEdge
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using QuickGraph;
using System;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Model;

internal class DBObjectGraphEdge : IEdge<DBObjectGraphVertex>, IDBObjectGraphTraitOwner
{
  public DBObjectGraphEdge(DBObjectGraphVertex source, DBObjectGraphVertex target)
  {
    if (source == null)
      throw new ArgumentNullException(nameof (source));
    if (target == null)
      throw new ArgumentNullException(nameof (target));
    this.Source = source;
    this.Target = target;
    this.Traits = new DBObjectGraphTraitCollection((IDBObjectGraphTraitOwner) this);
  }

  public DBObjectGraphVertex Source { get; }

  public DBObjectGraphVertex Target { get; }

  public DBObjectGraphTraitCollection Traits { get; }
}
