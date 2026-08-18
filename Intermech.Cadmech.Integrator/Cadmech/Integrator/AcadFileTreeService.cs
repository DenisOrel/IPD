// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.AcadFileTreeService
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using Intermech.Files;
using Intermech.IO;
using Intermech.Runtime;
using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.FileTrees;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

#nullable disable
namespace Intermech.Cadmech.Integrator;

internal sealed class AcadFileTreeService(IIntegrator owner) : 
  IntegratorService(owner),
  IFileTreeScanSupport
{
  private IFileVault fileVault;
  private IApplicationFileTypes fileTypeService;
  private PathCollection stopTable;
  private LinkedList<FileTreeNode> fileNodes;
  private List<string> unresolvedFiles;

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

  protected override void DoInitialize()
  {
    base.DoInitialize();
    if (this.FileTypeService == null)
      throw PropertyExceptions.PropertyNotSetException((object) this, "FileTypeService");
    if (this.FileVault == null)
      throw PropertyExceptions.PropertyNotSetException((object) this, "FileVault");
  }

  public FileTree ScanFile(string path, ICollection<string> globalStopTable)
  {
    return this.ScanFile(path, (string) null, globalStopTable);
  }

  public FileTree ScanFile(
    string path,
    string workingFolderPath,
    ICollection<string> globalStopTable)
  {
    if (string.IsNullOrEmpty(path))
      throw new ArgumentException();
    if (!Path.IsPathRooted(path))
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
        if (!this.stopTable.Contains(path))
          this.ScanRootFile(path);
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

  private void ScanRootFile(string rootFilePath)
  {
    if (!File.Exists(rootFilePath))
      this.stopTable.Add(rootFilePath);
    else if (!this.fileTypeService.IsApplicationFile(rootFilePath))
    {
      this.fileNodes.AddLast(new FileTreeNode(rootFilePath, (IEnumerable<string>) new string[0], (IEnumerable<string>) new string[0]));
      this.stopTable.Add(rootFilePath);
    }
    else
      this.ScanDocument(rootFilePath);
  }

  private void ScanDocument(string docFilePath)
  {
    List<string> liveXrefs = DwgOperations.GetLiveXRefs(this.Integrator, docFilePath);
    FileTreeNode fileTreeNode = new FileTreeNode(docFilePath, (IEnumerable<string>) new string[0], (IEnumerable<string>) liveXrefs);
    this.fileNodes.AddLast(fileTreeNode);
    this.stopTable.Add(docFilePath);
    foreach (string dependency in (IEnumerable<string>) fileTreeNode.Dependencies)
    {
      if (!this.stopTable.Contains(dependency))
        this.ScanRootFile(dependency);
    }
  }
}
