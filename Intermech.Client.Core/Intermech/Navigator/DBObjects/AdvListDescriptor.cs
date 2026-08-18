
// Type: Intermech.Navigator.DBObjects.AdvListDescriptor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;
using Intermech.Navigator.VirtualNodes;
using System;
using System.Collections;
using System.Collections.Generic;


namespace Intermech.Navigator.DBObjects;

/// <summary>
/// Дескриптор, позволяющий создавать иерархию узлов [Список объектов] =&gt; [Список для каждого объекта]
/// </summary>
public class AdvListDescriptor : HiveDescriptor
{
  /// <summary>
  /// Для каждого объекта (ключи в словарике) - свой список объектов
  /// </summary>
  protected Dictionary<long, List<long>> _objectLists;

  /// <summary>Создать экземпляр дескриптора</summary>
  /// <param name="typeID">Тип</param>
  /// <param name="caption">Заголовок</param>
  /// <param name="objectLists">Для каждого объекта (ключи в словарике) - свой список объектов</param>
  public AdvListDescriptor(int typeID, string caption, Dictionary<long, List<long>> objectLists)
    : base(Intermech.Navigator.Consts.CategoryAdvRootObjectsListNode, typeID, caption)
  {
    this._objectLists = objectLists;
  }

  /// <summary>
  /// Вернуть данные определённого формата по указанному описанию узла
  /// </summary>
  /// <param name="nodeID">Описание узла</param>
  /// <param name="dataFormat">Формат запрашиваемых данных</param>
  /// <returns>Данные определённого формата по указанному описанию узла</returns>
  public override object GetData(INodeID nodeID, Type dataFormat)
  {
    if (dataFormat == typeof (IDescriptor))
      return (object) new AdvListDescriptor(this._typeID, this._caption, this._objectLists);
    return dataFormat == typeof (ICanOpenInNewWindow) ? (object) new CanOpenInNewWindow() : base.GetData(nodeID, dataFormat);
  }

  /// <summary>Вернуть описание корневого узла</summary>
  /// <returns>Описание корневого узла</returns>
  public override INodeID GetRecordNodeID()
  {
    return (INodeID) new AdvRootObjectsListNodeID(this._categoryID, this._typeID, this._objectLists);
  }

  /// <summary>Вернуть дочерний узел согласно его описанию</summary>
  /// <param name="nodeID">Описание узла</param>
  /// <returns>Дочерний узел</returns>
  public override INode GetChild(INodeID nodeID)
  {
    AdvRootObjectsListNodeID objectsListNodeId = (AdvRootObjectsListNodeID) nodeID;
    long[] numArray = new long[objectsListNodeId.ObjectLists.Count];
    objectsListNodeId.ObjectLists.Keys.CopyTo(numArray, 0);
    return (INode) new AdvRootObjectsListNode((IList) numArray, objectsListNodeId.ObjectLists);
  }
}
