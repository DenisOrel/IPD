
// Type: Intermech.Navigator.DBObjects.ObjectsPart
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Navigator.DB;
using Intermech.Navigator.DBObjectTypes;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Queries;
using System;


namespace Intermech.Navigator.DBObjects;

/// <summary>
/// Реализует часть элемента навигации, работающую со списком объектов,
/// произвольной природы.
/// </summary>
/// <remarks>
/// Для чтения объектов используется коллекция объектов, что не позволяет
/// получать значения атрибутов связей.
/// </remarks>
public class ObjectsPart : ObjectsPartBase
{
  /// <summary>
  /// Идентификатор типа объектов, с которыми работает эта часть.
  /// </summary>
  protected int objTypeID;

  /// <summary>
  /// Конструктор по умолчанию. Созданная такию образом часть будет
  /// возвращать абсолютно все объекты из базы данных.
  /// </summary>
  /// <param name="services">Контейнер сервисов</param>
  public ObjectsPart(IServiceProvider services)
    : base(services)
  {
    this.objTypeID = -1;
  }

  /// <summary>
  /// Конструктор части, позволяющий указать условие, которому должны
  /// удовлетворять объекты.
  /// </summary>
  /// <param name="condition">Условие, которому должны удовлетворять объекты.</param>
  /// <param name="services">Контейнер сервисов</param>
  public ObjectsPart(ConditionStructure condition, IServiceProvider services)
    : base(condition, services)
  {
    this.objTypeID = -1;
  }

  /// <summary>
  /// Конструктор части, позволяющий указать несколько условий, которым
  /// должны удовлетворять объекты.
  /// </summary>
  /// <param name="conditions">Массив условий, которым должны удовлетворять объекты.</param>
  /// <param name="services">Контейнер сервисов</param>
  public ObjectsPart(ConditionStructure[] conditions, IServiceProvider services)
    : base(conditions, services)
  {
    this.objTypeID = -1;
  }

  /// <summary>
  /// Конструктор части, позволяющий указать провайдер динамически изменяющихся
  /// условий, которым должны удовлетворять объекты.
  /// </summary>
  /// <param name="conditionsProvider">Провайдер условий.</param>
  /// <param name="services">Контейнер сервисов</param>
  public ObjectsPart(IConditionsProvider conditionsProvider, IServiceProvider services)
    : base(conditionsProvider, services)
  {
    this.objTypeID = -1;
  }

  /// <summary>
  /// Конструктор части, позволяющий указать тип читамых объектов. Созданная
  /// таким образом часть будет возвращать все объекты из базы данных, тип
  /// которых совпадает с указанным или является производным от него.
  /// </summary>
  /// <param name="objTypeID">Идентификатор типа объектов.</param>
  /// <param name="services">Контейнер сервисов</param>
  public ObjectsPart(int objTypeID, IServiceProvider services)
    : base(services)
  {
    this.objTypeID = objTypeID;
  }

  /// <summary>
  /// Конструктор части, позволяющий указать тип читаемых объектов и условие,
  /// которому они должны удовлетворять. Созданная таким образом часть будет
  /// возвращать только те объекты, тип которых совпадает с указанным или
  /// является производным от него.
  /// </summary>
  /// <param name="objTypeID">Идентификатор типа объектов</param>
  /// <param name="condition">Условие, которому должны удовлетворять объекты.</param>
  /// <param name="services">Контейнер сервисов</param>
  public ObjectsPart(int objTypeID, ConditionStructure condition, IServiceProvider services)
    : base(condition, services)
  {
    this.objTypeID = objTypeID;
  }

  /// <summary>
  /// Конструктор части, позволяющий указать тип читаемых объектов и условия,
  /// которым они должны удовлетворять. Созданная таким образом часть будет
  /// возвращать только те объекты, тип которых совпадает с указанным или
  /// является производным от него.
  /// </summary>
  /// <param name="objTypeID">Идентификатор типа объектов</param>
  /// <param name="conditions">Массив условий, которым должны удовлетворять объекты</param>
  /// <param name="services">Контейнер сервисов</param>
  public ObjectsPart(int objTypeID, ConditionStructure[] conditions, IServiceProvider services)
    : base(conditions, services)
  {
    this.objTypeID = objTypeID;
  }

