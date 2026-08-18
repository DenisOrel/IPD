// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Mechanical.IModelDrawingsImportService
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.IO;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Integrators.Mechanical;

/// <summary>
/// Интерфейс сервиса, обслуживающего задачи импорта чертежей моделей.
/// </summary>
public interface IModelDrawingsImportService
{
  /// <summary>
  /// Возвращает режим импорта новых файлов чертежей в базу данных.
  /// </summary>
  /// <returns>Режим импорта новых файлов чертежей</returns>
  NewDrawingMode GetNewDrawingMode();

  /// <summary>
  /// Позволяет найти все файлы чертежей, связанные с указанным документом модели.
  /// </summary>
  /// <param name="modelDocumentFiles">Список файлов документа модели</param>
  /// <returns>Коллекция найденных файлов чертежей</returns>
  /// <exception cref="T:System.ArgumentNullException">Ни один из аргументов метода не может быть null</exception>
  PathCollection FindAllDrawingFiles(IEnumerable<string> modelDocumentFiles);
}
