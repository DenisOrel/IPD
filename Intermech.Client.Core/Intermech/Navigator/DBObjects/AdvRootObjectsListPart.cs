
// Type: Intermech.Navigator.DBObjects.AdvRootObjectsListPart
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Queries;
using System;
using System.Collections;
using System.Collections.Generic;


namespace Intermech.Navigator.DBObjects;

/// <summary>
/// Реализует часть элемента навигации, работающую со списком объектов
/// произвольной природы, заданных в виде коллекции идентификаторов.
/// </summary>
/// <remarks>
/// Для чтения объектов используется коллекция объектов, что не позволяет
/// получать значения атрибутов связей.
/// </remarks>
public class AdvRootObjectsListPart : ObjectsListPart
{
  /// <summary>
  /// Для каждого объекта (ключи в словарике) - свой список объектов
  /// </summary>
  public Dictionary<long, List<long>> ObjectLists;

  /// <summary>Создать экземпляр класса</summary>
  /// <param name="objectIDs">Список идентификаторов объектов</param>
  /// <param name="objectLists">Список идентификаторов объектов.
  /// Для каждого объекта (ключи в словарике) - свой список объектов.</param>
  /// <param name="services">Контейнер сервисов</param>
  public AdvRootObjectsListPart(
    IList objectIDs,
    Dictionary<long, List<long>> objectLists,
    IServiceProvider services)
    : base(objectIDs, services)
  {
    this.ObjectLists = objectLists;
  }

  /// <summary>Создать описание корневого узла</summary>
  /// <param name="fieldValues">Значения полей узла</param>
  /// <param name="adapter">Преобразование значений полей</param>
  /// <returns>Описание корневого узла</returns>
  public override INodeID CreateNodeId(object[] fieldValues, RecordAdapter adapter)
  {
    return (INodeID) new AdvRootObjectsListNodeID(1, Convert.ToInt32(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncF_OBJECT_TYPE)]), this.ObjectLists);
  }

  /// <summary>Создать дочерний узел в дереве</summary>
  /// <param name="nodeID">Описание дочернего узла</param>
  /// <returns>Дочерний узел в дереве</returns>
  public override INode GetChild(INodeID nodeID)
  {
    AdvRootObjectsListNodeID objectsListNodeId = (AdvRootObjectsListNodeID) nodeID;
    long[] numArray = new long[objectsListNodeId.ObjectLists.Count];
    objectsListNodeId.ObjectLists.Keys.CopyTo(numArray, 0);
    return (INode) new AdvRootObjectsListNode((IList) numArray, objectsListNodeId.ObjectLists);
  }
}