  /// <summary>
  /// Конструктор части, позволяющий указать тип читаемых объектов и провайдер
  /// динамически изменяющихся условий, которым должны удовлетворять объекты.
  /// Созданная таким образом часть будет возвращать только те объекты, тип
  /// которых совпадает с указанным или является производным от него.
  /// </summary>
  /// <param name="objTypeID">Идентификатор типа объектов.</param>
  /// <param name="conditionsProvider">Провайдер условий.</param>
  /// <param name="services">Контейнер сервисов</param>
  public ObjectsPart(
    int objTypeID,
    IConditionsProvider conditionsProvider,
    IServiceProvider services)
    : base(conditionsProvider, services)
  {
    this.objTypeID = objTypeID;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="objTypeID"></param>
  /// <param name="conditions"></param>
  /// <param name="conditionsProvider"></param>
  /// <param name="services">Контейнер сервисов</param>
  public ObjectsPart(
    int objTypeID,
    ConditionStructure[] conditions,
    IConditionsProvider conditionsProvider,
    IServiceProvider services)
    : base(conditions, conditionsProvider, services)
  {
    this.objTypeID = objTypeID;
  }

  /// <summary>
  /// Возвращает интерфейс объекта-запроса, с помощью которого эта часть
  /// читает список обрабатываемых ею объектов.
  /// </summary>
  /// <param name="conditions">Массив условий, которым должны удовлетворять объекты.</param>
  /// <returns>Ссылка на интерфейс объекта-запроса.</returns>
  protected override INodeQuery GetQuery(ConditionStructure[] conditions)
  {
    IServiceProvider services = this.Owner is IContextAware owner ? owner.Services : (IServiceProvider) null;
    IObjectTypeNodeOptionsHolder service = services != null ? services.GetService(typeof (IObjectTypeNodeOptionsHolder)) as IObjectTypeNodeOptionsHolder : (IObjectTypeNodeOptionsHolder) null;
    ObjectTypeNodeOptions objectTypeNodeOptions = ObjectTypeNodeOptions.None;
    if (service != null)
      objectTypeNodeOptions = service.Options;
    return (objectTypeNodeOptions & ObjectTypeNodeOptions.EmptyQuery) == ObjectTypeNodeOptions.EmptyQuery ? (INodeQuery) null : (INodeQuery) this.GetObjectsQuery(conditions, services);
  }

  protected virtual ObjectsQuery GetObjectsQuery(
    ConditionStructure[] conditions,
    IServiceProvider services)
  {
    return new ObjectsQuery((INodeQuerySupport) this, this.objTypeID, conditions, services);
  }

  public override NodeColumnCollection GetDefaultColumns()
  {
    NodeColumnCollection defaultColumns = (ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole).DefaultColumnPack[new NavigatorColumnsKey(4, this.objTypeID, (string) null)];
    if (defaultColumns != null)
      return defaultColumns;
    NodeColumnCollection columns = new NodeColumnCollection();
    Helper.AddObligatoryColumns(columns, true, false);
    return columns;
  }

  /// <summary>
  /// Возвращает коллекцию всех поддерживаемых данным элементом
  /// виртуальных колонок навигатора. Этот метод используется диалогом
  /// настройки отображения грида.
  /// </summary>
  /// <param name="ColumnSetName">Название набора колонок.
  /// Intermech.Navigator.Consts.NavigatorDefaultColumnSetName - набор колонок по умолчанию</param>
  /// <returns>Коллекция виртуальных колонок навигатора</returns>
  public override NodeColumnCollection GetSupportedColumns(string ColumnSetName)
  {
    Guid columnSchemeGuid = Intermech.Navigator.Consts.ObjectObligatoryColumnSchemeGuid;
    IColumnSchemes service = (IColumnSchemes) ServicesManager.GetService(typeof (IColumnSchemes));
    NodeColumnCollection columns = new NodeColumnCollection();
    Helper.AddObligatoryColumns(columns, true, true);
    Helper.AddObligatoryColumnsAdv(columns);
    if (this.objTypeID != -1)
      Helper.AddObjectTypeColumns(columns, this.objTypeID);
    else
      Helper.AddAllColumns(columns);
    this.GetSupportedColumns(ColumnSetName, columns);
    return columns;
  }
}
