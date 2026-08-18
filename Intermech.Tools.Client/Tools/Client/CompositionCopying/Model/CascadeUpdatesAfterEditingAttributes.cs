// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Model.CascadeUpdatesAfterEditingAttributes
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Model;

internal sealed class CascadeUpdatesAfterEditingAttributes : 
  DeferredEventHandler<DBObjectAttributesChangedDeferredEvent>
{
  private int changesCount;

  protected override void DoProcess(
    object sender,
    DBObjectAttributesChangedDeferredEvent deferredEvent)
  {
    DBObjectGraphVertex dbObjectVertex = deferredEvent.DBObjectVertex;
    if (!dbObjectVertex.IsDocument() && !dbObjectVertex.IsArticle())
      return;
    ++this.changesCount;
  }

  protected override void DoEnd(object sender)
  {
    if (this.changesCount != 0)
    {
      try
      {
        this.ProcessChanges((CopyingSession) sender);
      }
      finally
      {
        this.changesCount = 0;
      }
    }
    base.DoEnd(sender);
  }

  private void ProcessChanges(CopyingSession session)
  {
    ICollection<DBObjectGraphVertex> allVertices = session.Graph.GetAllVertices((Predicate<DBObjectGraphVertex>) (x => x.IsDocument() && x.CopyingSelector.IsSelected && x.IsScanned));
    CleanupCopyStateOperation batch = new CleanupCopyStateOperation();
    foreach (DBObjectGraphVertex objectGraphVertex in (IEnumerable<DBObjectGraphVertex>) allVertices)
    {
      DBObjectGraphVertex documentVertex = objectGraphVertex;
      DocumentTrait trait;
      if (documentVertex.TryGetTrait<DocumentTrait>(out trait))
      {
        if (trait.IsLocalFilesCopied)
          trait.ResetLocalFilesCopied((ICleanupCopyStateRegistry) batch);
        if (trait.IsDBCopied)
          trait.ResetDBCopyInfo((ICleanupCopyStateRegistry) batch);
      }
      session.UserWorkItems.RemoveAll((Predicate<UserWorkItem>) (x => x.Vertex == documentVertex));
    }
    batch.Invoke(session);
  }
}
