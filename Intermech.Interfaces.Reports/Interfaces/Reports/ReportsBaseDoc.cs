// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Reports.ReportsBaseDoc
// Assembly: Intermech.Interfaces.Reports, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3A40A7D8-A018-4590-B8F9-C63911182943
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Reports.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Reports.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Reports;

/// <summary>
/// Базовый класс для передачи документов со стороны сервера
/// </summary>
[Serializable]
public class ReportsBaseDoc
{
  /// <summary>Конструктор</summary>
  /// <param name="owner">Родительский элемент</param>
  public ReportsBaseDoc(ReportsBaseDoc owner = null)
  {
    this.ObjectID = 0L;
    this.ObjectTypeID = -1;
    this.Owner = owner;
    this.Items = new List<ReportsBaseDoc>();
    this.Attributes = new Dictionary<Guid, object>();
  }

  /// <summary>Ид. версии объекта</summary>
  public long ObjectID { get; set; }

  /// <summary>Ид. типа объекта</summary>
  public int ObjectTypeID { get; set; }

  /// <summary>Значение сортировки</summary>
  public long Order { get; set; }

  /// <summary>Родительский элемент</summary>
  public ReportsBaseDoc Owner { get; private set; }

  /// <summary>Список дочерних элементов</summary>
  public List<ReportsBaseDoc> Items { get; private set; }

  /// <summary>Атрибуты документа</summary>
  public Dictionary<Guid, object> Attributes { get; private set; }

  /// <summary>Получить (рекурсивно список документов)</summary>
  /// <param name="docList">Список документов</param>
  /// <param name="docType">Искомый тип</param>
  public void CollectDocItem(List<ReportsBaseDoc> docList, Type docType)
  {
    if (docList == null)
      return;
    if (this.GetType() == docType && !docList.Contains(this))
      docList.Add(this);
    foreach (ReportsBaseDoc reportsBaseDoc in this.Items)
      reportsBaseDoc?.CollectDocItem(docList, docType);
  }

  /// <summary>
  /// 
  /// </summary>
  public class RepDocComparer : IComparer<ReportsBaseDoc>
  {
    /// <summary>
    /// 
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public int Compare(ReportsBaseDoc x, ReportsBaseDoc y)
    {
      if (x != null && y != null)
        return Math.Sign(x.Order - y.Order);
      if (x == null && y == null)
        return 0;
      return x == null ? 1 : -1;
    }
  }
}
