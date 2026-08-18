
// Type: Intermech.Navigator.Controls.NavigatorJobResultPacket
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;
using System.Collections;
using System.Diagnostics;


namespace Intermech.Navigator.Controls;

/// <summary>Пакет с результатом выполнения фоновой задачи</summary>
internal class NavigatorJobResultPacket
{
  /// <summary>Коллекция колонок "Навигатора"</summary>
  private NodeColumnCollection _columns;
  /// <summary>Закладка</summary>
  private object _bookmark;
  /// <summary>Коллекция описаний узлов</summary>
  private NodeIDCollection _nodeIDs;
  /// <summary>Значения элементов</summary>
  private ArrayList _itemValues;
  /// <summary>Значения элементов (исходные)</summary>
  private ArrayList _rawItemValues;

  /// <summary>Конструктор</summary>
  /// <param name="columns">Коллекция колонок "Навигатора"</param>
  /// <param name="nodeIDs">Коллекция описаний узлов</param>
  /// <param name="itemValues">Значения элементов</param>
  /// <param name="rawItemValues">Значения элементов (исходные значения)</param>
  /// <param name="bookmark">Закладка</param>
  public NavigatorJobResultPacket(
    NodeColumnCollection columns,
    NodeIDCollection nodeIDs,
    ArrayList itemValues,
    ArrayList rawItemValues,
    object bookmark)
  {
    this._columns = columns;
    this._nodeIDs = nodeIDs;
    this._itemValues = itemValues;
    this._rawItemValues = rawItemValues;
    this._bookmark = bookmark;
  }

  /// <summary>Коллекция колонок "Навигатора"</summary>
  public NodeColumnCollection Columns
  {
    [DebuggerStepThrough] get => this._columns;
  }

  /// <summary>Коллекция описаний узлов</summary>
  public NodeIDCollection NodeIDs
  {
    [DebuggerStepThrough] get => this._nodeIDs;
  }

  /// <summary>Значения элементов (для отображения на экране)</summary>
  public ArrayList ItemValues
  {
    [DebuggerStepThrough] get => this._itemValues;
  }

  /// <summary>Значения элементов (исходные)</summary>
  public ArrayList RawItemValues
  {
    [DebuggerStepThrough] get => this._rawItemValues;
  }

  /// <summary>Закладка</summary>
  public object Bookmark
  {
    [DebuggerStepThrough] get => this._bookmark;
  }

  /// <summary>Получить данные из указанного запроса</summary>
  /// <param name="query">Запрос к источнику данных</param>
  /// <param name="columns">Коллекция колонок "Навигатора"</param>
  /// <param name="bookmark">Закладка</param>
  /// <param name="count">Количество запрашиваемых записей</param>
  /// <returns>Результирующий пакет или null</returns>
  public static NavigatorJobResultPacket FromQuery(
    INodeQuery query,
    NodeColumnCollection columns,
    object bookmark,
    int count)
  {
    if (query != null)
    {
      query.Execute(bookmark, count);
      if (query.RecordCount > 0)
      {
        NodeIDCollection nodeIDs = new NodeIDCollection();
        ArrayList itemValues = new ArrayList();
        ArrayList rawItemValues = new ArrayList();
        for (int index = 0; index < query.RecordCount; ++index)
        {
          nodeIDs.Add(query.GetRecordNodeID(index));
          itemValues.Add((object) query.GetRecordValues(index));
          rawItemValues.Add((object) query.GetRawRecordValues(index));
        }
        return new NavigatorJobResultPacket(columns, nodeIDs, itemValues, rawItemValues, query.Bookmark);
      }
    }
    return (NavigatorJobResultPacket) null;
  }

  /// <summary>Получить данные из указанного запроса</summary>
  /// <param name="query">Запрос к источнику данных</param>
  /// <param name="columns">Коллекция колонок "Навигатора"</param>
  /// <param name="nodeIDs">Список описаний узлов</param>
  /// <returns>Результирующий пакет или null</returns>
  public static NavigatorJobResultPacket FromQuery(
    INodeQuery query,
    NodeColumnCollection columns,
    NodeIDCollection nodeIDs)
  {
    if (query != null)
    {
      query.Execute(nodeIDs);
      if (query.RecordCount > 0)
      {
        NodeIDCollection nodeIdCollection = new NodeIDCollection();
        ArrayList itemValues = new ArrayList();
        ArrayList rawItemValues = new ArrayList();
        for (int index = 0; index < query.RecordCount; ++index)
        {
          nodeIdCollection.Add(query.GetRecordNodeID(index));
          itemValues.Add((object) query.GetRecordValues(index));
          rawItemValues.Add((object) query.GetRawRecordValues(index));
        }
        return new NavigatorJobResultPacket(columns, nodeIDs, itemValues, rawItemValues, query.Bookmark);
      }
    }
    return (NavigatorJobResultPacket) null;
  }
}
