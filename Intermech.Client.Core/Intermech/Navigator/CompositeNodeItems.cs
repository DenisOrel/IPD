
// Type: Intermech.Navigator.CompositeNodeItems
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace Intermech.Navigator;

/// <summary>
/// Реализует коллекцию элементов навигации на основе нескольких родительских элементов и
/// списка идентификаторов дочерних элементов навигации.
/// </summary>
public class CompositeNodeItems : ISelectedItems, ISimpleSelectedItems
{
  private Dictionary<INodeID, NodeIDPath> handlerPaths;
  private Dictionary<INodeID, INode> handlers;
  private NodeIDCollection nodeIDs;
  private IServiceProvider services;
  private bool isCollage;

  /// <summary>
  /// Создает коллекцию элементов навигации на основе родительских элементов и
  /// списка идентификаторов дочерних элементов навигации.
  /// </summary>
  /// <param name="handlerPaths">Полные пути родительских элементов</param>
  /// <param name="handlers">Родительские элементы</param>
  /// <param name="nodeIDs">Коллекция идентификаторов дочерних элементов</param>
  /// <param name="services">Дополнительные сервисы</param>
  /// <param name="isCollage">Является ли коллекция составной (несколько родительских элементов)</param>
  public CompositeNodeItems(
    Dictionary<INodeID, NodeIDPath> handlerPaths,
    Dictionary<INodeID, INode> handlers,
    NodeIDCollection nodeIDs,
    IServiceProvider services,
    bool isCollage)
  {
    this.handlerPaths = handlerPaths;
    this.handlers = handlers;
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
    INodeID nodeId = this.nodeIDs[index];
    return dataFormat == typeof (INode) ? (object) this.handlers[nodeId] : this.handlers[nodeId].GetData(nodeId, dataFormat);
  }

  public NodeIDPath GetParentPath(int index) => this.handlerPaths[this.nodeIDs[index]];

  public object GetParentData(int index, Type dataFormat)
  {
    NodeIDPath handlerPath = this.handlerPaths[this.nodeIDs[index]];
    return handlerPath.Length == 0 ? (object) null : Utils.GetDataFromPath(handlerPath, dataFormat, this.services);
  }
}
