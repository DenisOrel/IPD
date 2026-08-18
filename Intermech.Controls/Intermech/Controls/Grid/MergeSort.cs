
// Type: Intermech.Controls.Grid.MergeSort
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;


namespace Intermech.Controls.Grid;

/// <summary>Summary description for GLMergeSort.</summary>
internal class MergeSort
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

  public void sort(ListItemCollection items, int low_0, int high_0)
  {
    int num1 = low_0;
    int high_0_1 = high_0;
    if (num1 >= high_0_1)
      return;
    int high_0_2 = (num1 + high_0_1) / 2;
    this.sort(items, num1, high_0_2);
    this.sort(items, high_0_2 + 1, high_0_1);
    int num2 = high_0_2;
    int nItemIndex1 = high_0_2 + 1;
    while (num1 <= num2 && nItemIndex1 <= high_0_1 && !this.StopRequested)
    {
      if (this.CompareItems(items[num1], items[nItemIndex1], MergeSort.CompareDirection.LessThan))
      {
        ++num1;
      }
      else
      {
        ListItem listItem = items[nItemIndex1];
        for (int nItemIndex2 = nItemIndex1 - 1; nItemIndex2 >= num1; --nItemIndex2)
          items[nItemIndex2 + 1] = items[nItemIndex2];
        items[num1] = listItem;
        ++num1;
        ++num2;
        ++nItemIndex1;
      }
    }
  }

  private bool CompareItems(ListItem item1, ListItem item2, MergeSort.CompareDirection direction)
  {
    bool flag = false;
    if (direction == MergeSort.CompareDirection.GreaterThan)
      flag = true;
    if (this.SortDirection == SortDirection.Ascending)
      flag = !flag;
    if (!this.NumericCompare)
      return flag ? item1.SubItems[this.SortColumn].Text.CompareTo(item2.SubItems[this.SortColumn].Text) < 0 : item1.SubItems[this.SortColumn].Text.CompareTo(item2.SubItems[this.SortColumn].Text) > 0;
    try
    {
      double num1 = double.Parse(item1.SubItems[this.SortColumn].Text);
      double num2 = double.Parse(item2.SubItems[this.SortColumn].Text);
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
