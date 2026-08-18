// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.NormalDwgArticleEmitter
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using Intermech.Data;
using Intermech.Data.SectionEntities;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Tools.Data;
using Intermech.Tools.DataExchange;
using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.Mechanical;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Cadmech.Integrator;

internal sealed class NormalDwgArticleEmitter : IDwgArticleEmitter
{
  private readonly MechanicalDriver driver;
  private readonly IDrawingTypesInfo drawingTypes;

  public NormalDwgArticleEmitter(MechanicalDriver driver, IDrawingTypesInfo drawingTypes)
  {
    this.driver = driver;
    this.drawingTypes = drawingTypes;
  }

  public ICollection<InitialArticleData> EmitArticles(
    CaptureChangesDriverContext ctx,
    SectionEntity modelItem)
  {
    ICollection<InitialArticleData> initialArticleDatas = (ICollection<InitialArticleData>) new LinkedList<InitialArticleData>();
    Guid typeByDrawingType = this.drawingTypes.GetGroupTypeByDrawingType(ObjectSection.GetObjectType(modelItem), true);
    if (typeByDrawingType == MechanicalSettings.AssemblyDrawingsGroup)
    {
      foreach (DataRow row in (InternalDataCollectionBase) DBDocumentHelper.FindDocumentArticles(ObjectSection.GetObjectId(modelItem), VersionsRuleSources.GetEditorRule(), true).Rows)
      {
        Guid guid = new Guid(Convert.ToString(row[0]));
        long int64 = Convert.ToInt64(row[1]);
        int int32 = Convert.ToInt32(row[2]);
        ctx.Database.AddReferencedDBObject(int64, int32);
      }
    }
    else if (typeByDrawingType == MechanicalSettings.PartDrawingsGroup)
    {
      DwgArticleData dwgArticleData = new DwgArticleData();
      this.EmitArticleFileProperties(dwgArticleData, modelItem);
      this.EmitPossibleArticleTypes(dwgArticleData, modelItem);
      InitialArticleData initialArticleData = new InitialArticleData(MechanicalArticleKind.NormalArticle);
      initialArticleData.DisplayName = "Основное исполнение детали";
      initialArticleData.ArticleKey = "Basic article";
      initialArticleData.InitialDocumentType = ArticleInitialDocumentType.Normal;
      initialArticleData.CustomSections.Set((object) dwgArticleData);
      initialArticleDatas.Add(initialArticleData);
    }
    return initialArticleDatas;
  }

  private void EmitArticleFileProperties(DwgArticleData customData, SectionEntity modelItem)
  {
    AttributesSection attributesSection = modelItem.Sections.Get<AttributesSection>();
    customData.FileProperties.ImportRange((IEnumerable<ValueRecord>) attributesSection.EmbeddedSet.Bag);
    customData.FileProperties.AcceptChanges();
  }

  private void EmitPossibleArticleTypes(DwgArticleData customData, SectionEntity modelItem)
  {
    List<LocalId<int>> possibleArticleTypes = this.driver.MechanicalOperations.Articles.GetPossibleArticleTypes(ObjectSection.GetObjectType(modelItem));
    customData.PossibleObjectTypes.AddRange((IEnumerable<LocalId<int>>) possibleArticleTypes);
  }
}
