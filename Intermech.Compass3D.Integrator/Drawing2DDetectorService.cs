// Decompiled with JetBrains decompiler
// Type: Intermech.Compass3D.Integrator.Drawing2DDetectorService
// Assembly: Intermech.Compass3D.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E9700F29-129D-4EBE-8417-980BAD3DC32C
// Assembly location: D:\IPS\Client\Intermech.Compass3D.Integrator.dll

using Intermech.CADInterface.Proxies;
using Intermech.Client.Core;
using Intermech.Collections;
using Intermech.Files;
using Intermech.Interfaces.Client;
using Intermech.IO;
using Intermech.Runtime;
using Intermech.Tools.Integrators;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

#nullable disable
namespace Intermech.Compass3D.Integrator;

internal sealed class Drawing2DDetectorService(IIntegrator owner) : 
  IntegratorService(owner),
  IDrawing2DFeatureDetector
{
  private K3DSettingsService settingsService;

  public K3DSettingsService SettingsService
  {
    [DebuggerStepThrough] get
    {
      lock (this.Integrator.SyncRoot)
        return this.settingsService;
    }
    [DebuggerStepThrough] set
    {
      lock (this.Integrator.SyncRoot)
      {
        this.RequireNotInitialized();
        this.settingsService = value;
      }
    }
  }

  protected override void DoInitialize()
  {
    base.DoInitialize();
    if (this.SettingsService == null)
      throw PropertyExceptions.PropertyNotSetException((object) this, "SettingsService");
  }

  public bool IsDrawing2D(int documentType)
  {
    if (documentType == -1)
      throw new ArgumentException("Не задан тип документа", nameof (documentType));
    this.RequireReadyState();
    K3DIntegratorSettings settings = this.settingsService.GetSettings();
    return settings.EnableDrawings2DSupport && this.IsDrawing2DByType(documentType, settings);
  }

  private bool IsDrawing2DByType(int documentType, K3DIntegratorSettings k3dSettings)
  {
    return k3dSettings.PartDrawings2D.ContainsType(documentType) || k3dSettings.AssemblyDrawings2D.ContainsType(documentType);
  }

  public bool IsDrawing2D(CADDocumentProxy document)
  {
    if (document == null)
      throw new ArgumentNullException(nameof (document));
    this.RequireReadyState();
    return document is K3DDocument k3Ddocument && k3Ddocument.IsDrawing2D();
  }

  bool IDrawing2DFeatureDetector.IsDrawing2D(CADDocumentProxy document)
  {
    if (document == null)
      throw new ArgumentNullException(nameof (document));
    this.RequireReadyState();
    K3DIntegratorSettings k3dSettings = this.settingsService.GetSettings();
    if (!k3dSettings.EnableDrawings2DSupport || document.DocumentType != CADDocumentType.Drawing)
      return false;
    string fullName = document.FullName;
    return !string.IsNullOrEmpty(fullName) && Path.IsPathRooted(fullName) ? document.CADSystem.EvaluateCached<bool>((object) new StringKey(fullName), "IsDrawing2D", (Func<bool>) (() => this.IsDrawing2DSlowTest(document, k3dSettings))) : this.IsDrawing2DSlowTest(document, k3dSettings);
  }

  private bool IsDrawing2DSlowTest(CADDocumentProxy document, K3DIntegratorSettings k3dSettings)
  {
    if (!string.IsNullOrEmpty(document.FullName) && PathUtils.IsPlacedIn(document.FullName, ClientContext.FileVault.WorkArea.AreaPath))
    {
      FileOrigin fileOrigin = ClientContext.FileVault.WorkArea.GetFileOrigin(document.FullName, false);
      if (fileOrigin.OriginType == FileOriginType.WorkFile && DBHelper.IsObjectAlive(fileOrigin.WorkObject.ObjectId))
        return this.IsDrawing2DByType(DBHelper.GetObjectType(fileOrigin.WorkObject.ObjectId), k3dSettings);
    }
    if (this.CoexistsWithModelFile(document.FullName))
      return false;
    Tuple<PathCollection, PathCollection> dependencyFiles = document.GetDependencyFiles(true);
    return !this.ReferencesPartOrAssemblyModel(dependencyFiles.Item1) && !this.ReferencesPartOrAssemblyModel(dependencyFiles.Item2);
  }

  private bool CoexistsWithModelFile(string drawingFullPath)
  {
    return File.Exists(Path.ChangeExtension(drawingFullPath, CompassConsts.PartFileExtension)) || File.Exists(Path.ChangeExtension(drawingFullPath, CompassConsts.AssemblyFileExtension));
  }

  private bool ReferencesPartOrAssemblyModel(PathCollection drawingDependencies)
  {
    return CollectionUtils.Exists<string>((IEnumerable<string>) drawingDependencies, (Predicate<string>) (depPath =>
    {
      string firstPath = Path.GetExtension(depPath);
      return PathUtils.IsSamePath(firstPath, CompassConsts.PartFileExtension) || PathUtils.IsSamePath(firstPath, CompassConsts.AssemblyFileExtension);
    }));
  }
}
