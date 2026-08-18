// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.AvsRowByDocComparer
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Interfaces.Document;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace Intermech.AVS;

/// <summary>Служебный класс для сортировки записей в порядке строк документа</summary>
internal class AvsRowByDocComparer : IComparer, IComparer<AVSRow>
{
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
    if (x == null)
      throw new ArgumentNullException(nameof (x));
    if (y == null)
      throw new ArgumentNullException(nameof (y));
    if (x == y)
      return 0;
    int num = 0;
    if (x.DocNode != null && y.DocNode != null)
    {
      DocumentTreeNode documentTreeNode1 = (DocumentTreeNode) x.DocNodes[0];
      DocumentTreeNode documentTreeNode2 = (DocumentTreeNode) y.DocNodes[0];
      for (int index1 = 0; index1 < x.DocNodes.Count; ++index1)
      {
        TableData firstTable1 = x.DocNodes[index1].TopLevelTable.FindFirstTable();
        for (int index2 = 0; index2 < y.DocNodes.Count; ++index2)
        {
          TableData firstTable2 = y.DocNodes[index2].TopLevelTable.FindFirstTable();
          if (firstTable1 == firstTable2)
          {
            documentTreeNode1 = (DocumentTreeNode) x.DocNodes[index1];
            documentTreeNode2 = (DocumentTreeNode) y.DocNodes[index2];
            break;
          }
        }
      }
      for (; documentTreeNode1.Parent != documentTreeNode2.Parent && documentTreeNode1.Parent != null && documentTreeNode2.Parent != null; documentTreeNode2 = documentTreeNode2.Parent)
        documentTreeNode1 = documentTreeNode1.Parent;
      if (documentTreeNode1.Parent != null && documentTreeNode2.Parent != null)
        return documentTreeNode1.Index.CompareTo(documentTreeNode2.Index);
      if (documentTreeNode1.Parent != null)
        return -1;
      if (documentTreeNode2.Parent != null)
        return 1;
    }
    else
    {
      if (x.DocNode != null)
        return -1;
      if (y.DocNode != null)
        return 1;
    }
    if (!x.IsFreeSortIndex && !y.IsFreeSortIndex)
    {
      num = x.SortIndex.CompareTo(y.SortIndex);
    }
    else
    {
      if (x.IsFreeSortIndex)
        return 1;
      if (y.IsFreeSortIndex)
        return -1;
    }
    if (num != 0)
      return num;
    return (!x.HasRelation ? (x.ObjectId == -1L ? (long) x.GetHashCode() : x.ObjectId) : x.Relations[0].RelationId).CompareTo(!y.HasRelation ? (y.ObjectId == -1L ? (long) y.GetHashCode() : y.ObjectId) : y.Relations[0].RelationId);
  }

  /// <summary>Отсортирована ли запись</summary>
  /// <param name="row">Запись</param>
  /// <returns>Отсортирована ли запись</returns>
  public static bool IsSortedSpecRow(object row)
  {
    AVSRow avsRow = (AVSRow) row;
    return avsRow != null && avsRow.DocNode != null;
  }

  /// <summary>Реализация для IComparer</summary>
  /// <param name="x">Первая запись</param>
  /// <param name="y">Вторая запись</param>
  /// <returns></returns>
  int IComparer.Compare(object x, object y) => this.Compare((AVSRow) x, (AVSRow) y);
}
