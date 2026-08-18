// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Model.Operations.AddCopyingSelectorEntryRecursive
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Model.Operations;

internal sealed class AddCopyingSelectorEntryRecursive : CopyingSelectorOperation
{
  protected override void DoInvoke(
    CopyingSession session,
    DBObjectGraphVertex startVertex,
    CopyingSelectorEntry entry)
  {
    ICollection<DBObjectGraphVertex> foundVertices = entry.IsAllowing ? session.Graph.GetVerticesByInEdgesRecursive(startVertex, (Predicate<DBObjectGraphVertex>) (x => x.IsDocument())) : session.Graph.GetVerticesByOutEdgesRecursive(startVertex, (Predicate<DBObjectGraphVertex>) (x => x.IsDocument()));
    if (!this.ValidateAdd(foundVertices, entry))
      return;
    foreach (DBObjectGraphVertex objectGraphVertex in (IEnumerable<DBObjectGraphVertex>) foundVertices)
      objectGraphVertex.CopyingSelector.TryAdd(entry);
  }

  private bool ValidateAdd(
    ICollection<DBObjectGraphVertex> foundVertices,
    CopyingSelectorEntry entry)
  {
    int num = 0;
    foreach (DBObjectGraphVertex foundVertex in (IEnumerable<DBObjectGraphVertex>) foundVertices)
    {
      (bool flag, CopyingSelectorEntry copyingSelectorEntry) = foundVertex.CopyingSelector.CanAdd(entry.IsAllowing);
      if (!flag)
      {
        ++num;
        this.ErrorsBuilder.AddError(new OperationError($"Невозможно установить запрет/разрешение копирования для объекта IPS '{foundVertex.Caption}' из-за конфликта с другой операцией. Идентификатор конфликтующей операции: {copyingSelectorEntry.HeuristicsId}, сообщение конфликтующей операции: {copyingSelectorEntry.Description}", vertex: foundVertex));
      }
    }
    return num == 0;
  }
}
