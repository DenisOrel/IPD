// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Mechanical.ArticleGenerator
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Client.Core;
using Intermech.ControlFlow;
using Intermech.Data;
using Intermech.Data.EntityDb;
using Intermech.Data.SectionEntities;
using Intermech.Localization;
using Intermech.Tools.Data;
using Intermech.Tools.DataExchange;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Integrators.Mechanical;

internal sealed class ArticleGenerator
{
  private MechanicalDriver driver;

  public ArticleGenerator(MechanicalDriver driver)
  {
    this.driver = driver != null ? driver : throw new ArgumentNullException(nameof (driver));
  }

  public void MakeArticleEntities(
    CaptureChangesDriverContext ctx,
    IEnumerable<InitialArticleData> articleBlanks,
    SectionEntity modelItem)
  {
    if (ctx == null)
      throw new ArgumentNullException();
    if (articleBlanks == null)
      throw new ArgumentNullException();
    if (modelItem == null)
      throw new ArgumentNullException();
    int num = 1;
    foreach (InitialArticleData articleBlank in articleBlanks)
    {
      SectionEntity articleItem = this.EmitArticleItem(ctx, articleBlank, modelItem, num++);
      this.TryAddArticleItemToContext(ctx, articleItem);
    }
  }

  private SectionEntity EmitArticleItem(
    CaptureChangesDriverContext ctx,
    InitialArticleData initData,
    SectionEntity modelItem,
    int seqIndex)
  {
    ObjectSection sectionObject1 = new ObjectSection();
    if (initData.ObjectId != 0L)
    {
      sectionObject1.ExistenceStatus = ObjectExistenceStatus.ExistingObject;
      sectionObject1.ObjectId = initData.ObjectId;
      sectionObject1.ObjectType = DBHelper.GetObjectType(initData.ObjectId);
    }
    else
      sectionObject1.ExistenceStatus = ObjectExistenceStatus.NewObject;
    ArticleSection sectionObject2 = new ArticleSection();
    sectionObject2.ArticleKey = initData.ArticleKey;
    switch (initData.InitialDocumentType)
    {
      case ArticleInitialDocumentType.Normal:
      case ArticleInitialDocumentType.Hidden:
        sectionObject2.SetInitialDocument(initData.InitialDocumentType, modelItem);
        break;
    }
    MechanicalArtcleSection sectionObject3 = new MechanicalArtcleSection();
    sectionObject3.Kind = initData.ArticleKind;
    sectionObject3.SeqIndex = seqIndex;
    DisplaySection sectionObject4 = new DisplaySection()
    {
      DisplayName = string.IsNullOrEmpty(initData.DisplayName) ? LocalizationHolder.rm.GetString("Attribute.Tools.Components_33") : initData.DisplayName
    };
    sectionObject4.QualifiedName = string.Format(LocalizationHolder.rm.GetString("Tools.Components_502"), (object) sectionObject4.DisplayName);
    SectionEntity sectionEntity = new SectionEntity();
    sectionEntity.Sections.Set((object) sectionObject1);
    sectionEntity.Sections.Set((object) sectionObject2);
    sectionEntity.Sections.Set((object) sectionObject3);
    sectionEntity.Sections.Set((object) sectionObject4);
    sectionEntity.Sections.Set((object) new ArticleFiles());
    sectionEntity.Sections.Set((object) new ObjectActionsSection());
    sectionEntity.Sections.CopyFrom((IEnumerable<KeyValuePair<Type, object>>) initData.CustomSections);
    return sectionEntity;
  }

  private void TryAddArticleItemToContext(
    CaptureChangesDriverContext ctx,
    SectionEntity articleItem)
  {
    MechanicalArtcleSection mechanicalArtcleSection = articleItem.Sections.Get<MechanicalArtcleSection>();
    if (mechanicalArtcleSection.Kind == MechanicalArticleKind.Autodetect)
      mechanicalArtcleSection.Kind = this.DetectMechanicalArticleKind(articleItem);
    switch (mechanicalArtcleSection.Kind)
    {
      case MechanicalArticleKind.ReadOnlyArticle:
        ctx.Database.Add((IEntity) articleItem);
        this.driver.SchedulerStages.DerivedObjectsStage.Wait((IAction) new ReadOnlyArticlesHandler(this.driver, ctx, articleItem));
        break;
      case MechanicalArticleKind.NormalArticle:
        this.LazyReadArticleDataFromFile(articleItem);
        if (this.driver.MechanicalOperations.Articles.TryGetArticleMainDocument(articleItem) == null)
        {
          ValueRecord stringAttribute = this.TryReadIdentityAttribute(articleItem);
          if (stringAttribute != null && this.FindArticleItemByStringAttribute(ctx, stringAttribute) != null)
            break;
        }
        ctx.Database.Add((IEntity) articleItem);
        this.driver.SchedulerStages.DerivedObjectsStage.Wait((IAction) this.driver.CreateAndSetupNormalArticleHandler(articleItem));
        break;
      case MechanicalArticleKind.ImbaseObject:
        this.LazyReadArticleDataFromFile(articleItem);
        ValueRecord stringAttribute1 = this.TryReadImbaseKey(articleItem);
        if (stringAttribute1 != null && this.FindArticleItemByStringAttribute(ctx, stringAttribute1) != null)
          break;
        ctx.Database.Add((IEntity) articleItem);
        this.driver.SchedulerStages.DerivedObjectsStage.Wait((IAction) this.driver.CreateAndSetupImbaseObjectArticleHandler(articleItem));
        break;
      case MechanicalArticleKind.MinorMaterial:
        this.LazyReadArticleDataFromFile(articleItem);
        ctx.Database.Add((IEntity) articleItem);
        this.driver.SchedulerStages.DerivedObjectsStage.Wait((IAction) this.driver.CreateAndSetupMinorMaterialArticleHandler(articleItem));
        break;
    }
  }

