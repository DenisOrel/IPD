// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.LeftCoorComparer
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Вспомогательный класс для сортировки по левой границе</summary>
internal class LeftCoorComparer : IComparer<RectangleElement>
{
  /// <summary>Сравнить объекты X и Y</summary>
  /// <param name="x">Объект для сравнения</param>
  /// <param name="y">Объект для сравнения</param>
  /// <returns>Значение меньше нуля - X меньше Y. 0 - X равен Y. Больше 0 - X больше Y</returns>
  public int Compare(RectangleElement x, RectangleElement y)
  {
    if (x == null)
      throw new ArgumentNullException(nameof (x));
    if (y == null)
      throw new ArgumentNullException(nameof (y));
    float x1 = x.Bounds.X;
    float x2 = y.Bounds.X;
    if ((double) x1 == (double) x2)
      return 0;
    return (double) x1 < (double) x2 ? -1 : 1;
  }
}
