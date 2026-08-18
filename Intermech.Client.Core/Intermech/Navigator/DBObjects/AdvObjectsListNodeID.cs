
// Type: Intermech.Navigator.DBObjects.AdvObjectsListNodeID
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using System;
using System.Collections.Generic;


namespace Intermech.Navigator.DBObjects;

/// <summary>Описание дочернего узла со списком объектов</summary>
public class AdvObjectsListNodeID : NodeID
{
  /// <summary>Список идентификаторов объектов</summary>
  public Dictionary<long, List<long>> ObjectLists;

  /// <summary>
  /// Конструктор, позволяющий создать идентификатор, описывающий объект,
  /// информация о котором была прочитана из таблицы объектов.
  /// </summary>
  /// <param name="objTypeId">Идентификатор типа объекта</param>
  /// <param name="objId">Идентификатор версии объекта</param>
  /// <param name="id">Идентификатор объекта</param>
  /// <param name="checkedOutBy">Кем объект взят на изменение</param>
  /// <param name="objectLists">Список идентификаторов объектов</param>
  public AdvObjectsListNodeID(
    int objTypeId,
    long objId,
    long id,
    long checkedOutBy,
    Dictionary<long, List<long>> objectLists)
    : base(objTypeId, objId, id, checkedOutBy, 0L, -1, string.Empty, -1, 0L, 0L, ObjectFiltrationState.fsNotRequired, 0L, 0L, string.Empty, 0L, Guid.Empty, 0L)
  {
    this.ObjectLists = objectLists;
  }
}
