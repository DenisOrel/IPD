// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.CADFileTreeService
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.CADInterface.Proxies;
using Intermech.Collections;
using Intermech.ControlFlow;
using Intermech.Files;
using Intermech.IO;
using Intermech.Runtime;
using Intermech.Tools.Integrators.FileTrees;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

/// <summary>Создает объект.</summary>
/// <param name="owner">Владелец компонента</param>
/// <exception cref="T:System.ArgumentNullException">Ссылка на владельца компонента не может быть null</exception>
internal sealed class CADFileTreeService(IIntegrator owner) : IntegratorService(owner), IFileTreeScanSupport
{
  private IFileVault fileVault;
  private ICADInterfaceService cadApiService;
  private IApplicationFileTypes fileTypeService;
  private PathCollection stopTable;
  private LinkedList<FileTreeNode> fileNodes;
  private List<string> unresolvedFiles;

  /// <summary>
  /// Возвращает или задает ссылку на сервис типов файлов интегратора. Свойство должно быть заполнено до начала использования текущего сервиса.
  /// </summary>
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

  /// <summary>
  /// Возвращает или задает системный сервис файлового хранилища IPS. Свойство должно быть заполнено до начала использования текущего сервиса.
  /// </summary>
  public IFileVault FileVault
  {
    [DebuggerStepThrough] get
    {
      lock (this.Integrator.SyncRoot)
        return this.fileVault;
    }
    [DebuggerStepThrough] set
    {
      lock (this.Integrator.SyncRoot)
      {
        this.RequireNotInitialized();
        this.fileVault = value;
      }
    }
  }

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
    if (this.FileTypeService == null)
      throw PropertyExceptions.PropertyNotSetException((object) this, "FileTypeService");
    if (this.FileVault == null)
      throw PropertyExceptions.PropertyNotSetException((object) this, "FileVault");
    if (this.ApiService == null)
      throw PropertyExceptions.PropertyNotSetException((object) this, "ApiService");
  }

  public FileTree ScanFile(string filePath, ICollection<string> globalStopTable)
  {
    return this.ScanFile(filePath, (string) null, globalStopTable);
  }

  public FileTree ScanFile(
    string filePath,
    string workingFolderPath,
    ICollection<string> globalStopTable)
  {
    if (string.IsNullOrEmpty(filePath))
      throw new ArgumentException();
    if (!Path.IsPathRooted(filePath))
      throw new ArgumentException();
    if (globalStopTable == null)
      throw new ArgumentNullException(nameof (globalStopTable));
    this.RequireReadyState();
    lock (this.Integrator.SyncRoot)
    {
      try
      {
        this.stopTable = new PathCollection((IEnumerable<string>) globalStopTable);
        this.fileNodes = new LinkedList<FileTreeNode>();
        this.unresolvedFiles = new List<string>();
        if (!this.stopTable.Contains(filePath))
        {
          using (new DynamicScope())
          {
            IntegratorVars.NakedApiSessions.Declare(true);
            using (CADApiSession cadApiSession = new CADApiSession((IApplicationApiService) this.cadApiService))
            {
              CADSystemProxy application = cadApiSession.Application;
              if (!string.IsNullOrEmpty(workingFolderPath) && Directory.Exists(workingFolderPath))
                application.SetWorkingFolder(workingFolderPath);
              this.ScanRootFile(filePath, application);
            }
          }
        }
        return new FileTree(this.fileNodes, this.unresolvedFiles);
      }
      finally
      {
        this.stopTable = (PathCollection) null;
        this.fileNodes = (LinkedList<FileTreeNode>) null;
        this.unresolvedFiles = (List<string>) null;
      }
    }
  }

  private void ScanRootFile(string rootFilePath, CADSystemProxy cadProxy)
  {
    if (!File.Exists(rootFilePath))
      this.stopTable.Add(rootFilePath);
    else if (!this.fileTypeService.IsApplicationFile(rootFilePath))
    {
      this.fileNodes.AddLast(new FileTreeNode(rootFilePath, (IEnumerable<string>) new string[0], (IEnumerable<string>) new string[0]));
      this.stopTable.Add(rootFilePath);
    }
    else
    {
      CADDocumentProxy doc = cadProxy.OpenDocument(rootFilePath, false);
      if (doc.IsMasterDocument)
      {
        this.ScanDocument(doc);
      }
      else
      {
        string masterFile = doc.MasterFile;
        if (this.stopTable.Contains(masterFile))
          return;
        this.ScanRootFile(masterFile, cadProxy);
      }
    }
  }

  private void ScanDocument(CADDocumentProxy doc)
  {
    string fullName = doc.FullName;
    List<string> satelliteFiles = doc.GetSatelliteFiles();
    satelliteFiles.RemoveAll(new Predicate<string>(((OrderedList<string>) this.stopTable).Contains));
    Tuple<PathCollection, PathCollection> dependencyFiles = doc.GetDependencyFiles(true);
    List<CADDocumentProxy> cadDocumentProxyList = MasterDocumentsMapping.OpenMasterDocuments(doc.CADSystem, (ICollection<string>) dependencyFiles.Item1);
    List<string> dependencies = cadDocumentProxyList.ConvertAll<string>((Converter<CADDocumentProxy, string>) (depDoc => depDoc.FullName));
    Tuple<PathCollection, PathCollection> miscFiles = doc.GetMiscFiles(true);
    foreach (string path in (OrderedList<string>) miscFiles.Item1)
    {
      dependencies.Add(path);
      if (!this.stopTable.Contains(path))
      {
        this.fileNodes.AddLast(new FileTreeNode(path, new List<string>(), new List<string>()));
        this.stopTable.Add(path);
      }
    }
    this.fileNodes.AddLast(new FileTreeNode(fullName, satelliteFiles, dependencies));
    this.stopTable.Add(fullName);
    foreach (string str in satelliteFiles)
      this.stopTable.Add(str);
    foreach (string str in (OrderedList<string>) dependencyFiles.Item2)
      this.stopTable.Add(str);
    foreach (string str in (OrderedList<string>) miscFiles.Item2)
      this.stopTable.Add(str);
    foreach (CADDocumentProxy doc1 in cadDocumentProxyList)
    {
      if (!this.stopTable.Contains(doc1.FullName))
        this.ScanDocument(doc1);
    }
  }
}
