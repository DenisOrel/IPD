// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Model.Operations.DocumentsValidator
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Model.Operations;

internal sealed class DocumentsValidator : IObjectValidator<DBObjectGraph>
{
  public IEnumerable<OperationError> Validate(DBObjectGraph sessionGraph, ValidationContext context)
  {
    if (sessionGraph == null)
      throw new ArgumentNullException(nameof (sessionGraph));
    if (context == null)
      throw new ArgumentNullException(nameof (context));
    foreach (DBObjectGraphVertex allVertex in (IEnumerable<DBObjectGraphVertex>) sessionGraph.GetAllVertices((Predicate<DBObjectGraphVertex>) (x => x.IsDocument())))
    {
      if (!this.IsValidTraitStructure(allVertex))
        yield return new OperationError($"У документа '{allVertex.Caption}' (ид. версии {allVertex.ObjectId}) обнаружена некорректная внутренняя структура данных.", vertex: allVertex);
    }
  }

  private bool IsValidTraitStructure(DBObjectGraphVertex dbObjectVertex)
  {
    return dbObjectVertex.IsDocument() && !dbObjectVertex.IsArticle();
  }
}
