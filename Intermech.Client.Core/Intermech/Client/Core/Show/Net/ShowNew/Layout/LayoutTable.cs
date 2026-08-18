
// Type: Intermech.Client.Core.Show.Net.ShowNew.Layout.LayoutTable
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Show;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;


namespace Intermech.Client.Core.Show.Net.ShowNew.Layout;

/// <summary>список компоновок</summary>
[DebuggerDisplay("{Length}")]
internal class LayoutTable : ILayoutTable, IEnumerable
{
  /// <summary>длинна массива компоновок</summary>
  public int Length => this.Array.Length;

  /// <summary>компоновка с которой сохранён чертёж</summary>
  public ILayout InFile { get; }

  /// <summary>массив компоновок</summary>
  public ILayout[] Array { get; }

  /// <summary>получить по индексу в массиве компоновку</summary>
  /// <param name="index">индекс в массиве</param>
  /// <returns>компоновка</returns>
  public ILayout this[int index]
  {
    get
    {
      return index >= 0 && index < this.Length ? this.Array[index] : throw new IndexOutOfRangeException();
    }
  }

  /// <summary>получить по компоновке индекс в массиве</summary>
  /// <param name="vitem">компоновка</param>
  /// <returns>индекс в массиве</returns>
  public int this[ILayout vitem]
  {
    get
    {
      int index = System.Array.FindIndex<ILayout>(this.Array, (Predicate<ILayout>) (item => item.Index == vitem.Index));
      return index >= 0 ? index : throw new IndexOutOfRangeException();
    }
  }

  /// <summary> Вернуть ссылку на интерфейс IEnumerator </summary>
  /// <returns>Ссылка на интерфейс IEnumerator</returns>
  public IEnumerator GetEnumerator() => this.Array.GetEnumerator();

  /// <summary>создать таблицу компоновок</summary>
  /// <param name="strings">массив имён компановок</param>
  /// <param name="inFile">индекс сохранёной компановки</param>
  /// <param name="work"></param>
  internal LayoutTable(string[] strings, int inFile, IShowDwgWork work)
  {
    List<ILayout> layoutList = new List<ILayout>(strings.Length);
    for (int index = 1; index < strings.Length; ++index)
      layoutList.Add((ILayout) new LayoutObject(index, strings[index], work));
    layoutList.Sort((Comparison<ILayout>) ((a, b) => string.Compare(a.Name, b.Name, StringComparison.Ordinal)));
    layoutList.Insert(0, (ILayout) new LayoutObject(0, strings[0], work));
    this.Array = layoutList.ToArray();
    int index1 = System.Array.FindIndex<ILayout>(this.Array, (Predicate<ILayout>) (item => item.Index == inFile));
    this.InFile = this.Array[index1 == -1 ? 0 : index1];
  }
}
