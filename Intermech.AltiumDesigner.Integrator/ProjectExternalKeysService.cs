// Decompiled with JetBrains decompiler
// Type: Intermech.AltiumDesigner.Integrator.ProjectExternalKeysService
// Assembly: Intermech.AltiumDesigner.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 4CE9F573-7E4B-4FE9-9600-ADBDE2EC9D6B
// Assembly location: D:\IPS\Client\Intermech.AltiumDesigner.Integrator.dll

using Intermech.Data.SectionEntities;
using Intermech.Tools.DataExchange;
using Intermech.Tools.Integrators.Electrical;
using Intermech.Tools.Integrators.Mechanical;
using System.Collections.Generic;

#nullable disable
namespace Intermech.AltiumDesigner.Integrator;

internal sealed class ProjectExternalKeysService(
  MechanicalDriver driver,
  CaptureChangesDriverContext driverContext) : MechanicalDriverService(driver, driverContext), IArticleExternalKeysService
{
  public bool HasExternalKeySupport(SectionEntity articleItem, SectionEntity modelItem)
  {
    return articleItem.Sections.Get<ElectricalArticleCache>().ArticleType != 0;
  }

  public void CorrectExternalKeys(List<SectionEntity> articleItems, SectionEntity modelItem)
  {
  }

  public string GetExternalKey(SectionEntity articleItem, SectionEntity modelItem)
  {
    return ((ParametersContainer) articleItem.Sections.Get<ElectricalArticleCache>().Article).InternalId;
  }
}
