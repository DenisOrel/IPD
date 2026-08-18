// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.DecodeDocumentDesignationAction
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.ControlFlow;
using Intermech.Data;

#nullable disable
namespace Intermech.Tools.Integrators;

public sealed class DecodeDocumentDesignationAction(
  ValueBag source,
  StringKey sourceKey,
  ValueBag target,
  StringKey targetKey,
  int documentType) : CompositeAction(DecodeDocumentDesignationAction.CreateActions(source, sourceKey, target, targetKey, documentType))
{
  public DecodeDocumentDesignationAction(
    ValueBag source,
    ValueBag target,
    StringKey attributeKey,
    int documentType)
    : this(source, attributeKey, target, attributeKey, documentType)
  {
  }

  private static IAction[] CreateActions(
    ValueBag source,
    StringKey sourceKey,
    ValueBag target,
    StringKey targetKey,
    int documentType)
  {
    return new IAction[2]
    {
      (IAction) new DataTypeFilterAction((TransferValueRecordAction) new CopySourceValueAction(source, sourceKey, target, targetKey), typeof (string), true),
      (IAction) new CorrectDesignationSuffixAction(target, targetKey, documentType)
    };
  }
}
