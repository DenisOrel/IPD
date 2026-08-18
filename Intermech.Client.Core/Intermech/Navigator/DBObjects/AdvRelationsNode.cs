
// Type: Intermech.Navigator.DBObjects.AdvRelationsNode
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace Intermech.Navigator.DBObjects;

/// <summary>
/// Узел для получения состава указанного объекта по определённому типу связи
/// </summary>
public class AdvRelationsNode : CompositeNode, IContextAware
{
  /// <summary>Категория</summary>
  protected int _categoryID;
  /// <summary>Тип</summary>
  protected int _typeID;
  /// <summary>Параметры</summary>
  protected AdvCreateObjectNodeParams _pars;
  /// <summary>Контейнер сервисов</summary>
  protected IServiceProvider _services;

  /// <summary>Категория</summary>
  public int CategoryID
  {
    [DebuggerStepThrough] get => this._categoryID;
  }

  /// <summary>Тип</summary>
  public int TypeID
  {
    [DebuggerStepThrough] get => this._typeID;
  }

  /// <summary>
  /// Уникальный ключ настроек фильтрации состава.
  /// Если фильтрация состава не требуется, можно
  /// указать константу Intermech.SystemGUIDs.filtrationAllVersions.
  /// </summary>
  public string FiltrationOwnerID
  {
    [DebuggerStepThrough] get => this._pars.FiltrationOwnerID;
  }

  /// <summary>Контексты, в рамках которых будет получен состав</summary>
  public List<long> Contexts
  {
    [DebuggerStepThrough] get => this._pars.Contexts;
  }

  /// <summary>Идентификатор типа родительского объекта</summary>
  public int ProjObjType
  {
    [DebuggerStepThrough] get => this._pars.ProjObjType;
  }

  /// <summary>Идентификатор версии родительского объекта</summary>
  public long ProjID
  {
    [DebuggerStepThrough] get => this._pars.ProjID;
  }

  /// <summary>Guid связи</summary>
  public Guid RelGuid
  {
    [DebuggerStepThrough] get => this._pars.RelGuid;
  }

  /// <summary>Идентификатор версии объекта</summary>
  public long ObjID
  {
    [DebuggerStepThrough] get => this._pars.ObjectID;
  }

  /// <summary>Идентификатор объекта</summary>
  public long ID
  {
    [DebuggerStepThrough] get => this._pars.ID;
  }

  /// <summary>Идентификатор типа объекта</summary>
  public int ObjType
  {
    [DebuggerStepThrough] get => this._pars.ObjectTypeID;
  }

  /// <summary>
  /// Идентификатор типа связи, по которой будет получен состав
  /// </summary>
  public int RelationTypeID
  {
    [DebuggerStepThrough] get => this._pars.RelationTypeID;
  }

  /// <summary>Идентификатор связи</summary>
  public long PrjLinkID
  {
    [DebuggerStepThrough] get => this._pars.PrjLinkID;
  }

  /// <summary>Заголовок объекта</summary>
  public string Caption
  {
    [DebuggerStepThrough] get => this._pars.Caption;
  }

  /// <summary>Кем объект взят на изменение</summary>
  public long CheckedOutBy
  {
    [DebuggerStepThrough] get => this._pars.CheckedOutBy;
  }

  /// <summary>Владелец объекта</summary>
  public long Owner
  {
    [DebuggerStepThrough] get => this._pars.Owner;
  }

  /// <summary>Значение атрибута "Сортировка"</summary>
  public long Sorting
  {
    [DebuggerStepThrough] get => this._pars.Sorting;
  }

  /// <summary>Значение атрибута "Шаг ЖЦ"</summary>
  public int LCStepID
  {
    [DebuggerStepThrough] get => this._pars.LCStepID;
  }

  /// <summary>
  /// Список дополнительных идентификаторов атрибутов, которые будут загружаться в узел независимо от видимых колонок
  /// </summary>
  public List<int> Attributes
  {
    [DebuggerStepThrough] get => this._pars.Attributes;
  }

  /// <summary>Список значений дополнительных атрибутов</summary>
  public object[] Values
  {
    [DebuggerStepThrough] get => this._pars.Values;
  }

  /// <summary>Значение указанного атрибута</summary>
  /// <param name="attributeID">Идентификатор атрибута</param>
  /// <returns>null, если значение атрибута не найдено</returns>
  public object this[int attributeID]
  {
    get
    {
      return !this._pars.Attributes.Contains(attributeID) ? (object) null : this._pars.Values[this._pars.Attributes.IndexOf(attributeID)];
    }
  }

  /// <summary>
  /// Создать описание узла на основании указанных параметров
  /// </summary>
  /// <param name="e">Параметры для создания описания узла</param>
  public AdvRelationsNode(CreateObjectNodeParams e)
  {
    this._pars = new AdvCreateObjectNodeParams((object) e);
    this.options = NodeOptions.CanContainsComposition;
  }

  /// <summary>Контейнер сервисов</summary>
  public IServiceProvider Services
  {
    [DebuggerStepThrough] get => this._services;
    set => this._services = value;
  }

  /// <summary>
  /// Создает и возвращает часть, которая отвечает за дочерние элементы-папки.
  /// </summary>
  /// <returns>Ссылка на интерфейс части</returns>
  protected override List<PartSlot> CreateFolderSlots()
  {
    return this.ObjID != -1L && this.ObjID != 0L ? this.SlotsFromSinglePart((INodePart) new AdvRelationsPart(this.ObjType, this.ObjID, this.RelationTypeID, this.FiltrationOwnerID, this.Contexts, this.Attributes, this.Services)) : (List<PartSlot>) null;
  }
}
