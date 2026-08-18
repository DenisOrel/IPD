// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.RowComparer
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
internal class RowComparer : IComparer<AVSRow>
{
  /// <summary>Настройки сортировки строк</summary>
  private SectionSortSchema RowSortSchema;

  /// <summary>Конструктор</summary>
  /// <param name="rowSortSchema">Настройки сортировки строк</param>
  public RowComparer(SectionSortSchema rowSortSchema) => this.RowSortSchema = rowSortSchema;

  /// <summary>Сравнить две записи конструкторского документа</summary>
  /// <param name="x">Первая запись</param>
  /// <param name="y">Вторая запись</param>
  /// <returns>Результат сравнения.
  /// -1 означает x меньше y
  /// 0 означает x равно y
  /// 1 означает x больше y
  /// </returns>
  public int Compare(AVSRow x, AVSRow y)
  {
    if (x == y || x == null)
      return 0;
    if (this.RowSortSchema != null)
    {
      for (int index = 0; index < this.RowSortSchema.AttributeSortSchemas.Length; ++index)
      {
        string strX = Convert.ToString(x.GetFieldValue(this.RowSortSchema.AttributeSortSchemas[index].GetAttrInfo(), 0, -1, true, false));
        string strY = Convert.ToString(y.GetFieldValue(this.RowSortSchema.AttributeSortSchemas[index].GetAttrInfo(), 0, -1, true, false));
        int num = this.RowSortSchema.AttributeSortSchemas[index].Compare(strX, strY);
        if (num != 0)
          return num;
      }
    }
    else
    {
      int num = Convert.ToString(x.GetFieldValue(x.Field_Name, 0, -1, true, false)).CompareTo(Convert.ToString(y.GetFieldValue(y.Field_Name, 0, -1, true, false)));
      if (num != 0)
        return num;
    }
    return (!x.HasRelation ? (x.ObjectId == -1L ? (long) x.GetHashCode() : x.ObjectId) : x.Relations[0].RelationId).CompareTo(!y.HasRelation ? (y.ObjectId == -1L ? (long) y.GetHashCode() : y.ObjectId) : y.Relations[0].RelationId);
  }
}
