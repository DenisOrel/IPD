
// Type: Intermech.Navigator.DBObjects.NodeID
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Navigator.Interfaces;
using Intermech.Search.Utilities;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;


namespace Intermech.Navigator.DBObjects;

/// <summary>
/// Реализует унифицированный идентификатор, предназначенный для обозначения
/// элементов "Объект базы данных" из пространства навигации.
/// </summary>
public class NodeID : INodeID, IEquatable<NodeID>
{
  /// <summary>Параметры, в которых хранится описание узла</summary>
  protected CreateObjectNodeParams pars = new CreateObjectNodeParams();
  /// <summary>Печенюга</summary>
  protected object cookie;

  /// <summary>
  /// Создать описание узла на основании указанных параметров
  /// </summary>
  /// <param name="e">Параметры для создания описания узла</param>
  public NodeID(CreateObjectNodeParams e)
  {
    this.pars = new CreateObjectNodeParams((object) e);
    this.cookie = (object) null;
  }

  /// <summary>
  /// Конструктор, позволяющий создать идентификатор, описывающий объект,
  /// информация о котором была прочитана из таблицы связей объектов.
  /// </summary>
  /// <param name="objTypeId">Идентификатор типа объекта</param>
  /// <param name="objId">Идентификатор версии объекта</param>
  /// <param name="id">Идентификатор объекта</param>
  /// <param name="prjLinkId">Идентификатор связи</param>
  /// <param name="checkedOutBy">Кем объект взят на изменение</param>
  /// <param name="lcStepID">Шаг жизненного цикла</param>
  /// <param name="caption">Заголовок объекта</param>
  /// <param name="relTypeID">Идентификатор типа связи</param>
  /// <param name="owner">Идентификатор владельца объекта</param>
  /// <param name="sorting">Значение атрибута "Сортировка" (если объект - в составе)</param>
  /// <param name="state">Статус подбора версии</param>
  /// <param name="version">Версия объекта</param>
  /// <param name="baseVersion">Признак базовой версии</param>
  /// <param name="siteID">Узел информационной системы</param>
  /// <param name="projID">Идентификатор версии родительского объекта</param>
  /// <param name="relGuid">Guid связи</param>
  /// <param name="modificationID">Номер группы изменений (не равна 0 - объект принадлежит контексту редактирования)</param>
  public NodeID(
    int objTypeId,
    long objId,
    long id,
    long checkedOutBy,
    long prjLinkId,
    int lcStepID,
    string caption,
    int relTypeID,
    long owner,
    long sorting,
    ObjectFiltrationState state,
    long version,
    long baseVersion,
    string siteID,
    long projID,
    Guid relGuid,
    long modificationID)
  {
    this.pars.ObjectTypeID = objTypeId;
    this.pars.ObjectID = objId;
    this.pars.ID = id;
    this.pars.CheckedOutBy = checkedOutBy;
    this.pars.PrjLinkID = prjLinkId;
    this.pars.LCStepID = lcStepID;
    this.pars.Caption = caption;
    this.pars.RelationTypeID = relTypeID;
    this.pars.Owner = owner;
    this.pars.Sorting = sorting;
    this.pars.State = state;
    this.pars.Version = version;
    this.pars.BaseVersion = baseVersion;
    this.pars.SiteID = siteID;
    this.pars.ProjID = projID;
    this.pars.RelGuid = relGuid;
    this.pars.ModificationID = modificationID;
    this.cookie = (object) null;
  }

