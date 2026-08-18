// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.SynchronizedCADArticleAttributes
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Collections;
using Intermech.Interfaces;
using Intermech.Tools.Components.Properties;
using Intermech.Tools.Data;
using Intermech.Tools.Integrators.Mechanical;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

internal sealed class SynchronizedCADArticleAttributes : SynchronizedArticleAttributes
{
  private SynchronizedCADDocumentAttributes docSynchronizedAttributes;
  private bool sharedModelAttributes;

  internal SynchronizedCADArticleAttributes(ICADSettingsService service)
    : base((IIntegratorSettingsService) service)
  {
  }

  internal void LinkWithDocumentAttributes(
    bool sharedModelAttributes,
    SynchronizedCADDocumentAttributes documentAttributes)
  {
    if (documentAttributes == null)
      throw new ArgumentNullException(nameof (documentAttributes));
    this.sharedModelAttributes = sharedModelAttributes;
    this.docSynchronizedAttributes = documentAttributes;
  }

  internal ICollection<StringKey> GetPredefinedAttributesNonRecursive()
  {
    return this.GetPredefinedAttributes();
  }

  internal ICollection<StringKey> GetVirtualAttributesNonRecursive() => this.GetVirtualAttributes();

  protected override ICollection<StringKey> GetPredefinedAttributes()
  {
    ICollection<StringKey> predefinedAttributes = base.GetPredefinedAttributes();
    predefinedAttributes.Add((StringKey) IDCache.Default.MaterialReplacement1.Text);
    predefinedAttributes.Add((StringKey) IDCache.Default.MaterialReplacement2.Text);
    return predefinedAttributes;
  }

  protected override ICollection<StringKey> GetUserDefinedAttributes()
  {
    ICollection<StringKey> definedAttributes = base.GetUserDefinedAttributes();
    foreach (GlobalId<int> articleAttribute in ((CADSettings) this.Service.GetSettingsObject()).CustomArticleAttributes)
      definedAttributes.Add((StringKey) articleAttribute.Name);
    return definedAttributes;
  }

  protected override ICollection<StringKey> GetVirtualAttributes()
  {
    ICollection<StringKey> virtualAttributes = base.GetVirtualAttributes();
    virtualAttributes.Add((StringKey) IDCache.Default.ImbaseKey.Text);
    virtualAttributes.Add((StringKey) CADDocumentResources.EMB_ArticleTypeAttribute);
    virtualAttributes.Add((StringKey) CADDocumentResources.EMB_ArticleExternalKey);
    virtualAttributes.Add((StringKey) CADDocumentResources.EMB_ArticleLegacyExternalKey);
    virtualAttributes.Add((StringKey) CADDocumentResources.EMB_PDMFlagAttribute);
    virtualAttributes.Add((StringKey) CADDocumentResources.EMB_IgnoreConfiguration);
    virtualAttributes.Add((StringKey) CADDocumentResources.EMB_IgnoreConfigurationOld);
    virtualAttributes.Add((StringKey) CADDocumentResources.EMB_ReplaceWithAttribute);
    virtualAttributes.Add((StringKey) CADDocumentResources.EMB_MassFormat);
    return virtualAttributes;
  }

  protected override void FilterUserDefinedAttributes(
    ICollection<StringKey> list,
    int articleType,
    bool dbOnly)
  {
    base.FilterUserDefinedAttributes(list, articleType, dbOnly);
    if (this.sharedModelAttributes)
    {
      CollectionUtils.RemoveAll<StringKey>(list, (IEnumerable<StringKey>) this.docSynchronizedAttributes.GetAttributes(dbOnly));
    }
    else
    {
      CollectionUtils.RemoveAll<StringKey>(list, (IEnumerable<StringKey>) this.docSynchronizedAttributes.GetPredefinedAttributesNonRecursive());
      if (dbOnly)
        return;
      CollectionUtils.RemoveAll<StringKey>(list, (IEnumerable<StringKey>) this.docSynchronizedAttributes.GetVirtualAttributesNonRecursive());
    }
  }
}
