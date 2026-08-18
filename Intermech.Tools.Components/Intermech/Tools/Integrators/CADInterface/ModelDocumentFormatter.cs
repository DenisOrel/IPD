// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.ModelDocumentFormatter
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.CADInterface.Proxies;
using Intermech.Data;
using Intermech.Tools.Components.Properties;
using Intermech.Tools.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

public sealed class ModelDocumentFormatter : CADInterfaceFormatter
{
  private readonly CADInterfaceFormatter cfgFormatter;
  private ModelParametersWriteTargetStrategy writeTargetStrategy;
  private static readonly EmptyModelParametersWriteTargetStrategy emptyWriteTargetStrategy = new EmptyModelParametersWriteTargetStrategy();

  public ModelDocumentFormatter()
  {
    this.cfgFormatter = new CADInterfaceFormatter();
    this.writeTargetStrategy = (ModelParametersWriteTargetStrategy) ModelDocumentFormatter.emptyWriteTargetStrategy;
  }

  public ModelParametersWriteTargetStrategy WriteTargetStrategy
  {
    get => this.writeTargetStrategy;
    set
    {
      this.writeTargetStrategy = value != null ? value : throw new ArgumentNullException(nameof (value));
    }
  }

  public override bool IsContainerSupported(IValueBagContainer container)
  {
    return container is CADInterfaceValueBagContainer;
  }

  protected override ValueBag DoRead(IValueBagContainer container, ICollection<StringKey> valueKeys)
  {
    ValueBag values = base.DoRead(container, (ICollection<StringKey>) this.InjectHelperParameters(valueKeys));
    if (ModelDocumentFormatter.IsIdentityAttributesPresent(valueKeys))
      this.ReadIdentityParameters(container, values);
    return values;
  }

  private List<StringKey> InjectHelperParameters(ICollection<StringKey> valueKeys)
  {
    List<StringKey> valueKeys1 = new List<StringKey>(valueKeys.Count + 4);
    valueKeys1.AddRange((IEnumerable<StringKey>) valueKeys);
    if (ModelDocumentFormatter.IsIdentityAttributesPresent(valueKeys))
      ModelDocumentFormatter.InjectIdentityHelperParameters(valueKeys1);
    return valueKeys1;
  }

  private static bool IsIdentityAttributesPresent(ICollection<StringKey> valueKeys)
  {
    return valueKeys.Contains((StringKey) IDCache.Default.Designation.Text) || valueKeys.Contains((StringKey) IDCache.Default.Name.Text);
  }

  private static void InjectIdentityHelperParameters(List<StringKey> valueKeys)
  {
    valueKeys.Add((StringKey) CADDocumentResources.EMB_DocumentDesignationAttribute);
    valueKeys.Add((StringKey) CADDocumentResources.EMB_DocumentNameAttribute);
  }

  private void ReadIdentityParameters(IValueBagContainer container, ValueBag values)
  {
    if (this.writeTargetStrategy.IsIndependentDesignationMode(container, values))
      this.ReadIndependentIdentity(container, values);
    else
      this.ReadCombinedIdentity(container, values);
  }

  private void ReadIndependentIdentity(IValueBagContainer container, ValueBag values)
  {
    values.Remove((StringKey) IDCache.Default.Designation.Text);
    values.Remove((StringKey) IDCache.Default.Name.Text);
    ValueRecord valueRecord1 = values.Find((StringKey) CADDocumentResources.EMB_DocumentDesignationAttribute);
    if (valueRecord1 != null)
    {
      values.Add((StringKey) IDCache.Default.Designation.Text, valueRecord1.Value);
      valueRecord1.Remove();
    }
    ValueRecord valueRecord2 = values.Find((StringKey) CADDocumentResources.EMB_DocumentNameAttribute);
    if (valueRecord2 == null)
      return;
    values.Add((StringKey) IDCache.Default.Name.Text, valueRecord2.Value);
    valueRecord2.Remove();
  }

