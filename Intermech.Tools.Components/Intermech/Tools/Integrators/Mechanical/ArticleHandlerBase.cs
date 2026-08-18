// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Mechanical.ArticleHandlerBase
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.ControlFlow.Cooperative;
using Intermech.Data.SectionEntities;
using Intermech.Tools.DataExchange;
using System;

#nullable disable
namespace Intermech.Tools.Integrators.Mechanical;

public abstract class ArticleHandlerBase : CooperativeAction
{
  private readonly MechanicalDriver driver;
  protected readonly CaptureChangesDriverContext ctx;
  protected readonly SectionEntity articleItem;
  protected readonly IArticleCADApiService articleApiService;
  protected readonly SectionEntity docItem;
  protected readonly ArticleInitialDocumentType docLinkType;

  public ArticleHandlerBase(
    MechanicalDriver driver,
    CaptureChangesDriverContext ctx,
    SectionEntity articleItem)
    : base(ctx.Scheduler)
  {
    if (driver == null)
      throw new ArgumentNullException(nameof (driver));
    if (ctx == null)
      throw new ArgumentNullException(nameof (ctx));
    if (articleItem == null)
      throw new ArgumentNullException(nameof (articleItem));
    this.driver = driver;
    this.ctx = ctx;
    this.articleItem = articleItem;
    this.articleApiService = this.MechanicalDriver.GetArticleApiService(articleItem);
    ArticleSection articleSection = articleItem.Sections.Get<ArticleSection>();
    this.docItem = articleSection.InitialDocument;
    this.docLinkType = articleSection.InitialDocumentType;
  }

  protected MechanicalDriver MechanicalDriver => this.driver;

  protected void WriteChangedFileProperties()
  {
    AttributesSection attributesSection = this.articleItem.Sections.Get<AttributesSection>((AttributesSection) null);
    if (!attributesSection.EmbeddedSet.Bag.HasChanges || !this.articleApiService.WriteArticleProperties(this.articleItem, attributesSection.EmbeddedSet) || this.docLinkType == ArticleInitialDocumentType.None)
      return;
    AnalyzerChangesSection.Mark(this.docItem);
  }
}
