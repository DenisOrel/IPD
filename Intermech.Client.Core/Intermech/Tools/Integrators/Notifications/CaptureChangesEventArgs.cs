
// Type: Intermech.Tools.Integrators.Notifications.CaptureChangesEventArgs
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace Intermech.Tools.Integrators.Notifications;

/// <summary>
/// Аргументы событий "Сохранить" и "Расширенное сохранение".
/// </summary>
public class CaptureChangesEventArgs : NotificationEventArgs
{
  private const string captureChangesCompleted = "CaptureChangesCompleted";
  private readonly SaveChangesMode mode;
  private readonly bool isExtendedSave;
  private readonly IIntegrator integrator;
  private readonly List<CaptureChangesDocumentInfo> documents;

  /// <summary>Создает объект.</summary>
  /// <param name="eventName">Имя события</param>
  /// <param name="saveChangesMode">Режим сохранения изменений: обычный или перед завершением редактирования</param>
  /// <param name="isExtendedSave">Признак расширенного сохранения</param>
  /// <param name="integrator">Объект интегратора, который выполнял сохранение изменений</param>
  /// <param name="documents">Список документов, сохраненных интегратором</param>
  public CaptureChangesEventArgs(
    string eventName,
    SaveChangesMode saveChangesMode,
    bool isExtendedSave,
    IIntegrator integrator,
    List<CaptureChangesDocumentInfo> documents)
    : base(eventName)
  {
    if (integrator == null)
      throw new ArgumentNullException(nameof (integrator));
    if (documents == null)
      throw new ArgumentNullException(nameof (documents));
    this.mode = saveChangesMode;
    this.isExtendedSave = isExtendedSave;
    this.integrator = integrator;
    this.documents = documents;
  }

  /// <summary>
  /// Возвращает режим сохранения изменений: обычный или перед завершением редактирования.
  /// </summary>
  public SaveChangesMode Mode
  {
    [DebuggerStepThrough] get => this.mode;
  }

  /// <summary>Возвращает признак расширенного сохранения.</summary>
  public bool IsExtendedSave
  {
    [DebuggerStepThrough] get => this.isExtendedSave;
  }

  /// <summary>
  /// Возвращает объект интегратора, который выполнял сохранение изменений.
  /// </summary>
  public IIntegrator Integrator
  {
    [DebuggerStepThrough] get => this.integrator;
  }

  /// <summary>
  /// Список документов, сохраненных интегратором.
  /// Значение свойства может быть пусто, если в обработанных документах не было изменений для записи в базу данных.
  /// </summary>
  public List<CaptureChangesDocumentInfo> Documents
  {
    [DebuggerStepThrough] get => this.documents;
  }

  /// <summary>
  /// Имя события завершения команд "Сохранить" и "Расширенное сохранение".
  /// </summary>
  public static string CaptureChangesCompleted => nameof (CaptureChangesCompleted);
}
