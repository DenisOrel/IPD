// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Model.Operations.DenyCopyingForMinorMaterialModels
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Model.Operations;

internal sealed class DenyCopyingForMinorMaterialModels : CopyingSelectorHeuristics
{
  private ICollection<int> allMaterialsTypes;

  public DenyCopyingForMinorMaterialModels(ICollection<int> allMaterialsTypes)
    : base(false)
  {
    this.allMaterialsTypes = allMaterialsTypes != null ? allMaterialsTypes : throw new ArgumentNullException(nameof (allMaterialsTypes));
  }

  protected override void DoApply(CopyingSession session)
  {
    base.DoApply(session);
    ICollection<DBObjectGraphVertex> allVertices = session.Graph.GetAllVertices((Predicate<DBObjectGraphVertex>) (x => this.allMaterialsTypes.Contains(x.ObjectTypeId)));
    AddCopyingSelectorEntryRecursive selectorEntryRecursive = new AddCopyingSelectorEntryRecursive();
    foreach (DBObjectGraphVertex vertex in (IEnumerable<DBObjectGraphVertex>) allVertices)
    {
      foreach (DBObjectGraphEdge outEdge in (IEnumerable<DBObjectGraphEdge>) session.Graph.GetOutEdges(vertex, (Predicate<DBObjectGraphEdge>) (x => x.Target.IsDocument())))
      {
        CopyingSelectorEntry byHeuristics = CopyingSelectorEntry.CreateByHeuristics(this.IsAllowing, nameof (DenyCopyingForMinorMaterialModels), "Не допускается копирование пользовательских моделей неосновных материалов", outEdge.Target);
        selectorEntryRecursive.Invoke(session, outEdge.Target, byHeuristics);
        if (selectorEntryRecursive.Errors.Count != 0)
          this.ErrorsBuilder.AddErrors(selectorEntryRecursive.Errors);
      }
    }
  }
}
