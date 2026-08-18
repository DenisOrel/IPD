// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.DocNodeComparer
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Document;

internal class DocNodeComparer : IComparer<DocumentTreeNode>
{
  /// <summary>Сравнить положение страниц владеющих элементами. Вернёт результат сравнения или null, если нет страниц</summary>
  /// <param name="x">Элемент документа x</param>
  /// <param name="y">Элемент документа y</param>
  /// <returns></returns>
  private int? TryCompareByPages(DocumentTreeNode x, DocumentTreeNode y)
  {
    int? nullable = new int?();
    if (x is PageElementNode pageElementNode1 && y is PageElementNode pageElementNode2)
    {
      PageData page1 = pageElementNode1.Page;
      PageData page2 = pageElementNode2.Page;
      if (page1 != null && page2 != null)
        nullable = new int?(this.ComparePages(page1, page2));
    }
    return nullable;
  }

  /// <summary>Сравнить положение страниц</summary>
  /// <param name="x">Страница документа x</param>
  /// <param name="y">Страница документа y</param>
  private int ComparePages(PageData pageX, PageData pageY)
  {
    return pageX.OwnerDocument != pageY.OwnerDocument ? this.Compare((DocumentTreeNode) pageX, (DocumentTreeNode) pageY) : pageX.Index.CompareTo(pageY.Index);
  }

  public int Compare(DocumentTreeNode x, DocumentTreeNode y)
  {
    if (x == null)
      throw new ArgumentNullException(nameof (x));
    if (y == null)
      throw new ArgumentNullException(nameof (y));
    if (x == y)
      return 0;
    int? nullable = this.TryCompareByPages(x, y);
    if (nullable.HasValue && nullable.Value != 0)
      return nullable.Value;
    List<int> intList1 = new List<int>(5);
    List<int> intList2 = new List<int>(5);
    DocumentTreeNode documentTreeNode1;
    for (documentTreeNode1 = x; documentTreeNode1.Parent != null; documentTreeNode1 = documentTreeNode1.Parent)
      intList1.Add(documentTreeNode1.Index);
    DocumentTreeNode documentTreeNode2;
    for (documentTreeNode2 = y; documentTreeNode2.Parent != null; documentTreeNode2 = documentTreeNode2.Parent)
      intList2.Add(documentTreeNode2.Index);
    if (documentTreeNode1 != documentTreeNode2)
      return 0;
    int index1 = intList2.Count - 1;
    int index2;
    for (index2 = intList1.Count - 1; index2 >= 0 && index1 >= 0; --index1)
    {
      if (intList1[index2] != intList2[index1])
        return intList1[index2].CompareTo(intList2[index1]);
      documentTreeNode1 = documentTreeNode1.Nodes[intList1[index2]];
      documentTreeNode2 = documentTreeNode2.Nodes[intList2[index1]];
      --index2;
    }
    return index2.CompareTo(index1);
  }
}
