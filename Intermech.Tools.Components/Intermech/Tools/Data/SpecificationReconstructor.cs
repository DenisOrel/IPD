// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Data.SpecificationReconstructor
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Collections;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Data.Queries;
using Intermech.Kernel.Search;
using Intermech.Tools.Components.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.Tools.Data;

/// <summary>
/// Выполняет реконструкцию спецификации, используя составы исполнений сборочной единицы.
/// </summary>
/// <remarks>
/// Алгоритм реконструкции спецификации основан на том что:
/// 1. каждая запись спецификации - это одна позиция, которая является либо общей для всех исполнений, либо частной для определенных исполнений
/// 2. каждая связь в составах исполнений сборки помечена либо уникальным идентификатором входимости, либо номером позиции
/// 3. две связи в составах разных исполнений описывают одну позицию, если у них совпадает идентификатор (входимости или номер позиции) и количество
/// 4. требование к совпадающему количеству обязательно, так как возможна ситуация, когда разные записи спецификации имеют одинаковый номер позиции, но разное количество
/// </remarks>
public sealed class SpecificationReconstructor
{
  private static readonly TraceSwitch tracer = new TraceSwitch("Tools.SpecificationReconstructor", "", "0");
  private long documentId;
  private List<long> articleInstances;
  private List<Tuple<long, string, Guid>> articleInfos;

  public SpecificationReconstructor() => this.articleInstances = new List<long>();

  public long Document
  {
    get => this.documentId;
    set => this.documentId = value;
  }

  public List<long> ArticleInstances => this.articleInstances;

  public List<SimpleSpecificationRow> CreateSpecification()
  {
    this.ValidateProperties();
    try
    {
      this.CollectArticleInfo();
      this.CheckArticleDesignations();
      this.CheckArticleInstanceGroupIds();
      LinkedList<SimpleSpecificationRow> linkedList = new LinkedList<SimpleSpecificationRow>();
      this.CollectRows((ICollection<SimpleSpecificationRow>) linkedList);
      this.MarkCommonPart((ICollection<SimpleSpecificationRow>) linkedList);
      if (SpecificationReconstructor.tracer.TraceInfo)
        SpecificationReconstructor.TraceSpecification((IEnumerable<SimpleSpecificationRow>) linkedList);
      return new List<SimpleSpecificationRow>((IEnumerable<SimpleSpecificationRow>) linkedList);
    }
    finally
    {
      this.Cleanup();
    }
  }

  private void Cleanup() => this.articleInfos.Clear();

  private void ValidateProperties()
  {
    if (this.documentId == 0L)
      throw new InvalidOperationException($"Для получения спецификации требуется, чтобы было задано свойство {"Document"}.");
    if (this.articleInstances.Count == 0)
      throw new InvalidOperationException($"Для получения спецификации требуется, чтобы было задано свойство {"ArticleInstances"}.");
    foreach (long articleInstance in this.articleInstances)
    {
      if (articleInstance == 0L)
        throw new InvalidOperationException($"Свойство {"ArticleInstances"} содержит недопустимые значения.");
    }
  }

