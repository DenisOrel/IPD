
// Type: Intermech.Navigator.NodeItems
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;
using System;
using System.Diagnostics;


namespace Intermech.Navigator;

/// <summary>
/// Реализует коллекцию элементов навигации на основе родительского элемента и
/// списка идентификаторов дочерних элементов навигации.
/// </summary>
public class NodeItems : IKeyedSelectedItems, ISelectedItems, ISimpleSelectedItems
{
  private NodeIDPath handlerPath;
  private INode handler;
  private NodeIDCollection nodeIDs;
  private IServiceProvider services;
  private bool isCollage;

  /// <summary>
  /// Создает коллекцию элементов навигации на основе родительского элемента и
  /// списка идентификаторов дочерних элементов навигации.
  /// </summary>
  /// <param name="handlerPath">Полный путь родительского элемента</param>
  /// <param name="handler">Родительский элемент</param>
  /// <param name="nodeIDs">Коллекция идентификаторов дочерних элементов</param>
  /// <param name="services">Дополнительные сервисы</param>
  public NodeItems(
    NodeIDPath handlerPath,
    INode handler,
    NodeIDCollection nodeIDs,
    IServiceProvider services)
    : this(handlerPath, handler, nodeIDs, services, false)
  {
  }

  public NodeItems(
    NodeIDPath handlerPath,
    INode handler,
    NodeIDCollection nodeIDs,
    IServiceProvider services,
    bool isCollage)
  {
    this.handlerPath = handlerPath;
    this.handler = handler;
    this.nodeIDs = nodeIDs;
    this.services = services;
    this.isCollage = isCollage;
  }

  /// <summary>
  /// Возвращает коллекцию идентификаторов дочерних элементов.
  /// </summary>
  public NodeIDCollection NodeIDs
  {
    [DebuggerStepThrough] get => this.nodeIDs;
  }

  /// <summary>Контейнер сервисов</summary>
  public IServiceProvider Services
  {
    [DebuggerStepThrough] get => this.services;
  }

  public int Count
  {
    [DebuggerStepThrough] get => this.nodeIDs.Count;
  }

  public bool IsCollage
  {
    [DebuggerStepThrough] get => this.isCollage;
  }

  public INodeID GetItemID(int index) => this.nodeIDs[index];

  public object GetItemData(int index, Type dataFormat)
  {
    return dataFormat == typeof (INode) ? (object) this.handler : this.handler.GetData(this.nodeIDs[index], dataFormat);
  }

  public NodeIDPath GetParentPath(int index) => this.handlerPath;

  public object GetParentData(int index, Type dataFormat)
  {
    return this.handlerPath.Length == 0 ? (object) null : Utils.GetDataFromPath(this.handlerPath, dataFormat, this.services);
  }

  /// <summary>
  /// Отыскать индекс элемента коллекции, которому назначен указанный ключ
  /// </summary>
  /// <param name="key">Ключ искомого элемента коллекции</param>
  /// <returns>Индекс или -1, если элемент с указанным ключом не найден в коллекции</returns>
  public int GetItemIndex(string key) => this.nodeIDs.IndexOfKey(key);

  /// <summary>Отыскать ключ элемента коллекции с указанным индексом</summary>
  /// <param name="index">Индекс элемента коллекции</param>
  /// <returns>Ключ элемента коллекции</returns>
  public string GetItemKey(int index) => this.nodeIDs.KeyOfIndex(index);
}
