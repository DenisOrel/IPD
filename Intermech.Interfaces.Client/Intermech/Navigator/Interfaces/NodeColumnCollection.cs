// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.NodeColumnCollection
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml;

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>
/// Коллекция предназначена для хранения и управления списком колонок пространства навигации NodeColumn.
/// Класс унаследован от типизированного списка List[NodeColumn], реализует интерфейс ICloneable.
/// </summary>
[Serializable]
public class NodeColumnCollection : List<NodeColumn>, ICloneable
{
  /// <summary>Сервис по управлению схемами колонок</summary>
  private static IColumnSchemes columnSchemes;
  /// <summary>Кэш для быстрого поиска колонок по их ID и Guid схемы</summary>
  protected IDictionary<NodeColumnCollection.IDSchemeKey, NodeColumn> _idschemeColumns = (IDictionary<NodeColumnCollection.IDSchemeKey, NodeColumn>) new Dictionary<NodeColumnCollection.IDSchemeKey, NodeColumn>();
  /// <summary>Кэш для быстрого поиска колонок по их ключам</summary>
  protected IDictionary<string, NodeColumn> _keyColumns = (IDictionary<string, NodeColumn>) new Dictionary<string, NodeColumn>();
  /// <summary>Кэш для быстрого поиска колонок по их идентификаторам</summary>
  protected IDictionary<object, NodeColumn> _idColumns = (IDictionary<object, NodeColumn>) new Dictionary<object, NodeColumn>();
  /// <summary>Кэш для быстрого поиска колонок по их заголовкам</summary>
  protected IDictionary<string, NodeColumn> _captionColumns = (IDictionary<string, NodeColumn>) new Dictionary<string, NodeColumn>();

  public static bool Equals(
    NodeColumnCollection firstNodeColumnCollection,
    NodeColumnCollection secondNodeColumnCollection)
  {
    if (firstNodeColumnCollection == null)
      throw new ArgumentNullException(nameof (firstNodeColumnCollection));
    if (secondNodeColumnCollection == null)
      throw new ArgumentNullException(nameof (secondNodeColumnCollection));
    if (firstNodeColumnCollection == secondNodeColumnCollection)
      return true;
    if (firstNodeColumnCollection.Count != secondNodeColumnCollection.Count)
      return false;
    for (int index = 0; index < firstNodeColumnCollection.Count; ++index)
    {
      NodeColumn firstNodeColumn = firstNodeColumnCollection[index];
      NodeColumn secondNodeColumn = secondNodeColumnCollection[index];
      if (firstNodeColumn == null || secondNodeColumn == null || !NodeColumn.Equals(firstNodeColumn, secondNodeColumn))
        return false;
    }
    return true;
  }

  public static bool EqualsWithNoWidth(
    NodeColumnCollection firstNodeColumnCollection,
    NodeColumnCollection secondNodeColumnCollection)
  {
    if (firstNodeColumnCollection == null)
      throw new ArgumentNullException(nameof (firstNodeColumnCollection));
    if (secondNodeColumnCollection == null)
      throw new ArgumentNullException(nameof (secondNodeColumnCollection));
    if (firstNodeColumnCollection == secondNodeColumnCollection)
      return true;
    if (firstNodeColumnCollection.Count != secondNodeColumnCollection.Count)
      return false;
    for (int index = 0; index < firstNodeColumnCollection.Count; ++index)
    {
      NodeColumn firstNodeColumn = firstNodeColumnCollection[index];
      NodeColumn secondNodeColumn = secondNodeColumnCollection[index];
      if (firstNodeColumn == null || secondNodeColumn == null || !NodeColumn.EqualsWithNoWidth(firstNodeColumn, secondNodeColumn))
        return false;
    }
    return true;
  }

  /// <summary>Создать коллекцию</summary>
  public NodeColumnCollection()
  {
    if (NodeColumnCollection.columnSchemes != null)
      return;
    NodeColumnCollection.columnSchemes = ServicesManager.GetService(typeof (IColumnSchemes)) as IColumnSchemes;
  }

