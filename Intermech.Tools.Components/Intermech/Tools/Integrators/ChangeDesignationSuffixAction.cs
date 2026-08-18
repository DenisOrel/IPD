// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.ChangeDesignationSuffixAction
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data;

#nullable disable
namespace Intermech.Tools.Integrators;

public abstract class ChangeDesignationSuffixAction : ChangeValueRecordAction
{
  protected readonly int documentType;

  protected ChangeDesignationSuffixAction(ValueBag bag, StringKey valueKey, int documentType)
    : base(bag, valueKey)
  {
    this.documentType = documentType;
  }

  public sealed override void Perform()
  {
    ValueRecord valueRecord = this.Bag.Find(this.ValueKey);
    if (valueRecord == null || valueRecord.IsNull || !(valueRecord.DataType == typeof (string)))
      return;
    string designation = valueRecord.Read<string>((string) null);
    if (string.IsNullOrEmpty(designation))
      return;
    string str = this.ChangeSuffix(designation);
    valueRecord.Value = (object) str;
  }

  protected abstract string ChangeSuffix(string designation);
}
