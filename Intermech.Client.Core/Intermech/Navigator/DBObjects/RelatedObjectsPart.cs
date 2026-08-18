
// Type: Intermech.Navigator.DBObjects.RelatedObjectsPart
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Navigator.DB;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Queries;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;


namespace Intermech.Navigator.DBObjects;

/// <summary>
/// Реализует часть элемента навигации, работающую со списком объектов,
/// входящих в состав или применяющихся в указанном объекте. Тип отношений
/// объектов указывается с помощью <see cref="T:Intermech.Navigator.DBObjects.RelatedObjectsRole" />.
/// </summary>
/// <remarks>
/// Для чтения объектов используется коллекция связей объектов, что позволяет
/// получать значения как атрибутов объектов, так и атрибутов связей.
/// </remarks>
public class RelatedObjectsPart : RelatedPartBase
{
  /// <summary>Роль объектов, связанных с обрабатываемым. Позволяет указать части, что она должна читать - состав или применяемость объекта.</summary>
  protected RelatedObjectsRole _role;
  /// <summary>Идентификатор типа используемой связи.</summary>
  protected int _relTypeID = -1;
  /// <summary>Текущий пользователь и роль</summary>
  [NonSerialized]
  protected static ICurrentUserAndRole _userRole;
  /// <summary>Идентификатор типа, объекты которого и его дочерние объекты будут возвращены создаваемой частью</summary>
  protected int _parentObjTypeID = -1;

  /// <summary>Идентификатор типа, объекты которого и его дочерние объекты будут возвращены создаваемой частью.</summary>
  public int ParentObjectTypeID
  {
    [DebuggerStepThrough] get => this._parentObjTypeID;
    set => this._parentObjTypeID = value;
  }

  /// <summary>Идентификатор типа используемой связи.</summary>
  public int RelationTypeID
  {
    [DebuggerStepThrough] get => this._relTypeID;
  }

  /// <summary>Текущий пользователь и роль</summary>
  protected static ICurrentUserAndRole UserRole
  {
    get
    {
      return LazyInitializer.EnsureInitialized<ICurrentUserAndRole>(ref RelatedObjectsPart._userRole, (Func<ICurrentUserAndRole>) (() => ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole));
    }
  }

  /// <summary>
  /// Конструктор части, позволяющий указать обрабатываемый объект и роль связанных
  /// с ним объектов. Созданная часть будет возвращать все объекты из
  /// состава/применяемости обрабатываемого объекта, связанные с ним любым
  /// типом связи.
  /// </summary>
  /// <param name="objTypeID">Идентификатор типа обрабатываемого объекта.</param>
  /// <param name="objID">Идентификатор версии обрабатываемого объекта.</param>
  /// <param name="role">Роль связанных объектов.</param>
  /// <param name="services">Контейнер сервисов</param>
  public RelatedObjectsPart(
    int objTypeID,
    long objID,
    RelatedObjectsRole role,
    IServiceProvider services)
    : base(services)
  {
    this._objTypeID = objTypeID;
    this._objID = objID;
    this._role = role;
    this._relTypeID = -1;
  }

  /// <summary>
  /// Конструктор части, позволяющий указать обрабатываемый объект и роль связанных
  /// с ним объектов. Созданная часть будет возвращать объекты из
  /// состава/применяемости обрабатываемого объекта, связанные с ним любым
  /// типом связи и удовлетворяющие указанному условию.
  /// </summary>
  /// <param name="objTypeID">Идентификатор типа обрабатываемого объекта.</param>
  /// <param name="objID">Идентификатор версии обрабатываемого объекта.</param>
  /// <param name="role">Роль связанных объектов.</param>
  /// <param name="condition">Условие, которому должны удовлетворять объекты.</param>
  /// <param name="services">Контейнер сервисов</param>
  public RelatedObjectsPart(
    int objTypeID,
    long objID,
    RelatedObjectsRole role,
    ConditionStructure condition,
    IServiceProvider services)
    : base(condition, services)
  {
    this._objTypeID = objTypeID;
    this._objID = objID;
    this._role = role;
    this._relTypeID = -1;
  }

  /// <summary>
  /// Конструктор части, позволяющий указать обрабатываемый объект и роль связанных
  /// с ним объектов. Созданная часть будет возвращать объекты из
  /// состава/применяемости обрабатываемого объекта, связанные с ним любым
  /// типом связи и удовлетворяющие указанным условиям.
  /// </summary>
  /// <param name="objTypeID">Идентификатор типа обрабатываемого объекта.</param>
  /// <param name="objID">Идентификатор версии обрабатываемого объекта.</param>
  /// <param name="role">Роль связанных объектов.</param>
  /// <param name="conditions">Массив условий, которым должны удовлетворять объекты.</param>
  /// <param name="services">Контейнер сервисов</param>
  public RelatedObjectsPart(
    int objTypeID,
    long objID,
    RelatedObjectsRole role,
    ConditionStructure[] conditions,
    IServiceProvider services)
    : base(conditions, services)
  {
    this._objTypeID = objTypeID;
    this._objID = objID;
    this._role = role;
    this._relTypeID = -1;
  }