  /// <summary>Создать коллекцию на основе другой коллекции</summary>
  /// <param name="collection">Коллекция-прототип</param>
  public NodeColumnCollection(IEnumerable<NodeColumn> collection)
    : this()
  {
    this.Assign((object) collection);
  }

  /// <summary>Создать коллекцию, указать её ёмкость</summary>
  /// <param name="capacity">Ёмкость коллекции</param>
  public NodeColumnCollection(int capacity)
    : base(capacity)
  {
    if (NodeColumnCollection.columnSchemes != null)
      return;
    NodeColumnCollection.columnSchemes = ServicesManager.GetService(typeof (IColumnSchemes)) as IColumnSchemes;
  }

  /// <summary>
  /// Создать коллекцию, заполнить её информацией из указанного объекта-источника
  /// </summary>
  /// <param name="source">Объект-источник</param>
  public NodeColumnCollection(object source)
    : this()
  {
    this.Assign(source);
  }

  public bool HasInvalidColumns => this.GetInvalidColumns().Length != 0;

  public void RemoveInvalidColumns()
  {
    foreach (NodeColumn invalidColumn in this.GetInvalidColumns())
      this.Remove(invalidColumn);
  }

  private NodeColumn[] GetInvalidColumns()
  {
    List<NodeColumn> nodeColumnList = new List<NodeColumn>();
    foreach (NodeColumn nodeColumn in (List<NodeColumn>) this)
    {
      if (nodeColumn.ID is int && !ObligatoryObjectAttributesHelper.IsObligatoryAttribute((int) nodeColumn.ID) && (nodeColumn.Attribute == null || MetaDataHelper.GetAttributeType((int) nodeColumn.ID) == null))
        nodeColumnList.Add(nodeColumn);
    }
    return nodeColumnList.ToArray();
  }

  /// <summary>Перестроить внутренние кэши</summary>
  internal void RebuildDictionaries()
  {
    this._idschemeColumns.Clear();
    this._keyColumns.Clear();
    this._idColumns.Clear();
    this._captionColumns.Clear();
    for (int index = 0; index < this.Count; ++index)
    {
      NodeColumn nodeColumn = this[index];
      NodeColumnCollection.IDSchemeKey key = new NodeColumnCollection.IDSchemeKey(nodeColumn.ID, nodeColumn.SchemeGuid);
      if (!this._idschemeColumns.ContainsKey(key))
        this._idschemeColumns.Add(key, nodeColumn);
      this._keyColumns[nodeColumn.Key] = nodeColumn;
      if (!this._idColumns.ContainsKey(nodeColumn.ID))
        this._idColumns.Add(nodeColumn.ID, nodeColumn);
      if (!this._captionColumns.ContainsKey(nodeColumn.Caption))
        this._captionColumns.Add(nodeColumn.Caption, nodeColumn);
      else if (this._captionColumns[nodeColumn.Caption].Priority < nodeColumn.Priority)
        this._captionColumns[nodeColumn.Caption] = nodeColumn;
    }
  }

  /// <summary>Добавить коллекцию колонок в состав текущей коллекции</summary>
  /// <param name="collection">Добавляемая коллекция колонок</param>
  public new void AddRange(IEnumerable<NodeColumn> collection)
  {
    base.AddRange(collection);
    this.RebuildDictionaries();
  }

  /// <summary>Проверить наличие колонки в коллекции</summary>
  /// <param name="item">Искомая колонка</param>
  /// <returns>true, если найдена</returns>
  public new bool Contains(NodeColumn item)
  {
    return item != null && this.ColumnIDExists(item.ID, item.SchemeGuid);
  }

  /// <summary>Удалить указанную колонку из коллекции</summary>
  /// <param name="item">Удаляемая колонка</param>
  /// <returns>true, если колонка была удалена</returns>
  public new bool Remove(NodeColumn item)
  {
    if (item == null)
      return false;
    this._idschemeColumns.Remove(new NodeColumnCollection.IDSchemeKey(item.ID, item.SchemeGuid));
    this._keyColumns.Remove(item.Key);
    this._idColumns.Remove(item.ID);
    this._captionColumns.Remove(item.Caption);
    return base.Remove(item);
  }

