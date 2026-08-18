// Decompiled with JetBrains decompiler
// Type: Intermech.Compass3D.Integrator.Drawing2DGenericDocumentApiService
// Assembly: Intermech.Compass3D.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E9700F29-129D-4EBE-8417-980BAD3DC32C
// Assembly location: D:\IPS\Client\Intermech.Compass3D.Integrator.dll

using Intermech.Data;
using Intermech.Data.SectionEntities;
using Intermech.Tools.DataExchange;
using Intermech.Tools.Integrators.CADInterface;
using Intermech.Tools.Integrators.Mechanical;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.Compass3D.Integrator;

internal class Drawing2DGenericDocumentApiService : CIDocumentApiService
{
  private readonly K3DCADInterfaceService cadInterfaceService;

  public Drawing2DGenericDocumentApiService(
    K3DCaptureChangesDriver driver,
    CaptureChangesDriverContext driverContext,
    K3DCADInterfaceService cadInterfaceService)
    : base((CICaptureChangesDriver) driver, driverContext)
  {
    this.cadInterfaceService = cadInterfaceService != null ? cadInterfaceService : throw new ArgumentNullException(nameof (cadInterfaceService));
  }

  private K3DCaptureChangesDriver K3DDriver
  {
    [DebuggerStepThrough] get => (K3DCaptureChangesDriver) this.CIDriver;
  }

  protected override IAttributeCodec GetDocumentCodec(SectionEntity docItem)
  {
    return this.cadInterfaceService.GetDrawing2DDocumentCodec();
  }

  public override ICollection<InitialArticleData> ReadArticles(SectionEntity documentItem)
  {
    ICollection<InitialArticleData> initialArticleDatas = base.ReadArticles(documentItem);
    foreach (InitialArticleData initialArticleData in (IEnumerable<InitialArticleData>) initialArticleDatas)
    {
      if (initialArticleData.InitialDocumentType == ArticleInitialDocumentType.Normal)
        this.K3DDriver.Drawing2DOperations.AddCustomArticleData(initialArticleData.CustomSections, Drawing2DArticleKind.HeadArticle);
    }
    return initialArticleDatas;
  }
}
