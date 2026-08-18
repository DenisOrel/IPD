// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.AcadOpenFiles
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using Intermech.AutoCAD.Proxies;
using Intermech.Collections;
using Intermech.Files;
using Intermech.IO;
using Intermech.Runtime;
using Intermech.Tools.Integrators;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

#nullable disable
namespace Intermech.Cadmech.Integrator;

internal sealed class AcadOpenFiles(IIntegrator owner) : 
  IntegratorService(owner),
  IOpenFilesServiceExtension,
  IOpenFiles
{
  private IApplicationFileTypes fileTypeService;
  private CadApiService apiService;

  public IApplicationFileTypes FileTypeService
  {
    [DebuggerStepThrough] get
    {
      lock (this.Integrator.SyncRoot)
        return this.fileTypeService;
    }
    [DebuggerStepThrough] set
    {
      lock (this.Integrator.SyncRoot)
      {
        this.RequireNotInitialized();
        this.fileTypeService = value;
      }
    }
  }

  public CadApiService ApiService
  {
    [DebuggerStepThrough] get
    {
      lock (this.Integrator.SyncRoot)
        return this.apiService;
    }
    [DebuggerStepThrough] set
    {
      lock (this.Integrator.SyncRoot)
      {
        this.RequireNotInitialized();
        this.apiService = value;
      }
    }
  }

  protected override void DoInitialize()
  {
    base.DoInitialize();
    if (this.FileTypeService == null)
      throw PropertyExceptions.PropertyNotSetException((object) this, "FileTypeService");
    if (this.ApiService == null)
      throw PropertyExceptions.PropertyNotSetException((object) this, "ApiService");
  }

  public bool IsOpen(string filePath)
  {
    if (filePath == null)
      throw new ArgumentNullException(nameof (filePath));
    this.RequireReadyState();
    if (!Path.IsPathRooted(filePath))
      return false;
    try
    {
      if (!this.fileTypeService.IsApplicationFile(filePath) || !this.apiService.IsApplicationRunning)
        return false;
      using (AcadApiSession acadApiSession = new AcadApiSession((IApplicationApiService) this.apiService))
        return acadApiSession.Application.FindOpenDocument(filePath) != null;
    }
    catch (Exception ex)
    {
      this.ShowError(ex);
      return false;
    }
  }

  public bool IsDirty(string filePath)
  {
    if (filePath == null)
      throw new ArgumentNullException(nameof (filePath));
    this.RequireReadyState();
    if (!Path.IsPathRooted(filePath))
      return false;
    try
    {
      if (!this.fileTypeService.IsApplicationFile(filePath) || !this.apiService.IsApplicationRunning)
        return false;
      using (AcadApiSession acadApiSession = new AcadApiSession((IApplicationApiService) this.apiService))
      {
        ICadDocumentProxy openDocument = acadApiSession.Application.FindOpenDocument(filePath);
        return openDocument != null && openDocument.Modified;
      }
    }
    catch (Exception ex)
    {
      this.ShowError(ex);
      return false;
    }
  }

  public void Save(string filePath)
  {
    if (filePath == null)
      throw new ArgumentNullException(nameof (filePath));
    this.RequireReadyState();
    if (!Path.IsPathRooted(filePath))
      return;
    try
    {
      if (!this.fileTypeService.IsApplicationFile(filePath) || !this.apiService.IsApplicationRunning)
        return;
      using (AcadApiSession acadApiSession = new AcadApiSession((IApplicationApiService) this.apiService))
      {
        ICadDocumentProxy openDocument = acadApiSession.Application.FindOpenDocument(filePath);
        if (openDocument == null || !openDocument.Modified || openDocument.IsReadOnly)
          return;
        openDocument.Save();
      }
    }
    catch (Exception ex)
    {
      this.ShowError(ex);
    }
  }

  public void SetReadOnlyFlag(string filePath, bool readOnlyFlag) => this.RequireReadyState();

  public bool IsReloadable(string filePath)
  {
    this.RequireReadyState();
    return false;
  }

  public void Reload(string filePath) => this.RequireReadyState();

  public object Unload(IEnumerable<string> fileList)
  {
    if (fileList == null)
      throw new ArgumentNullException(nameof (fileList), "Ссылка на список файлов не может быть null.");
    this.RequireReadyState();
    try
    {
      LinkedList<string> allAsLinkedList = CollectionUtils.FindAllAsLinkedList<string>(fileList, (Predicate<string>) (filePath => Path.IsPathRooted(filePath) && this.fileTypeService.IsApplicationFile(filePath)));
      if (allAsLinkedList.Count == 0 || !this.apiService.IsApplicationRunning)
        return (object) null;
      using (AcadApiSession acadApiSession = new AcadApiSession((IApplicationApiService) this.apiService))
        return this.UnloadCore(acadApiSession.Application, (ICollection<string>) allAsLinkedList);
    }
    catch (Exception ex)
    {
      this.ShowError(ex);
      return (object) null;
    }
  }

  private object UnloadCore(ICadProxy acad, ICollection<string> fullPathList)
  {
    List<AcadOpenFiles.ReloadItem> reloadItems = this.GetReloadItems(fullPathList, acad);
    if (reloadItems.Count <= 0)
      return (object) null;
    for (int index = 0; index < reloadItems.Count; ++index)
    {
      if (reloadItems[index].Document.IsNew)
        reloadItems[index].Document.Save();
    }
    object obj = acad.SaveVisualState(CadVisualStateFlags.All);
    for (int index = 0; index < reloadItems.Count; ++index)
      reloadItems[index].Document.Close(false);
    return obj;
  }

  public object UnloadAll()
  {
    this.RequireReadyState();
    try
    {
      if (!this.apiService.IsApplicationRunning)
        return (object) null;
      List<string> fileList = (List<string>) null;
      using (AcadApiSession acadApiSession = new AcadApiSession((IApplicationApiService) this.apiService))
        fileList = acadApiSession.Application.GetOpenDocuments(false).ConvertAll<string>((Converter<ICadDocumentProxy, string>) (x => x.GetMasterFile()));
      return fileList.Count > 0 ? this.Unload((IEnumerable<string>) fileList) : (object) null;
    }
    catch (Exception ex)
    {
      this.ShowError(ex);
      return (object) null;
    }
  }

  public void Reload(object reloadState)
  {
    this.RequireReadyState();
    if (reloadState == null)
      return;
    try
    {
      using (AcadApiSession acadApiSession = new AcadApiSession((IApplicationApiService) this.apiService))
        acadApiSession.Application.RestoreVisualState(reloadState);
    }
    catch (Exception ex)
    {
      this.ShowError(ex);
    }
  }

  private void ShowError(Exception x)
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append("Заблокирована ошибка сервиса открытых файлов, вызванная интегратором.");
    stringBuilder.Append(' ');
    stringBuilder.Append(x.Message);
    this.OutputService.WriteLine(stringBuilder.ToString());
  }

  private List<AcadOpenFiles.ReloadItem> GetReloadItems(
    ICollection<string> doomedFiles,
    ICadProxy acad)
  {
    List<AcadOpenFiles.ReloadItem> reloadItems1 = this.CollectReloadableDocuments(acad);
    this.MarkDocumentsToUnload(reloadItems1, doomedFiles);
    List<AcadOpenFiles.ReloadItem> reloadItems2 = new List<AcadOpenFiles.ReloadItem>(reloadItems1.Count);
    for (int index = 0; index < reloadItems1.Count; ++index)
    {
      AcadOpenFiles.ReloadItem reloadItem = reloadItems1[index];
      if (reloadItem.Unload)
        reloadItems2.Add(reloadItem);
    }
    return reloadItems2;
  }

  private List<AcadOpenFiles.ReloadItem> CollectReloadableDocuments(ICadProxy acad)
  {
    List<ICadDocumentProxy> openDocuments = acad.GetOpenDocuments(false);
    List<AcadOpenFiles.ReloadItem> reloadItemList = new List<AcadOpenFiles.ReloadItem>(openDocuments.Count);
    for (int index = 0; index < openDocuments.Count; ++index)
    {
      ICadDocumentProxy doc = openDocuments[index];
      reloadItemList.Add(new AcadOpenFiles.ReloadItem(doc));
    }
    return reloadItemList;
  }

  private void MarkDocumentsToUnload(
    List<AcadOpenFiles.ReloadItem> reloadItems,
    ICollection<string> doomedFiles)
  {
    foreach (string doomedFile in (IEnumerable<string>) doomedFiles)
    {
      foreach (AcadOpenFiles.ReloadItem reloadItem in reloadItems)
      {
        if (!reloadItem.Unload && reloadItem.ContainsFile(doomedFile))
          reloadItem.Unload = true;
      }
    }
  }

  private sealed class ReloadItem
  {
    private ICadDocumentProxy document;
    private string masterFile;
    private List<string> satelliteFiles;
    private bool unload;

    public ReloadItem(ICadDocumentProxy doc)
    {
      this.document = doc;
      this.masterFile = doc.GetMasterFile();
      SatelliteFileType selectedTypes = doc.CADSystem.XRefLoadingIsBlocking ? SatelliteFileType.All : SatelliteFileType.RasterImage | SatelliteFileType.Underlay;
      this.satelliteFiles = doc.GetSatelliteFiles(selectedTypes);
    }

    public bool ContainsFile(string fullName)
    {
      if (string.IsNullOrEmpty(fullName))
        throw new ArgumentException("Не задано имя файла.", nameof (fullName));
      if (!this.document.IsNew && PathUtils.IsSamePath(fullName, this.masterFile))
        return true;
      for (int index = 0; index < this.satelliteFiles.Count; ++index)
      {
        if (PathUtils.IsSamePath(fullName, this.satelliteFiles[index]))
          return true;
      }
      return false;
    }

    public ICadDocumentProxy Document => this.document;

    public string MasterFile => this.masterFile;

    public List<string> SatelliteFiles => this.satelliteFiles;

    public bool Unload
    {
      get => this.unload;
      set => this.unload = value;
    }
  }
}