  /// <summary>Удалить колонку с указанным индексом из коллекции</summary>
  /// <param name="index">Индекс удаляемой колонки</param>
  public new void RemoveAt(int index)
  {
    NodeColumn nodeColumn = this[index];
    this._idschemeColumns.Remove(new NodeColumnCollection.IDSchemeKey(nodeColumn.ID, nodeColumn.SchemeGuid));
    this._keyColumns.Remove(nodeColumn.Key);
    this._idColumns.Remove(nodeColumn.ID);
    this._captionColumns.Remove(nodeColumn.Caption);
    base.RemoveAt(index);
  }

  /// <summary>Добавить колонку в коллекцию</summary>
  /// <param name="item">Колонка</param>
  public new void Add(NodeColumn item)
  {
    NodeColumnCollection.IDSchemeKey key1 = new NodeColumnCollection.IDSchemeKey(item.ID, item.SchemeGuid);
    if (!this._captionColumns.ContainsKey(item.Caption))
    {
      this._captionColumns.Add(item.Caption, item);
      base.Add(item);
      this._keyColumns.Add(item.Key, item);
      if (!this._idschemeColumns.ContainsKey(key1))
        this._idschemeColumns.Add(key1, item);
      if (this._idColumns.ContainsKey(item.ID))
        return;
      this._idColumns.Add(item.ID, item);
    }
    else
    {
      if (this._captionColumns[item.Caption].Priority >= item.Priority)
        return;
      NodeColumn captionColumn = this._captionColumns[item.Caption];
      NodeColumnCollection.IDSchemeKey key2 = new NodeColumnCollection.IDSchemeKey(captionColumn.ID, captionColumn.SchemeGuid);
      if (this._idschemeColumns.ContainsKey(key2))
        this._idschemeColumns.Remove(key2);
      if (this._keyColumns.ContainsKey(captionColumn.Key))
        this._keyColumns.Remove(captionColumn.Key);
      if (this._idColumns.ContainsKey(captionColumn.ID))
        this._idColumns.Remove(item.ID);
      this._captionColumns[item.Caption] = item;
      this._keyColumns.Add(item.Key, item);
      if (!this._idschemeColumns.ContainsKey(key1))
        this._idschemeColumns.Add(key1, item);
      if (this._idColumns.ContainsKey(item.ID))
        return;
      this._idColumns.Add(item.ID, item);
    }
  }

  /// <summary>
  /// Отыскать колонки, которые содержат значение указанного атрибута
  /// (объект, связи, т.п.)
  /// </summary>
  /// <param name="attrID">Идентификатор типа атрибута</param>
  /// <returns>Колонки, которые содержат значение указанного атрибута, либо пустой массив</returns>
  public virtual NodeColumn[] FindByAttrID(int attrID)
  {
    List<NodeColumn> result = new List<NodeColumn>();
    this.ForEach((Action<NodeColumn>) (item =>
    {
      IColumnAttributeInfo columnAttributeInfo = (IColumnAttributeInfo) item;
      if (columnAttributeInfo == null || columnAttributeInfo.Attribute == null || columnAttributeInfo.Attribute.AttributeID == -10000 || columnAttributeInfo.Attribute.AttributeID != attrID || result.IndexOf(item) >= 0)
        return;
      result.Add(item);
    }));
    return result.ToArray();
  }

  /// <summary>
  /// Отыскать колонки, которые содержат значение указанного атрибута
  /// (объект, связи, т.п.)
  /// </summary>
  /// <param name="attrGuid">Глобальный идентификатор типа атрибута</param>
  /// <returns>Колонки, которые содержат значение указанного атрибута, либо пустой массив</returns>
  public virtual NodeColumn[] FindByAttrID(Guid attrGuid)
  {
    return this.FindByAttrID(MetaDataHelper.GetAttributeTypeID(attrGuid));
  }

  /// <summary>Отыскать колонку по её ключу</summary>
  /// <param name="key">Ключ колонки</param>
  /// <returns>Колонка или null</returns>
  public virtual NodeColumn Find(string key)
  {
    return !this._keyColumns.ContainsKey(key) ? (NodeColumn) null : this._keyColumns[key];
  }

