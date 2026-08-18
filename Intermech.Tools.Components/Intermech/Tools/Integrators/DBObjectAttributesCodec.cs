// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.DBObjectAttributesCodec
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.ControlFlow;
using Intermech.Data;
using Intermech.Interfaces;
using Intermech.Tools.Data;
using System;

#nullable disable
namespace Intermech.Tools.Integrators;

public class DBObjectAttributesCodec(IValueBagFormatter formatter) : BasicAttributeCodec(formatter)
{
  protected override IAction EmitDecodeAction(
    IValueBagContainer container,
    StringKey attributeKey,
    ContainerValues containerValues,
    ValueBag attributes,
    DecodeAttributesOptions options)
  {
    Tuple<int, Type> attributeDataType = DBObjectAttributesCodec.GetAttributeDataType(attributeKey);
    return attributeDataType.Item1 != -1 && attributeDataType.Item2 != (Type) null && attributeDataType.Item2 == typeof (string) ? (IAction) new DataTypeFilterAction((TransferValueRecordAction) new CopySourceValueAction(containerValues.Bag, this.GetContainerValueKey(attributeKey), attributes, attributeKey), attributeDataType.Item2, true) : base.EmitDecodeAction(container, attributeKey, containerValues, attributes, options);
  }

  private static Tuple<int, Type> GetAttributeDataType(StringKey attributeKey)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType((string) attributeKey, false);
      return attributeType == null ? Tuple.Create<int, Type>(-1, (Type) null) : Tuple.Create<int, Type>(attributeType.AttributeID, DBAttributeHelper.TryGetDataType(attributeType));
    }
  }
}
