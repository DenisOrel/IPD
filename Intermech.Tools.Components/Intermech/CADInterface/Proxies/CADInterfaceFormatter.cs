// Decompiled with JetBrains decompiler
// Type: Intermech.CADInterface.Proxies.CADInterfaceFormatter
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Collections;
using Intermech.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.CADInterface.Proxies;

public class CADInterfaceFormatter : OpenMetadataValueBagFormatter
{
  private CADInterfaceFormatterMode mode;

  public CADInterfaceFormatter()
    : this(CADInterfaceFormatterMode.Default)
  {
  }

  public CADInterfaceFormatter(CADInterfaceFormatterMode mode) => this.mode = mode;

  public override bool IsContainerSupported(IValueBagContainer container)
  {
    return container is CADInterfaceValueBagContainer;
  }

  protected IParametersContainerProxy GetParametersContainer(IValueBagContainer container)
  {
    return ((CADInterfaceValueBagContainer) container).CADInterfaceObject;
  }

  protected override ValueBag DoRead(IValueBagContainer container, ICollection<StringKey> valueKeys)
  {
    IParametersContainerProxy parametersContainer = this.GetParametersContainer(container);
    switch (this.mode)
    {
      case CADInterfaceFormatterMode.Default:
        return this.ReadDefault(parametersContainer, valueKeys);
      case CADInterfaceFormatterMode.UncheckedRead:
        return this.ReadUnchecked(parametersContainer, valueKeys);
      default:
        throw new NotSupportedEnumException((Enum) this.mode);
    }
  }

  private ValueBag ReadDefault(
    IParametersContainerProxy appParameters,
    ICollection<StringKey> valueKeys)
  {
    List<string> parameterNames = new List<string>((IEnumerable<string>) appParameters.GetParameterNames());
    parameterNames.RemoveAll((Predicate<string>) (valueName => !valueKeys.Contains((StringKey) valueName)));
    List<ValueRecord> initialItems = new List<ValueRecord>(parameterNames.Count);
    if (parameterNames.Count > 0)
      initialItems.AddRange((IEnumerable<ValueRecord>) appParameters.GetParameters((IList<string>) parameterNames));
    return new ValueBag((ICollection<ValueRecord>) initialItems);
  }

  private ValueBag ReadUnchecked(
    IParametersContainerProxy appParameters,
    ICollection<StringKey> valueKeys)
  {
    List<string> parameterNames = CollectionUtils.ConvertAsList<StringKey, string>(valueKeys, (Converter<StringKey, string>) (item => (string) item));
    List<ValueRecord> initialItems = new List<ValueRecord>(valueKeys.Count);
    if (valueKeys.Count > 0)
    {
      initialItems.AddRange((IEnumerable<ValueRecord>) appParameters.GetParameters((IList<string>) parameterNames));
      initialItems.RemoveAll((Predicate<ValueRecord>) (item => item.IsNull));
    }
    return new ValueBag((ICollection<ValueRecord>) initialItems);
  }

  protected override void DoWrite(
    IValueBagContainer container,
    ContainerValues values,
    ICollection<StringKey> changedValues)
  {
    this.GetParametersContainer(container).SetParameters((IList<ValueRecord>) values.Bag.FindAll((Predicate<ValueRecord>) (record => changedValues.Contains(record.Key))));
  }
}