  /// <summary>Отыскать колонку по её уникальному заголовку</summary>
  /// <param name="caption">Уникальный заголовок колонки</param>
  /// <returns>Колонка или null</returns>
  public virtual NodeColumn FindCaption(string caption)
  {
    return !this._captionColumns.ContainsKey(caption) ? (NodeColumn) null : this._captionColumns[caption];
  }

  /// <summary>Отыскать колонку по её идентификатору</summary>
  /// <param name="columnID">Идентификатор колонки</param>
  /// <returns>Колонка или null</returns>
  public virtual NodeColumn Find(object columnID)
  {
    NodeColumn nodeColumn;
    this._idColumns.TryGetValue(columnID, out nodeColumn);
    return nodeColumn;
  }

  /// <summary>
  /// Проверяет, есть ли в коллекции колонка с указанным идентификатором, либо любая из колонок с перечисленными идентификаторами.
  /// </summary>
  /// <param name="columnID">Идентификатор колонки</param>
  /// <returns>true, если колонка с указанным идентификатором найдена</returns>
  public bool ColumnIDExists(object columnID) => this._idColumns.ContainsKey(columnID);

  /// <summary>Отыскать колонку по её идентификатору</summary>
  /// <param name="columnID">Идентификатор колонки</param>
  /// <param name="schemeGuid">Идентификатор схемы</param>
  /// <returns>Колонка или null</returns>
  public virtual NodeColumn Find(object columnID, Guid schemeGuid)
  {
    NodeColumn nodeColumn;
    this._idschemeColumns.TryGetValue(new NodeColumnCollection.IDSchemeKey(columnID, schemeGuid), out nodeColumn);
    return nodeColumn;
  }

  /// <summary>
  /// Проверяет, есть ли в коллекции колонка с указанным идентификатором, либо любая из колонок с перечисленными идентификаторами.
  /// </summary>
  /// <param name="columnID">Идентификатор колонки</param>
  /// <param name="schemeGuid">Идентификатор схемы</param>
  /// <returns>true, если колонка с указанным идентификатором найдена</returns>
  public bool ColumnIDExists(object columnID, Guid schemeGuid)
  {
    return columnID != null && this._idschemeColumns.ContainsKey(new NodeColumnCollection.IDSchemeKey(columnID, schemeGuid));
  }

  /// <summary>
  /// Проверяет, есть ли в коллекции колонка с указанным идентификатором, либо любая из колонок с перечисленными идентификаторами.
  /// </summary>
  /// <param name="columnIDs">Идентификаторы колонок</param>
  /// <returns>true, если колонка с любым из указанных идентификаторов найдена</returns>
  public bool ColumnIDsExists(IList columnIDs)
  {
    if (columnIDs == null || columnIDs.Count == 0 || this.Count == 0)
      return false;
    for (int index = 0; index < columnIDs.Count; ++index)
    {
      if (this.ColumnIDExists(columnIDs[index]))
        return true;
    }
    return false;
  }

  /// <summary>
  /// Проверяет, есть ли в коллекции колонка с указанным идентификатором, либо любая из колонок с перечисленными идентификаторами.
  /// </summary>
  /// <param name="columnIDs">Идентификаторы колонок</param>
  /// <returns>true, если колонка с любым из указанных идентификаторов найдена</returns>
  public bool ColumnIDsExists(IList<int> columnIDs)
  {
    if (columnIDs == null || columnIDs.Count == 0 || this.Count == 0)
      return false;
    for (int index = 0; index < columnIDs.Count; ++index)
    {
      if (this.ColumnIDExists((object) columnIDs[index]))
        return true;
    }
    return false;
  }

  /// <summary>Выполнить сортировку</summary>
  /// <param name="ascending">true - по возрастанию</param>
  public virtual void Sort(bool ascending)
  {
    this.Sort((IComparer<NodeColumn>) new NodeColumnCollection.NodeColumnComparer(ascending));
  }

  /// <summary>
  /// Упорядочить колонки в коллекции согласно их свойству SortIndex - очерёдности сортировки
  /// </summary>
  public virtual void SortByIndex()
  {
    this.Sort((IComparer<NodeColumn>) new NodeColumnCollection.SortIndexColumnComparer());
  }

