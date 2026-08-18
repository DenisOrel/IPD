
// Type: Intermech.Navigator.DBObjects.AdvObjectsListPart
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
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
public class AdvObjectsListPart : ObjectsListPart
{
  /// <summary>Список идентификаторов объектов</summary>
  protected IList ObjectIDs;
  /// <summary>
  /// Для каждого объекта (ключи в словарике) - свой список объектов
  /// </summary>
  public Dictionary<long, List<long>> ObjectLists;

  /// <summary>Создать экземпляр класса</summary>
  /// <param name="objectIDs">Список идентификаторов объектов</param>
  /// <param name="objectLists">Для каждого объекта (ключи в словарике) - свой список объектов</param>
  /// <param name="services">Контейнер сервисов</param>
  public AdvObjectsListPart(
    IList objectIDs,
    Dictionary<long, List<long>> objectLists,
    IServiceProvider services)
    : base(objectIDs, services)
  {
    this.ObjectIDs = objectIDs;
    this.ObjectLists = objectLists;
  }

  /// <summary>Создать описание корневого узла</summary>
  /// <param name="fieldValues">Значения полей узла</param>
  /// <param name="adapter">Преобразование значений полей</param>
  /// <returns>Описание корневого узла</returns>
  public override INodeID CreateNodeId(object[] fieldValues, RecordAdapter adapter)
  {
    int int32 = Convert.ToInt32(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncF_OBJECT_TYPE)]);
    long int64_1 = Convert.ToInt64(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncF_OBJECT_ID)]);
    long int64_2 = Convert.ToInt64(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncF_ID)]);
    long int64_3 = Convert.ToInt64(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncF_CHKOUT_BY)]);
    long objId = int64_1;
    long id = int64_2;
    long checkedOutBy = int64_3;
    Dictionary<long, List<long>> objectLists = this.ObjectLists;
    return (INodeID) new AdvObjectsListNodeID(int32, objId, id, checkedOutBy, objectLists);
  }

  /// <summary>Создать дочерний узел в дереве</summary>
  /// <param name="nodeID">Описание дочернего узла</param>
  /// <returns>Дочерний узел в дереве</returns>
  public override INode GetChild(INodeID nodeID)
  {
    AdvObjectsListNodeID objectsListNodeId = (AdvObjectsListNodeID) nodeID;
    return (INode) new AdvObjectsListNode(objectsListNodeId.ObjectTypeID, objectsListNodeId.ObjectID, this.ObjectLists);
  }

  public override object GetData(INodeID nodeID, Type dataFormat)
  {
    AdvObjectsListNodeID objectsListNodeId = (AdvObjectsListNodeID) nodeID;
    if (dataFormat == typeof (INode))
      return (object) this.GetChild(nodeID);
    if (dataFormat == typeof (IDescriptor))
      return (object) new Descriptor((nodeID as NodeID).ObjectID, (nodeID as NodeID).State);
    if (dataFormat == typeof (IDBTypedObjectID))
      return (object) new DBTypedObjectID(nodeID.TypeID, (nodeID as NodeID).ObjectID, (nodeID as NodeID).ID, (nodeID as NodeID).Caption, (nodeID as NodeID).Owner, (nodeID as NodeID).Version, (nodeID as NodeID).BaseVersion, (nodeID as NodeID).SiteID, (nodeID as NodeID).ModificationID);
    if (dataFormat == typeof (IDBObjectID))
      return (object) new DBObjectID((nodeID as NodeID).ObjectID, (nodeID as NodeID).ID, (nodeID as NodeID).Caption, (nodeID as NodeID).Owner);
    if (dataFormat == typeof (IDBObjectTypeID))
      return (object) new DBObjectTypeID(nodeID.TypeID);
    if (dataFormat == typeof (IDBObjectFiltrationState))
      return (object) new DBObjectFiltrationState((nodeID as NodeID).State);
    return dataFormat == typeof (IDBCheckedOutByID) ? (object) new DBCheckedOutByID(objectsListNodeId.ObjectID, objectsListNodeId.CheckedOutBy, (nodeID as NodeID).Owner) : (object) null;
  }
}
