// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.OpenFilesReloadService
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.CADInterface.Proxies;
using Intermech.Collections;
using Intermech.ControlFlow;
using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.IO;
using Intermech.Runtime;
using Intermech.Runtime.ComInterop.Proxies;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

/// <summary>
/// Класс вспомогательного сервиса интегратора для закрытия и последующего переоткрытия документов, открытых в CAD-системе.
/// Реализация использует продвинутые техники управления файлами.
/// </summary>
/// <remarks>Реализация является thread safe.</remarks>
public class OpenFilesReloadService
{
  private IIntegrator integrator;
  private bool enableModelConfigurationFiles;
  private ICADInterfaceService cadApiService;
  private static readonly OpenFilesUnloadResult noOpenFilesToUnload = new OpenFilesUnloadResult(true, (object) null);

  /// <summary>Создает объект.</summary>
  /// <param name="integrator">Объект интегратора</param>
  /// <param name="enableModelConfigurationFiles">Признак наличия файлов у конфигураций моделей</param>
  public OpenFilesReloadService(IIntegrator integrator, bool enableModelConfigurationFiles)
  {
    this.integrator = integrator != null ? integrator : throw new ArgumentNullException(nameof (integrator));
    this.enableModelConfigurationFiles = enableModelConfigurationFiles;
    this.cadApiService = ServiceUtils.GetService<ICADInterfaceService>((object) integrator, true);
  }

  private IIntegrator Integrator
  {
    [DebuggerStepThrough] get => this.integrator;
  }

  private bool EnableModelConfigurationFiles
  {
    [DebuggerStepThrough] get => this.enableModelConfigurationFiles;
  }

  /// <summary>Выгружает все файлы, открытые в CAD-системе.</summary>
  /// <returns>Результат выполнения операции</returns>
  public OpenFilesUnloadResult UnloadAll()
  {
    if (this.cadApiService.IsApplicationRunning)
    {
      List<OpenFilesReloadService.ReloadItem> unloadableItems = this.PrepareForUnloadAll().Item2;
      if (unloadableItems.Count != 0)
      {
        object reloadState = this.SaveAppState();
        this.UnloadInternal(unloadableItems);
        return new OpenFilesUnloadResult(this.GetReloadItems(false).Count == 0, reloadState);
      }
    }
    return OpenFilesReloadService.noOpenFilesToUnload;
  }

  private (OpenFilesReloadService.UnloadAllMode, List<OpenFilesReloadService.ReloadItem>) PrepareForUnloadAll()
  {
    List<OpenFilesReloadService.ReloadItem> reloadItems = this.GetReloadItems(true);
    return reloadItems.Count != 0 ? (OpenFilesReloadService.UnloadAllMode.VisiblesFirst, reloadItems) : (OpenFilesReloadService.UnloadAllMode.InvisiblesFirst, this.GetReloadItems(false));
  }

  private void UnloadInternal(
    List<OpenFilesReloadService.ReloadItem> unloadableItems)
  {
    foreach (OpenFilesReloadService.ReloadItem unloadableItem in unloadableItems)
      unloadableItem.PrepareForClose();
    foreach (OpenFilesReloadService.ReloadItem unloadableItem in unloadableItems)
    {
      try
      {
        unloadableItem.Close(false);
      }
      catch (Exception ex)
      {
        string currentMethodName = this.GetCurrentMethodName(nameof (UnloadInternal));
        SuppressedExceptions.TraceException(ex, currentMethodName);
      }
    }
  }

  private List<OpenFilesReloadService.ReloadItem> GetReloadItems(bool openVisible)
  {
    List<OpenFilesReloadService.ReloadItem> resultList = new List<OpenFilesReloadService.ReloadItem>(openVisible ? 8 : 128 /*0x80*/);
    using (CADApiSession cadApiSession = new CADApiSession((IApplicationApiService) this.cadApiService))
      this.CollectReloadableDocuments(cadApiSession.Application, openVisible, resultList);
    return resultList;
  }

  private void CollectReloadableDocuments(
    CADSystemProxy cadSystem,
    bool openVisible,
    List<OpenFilesReloadService.ReloadItem> resultList)
  {
    foreach (CADDocumentProxy openDocument in cadSystem.GetOpenDocuments(openVisible))
      resultList.Add(new OpenFilesReloadService.ReloadItem(this, openDocument));
  }

