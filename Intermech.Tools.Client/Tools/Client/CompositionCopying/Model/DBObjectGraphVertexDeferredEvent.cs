// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Model.DBObjectGraphVertexDeferredEvent
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using System;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Model;

internal abstract class DBObjectGraphVertexDeferredEvent : DeferredEvent
{
  public DBObjectGraphVertexDeferredEvent(DBObjectGraphVertex dbObjectVertex)
  {
    this.DBObjectVertex = dbObjectVertex != null ? dbObjectVertex : throw new ArgumentNullException(nameof (dbObjectVertex));
  }

  public DBObjectGraphVertex DBObjectVertex { get; }
}