  /// <summary>
  /// Упорядочить колонки в коллекции согласно названиям схем, а затем по названию колонок
  /// </summary>
  public virtual void SortBySchemesAndNames()
  {
    this.Sort((IComparer<NodeColumn>) new NodeColumnCollection.SchemeAndNameColumnComparer());
  }

  /// <summary>
  /// Удалить из коллекции колонки, которые не сортируются, проверить у остальных свойство SortIndex
  /// </summary>
  public virtual void RemoveNonSortedColumns()
  {
    ArrayList arrayList = new ArrayList();
    for (int index = 0; index < this.Count; ++index)
    {
      NodeColumn nodeColumn = this[index];
      if (nodeColumn.SortOrder == NodeColumnSortOrder.None || nodeColumn.SortIndex < 0)
        arrayList.Add((object) nodeColumn);
      else if (nodeColumn.SortIndex < 0)
        nodeColumn.SortIndex = 0;
    }
    for (int index = 0; index < arrayList.Count; ++index)
      this.Remove(arrayList[index] as NodeColumn);
  }

  /// <summary>
  /// Метод выбирает из указанной коллекции все сортируемые колонки и возвращает новую коллекцию колонок,
  /// в которую входят сортируемые колонки, упорядоченные по возрастанию их свойства SortIndex
  /// </summary>
  /// <param name="columns">Список колонок навигатора</param>
  /// <returns>Упорядоченная коллекция сортируемых колонок</returns>
  public static NodeColumnCollection GetSortedColumns(NodeColumnCollection columns)
  {
    if (columns == null)
      return new NodeColumnCollection();
    NodeColumnCollection sortedColumns = columns.Clone() as NodeColumnCollection;
    sortedColumns.RemoveNonSortedColumns();
    sortedColumns.SortByIndex();
    return sortedColumns;
  }

  /// <summary>Есть ли в коллекции хотя бы одна сортируемая колонка</summary>
  /// <returns>true, если в коллекции есть хотя бы одна сортируемая колонка</returns>
  public virtual bool HasSortedColumns()
  {
    for (int index = 0; index < this.Count; ++index)
    {
      if (this[index].SortOrder != NodeColumnSortOrder.None)
        return true;
    }
    return false;
  }

  /// <summary>
  /// Если из коллекции колонок были удалены колонки, участвующие в сортировке, то, вызвав данный метод,
  /// можно откорректировать значения свойств SortIndex у оставшихся сортируемых колонок, причём эти значения
  /// будут коректно упорядочены по возрастанию.
  /// </summary>
  /// <param name="columns">Список колонок навигатора, у которых следует откорректировать значения свойства SortIndex</param>
  public static void CorrectSortIndex(NodeColumnCollection columns)
  {
    if (columns == null || columns.Count == 0)
      return;
    NodeColumnCollection sortedColumns = NodeColumnCollection.GetSortedColumns(columns);
    if (sortedColumns == null || sortedColumns.Count == 0)
      return;
    for (int index = 0; index < sortedColumns.Count; ++index)
    {
      string key = sortedColumns[index].Key;
      NodeColumn nodeColumn = columns.Find(key) ?? columns.FindCaption(sortedColumns[index].Caption);
      if (nodeColumn != null)
        nodeColumn.SortIndex = index;
    }
  }

  /// <summary>
  /// Удалить у всех колонок информацию о сортировке (все колонки становятся несортируемыми)
  /// </summary>
  public virtual void RemoveSortInfo()
  {
    for (int index = 0; index < this.Count; ++index)
    {
      NodeColumn nodeColumn = this[index];
      nodeColumn.SortOrder = NodeColumnSortOrder.None;
      nodeColumn.SortIndex = -1;
    }
  }

  /// <summary>Добавить колонку, с учётом её новой ширины</summary>
  /// <param name="column">Колонка</param>
  /// <param name="width">Новая ширина колонки</param>
  public virtual void Add(NodeColumn column, int width)
  {
    if (column != null && width > 5)
      column.Width = width;
    this.Add(column);
  }