  private void ReadCombinedIdentity(IValueBagContainer container, ValueBag values)
  {
    IValueBagContainer articleContainer = this.writeTargetStrategy.GetBasicArticleContainer(container, values);
    foreach (ValueRecord valueRecord in this.cfgFormatter.Read(articleContainer, (ICollection<StringKey>) new StringKey[2]
    {
      (StringKey) IDCache.Default.Designation.Text,
      (StringKey) IDCache.Default.Name.Text
    }).Bag)
    {
      if (values.Exists(valueRecord.Key))
        values.Remove(valueRecord.Key);
      values.Add(valueRecord.Clone());
    }
    values.Remove((StringKey) CADDocumentResources.EMB_DocumentDesignationAttribute);
    values.Remove((StringKey) CADDocumentResources.EMB_DocumentNameAttribute);
  }

  protected override void DoWrite(
    IValueBagContainer container,
    ContainerValues values,
    ICollection<StringKey> changedValues)
  {
    this.WriteIdentityParameters(container, values, changedValues);
    base.DoWrite(container, values, changedValues);
  }

  private void WriteIdentityParameters(
    IValueBagContainer container,
    ContainerValues values,
    ICollection<StringKey> changedValues)
  {
    bool hasDesignation = changedValues.Contains((StringKey) IDCache.Default.Designation.Text);
    bool hasName = changedValues.Contains((StringKey) IDCache.Default.Name.Text);
    if (!(hasDesignation | hasName))
      return;
    if (this.writeTargetStrategy.IsIndependentDesignationMode(container, values.Bag))
      this.WriteIndependentIdentity(container, values, changedValues, hasDesignation, hasName);
    else
      this.WriteCombinedIdentity(container, values, changedValues, hasDesignation, hasName);
  }

  private void WriteIndependentIdentity(
    IValueBagContainer container,
    ContainerValues values,
    ICollection<StringKey> changedValues,
    bool hasDesignation,
    bool hasName)
  {
    if (hasDesignation)
    {
      ValueRecord valueRecord1 = values.Bag.Find((StringKey) IDCache.Default.Designation.Text);
      ValueRecord valueRecord2 = values.Bag.Add((StringKey) CADDocumentResources.EMB_DocumentDesignationAttribute, valueRecord1.Value);
      valueRecord2.Flags.Copy(valueRecord1.Flags, NamedFlags.ThrowSetException);
      changedValues.Add(valueRecord2.Key);
      valueRecord1.Remove();
      changedValues.Remove(valueRecord1.Key);
    }
    if (!hasName)
      return;
    ValueRecord valueRecord3 = values.Bag.Find((StringKey) IDCache.Default.Name.Text);
    ValueRecord valueRecord4 = values.Bag.Add((StringKey) CADDocumentResources.EMB_DocumentNameAttribute, valueRecord3.Value);
    valueRecord4.Flags.Copy(valueRecord3.Flags, NamedFlags.ThrowSetException);
    changedValues.Add(valueRecord4.Key);
    valueRecord3.Remove();
    changedValues.Remove(valueRecord3.Key);
  }

  private void WriteCombinedIdentity(
    IValueBagContainer container,
    ContainerValues values,
    ICollection<StringKey> changedValues,
    bool hasDesignation,
    bool hasName)
  {
    IValueBagContainer articleContainer = this.writeTargetStrategy.GetBasicArticleContainer(container, values.Bag);
    ValueBag bag = new ValueBag();
    if (hasDesignation)
    {
      ValueRecord valueRecord = values.Bag.Find((StringKey) IDCache.Default.Designation.Text);
      bag.Add(valueRecord.Clone());
      valueRecord.Remove();
      changedValues.Remove(valueRecord.Key);
    }
    if (hasName)
    {
      ValueRecord valueRecord = values.Bag.Find((StringKey) IDCache.Default.Name.Text);
      bag.Add(valueRecord.Clone());
      valueRecord.Remove();
      changedValues.Remove(valueRecord.Key);
    }
    this.cfgFormatter.Write(articleContainer, new ContainerValues(bag, true));
  }
}
