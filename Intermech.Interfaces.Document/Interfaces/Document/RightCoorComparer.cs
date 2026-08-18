// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.RightCoorComparer
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Вспомогательный класс для сортировки по правой границе</summary>
internal class RightCoorComparer : IComparer<RectangleElement>
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
    float right1 = x.Bounds.Right;
    float right2 = y.Bounds.Right;
    if ((double) right1 == (double) right2)
      return 0;
    return (double) right1 > (double) right2 ? -1 : 1;
  }
}