  /// <summary>Добавить колонку, с учётом её новой ширины</summary>
  /// <param name="column">Колонка</param>
  /// <param name="content">Содержимое колонки</param>
  public virtual void Add(NodeColumn column, ColumnContents content)
  {
    if (column != null)
      column.Contents = content;
    this.Add(column);
  }

  /// <summary>Очистить поля класса</summary>
  public new void Clear()
  {
    this._idschemeColumns.Clear();
    this._keyColumns.Clear();
    this._idColumns.Clear();
    this._captionColumns.Clear();
    base.Clear();
  }

  /// <summary>Скопировать в текущий объект поля из другого объекта.</summary>
  /// <param name="source">Объект-источник</param>
  public void Assign(object source)
  {
    if (this == source)
      return;
    this.Clear();
    if (!(source is IEnumerable<NodeColumn> nodeColumns))
      return;
    IEnumerator<NodeColumn> enumerator = nodeColumns.GetEnumerator();
    enumerator.Reset();
    while (enumerator.MoveNext())
      this.Add(enumerator.Current.Clone() as NodeColumn);
  }

  /// <summary>Создать копию объекта, идентичную натуральной</summary>
  /// <returns>Копия объекта, идентичная натуральной</returns>
  public object Clone()
  {
    NodeColumnCollection columnCollection = new NodeColumnCollection();
    for (int index = 0; index < this.Count; ++index)
      columnCollection.Add(this[index].Clone() as NodeColumn);
    return (object) columnCollection;
  }

  /// <summary>
  /// Выполнить синхронизацию коллекции допустимых колонок (текущий экземпляр)
  /// с коллекцией выбранных колонок (параметр master). Колонки в коллекции master
  /// будут сверены с аналогичными колонками текущей коллекции и при необходимости
  /// заменены на более приоритетные колонки. Затем из текущей коллекции будут
  /// удалены все дубликаты колонок, кроме самых приоритетных
  /// </summary>
  public virtual void SyncWithMaster(NodeColumnCollection master)
  {
    if (master != null)
    {
      NodeColumnCollection columnCollection = new NodeColumnCollection(master.Count);
      for (int index = 0; index < master.Count; ++index)
      {
        NodeColumn caption = this.FindCaption(master[index].Caption);
        if (caption != null)
          columnCollection.Add(master[index].Priority >= caption.Priority ? master[index] : caption);
      }
      master = columnCollection;
      for (int index = 0; index < master.Count; ++index)
        this.Add(master[index]);
    }
    for (int index = this.Count - 1; index >= 0; --index)
    {
      NodeColumn nodeColumn = this[index];
      NodeColumn caption = this.FindCaption(nodeColumn.Caption);
      if (nodeColumn != caption)
      {
        this.RemoveAt(index);
        if (!this._captionColumns.ContainsKey(caption.Caption))
          this._captionColumns.Add(caption.Caption, caption);
      }
    }
  }

  /// <summary>Загрузка состояния из XML</summary>
  /// <param name="xmlNode">Нод с настройками столбцов</param>
  /// <returns>Результат операции</returns>
  public virtual bool LoadData(XmlNode xmlNode)
  {
    this.Clear();
    if (xmlNode == null)
      return false;
    NodeColumnCollection columns = this;
    IColumnSchemes service = (IColumnSchemes) ServicesManager.GetService(typeof (IColumnSchemes));
    for (int i = 0; i < xmlNode.ChildNodes.Count; ++i)
    {
      XmlNode childNode = xmlNode.ChildNodes[i];
      XmlNode xmlNode1 = childNode.SelectSingleNode("Guid");
      if (xmlNode1 != null)
      {
        Guid guid = XmlConvert.ToGuid(xmlNode1.InnerText);
        XmlNode xmlNode2 = childNode.SelectSingleNode("ID");
        if (xmlNode2 != null)
        {
          object columnId = service.PersistNameToColumnID(guid, xmlNode2.InnerText);
          NodeColumn column = service.CreateColumn(guid, columnId);
          if (column != null)
          {
            XmlNode xmlNode3 = childNode.SelectSingleNode("Sorting");
            if (xmlNode3 != null)
              column.SortOrder = (NodeColumnSortOrder) Enum.Parse(typeof (NodeColumnSortOrder), xmlNode3.InnerText);
            XmlNode xmlNode4 = childNode.SelectSingleNode("SortIndex");
            if (xmlNode4 != null)
              column.SortIndex = XmlConvert.ToInt32(xmlNode4.InnerText);
            if (xmlNode4 == null || column.SortOrder == NodeColumnSortOrder.None)
            {
              column.SortOrder = NodeColumnSortOrder.None;
              column.SortIndex = -1;
            }
            XmlNode xmlNode5 = childNode.SelectSingleNode("Width");
            if (xmlNode5 != null)
              column.Width = XmlConvert.ToInt32(xmlNode5.InnerText);
            columns.Add(column);
          }
        }
      }
    }
    NodeColumnCollection.CorrectSortIndex(columns);
    return true;
  }

