
// Type: Intermech.Tools.Integrators.Notifications.CaptureChangesDocumentInfo
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Diagnostics;


namespace Intermech.Tools.Integrators.Notifications;

/// <summary>Контейнер для сведений о сохраненном документе IPS.</summary>
/// <remarks>Реализация является immutable и thread safe.</remarks>
public sealed class CaptureChangesDocumentInfo : FileDocumentInfo
{
  private readonly bool isInitialDocument;
  private readonly bool isUpdatedOnSave;

  /// <summary>Создает объект.</summary>
  /// <param name="objectId">Идентификатор версии документа</param>
  /// <param name="objectTypeId">Идентификатор типа документа</param>
  /// <param name="filePath">Абсолютный путь к файлу документа</param>
  /// <param name="isInitialDocument">Признак исходного документа, который был выбран пользователем для сохранения изменений</param>
  /// <param name="isUpdatedOnSave">Признак, что документ содержал несохраненные изменения, и эти изменения были записаны в базу данных</param>
  public CaptureChangesDocumentInfo(
    long objectId,
    int objectTypeId,
    string filePath,
    bool isInitialDocument,
    bool isUpdatedOnSave)
    : base(objectId, objectTypeId, filePath)
  {
    this.isInitialDocument = isInitialDocument;
    this.isUpdatedOnSave = isUpdatedOnSave;
  }

  /// <summary>
  /// Возвращает признак исходного документа, который был выбран пользователем для сохранения изменений.
  /// </summary>
  public bool IsInitialDocument
  {
    [DebuggerStepThrough] get => this.isInitialDocument;
  }

  /// <summary>
  /// Возвращает признак, что признак, что документ содержал несохраненные изменения, и эти изменения были записаны в базу данных
  /// </summary>
  public bool IsUpdatedOnSave
  {
    [DebuggerStepThrough] get => this.isUpdatedOnSave;
  }
}
