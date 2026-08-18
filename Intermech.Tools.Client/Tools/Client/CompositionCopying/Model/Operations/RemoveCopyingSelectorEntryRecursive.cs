// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Model.Operations.RemoveCopyingSelectorEntryRecursive
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Model.Operations;

internal sealed class RemoveCopyingSelectorEntryRecursive : CopyingSelectorOperation
{
  protected override void DoInvoke(
    CopyingSession session,
    DBObjectGraphVertex startVertex,
    CopyingSelectorEntry entry)
  {
    foreach (DBObjectGraphVertex objectGraphVertex in entry.IsAllowing ? (IEnumerable<DBObjectGraphVertex>) session.Graph.GetVerticesByOutEdgesRecursive(startVertex, (Predicate<DBObjectGraphVertex>) (x => x.IsDocument())) : (IEnumerable<DBObjectGraphVertex>) session.Graph.GetVerticesByInEdgesRecursive(startVertex, (Predicate<DBObjectGraphVertex>) (x => x.IsDocument())))
      objectGraphVertex.CopyingSelector.Remove(entry);
  }
}
