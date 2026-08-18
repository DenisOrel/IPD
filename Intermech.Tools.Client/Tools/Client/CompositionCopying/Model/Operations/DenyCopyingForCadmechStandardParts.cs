// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Model.Operations.DenyCopyingForCadmechStandardParts
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Model.Operations;

internal sealed class DenyCopyingForCadmechStandardParts : CopyingSelectorHeuristics
{
  private int standardPartObjectTypeId;

  public DenyCopyingForCadmechStandardParts(int standardPartObjectTypeId)
    : base(false)
  {
    this.standardPartObjectTypeId = standardPartObjectTypeId != -1 ? standardPartObjectTypeId : throw new ArgumentException("Не задан идентификатор типа моделей стандартных CADMECH.", nameof (standardPartObjectTypeId));
  }

  protected override void DoApply(CopyingSession session)
  {
    base.DoApply(session);
    AddCopyingSelectorEntryRecursive selectorEntryRecursive = new AddCopyingSelectorEntryRecursive();
    foreach (DBObjectGraphVertex allVertex in (IEnumerable<DBObjectGraphVertex>) session.Graph.GetAllVertices((Predicate<DBObjectGraphVertex>) (x => x.ObjectTypeId == this.standardPartObjectTypeId)))
    {
      CopyingSelectorEntry byHeuristics = CopyingSelectorEntry.CreateByHeuristics(this.IsAllowing, nameof (DenyCopyingForCadmechStandardParts), "Не допускается копирование моделей стандартных изделий CADMECH", allVertex);
      selectorEntryRecursive.Invoke(session, allVertex, byHeuristics);
      if (selectorEntryRecursive.Errors.Count != 0)
        this.ErrorsBuilder.AddErrors(selectorEntryRecursive.Errors);
    }
  }
}
