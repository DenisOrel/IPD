// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Model.Operations.DenyCopyingForUsersStandardParts
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Model.Operations;

internal sealed class DenyCopyingForUsersStandardParts : CopyingSelectorHeuristics
{
  private int standardPartObjectTypeId;

  public DenyCopyingForUsersStandardParts(int standardPartObjectTypeId)
    : base(false)
  {
    this.standardPartObjectTypeId = standardPartObjectTypeId != -1 ? standardPartObjectTypeId : throw new ArgumentException("Не задан идентификатор типа моделей стандартных CADMECH.", nameof (standardPartObjectTypeId));
  }

  protected override void DoApply(CopyingSession session)
  {
    base.DoApply(session);
    ICollection<DBObjectGraphVertex> outEdgesRecursive = session.Graph.GetVerticesByOutEdgesRecursive(session.Graph.RootVertext);
    List<int> allArticlesTypesID = MetaDataHelper.GetObjectTypeChildrenIDRecursive(session.Services.IntegratorsIDCache.AllArticles.Id);
    int standartArticleTypeID = MetaDataHelper.GetObjectTypeID("cad00252-306c-11d8-b4e9-00304f19f545");
    int otherArticleTypeID = MetaDataHelper.GetObjectTypeID("cad0038d-306c-11d8-b4e9-00304f19f545");
    AddCopyingSelectorEntryRecursive selectorEntryRecursive = new AddCopyingSelectorEntryRecursive();
    foreach (DBObjectGraphVertex vertex in (IEnumerable<DBObjectGraphVertex>) outEdgesRecursive)
    {
      ICollection<DBObjectGraphEdge> inEdges = session.Graph.GetInEdges(vertex, (Predicate<DBObjectGraphEdge>) (x => !x.Source.IsCADModelDrawing() && allArticlesTypesID.Contains(x.Source.ObjectTypeId) && x.Target.ObjectTypeId != this.standardPartObjectTypeId));
      if (inEdges.Where<DBObjectGraphEdge>((Func<DBObjectGraphEdge, bool>) (x => x.Source.ObjectTypeId == standartArticleTypeID || x.Source.ObjectTypeId == otherArticleTypeID)).ToList<DBObjectGraphEdge>().Count == inEdges.Count)
      {
        foreach (DBObjectGraphEdge dbObjectGraphEdge in (IEnumerable<DBObjectGraphEdge>) inEdges)
        {
          CopyingSelectorEntry byHeuristics = CopyingSelectorEntry.CreateByHeuristics(this.IsAllowing, nameof (DenyCopyingForUsersStandardParts), "Не допускается копирование пользовательских моделей стандартных изделий", dbObjectGraphEdge.Target);
          selectorEntryRecursive.Invoke(session, dbObjectGraphEdge.Target, byHeuristics);
          if (selectorEntryRecursive.Errors.Count != 0)
            this.ErrorsBuilder.AddErrors(selectorEntryRecursive.Errors);
        }
      }
    }
  }
}
