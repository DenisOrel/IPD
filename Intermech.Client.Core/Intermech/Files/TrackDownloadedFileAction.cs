
// Type: Intermech.Files.TrackDownloadedFileAction
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.ControlFlow;
using System;


namespace Intermech.Files;

/// <summary>
/// Реализует в виде объекта обновление трекера состояний файлов после извлечения файла объекта из базы IPS на локальный диск.
/// </summary>
public class TrackDownloadedFileAction : IAction
{
  private readonly FileTracker fileTracker;
  private readonly long objectId;
  private readonly FileState fileState;

  /// <summary>Создает объект.</summary>
  /// <param name="fileTracker">Файловый трекер</param>
  /// <param name="objectId">Идентификатор версии объекта, которому принадлежит этот файл</param>
  /// <param name="fileState">Состояние файла объекта</param>
  public TrackDownloadedFileAction(FileTracker fileTracker, long objectId, FileState fileState)
  {
    if (fileTracker == null)
      throw new ArgumentNullException(nameof (fileTracker));
    if (objectId == 0L)
      throw new ArgumentException();
    if (fileState == null)
      throw new ArgumentNullException(nameof (fileState));
    this.fileTracker = fileTracker;
    this.objectId = objectId;
    this.fileState = fileState;
  }

  /// <summary>Выполняет действие.</summary>
  public void Perform() => this.fileTracker.SaveFileState(this.objectId, this.fileState);
}
