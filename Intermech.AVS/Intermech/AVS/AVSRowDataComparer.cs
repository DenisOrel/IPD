// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.AVSRowDataComparer
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Interfaces.AVS;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.AVS;

/// <summary>Служебный класс для сортировки строк</summary>
internal class AVSRowDataComparer : IComparer<AvsRowData>
{
  /// <summary>Настройки сортировки строк</summary>
  private SectionSortSchema RowSortSchema;

  /// <summary>Конструктор</summary>
  /// <param name="rowSortSchema">Настройки сортировки строк</param>
  public AVSRowDataComparer(SectionSortSchema rowSortSchema) => this.RowSortSchema = rowSortSchema;

  /// <summary>Сравнить две записи конструкторского документа</summary>
  /// <param name="x">Первая запись</param>
  /// <param name="y">Вторая запись</param>
  /// <returns>Результат сравнения.
  /// -1 означает x меньше y
  /// 0 означает x равно y
  /// 1 означает x больше y
  /// </returns>
  public int Compare(AvsRowData x, AvsRowData y)
  {
    if (x == y || x == null)
      return 0;
    if (this.RowSortSchema != null)
    {
      for (int index = 0; index < this.RowSortSchema.AttributeSortSchemas.Length; ++index)
      {
        string strX = Convert.ToString(x.GetFieldValue(this.RowSortSchema.AttributeSortSchemas[index].GetAttrInfo(), false));
        string strY = Convert.ToString(y.GetFieldValue(this.RowSortSchema.AttributeSortSchemas[index].GetAttrInfo(), false));
        int num = this.RowSortSchema.AttributeSortSchemas[index].Compare(strX, strY);
        if (num != 0)
          return num;
      }
    }
    return x.ObjectID.CompareTo(y.ObjectID);
  }
}
