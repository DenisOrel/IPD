// Decompiled with JetBrains decompiler
// Type: Intermech.Security.EventLog.DateTimeFilterItem
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Client.Core;
using Intermech.Kernel.Search;
using System;

#nullable disable
namespace Intermech.Security.EventLog;

internal class DateTimeFilterItem : FilterItem
{
  private DateTime _value;

  public DateTimeFilterItem(
    ObligatoryObjectAttributes attributeID,
    FlagsConditions possibleOperators,
    FlagsConditions defaultOperator)
    : base(attributeID, possibleOperators, defaultOperator)
  {
    this._value = DateTime.Now;
  }

  public DateTime Value
  {
    get => this._value;
    set
    {
      if (!(this._value != value))
        return;
      this._value = value;
      this.DiscardCachedValues();
    }
  }

  protected override string GetAsString() => this._value.ToString();

  protected override void SetAsString(string value)
  {
    this._value = DateTime.Parse(value);
    if (this.AttributeID != ObligatoryObjectAttributes.F_END_DATE)
      return;
    DateTime dateTime = this._value.Date.AddHours(23.0);
    dateTime = dateTime.AddMinutes(59.0);
    dateTime = dateTime.AddSeconds(59.0);
    this._value = dateTime.AddMilliseconds(999.0);
  }

  protected override ConditionStructure[] GetQueryConditions()
  {
    return new ConditionStructure[1]
    {
      new ConditionStructure((int) this.AttributeID, FlagsConditionsHelper.ConvertToRelationalOperators(this.Operator), (object) this._value.Date, LogicalOperators.NONE, 0, false)
    };
  }

  public override void Assign(FilterItem source)
  {
    base.Assign(source);
    if (!(source is DateTimeFilterItem dateTimeFilterItem))
      return;
    this._value = dateTimeFilterItem._value;
  }

  public override object Clone()
  {
    DateTimeFilterItem dateTimeFilterItem = new DateTimeFilterItem(this.AttributeID, this.PossibleOperators, this.Operator);
    dateTimeFilterItem.Assign((FilterItem) this);
    return (object) dateTimeFilterItem;
  }
}