  private void CollectArticleInfo()
  {
    if (this.articleInfos == null)
      this.articleInfos = new List<Tuple<long, string, Guid>>(this.articleInstances.Count);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (long articleInstance in this.articleInstances)
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(articleInstance, true);
        string str = (string) null;
        IDBAttribute attributeById1 = dbObject.GetAttributeByID(IDCache.Default.Designation.Id);
        if (attributeById1 != null && !attributeById1.IsNull)
          str = attributeById1.AsString;
        Guid guid = Guid.Empty;
        IDBAttribute attributeById2 = dbObject.GetAttributeByID(IDCache.Default.InstanceGroupId.Id);
        if (attributeById2 != null && !attributeById2.IsNull && GuidHelper.IsGuid(attributeById2.AsString))
          guid = new Guid(attributeById2.AsString);
        this.articleInfos.Add(Tuple.Create<long, string, Guid>(articleInstance, str, guid));
      }
    }
  }

  private void CheckArticleDesignations()
  {
    foreach (Tuple<long, string, Guid> articleInfo in this.articleInfos)
    {
      if (string.IsNullOrEmpty(articleInfo.Item2))
        throw new FaultException($"Для получения спецификации требуется, чтобы все исполнения сборочной единицы имели обозначения, но у изделия с идентификатором версии {articleInfo.Item1} не задано обозначение.");
    }
  }

  private void CheckArticleInstanceGroupIds()
  {
    if (this.articleInfos.Count == 1)
      return;
    foreach (Tuple<long, string, Guid> articleInfo in this.articleInfos)
    {
      if (articleInfo.Item3 == Guid.Empty)
        throw new FaultException($"Для получения спецификации требуется, чтобы все исходные сборочные единицы были исполнениями, но у изделия с идентификатором версии {articleInfo.Item1} не заполнен атрибут '{IDCache.Default.InstanceGroupId.Text}'.");
    }
    Tuple<long, string, Guid> articleInfo1 = this.articleInfos[0];
    foreach (Tuple<long, string, Guid> articleInfo2 in this.articleInfos)
    {
      if (articleInfo2.Item3 != articleInfo1.Item3)
        throw new FaultException($"Для получения спецификации требуется, чтобы все исходные сборочные единицы были исполнениями, но у изделий с идентификаторами версий {articleInfo2.Item1} и {articleInfo1.Item1} отличается значение атрибута '{IDCache.Default.InstanceGroupId.Text}'.");
    }
  }

  private void CollectRows(ICollection<SimpleSpecificationRow> specification)
  {
    DBCompositionQuery<SimpleSpecificationRow> articleQuery1 = new DBCompositionQuery<SimpleSpecificationRow>();
    articleQuery1.RelationType = IDCache.Default.ArticleTree.Id;
    articleQuery1.VersionsRule = VersionsRuleSources.GetEditorRule();
    articleQuery1.RecordCount = -1;
    articleQuery1.Conditions.AddRange((IEnumerable<ConditionStructure>) this.MakeCadRelationsConditions());
    articleQuery1.ResultObjectTypeFilter = IDCache.Default.AllArticles.Id;
    articleQuery1.ResultBuilder = (DBQueryRecordBuilder<SimpleSpecificationRow>) new SimpleSpecificationRowBuilder();
    DBCompositionQuery<SimpleSpecificationRow> articleQuery2 = articleQuery1.Clone();
    articleQuery2.ResultObjectTypeFilter = IDCache.Default.AllMaterials.Id;
    articleQuery2.ResultBuilder = (DBQueryRecordBuilder<SimpleSpecificationRow>) new SimpleSpecificationRowBuilder(CADDocumentResources.EMB_MaterialsSection);
    foreach (Tuple<long, string, Guid> articleInfo in this.articleInfos)
    {
      this.CollectRows(articleInfo.Item1, articleInfo.Item2, articleQuery1, specification);
      this.CollectRows(articleInfo.Item1, articleInfo.Item2, articleQuery2, specification);
    }
  }

  private void CollectRows(
    long articleId,
    string articleDesignation,
    DBCompositionQuery<SimpleSpecificationRow> articleQuery,
    ICollection<SimpleSpecificationRow> specification)
  {
    foreach (SimpleSpecificationRow newRow in articleQuery.ConsistFrom(articleId))
    {
      SimpleSpecificationRow specificationRow = this.FindSameRow(specification, newRow);
      if (specificationRow == null)
      {
        specificationRow = newRow;
        specification.Add(specificationRow);
      }
      specificationRow.ProjectDesignations.Add(articleDesignation);
    }
  }

  private SimpleSpecificationRow FindSameRow(
    ICollection<SimpleSpecificationRow> specification,
    SimpleSpecificationRow newRow)
  {
    return CollectionUtils.Find<SimpleSpecificationRow>((IEnumerable<SimpleSpecificationRow>) specification, (Predicate<SimpleSpecificationRow>) (existingRow => this.IsSameOccurence(existingRow, newRow))) ?? CollectionUtils.Find<SimpleSpecificationRow>((IEnumerable<SimpleSpecificationRow>) specification, (Predicate<SimpleSpecificationRow>) (existingRow => this.IsSamePosition(existingRow, newRow))) ?? (SimpleSpecificationRow) null;
  }

  private bool IsSameOccurence(SimpleSpecificationRow x, SimpleSpecificationRow y)
  {
    return x.ObjectId == y.ObjectId && x.OccurenceGuid != Guid.Empty && x.OccurenceGuid == y.OccurenceGuid && MeasureHelper.Compare(x.Count, y.Count) == CompareResult.Equal;
  }

  private bool IsSamePosition(SimpleSpecificationRow x, SimpleSpecificationRow y)
  {
    return x.ObjectId == y.ObjectId && string.Equals(x.Position, y.Position, StringComparison.InvariantCulture) && MeasureHelper.Compare(x.Count, y.Count) == CompareResult.Equal;
  }

  private ConditionStructure[] MakeCadRelationsConditions()
  {
    return new ConditionStructure[2]
    {
      new ConditionStructure(IDCache.Default.OccurenceKey.Id, RelationalOperators.NotEmpty, (object) null, LogicalOperators.OR, 0, true),
      new ConditionStructure(IDCache.Default.BasedOnCADModel.Id, RelationalOperators.Equal, (object) true, LogicalOperators.NONE, 0, true)
    };
  }

  private void MarkCommonPart(ICollection<SimpleSpecificationRow> specification)
  {
    foreach (SimpleSpecificationRow specificationRow in (IEnumerable<SimpleSpecificationRow>) specification)
    {
      if (specificationRow.ProjectDesignations.Count == this.articleInfos.Count)
        specificationRow.ProjectDesignations.Clear();
    }
  }

  private static void TraceSpecification(IEnumerable<SimpleSpecificationRow> specification)
  {
    foreach (SimpleSpecificationRow specificationRow in specification)
    {
      string str = specificationRow.GetProjectDesignationsList();
      if (string.IsNullOrEmpty(str))
        str = "<all projects>";
      Trace.WriteLine($"Component: object id={specificationRow.ObjectId}, guid={specificationRow.OccurenceGuid:D}, position='{specificationRow.Position}', count={specificationRow.Count} in '{str}'");
    }
  }
}