  /// <summary>Возвращает идентификатор версии объекта.</summary>
  public long ObjectID
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.pars.ObjectID;
    }
  }

  /// <summary>Возвращает идентификатор объекта.</summary>
  public long ID
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.pars.ID;
  }

  /// <summary>Возвращает идентификатор типа объекта.</summary>
  public int ObjectTypeID
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.pars.ObjectTypeID;
    }
  }

  /// <summary>Возвращает идентификатор связи объекта.</summary>
  public long PrjLinkID
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.pars.PrjLinkID;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)] set => this.pars.PrjLinkID = value;
  }

  /// <summary>
  /// Возвращает идентификатор пользователя, взявшего объект на изменение.
  /// </summary>
  public long CheckedOutBy
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.pars.CheckedOutBy;
    }
  }

  /// <summary>Шаг жизненного цикла</summary>
  public int LCStepID
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.pars.LCStepID;
    }
  }

  /// <summary>Заголовок объекта</summary>
  public string Caption
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.pars.Caption;
    }
  }

  /// <summary>Возвращает идентификатор типа связи.</summary>
  public int RelationTypeID
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.pars.RelationTypeID;
    }
  }

  /// <summary>Владелец объекта</summary>
  public long Owner
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.pars.Owner;
    [MethodImpl(MethodImplOptions.AggressiveInlining)] set => this.pars.Owner = value;
  }

  /// <summary>
  /// Значение атрибута "Сортировка" (если объект - в составе)
  /// </summary>
  public long Sorting
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.pars.Sorting;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)] set => this.pars.Sorting = value;
  }

  /// <summary>Состояние фильтрации версии</summary>
  public ObjectFiltrationState State
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.pars.State;
    [MethodImpl(MethodImplOptions.AggressiveInlining)] set => this.pars.State = value;
  }

  public FontStyle fontStyle
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.pars.fontStyle;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)] set => this.pars.fontStyle = value;
  }

  /// <summary>Номер версии объекта</summary>
  public long Version
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.pars.Version;
    }
  }

  /// <summary>Признак базовой версии</summary>
  public long BaseVersion
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.pars.BaseVersion;
    }
  }

  /// <summary>Узел информационной системы</summary>
  public string SiteID
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.pars.SiteID;
  }

  /// <summary>Идентификатор версии родительского объекта</summary>
  public virtual long ProjID
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.pars.ProjID;
  }

  /// <summary>Guid связи</summary>
  public Guid RelGuid
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.pars.RelGuid;
    }
  }

  /// <summary>
  /// Номер группы изменений (не равна 0 - объект принадлежит контексту редактирования)
  /// </summary>
  public long ModificationID
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.pars.ModificationID;
    }
  }

  /// <summary>Значение атрибута "Идентификатор версии в составе"</summary>
  public long ExplicitPartVersionID { [MethodImpl(MethodImplOptions.AggressiveInlining)] get; [MethodImpl(MethodImplOptions.AggressiveInlining)] set; }

  /// <summary>
  /// Возвращает идентификатор категории описываемого элемента.
  /// </summary>
  public virtual int CategoryID
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => 1;
  }

  /// <summary>Возвращает идентификатор типа описываемого элемента.</summary>
  public int TypeID
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.pars.ObjectTypeID;
    }
  }

  /// <summary>Печенюга</summary>
  public object Cookie
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.cookie;
    [MethodImpl(MethodImplOptions.AggressiveInlining)] set => this.cookie = value;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="other"></param>
  /// <returns></returns>
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public bool Equals(NodeID other)
  {
    if (other == this)
      return true;
    if (Math.Abs(this.pars.ObjectID) != Math.Abs(other.pars.ObjectID))
      return false;
    long prjLinkId1 = this.pars.PrjLinkID;
    long prjLinkId2 = other.pars.PrjLinkID;
    return RelationHelper.IsUnknownRelationID(prjLinkId1) && RelationHelper.IsUnknownRelationID(prjLinkId2) || prjLinkId1 == prjLinkId2 || this.pars.RelGuid == other.pars.RelGuid;
  }

  /// <summary>Сравнить с указанным объектом</summary>
  /// <param name="obj">Объект для сравнения</param>
  /// <returns>true, если объекты равны</returns>
  public override bool Equals(object obj)
  {
    if (obj == this)
      return true;
    return obj is NodeID other && this.Equals(other);
  }

  /// <summary>Вернуть 32-битный хэш-код экземпляра объекта</summary>
  /// <returns>32-битный хэш-код экземпляра объекта</returns>
  public override int GetHashCode()
  {
    long num = this.pars.PrjLinkID;
    int hashCode1 = num.GetHashCode();
    num = this.pars.ObjectID;
    int hashCode2 = num.GetHashCode();
    return hashCode1 ^ hashCode2;
  }

  /// <summary>Преобразование в IDBTypedObjectID</summary>
  /// <remarks>Учитывая что оба класса по сути содержат одну и ту же информацию непонятно зачем их разделили.
  /// Но тем не менее некоторые методы просят на вход один тип, а некоторые - другой, так что надо как-то их преобразовывать</remarks>
  public static explicit operator DBTypedObjectID(NodeID nodeID)
  {
    return new DBTypedObjectID(nodeID.pars.ObjectTypeID, nodeID.pars.ObjectID, nodeID.pars.ID, nodeID.pars.Caption, nodeID.pars.Owner, nodeID.pars.Version, nodeID.pars.BaseVersion, nodeID.pars.SiteID, nodeID.pars.ModificationID);
  }

  public NodeID InverseCheckedNode()
  {
    NodeID nodeId = new NodeID(this.pars);
    nodeId.cookie = this.cookie;
    if (nodeId.pars.ObjectID != 0L && nodeId.pars.ObjectID != 0L)
      nodeId.pars.ObjectID = -nodeId.pars.ObjectID;
    return nodeId;
  }
}
