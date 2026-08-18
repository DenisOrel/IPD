// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Model.Operations.CleanupFullStateOperation
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Model.Operations;

internal sealed class CleanupFullStateOperation
{
  public void Invoke(CopyingSession session)
  {
    if (session == null)
      throw new ArgumentNullException(nameof (session));
    CleanupCopyStateOperation batch = new CleanupCopyStateOperation();
    foreach (IDBObjectGraphTraitOwner allVertex in (IEnumerable<DBObjectGraphVertex>) session.Graph.GetAllVertices())
    {
      DocumentTrait trait;
      if (allVertex.TryGetTrait<DocumentTrait>(out trait))
      {
        if (trait.IsLocalFilesCopied)
          trait.ResetLocalFilesCopied((ICleanupCopyStateRegistry) batch);
        if (trait.IsDBCopied)
          trait.ResetDBCopyInfo((ICleanupCopyStateRegistry) batch);
      }
    }
    batch.Invoke(session);
  }
}
