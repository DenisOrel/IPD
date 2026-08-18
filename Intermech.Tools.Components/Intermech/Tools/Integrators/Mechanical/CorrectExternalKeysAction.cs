// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Mechanical.CorrectExternalKeysAction
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Collections;
using Intermech.ControlFlow.Cooperative;
using Intermech.Data.SectionEntities;
using Intermech.Tools.DataExchange;
using Intermech.UI;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Integrators.Mechanical;

internal sealed class CorrectExternalKeysAction : NearestCheckpointAction
{
  private readonly IArticleExternalKeysService externalKeysService;
  private readonly LinkedList<SectionEntity> articles;
  private readonly SectionEntity modelItem;

  private CorrectExternalKeysAction(
    CooperativeScheduler scheduler,
    SectionEntity modelItem,
    IArticleExternalKeysService externalKeysService)
    : base(scheduler)
  {
    this.externalKeysService = externalKeysService;
    this.articles = new LinkedList<SectionEntity>();
    this.modelItem = modelItem;
  }

  public static CorrectExternalKeysAction GetOrCreate(
    CooperativeScheduler scheduler,
    SectionEntity documentItem,
    IArticleExternalKeysService externalKeysApi)
  {
    if (scheduler == null)
      throw new ArgumentNullException(nameof (scheduler));
    if (documentItem == null)
      throw new ArgumentNullException(nameof (documentItem));
    if (externalKeysApi == null)
      throw new ArgumentNullException(nameof (externalKeysApi));
    return CaptureChangesDatabaseGlobals<CorrectExternalKeysAction>.GetOrCreate(documentItem, (Func<CorrectExternalKeysAction>) (() => new CorrectExternalKeysAction(scheduler, documentItem, externalKeysApi)));
  }

  public void RegisterArticle(SectionEntity articleItem)
  {
    if (articleItem == null)
      throw new ArgumentNullException(nameof (articleItem));
    this.articles.AddLast(articleItem);
  }

  protected override void DoPerform()
  {
    base.DoPerform();
    using (UIReport.CreateLogicalOperation((object) this.modelItem))
    {
      List<SectionEntity> allAsList = CollectionUtils.FindAllAsList<SectionEntity>((ICollection<SectionEntity>) this.articles, (Predicate<SectionEntity>) (articleItem => this.externalKeysService.HasExternalKeySupport(articleItem, this.modelItem)));
      if (allAsList.Count == 0)
        return;
      this.externalKeysService.CorrectExternalKeys(allAsList, this.modelItem);
    }
  }
}
