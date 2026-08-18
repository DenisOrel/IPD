
// Type: Intermech.Tools.Integrators.ApplicationApiResourceManager
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Diagnostics;
using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace Intermech.Tools.Integrators;

/// <summary>
/// Позволяет реализовать менеджер ресурсов приложения, использованных интегратором в сессии подключения к API приложения.
/// Менеджер позволяет сохранить информацию о ресурсах приложения (COM-объекты и др.), открытых интегратором, а при
/// закрытии сессии подключения к API приложения - освободить записанные ресурсы.
/// </summary>
public class ApplicationApiResourceManager
{
  private readonly List<ErrorInfo> errors;
  private IErrorReporter errorReporter;
  private bool isStarted;

  /// <summary>Создает объект.</summary>
  public ApplicationApiResourceManager() => this.errors = new List<ErrorInfo>();

  /// <summary>
  /// Возвращает список ошибок, произошедших в последней операции освобождения ресурсов приложения.
  /// </summary>
  public List<ErrorInfo> Errors
  {
    [DebuggerStepThrough] get => this.errors;
  }

  /// <summary>
  /// Возвращает или задает объект для вывода ошибок, произошедших в последней операции освобождения ресурсов приложения.
  /// Может быть не задан, если вывод ошибок не требуется.
  /// </summary>
  public IErrorReporter ErrorReporter
  {
    [DebuggerStepThrough] get => this.errorReporter;
    [DebuggerStepThrough] set => this.errorReporter = value;
  }

  /// <summary>
  /// Активирует сохранение информации о ресурсах приложения (COM-объекты и др.), открытых интегратором.
  /// </summary>
  public void Start()
  {
    if (this.isStarted)
      throw new InvalidOperationException($"Object '{this.GetType()}' is already started");
    this.DoStart();
    this.isStarted = true;
    this.errors.Clear();
  }

  /// <summary>
  /// Освобождает ресурсы приложения, открытые интегратором, а также деактивирует сохранение информации об открытых ресурсах приложения.
  /// </summary>
  public void ReleaseResourcesAndStop()
  {
    if (!this.isStarted)
      return;
    this.isStarted = false;
    this.SilentlyReleaseResourcesAndStop();
    if (this.Errors.Count == 0)
      return;
    this.ReportErrors();
  }

  private void SilentlyReleaseResourcesAndStop()
  {
    try
    {
      this.DoReleaseResourcesAndStop();
    }
    catch (Exception ex)
    {
      this.Errors.Add(ErrorInfo.FromException(ex, $"Необработанное исключение при закрытии ресурсов приложения в менеджере '{this.GetType()}'."));
    }
  }

  /// <summary>
  /// Активирует сохранение информации о ресурсах приложения (COM-объекты и др.), открытых интегратором.
  /// </summary>
  protected virtual void DoStart()
  {
  }

  /// <summary>
  /// Освобождает ресурсы приложения, открытые интегратором, а также деактивирует сохранение информации об открытых ресурсах приложения.
  /// Метод не должен сбрасывать исключения. Все ошибки освобождения ресурсов приложения должны сохраняться в коллекции Errors.
  /// </summary>
  protected virtual void DoReleaseResourcesAndStop()
  {
  }

  /// <summary>
  /// Позволяет обработать ошибки освобождения ресурсов интегратора.
  /// </summary>
  protected virtual void ReportErrors()
  {
    if (this.errorReporter == null)
      return;
    this.errorReporter.ReportErrors((ICollection<ErrorInfo>) this.Errors);
  }
}
