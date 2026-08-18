// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Model.Operations.DenyCopyingForNonCADDocuments
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Model.Operations;

internal sealed class DenyCopyingForNonCADDocuments : CopyingSelectorHeuristics
{
  private readonly ICollection<int> cadDocumentTypes;

  public DenyCopyingForNonCADDocuments(ICollection<int> cadDocumentTypes)
    : base(false)
  {
    this.cadDocumentTypes = cadDocumentTypes;
  }

  protected override void DoApply(CopyingSession session)
  {
    base.DoApply(session);
    AddCopyingSelectorEntryRecursive selectorEntryRecursive = new AddCopyingSelectorEntryRecursive();
    foreach (DBObjectGraphVertex allVertex in (IEnumerable<DBObjectGraphVertex>) session.Graph.GetAllVertices((Predicate<DBObjectGraphVertex>) (x => !this.cadDocumentTypes.Contains(x.ObjectTypeId))))
    {
      CopyingSelectorEntry byHeuristics = CopyingSelectorEntry.CreateByHeuristics(this.IsAllowing, "DenyCopyingForCadmechStandardParts", "Не допускается копирование документов, не обрабатываемых выбранной CAD-системой", allVertex);
      allVertex.CopyingSelector.TryAdd(byHeuristics);
    }
  }
}
