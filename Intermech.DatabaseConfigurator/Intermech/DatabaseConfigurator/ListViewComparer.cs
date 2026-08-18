// Decompiled with JetBrains decompiler
// Type: Intermech.DatabaseConfigurator.ListViewComparer
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using System;
using System.Collections;
using System.Windows.Forms;

#nullable disable
namespace Intermech.DatabaseConfigurator;

internal class ListViewComparer : IComparer
{
  private int ColumnNumber;
  private SortOrder SortOrder;

  public ListViewComparer(int column_number, SortOrder sort_order)
  {
    this.ColumnNumber = column_number;
    this.SortOrder = sort_order;
  }

  public int Compare(object object_x, object object_y)
  {
    ListViewItem listViewItem1 = object_x as ListViewItem;
    ListViewItem listViewItem2 = object_y as ListViewItem;
    string text1 = listViewItem1.SubItems.Count > this.ColumnNumber ? listViewItem1.SubItems[this.ColumnNumber].Text : "";
    string text2 = listViewItem2.SubItems.Count > this.ColumnNumber ? listViewItem2.SubItems[this.ColumnNumber].Text : "";
    double result1;
    double result2;
    DateTime result3;
    DateTime result4;
    int num = !double.TryParse(text1, out result1) || !double.TryParse(text2, out result2) ? (!DateTime.TryParse(text1, out result3) || !DateTime.TryParse(text2, out result4) ? text1.CompareTo(text2) : result3.CompareTo(result4)) : result1.CompareTo(result2);
    return this.SortOrder == SortOrder.Ascending ? num : -num;
  }
}
