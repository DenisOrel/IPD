// Decompiled with JetBrains decompiler
// Type: Intermech.Security.EventLog.Filter
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using ImSSP;
using Intermech.Client.Core;
using Intermech.Kernel.Search;
using Intermech.Localization;
using System;
using System.Collections;

#nullable disable
namespace Intermech.Security.EventLog;

internal class Filter : ICloneable
{
  public bool IsClone;
  private FilterCollection _collection;
  private Guid _guid;
  private string _name;
  private FilterItem[] _items;
  private ConditionStructure[] _queryConditions;
  private const FlagsConditions _equalityOperators = FlagsConditions.EQUAL | FlagsConditions.NOTEQUAL;
  private const FlagsConditions _equalityDefault = FlagsConditions.EQUAL;
  private const FlagsConditions _stringOperators = FlagsConditions.EQUAL | FlagsConditions.NOTEQUAL | FlagsConditions.SUBSTR;
  private const FlagsConditions _stringDefault = FlagsConditions.EQUAL;
  private const FlagsConditions _anyOperators = FlagsConditions.EQUAL | FlagsConditions.NOTEQUAL | FlagsConditions.LESS | FlagsConditions.LESSEQUAL | FlagsConditions.GREATER | FlagsConditions.GREATEREQUAL;
  private const FlagsConditions _anyDefault = FlagsConditions.EQUAL;
  private const FlagsConditions _beginDateOperators = FlagsConditions.LESSEQUAL | FlagsConditions.GREATEREQUAL;
  private const FlagsConditions _beginDateDefault = FlagsConditions.GREATEREQUAL;
  private const FlagsConditions _endDateOperators = FlagsConditions.LESSEQUAL;
  private const FlagsConditions _endDateDefault = FlagsConditions.LESSEQUAL;

  public Filter(Guid guid)
  {
    this._collection = (FilterCollection) null;
    this._guid = guid;
    this._name = "";
    this._items = this.GetFilterItems();
    this.DiscardCachedValues();
  }

  public Filter(Guid guid, string name)
  {
    this._collection = (FilterCollection) null;
    this._guid = guid;
    this._name = name;
    this._items = this.GetFilterItems();
    this.DiscardCachedValues();
  }

  public Guid Guid
  {
    get => this._guid;
    set
    {
      if (!(this._guid != value))
        return;
      if (value == Guid.Empty)
        throw new ApplicationException(LocalizationHolder.rm.GetString(sc_5850.ssp_imclient_5851()));
      if (this._collection != null)
        this._collection.ChangeGuid(this._guid, value);
      this._guid = value;
    }
  }

  public string Name
  {
    get => this._name;
    set
    {
      if (!(this._name != value))
        return;
      if (value == null || value.Length == 0)
        throw new ApplicationException(LocalizationHolder.rm.GetString(sc_5850.ssp_imclient_5852()));
      if (this._collection != null)
        this._collection.ChangeName(this._name, value);
      this._name = value;
    }
  }

  public FilterItem[] Items => this._items;

  public FilterItem FindItem(ObligatoryObjectAttributes attributeID)
  {
    for (int index = 0; index < this._items.Length; ++index)
    {
      if (this._items[index].AttributeID == attributeID)
        return this._items[index];
    }
    return (FilterItem) null;
  }

  public ConditionStructure[] QueryConditions
  {
    get
    {
      if (this._queryConditions == null)
      {
        ArrayList arrayList = new ArrayList();
        for (int index = 0; index < this._items.Length; ++index)
        {
          if (this._items[index].Enabled)
          {
            if (arrayList.Count > 0)
            {
              ConditionStructure conditionStructure = (ConditionStructure) arrayList[arrayList.Count - 1] with
              {
                LogicalOperator = LogicalOperators.AND
              };
              arrayList[arrayList.Count - 1] = (object) conditionStructure;
            }
            ConditionStructure[] queryConditions = this._items[index].QueryConditions;
            if (queryConditions != null)
              arrayList.AddRange((ICollection) queryConditions);
          }
        }
        this._queryConditions = arrayList.Count <= 0 ? (ConditionStructure[]) null : (ConditionStructure[]) arrayList.ToArray(typeof (ConditionStructure));
      }
      return this._queryConditions;
    }
  }

