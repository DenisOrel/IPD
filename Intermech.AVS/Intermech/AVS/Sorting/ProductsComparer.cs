// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Sorting.ProductsComparer
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Interfaces.AVS;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace Intermech.AVS.Sorting;

/// <summary>Вспомогательный класс для сортировки исполнений спецификаций</summary>
public class ProductsComparer : IComparer<ProductInfo>, IComparer
{
  /// <summary>Конструкторский документ</summary>
  public AVSDocument avsDocument;

  /// <summary>Конструктор</summary>
  /// <param name="avsDocument">Конструкторский документ</param>
  public ProductsComparer(AVSDocument avsDocument) => this.avsDocument = avsDocument;

  /// <summary>Сравнить исполнения</summary>
  /// <param name="x">Информация об исполнении x</param>
  /// <param name="y">Информация об исполнении y</param>
  /// <returns>Если результат меньше ноля - x меньше чем y;
  /// Ноль x равен y;
  /// Больше ноля - x больше чем y.
  /// </returns>
  public int Compare(ProductInfo x, ProductInfo y)
  {
    if (this.avsDocument != null && (this.avsDocument.IsFormB || this.avsDocument.AvsDocumentForm == AVSDocumentForm.V) && this.avsDocument.DocumentDesignation != null)
    {
      string number1 = x.GetNumber(this.avsDocument.DocumentDesignation, this.avsDocument.UseSameDesignationForProducts);
      string number2 = y.GetNumber(this.avsDocument.DocumentDesignation, this.avsDocument.UseSameDesignationForProducts);
      if (number1 != number2)
        return AttributeSortSchema.StringCompare(number1, number2, true);
    }
    return AttributeSortSchema.StringCompare(x.Designation, y.Designation, true);
  }

  public int Compare(object x, object y) => this.Compare((ProductInfo) x, (ProductInfo) y);
}
