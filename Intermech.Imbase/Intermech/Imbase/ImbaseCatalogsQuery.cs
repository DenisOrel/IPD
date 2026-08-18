// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.ImbaseCatalogsQuery
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Imbase.Cache;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Queries;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Imbase;

internal class ImbaseCatalogsQuery : BaseNodeQuery
{
  internal static List<string> _catalogTypes = new List<string>();
  private INodeQuerySupport support;
  private List<string> rows = new List<string>();
  public const string F_CAPTION = "F_CAPTION";
  public static readonly object[] FieldsOrder = new object[1]
  {
    (object) nameof (F_CAPTION)
  };

  public ImbaseCatalogsQuery(INodeQuerySupport support)
  {
    this.support = support;
    this.rows = new List<string>();
    this.BuildCatalogsList();
  }

  private void BuildCatalogsList()
  {
    if (ImbaseCatalogsQuery._catalogTypes.Count != 0)
      return;
    ImbaseCatalogsQuery._catalogTypes.AddRange((IEnumerable<string>) CatalogTypes.Names);
  }

  protected override NodeQueryResult Execute(object bookmark, int count, RecordMapping mapping)
  {
    if (mapping != null && mapping.SortFields != null && mapping.SortFields.Length != 0)
    {
      bool flag = false;
      NodeColumnSortOrder nodeColumnSortOrder = NodeColumnSortOrder.None;
      for (int index = 0; index < mapping.SortFields.Length; ++index)
      {
        flag = mapping.SortFields[index].Equals((object) "F_CAPTION");
        if (flag)
        {
          nodeColumnSortOrder = mapping.SortOrders == null || mapping.SortOrders.Length == 0 ? NodeColumnSortOrder.Ascending : mapping.SortOrders[index];
          break;
        }
      }
      if (flag)
      {
        if (nodeColumnSortOrder == NodeColumnSortOrder.Descending)
          ImbaseCatalogsQuery._catalogTypes.Sort((IComparer<string>) new ImbaseCatalogsQuery.DescStringComparer());
        else
          ImbaseCatalogsQuery._catalogTypes.Sort();
      }
    }
    int position1 = bookmark != null ? ((PositionBookmark) bookmark).Position : 0;
    if (position1 + count > ImbaseCatalogsQuery._catalogTypes.Count)
      count = ImbaseCatalogsQuery._catalogTypes.Count - position1;
    if (count <= 0)
      return NodeQueryResult.Empty;
    this.rows.Clear();
    string empty = string.Empty;
    for (int index = 0; index < count; ++index)
      this.rows.Add(ImbaseCatalogsQuery._catalogTypes[position1 + index]);
    int position2 = position1 + count;
    return new NodeQueryResult(position2 < ImbaseCatalogsQuery._catalogTypes.Count ? (object) new PositionBookmark(position2) : (object) (PositionBookmark) null, count, this.TotalRecordCount, ImbaseCatalogsQuery.FieldsOrder);
  }

  protected override NodeQueryResult Execute(object[] recordIds, RecordMapping mapping)
  {
    this.rows.Clear();
    for (int index1 = 0; index1 < recordIds.Length; ++index1)
    {
      int index2 = ImbaseCatalogsQuery._catalogTypes.IndexOf((string) recordIds[index1]);
      if (index2 >= 0)
        this.rows.Add(ImbaseCatalogsQuery._catalogTypes[index2]);
    }
    return new NodeQueryResult(this.rows.Count, this.TotalRecordCount, ImbaseCatalogsQuery.FieldsOrder);
  }

  protected override object[] GetFieldValues(int index)
  {
    return new object[1]{ (object) this.rows[index] };
  }

  protected override INodeQuerySupport Support => this.support;

  private class DescStringComparer : IComparer<string>
  {
    public int Compare(string x, string y) => -x.CompareTo(y);
  }
}
