// Decompiled with JetBrains decompiler
// Type: Intermech.CADInterface.Proxies.StructureComponentOccurenceCodec
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.ControlFlow;
using Intermech.Data;
using Intermech.Tools.Data;

#nullable disable
namespace Intermech.CADInterface.Proxies;

internal sealed class StructureComponentOccurenceCodec(IValueBagFormatter formatter) : 
  BasicAttributeCodec(formatter)
{
  protected override IAction EmitDecodeAction(
    IValueBagContainer container,
    StringKey attributeKey,
    ContainerValues containerValues,
    ValueBag attributes,
    DecodeAttributesOptions options)
  {
    if (attributeKey == (StringKey) IDCache.Default.Position.Text)
      return (IAction) new DecodeConvertibleValueAction(containerValues.Bag, attributes, attributeKey, typeof (int));
    return attributeKey == (StringKey) IDCache.Default.Note.Text ? (IAction) new DataTypeFilterAction((TransferValueRecordAction) new CopySourceValueAction(containerValues.Bag, attributes, attributeKey), typeof (string), true) : base.EmitDecodeAction(container, attributeKey, containerValues, attributes, options);
  }
}
