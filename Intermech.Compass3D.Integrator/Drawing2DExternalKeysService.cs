// Decompiled with JetBrains decompiler
// Type: Intermech.Compass3D.Integrator.Drawing2DExternalKeysService
// Assembly: Intermech.Compass3D.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E9700F29-129D-4EBE-8417-980BAD3DC32C
// Assembly location: D:\IPS\Client\Intermech.Compass3D.Integrator.dll

using Intermech.Data.SectionEntities;
using Intermech.Tools.DataExchange;
using Intermech.Tools.Integrators.CADInterface;
using System.Diagnostics;

#nullable disable
namespace Intermech.Compass3D.Integrator;

internal sealed class Drawing2DExternalKeysService(
  K3DCaptureChangesDriver driver,
  CaptureChangesDriverContext driverContext) : CIBaseExternalKeysService((CICaptureChangesDriver) driver, driverContext)
{
  private K3DCaptureChangesDriver K3DDriver
  {
    [DebuggerStepThrough] get => (K3DCaptureChangesDriver) this.Driver;
  }

  protected override bool DoHasExternalKeySupport(
    SectionEntity articleItem,
    SectionEntity modelItem)
  {
    return this.K3DDriver.Drawing2DOperations.IsHeadArticle(articleItem);
  }

  protected override string DoGetArticleInternalId(SectionEntity articleItem)
  {
    return (string) articleItem.Sections.Get<CIArticleData>().Configuration.Name;
  }
}
