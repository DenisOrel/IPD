// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.ModelArticleFormatter
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.CADInterface.Proxies;
using Intermech.Collections;
using Intermech.Data;
using Intermech.Tools.Components.Properties;
using Intermech.Tools.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

public sealed class ModelArticleFormatter : CADInterfaceFormatter
{
  private readonly CADInterfaceFormatter docFormatter;
  private ModelArticleParametersReadTargetStrategy readTargetStrategy;
  private static readonly EmptyModelArticleParametersReadTargetStrategy emptyReadTargetStrategy = new EmptyModelArticleParametersReadTargetStrategy();

  public ModelArticleFormatter()
  {
    this.docFormatter = new CADInterfaceFormatter();
    this.readTargetStrategy = (ModelArticleParametersReadTargetStrategy) ModelArticleFormatter.emptyReadTargetStrategy;
  }

  public ModelArticleParametersReadTargetStrategy ReadTargetStrategy
  {
    get => this.readTargetStrategy;
    set
    {
      this.readTargetStrategy = value != null ? value : throw new ArgumentNullException(nameof (value));
    }
  }

  public override bool IsContainerSupported(IValueBagContainer container)
  {
    return container is CADInterfaceValueBagContainer;
  }

  protected override ValueBag DoRead(IValueBagContainer container, ICollection<StringKey> valueKeys)
  {
    ValueBag values = base.DoRead(container, valueKeys);
    this.ReadMissingValues(container, valueKeys, values);
    return values;
  }

  private void ReadMissingValues(
    IValueBagContainer container,
    ICollection<StringKey> valueKeys,
    ValueBag values)
  {
    if (values.Count >= valueKeys.Count || !this.readTargetStrategy.AllowReadMissingValuesFromDocument(container))
      return;
    List<StringKey> missingValues = this.CalculateMissingValues(valueKeys, values);
    if (missingValues.Count <= 0)
      return;
    foreach (ValueRecord valueRecord in this.docFormatter.Read(this.readTargetStrategy.GetDocumentContainer(container), (ICollection<StringKey>) missingValues).Bag)
      values.Add(valueRecord.Clone());
  }

  private List<StringKey> CalculateMissingValues(ICollection<StringKey> valueKeys, ValueBag values)
  {
    ModelArticleFormatter.ForbiddenDocumentAttributes forbiddenAttrs = new ModelArticleFormatter.ForbiddenDocumentAttributes();
    if (this.GetForbiddenDocumentAttributes != null)
      this.GetForbiddenDocumentAttributes((object) this, forbiddenAttrs);
    forbiddenAttrs.Keys.Remove((StringKey) IDCache.Default.Designation.Text);
    forbiddenAttrs.Keys.Remove((StringKey) IDCache.Default.Name.Text);
    forbiddenAttrs.Keys.Add((StringKey) IDCache.Default.Mass.Text);
    forbiddenAttrs.Keys.Add((StringKey) CADDocumentResources.EMB_MassMeasureAttribute);
    return CollectionUtils.FindAllAsList<StringKey>(valueKeys, (Predicate<StringKey>) (key => !values.Keys.Contains(key) && !forbiddenAttrs.Keys.Contains(key)));
  }

  public event EventHandler<ModelArticleFormatter.ForbiddenDocumentAttributes> GetForbiddenDocumentAttributes;

  public class ForbiddenDocumentAttributes : EventArgs
  {
    private ICollection<StringKey> keys;

    public ForbiddenDocumentAttributes()
    {
      this.keys = (ICollection<StringKey>) new HashSet<StringKey>();
    }

    public ICollection<StringKey> Keys => this.keys;
  }
}
