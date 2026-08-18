// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Mechanical.RandomizeIdentityAction
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.ControlFlow.Cooperative;
using Intermech.Data;
using Intermech.Data.SectionEntities;
using Intermech.Tools.Data;
using Intermech.Tools.DataExchange;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Integrators.Mechanical;

internal sealed class RandomizeIdentityAction : NearestCheckpointAction
{
  private readonly LinkedList<SectionEntity> articles;

  private RandomizeIdentityAction(CooperativeScheduler scheduler)
    : base(scheduler)
  {
    this.articles = new LinkedList<SectionEntity>();
  }

  public static RandomizeIdentityAction GetOrCreate(
    CooperativeScheduler scheduler,
    SectionEntity documentItem)
  {
    if (scheduler == null)
      throw new ArgumentNullException(nameof (scheduler));
    return documentItem != null ? CaptureChangesDatabaseGlobals<RandomizeIdentityAction>.GetOrCreate(documentItem, (Func<RandomizeIdentityAction>) (() => new RandomizeIdentityAction(scheduler))) : throw new ArgumentNullException(nameof (documentItem));
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
    LinkedList<Tuple<SectionEntity, ValueRecord>> foundArticles1 = new LinkedList<Tuple<SectionEntity, ValueRecord>>();
    LinkedList<Tuple<SectionEntity, ValueRecord>> foundArticles2 = new LinkedList<Tuple<SectionEntity, ValueRecord>>();
    foreach (SectionEntity article in this.articles)
    {
      AttributesSection attributesSection = article.Sections.Get<AttributesSection>();
      ValueRecord valueRecord1 = attributesSection.WorkingSet.Find((StringKey) IDCache.Default.Designation.Text);
      if (!RandomizeIdentityAction.IsEmptyString(valueRecord1))
      {
        foundArticles1.AddFirst(Tuple.Create<SectionEntity, ValueRecord>(article, valueRecord1));
      }
      else
      {
        ValueRecord valueRecord2 = attributesSection.WorkingSet.Find((StringKey) IDCache.Default.OKPCode.Text);
        ValueRecord valueRecord3 = attributesSection.WorkingSet.Find((StringKey) IDCache.Default.Name.Text);
        if (RandomizeIdentityAction.IsEmptyString(valueRecord2) && !RandomizeIdentityAction.IsEmptyString(valueRecord3))
          foundArticles2.AddFirst(Tuple.Create<SectionEntity, ValueRecord>(article, valueRecord3));
      }
    }
    this.RandomizeValues(foundArticles1);
    this.RandomizeValues(foundArticles2);
  }

  private static bool IsEmptyString(ValueRecord item)
  {
    return item == null || item.IsNull || item.DataType != typeof (string) || object.Equals(item.Value, (object) string.Empty);
  }

  private void RandomizeValues(
    LinkedList<Tuple<SectionEntity, ValueRecord>> foundArticles)
  {
    foreach (Tuple<SectionEntity, ValueRecord> foundArticle1 in foundArticles)
    {
      string a = foundArticle1.Item2.Read<string>(string.Empty);
      string b = a;
      bool flag;
      do
      {
        flag = false;
        foreach (Tuple<SectionEntity, ValueRecord> foundArticle2 in foundArticles)
        {
          if (foundArticle2.Item1 != foundArticle1.Item1 && string.Equals(foundArticle2.Item2.Read<string>(string.Empty), b))
          {
            flag = true;
            break;
          }
        }
        if (flag)
          b = $"{a} [{Environment.TickCount}]";
      }
      while (flag);
      if (!string.Equals(a, b))
      {
        foundArticle1.Item2.Value = (object) b;
        foundArticle1.Item2.Flags.Set(NamedFlags.ThrowSetException);
      }
    }
  }
}
