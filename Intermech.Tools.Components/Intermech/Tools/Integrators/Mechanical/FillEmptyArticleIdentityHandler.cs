// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Mechanical.FillEmptyArticleIdentityHandler
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.ControlFlow;
using Intermech.Data;
using Intermech.Data.SectionEntities;
using Intermech.Localization;
using Intermech.Tools.Data;
using Intermech.Tools.DataExchange;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Integrators.Mechanical;

public sealed class FillEmptyArticleIdentityHandler : IAction
{
  private readonly MechanicalDriver driver;
  private readonly SectionEntity articleItem;
  private readonly SectionEntity modelItem;

  public FillEmptyArticleIdentityHandler(
    MechanicalDriver driver,
    SectionEntity articleItem,
    SectionEntity modelItem)
  {
    if (driver == null)
      throw new ArgumentNullException(nameof (driver));
    if (articleItem == null)
      throw new ArgumentNullException();
    if (modelItem == null)
      throw new ArgumentNullException();
    this.driver = driver;
    this.articleItem = articleItem;
    this.modelItem = modelItem;
  }

  public void Perform()
  {
    if (DbOperations.FindIdentityAttribute(this.articleItem, (IEnumerable<StringKey>) this.driver.MechanicalOperations.Articles.GetIdentityKeys(), false) != null)
      return;
    Tuple<StringKey, string> tuple = this.MakeIdentity();
    this.articleItem.Sections.Get<AttributesSection>().WorkingSet.Update(tuple.Item1, (object) tuple.Item2);
  }

  private Tuple<StringKey, string> MakeIdentity()
  {
    MechanicalArtcleSection mechanicalArtcleSection = this.articleItem.Sections.Get<MechanicalArtcleSection>();
    ObjectSection objectSection = this.articleItem.Sections.Get<ObjectSection>();
    AttributesSection attributesSection = this.modelItem.Sections.Get<AttributesSection>();
    ValueRecord identityAttribute = DbOperations.FindIdentityAttribute(attributesSection.WorkingSet, (IEnumerable<StringKey>) this.driver.Operations.Documents.GetIdentityKeys(), false);
    if (identityAttribute == null)
    {
      identityAttribute = DbOperations.FindIdentityAttribute(attributesSection.DatabaseSet, (IEnumerable<StringKey>) this.driver.Operations.Documents.GetIdentityKeys(), false);
      if (identityAttribute == null)
        throw new InvalidOperationException(string.Format(LocalizationHolder.rm.GetString("Tools.Components_450"), (object) FilesSection.GetMasterFile(this.modelItem)));
    }
    string str1;
    if (identityAttribute.Key == (StringKey) IDCache.Default.Designation.Text)
    {
      string str2 = DocumentDesignationHelper.RemoveDocCode(identityAttribute.Read<string>(string.Empty), ObjectSection.GetObjectType(this.modelItem));
      str1 = mechanicalArtcleSection.SeqIndex == 1 ? str2 : $"{str2} [{Math.Abs(objectSection.ObjectId)}]";
    }
    else
    {
      string str3 = identityAttribute.Read<string>(string.Empty);
      str1 = mechanicalArtcleSection.SeqIndex == 1 ? str3 : $"{str3} [{Math.Abs(objectSection.ObjectId)}]";
    }
    return Tuple.Create<StringKey, string>(identityAttribute.Key, str1);
  }
}
