// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.CIArticleKindDetectorService
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data.SectionEntities;
using Intermech.Tools.DataExchange;
using Intermech.Tools.Integrators.Mechanical;
using System;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

internal sealed class CIArticleKindDetectorService : ArticleKindDetectorService
{
  private ICADInterfaceService cadService;

  public CIArticleKindDetectorService(
    MechanicalDriver driver,
    CaptureChangesDriverContext driverContext,
    ICADInterfaceService cadService)
    : base(driver, driverContext)
  {
    this.cadService = cadService != null ? cadService : throw new ArgumentNullException(nameof (cadService));
  }

  /// <summary>
  /// Позволяет определить, является ли указанное изделие или материал объектом Imbase. Данный метод вызывается в процессе анализа заготовок изделий и материалов для
  /// выбора способа дальнейшего анализа. При реализации метода следует использовать рабочий набор атрибутов, а также API приложения.
  /// </summary>
  /// <param name="articleItem">Рабочий элемент изделия или неосновного материала</param>
  /// <returns>true, если это объект Imbase</returns>
  protected override bool DoIsImbaseObject(SectionEntity articleItem)
  {
    return this.cadService.GetArticleProcessingMethod(this.GetArticleProcessingParams(articleItem)) == ArticleProcessingMethod.ImbaseObject;
  }

  /// <summary>
  /// Позволяет определить, является ли указанный объект неосновным материалом. Данный метод вызывается в процессе анализа заготовок изделий и материалов для
  /// выбора способа дальнейшего анализа. При реализации метода следует использовать рабочий набор атрибутов, а также API приложения.
  /// </summary>
  /// <param name="articleItem">Рабочий элемент объекта</param>
  /// <returns>true, если это неосновной материал</returns>
  protected override bool DoIsMinorMaterial(SectionEntity articleItem)
  {
    return this.cadService.GetArticleProcessingMethod(this.GetArticleProcessingParams(articleItem)) == ArticleProcessingMethod.MinorMaterial;
  }

  private ArticleProcessingParams GetArticleProcessingParams(SectionEntity articleItem)
  {
    ArticleProcessingParams processingParams = new ArticleProcessingParams((string) articleItem.Sections.Get<CIArticleData>().Configuration.Name, articleItem.Sections.Get<AttributesSection>().WorkingSet);
    SectionEntity articleMainDocument = this.Driver.MechanicalOperations.Articles.TryGetArticleMainDocument(articleItem);
    if (articleMainDocument != null)
    {
      int objectType = ObjectSection.TryGetObjectType(articleMainDocument);
      if (objectType != -1)
        processingParams.SetDocumentInfo(objectType);
    }
    return processingParams;
  }
}
