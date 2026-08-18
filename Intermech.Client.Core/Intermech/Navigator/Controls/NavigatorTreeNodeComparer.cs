
// Type: Intermech.Navigator.Controls.NavigatorTreeNodeComparer
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;


namespace Intermech.Navigator.Controls;

/// <summary>
/// Вспомогательный класс, позволяющий сравнивать узлы дерева Навигатора, содержащие состав
/// </summary>
public class NavigatorTreeNodeComparer : IComparer<NavigatorTreeNode>
{
  /// <summary>Информация о родительском объекте состава</summary>
  private IDBTypedObjectID _parObject;
  /// <summary>Текущее правило отображения и сортировки составов</summary>
  private CompositionsAutosortRule _rule;
  /// <summary>Номер сортируемой колонки</summary>
  private int _sortColumnIdx = -1;
  /// <summary>Направление сортировки в колонке</summary>
  private NodeColumnSortOrder _sortOrder;
  /// <summary>Обработчик сортируемых узлов</summary>
  private INode _handler;

  /// <summary>Создать экземпляр класса</summary>
  /// <param name="parObject">Информация о родительском объекте состава</param>
  /// <param name="rule">Текущее правило отображения и сортировки составов</param>
  /// <param name="sortColumnIdx">Номер сортируемой колонки</param>
  /// <param name="sortOrder">Направление сортировки в колонке</param>
  /// <param name="handler">Обработчик сортируемых узлов</param>
  public NavigatorTreeNodeComparer(
    IDBTypedObjectID parObject,
    CompositionsAutosortRule rule,
    INode handler,
    int sortColumnIdx,
    NodeColumnSortOrder sortOrder)
  {
    if (parObject == null)
      throw new ArgumentNullException(nameof (parObject), LocalizationHolder.rm.GetString("Client.Core_1397"));
    if (rule == null)
      throw new ArgumentNullException(nameof (rule), LocalizationHolder.rm.GetString("Client.Core_1398"));
    if (handler == null)
      throw new ArgumentNullException(nameof (handler), LocalizationHolder.rm.GetString("Client.Core_1399"));
    this._parObject = parObject;
    this._rule = rule;
    this._handler = handler;
    this._sortColumnIdx = sortColumnIdx;
    this._sortOrder = sortOrder;
  }

  /// <summary>
  /// Сравнить два узла дерева Навигатора, содержащие состав
  /// </summary>
  /// <param name="x">Первый узел</param>
  /// <param name="y">Второй узел</param>
  /// <returns>-1, 0, 1</returns>
  public int Compare(NavigatorTreeNode x, NavigatorTreeNode y)
  {
    if (x == null || y == null || x == y)
      return 0;
    IDBTypedObjectID data1 = x.NodeID != null ? this._handler.GetData(x.NodeID, typeof (IDBTypedObjectID)) as IDBTypedObjectID : (IDBTypedObjectID) null;
    IDBTypedObjectID data2 = y.NodeID != null ? this._handler.GetData(y.NodeID, typeof (IDBTypedObjectID)) as IDBTypedObjectID : (IDBTypedObjectID) null;
    IDBRelationID data3 = x.NodeID != null ? this._handler.GetData(x.NodeID, typeof (IDBRelationID)) as IDBRelationID : (IDBRelationID) null;
    IDBRelationID data4 = y.NodeID != null ? this._handler.GetData(y.NodeID, typeof (IDBRelationID)) as IDBRelationID : (IDBRelationID) null;
    int num1 = data1 == null || data2 == null || data3 == null || data4 == null || data3.Value == 0L || data4.Value == 0L ? 0 : this._rule.CompareTo(this._parObject.ObjectType, this._sortOrder == NodeColumnSortOrder.Ascending ? data3.RelationType : data4.RelationType, this._sortOrder == NodeColumnSortOrder.Ascending ? data4.RelationType : data3.RelationType, this._sortOrder == NodeColumnSortOrder.Ascending ? data1.ObjectType : data2.ObjectType, this._sortOrder == NodeColumnSortOrder.Ascending ? data2.ObjectType : data1.ObjectType, OptimizationSettings.FullCompositionsSorting);
    if (num1 != 0)
      return num1;
    object rawValue1 = this._sortColumnIdx >= 0 ? x.RawValues[this._sortColumnIdx] : (object) null;
    object rawValue2 = this._sortColumnIdx >= 0 ? y.RawValues[this._sortColumnIdx] : (object) null;
    int num2 = ObjectsCompareHelper.CompareTo(this._sortOrder == NodeColumnSortOrder.Ascending ? rawValue1 : rawValue2, this._sortOrder == NodeColumnSortOrder.Ascending ? rawValue2 : rawValue1);
    if (num2 != 0 || x.Parent == null || y.Parent == null || x.Parent != y.Parent)
      return num2;
    return this._sortOrder == NodeColumnSortOrder.Ascending ? x.Parent.Children.IndexOf(x).CompareTo(y.Parent.Children.IndexOf(y)) : y.Parent.Children.IndexOf(y).CompareTo(x.Parent.Children.IndexOf(x));
  }
}
