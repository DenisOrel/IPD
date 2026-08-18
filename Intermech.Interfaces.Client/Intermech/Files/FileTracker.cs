// Decompiled with JetBrains decompiler
// Type: Intermech.Files.FileTracker
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.IO;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Files;

/// <summary>
/// Реализует трекер состояний для файлов объектов, извлеченных на локальный диск. Он используется для определения характера изменений в таких файлах.
/// Трекер позволяет получить только состояние самого файла. Его нельзя использовать для определения факта наличия/отсутствия файлов у объекта,
/// а также для получения списка файлов объекта.
/// </summary>
/// <remarks>
/// Необходимость в подобном трекере возникла из-за того, что дату модификации файла, хранящуюся в файловом атрибуте объекта в базе, нельзя использовать
/// в качестве точки отсчета.
/// </remarks>
public class FileTracker
{
  private readonly PathDictionary<ICollection<long>> fileOwners;
  private readonly Dictionary<Tuple<long, StringKey>, DateTime> fileCheckpoints;

  /// <summary>Создает объект.</summary>
  public FileTracker()
    : this(1024 /*0x0400*/)
  {
  }

  /// <summary>Создает объект.</summary>
  /// <param name="capacity">Начальная емкость внутренних таблиц трекера в количестве файлов</param>
  public FileTracker(int capacity)
  {
    this.fileOwners = capacity > 0 ? new PathDictionary<ICollection<long>>(capacity) : throw new ArgumentOutOfRangeException(nameof (capacity));
    this.fileCheckpoints = new Dictionary<Tuple<long, StringKey>, DateTime>(capacity);
  }

  /// <summary>
  /// Сохраняет состояние файла объекта, скопированного из базы на локальный диск, либо записанного обратно в базу с диска.
  /// </summary>
  /// <param name="objectId">Идентификатор версии объекта, которому принадлежит этот файл</param>
  /// <param name="fileState">Состояние файла объекта</param>
  public void SaveFileState(long objectId, FileState fileState)
  {
    if (objectId == 0L)
      throw new ArgumentException();
    if (fileState == null)
      throw new ArgumentNullException(nameof (fileState));
    this.SaveFileState(objectId, fileState.FileName, fileState.LastWriteTimeUtc);
  }

  /// <summary>
  /// Сохраняет состояние файла объекта, скопированного из базы на локальный диск, либо записанного обратно в базу с диска.
  /// </summary>
  /// <param name="objectId">Идентификатор версии объекта, которому принадлежит этот файл</param>
  /// <param name="fileName">Имя файла</param>
  /// <param name="lastWriteTimeUtc">Дата последней модификации файла</param>
  public void SaveFileState(long objectId, string fileName, DateTime lastWriteTimeUtc)
  {
    if (objectId == 0L)
      throw new ArgumentException();
    if (fileName == null)
      throw new ArgumentNullException(nameof (fileName));
    this.ValidateFileName(fileName);
    this.DoSaveFileState(objectId, fileName, lastWriteTimeUtc);
  }

  /// <summary>
  /// Реализует сохранение состояния файла объекта, скопированного из базы на локальный диск, либо записанного обратно в базу с диска.
  /// </summary>
  /// <param name="objectId">Идентификатор версии объекта, которому принадлежит этот файл</param>
  /// <param name="fileName">Имя файла</param>
  /// <param name="lastWriteTimeUtc">Дата последней модификации файла</param>
  protected virtual void DoSaveFileState(long objectId, string fileName, DateTime lastWriteTimeUtc)
  {
    ICollection<long> longs;
    if (!this.fileOwners.TryGetValue(fileName, out longs))
    {
      longs = (ICollection<long>) new List<long>();
      this.fileOwners.Add(fileName, longs);
    }
    if (!longs.Contains(objectId))
      longs.Add(objectId);
    this.fileCheckpoints[Tuple.Create<long, StringKey>(objectId, new StringKey(fileName))] = lastWriteTimeUtc;
  }

  /// <summary>Удаляет сохраненное состояние для указанного файла.</summary>
  /// <param name="fileName">Имя файла</param>
  public void RemoveFileState(string fileName)
  {
    if (fileName == null)
      throw new ArgumentNullException(nameof (fileName));
    this.ValidateFileName(fileName);
    this.DoRemoveFileState(fileName);
  }

  /// <summary>
  /// Реализует удаление сохраненного состояния для указанного файла.
  /// </summary>
  /// <param name="fileName">Имя файла</param>
  protected virtual void DoRemoveFileState(string fileName)
  {
    ICollection<long> longs;
    if (!this.fileOwners.TryGetValue(fileName, out longs))
      return;
    this.fileOwners.Remove(fileName);
    foreach (long num in (IEnumerable<long>) longs)
      this.fileCheckpoints.Remove(Tuple.Create<long, StringKey>(num, new StringKey(fileName)));
  }

  /// <summary>
  /// Возвращает дату последней модификации для указанного файла.
  /// </summary>
  /// <param name="objectId">Идентификатор версии объекта, которому принадлежит этот файл</param>
  /// <param name="fileName">Имя файла</param>
  /// <returns>Дата последней модификации файла</returns>
  public DateTime? TryGetLastWriteTime(long objectId, string fileName)
  {
    if (objectId == 0L)
      throw new ArgumentException();
    if (fileName == null)
      throw new ArgumentNullException(nameof (fileName));
    this.ValidateFileName(fileName);
    return this.DoGetLastWriteTime(objectId, fileName);
  }

  /// <summary>
  /// Реализует получение даты последней модификации для указанного файла.
  /// </summary>
  /// <param name="objectId">Идентификатор версии объекта, которому принадлежит этот файл</param>
  /// <param name="fileName">Имя файла</param>
  /// <returns>Дата последней модификации файла</returns>
  protected virtual DateTime? DoGetLastWriteTime(long objectId, string fileName)
  {
    DateTime dateTime;
    return this.fileCheckpoints.TryGetValue(Tuple.Create<long, StringKey>(objectId, new StringKey(fileName)), out dateTime) ? new DateTime?(dateTime) : new DateTime?();
  }

  /// <summary>
  /// Позволяет выполнить проверку корректности имени файла объекта.
  /// </summary>
  /// <param name="fileName">Имя файла объекта</param>
  protected virtual void ValidateFileName(string fileName)
  {
  }
}
