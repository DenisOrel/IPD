// Decompiled with JetBrains decompiler
// Type: Intermech.Security.EventLog.FilterItem
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Client.Core;
using Intermech.Kernel.Search;
using System;

#nullable disable
namespace Intermech.Security.EventLog;

internal abstract class FilterItem : ICloneable
{
  private Filter _filter;
  private ObligatoryObjectAttributes _attributeID;
  private FlagsConditions _possibleOperators;
  private FlagsConditions _operator;
  private bool _enabled;
  private string _asString;
  private ConditionStructure[] _queryConditions;

  public FilterItem(
    ObligatoryObjectAttributes attributeID,
    FlagsConditions possibleOperators,
    FlagsConditions defaultOperator)
  {
    this._filter = (Filter) null;
    this._attributeID = attributeID;
    this._possibleOperators = possibleOperators;
    this._operator = defaultOperator;
    this._enabled = false;
    this.DiscardCachedValues();
  }

  public bool Enabled
  {
    get => this._enabled;
    set
    {
      if (this._enabled == value)
        return;
      this._enabled = value;
      if (this._filter == null)
        return;
      this._filter.DiscardCachedValues();
    }
  }

  public ObligatoryObjectAttributes AttributeID => this._attributeID;

  public FlagsConditions Operator
  {
    get => this._operator;
    set
    {
      if (this._operator == value)
        return;
      this._operator = value;
      this._queryConditions = (ConditionStructure[]) null;
      if (this._filter == null)
        return;
      this._filter.DiscardCachedValues();
    }
  }

  public FlagsConditions PossibleOperators => this._possibleOperators;

  public string AsString
  {
    get
    {
      if (this._asString == null)
        this._asString = this.GetAsString();
      return this._asString;
    }
    set
    {
      if (this._asString == null)
        this._asString = this.GetAsString();
      if (!(this._asString != value))
        return;
      this.SetAsString(value);
      this._asString = value;
      this._queryConditions = (ConditionStructure[]) null;
      if (this._filter == null)
        return;
      this._filter.DiscardCachedValues();
    }
  }

  public ConditionStructure[] QueryConditions
  {
    get
    {
      if (this._queryConditions == null)
        this._queryConditions = this.GetQueryConditions();
      return this._queryConditions;
    }
  }

  protected void DiscardCachedValues()
  {
    this._asString = (string) null;
    this._queryConditions = (ConditionStructure[]) null;
    if (this._filter == null)
      return;
    this._filter.DiscardCachedValues();
  }

  internal Filter Filter
  {
    get => this._filter;
    set => this._filter = value;
  }

  protected abstract string GetAsString();

  protected abstract void SetAsString(string value);

  protected abstract ConditionStructure[] GetQueryConditions();

  public virtual void Assign(FilterItem source)
  {
    if (source == null)
      return;
    this._filter = source._filter;
    this._attributeID = source._attributeID;
    this._possibleOperators = source._possibleOperators;
    this._operator = source._operator;
    this._enabled = source._enabled;
    this._asString = source._asString;
    this._queryConditions = source._queryConditions;
  }

  public abstract object Clone();
}
