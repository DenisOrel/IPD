// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Model.Operations.ForceCopyingForRootCADDocument
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Model.Operations;

internal sealed class ForceCopyingForRootCADDocument : CopyingSelectorHeuristics
{
  public ForceCopyingForRootCADDocument()
    : base(true)
  {
  }

  protected override void DoApply(CopyingSession session)
  {
    base.DoApply(session);
    CopyingSelectorEntry byHeuristics = CopyingSelectorEntry.CreateByHeuristics(this.IsAllowing, nameof (ForceCopyingForRootCADDocument), "Головной документ копируется всегда", session.Graph.RootVertext);
    new AddCopyingSelectorEntryRecursive().Invoke(session, session.Graph.RootVertext, byHeuristics);
  }
}
