
// Type: Intermech.Controls.Grid.ListQuickSort
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;


namespace Intermech.Controls.Grid;

/// <summary>Summary description for GLQuickSort.</summary>
internal class ListQuickSort
{
  /// <summary>
  /// compare only numeric values in items.  Warning, this can end up slowing down routine quite a bit
  /// </summary>
  private bool _numericCompare;
  /// <summary>Stop this sort before it finishes</summary>
  private bool _stopRequested;
  /// <summary>Column within the items structure to sort</summary>
  private int _sortColumn;
  /// <summary>Direction this sorting routine will move items</summary>
  private SortDirection _sortDirection = SortDirection.Descending;

  public bool NumericCompare
  {
    get => this._numericCompare;
    set => this._numericCompare = value;
  }

  public bool StopRequested
  {
    get => this._stopRequested;
    set => this._stopRequested = value;
  }

  public int SortColumn
  {
    get => this._sortColumn;
    set => this._sortColumn = value;
  }

  public SortDirection SortDirection
  {
    get => this._sortDirection;
    set => this._sortDirection = value;
  }

  public void QuickSort(ListItemCollection items, int vleft, int vright)
  {
    int num1 = 4;
    if (vright - vleft <= num1)
      return;
    int num2 = (vright + vleft) / 2;
    if (this.CompareItems(items[vleft], items[num2], ListQuickSort.CompareDirection.GreaterThan))
      this.swap(items, vleft, num2);
    if (this.CompareItems(items[vleft], items[vright], ListQuickSort.CompareDirection.GreaterThan))
      this.swap(items, vleft, vright);
    if (this.CompareItems(items[num2], items[vright], ListQuickSort.CompareDirection.GreaterThan))
      this.swap(items, num2, vright);
    int num3 = vright - 1;
    this.swap(items, num2, num3);
    int x = vleft;
    ListItem listItem = items[num3];
    do
    {
      do
        ;
      while (this.CompareItems(items[++x], listItem, ListQuickSort.CompareDirection.LessThan));
      do
        ;
      while (this.CompareItems(items[--num3], listItem, ListQuickSort.CompareDirection.GreaterThan));
      if (num3 >= x)
        this.swap(items, x, num3);
      else
        goto label_13;
    }
    while (!this._stopRequested);
    return;
label_13:
    this.swap(items, x, vright - 1);
    this.QuickSort(items, vleft, num3);
    this.QuickSort(items, x + 1, vright);
  }

  private void swap(ListItemCollection items, int x, int w)
  {
    ListItem listItem = items[x];
    items[x] = items[w];
    items[w] = listItem;
  }

  public void InsertionSort(ListItemCollection items, int nLow0, int nHigh0)
  {
    for (int nItemIndex1 = nLow0 + 1; nItemIndex1 <= nHigh0; ++nItemIndex1)
    {
      ListItem listItem = items[nItemIndex1];
      int nItemIndex2;
      for (nItemIndex2 = nItemIndex1; nItemIndex2 > nLow0 && this.CompareItems(items[nItemIndex2 - 1], listItem, ListQuickSort.CompareDirection.GreaterThan); --nItemIndex2)
        items[nItemIndex2] = items[nItemIndex2 - 1];
      items[nItemIndex2] = listItem;
    }
  }

  public void sort(ListItemCollection items)
  {
    this.QuickSort(items, 0, items.Count - 1);
    this.InsertionSort(items, 0, items.Count - 1);
  }

  private bool CompareItems(
    ListItem item1,
    ListItem item2,
    ListQuickSort.CompareDirection direction)
  {
    bool flag = false;
    if (direction == ListQuickSort.CompareDirection.GreaterThan)
      flag = true;
    if (this.SortDirection == SortDirection.Ascending)
      flag = !flag;
    if (!this.NumericCompare)
      return flag ? item1.SubItems[this.SortColumn].Text.CompareTo(item2.SubItems[this.SortColumn].Text) < 0 : item1.SubItems[this.SortColumn].Text.CompareTo(item2.SubItems[this.SortColumn].Text) > 0;
    try
    {
      double num1 = item1.SubItems[this.SortColumn].Value;
      double num2 = item2.SubItems[this.SortColumn].Value;
      return flag ? num1 < num2 : num1 > num2;
    }
    catch (Exception ex)
    {
      return false;
    }
  }

  private enum CompareDirection
  {
    GreaterThan,
    LessThan,
  }
}
