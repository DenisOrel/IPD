// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Mechanical.ArticleAttributesCodec
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.ControlFlow;
using Intermech.Data;
using Intermech.Tools.Components.Properties;
using Intermech.Tools.Data;

#nullable disable
namespace Intermech.Tools.Integrators.Mechanical;

public class ArticleAttributesCodec(IValueBagFormatter formatter) : DBObjectAttributesCodec(formatter)
{
  protected override IAttributeLayout GetContainerAttributeLayout(StringKey attributeKey)
  {
    if (attributeKey == (StringKey) IDCache.Default.Mass.Text)
      return (IAttributeLayout) new MassAttributeLayout(this.GetContainerValueKey(attributeKey));
    return attributeKey == (StringKey) IDCache.Default.Material.Text ? (IAttributeLayout) new MaterialAttributeLayout(this.GetContainerValueKey(attributeKey), (StringKey) CADDocumentResources.EMB_MaterialID) : base.GetContainerAttributeLayout(attributeKey);
  }

  protected override IAction EmitDecodeAction(
    IValueBagContainer container,
    StringKey attributeKey,
    ContainerValues containerValues,
    ValueBag attributes,
    DecodeAttributesOptions options)
  {
    if (attributeKey == (StringKey) IDCache.Default.Designation.Text)
      return (IAction) new DecodeArticleDesignationAction(containerValues.Bag, this.GetContainerValueKey(attributeKey), attributes, attributeKey, DocumentAttributesOptions.TryGetDocumentTypeFromOptions((IAttributeCodecOptions) options));
    if (attributeKey == (StringKey) IDCache.Default.Material.Text)
      return (IAction) new DecodeMaterialAction(containerValues.Bag, this.GetContainerValueKey(attributeKey), attributes, attributeKey, (StringKey) CADDocumentResources.EMB_MaterialID);
    return attributeKey == (StringKey) IDCache.Default.Mass.Text ? (IAction) new DecodeMassAction(containerValues.Bag, this.GetContainerValueKey(attributeKey), attributes, attributeKey) : base.EmitDecodeAction(container, attributeKey, containerValues, attributes, options);
  }

  protected override IAction EmitEncodeAction(
    IValueBagContainer container,
    StringKey attributeKey,
    ValueBag attributes,
    ContainerValues containerValues,
    EncodeAttributesOptions options)
  {
    if (attributeKey == (StringKey) IDCache.Default.Material.Text)
      return (IAction) new EncodeMaterialAction(attributes, attributeKey, containerValues.Bag, this.GetContainerValueKey(attributeKey), (StringKey) CADDocumentResources.EMB_MaterialID)
      {
        IsOpenMetadataTarget = containerValues.IsOpenMetadata
      };
    if (!(attributeKey == (StringKey) IDCache.Default.Mass.Text))
      return base.EmitEncodeAction(container, attributeKey, attributes, containerValues, options);
    return (IAction) new EncodeMassAction(attributes, attributeKey, containerValues.Bag, this.GetContainerValueKey(attributeKey))
    {
      IsOpenMetadataTarget = containerValues.IsOpenMetadata
    };
  }
}
