// Decompiled with JetBrains decompiler
// Type: Intermech.Compass3D.Integrator.Drawing2DComponentArticleLocatorService
// Assembly: Intermech.Compass3D.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E9700F29-129D-4EBE-8417-980BAD3DC32C
// Assembly location: D:\IPS\Client\Intermech.Compass3D.Integrator.dll

using Intermech.Data;
using Intermech.Data.SectionEntities;
using Intermech.Interfaces;
using Intermech.Interfaces.Data;
using Intermech.Tools.Data;
using Intermech.Tools.DataExchange;
using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.CADInterface;
using Intermech.Tools.Integrators.Mechanical;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Compass3D.Integrator;

internal sealed class Drawing2DComponentArticleLocatorService(
  K3DCaptureChangesDriver driver,
  CaptureChangesDriverContext driverContext) : ArticleLocatorService((MechanicalDriver) driver, driverContext)
{
  protected override IObjectLocator DoCreateNormalArticleLocator(SectionEntity articleItem)
  {
    List<IObjectLocator> locators = new List<IObjectLocator>();
    locators.Add((IObjectLocator) new IdentityArticleLocator((IIdentityArticleLocatorData) new IdentityArticleLocatorData(articleItem)));
    ValueBag workingSet = articleItem.Sections.Get<AttributesSection>().WorkingSet;
    ValueRecord attributeRecord = workingSet.Find((StringKey) CADVirtualAttributes.ArticleSection);
    if (attributeRecord != null && this.HasStringValue(attributeRecord))
    {
      int? rootArticleType = this.ArticleSectionToRootArticleType((string) attributeRecord.Value);
      if (rootArticleType.HasValue)
        this.TryAddImbaseLocator(articleItem, workingSet, rootArticleType.Value, locators);
    }
    return locators.Count != 1 ? (IObjectLocator) new CompositeObjectLocator((IEnumerable<IObjectLocator>) locators) : locators[0];
  }

  protected override IObjectLocator DoCreateImbaseObjectLocator(SectionEntity articleItem)
  {
    throw new NotSupportedException("Использование ключей IMBASE в записях спецификации Компас 3D не поддерживается.");
  }

  protected override IObjectLocator DoCreateMinorMaterialLocator(SectionEntity articleItem)
  {
    List<IObjectLocator> locators = new List<IObjectLocator>();
    locators.Add((IObjectLocator) new IdentityArticleLocator((IIdentityArticleLocatorData) new IdentityArticleLocatorData(articleItem)));
    ValueBag workingSet = articleItem.Sections.Get<AttributesSection>().WorkingSet;
    int id = IDCache.Default.AllMaterials.Id;
    this.TryAddImbaseLocator(articleItem, workingSet, id, locators);
    return locators.Count != 1 ? (IObjectLocator) new CompositeObjectLocator((IEnumerable<IObjectLocator>) locators) : locators[0];
  }

  private void TryAddImbaseLocator(
    SectionEntity articleItem,
    ValueBag articleAttributes,
    int rootArticleType,
    List<IObjectLocator> locators)
  {
    ValueRecord attributeRecord1 = articleAttributes.Find((StringKey) IDCache.Default.Designation.Text);
    if (attributeRecord1 != null && this.HasStringValue(attributeRecord1))
      locators.Add((IObjectLocator) new ImbaseAttributeArticleLocator((IImbaseAttributeLocatorData) new ImbaseAttributeArticleLocatorData(articleItem, rootArticleType, (LocalId<int>) IDCache.Default.Designation.GID)));
    ValueRecord attributeRecord2 = articleAttributes.Find((StringKey) IDCache.Default.Name.Text);
    if (attributeRecord2 == null || !this.HasStringValue(attributeRecord2))
      return;
    locators.Add((IObjectLocator) new ImbaseAttributeArticleLocator((IImbaseAttributeLocatorData) new ImbaseAttributeArticleLocatorData(articleItem, rootArticleType, (LocalId<int>) IDCache.Default.Name.GID)));
  }

  private bool HasStringValue(ValueRecord attributeRecord)
  {
    return !attributeRecord.IsNull && attributeRecord.DataType == typeof (string) && !string.IsNullOrEmpty((string) attributeRecord.Value);
  }

  private int? ArticleSectionToRootArticleType(string sectionName)
  {
    if (string.Compare(IDCache.Default.StandardArticles.Text, sectionName, StringComparison.CurrentCultureIgnoreCase) == 0)
      return new int?(IDCache.Default.StandardArticles.Id);
    return string.Compare(IDCache.Default.AssistiveArticles.Text, sectionName, StringComparison.CurrentCultureIgnoreCase) == 0 ? new int?(IDCache.Default.AssistiveArticles.Id) : new int?();
  }
}
