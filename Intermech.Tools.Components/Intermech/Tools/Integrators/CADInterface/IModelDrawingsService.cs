// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.IModelDrawingsService
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.IO;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

/// <summary>
/// Сервис интегратора для определения файлов чертежей, а также поиска чертежей, связанных с 3D-моделями по имени файла.
/// Реализация обязана быть thread safe.
/// </summary>
public interface IModelDrawingsService : IIntegratorService
{
  /// <summary>
  /// Позволяет определить по имени файла, является ли он чертежом.
  /// </summary>
  /// <param name="fileName">Имя файла, путь может быть относительным или отсутствовать</param>
  /// <returns>true, если это файл чертежа</returns>
  /// <exception cref="T:System.ArgumentException">Имя файла не может быть пустым</exception>
  bool IsDrawingFileName(string fileName);

  /// <summary>
  /// Позволяет найти файл чертежа по имени файла 3D-модели.
  /// </summary>
  /// <param name="modelFileName">Имя файла 3D-модели, может содержать абсолютный или относительный путь</param>
  /// <param name="fileExists">Функция для тестирования существования файла с указанным именем файла и путем</param>
  /// <returns>Имя файла найденного чертежа или null</returns>
  /// <exception cref="T:System.ArgumentException">Имя файла 3D-модели не может быть пустым</exception>
  /// <exception cref="T:System.ArgumentNullException">Функция для тестирования существования файла не указана</exception>
  string FindDrawingFile(string modelFileName, Func<string, bool> fileExists);

  /// <summary>
  /// Позволяет найти все файлы чертежей, связанные с указанным документом 3D-модели.
  /// </summary>
  /// <param name="modelDocumentFiles">Список файлов документа 3D-модели</param>
  /// <param name="fileExists">Функция для тестирования существования файла с указанным именем файла и путем</param>
  /// <returns>Коллекция найденных файлов чертежей</returns>
  /// <exception cref="T:System.ArgumentNullException">Ни один из аргументов метода не может быть null</exception>
  PathCollection FindAllDrawingFiles(
    IEnumerable<string> modelDocumentFiles,
    Func<string, bool> fileExists);

  /// <summary>
  /// Позволяет проверить, соответствуют ли друг другу указанные имена файлов чертежа и 3D-модели.
  /// </summary>
  /// <param name="modelFileName">Имя файла 3D-модели</param>
  /// <param name="drawingFileName">Имя файла чертежа</param>
  /// <returns>true, если имена файлов соответствуют друг другу</returns>
  /// <exception cref="T:System.ArgumentException">Имена файлов чертежа и 3D-модели не могут быть пустыми</exception>
  bool IsSourceModelFile(string modelFileName, string drawingFileName);

  /// <summary>
  /// Позволяет найти среди файлов документа 3D-модели тот, который соответствует указанному файлу чертежа.
  /// </summary>
  /// <param name="modelDocumentFiles">Список файлов документа 3D-модели</param>
  /// <param name="drawingFileName">Имя файла чертежа</param>
  /// <returns>Найденный файл 3D-модели или null</returns>
  string FindSourceModelFile(IEnumerable<string> modelDocumentFiles, string drawingFileName);
}
