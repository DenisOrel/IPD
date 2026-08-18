// Decompiled with JetBrains decompiler
// Type: Intermech.Files.FileImportResult
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.Files;

/// <summary>Результат импорта файла в базу данных IPS.</summary>
[Serializable]
public abstract class FileImportResult
{
  /// <summary>Создает объект.</summary>
  /// <param name="filePath">Путь к импортируемому файлу</param>
  /// <exception cref="T:ArgumentException">Параметр <paramref name="filePath" /> не должен быть равен null или пустой строке.</exception>
  internal FileImportResult(string filePath)
  {
    this.FilePath = !string.IsNullOrEmpty(filePath) ? filePath : throw new ArgumentException("Не задан путь к импортируемому файлу.", nameof (filePath));
  }

  /// <summary>Возвращает путь к импортируемому файлу.</summary>
  public string FilePath { get; private set; }

  [Serializable]
  public sealed class Success : FileImportResult
  {
    private List<string> deferredFiles;
    private List<FileImportResult> relatedErrors;

    /// <summary>Создает объект.</summary>
    /// <param name="filePath">Путь к импортируемому файлу</param>
    /// <param name="objectId">Идентификатор версии объекта IPS</param>
    /// <exception cref="T:ArgumentException">Параметр <paramref name="filePath" /> не должен быть равен null или пустой строке. Параметр <paramref name="objectId" /> не задан.</exception>
    public Success(string filePath, long objectId)
      : base(filePath)
    {
      this.ObjectId = objectId != 0L ? objectId : throw new ArgumentException("Не задан идентификатор версии объекта IPS.", nameof (objectId));
    }

    /// <summary>
    /// Возвращает задает идентификатор версии объекта IPS, который был создан в процессе импорта файла.
    /// </summary>
    public long ObjectId { get; private set; }

    /// <summary>
    /// Возвращает коллекцию абсолютных путей для непосредственных ссылочных зависимостей текущего импортированного файла,
    /// импорт которых был отложен на неопределенное время. В процессе импорта текущего файла для каждого такой зависимости
    /// в базе данных IPS был создан черновик документа, на который ссылается текущий импортированный объект IPS.
    /// </summary>
    public List<string> DeferredFiles
    {
      [DebuggerStepThrough] get
      {
        if (this.deferredFiles == null)
          this.deferredFiles = new List<string>();
        return this.deferredFiles;
      }
    }

    /// <summary>
    /// Возвращает коллекцию ошибок импорта для любых ссылочных зависимостей текущего импортированного файла.
    /// Если свойство не пусто, значит текущий файл импортирован не полностью, а частично.
    /// </summary>
    public List<FileImportResult> RelatedErrors
    {
      [DebuggerStepThrough] get
      {
        if (this.relatedErrors == null)
          this.relatedErrors = new List<FileImportResult>();
        return this.relatedErrors;
      }
    }
  }

  [Serializable]
  public sealed class Error : FileImportResult
  {
    /// <summary>Создает объект.</summary>
    /// <param name="filePath">Путь к импортируемому файлу</param>
    /// <param name="exception">Объект исключения, ставшего причиной ошибки импорта файла</param>
    /// <exception cref="T:ArgumentException">Параметр <paramref name="filePath" /> не должен быть равен null или пустой строке.</exception>
    /// <exception cref="T:ArgumentNullException">Параметр <paramref name="exception" /> не должен быть равен null</exception>
    public Error(string filePath, Exception exception)
      : base(filePath)
    {
      this.Exception = exception != null ? exception : throw new ArgumentNullException(nameof (exception));
    }

    /// <summary>
    /// Возвращает исключение, ставшее причиной ошибки импорта файла.
    /// </summary>
    public Exception Exception { get; private set; }
  }

  [Serializable]
  public sealed class IgnoredFile : FileImportResult
  {
    /// <summary>Создает объект.</summary>
    /// <param name="filePath">Путь к импортируемому файлу</param>
    /// <param name="reason">Причина, по которой файл не может быть импортирован</param>
    /// <exception cref="T:ArgumentException">Параметр <paramref name="filePath" /> не должен быть равен null или пустой строке. Параметр <paramref name="reason" /> не должен быть равен null или пустой строке.</exception>
    public IgnoredFile(string filePath, string reason)
      : base(filePath)
    {
      this.Reason = !string.IsNullOrEmpty(reason) ? reason : throw new ArgumentException("Не задана причина, по которой файл не может быть импортирован.", nameof (reason));
    }

    /// <summary>
    /// Возвращает причину, по которой файл не может быть импортирован.
    /// </summary>
    public string Reason { get; private set; }
  }

  [Serializable]
  public sealed class AlreadyImportedFile : FileImportResult
  {
    /// <summary>Создает объект.</summary>
    /// <param name="filePath">Путь к импортируемому файлу</param>
    /// <param name="objectId">Идентификатор версии объекта IPS</param>
    /// <exception cref="T:ArgumentException">Параметр <paramref name="filePath" /> не должен быть равен null или пустой строке. Параметр <paramref name="objectId" /> не задан.</exception>
    public AlreadyImportedFile(string filePath, long objectId)
      : base(filePath)
    {
      this.ObjectId = objectId != 0L ? objectId : throw new ArgumentException("Не задан идентификатор версии объекта IPS.", nameof (objectId));
    }

    /// <summary>
    /// Возвращает задает идентификатор версии объекта IPS, который был создан в процессе импорта файла.
    /// </summary>
    public long ObjectId { get; private set; }
  }
}