  /// <summary>Сохранение состояния в XML</summary>
  /// <param name="xmlNode">Нод для настроек столбцов</param>
  /// <returns>Результат опереции</returns>
  public virtual bool SaveData(XmlNode xmlNode)
  {
    if (xmlNode == null)
      return false;
    NodeColumnCollection columns = this;
    XmlDocument ownerDocument = xmlNode.OwnerDocument;
    xmlNode.RemoveAll();
    NodeColumnCollection.CorrectSortIndex(columns);
    IColumnSchemes service = (IColumnSchemes) ServicesManager.GetService(typeof (IColumnSchemes));
    for (int index = 0; index < columns.Count; ++index)
    {
      XmlNode element1 = (XmlNode) ownerDocument.CreateElement("Column");
      XmlNode element2 = (XmlNode) ownerDocument.CreateElement("Guid");
      element2.AppendChild((XmlNode) ownerDocument.CreateTextNode(XmlConvert.ToString(columns[index].SchemeGuid)));
      element1.AppendChild(element2);
      XmlNode element3 = (XmlNode) ownerDocument.CreateElement("ID");
      element3.AppendChild((XmlNode) ownerDocument.CreateTextNode(service.ColumnIDToPersistName(columns[index].SchemeGuid, columns[index].ID)));
      element1.AppendChild(element3);
      XmlNode element4 = (XmlNode) ownerDocument.CreateElement("Sorting");
      element4.AppendChild((XmlNode) ownerDocument.CreateTextNode(Enum.GetName(typeof (NodeColumnSortOrder), (object) columns[index].SortOrder)));
      element1.AppendChild(element4);
      XmlNode element5 = (XmlNode) ownerDocument.CreateElement("SortIndex");
      element5.AppendChild((XmlNode) ownerDocument.CreateTextNode(XmlConvert.ToString(columns[index].SortIndex)));
      element1.AppendChild(element5);
      XmlNode element6 = (XmlNode) ownerDocument.CreateElement("Width");
      element6.AppendChild((XmlNode) ownerDocument.CreateTextNode(XmlConvert.ToString(columns[index].Width)));
      element1.AppendChild(element6);
      xmlNode.AppendChild(element1);
    }
    return true;
  }

  /// <summary>
  /// Класс-ключ, состоящий из идентификатора колонки и Guid схемы
  /// </summary>
  [Serializable]
  protected sealed class IDSchemeKey : IComparable, IComparable<NodeColumnCollection.IDSchemeKey>
  {
    /// <summary>Хэш-код Int32</summary>
    private int _hash32;
    /// <summary>Хэш-код Int64</summary>
    private long _hash64;

    /// <summary>Создать экземпляр класса</summary>
    /// <param name="id">Идентификатор колонки</param>
    /// <param name="guid">Guid её схемы</param>
    public IDSchemeKey(object id, Guid guid)
    {
      this._hash32 = id.GetHashCode() << 16 /*0x10*/ ^ guid.GetHashCode();
      this._hash64 = (long) (id.GetHashCode() | guid.GetHashCode());
    }

    /// <summary>Сравнить с указанным объектом</summary>
    /// <param name="obj">Объект для сравнения</param>
    /// <returns>true, если объекты равны</returns>
    public override bool Equals(object obj)
    {
      return this._hash64 == ((NodeColumnCollection.IDSchemeKey) obj)._hash64;
    }

