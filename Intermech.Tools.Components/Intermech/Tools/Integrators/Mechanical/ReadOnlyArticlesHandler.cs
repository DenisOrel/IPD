// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Mechanical.ReadOnlyArticlesHandler
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.ControlFlow.Cooperative;
using Intermech.Data.SectionEntities;
using Intermech.Interfaces.Data;
using Intermech.Tools.DataExchange;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Integrators.Mechanical;

internal sealed class ReadOnlyArticlesHandler : CooperativeAction
{
  private readonly MechanicalDriver driver;
  private readonly CaptureChangesDriverContext ctx;
  private readonly SectionEntity articleItem;
  private static readonly IObjectLocator emptyLocator = (IObjectLocator) new EmptyObjectLocator();

  public ReadOnlyArticlesHandler(
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
  }

  private MechanicalDriver MechanicalDriver => this.driver;

  protected override object GetUIReportOperationId() => (object) this.articleItem;

  protected override IEnumerable<CooperativeState> Coroutine()
  {
    this.Initialize();
    this.BindToDBObject();
    this.ReadDBObjectData();
    yield return this.Call(new Func<IEnumerable<CooperativeState>>(this.ProcessRelations));
  }

  private void Initialize()
  {
  }

  private void BindToDBObject()
  {
    ArticleBinder.BindArticle(this.ctx, this.articleItem, ReadOnlyArticlesHandler.emptyLocator, true);
  }

  private void ReadDBObjectData()
  {
  }

  private IEnumerable<CooperativeState> ProcessRelations()
  {
    yield return this.Wait((IWaitObject) this.MechanicalDriver.SchedulerStages.RelationsStage);
    IArticleDocumentationService documentationService = this.MechanicalDriver.TryGetArticleDocumentationService(this.articleItem);
    if (documentationService != null)
      new SyncArticleDocumentationAction((DocumentCaptureChangesDriver) this.MechanicalDriver, this.ctx, documentationService, this.articleItem).Perform();
  }
}
