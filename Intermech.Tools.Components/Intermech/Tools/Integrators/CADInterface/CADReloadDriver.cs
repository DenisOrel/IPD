// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.CADReloadDriver
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.CADInterface.Proxies;
using Intermech.ControlFlow;
using Intermech.IO;
using Intermech.Localization;
using Intermech.Runtime;
using Intermech.Runtime.ComInterop.Proxies;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

internal sealed class CADReloadDriver(IIntegrator owner) : IntegratorService(owner), IReloadDriver
{
  private ICADInterfaceService cadApiService;

  /// <summary>
  /// Возвращает или задает ссылку на сервис для доступа к API приложения. Свойство должно быть заполнено до начала использования текущего сервиса.
  /// </summary>
  public ICADInterfaceService ApiService
  {
    [DebuggerStepThrough] get
    {
      lock (this.Integrator.SyncRoot)
        return this.cadApiService;
    }
    [DebuggerStepThrough] set
    {
      lock (this.Integrator.SyncRoot)
      {
        this.RequireNotInitialized();
        this.cadApiService = value;
      }
    }
  }

  /// <summary>
  /// Проверяет конфигурацию сервиса и выполняет его окончательную инициализацию.
  /// После успешного выполнения этого метода сервис интегратора можно использовать.
  /// </summary>
  /// <exception cref="T:InvalidOperationException">Конфигурация сервиса некорректна</exception>
  protected override void DoInitialize()
  {
    base.DoInitialize();
    if (this.ApiService == null)
      throw PropertyExceptions.PropertyNotSetException((object) this, "ApiService");
  }

  public List<IReloadItem> GetReloadItems()
  {
    this.RequireReadyState();
    List<IReloadItem> reloadItems = new List<IReloadItem>();
    if (this.cadApiService.IsApplicationRunning)
    {
      using (CADApiSession cadApiSession = new CADApiSession((IApplicationApiService) this.cadApiService))
      {
        CADSystemProxy application = cadApiSession.Application;
        reloadItems.AddRange((IEnumerable<IReloadItem>) this.CollectReloadableDocuments(application));
      }
    }
    return reloadItems;
  }

  private List<IReloadItem> CollectReloadableDocuments(CADSystemProxy app)
  {
    List<CADDocumentProxy> openDocuments = app.GetOpenDocuments(false);
    List<IReloadItem> reloadItemList = new List<IReloadItem>(openDocuments.Count);
    for (int index = 0; index < openDocuments.Count; ++index)
    {
      CADDocumentProxy doc = openDocuments[index];
      reloadItemList.Add((IReloadItem) new CADReloadDriver.ReloadItem(doc));
    }
    return reloadItemList;
  }

  public object SaveAppState()
  {
    this.RequireReadyState();
    using (CADApiSession cadApiSession = new CADApiSession((IApplicationApiService) this.cadApiService))
      return (object) cadApiSession.Application.SaveVisualState(CADSystemVisualStateFlags.All);
  }

  public void RestoreAppState(object reloadState)
  {
    this.RequireReadyState();
    using (new DynamicScope())
    {
      IntegratorVars.ConserveAppResources.Declare(false);
      using (CADApiSession cadApiSession = new CADApiSession((IApplicationApiService) this.cadApiService))
        cadApiSession.Application.RestoreVisualState((ApplicationVisualState<CADSystemProxy>) reloadState);
    }
  }

  private class ReloadItem : IReloadItem
  {
    private CADSystemProxy cadSystem;
    private string documentFullPath;
    private List<string> allFiles;

    public ReloadItem(CADDocumentProxy doc)
    {
      this.cadSystem = doc.CADSystem;
      this.documentFullPath = doc.FullName;
      this.allFiles = doc.GetAllFiles();
    }

    public bool ContainsFile(string fullName)
    {
      if (string.IsNullOrEmpty(fullName))
        throw new ArgumentException(LocalizationHolder.rm.GetString("Tools.Components_387"), nameof (fullName));
      for (int index = 0; index < this.allFiles.Count; ++index)
      {
        if (PathUtils.IsSamePath(fullName, this.allFiles[index]))
          return true;
      }
      return false;
    }

    public void PrepareForClose()
    {
    }

    public void Close(bool saveChanges)
    {
      CADDocumentProxy openDocument = this.cadSystem.FindOpenDocument(this.documentFullPath);
      if (openDocument == null)
        return;
      if (saveChanges && !openDocument.ReadOnly)
        openDocument.Save();
      openDocument.Close();
    }
  }
}
