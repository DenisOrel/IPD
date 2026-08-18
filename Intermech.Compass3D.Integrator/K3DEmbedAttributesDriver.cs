// Decompiled with JetBrains decompiler
// Type: Intermech.Compass3D.Integrator.K3DEmbedAttributesDriver
// Assembly: Intermech.Compass3D.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E9700F29-129D-4EBE-8417-980BAD3DC32C
// Assembly location: D:\IPS\Client\Intermech.Compass3D.Integrator.dll

using Intermech.Collections;
using Intermech.Interfaces;
using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.CADInterface;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.Compass3D.Integrator;

internal sealed class K3DEmbedAttributesDriver(IIntegrator integrator) : CIEmbedAttributesDriver(integrator)
{
  private IModelDrawingsService modelDrawingsService;
  private K3DAncillaryDrawingsService ancillaryDrawingsService;

  protected override void InitializeDriver(long documentId, int documentTypeId)
  {
    base.InitializeDriver(documentId, documentTypeId);
    this.modelDrawingsService = ServiceUtils.GetService<IModelDrawingsService>((object) this.Integrator, true);
    this.ancillaryDrawingsService = ServiceUtils.GetService<K3DAncillaryDrawingsService>((object) this.Integrator, true);
  }

  protected override void ClearDriver()
  {
    base.ClearDriver();
    this.modelDrawingsService = (IModelDrawingsService) null;
    this.ancillaryDrawingsService = (K3DAncillaryDrawingsService) null;
  }

  private IModelDrawingsService ModelDrawingsService
  {
    [DebuggerStepThrough] get => this.modelDrawingsService;
  }

  private K3DAncillaryDrawingsService AncillaryDrawingsService
  {
    [DebuggerStepThrough] get => this.ancillaryDrawingsService;
  }

  protected override bool DoHasAncillaryDocumentFiles(long documentId)
  {
    return this.AncillaryDrawingsService.IsProcessingEnabled;
  }

  protected override ICollection<string> DoGetAncillaryDocumentFiles(long documentId)
  {
    return (ICollection<string>) CollectionUtils.FindAllAsList<string>((ICollection<string>) this.FileVaultService.DBFilesInfo.GetFileNames(documentId), new Predicate<string>(this.ModelDrawingsService.IsDrawingFileName));
  }
}
