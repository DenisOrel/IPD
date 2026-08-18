
// Type: Intermech.Files.TrackUploadedFileAction
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.ControlFlow;
using Intermech.Interfaces.Data;
using System;
using System.Collections.Generic;


namespace Intermech.Files;

/// <summary>
/// Реализует в виде объекта обновление трекера состояний файлов после записи измененного файла объекта с локального диска в базу IPS.
/// </summary>
public class TrackUploadedFileAction : IAction
{
  private readonly FileTracker fileTracker;
  private readonly IDBObjectRef objectRef;
  private readonly IObjectFilesUploadResult uploadResult;

  /// <summary>Создает объект.</summary>
  /// <param name="fileTracker">Файловый трекер</param>
  /// <param name="objectRef">Ссылка на версию объекта, которому принадлежат файлы</param>
  /// <param name="uploadResult">Сведения о записанных в базу IPS файлах</param>
  public TrackUploadedFileAction(
    FileTracker fileTracker,
    IDBObjectRef objectRef,
    IObjectFilesUploadResult uploadResult)
  {
    if (fileTracker == null)
      throw new ArgumentNullException(nameof (fileTracker));
    if (objectRef == null)
      throw new ArgumentNullException(nameof (objectRef));
    if (uploadResult == null)
      throw new ArgumentNullException(nameof (uploadResult));
    this.fileTracker = fileTracker;
    this.objectRef = objectRef;
    this.uploadResult = uploadResult;
  }

  /// <summary>Создает объект.</summary>
  /// <param name="fileTracker">Файловый трекер</param>
  /// <param name="objectId">Идентификатор версии объекта, которому принадлежат файлы</param>
  /// <param name="uploadResult">Сведения о записанных в базу IPS файлах</param>
  public TrackUploadedFileAction(
    FileTracker fileTracker,
    long objectId,
    IObjectFilesUploadResult uploadResult)
    : this(fileTracker, (IDBObjectRef) new DirectDBObjectRef(objectId), uploadResult)
  {
  }

  /// <summary>Выполняет действие.</summary>
  public void Perform()
  {
    if (this.uploadResult.UploadedFileStates == null || this.uploadResult.UploadedFileStates.Count == 0)
      return;
    long objectId = this.objectRef.GetObjectId();
    foreach (FileState uploadedFileState in (IEnumerable<FileState>) this.uploadResult.UploadedFileStates)
      this.fileTracker.SaveFileState(objectId, uploadedFileState);
  }
}
