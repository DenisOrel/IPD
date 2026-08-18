// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Views.DBObjectGraphVertexReference
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Tools.Client.CompositionCopying.Model;
using System;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Views;

internal sealed class DBObjectGraphVertexReference
{
  public DBObjectGraphVertexReference(CopyingSession session, DBObjectGraphVertex vertex)
  {
    if (session == null)
      throw new ArgumentNullException(nameof (session));
    if (vertex == null)
      throw new ArgumentNullException(nameof (vertex));
    this.Session = session;
    this.Vertex = vertex;
  }

  public CopyingSession Session { get; }

  public DBObjectGraphVertex Vertex { get; }
}
