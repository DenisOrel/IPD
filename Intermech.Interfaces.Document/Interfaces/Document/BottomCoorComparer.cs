// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.BottomCoorComparer
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Вспомогательный класс для сортировки по правой границе</summary>
internal class BottomCoorComparer : IComparer<RectangleElement>
{
  /// <summary>Сравнить объекты X и Y</summary>
  /// <param name="x">Объект для сравнения</param>
  /// <param name="y">Объект для сравнения</param>
  /// <returns>Значение меньше нуля - X ближе Y к правому краю страницы.
  /// 0 - X равен Y.
  /// Больше 0 - X дальше Y от правого края страницы</returns>
  public int Compare(RectangleElement x, RectangleElement y)
  {
    if (x == null)
      throw new ArgumentNullException(nameof (x));
    if (y == null)
      throw new ArgumentNullException(nameof (y));
    float bottom1 = x.Bounds.Bottom;
    float bottom2 = y.Bounds.Bottom;
    if ((double) bottom1 == (double) bottom2)
      return 0;
    return (double) bottom1 > (double) bottom2 ? -1 : 1;
  }
}
