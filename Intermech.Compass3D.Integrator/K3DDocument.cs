// Decompiled with JetBrains decompiler
// Type: Intermech.Compass3D.Integrator.K3DDocument
// Assembly: Intermech.Compass3D.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E9700F29-129D-4EBE-8417-980BAD3DC32C
// Assembly location: D:\IPS\Client\Intermech.Compass3D.Integrator.dll

using Intermech.CADInterface.Proxies;
using Intermech.IO;
using System;
using System.Collections.Generic;
using System.IO;

#nullable disable
namespace Intermech.Compass3D.Integrator;

internal sealed class K3DDocument(ICADDocumentProvider docProvider, CADSystemProxy appProxy) : 
  CADDocumentProxy(docProvider, appProxy)
{
  private IInMemoryConfigurationFeatureDetector inMemoryDetector;
  private IDrawing2DFeatureDetector drawing2DDetector;
  private bool? isDrawing2D;

  protected override void ResetPropertyCache()
  {
    base.ResetPropertyCache();
    this.isDrawing2D = new bool?();
  }

  internal void AttachInMemoryDetector(
    IInMemoryConfigurationFeatureDetector inMemoryDetector)
  {
    if (CADInterfaceTracing.ProxyCallTracer.Enabled)
      CADInterfaceTracing.ProxyCallTracer.AddToTrace<IInMemoryConfigurationFeatureDetector>("K3DDocument.AttachInMemoryDetector()", inMemoryDetector);
    this.inMemoryDetector = inMemoryDetector != null ? inMemoryDetector : throw new ArgumentNullException(nameof (inMemoryDetector));
    this.ResetPropertyCache();
  }

  protected override bool DetectIsInMemory()
  {
    return this.inMemoryDetector == null ? base.DetectIsInMemory() : this.inMemoryDetector.IsInMemory;
  }

  protected override string DetectFullName()
  {
    return !this.IsInMemory ? base.DetectFullName() : this.inMemoryDetector.DocumentVirtualPath;
  }

  protected override string DetectMasterFile()
  {
    return !this.IsInMemory ? base.DetectMasterFile() : this.inMemoryDetector.DocumentVirtualPath;
  }

  internal void AttachDrawing2DDetector(IDrawing2DFeatureDetector drawing2DDetector)
  {
    if (CADInterfaceTracing.ProxyCallTracer.Enabled)
      CADInterfaceTracing.ProxyCallTracer.AddToTrace<IDrawing2DFeatureDetector>("K3DDocument.AttachDrawing2DDetector()", drawing2DDetector);
    this.drawing2DDetector = drawing2DDetector != null ? drawing2DDetector : throw new ArgumentNullException(nameof (drawing2DDetector));
    this.ResetPropertyCache();
  }

  public bool IsDrawing2D()
  {
    if (CADInterfaceTracing.ProxyCallTracer.Enabled)
      CADInterfaceTracing.ProxyCallTracer.AddToTrace("K3DDocument.IsDrawing2D()");
    if (!this.isDrawing2D.HasValue)
      this.isDrawing2D = new bool?(this.drawing2DDetector != null && this.drawing2DDetector.IsDrawing2D((CADDocumentProxy) this));
    return this.isDrawing2D.Value;
  }

  protected override bool DetectHasConfigurations()
  {
    return this.IsDrawing2D() || base.DetectHasConfigurations();
  }

  protected override void FilterDependencyFiles(List<string> dependencyFiles)
  {
    base.FilterDependencyFiles(dependencyFiles);
    dependencyFiles.RemoveAll((Predicate<string>) (x => PathUtils.IsSamePath(Path.GetExtension(x), CompassConsts.TextStyleLibraryExtension)));
    dependencyFiles.RemoveAll((Predicate<string>) (x => PathUtils.IsSamePath(Path.GetExtension(x), CompassConsts.TypographyLibraryExtension)));
    dependencyFiles.RemoveAll((Predicate<string>) (x => PathUtils.IsSamePath(Path.GetExtension(x), CompassConsts.AttributeLibraryExtension)));
  }
}
