// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Model.Operations.ScannedVerticesValidator
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Collections;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Model.Operations;

internal sealed class ScannedVerticesValidator : IObjectValidator<DBObjectGraph>
{
  public IEnumerable<OperationError> Validate(DBObjectGraph sessionGraph, ValidationContext context)
  {
    if (sessionGraph == null)
      throw new ArgumentNullException(nameof (sessionGraph));
    if (context == null)
      throw new ArgumentNullException(nameof (context));
    foreach (DBObjectGraphVertex dbObjectVertex in (IEnumerable<DBObjectGraphVertex>) sessionGraph.GetAllVertices((Predicate<DBObjectGraphVertex>) (x => x.IsScanned)))
    {
      if (dbObjectVertex.Content.IsEmpty())
        yield return new OperationError($"У документа '{dbObjectVertex.Content}' (ид.версии {dbObjectVertex.ObjectId}) должно быть заполнено свойство {"Content"}.", vertex: dbObjectVertex);
      if (dbObjectVertex.Content.IsCADDocument && dbObjectVertex.Files.Count == 0)
        yield return new OperationError($"У документа '{dbObjectVertex.Content}' (ид.версии {dbObjectVertex.ObjectId}) должен быть хотя бы один файл. Проверьте корректность заполнения свойство {"Content"} и {"Files"}.", vertex: dbObjectVertex);
      if (dbObjectVertex.Files.Count != 0)
      {
        foreach (DBObjectFileEntry file in (IEnumerable<DBObjectFileEntry>) dbObjectVertex.Files)
        {
          if (file.Content == null)
            yield return new OperationError(string.Format("У документа '{0}' (ид.версии {1}) у файла должно быть заполнено свойство {2}.", (object) dbObjectVertex.Content, (object) dbObjectVertex.ObjectId, (object) file.OriginalName, (object) "Content"), vertex: dbObjectVertex);
        }
        DBObjectFileEntry firstFileRecord = dbObjectVertex.Files[0];
        if (!firstFileRecord.Content.IsMainFile)
          yield return new OperationError($"У документа '{dbObjectVertex.Content}' (ид.версии {dbObjectVertex.ObjectId}) первый файл должен быть основным файлом документа. Проверьте корректность заполнения свойства {"Content"} у файла '{firstFileRecord.OriginalName}'.", vertex: dbObjectVertex);
        int index = CollectionUtils.IndexOf<DBObjectFileEntry>((IEnumerable<DBObjectFileEntry>) dbObjectVertex.Files, 1, (Predicate<DBObjectFileEntry>) (x => x.Content.IsMainFile));
        if (index >= 1)
          yield return new OperationError($"У документа '{dbObjectVertex.Content}' (ид.версии {dbObjectVertex.ObjectId}) может быть только один основной файл. Проверьте корректность заполнения свойства {"Content"} у файла '{dbObjectVertex.Files[index].OriginalName}'.", vertex: dbObjectVertex);
        if (dbObjectVertex.Content.IsCADDocument && !firstFileRecord.Content.IsCADFile)
          yield return new OperationError($"У документа '{dbObjectVertex.Content}' (ид.версии {dbObjectVertex.ObjectId}) первый файл должен быть файлом CAD-системы. Проверьте корректность заполнения свойства {"Content"} у файла '{firstFileRecord.OriginalName}'.", vertex: dbObjectVertex);
        firstFileRecord = (DBObjectFileEntry) null;
      }
    }
  }
}