    /// <summary>Получить 32-битный хэш-код экземпляра класса</summary>
    /// <returns>32-битный хэш-код экземпляра класса</returns>
    [DebuggerStepThrough]
    public override int GetHashCode() => this._hash32;

    /// <summary>Сравнить с указанным объектом</summary>
    /// <param name="obj">Объект для сравнения</param>
    /// <returns>-1, 0, 1</returns>
    public int CompareTo(object obj)
    {
      return this._hash32.CompareTo(((NodeColumnCollection.IDSchemeKey) obj)._hash32);
    }

    /// <summary>Сравнить с указанным объектом</summary>
    /// <param name="other">Объект для сравнения</param>
    /// <returns>-1, 0, 1</returns>
    public int CompareTo(NodeColumnCollection.IDSchemeKey other)
    {
      return this._hash32.CompareTo(other._hash32);
    }
  }

  /// <summary>
  /// Класс для упорядочивания сортируемых колонок по очерёдности их сортировки
  /// </summary>
  private class SchemeAndNameColumnComparer : IComparer, IComparer<NodeColumn>
  {
    /// <summary>Сравнить между собой две колонки</summary>
    /// <param name="x">Первая колонка</param>
    /// <param name="y">Вторая колонка</param>
    /// <returns>-1 - колонка x меньше колонки y, 0 - колонки равны, 1 - колонка x больше колонки y</returns>
    public int Compare(object x, object y) => this.Compare(x as NodeColumn, y as NodeColumn);

    /// <summary>Сравнить между собой две колонки</summary>
    /// <param name="x">Первая колонка</param>
    /// <param name="y">Вторая колонка</param>
    /// <returns>-1 - колонка x меньше колонки y, 0 - колонки равны, 1 - колонка x больше колонки y</returns>
    public int Compare(NodeColumn x, NodeColumn y)
    {
      if (x == null || y == null)
        return 0;
      int num = NodeColumnCollection.columnSchemes[x.SchemeGuid].Name.CompareTo(NodeColumnCollection.columnSchemes[y.SchemeGuid].Name);
      return num != 0 ? num : x.Caption.CompareTo(y.Caption);
    }
  }

  /// <summary>
  /// Класс для упорядочивания сортируемых колонок по очерёдности их сортировки
  /// </summary>
  private class SortIndexColumnComparer : IComparer, IComparer<NodeColumn>
  {
    /// <summary>Сравнить между собой две колонки</summary>
    /// <param name="x">Первая колонка</param>
    /// <param name="y">Вторая колонка</param>
    /// <returns>-1 - колонка x меньше колонки y, 0 - колонки равны, 1 - колонка x больше колонки y</returns>
    public int Compare(object x, object y) => this.Compare(x as NodeColumn, y as NodeColumn);

    /// <summary>Сравнить между собой две колонки</summary>
    /// <param name="x">Первая колонка</param>
    /// <param name="y">Вторая колонка</param>
    /// <returns>-1 - колонка x меньше колонки y, 0 - колонки равны, 1 - колонка x больше колонки y</returns>
    public int Compare(NodeColumn x, NodeColumn y)
    {
      if (x == null || y == null)
        return 0;
      if (x.SortIndex < y.SortIndex && x.SortIndex >= 0)
        return -1;
      return x.SortIndex > y.SortIndex && x.SortIndex >= 0 ? 1 : 0;
    }
  }

  /// <summary>Внутренний класс по сортировке колонок</summary>
  internal class NodeColumnComparer : IComparer<NodeColumn>
  {
    /// <summary>
    /// Порядок сортировки (1 - по возрастанию, -1 - по убыванию)
    /// </summary>
    private int factor;

    /// <summary>Конструктор</summary>
    /// <param name="ascending">true - по возрастанию</param>
    public NodeColumnComparer(bool ascending) => this.factor = ascending ? 1 : -1;

    /// <summary>Сравнить две указанные колонки атрибутов</summary>
    /// <param name="x">Колонка x</param>
    /// <param name="y">Колонка y</param>
    /// <returns></returns>
    public int Compare(NodeColumn x, NodeColumn y)
    {
      return this.factor * string.Compare(x.Caption, y.Caption);
    }
  }
}
