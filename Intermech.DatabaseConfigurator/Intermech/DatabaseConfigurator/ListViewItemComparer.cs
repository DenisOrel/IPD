// Decompiled with JetBrains decompiler
// Type: Intermech.DatabaseConfigurator.ListViewItemComparer
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using System.Collections;
using System.Windows.Forms;

#nullable disable
namespace Intermech.DatabaseConfigurator;

internal class ListViewItemComparer : IComparer
{
  private int col;
  private SortOrder order;

  public ListViewItemComparer()
  {
    this.col = 0;
    this.order = SortOrder.Ascending;
  }

  public ListViewItemComparer(int column, SortOrder order)
  {
    this.col = column;
    this.order = order;
  }

  public int Compare(object x, object y)
  {
    int num = string.Compare(((ListViewItem) x).SubItems[this.col].Text, ((ListViewItem) y).SubItems[this.col].Text);
    if (this.order == SortOrder.Descending)
      num *= -1;
    return num;
  }
}
