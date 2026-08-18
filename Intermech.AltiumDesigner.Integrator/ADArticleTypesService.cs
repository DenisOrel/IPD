// Decompiled with JetBrains decompiler
// Type: Intermech.AltiumDesigner.Integrator.ADArticleTypesService
// Assembly: Intermech.AltiumDesigner.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 4CE9F573-7E4B-4FE9-9600-ADBDE2EC9D6B
// Assembly location: D:\IPS\Client\Intermech.AltiumDesigner.Integrator.dll

using Intermech.Data.SectionEntities;
using Intermech.Interfaces;
using Intermech.Tools.DataExchange;
using Intermech.Tools.Integrators.Electrical;
using Intermech.Tools.Integrators.Mechanical;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.AltiumDesigner.Integrator;

internal sealed class ADArticleTypesService : ArticleTypesService
{
  private SettingsService settingsService;

  public ADArticleTypesService(
    MechanicalDriver driver,
    CaptureChangesDriverContext driverContext,
    SettingsService settingsService)
    : base(driver, driverContext)
  {
    this.settingsService = settingsService != null ? settingsService : throw new ArgumentNullException(nameof (settingsService));
  }

  protected override string DoGetArticleTypeAttributeName(SectionEntity articleItem)
  {
    return "Article type";
  }

  protected override List<LocalId<int>> DoGetPossibleArticleTypes(SectionEntity articleItem)
  {
    ElectricalArticleCache electricalArticleCache = articleItem.Sections.Get<ElectricalArticleCache>();
    switch (electricalArticleCache.ArticleType)
    {
      case ArticleTypes.Component:
        Guid componentObjectType = ADArticleTypesService.GetComponentObjectType(this.settingsService.GetSettings(), (IElectricalComponent) electricalArticleCache.Article);
        List<LocalId<int>> possibleArticleTypes = new List<LocalId<int>>(1);
        IMSObjectType objectType = MetaDataHelper.GetObjectType(componentObjectType);
        possibleArticleTypes.Add(new LocalId<int>(objectType.ObjectTypeID, objectType.ObjectTypeName));
        return possibleArticleTypes;
      case ArticleTypes.Assembly:
      case ArticleTypes.VirtualAssembly:
        return this.Driver.MechanicalOperations.Articles.GetPossibleArticleTypes(articleItem);
      default:
        throw new ArgumentOutOfRangeException("cache.ArticleType");
    }
  }

  public static Guid GetComponentObjectType(
    ADIntegratorSettings settings,
    IElectricalComponent component)
  {
    if (!string.IsNullOrEmpty(settings.PartTypeParameter))
    {
      object propertyValue = component.GetPropertyValue(settings.PartTypeParameter);
      if (propertyValue != null)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBObjectType objectType = sessionKeeper.Session.GetObjectType(Convert.ToString(propertyValue), false);
          if (objectType != null)
            return (objectType as IDBGuid).GUID;
        }
      }
    }
    return new Guid("cad0038d-306c-11d8-b4e9-00304f19f545");
  }
}
