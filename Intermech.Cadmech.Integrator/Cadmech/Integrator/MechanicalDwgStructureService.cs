// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.MechanicalDwgStructureService
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using Intermech.Collections;
using Intermech.Data.SectionEntities;
using Intermech.Tools.DataExchange;
using Intermech.Tools.Integrators.Mechanical;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.Cadmech.Integrator;

internal sealed class MechanicalDwgStructureService(
  MechanicalDwgDriver driver,
  CaptureChangesDriverContext driverContext) : ArticleStructureService((MechanicalDriver) driver, driverContext)
{
  private MechanicalDwgDriver DwgDriver
  {
    [DebuggerStepThrough] get => (MechanicalDwgDriver) this.Driver;
  }

  protected override bool IsProjectArticle(SectionEntity articleItem, SectionEntity documentItem)
  {
    return documentItem != null && this.DwgDriver.ArticleEmitter is AssemblyDwgArticleEmitter || base.IsProjectArticle(articleItem, documentItem);
  }

  protected override List<ArticleStructureOccurence> DoReadArticleStructure(
    SectionEntity projectArticleItem)
  {
    List<ArticleStructureOccurence> list = base.DoReadArticleStructure(projectArticleItem);
    DwgArticleData dwgArticleData = projectArticleItem.Sections.Get<DwgArticleData>();
    CollectionUtils.EnsureNewItemsCapacity<ArticleStructureOccurence>(list, dwgArticleData.Structure.Count);
    list.AddRange((IEnumerable<ArticleStructureOccurence>) dwgArticleData.Structure);
    return list;
  }
}
