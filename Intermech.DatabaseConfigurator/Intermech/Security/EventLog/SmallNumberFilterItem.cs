// Decompiled with JetBrains decompiler
// Type: Intermech.Security.EventLog.SmallNumberFilterItem
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Client.Core;
using Intermech.Kernel.Search;

#nullable disable
namespace Intermech.Security.EventLog;

internal class SmallNumberFilterItem : FilterItem
{
  private int _value;

  public SmallNumberFilterItem(
    ObligatoryObjectAttributes attributeID,
    FlagsConditions possibleOperators,
    FlagsConditions defaultOperator,
    int defaultValue)
    : base(attributeID, possibleOperators, defaultOperator)
  {
    this._value = defaultValue;
  }

  public int Value
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

  protected override void SetAsString(string value) => this._value = int.Parse(value);

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
    if (!(source is SmallNumberFilterItem numberFilterItem))
      return;
    this._value = numberFilterItem._value;
  }

  public override object Clone()
  {
    SmallNumberFilterItem numberFilterItem = new SmallNumberFilterItem(this.AttributeID, this.PossibleOperators, this.Operator, this._value);
    numberFilterItem.Assign((FilterItem) this);
    return (object) numberFilterItem;
  }
}
