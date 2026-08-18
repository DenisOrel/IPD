
// Type: Intermech.Files.WorkAreaFilesDifferenceRules
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Diagnostics;


namespace Intermech.Files;

/// <summary>
/// Реализует специальные правила для определения изменений в файлах объектов, опубликованных в рабочей области. Основное назначение этих правил:
/// обеспечить независимость от рассинхронизации хода системных часов клиента и сервера IPS, а также обеспечить корректную работу с объектами,
/// изменяемыми непосредственно в базе IPS.
/// </summary>
/// <remarks>
/// <para>
/// Специальной обработки требуют устаревшие локальные файлы. Причина - в невозможности обеспечить синхронное течение системного времени клиента и сервера IPS
/// Если время клиента отстает от времени сервера или времени другого клиента, именявшего файл, то время модификации файла в базе будет находиться в
/// будущем времени для этого клиента. Поэтому любые изменения локальных файлов все равно будут оказываться в прошлом, а следовательно локальные файлы будут
/// устаревать при любых изменениях.</para>
/// <para>
/// Решение в том, чтобы отказаться от сравнения времен модификации на больше-меньше. Вместо этого следует учитывать состояние самого объекта,
/// время извлечения файла на локальный диск и равенство-неравенство времен модификации файла.</para>
/// </remarks>
internal sealed class WorkAreaFilesDifferenceRules : DBObjectFilesDifferenceRules
{
  private FileTracker fileTracker;

  /// <summary>Создает объект.</summary>
  /// <param name="fileTracker">Трекер состояний файлов объектов IPS</param>
  /// <exception cref="T:ArgumentNullException">fileTracker</exception>
  public WorkAreaFilesDifferenceRules(FileTracker fileTracker)
  {
    this.fileTracker = fileTracker != null ? fileTracker : throw new ArgumentNullException(nameof (fileTracker));
  }

  /// <summary>Возвращает трекер состояний файлов объектов IPS.</summary>
  private FileTracker FileTracker
  {
    [DebuggerStepThrough] get => this.fileTracker;
  }

  /// <summary>
  /// Определяет наличие или отсутствие изменений в локальном файле объекта IPS, сравнивая локальное и удаленное (remote) состояние файла.
  /// </summary>
  /// <param name="utcNow">Текущее время в UTC</param>
  /// <param name="objectState">Версия объекта IPS, чей файл анализируется</param>
  /// <param name="localFileState">Состояние файла на локальном диске. Может быть null, если на диске файл отсутствует, но не одновременно с remoteFileState</param>
  /// <param name="remoteFileState">Состояние файла на базе данных. Может быть null, если в базе файл отсутствует, но не одновременно с localFileState</param>
  /// <returns>Результат сравнения состояний файлов</returns>
  protected override FileDifferencePair DoCalculateDifference(
    DateTime utcNow,
    DBObjectState objectState,
    FileState localFileState,
    FileState remoteFileState)
  {
    if (localFileState != null && remoteFileState != null)
    {
      FileDifferenceType? differenceType = this.CalculateDifferenceType(utcNow, objectState, localFileState, remoteFileState);
      if (differenceType.HasValue)
        return new FileDifferencePair(differenceType.Value, localFileState, remoteFileState);
    }
    return base.DoCalculateDifference(utcNow, objectState, localFileState, remoteFileState);
  }

  private FileDifferenceType? CalculateDifferenceType(
    DateTime utcNow,
    DBObjectState objectState,
    FileState localFileState,
    FileState remoteFileState)
  {
    if (objectState.IsEditableState)
    {
      DateTime? lastWriteTime = this.FileTracker.TryGetLastWriteTime(objectState.ObjectId, localFileState.FileName);
      if (lastWriteTime.HasValue && remoteFileState.CompareTo(lastWriteTime.Value) == 0)
        return new FileDifferenceType?(localFileState.CompareTo(lastWriteTime.Value) == 0 ? FileDifferenceType.UnchangedFile : FileDifferenceType.UpdatedFile);
    }
    return new FileDifferenceType?();
  }
}
