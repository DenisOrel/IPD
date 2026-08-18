// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Mechanical.SetGroupIdAction
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Collections;
using Intermech.ControlFlow.Cooperative;
using Intermech.Data.SectionEntities;
using Intermech.Interfaces;
using Intermech.Tools.Data;
using Intermech.Tools.DataExchange;
using Intermech.UI;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Integrators.Mechanical;

internal sealed class SetGroupIdAction : NearestCheckpointAction
{
  private readonly LinkedList<SectionEntity> articles;
  private readonly SectionEntity modelItem;

  private SetGroupIdAction(CooperativeScheduler scheduler, SectionEntity modelItem)
    : base(scheduler)
  {
    this.articles = new LinkedList<SectionEntity>();
    this.modelItem = modelItem;
  }

  public static SetGroupIdAction GetOrCreate(
    CooperativeScheduler scheduler,
    SectionEntity documentItem)
  {
    if (scheduler == null)
      throw new ArgumentNullException(nameof (scheduler));
    return documentItem != null ? CaptureChangesDatabaseGlobals<SetGroupIdAction>.GetOrCreate(documentItem, (Func<SetGroupIdAction>) (() => new SetGroupIdAction(scheduler, documentItem))) : throw new ArgumentNullException(nameof (documentItem));
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
      if (this.articles.Count <= 1)
        return;
      SetGroupIdAction.SetArticleGroupId(this.articles, this.modelItem);
    }
  }

  private static void SetArticleGroupId(
    LinkedList<SectionEntity> articleItems,
    SectionEntity modelItem)
  {
    List<SectionEntity> allAsList1 = CollectionUtils.FindAllAsList<SectionEntity>((ICollection<SectionEntity>) articleItems, (Predicate<SectionEntity>) (article => ObjectSection.IsNewObject(article) && SetGroupIdAction.GetArticleGroupId(article) == Guid.Empty));
    if (allAsList1.Count == 0)
      return;
    List<SectionEntity> allAsList2 = CollectionUtils.FindAllAsList<SectionEntity>((ICollection<SectionEntity>) articleItems, (Predicate<SectionEntity>) (article => !ObjectSection.IsNewObject(article)));
    if (allAsList2.Count != 0 && !SetGroupIdAction.HasCheckoutsByCreateVersion(allAsList2))
    {
      SectionEntity sectionEntity = allAsList2[0];
      Guid newGroupId = SetGroupIdAction.GetArticleGroupId(sectionEntity);
      if (newGroupId == Guid.Empty)
      {
        if (allAsList2.Count != 1)
          throw new InvalidOperationException($"У объекта '{DisplaySection.GetDisplayName(sectionEntity)}' (ид. версии {ObjectSection.GetObjectId(sectionEntity)}) должен быть заполнен атрибут '{IDCache.Default.InstanceGroupId.Text}', так как это - исполнение изделия.");
        newGroupId = Guid.NewGuid();
        SetGroupIdAction.UpdateArticleGroupId(sectionEntity, newGroupId);
      }
      foreach (SectionEntity articleItem in allAsList1)
        SetGroupIdAction.UpdateArticleGroupId(articleItem, newGroupId);
    }
    else
    {
      Guid newGroupId = Guid.NewGuid();
      foreach (SectionEntity articleItem in articleItems)
        SetGroupIdAction.UpdateArticleGroupId(articleItem, newGroupId);
    }
  }

  private static bool HasCheckoutsByCreateVersion(List<SectionEntity> existingInstances)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (SectionEntity existingInstance in existingInstances)
      {
        long objectId = ObjectSection.GetObjectId(existingInstance);
        if (objectId > 0L && sessionKeeper.Session.GetObject(objectId).ObjectModifyMode == ObjectModifyModes.CreateVersion)
          return true;
      }
    }
    return false;
  }

  private static Guid GetArticleGroupId(SectionEntity articleItem)
  {
    return articleItem.Sections.Get<AttributesSection>().DatabaseSet.Read<Guid>((StringKey) IDCache.Default.InstanceGroupId.Text, Guid.Empty);
  }

  private static void UpdateArticleGroupId(SectionEntity articleItem, Guid newGroupId)
  {
    articleItem.Sections.Get<AttributesSection>().DatabaseSet.Update((StringKey) IDCache.Default.InstanceGroupId.Text, (object) newGroupId);
  }
}
