// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.COM.SpecificationUpdater
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using Intermech.ControlFlow;
using Intermech.Files;
using Intermech.Interfaces;
using Intermech.Tools.Data;
using Intermech.Tools.Integrators;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Cadmech.Integrator.COM;

internal sealed class SpecificationUpdater
{
  private IIntegrator integrator;
  private SearchAPIServiceLink serviceLink;

  public SpecificationUpdater(IIntegrator integrator, SearchAPIServiceLink serviceLink)
  {
    if (integrator == null)
      throw new ArgumentNullException(nameof (integrator));
    if (serviceLink == null)
      throw new ArgumentNullException(nameof (serviceLink));
    this.integrator = integrator;
    this.serviceLink = serviceLink;
  }

  public StructData CreateOrUpdateProjects(
    string dwgPath,
    string inpFieldLayout,
    string structFileContent,
    string passportData)
  {
    if (dwgPath == null)
      throw new ArgumentNullException(nameof (dwgPath));
    if (inpFieldLayout == null)
      throw new ArgumentNullException(nameof (inpFieldLayout));
    if (structFileContent == null)
      throw new ArgumentNullException(nameof (structFileContent));
    Cadmech2DService service = ServiceUtils.GetService<Cadmech2DService>((object) this.integrator, true);
    using (new DynamicScope())
    {
      FileVars.SoftMode.Declare(false);
      return service.CreateComposition(dwgPath, inpFieldLayout, structFileContent, passportData);
    }
  }

  public void CheckoutProjects(StructData structData)
  {
    List<long> objectList = structData != null ? new List<long>((IEnumerable<long>) structData.ProjectIds) : throw new ArgumentNullException(nameof (structData));
    int index = objectList.IndexOf(structData.BaseProjectId);
    if (index == -1)
      throw new InvalidOperationException("При формировании/обновлении изделия по сборочному чертежу CADMECH 2D произошла ошибка. В списке исполнений изделия отсутствует основное исполнение.");
    IList<long> collection = DBDocumentHelper.Checkout((IList<long>) objectList, (DBDocumentHelper.CheckoutErrorHandler) null);
    structData.ProjectIds.Clear();
    structData.ProjectIds.AddRange((IEnumerable<long>) collection);
    structData.BaseProjectId = collection[index];
  }

  public void EditSpecification(StructData structData)
  {
    if (structData == null)
      throw new ArgumentNullException(nameof (structData));
    this.serviceLink.AvsImportService.Value.EditDrawingSpec(structData);
  }

  public string CreateStructFileContent(
    string dwgPath,
    string outFieldLayout,
    StructData structData)
  {
    if (dwgPath == null)
      throw new ArgumentNullException(nameof (dwgPath));
    if (outFieldLayout == null)
      throw new ArgumentNullException(nameof (outFieldLayout));
    if (structData == null)
      throw new ArgumentNullException(nameof (structData));
    Cadmech2DService service = ServiceUtils.GetService<Cadmech2DService>((object) this.integrator, true);
    using (new DynamicScope())
    {
      FileVars.SoftMode.Declare(false);
      return service.PackCompositionToFile(structData, outFieldLayout);
    }
  }
}
