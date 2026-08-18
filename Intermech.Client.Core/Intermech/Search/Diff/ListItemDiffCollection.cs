
// Type: Intermech.Search.Diff.ListItemDiffCollection
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;


namespace Intermech.Search.Diff;

[TypeConverter(typeof (ListItemDiffCollectionConverter))]
public sealed class ListItemDiffCollection : DiffCollectionBase<ListItemDiff>
{
  private List<ListItemDiff> _diffs = new List<ListItemDiff>();

  public ListItemDiffCollection(IList first, IList second)
  {
    if (first == null)
      throw new ArgumentNullException(nameof (first));
    if (second == null)
      throw new ArgumentNullException(nameof (second));
    List<object> list1 = first.Cast<object>().ToList<object>();
    List<object> list2 = second.Cast<object>().ToList<object>();
    int num = list1.Count > list2.Count ? list1.Count : list2.Count;
    for (int index = 0; index < num; ++index)
    {
      DiffOperand firstOperand = index < list1.Count ? new DiffOperand(list1[index]) : (DiffOperand) null;
      DiffOperand secondOperand = index < list2.Count ? new DiffOperand(list2[index]) : (DiffOperand) null;
      if (firstOperand != null && secondOperand != null && firstOperand.Value is BlobInfo && secondOperand.Value is BlobInfo)
      {
        DiffOperand diffOperand1 = new DiffOperand((object) new PropertyDiffCollection(firstOperand.Value, secondOperand.Value));
        DiffOperand diffOperand2 = new DiffOperand((object) new PropertyDiffCollection(secondOperand.Value, firstOperand.Value));
        firstOperand = diffOperand1;
        secondOperand = diffOperand2;
      }
      this._diffs.Add(new ListItemDiff(index, firstOperand, secondOperand));
    }
  }

  public ListItemDiff this[int index] => this._diffs[index];

  public override IEnumerator<ListItemDiff> GetEnumerator()
  {
    return (IEnumerator<ListItemDiff>) this._diffs.GetEnumerator();
  }

  public override bool Equals(object obj)
  {
    if (obj is ListItemDiffCollection source)
    {
      if (this._diffs.Count != ((IEnumerable<IDiff>) source).Count<IDiff>())
        return false;
      foreach (ListItemDiff diff in this._diffs)
      {
        if (diff.FirstOperand == null && diff.SecondOperand != null || diff.FirstOperand != null && diff.SecondOperand == null || !object.Equals(diff.FirstOperand.Value, diff.SecondOperand.Value))
          return false;
      }
    }
    return true;
  }
}
