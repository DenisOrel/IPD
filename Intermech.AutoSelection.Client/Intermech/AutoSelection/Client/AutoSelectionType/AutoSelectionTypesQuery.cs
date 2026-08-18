// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.AutoSelectionType.AutoSelectionTypesQuery
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using Intermech.Kernel.Search;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Queries;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.AutoSelection.Client.AutoSelectionType;

internal class AutoSelectionTypesQuery : BaseNodeQuery
{
  private readonly INodeQuerySupport _support;
  protected readonly List<AutoSelectionTypeRec> _items;
  private readonly List<AutoSelectionTypeRec> _rows = new List<AutoSelectionTypeRec>();
  protected internal static readonly NodeColumnID _ncNodeType = new NodeColumnID((object) ObligatoryObjectAttributes.F_ID, AttributeSourceTypes.Object);
  protected internal static string _CAPTION = "F_CAPTION";
  private static readonly object[] FieldsOrder = new object[2]
  {
    (object) AutoSelectionTypesQuery._CAPTION,
    (object) AutoSelectionTypesQuery._ncNodeType
  };

  public AutoSelectionTypesQuery(INodeQuerySupport support)
  {
    this._support = support;
    this._items = new List<AutoSelectionTypeRec>();
    foreach (object obj1 in Enum.GetValues(typeof (AutoSelectionNodeType)))
    {
      object obj2;
      if ((obj2 = obj1) is AutoSelectionNodeType)
      {
        AutoSelectionNodeType type = (AutoSelectionNodeType) obj2;
        if (type != AutoSelectionNodeType.None)
          this._items.Add(new AutoSelectionTypeRec(type));
      }
    }
  }

  protected override NodeQueryResult Execute(object bookmark, int count, RecordMapping recMapping)
  {
    if (recMapping != null && recMapping.SortFields != null && recMapping.SortFields.Length != 0)
    {
      bool flag = false;
      NodeColumnSortOrder nodeColumnSortOrder = NodeColumnSortOrder.None;
      for (int index = 0; index < recMapping.SortFields.Length; ++index)
      {
        flag = recMapping.SortFields[index].Equals((object) AutoSelectionTypesQuery._CAPTION);
        if (flag)
        {
          nodeColumnSortOrder = recMapping.SortOrders == null || recMapping.SortOrders.Length == 0 ? NodeColumnSortOrder.Ascending : recMapping.SortOrders[index];
          break;
        }
      }
      if (flag && nodeColumnSortOrder == NodeColumnSortOrder.Descending)
        this._items.Sort((IComparer<AutoSelectionTypeRec>) new AutoSelectionTypesQuery.DescTypesComparer());
    }
    int position1 = bookmark != null ? ((PositionBookmark) bookmark).Position : 0;
    if (position1 + count > this._items.Count)
      count = this._items.Count - position1;
    if (count <= 0)
      return NodeQueryResult.Empty;
    this._rows.Clear();
    for (int index = 0; index < count; ++index)
      this._rows.Add(this._items[position1 + index]);
    int position2 = position1 + count;
    return new NodeQueryResult(position2 < this._items.Count ? (object) new PositionBookmark(position2) : (object) (PositionBookmark) null, count, this.TotalRecordCount, AutoSelectionTypesQuery.FieldsOrder);
  }

  protected override NodeQueryResult Execute(object[] recordIds, RecordMapping recMapping)
  {
    this._rows.Clear();
    foreach (object recordId in recordIds)
    {
      int index = this._items.IndexOf(recordId as AutoSelectionTypeRec);
      if (index >= 0)
        this._rows.Add(this._items[index]);
    }
    return new NodeQueryResult(this._rows.Count, this.TotalRecordCount, AutoSelectionTypesQuery.FieldsOrder);
  }

  protected override object[] GetFieldValues(int index)
  {
    return new object[2]
    {
      (object) this._rows[index].Name,
      (object) this._rows[index]
    };
  }

  protected override INodeQuerySupport Support => this._support;

  private class DescTypesComparer : IComparer<AutoSelectionTypeRec>
  {
    public int Compare(AutoSelectionTypeRec x, AutoSelectionTypeRec y)
    {
      if (x == y)
        return 0;
      if (y == null)
        return 1;
      return x == null ? -1 : -string.Compare(x.Name, y.Name, StringComparison.Ordinal);
    }
  }
}
