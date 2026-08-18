// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Model.CascadeUpdatesAfterReselectingDocuments
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Model;

internal sealed class CascadeUpdatesAfterReselectingDocuments : 
  DeferredEventHandler<DBObjectReselectedDeferredEvent>
{
  private ICollection<DBObjectGraphVertex> notSelectedVertices;

  public CascadeUpdatesAfterReselectingDocuments()
  {
    this.notSelectedVertices = (ICollection<DBObjectGraphVertex>) new HashSet<DBObjectGraphVertex>();
  }

  protected override void DoProcess(object sender, DBObjectReselectedDeferredEvent deferredEvent)
  {
    DBObjectGraphVertex dbObjectVertex = deferredEvent.DBObjectVertex;
    if (dbObjectVertex.CopyingSelector.IsSelected)
      return;
    this.notSelectedVertices.Add(dbObjectVertex);
  }

  protected override void DoEnd(object sender)
  {
    if (this.notSelectedVertices.Count != 0)
    {
      try
      {
        this.ProcessNoSelectedVertices((CopyingSession) sender);
      }
      finally
      {
        this.notSelectedVertices.Clear();
      }
    }
    base.DoEnd(sender);
  }

  private void ProcessNoSelectedVertices(CopyingSession session)
  {
    CleanupCopyStateOperation batch = new CleanupCopyStateOperation();
    foreach (DBObjectGraphVertex notSelectedVertex in (IEnumerable<DBObjectGraphVertex>) this.notSelectedVertices)
    {
      DBObjectGraphVertex dbObjectVertex = notSelectedVertex;
      dbObjectVertex.Attributes.Clear();
      dbObjectVertex.Files.Clear();
      dbObjectVertex.Content = (DBObjectContent) DBObjectEmptyContent.Instance;
      if (dbObjectVertex.IsScanned)
        dbObjectVertex.IsScanned = false;
      DocumentTrait trait;
      if (dbObjectVertex.TryGetTrait<DocumentTrait>(out trait))
      {
        if (trait.IsLocalFilesCopied)
          trait.ResetLocalFilesCopied((ICleanupCopyStateRegistry) batch);
        if (trait.IsDBCopied)
          trait.ResetDBCopyInfo((ICleanupCopyStateRegistry) batch);
      }
      session.UserWorkItems.RemoveAll((Predicate<UserWorkItem>) (x => x.Vertex == dbObjectVertex));
    }
    batch.Invoke(session);
  }
}
