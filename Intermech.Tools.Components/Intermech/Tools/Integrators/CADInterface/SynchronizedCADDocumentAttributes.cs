// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.SynchronizedCADDocumentAttributes
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Collections;
using Intermech.Interfaces;
using Intermech.Tools.Components.Properties;
using Intermech.Tools.Integrators.Mechanical;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

internal sealed class SynchronizedCADDocumentAttributes : SynchronizedDocumentAttributes
{
  private SynchronizedCADArticleAttributes artSynchronizedAttributes;
  private bool sharedModelAttributes;

  internal SynchronizedCADDocumentAttributes(ICADSettingsService service)
    : base((IIntegratorSettingsService) service)
  {
  }

  public void LinkWithArticleAttributes(
    bool sharedModelAttributes,
    SynchronizedCADArticleAttributes articleAttributes)
  {
    if (articleAttributes == null)
      throw new ArgumentNullException(nameof (articleAttributes));
    this.sharedModelAttributes = sharedModelAttributes;
    this.artSynchronizedAttributes = articleAttributes;
  }

  internal ICollection<StringKey> GetPredefinedAttributesNonRecursive()
  {
    return this.GetPredefinedAttributes();
  }

  internal ICollection<StringKey> GetVirtualAttributesNonRecursive() => this.GetVirtualAttributes();

  protected override ICollection<StringKey> GetUserDefinedAttributes()
  {
    ICollection<StringKey> definedAttributes = base.GetUserDefinedAttributes();
    foreach (GlobalId<int> documentAttribute in ((CADSettings) this.Service.GetSettingsObject()).CustomDocumentAttributes)
      definedAttributes.Add((StringKey) documentAttribute.Name);
    return definedAttributes;
  }

  protected override ICollection<StringKey> GetVirtualAttributes()
  {
    ICollection<StringKey> virtualAttributes = base.GetVirtualAttributes();
    virtualAttributes.Add((StringKey) CADDocumentResources.EMB_DocumentTypeAttribute);
    virtualAttributes.Add((StringKey) CADDocumentResources.EMB_DesignTypeAttribute);
    virtualAttributes.Add((StringKey) CADDocumentResources.EMB_DocumentCode);
    return virtualAttributes;
  }

  protected override void FilterUserDefinedAttributes(
    ICollection<StringKey> list,
    int documentType,
    bool dbOnly)
  {
    base.FilterUserDefinedAttributes(list, documentType, dbOnly);
    CollectionUtils.RemoveAll<StringKey>(list, (IEnumerable<StringKey>) this.artSynchronizedAttributes.GetPredefinedAttributesNonRecursive());
    if (dbOnly)
      return;
    CollectionUtils.RemoveAll<StringKey>(list, (IEnumerable<StringKey>) this.artSynchronizedAttributes.GetVirtualAttributesNonRecursive());
  }
}
