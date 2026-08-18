
// Type: Intermech.PropertyEditors.ListSorter
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections;
using System.Windows.Forms;


namespace Intermech.PropertyEditors;

internal class ListSorter : IComparer
{
  private int column = -1;
  private SortOrder sortOrder;

  public ListSorter(int column, SortOrder sortOrder)
  {
    this.column = column;
    this.sortOrder = sortOrder;
  }

  public int Compare(object x, object y)
  {
    if (this.sortOrder == SortOrder.None || !(x is ListViewItem) || !(y is ListViewItem) || !(((ListViewItem) x).Tag is PossibleValuesClass) || !(((ListViewItem) y).Tag is PossibleValuesClass))
      return 0;
    int num;
    if (this.column == 0 && (((PossibleValuesClass) ((ListViewItem) x).Tag).FieldType == FieldTypes.ftInteger || ((PossibleValuesClass) ((ListViewItem) x).Tag).FieldType == FieldTypes.ftDouble))
    {
      PossibleValuesClass tag1 = (PossibleValuesClass) ((ListViewItem) x).Tag;
      PossibleValuesClass tag2 = (PossibleValuesClass) ((ListViewItem) y).Tag;
      num = ((PossibleValuesClass) ((ListViewItem) x).Tag).FieldType != FieldTypes.ftInteger ? Convert.ToDouble(tag1.Value).CompareTo(Convert.ToDouble(tag2.Value)) : Convert.ToInt64(tag1.Value).CompareTo(Convert.ToInt64(tag2.Value));
    }
    else
      num = ((ListViewItem) x).SubItems[this.column].ToString().CompareTo(((ListViewItem) y).SubItems[this.column].ToString());
    if (this.sortOrder == SortOrder.Descending)
      num = -num;
    return num;
  }
}
