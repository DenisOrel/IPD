
// Type: Intermech.Tools.Integrators.IntegratorOpenFilesHandler`1
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Collections;
using Intermech.Files;
using Intermech.Interfaces;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace Intermech.Tools.Integrators;

public abstract class IntegratorOpenFilesHandler<TApplication> : 
  IOpenFilesServiceExtension,
  IOpenFiles
{
  protected readonly IServiceProvider integrator;
  protected readonly IApplicationFileTypes fileTypeSvc;
  protected readonly IIntegratorOutput outputSvc;
  protected readonly IApplicationApiService apiSvc;

  public IntegratorOpenFilesHandler(IServiceProvider integrator)
  {
    this.integrator = integrator != null ? integrator : throw new ArgumentNullException(nameof (integrator));
    this.fileTypeSvc = ServiceUtils.GetService<IApplicationFileTypes>((object) integrator, true);
    this.outputSvc = ServiceUtils.GetService<IIntegratorOutput>((object) integrator, true);
    this.apiSvc = ServiceUtils.GetService<IApplicationApiService>((object) integrator, true);
  }

  /// <summary>
  /// Возвращает true, если указанный файл открыт в каком-либо приложении.
  /// </summary>
  /// <param name="filePath">Путь к файлу</param>
  /// <returns>Признак того, что файл открыт в приложении</returns>
  /// <exception cref="T:System.ArgumentNullException">Путь к файлу не может быть null</exception>
  public bool IsOpen(string filePath)
  {
    if (filePath == null)
      throw new ArgumentNullException(nameof (filePath));
    if (!Path.IsPathRooted(filePath))
      return false;
    try
    {
      return this.fileTypeSvc.IsApplicationFile(filePath) && this.apiSvc.IsApplicationRunning && this.IsAppFileOpen(filePath);
    }
    catch (Exception ex)
    {
      this.ShowError(ex);
      return false;
    }
  }

  protected abstract bool IsAppFileOpen(string filePath);

  /// <summary>
  /// Возвращает true, если указанный файл открыт в каком-либо приложении и имеет несохраненные изменения.
  /// </summary>
  /// <param name="filePath">Путь к файлу</param>
  /// <returns>Признак того, что файл имеет несохраненные изменения</returns>
  /// <exception cref="T:System.ArgumentNullException">Путь к файлу не может быть null</exception>
  public bool IsDirty(string filePath)
  {
    if (filePath == null)
      throw new ArgumentNullException(nameof (filePath));
    if (!Path.IsPathRooted(filePath))
      return false;
    try
    {
      return this.fileTypeSvc.IsApplicationFile(filePath) && this.apiSvc.IsApplicationRunning && this.IsAppFileDirty(filePath);
    }
    catch (Exception ex)
    {
      this.ShowError(ex);
      return false;
    }
  }

  protected abstract bool IsAppFileDirty(string filePath);

  /// <summary>Сохраняет на диск имеющиеся изменения в файле.</summary>
  /// <param name="filePath">Путь к файлу</param>
  /// <exception cref="T:System.ArgumentNullException">Путь к файлу не может быть null</exception>
  public void Save(string filePath)
  {
    if (filePath == null)
      throw new ArgumentNullException(nameof (filePath));
    if (!Path.IsPathRooted(filePath))
      return;
    try
    {
      if (!this.fileTypeSvc.IsApplicationFile(filePath) || !this.apiSvc.IsApplicationRunning)
        return;
      this.SaveAppFile(filePath);
    }
    catch (Exception ex)
    {
      this.ShowError(ex);
    }
  }

  protected abstract void SaveAppFile(string filePath);

  /// <summary>Управляет возможностью внесения изменений в документ.</summary>
  /// <param name="filePath">Путь к файлу, открытому в приложении</param>
  /// <param name="readOnlyFlag">Значение флага</param>
  public void SetReadOnlyFlag(string filePath, bool readOnlyFlag)
  {
    if (filePath == null)
      throw new ArgumentNullException(nameof (filePath));
    if (!this.IsOpen(filePath))
      return;
    try
    {
      this.SetAppReadOnlyFlag(filePath, readOnlyFlag);
    }
    catch (Exception ex)
    {
      this.ShowError(ex);
    }
  }

  protected virtual void SetAppReadOnlyFlag(string filePath, bool readOnlyFlag)
  {
  }

  /// <summary>
  /// Проверяет, поддерживается ли перезагрузка октрытого файла без его предварительного закрытия. Если файл не открыт, то метод вернет false.
  /// </summary>
  /// <param name="filePath">Путь к файлу</param>
  /// <returns>Признак, что поддерживается перезагрузка октрытого файла без его предварительного закрытия</returns>
  /// <exception cref="T:System.ArgumentNullException">Путь к файлу не может быть null</exception>
  public bool IsReloadable(string filePath)
  {
    if (filePath == null)
      throw new ArgumentNullException(nameof (filePath));
    if (!Path.IsPathRooted(filePath))
      return false;
    try
    {
      return this.fileTypeSvc.IsApplicationFile(filePath) && this.apiSvc.IsApplicationRunning && this.IsAppFileReloadable(filePath);
    }
    catch (Exception ex)
    {
      this.ShowError(ex);
      return false;
    }
  }

  protected virtual bool IsAppFileReloadable(string filePath) => false;

  /// <summary>
  /// Выполняет перезагрузку открытого файла без его предварительного закрытия.
  /// </summary>
  /// <param name="filePath">Путь к файлу</param>
  /// <exception cref="T:System.ArgumentNullException">Путь к файлу не может быть null</exception>
  public void Reload(string filePath)
  {
    if (filePath == null)
      throw new ArgumentNullException(nameof (filePath));
    if (!Path.IsPathRooted(filePath))
      return;
    try
    {
      if (!this.fileTypeSvc.IsApplicationFile(filePath) || !this.apiSvc.IsApplicationRunning)
        return;
      this.ReloadAppFile(filePath);
    }
    catch (Exception ex)
    {
      this.ShowError(ex);
    }
  }

  protected virtual void ReloadAppFile(string filePath) => throw new NotSupportedException();

  /// <summary>
  /// Выгружает из приложений все документы, которые используют указанные файлы. Все несохраненные изменения будут потеряны.
  /// </summary>
  /// <param name="fileList">Список путей к файлам, которые должны быть освобождены приложениями</param>
  /// <returns>Объект с информацией для переоткрытия закрытых документов. Может быть null, если ни один документ не был закрыт</returns>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на список файлов не может быть null</exception>
  public object Unload(IEnumerable<string> fileList)
  {
    if (fileList == null)
      throw new ArgumentNullException(nameof (fileList));
    try
    {
      LinkedList<string> allAsLinkedList = CollectionUtils.FindAllAsLinkedList<string>(fileList, (Predicate<string>) (filePath => Path.IsPathRooted(filePath) && this.fileTypeSvc.IsApplicationFile(filePath)));
      return allAsLinkedList.Count == 0 || !this.apiSvc.IsApplicationRunning ? (object) null : this.UnloadAppFiles((ICollection<string>) allAsLinkedList);
    }
    catch (Exception ex)
    {
      this.ShowError(ex);
      return (object) null;
    }
  }

  protected abstract object UnloadAppFiles(ICollection<string> applicationFiles);

  /// <summary>
  /// Выгружает все открытые в приложениях документы. Все несохраненные изменения будут потеряны.
  /// </summary>
  /// <returns>Объект с информацией для переоткрытия закрытых документов. Может быть null, если ни один документ не был закрыт</returns>
  public object UnloadAll()
  {
    try
    {
      return !this.apiSvc.IsApplicationRunning ? (object) null : this.UnloadAllAppFiles();
    }
    catch (Exception ex)
    {
      this.ShowError(ex);
      return (object) null;
    }
  }

  protected abstract object UnloadAllAppFiles();

  /// <summary>Переоткрывает закрытые ранее документы.</summary>
  /// <param name="reloadState">Объект состояния с информацией для переоткрытия документов</param>
  public void Reload(object reloadState)
  {
    if (reloadState == null)
      return;
    try
    {
      this.ReloadAppState(reloadState);
    }
    catch (Exception ex)
    {
      this.ShowError(ex);
    }
  }

  protected abstract void ReloadAppState(object reloadState);

  private void ShowError(Exception x)
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append(LocalizationHolder.rm.GetString("SR_1619"));
    stringBuilder.Append(' ');
    stringBuilder.Append(x.Message);
    this.outputSvc.WriteLine(stringBuilder.ToString());
  }
}
