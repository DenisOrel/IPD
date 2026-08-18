// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.ImportBlanks.LeftCoorForPrimiteveComparer
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Document.Model.ImportBlanks;

/// <summary>Вспомогательный класс для сортировки по левой границе</summary>
internal class LeftCoorForPrimiteveComparer : IComparer<PrimitiveBase>
{
  /// <summary>Сравнить объекты X и Y</summary>
  /// <param name="x">Объект для сравнения</param>
  /// <param name="y">Объект для сравнения</param>
  /// <returns>Значение меньше нуля - X меньше Y. 0 - X равен Y. Больше 0 - X больше Y</returns>
  public int Compare(PrimitiveBase x, PrimitiveBase y)
  {
    if (x == null)
      throw new ArgumentNullException(nameof (x));
    if (y == null)
      throw new ArgumentNullException(nameof (y));
    float x1 = x.OrgMm.X;
    float x2 = y.OrgMm.X;
    if ((double) x1 == (double) x2)
      return 0;
    return (double) x1 < (double) x2 ? -1 : 1;
  }
}