  /// <summary>
  /// Возвращает значение указанного атрибута и набора, прочитанного из внешнего приложения.
  /// </summary>
  /// <param name="articleItem">Объект изделия</param>
  /// <param name="attributeKey">Ключ атрибута</param>
  /// <returns>Найденный объект значения или null, если атрибут пуст или отсутствует</returns>
  private ValueRecord TryReadStringAttributeFromWorkingSet(
    SectionEntity articleItem,
    StringKey attributeKey)
  {
    AttributesSection attributesSection = articleItem.Sections.Get<AttributesSection>((AttributesSection) null);
    if (attributesSection != null)
    {
      ValueRecord valueRecord = attributesSection.WorkingSet.Find(attributeKey);
      if (valueRecord != null && valueRecord.DataType == typeof (string) && !valueRecord.IsNull && !string.IsNullOrEmpty((string) valueRecord.Value))
        return valueRecord;
    }
    return (ValueRecord) null;
  }

  /// <summary>Возвращает ключ Imbase.</summary>
  /// <param name="articleItem">Объект изделия</param>
  /// <returns>Значение ключа Imbase или null, если атрибут пуст или отсутствует</returns>
  private ValueRecord TryReadImbaseKey(SectionEntity articleItem)
  {
    return this.TryReadStringAttributeFromWorkingSet(articleItem, (StringKey) IDCache.Default.ImbaseKey.Text);
  }

  /// <summary>
  /// Возвращает первый заполненный идентифицирующий атрибут изделия (обозначение, код ОКП или наименование).
  /// </summary>
  /// <param name="articleItem">Объект изделия</param>
  /// <returns>Значение атрибута или null, если все идентифицирующие атрибуты пусты или отсутствуют</returns>
  private ValueRecord TryReadIdentityAttribute(SectionEntity articleItem)
  {
    ICollection<StringKey> identityKeys = this.driver.MechanicalOperations.Articles.GetIdentityKeys();
    return DbOperations.FindIdentityAttribute(articleItem, (IEnumerable<StringKey>) identityKeys, false);
  }

  private SectionEntity FindArticleItemByStringAttribute(
    CaptureChangesDriverContext ctx,
    ValueRecord stringAttribute)
  {
    return ctx.Database.QueryFirst((IQueryCondition) new CompoundSetCondition(CompoundSetOperator.Intersection, new IQueryCondition[2]
    {
      (IQueryCondition) new BinaryCondition(SectionVirtualProperties.SectionTypeRef, BinaryOperator.Equal, (object) typeof (MechanicalArtcleSection)),
      (IQueryCondition) new CodeCondition((Predicate<IEntity>) (otherItem =>
      {
        ValueRecord valueRecord = this.TryReadStringAttributeFromWorkingSet((SectionEntity) otherItem, stringAttribute.Key);
        return valueRecord != null && object.Equals(valueRecord.Value, stringAttribute.Value);
      }))
    }));
  }

  private void LazyReadArticleDataFromFile(SectionEntity articleItem)
  {
    if (articleItem.Sections.Get<AttributesSection>((AttributesSection) null) != null)
      return;
    IArticleCADApiService articleApiService = this.driver.GetArticleApiService(articleItem);
    AttributesSection sectionObject = new AttributesSection()
    {
      EmbeddedSet = articleApiService.ReadArticleProperties(articleItem)
    };
    sectionObject.WorkingSet = articleApiService.DecodeArticleAttributes(articleItem, sectionObject.EmbeddedSet);
    articleItem.Sections.Set((object) sectionObject);
  }

  private MechanicalArticleKind DetectMechanicalArticleKind(SectionEntity articleItem)
  {
    this.LazyReadArticleDataFromFile(articleItem);
    IArticleKindDetectorService kindDetectorService = this.driver.TryGetArticleKindDetectorService(articleItem);
    if (kindDetectorService == null)
      throw new InvalidOperationException($"Не удалось определить вид изделия '{DisplaySection.GetQualifiedName(articleItem)}' и способ его обработки, так как интегратор не реализует сервис '{typeof (IArticleKindDetectorService)}'.");
    if (kindDetectorService.IsImbaseObject(articleItem))
      return MechanicalArticleKind.ImbaseObject;
    return kindDetectorService.IsMinorMaterial(articleItem) ? MechanicalArticleKind.MinorMaterial : MechanicalArticleKind.NormalArticle;
  }
}