  /// <summary>
  /// Конструктор части, позволяющий указать обрабатываемый объект и роль связанных
  /// с ним объектов. Созданная часть будет возвращать объекты из
  /// состава/применяемости обрабатываемого объекта, связанные с ним любым
  /// типом связи и удовлетворяющие динамически изменяющимся условиям.
  /// </summary>
  /// <param name="objTypeID">Идентификатор типа обрабатываемого объекта.</param>
  /// <param name="objID">Идентификатор версии обрабатываемого объекта.</param>
  /// <param name="role">Роль связанных объектов.</param>
  /// <param name="conditionsProvider">Провайдер условий, которым должны удовлетворять объекты.</param>
  /// <param name="services">Контейнер сервисов</param>
  public RelatedObjectsPart(
    int objTypeID,
    long objID,
    RelatedObjectsRole role,
    IConditionsProvider conditionsProvider,
    IServiceProvider services)
    : base(conditionsProvider, services)
  {
    this._objTypeID = objTypeID;
    this._objID = objID;
    this._role = role;
    this._relTypeID = -1;
  }

  /// <summary>
  /// Конструктор части, позволяющий указать обрабатываемый объект и роль связанных
  /// с ним объектов. Созданная часть будет возвращать все объекты из
  /// состава/применяемости обрабатываемого объекта, связанные с ним указанным
  /// типом связи.
  /// </summary>
  /// <param name="objTypeID">Идентификатор типа обрабатываемого объекта.</param>
  /// <param name="objID">Идентификатор версии обрабатываемого объекта.</param>
  /// <param name="role">Роль связанных объектов.</param>
  /// <param name="relTypeID">Идентификатор типа связи.</param>
  /// <param name="services">Контейнер сервисов</param>
  public RelatedObjectsPart(
    int objTypeID,
    long objID,
    RelatedObjectsRole role,
    int relTypeID,
    IServiceProvider services)
    : base(services)
  {
    this._objTypeID = objTypeID;
    this._objID = objID;
    this._role = role;
    this._relTypeID = relTypeID;
  }

  /// <summary>
  /// Конструктор части, позволяющий указать обрабатываемый объект и роль связанных
  /// с ним объектов. Созданная часть будет возвращать объекты из
  /// состава/применяемости обрабатываемого объекта, связанные с ним указанным
  /// типом связи и удовлетворяющие указанному условию.
  /// </summary>
  /// <param name="objTypeID">Идентификатор типа обрабатываемого объекта.</param>
  /// <param name="objID">Идентификатор версии обрабатываемого объекта.</param>
  /// <param name="role">Роль связанных объектов.</param>
  /// <param name="relTypeID">Идентификатор типа связи.</param>
  /// <param name="condition">Условие, которому должны удовлетворять объекты.</param>
  /// <param name="services">Контейнер сервисов</param>
  public RelatedObjectsPart(
    int objTypeID,
    long objID,
    RelatedObjectsRole role,
    int relTypeID,
    ConditionStructure condition,
    IServiceProvider services)
    : base(condition, services)
  {
    this._objTypeID = objTypeID;
    this._objID = objID;
    this._role = role;
    this._relTypeID = relTypeID;
  }

  /// <summary>
  /// Конструктор части, позволяющий указать обрабатываемый объект и роль связанных
  /// с ним объектов. Созданная часть будет возвращать объекты из
  /// состава/применяемости обрабатываемого объекта, связанные с ним указанным
  /// типом связи и удовлетворяющие указанным условиям.
  /// </summary>
  /// <param name="objTypeID">Идентификатор типа обрабатываемого объекта.</param>
  /// <param name="objID">Идентификатор версии обрабатываемого объекта.</param>
  /// <param name="role">Роль связанных объектов.</param>
  /// <param name="relTypeID">Идентификатор типа связи.</param>
  /// <param name="conditions">Массив условий, которым должны удовлетворять объекты.</param>
  /// <param name="services">Контейнер сервисов</param>
  public RelatedObjectsPart(
    int objTypeID,
    long objID,
    RelatedObjectsRole role,
    int relTypeID,
    ConditionStructure[] conditions,
    IServiceProvider services)
    : base(conditions, services)
  {
    this._objTypeID = objTypeID;
    this._objID = objID;
    this._role = role;
    this._relTypeID = relTypeID;
  }

