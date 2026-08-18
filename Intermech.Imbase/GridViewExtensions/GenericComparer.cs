// Decompiled with JetBrains decompiler
// Type: GridViewExtensions.GenericComparer
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System;
using System.Collections;
using System.ComponentModel;

#nullable disable
namespace GridViewExtensions;

public class GenericComparer : IComparer
{
  private ListSortDescriptionCollection _sortDescriptions;

  public GenericComparer(ListSortDescriptionCollection sortDescriptions)
  {
    this._sortDescriptions = sortDescriptions;
  }

  public int Compare(object x, object y)
  {
    for (int index = 0; index < this._sortDescriptions.Count; ++index)
    {
      PropertyDescriptor propertyDescriptor = this._sortDescriptions[index].PropertyDescriptor;
      object obj1 = propertyDescriptor.GetValue(x);
      object obj2 = propertyDescriptor.GetValue(y);
      int num1 = obj1 == DBNull.Value ? 1 : (obj1 == null ? 1 : 0);
      bool flag = obj2 == DBNull.Value || obj2 == null;
      int num2 = num1 == 0 ? (!flag ? (obj1 as IComparable).CompareTo((object) (obj2 as IComparable)) : 1) : (!flag ? -1 : 0);
      if (num2 != 0)
        return this._sortDescriptions[index].SortDirection != ListSortDirection.Ascending ? -num2 : num2;
    }
    return 0;
  }
}
