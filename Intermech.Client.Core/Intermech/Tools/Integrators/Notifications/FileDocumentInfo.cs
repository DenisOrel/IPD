
// Type: Intermech.Tools.Integrators.Notifications.FileDocumentInfo
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Diagnostics;
using System.IO;


namespace Intermech.Tools.Integrators.Notifications;

/// <summary>Контейнер сведений о документе IPS.</summary>
/// <remarks>Реализация является immutable и thread safe.</remarks>
public class FileDocumentInfo
{
  private readonly long objectId;
  private readonly int objectTypeId;
  private readonly string filePath;

  /// <summary>Создает объект.</summary>
  /// <param name="objectId">Идентификатор версии документа</param>
  /// <param name="objectTypeId">Идентификатор типа документа</param>
  /// <param name="filePath">Абсолютный путь к файлу документа</param>
  public FileDocumentInfo(long objectId, int objectTypeId, string filePath)
  {
    if (objectId == 0L)
      throw new ArgumentException("Идентификатор версии документа не задан.", nameof (objectId));
    if (objectTypeId == -1)
      throw new ArgumentException("Идентификатор типа документа не задан.", nameof (objectTypeId));
    if (filePath == null)
      throw new ArgumentNullException(nameof (filePath));
    if (!Path.IsPathRooted(filePath))
      throw new ArgumentException("Путь к файлу документа должен быть указан в абсолютной форме.", nameof (filePath));
    this.objectId = objectId;
    this.objectTypeId = objectTypeId;
    this.filePath = filePath;
  }

  /// <summary>Возвращает идентификатор версии документа.</summary>
  public long ObjectId
  {
    [DebuggerStepThrough] get => this.objectId;
  }

  /// <summary>Возвращает идентификатор типа документа.</summary>
  public int ObjectTypeId
  {
    [DebuggerStepThrough] get => this.objectTypeId;
  }

  /// <summary>Возвращает абсолютный путь к файлу документа.</summary>
  public string FilePath
  {
    [DebuggerStepThrough] get => this.filePath;
  }
}
