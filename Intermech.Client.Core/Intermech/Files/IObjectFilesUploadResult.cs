
// Type: Intermech.Files.IObjectFilesUploadResult
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Collections.Generic;


namespace Intermech.Files;

/// <summary>
/// Позволяет получить информацию о файлах объекта после записи измененных файлов с локального диска в базу IPS.
/// </summary>
public interface IObjectFilesUploadResult
{
  /// <summary>
  /// Возвращает коллекцию состояний файлов непосредственно после записи в базу IPS. Значение свойства может быть null,
  /// если запись файлов еще не была выполнена.
  /// </summary>
  ICollection<FileState> UploadedFileStates { get; }
}
