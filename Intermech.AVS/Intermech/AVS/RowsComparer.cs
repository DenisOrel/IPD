// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.RowsComparer
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Infralution.Controls.VirtualTree;
using System.Collections.Generic;

#nullable disable
namespace Intermech.AVS;

/// <summary>
/// Класс, позволяющий сравнивать строки в дереве по их порядковому номеру в родительском списке
/// </summary>
internal class RowsComparer : IComparer<Row>
{
  /// <summary>
  /// Сравнить две строки в дереве по их порядковым номерам в родительском списке
  /// </summary>
  /// <param name="x">Первая строка</param>
  /// <param name="y">Вторая строка</param>
  /// <returns>-1, 0, 1</returns>
  public int Compare(Row x, Row y)
  {
    return x == null || y == null || x.ParentRow != y.ParentRow ? 0 : x.ChildIndex.CompareTo(y.ChildIndex);
  }
}