  private FilterItem[] GetFilterItems()
  {
    FilterItem[] filterItems = new FilterItem[13]
    {
      (FilterItem) new SmallNumberFilterItem(ObligatoryObjectAttributes.F_AUDIT_TYPE, FlagsConditions.EQUAL | FlagsConditions.NOTEQUAL, FlagsConditions.EQUAL, 0),
      (FilterItem) new DateTimeFilterItem(ObligatoryObjectAttributes.F_BEGIN_DATE, FlagsConditions.LESSEQUAL | FlagsConditions.GREATEREQUAL, FlagsConditions.GREATEREQUAL),
      (FilterItem) new DateTimeFilterItem(ObligatoryObjectAttributes.F_END_DATE, FlagsConditions.LESSEQUAL, FlagsConditions.LESSEQUAL),
      (FilterItem) new BigNumberFilterItem(ObligatoryObjectAttributes.F_EVENT_ID, FlagsConditions.EQUAL | FlagsConditions.NOTEQUAL | FlagsConditions.LESS | FlagsConditions.LESSEQUAL | FlagsConditions.GREATER | FlagsConditions.GREATEREQUAL, FlagsConditions.EQUAL, 0L),
      (FilterItem) new SmallNumberListFilterItem(ObligatoryObjectAttributes.F_EVENT_TYPE, FlagsConditions.EQUAL | FlagsConditions.NOTEQUAL, FlagsConditions.EQUAL),
      (FilterItem) new StringFilterItem(ObligatoryObjectAttributes.F_OBJECT_NAME, FlagsConditions.EQUAL | FlagsConditions.NOTEQUAL | FlagsConditions.SUBSTR, FlagsConditions.EQUAL),
      (FilterItem) new BigNumberFilterItem(ObligatoryObjectAttributes.F_USER_ID, FlagsConditions.EQUAL | FlagsConditions.NOTEQUAL, FlagsConditions.EQUAL, 0L),
      (FilterItem) new BigNumberFilterItem(ObligatoryObjectAttributes.F_OBJECT_ID, FlagsConditions.EQUAL | FlagsConditions.NOTEQUAL | FlagsConditions.LESS | FlagsConditions.LESSEQUAL | FlagsConditions.GREATER | FlagsConditions.GREATEREQUAL, FlagsConditions.EQUAL, 0L),
      (FilterItem) new BigNumberFilterItem(ObligatoryObjectAttributes.F_RELATION_ID, FlagsConditions.EQUAL | FlagsConditions.NOTEQUAL | FlagsConditions.LESS | FlagsConditions.LESSEQUAL | FlagsConditions.GREATER | FlagsConditions.GREATEREQUAL, FlagsConditions.EQUAL, 0L),
      (FilterItem) new StringFilterItem(ObligatoryObjectAttributes.F_NOTE, FlagsConditions.EQUAL | FlagsConditions.NOTEQUAL | FlagsConditions.SUBSTR, FlagsConditions.EQUAL),
      (FilterItem) new SmallNumberFilterItem(ObligatoryObjectAttributes.F_CATEGORY_TYPE, FlagsConditions.EQUAL | FlagsConditions.NOTEQUAL, FlagsConditions.EQUAL, 0),
      (FilterItem) new BigNumberFilterItem(ObligatoryObjectAttributes.F_CATEGORY_ID, FlagsConditions.EQUAL | FlagsConditions.NOTEQUAL | FlagsConditions.LESS | FlagsConditions.LESSEQUAL | FlagsConditions.GREATER | FlagsConditions.GREATEREQUAL, FlagsConditions.EQUAL, 0L),
      (FilterItem) new StringFilterItem(ObligatoryObjectAttributes.F_COMPUTER_NAME, FlagsConditions.EQUAL | FlagsConditions.NOTEQUAL | FlagsConditions.SUBSTR, FlagsConditions.EQUAL)
    };
    for (int index = 0; index < filterItems.Length; ++index)
      filterItems[index].Filter = this;
    return filterItems;
  }

  internal FilterCollection Collection
  {
    get => this._collection;
    set => this._collection = value;
  }

  internal void DiscardCachedValues()
  {
    this._queryConditions = (ConditionStructure[]) null;
    if (this._collection == null)
      return;
    this._collection.Modified = true;
  }

  public virtual void Assign(Filter source)
  {
    if (source == null)
      return;
    this._guid = source.Guid;
    this._name = source.Name;
    this._items = new FilterItem[source.Items.Length];
    for (int index = 0; index < source.Items.Length; ++index)
    {
      this._items[index] = source.Items[index].Clone() as FilterItem;
      this._items[index].Filter = this;
    }
    this._queryConditions = source.QueryConditions;
  }

  public object Clone()
  {
    Filter filter = new Filter(this._guid, this._name);
    filter.Assign(this);
    filter.IsClone = true;
    return (object) filter;
  }
}
