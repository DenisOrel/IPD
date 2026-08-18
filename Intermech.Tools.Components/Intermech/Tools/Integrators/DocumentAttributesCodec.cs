// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.DocumentAttributesCodec
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.ControlFlow;
using Intermech.Data;
using Intermech.Tools.Data;

#nullable disable
namespace Intermech.Tools.Integrators;

public class DocumentAttributesCodec : DBObjectAttributesCodec
{
  public DocumentAttributesCodec(IValueBagFormatter formatter)
    : base(formatter)
  {
    this.SaveDesignationSuffix = true;
  }

  protected override IAction EmitDecodeAction(
    IValueBagContainer container,
    StringKey attributeKey,
    ContainerValues containerValues,
    ValueBag attributes,
    DecodeAttributesOptions options)
  {
    return attributeKey == (StringKey) IDCache.Default.Designation.Text ? (IAction) new DecodeDocumentDesignationAction(containerValues.Bag, this.GetContainerValueKey(attributeKey), attributes, attributeKey, DocumentAttributesOptions.TryGetDocumentTypeFromOptions((IAttributeCodecOptions) options)) : base.EmitDecodeAction(container, attributeKey, containerValues, attributes, options);
  }

  protected override IAction EmitEncodeAction(
    IValueBagContainer container,
    StringKey attributeKey,
    ValueBag attributes,
    ContainerValues containerValues,
    EncodeAttributesOptions options)
  {
    if (!(attributeKey == (StringKey) IDCache.Default.Designation.Text))
      return base.EmitEncodeAction(container, attributeKey, attributes, containerValues, options);
    return (IAction) new EncodeDocumentDesignationAction(attributes, attributeKey, containerValues.Bag, this.GetContainerValueKey(attributeKey), DocumentAttributesOptions.TryGetDocumentTypeFromOptions((IAttributeCodecOptions) options), this.SaveDesignationSuffix)
    {
      IsOpenMetadataTarget = containerValues.IsOpenMetadata,
      OptimizeEmptyValues = options.OptimizeEmptyValues
    };
  }

  public bool SaveDesignationSuffix { get; set; }
}
