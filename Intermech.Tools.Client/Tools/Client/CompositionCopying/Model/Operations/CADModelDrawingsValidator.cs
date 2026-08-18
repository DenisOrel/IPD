// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Model.Operations.CADModelDrawingsValidator
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Model.Operations;

internal sealed class CADModelDrawingsValidator : IObjectValidator<DBObjectGraph>
{
  public IEnumerable<OperationError> Validate(DBObjectGraph sessionGraph, ValidationContext context)
  {
    if (sessionGraph == null)
      throw new ArgumentNullException(nameof (sessionGraph));
    if (context == null)
      throw new ArgumentNullException(nameof (context));
    foreach (DBObjectGraphVertex dbDrawingVertex in (IEnumerable<DBObjectGraphVertex>) sessionGraph.GetAllVertices((Predicate<DBObjectGraphVertex>) (x => x.IsCADModelDrawing())))
    {
      if (!this.IsValidTraitStructure(dbDrawingVertex))
        yield return new OperationError($"У чертежа '{dbDrawingVertex.Caption}' (ид. версии {dbDrawingVertex.ObjectId}) некорректная внутренняя структура данных.", vertex: dbDrawingVertex);
      foreach (DBObjectGraphEdge outEdge in (IEnumerable<DBObjectGraphEdge>) sessionGraph.GetOutEdges(dbDrawingVertex))
      {
        if (!outEdge.Target.IsDocument())
          yield return new OperationError($"У чертежа '{dbDrawingVertex.Caption}' (ид. версии {dbDrawingVertex.ObjectId}) исходящие ребра в графе между объектами IPS должны вести к документами. А объект '{outEdge.Target.Caption}' (ид. версии {outEdge.Target.ObjectId}) не является документом.", vertex: dbDrawingVertex);
      }
    }
  }

  private bool IsValidTraitStructure(DBObjectGraphVertex dbObjectVertex)
  {
    return dbObjectVertex.IsDocument() && dbObjectVertex.IsCADModelDrawing() && !dbObjectVertex.IsArticle();
  }
}
