// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Model.Operations.ArticlesValidator
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Model.Operations;

internal sealed class ArticlesValidator : IObjectValidator<DBObjectGraph>
{
  public IEnumerable<OperationError> Validate(DBObjectGraph sessionGraph, ValidationContext context)
  {
    if (sessionGraph == null)
      throw new ArgumentNullException(nameof (sessionGraph));
    if (context == null)
      throw new ArgumentNullException(nameof (context));
    foreach (DBObjectGraphVertex dbArticleVertex in (IEnumerable<DBObjectGraphVertex>) sessionGraph.GetAllVertices((Predicate<DBObjectGraphVertex>) (x => x.IsArticle())))
    {
      if (!this.IsValidTraitStructure(dbArticleVertex))
        yield return new OperationError($"У изделия '{dbArticleVertex.Caption}' (ид. версии {dbArticleVertex.ObjectId}) обнаружена некорректная внутренняя структура данных.", vertex: dbArticleVertex);
      if (sessionGraph.GetInEdges(dbArticleVertex).Count != 0)
        yield return new OperationError($"У изделия '{dbArticleVertex.Caption}' (ид. версии {dbArticleVertex.ObjectId}) не должно быть входящих ребер в графе связей между объектами IPS.", vertex: dbArticleVertex);
      ICollection<DBObjectGraphEdge> outEdges = sessionGraph.GetOutEdges(dbArticleVertex);
      if (outEdges.Count == 0)
        yield return new OperationError($"У изделия '{dbArticleVertex.Caption}' (ид. версии {dbArticleVertex.ObjectId}) обязательно должны быть исходящие ребра в графе связей между объектами IPS.", vertex: dbArticleVertex);
      foreach (DBObjectGraphEdge edge in (IEnumerable<DBObjectGraphEdge>) outEdges)
      {
        if (!edge.Target.IsDocument())
          yield return new OperationError($"У изделия '{dbArticleVertex.Caption}' (ид. версии {dbArticleVertex.ObjectId}) исходящие ребра в графе между объектами IPS должны вести к документами. А объект '{edge.Target.Caption}' (ид. версии {edge.Target.ObjectId}) не является документом.", vertex: dbArticleVertex);
        if (!edge.IsArticleDocumentation())
          yield return new OperationError($"У изделия '{dbArticleVertex.Caption}' (ид. версии {dbArticleVertex.ObjectId}) на исходящем ребре к документу '{edge.Target.Caption}' (ид. версии {edge.Target.ObjectId}) отсутствует обязательная черта (trait) {"ArticleDocumentationTrait"}.", vertex: dbArticleVertex);
      }
      outEdges = (ICollection<DBObjectGraphEdge>) null;
    }
  }

  private bool IsValidTraitStructure(DBObjectGraphVertex dbObjectVertex)
  {
    return dbObjectVertex.IsArticle() && !dbObjectVertex.IsDocument() && !dbObjectVertex.IsCADModelDrawing();
  }
}