  /// <summary>Загружает обратно в CAD-систему закрытые ранее файлы.</summary>
  /// <param name="reloadState">Объект состояния, позволяющие переоткрыть закрытые файлы</param>
  public void Reload(object reloadState)
  {
    if (reloadState == null || !this.cadApiService.IsApplicationRunning)
      return;
    this.RestoreAppState((ApplicationVisualState<CADSystemProxy>) reloadState);
  }

  private object SaveAppState()
  {
    using (CADApiSession cadApiSession = new CADApiSession((IApplicationApiService) this.cadApiService))
      return (object) cadApiSession.Application.SaveVisualState(CADSystemVisualStateFlags.All);
  }

  private void RestoreAppState(
    ApplicationVisualState<CADSystemProxy> savedAppState)
  {
    using (new DynamicScope())
    {
      IntegratorVars.ConserveAppResources.Declare(false);
      using (CADApiSession cadApiSession = new CADApiSession((IApplicationApiService) this.cadApiService))
        cadApiSession.Application.RestoreVisualState(savedAppState);
    }
  }

  private enum UnloadAllMode
  {
    VisiblesFirst,
    InvisiblesFirst,
  }

  private sealed class ReloadItem
  {
    private readonly OpenFilesReloadService parentService;
    private readonly CADDocumentProxy cadDocument;
    private readonly CADSystemProxy cadSystem;
    private string cadDocumentPath;
    private List<string> ownFilesCache;
    private List<string> dependencyFilesCache;

    public ReloadItem(OpenFilesReloadService parentService, CADDocumentProxy cadDocument)
    {
      this.parentService = parentService;
      this.cadDocument = cadDocument;
      this.cadSystem = cadDocument.CADSystem;
    }

    private List<string> OwnFiles
    {
      get
      {
        if (this.ownFilesCache == null)
        {
          this.ownFilesCache = new List<string>();
          this.ownFilesCache.Add(this.cadDocument.FullName);
          if (this.parentService.EnableModelConfigurationFiles)
            this.ownFilesCache.AddRange<string>((IEnumerable<string>) this.cadDocument.GetSatelliteFiles());
        }
        return this.ownFilesCache;
      }
    }

    private List<string> DependencyFiles
    {
      get
      {
        if (this.dependencyFilesCache == null)
          this.dependencyFilesCache = new List<string>((IEnumerable<string>) this.cadDocument.GetDependencyFiles(true).Item1);
        return this.dependencyFilesCache;
      }
    }

    public bool ContainsFile(string fullName, bool includeDependencies)
    {
      if (string.IsNullOrEmpty(fullName))
        throw new ArgumentException("Не задано имя файла.", nameof (fullName));
      foreach (string ownFile in this.OwnFiles)
      {
        if (PathUtils.IsSamePath(ownFile, fullName))
          return true;
      }
      if (includeDependencies)
      {
        foreach (string dependencyFile in this.DependencyFiles)
        {
          if (PathUtils.IsSamePath(dependencyFile, fullName))
            return true;
        }
      }
      return false;
    }

    public bool ContainsAnyFile(IEnumerable<string> files, bool includeDependencies)
    {
      if (files == null)
        throw new ArgumentNullException(nameof (files));
      foreach (string file in files)
      {
        if (this.ContainsFile(file, includeDependencies))
          return true;
      }
      return false;
    }

    public bool ContainsAnyFile(
      OpenFilesReloadService.ReloadItem otherItem,
      bool includeDependencies)
    {
      if (otherItem == null)
        throw new ArgumentNullException(nameof (otherItem));
      return this.ContainsAnyFile((IEnumerable<string>) otherItem.OwnFiles, includeDependencies);
    }

    public void PrepareForClose()
    {
      if (this.cadDocumentPath != null)
        return;
      this.cadDocumentPath = this.cadDocument.FullName;
    }

    public void Close(bool saveChanges)
    {
      if (this.cadSystem.GetDocumentOpenStatus(this.cadDocumentPath) == CADDocumentOpenStatus.NotOpen)
        return;
      if (saveChanges && !this.cadDocument.ReadOnly)
        this.cadDocument.Save();
      this.cadDocument.Close();
    }
  }
}
