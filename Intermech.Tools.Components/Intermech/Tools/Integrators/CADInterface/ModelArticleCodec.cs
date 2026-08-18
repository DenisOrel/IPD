// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.ModelArticleCodec
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Collections;
using Intermech.ControlFlow;
using Intermech.Data;
using Intermech.Interfaces;
using Intermech.Tools.Components.Properties;
using Intermech.Tools.Data;
using Intermech.Tools.Integrators.Mechanical;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

public sealed class ModelArticleCodec : ArticleAttributesCodec
{
  private readonly IServiceProvider integrator;

  public ModelArticleCodec(IServiceProvider integrator)
    : base((IValueBagFormatter) new ModelArticleFormatter())
  {
    this.integrator = integrator;
    this.Formatter.ReadTargetStrategy = (ModelArticleParametersReadTargetStrategy) new DefaultModelArticleParametersReadTargetStrategy();
    this.Formatter.GetForbiddenDocumentAttributes += new EventHandler<ModelArticleFormatter.ForbiddenDocumentAttributes>(this.GetForbiddenDocumentAttributes);
  }

  private ModelArticleFormatter Formatter => (ModelArticleFormatter) base.Formatter;

  private void GetForbiddenDocumentAttributes(
    object sender,
    ModelArticleFormatter.ForbiddenDocumentAttributes e)
  {
    List<StringKey> stringKeyList = new List<StringKey>((IEnumerable<StringKey>) ServiceUtils.GetService<ICADSettingsService>((object) this.integrator, true).SynchronizedDocumentAttributes.GetAttributes());
    CollectionUtils.RemoveAll<StringKey>((IList<StringKey>) stringKeyList, (Predicate<StringKey>) (key => !this.IsAttributeSupported(key)));
    e.Keys.AddRange<StringKey>((IEnumerable<StringKey>) this.GetContainerValueKeys((ICollection<StringKey>) stringKeyList));
  }

  protected override IAttributeLayout GetContainerAttributeLayout(StringKey attributeKey)
  {
    if (attributeKey == (StringKey) IDCache.Default.MaterialReplacement1.Text)
      return (IAttributeLayout) new MaterialAttributeLayout(this.GetContainerValueKey(attributeKey), (StringKey) CADDocumentResources.EMB_MaterialID1);
    return attributeKey == (StringKey) IDCache.Default.MaterialReplacement2.Text ? (IAttributeLayout) new MaterialAttributeLayout(this.GetContainerValueKey(attributeKey), (StringKey) CADDocumentResources.EMB_MaterialID2) : base.GetContainerAttributeLayout(attributeKey);
  }

  protected override IAction EmitDecodeAction(
    IValueBagContainer container,
    StringKey attributeKey,
    ContainerValues containerValues,
    ValueBag attributes,
    DecodeAttributesOptions options)
  {
    if (attributeKey == (StringKey) CADDocumentResources.EMB_PDMFlagAttribute)
      return (IAction) new DecodeConvertibleValueAction(containerValues.Bag, this.GetContainerValueKey(attributeKey), attributes, attributeKey, typeof (int));
    if (attributeKey == (StringKey) CADDocumentResources.EMB_MassFormat)
      return (IAction) new DecodeConvertibleValueAction(containerValues.Bag, this.GetContainerValueKey(attributeKey), attributes, attributeKey, typeof (int));
    if (attributeKey == (StringKey) CADDocumentResources.EMB_IgnoreConfiguration)
      return (IAction) new DecodeConvertibleValueAction(containerValues.Bag, this.GetContainerValueKey(attributeKey), attributes, attributeKey, typeof (bool));
    if (attributeKey == (StringKey) CADDocumentResources.EMB_IgnoreConfigurationOld)
      return (IAction) new DecodeConvertibleValueAction(containerValues.Bag, this.GetContainerValueKey(attributeKey), attributes, attributeKey, typeof (bool));
    if (attributeKey == (StringKey) IDCache.Default.MaterialReplacement1.Text)
      return (IAction) new DecodeMaterialAction(containerValues.Bag, this.GetContainerValueKey(attributeKey), attributes, attributeKey, (StringKey) CADDocumentResources.EMB_MaterialID1);
    return attributeKey == (StringKey) IDCache.Default.MaterialReplacement2.Text ? (IAction) new DecodeMaterialAction(containerValues.Bag, this.GetContainerValueKey(attributeKey), attributes, attributeKey, (StringKey) CADDocumentResources.EMB_MaterialID2) : base.EmitDecodeAction(container, attributeKey, containerValues, attributes, options);
  }

  protected override IAction EmitEncodeAction(
    IValueBagContainer container,
    StringKey attributeKey,
    ValueBag attributes,
    ContainerValues containerValues,
    EncodeAttributesOptions options)
  {
    if (attributeKey == (StringKey) IDCache.Default.MaterialReplacement1.Text)
      return (IAction) new EncodeMaterialAction(attributes, attributeKey, containerValues.Bag, this.GetContainerValueKey(attributeKey), (StringKey) CADDocumentResources.EMB_MaterialID1)
      {
        IsOpenMetadataTarget = containerValues.IsOpenMetadata
      };
    if (!(attributeKey == (StringKey) IDCache.Default.MaterialReplacement2.Text))
      return base.EmitEncodeAction(container, attributeKey, attributes, containerValues, options);
    return (IAction) new EncodeMaterialAction(attributes, attributeKey, containerValues.Bag, this.GetContainerValueKey(attributeKey), (StringKey) CADDocumentResources.EMB_MaterialID2)
    {
      IsOpenMetadataTarget = containerValues.IsOpenMetadata
    };
  }
}
