// Decompiled with JetBrains decompiler
// Type: Intermech.Compass3D.Integrator.Drawing2DHeadArticleStructureService
// Assembly: Intermech.Compass3D.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E9700F29-129D-4EBE-8417-980BAD3DC32C
// Assembly location: D:\IPS\Client\Intermech.Compass3D.Integrator.dll

using Intermech.Data.SectionEntities;
using Intermech.Tools.DataExchange;
using Intermech.Tools.Integrators.CADInterface;
using System.Diagnostics;

#nullable disable
namespace Intermech.Compass3D.Integrator;

internal sealed class Drawing2DHeadArticleStructureService(
  K3DCaptureChangesDriver driver,
  CaptureChangesDriverContext driverContext,
  ICADInterfaceService cadInterfaceService) : CIArticleStructureService((CICaptureChangesDriver) driver, driverContext, cadInterfaceService)
{
  private K3DCaptureChangesDriver K3DDriver
  {
    [DebuggerStepThrough] get => (K3DCaptureChangesDriver) this.Driver;
  }

  protected override bool IsProjectArticle(SectionEntity articleItem, SectionEntity documentItem)
  {
    return this.K3DDriver.Drawing2DOperations.IsHeadArticle(articleItem) || base.IsProjectArticle(articleItem, documentItem);
  }
}
