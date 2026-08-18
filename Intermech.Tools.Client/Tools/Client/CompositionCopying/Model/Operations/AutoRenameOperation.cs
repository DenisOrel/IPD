// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Model.Operations.AutoRenameOperation
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Model.Operations;

internal sealed class AutoRenameOperation : LongRunningOperation
{
  private CopyingSession session;

  public void Invoke(CopyingSession session)
  {
    if (session == null)
      throw new ArgumentNullException(nameof (session));
    this.ErrorsBuilder.Clear();
    try
    {
      this.InitializeCore(session);
      this.InvokeCore();
    }
    finally
    {
      this.CleanupCore();
    }
  }

  private void InitializeCore(CopyingSession session) => this.session = session;

  private void CleanupCore() => this.session = (CopyingSession) null;

  private void InvokeCore()
  {
    ICollection<DBObjectGraphVertex> allVertices = this.session.Graph.GetAllVertices((Predicate<DBObjectGraphVertex>) (x => x.IsDocument() && x.CopyingSelector.IsSelected && x.IsScanned));
    if (allVertices.Count == 0)
      return;
    this.RenameFiles(allVertices);
  }

  private void RenameFiles(ICollection<DBObjectGraphVertex> vertices)
  {
    double num1 = 100.0 / (double) vertices.Count;
    int num2 = 0;
    foreach (DBObjectGraphVertex vertex in (IEnumerable<DBObjectGraphVertex>) vertices)
    {
      foreach (DBObjectFileEntry file in (IEnumerable<DBObjectFileEntry>) vertex.Files)
        file.NewName = this.session.IntegratorHeuristics.RenameFile(this.session, vertex, file);
      ++num2;
      this.ReportProgress((int) Math.Round(num1 * (double) num2));
    }
  }
}
