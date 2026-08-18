// Decompiled with JetBrains decompiler
// Type: Intermech.Security.EventLog.BigNumberFilterItem
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Client.Core;
using Intermech.Kernel.Search;

#nullable disable
namespace Intermech.Security.EventLog;

internal class BigNumberFilterItem : FilterItem
{
  private long _value;

  public BigNumberFilterItem(
    ObligatoryObjectAttributes attributeID,
    FlagsConditions possibleOperators,
    FlagsConditions defaultOperator,
    long defaultValue)
    : base(attributeID, possibleOperators, defaultOperator)
  {
    this._value = defaultValue;
  }

  public long Value
  {
    get => this._value;
    set
    {
      if (this._value == value)
        return;
      this._value = value;
      this.DiscardCachedValues();
    }
  }

  protected override string GetAsString() => this._value.ToString();

  protected override void SetAsString(string value) => this._value = long.Parse(value);

  protected override ConditionStructure[] GetQueryConditions()
  {
    return new ConditionStructure[1]
    {
      new ConditionStructure((int) this.AttributeID, FlagsConditionsHelper.ConvertToRelationalOperators(this.Operator), (object) this._value, LogicalOperators.NONE, 0, false)
    };
  }

  public override void Assign(FilterItem source)
  {
    base.Assign(source);
    if (!(source is BigNumberFilterItem numberFilterItem))
      return;
    this._value = numberFilterItem._value;
  }

  public override object Clone()
  {
    BigNumberFilterItem numberFilterItem = new BigNumberFilterItem(this.AttributeID, this.PossibleOperators, this.Operator, this.Value);
    numberFilterItem.Assign((FilterItem) this);
    return (object) numberFilterItem;
  }
}
