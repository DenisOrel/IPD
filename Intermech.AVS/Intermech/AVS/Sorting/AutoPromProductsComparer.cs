// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Sorting.AutoPromProductsComparer
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Interfaces.AVS;
using System.Collections.Generic;

#nullable disable
namespace Intermech.AVS.Sorting;

/// <summary>Вспомогательный класс для сортировки исполнений автомобилестроительных спецификаций</summary>
public class AutoPromProductsComparer : IComparer<ProductInfo>
{
  /// <summary>Конструкторский документ</summary>
  public AVSDocument avsDocument;

  /// <summary>Конструктор</summary>
  /// <param name="avsDocument">Конструкторский документ</param>
  public AutoPromProductsComparer(AVSDocument avsDocument) => this.avsDocument = avsDocument;

  /// <summary>Сравнить исполнения</summary>
  /// <param name="x">Информация об исполнении x</param>
  /// <param name="y">Информация об исполнении y</param>
  /// <returns>Если результат меньше ноля - x меньше чем y;
  /// Ноль x равен y;
  /// Больше ноля - x больше чем y.
  /// </returns>
  public int Compare(ProductInfo x, ProductInfo y)
  {
    string strX = x.Number;
    if (strX == "")
      strX = (string) null;
    string strY = y.Number;
    if (strY == "")
      strY = (string) null;
    return strX != null || strY != null ? AttributeSortSchema.StringCompare(strX, strY, true) : AttributeSortSchema.StringCompare(x.Designation, y.Designation, true);
  }
}