  /// <summary>
  /// Конструктор части, позволяющий указать обрабатываемый объект и роль связанных с ним объектов.
  /// Созданная часть будет возвращать объекты из состава/применяемости обрабатываемого объекта,
  /// связанные с ним указанным типом связи и удовлетворяющие указанным условиям.
  /// </summary>
  /// <param name="objTypeID">Идентификатор типа обрабатываемого объекта.</param>
  /// <param name="objID">Идентификатор версии обрабатываемого объекта.</param>
  /// <param name="role">Роль связанных объектов.</param>
  /// <param name="relTypeID">Идентификатор типа связи.</param>
  /// <param name="parentObjTypeID">Идентификатор типа, объекты которого и его дочерние объекты будут возвращены создаваемой частью.</param>
  /// <param name="conditions">Массив условий, которым должны удовлетворять объекты.</param>
  /// <param name="services">Контейнер сервисов</param>
  public RelatedObjectsPart(
    int objTypeID,
    long objID,
    RelatedObjectsRole role,
    int relTypeID,
    int parentObjTypeID,
    ConditionStructure[] conditions,
    IServiceProvider services)
    : this(objTypeID, objID, role, relTypeID, conditions, services)
  {
    this._parentObjTypeID = parentObjTypeID;
  }

  /// <summary>
  /// Конструктор части, позволяющий указать обрабатываемый объект и роль связанных
  /// с ним объектов. Созданная часть будет возвращать объекты из
  /// состава/применяемости обрабатываемого объекта, связанные с ним указанным
  /// типом связи и удовлетворяющие динамически изменяющимся условиям.
  /// </summary>
  /// <param name="objTypeID">Идентификатор типа обрабатываемого объекта.</param>
  /// <param name="objID">Идентификатор версии обрабатываемого объекта.</param>
  /// <param name="role">Роль связанных объектов.</param>
  /// <param name="relTypeID">Идентификатор типа связи.</param>
  /// <param name="conditionsProvider">Провайдер условий, которым должны удовлетворять объекты.</param>
  /// <param name="services">Контейнер сервисов</param>
  public RelatedObjectsPart(
    int objTypeID,
    long objID,
    RelatedObjectsRole role,
    int relTypeID,
    IConditionsProvider conditionsProvider,
    IServiceProvider services)
    : base(conditionsProvider, services)
  {
    this._objTypeID = objTypeID;
    this._objID = objID;
    this._role = role;
    this._relTypeID = relTypeID;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="conditionsProvider"></param>
  /// <param name="services">Контейнер сервисов</param>
  public RelatedObjectsPart(IConditionsProvider conditionsProvider, IServiceProvider services)
    : base(conditionsProvider, services)
  {
    this._objTypeID = -1;
    this._objID = 0L;
    this._relTypeID = -1;
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
    NodeColumnCollection columns = new NodeColumnCollection();
    if (this._objTypeID != -1)
      Helper.AddObjectTypeColumns(columns, this._objTypeID);
    if (this._relTypeID != -1)
      Helper.AddRelationTypeColumns(columns, this._relTypeID);
    List<int> visibleRelations = RelatedObjectsPart.UserRole.Rule.GetObjectTypeVisibleRelations(this._objTypeID, true);
    if (visibleRelations != null)
    {
      for (int index = 0; index < visibleRelations.Count; ++index)
      {
        int relTypeID = visibleRelations[index];
        if (relTypeID != this._relTypeID)
          Helper.AddRelationTypeColumns(columns, relTypeID);
      }
    }
    base.GetSupportedColumns(ColumnSetName, columns);
    return columns;
  }

  /// <summary>
  /// Возвращает коллекцию всех поддерживаемых данным элементом
  /// виртуальных колонок навигатора. Этот метод используется диалогом
  /// настройки отображения грида.
  /// </summary>
  /// <param name="ColumnSetName">Название набора колонок.
  /// Intermech.Navigator.Consts.NavigatorDefaultColumnSetName - набор колонок по умолчанию</param>
  /// <param name="columns">Коллекция колонок</param>
  /// <returns>Коллекция виртуальных колонок навигатора</returns>
  public override void GetSupportedColumns(string ColumnSetName, NodeColumnCollection columns)
  {
    if (columns == null)
      return;
    if (this._objTypeID != -1)
      Helper.AddObjectTypeColumns(columns, this._objTypeID);
    if (this._relTypeID != -1)
      Helper.AddRelationTypeColumns(columns, this._relTypeID);
    List<int> visibleRelations = RelatedObjectsPart.UserRole.Rule.GetObjectTypeVisibleRelations(this._objTypeID, true);
    if (visibleRelations != null)
    {
      for (int index = 0; index < visibleRelations.Count; ++index)
      {
        int relTypeID = visibleRelations[index];
        if (relTypeID != this._relTypeID)
          Helper.AddRelationTypeColumns(columns, relTypeID);
      }
    }
    base.GetSupportedColumns(ColumnSetName, columns);
  }

  /// <summary>
  /// Возвращает интерфейс объекта-запроса, с помощью которого эта часть
  /// читает список обрабатываемых ею объектов.
  /// </summary>
  /// <param name="conditions">Массив условий, которым должны удовлетворять объекты.</param>
  /// <returns>Ссылка на интерфейс объекта-запроса.</returns>
  protected override INodeQuery GetQuery(ConditionStructure[] conditions)
  {
    if (this._relTypeID != -1 && !(this._role == RelatedObjectsRole.Composition ? (IEnumerable<IMSApplicability>) MetaDataHelper.GetObjectTypeApplicabilities(this._objTypeID) : (IEnumerable<IMSApplicability>) MetaDataHelper.GetObjectTypeParentApplicabilities(this._objTypeID)).Any<IMSApplicability>((Func<IMSApplicability, bool>) (item => item.RelationTypeID == this._relTypeID)))
      return (INodeQuery) null;
    RelatedObjectsQuery query = this.QueryConstruction(conditions);
    query.FiltrationClass = !(this.Owner is IContextAware owner) || owner.Services.GetService(typeof (IFiltrationClass)) == null ? (IFiltrationClass) null : (IFiltrationClass) owner.Services.GetService(typeof (IFiltrationClass));
    IContextAware contextAware = (IContextAware) query;
    if (contextAware == null)
      return (INodeQuery) query;
    if (owner == null)
      return (INodeQuery) query;
    contextAware.Services = owner.Services;
    return (INodeQuery) query;
  }

  protected virtual RelatedObjectsQuery QueryConstruction(ConditionStructure[] conditions)
  {
    return new RelatedObjectsQuery((INodeQuerySupport) this, this._objID, this._objTypeID, this._role, this._relTypeID, this._parentObjTypeID, conditions);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="capabilities"></param>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  /// <returns></returns>
  public override IUpdateAnalyser GetAnalyser(
    NodeViewCapabilities capabilities,
    object sender,
    NotificationEventArgs e)
  {
    if (e is DBRelationsEventArgs relationsEventArgs)
    {
      switch (e.EventName)
      {
        case "RelationsCreated":
          if (!relationsEventArgs.Exists(this._objID, this._relTypeID) && !relationsEventArgs.HasEmptyItems())
            return (IUpdateAnalyser) null;
          return relationsEventArgs.ProjIDs != null && relationsEventArgs.ProjIDs.Contains(this._objID) ? (IUpdateAnalyser) new RelationsCreatedAnalyser(relationsEventArgs.RelationIDs) : (IUpdateAnalyser) null;
        case "RelationsChanged":
          return !relationsEventArgs.Exists(this._objID, this._relTypeID) && !relationsEventArgs.HasEmptyItems() ? (IUpdateAnalyser) null : (IUpdateAnalyser) new RelationsChangedAnalyser(relationsEventArgs.RelationIDs);
        case "RelationsRemoved":
          return !relationsEventArgs.Exists(this._objID, this._relTypeID) && !relationsEventArgs.HasEmptyItems() ? (IUpdateAnalyser) null : (IUpdateAnalyser) new RelationsRemovedAnalyser(relationsEventArgs.RelationIDs);
      }
    }
    DBRelationsManagedEventArgs managedEventArgs = e as DBRelationsManagedEventArgs;
    if (relationsEventArgs == null || !(e.EventName == "ManagedRelationsCreated"))
      return base.GetAnalyser(capabilities, sender, e);
    return !capabilities.CanAppend && (!this.AcceptManagedEvents || !managedEventArgs.AcceptEvent) ? (IUpdateAnalyser) null : (IUpdateAnalyser) new RelationsCreatedAnalyser(relationsEventArgs.RelationIDs);
  }

  /// <summary>
  /// Получить список служебных полей (которые загружаются в узел независимо от настройки вида)
  /// </summary>
  /// <returns>Список служебных полей (которые загружаются в узел независимо от настройки вида)</returns>
  public override List<object> GetSpecialFields()
  {
    List<object> specialFields = base.GetSpecialFields() ?? new List<object>();
    if (MetaDataHelper.GetAttribute4RelationType(this._relTypeID, Convert.ToInt32(ObjectsPartBase.ncSORTING.ID)) != null && !specialFields.Contains((object) ObjectsPartBase.ncSORTING))
      specialFields.Add((object) ObjectsPartBase.ncSORTING);
    return specialFields;
  }
}
