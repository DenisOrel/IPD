// Decompiled with JetBrains decompiler
// Type: Intermech.Security.EventLog.SmallNumberListFilterItem
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Client.Core;
using Intermech.Kernel.Search;
using System.Collections;
using System.Text;

#nullable disable
namespace Intermech.Security.EventLog;

internal class SmallNumberListFilterItem : FilterItem
{
  private ArrayList _values;

  public SmallNumberListFilterItem(
    ObligatoryObjectAttributes attributeID,
    FlagsConditions possibleOperators,
    FlagsConditions defaultOperator)
    : base(attributeID, possibleOperators, defaultOperator)
  {
    this._values = new ArrayList();
  }

  public int Count => this._values.Count;

  public int this[int index]
  {
    get => (int) this._values[index];
    set
    {
      if ((int) this._values[index] == value)
        return;
      this._values[index] = (object) value;
      this.DiscardCachedValues();
    }
  }

  public void Add(int value)
  {
    if (this._values.Contains((object) value))
      return;
    this._values.Add((object) value);
    this.DiscardCachedValues();
  }

  public void Remove(int value)
  {
    if (!this._values.Contains((object) value))
      return;
    this._values.Remove((object) value);
    this.DiscardCachedValues();
  }

  protected override string GetAsString()
  {
    if (this._values.Count <= 0)
      return "";
    StringBuilder stringBuilder = new StringBuilder(12 * this._values.Count);
    stringBuilder.Append((int) this._values[0]);
    for (int index = 1; index < this._values.Count; ++index)
    {
      stringBuilder.Append(';');
      stringBuilder.Append((int) this._values[index]);
    }
    return stringBuilder.ToString();
  }

  protected override void SetAsString(string value)
  {
    this._values.Clear();
    if (value == null || value.Length <= 0)
      return;
    string str = value;
    char[] chArray = new char[1]{ ';' };
    foreach (string s in str.Split(chArray))
      this._values.Add((object) int.Parse(s));
  }

  protected override ConditionStructure[] GetQueryConditions()
  {
    if (this._values.Count <= 0)
      return (ConditionStructure[]) null;
    RelationalOperators relationalOperators = FlagsConditionsHelper.ConvertToRelationalOperators(this.Operator);
    LogicalOperators logicalOperators = this.Operator == FlagsConditions.EQUAL ? LogicalOperators.OR : LogicalOperators.AND;
    ConditionStructure[] queryConditions = new ConditionStructure[this._values.Count];
    int num = this._values.Count - 1;
    for (int index = 0; index <= num; ++index)
    {
      int groupID = 0;
      if (this.Operator == FlagsConditions.EQUAL)
      {
        if (index == 0)
          groupID = 1;
        if (index == num)
          groupID = index != 0 ? -1 : 0;
      }
      queryConditions[index] = new ConditionStructure((int) this.AttributeID, relationalOperators, this._values[index], index == num ? LogicalOperators.NONE : logicalOperators, groupID, false);
    }
    return queryConditions;
  }

  public override void Assign(FilterItem source)
  {
    base.Assign(source);
    if (!(source is SmallNumberListFilterItem numberListFilterItem))
      return;
    this._values.Clear();
    this._values.InsertRange(0, (ICollection) numberListFilterItem._values);
  }

  public override object Clone()
  {
    SmallNumberListFilterItem numberListFilterItem = new SmallNumberListFilterItem(this.AttributeID, this.PossibleOperators, this.Operator);
    numberListFilterItem.Assign((FilterItem) this);
    return (object) numberListFilterItem;
  }
}
