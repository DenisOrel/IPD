
// Type: Intermech.Navigator.DBObjects.ObjectsListPart
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Kernel.Search;
using Intermech.Navigator.DB;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Queries;
using System;
using System.Collections;


namespace Intermech.Navigator.DBObjects;

/// <summary>
/// Реализует часть элемента навигации, работающую со списком объектов
/// произвольной природы, заданных в виде коллекции идентификаторов.
/// </summary>
/// <remarks>
/// Для чтения объектов используется коллекция объектов, что не позволяет
/// получать значения атрибутов связей.
/// </remarks>
public class ObjectsListPart : ObjectsPartBase
{
  /// <summary>Признак раскрытия состава дочерних элементов</summary>
  protected bool _expandNode;
  /// <summary>Список идентификаторов версий объектов</summary>
  protected IList _objectIDs;
  /// <summary>Список условий запроса</summary>
  protected ConditionStructure[] _conditions;
  /// <summary>Тип объектов, среди которых производится поиск</summary>
  protected int objectTypeID = -1;

  /// <summary>Создать экземпляр класса</summary>
  /// <param name="objectIDs">Список идентификаторов версий объекта</param>
  /// <param name="services">Контейнер сервисов</param>
  public ObjectsListPart(IList objectIDs, IServiceProvider services)
    : base(services)
  {
    this._objectIDs = objectIDs;
  }

  /// <summary>
  /// Создать экземпляр класса, указав тип объектов для поиска
  /// </summary>
  /// <param name="objectIDs">Список идентификаторов версий объекта</param>
  /// <param name="services">Контейнер сервисов</param>
  /// <param name="objectTypeID">Тип объектов, версии которых указаны в списке</param>
  public ObjectsListPart(IList objectIDs, IServiceProvider services, int objectTypeID)
    : this(objectIDs, services, objectTypeID, true)
  {
  }

  /// <summary>
  /// Создать экземпляр класса, указав тип объектов для поиска
  /// </summary>
  /// <param name="objectIDs">Список идентификаторов версий объекта</param>
  /// <param name="services">Контейнер сервисов</param>
  /// <param name="objectTypeID">Тип объектов, версии которых указаны в списке</param>
  /// <param name="expandNode"></param>
  public ObjectsListPart(
    IList objectIDs,
    IServiceProvider services,
    int objectTypeID,
    bool expandNode)
    : this(objectIDs, (IConditionsProvider) null, services, objectTypeID, expandNode)
  {
  }

  /// <summary>
  /// Создать экземпляр класса, указав тип объектов для поиска
  /// </summary>
  /// <param name="objectIDs">Список идентификаторов версий объекта</param>
  /// <param name="services">Контейнер сервисов</param>
  /// <param name="objectTypeID">Тип объектов, версии которых указаны в списке</param>
  /// <param name="expandNode"></param>
  public ObjectsListPart(
    IList objectIDs,
    IConditionsProvider conditionsProvider,
    IServiceProvider services,
    int objectTypeID,
    bool expandNode)
    : base(conditionsProvider, services)
  {
    this._objectIDs = objectIDs;
    this._expandNode = expandNode;
    this.objectTypeID = objectTypeID;
  }

  /// <summary>Тип объектов, среди которых производится поиск</summary>
  protected virtual int ObjectTypeID => this.objectTypeID;

  /// <summary>
  /// Возвращает интерфейс объекта-запроса, с помощью которого эта часть
  /// читает список обрабатываемых ею объектов.
  /// </summary>
  /// <param name="conditions">Массив условий, которым должны удовлетворять объекты.</param>
  /// <returns>Ссылка на интерфейс объекта-запроса.</returns>
  protected override INodeQuery GetQuery(ConditionStructure[] conditions)
  {
    if (this._objectIDs != null && this._objectIDs.Count > 0)
    {
      object[] conditionValue = new object[this._objectIDs.Count];
      this._objectIDs.CopyTo((Array) conditionValue, 0);
      this._conditions = new ConditionStructure[1]
      {
        new ConditionStructure(-2, RelationalOperators.In, (object) conditionValue, LogicalOperators.NONE, 0, false)
      };
    }
    else
      this._conditions = (ConditionStructure[]) null;
    IServiceProvider services = this.Owner is IContextAware owner ? owner.Services : (IServiceProvider) null;
    return this._conditions == null ? (INodeQuery) null : this.GetObjectsQuery((INodeQuerySupport) this, this.ObjectTypeID, ConditionStructure.Join(conditions, this._conditions), services);
  }

  protected virtual INodeQuery GetObjectsQuery(
    INodeQuerySupport support,
    int objTypeID,
    ConditionStructure[] conditions,
    IServiceProvider services)
  {
    return (INodeQuery) new ObjectsQuery((INodeQuerySupport) this, objTypeID, conditions, services);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="nodeID"></param>
  /// <returns></returns>
  public override INode GetChild(INodeID nodeID)
  {
    return this._expandNode ? base.GetChild(nodeID) : (INode) this.GetNonExpandedNode(nodeID as NodeID);
  }

  protected virtual ObjectNode GetNonExpandedNode(NodeID objNodeID)
  {
    return objNodeID == null ? (ObjectNode) null : (ObjectNode) new NonExpandedNode(objNodeID.TypeID, objNodeID.ObjectID);
  }
}
