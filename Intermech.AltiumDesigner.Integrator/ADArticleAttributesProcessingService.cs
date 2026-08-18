// Decompiled with JetBrains decompiler
// Type: Intermech.AltiumDesigner.Integrator.ADArticleAttributesProcessingService
// Assembly: Intermech.AltiumDesigner.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 4CE9F573-7E4B-4FE9-9600-ADBDE2EC9D6B
// Assembly location: D:\IPS\Client\Intermech.AltiumDesigner.Integrator.dll

using Intermech.AltiumDesigner.Interfaces;
using Intermech.Data;
using Intermech.Data.SectionEntities;
using Intermech.Interfaces;
using Intermech.Tools.DataExchange;
using Intermech.Tools.Integrators.Electrical;
using Intermech.Tools.Integrators.Mechanical;
using System;

#nullable disable
namespace Intermech.AltiumDesigner.Integrator;

internal sealed class ADArticleAttributesProcessingService(
  MechanicalDriver driver,
  CaptureChangesDriverContext driverContext) : ArticleAttributesProcessingService(driver, driverContext)
{
  protected override void DoPreprocessAttributes(
    SectionEntity articleItem,
    ValueBag workingSet,
    ValueBag databaseSet)
  {
    base.DoPreprocessAttributes(articleItem, workingSet, databaseSet);
    ElectricalArticleCache electricalArticleCache = articleItem.Sections.Get<ElectricalArticleCache>();
    if (electricalArticleCache.ArticleType != ArticleTypes.Component)
      return;
    int componentKind = this.GetComponentKind((ParametersContainer) electricalArticleCache.Article);
    if (componentKind == -1)
      return;
    workingSet.Add(new ValueRecord((StringKey) MetaDataHelper.GetAttributeTypeName(AltiumAttributes.attributeComponentKind), (object) componentKind));
  }

  private int GetComponentKind(ParametersContainer article)
  {
    Parameter parameter = Array.Find<Parameter>(article.Parameters, (Predicate<Parameter>) (p => p.Name.Equals("ComponentKind")));
    return parameter != null ? (int) parameter.Value : -1;
  }
}
