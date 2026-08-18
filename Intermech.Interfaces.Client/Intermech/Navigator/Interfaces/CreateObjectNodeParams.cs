// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.CreateObjectNodeParams
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Interfaces;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>Параметры для создания описания узла</summary>
public class CreateObjectNodeParams : IAssignable, ICloneable
{
  /// <summary>Идентификатор типа объекта</summary>
  private int objTypeId;
  /// <summary>Идентификатор версии объекта</summary>
  private long objId;
  /// <summary>Идентификатор объекта</summary>
  private long id;
  /// <summary>Идентификатор связи</summary>
  private long prjLinkId;
  /// <summary>
  /// Идентификатор пользователя, взявшего объект на изменение
  /// </summary>
  private long checkedOutBy;
  /// <summary>Шаг жизненного цикла</summary>
  private int lcStepID;
  /// <summary>Заголовок объекта</summary>
  private string caption;
  /// <summary>Идентификатор типа связи</summary>
  private int relTypeID;
  /// <summary>
  /// Идентификатор версии родительского объекта (для связи)
  /// </summary>
  protected long projID;
  /// <summary>Guid связи</summary>
  private Guid relGuid;
  /// <summary>Идентификатор владельца объекта</summary>
  private long owner;
  /// <summary>
  /// Значение атрибута связи "Сортировка" (если объект - в составе)
  /// </summary>
  private long sorting;
  /// <summary>Состояние фильтрации версии</summary>
  private ObjectFiltrationState state;
  /// <summary>Номер версии объекта</summary>
  private long version;
  /// <summary>Признак базовой версии</summary>
  private long baseVersion;
  /// <summary>Узлы информационной системы</summary>
  private string siteID;
  /// <summary>
  /// Номер группы изменений (не равна 0 - объект принадлежит контексту редактирования)
  /// </summary>
  private long modificationID;
  /// <summary>Стиль шрифта</summary>
  private FontStyle _fontStyle;

  /// <summary>Идентификатор версии объекта</summary>
  public long ObjectID
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.objId;
    [MethodImpl(MethodImplOptions.AggressiveInlining)] set => this.objId = value;
  }

  /// <summary>Идентификатор объекта</summary>
  public long ID
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.id;
    [MethodImpl(MethodImplOptions.AggressiveInlining)] set => this.id = value;
  }

  /// <summary>Идентификатор типа объекта</summary>
  public int ObjectTypeID
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.objTypeId;
    [MethodImpl(MethodImplOptions.AggressiveInlining)] set => this.objTypeId = value;
  }

  /// <summary>Идентификатор связи объекта</summary>
  public long PrjLinkID
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.prjLinkId;
    [MethodImpl(MethodImplOptions.AggressiveInlining)] set => this.prjLinkId = value;
  }

  /// <summary>
  /// Идентификатор пользователя, взявшего объект на изменение
  /// </summary>
  public long CheckedOutBy
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.checkedOutBy;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)] set => this.checkedOutBy = value;
  }

  /// <summary>Шаг жизненного цикла</summary>
  public int LCStepID
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.lcStepID;
    [MethodImpl(MethodImplOptions.AggressiveInlining)] set => this.lcStepID = value;
  }

  /// <summary>Заголовок объекта</summary>
  public string Caption
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.caption;
    [MethodImpl(MethodImplOptions.AggressiveInlining)] set => this.caption = value;
  }

  /// <summary>Идентификатор типа связи</summary>
  public int RelationTypeID
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.relTypeID;
    [MethodImpl(MethodImplOptions.AggressiveInlining)] set => this.relTypeID = value;
  }

  /// <summary>Владелец объекта</summary>
  public long Owner
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.owner;
    [MethodImpl(MethodImplOptions.AggressiveInlining)] set => this.owner = value;
  }

  /// <summary>
  /// Значение атрибута "Сортировка" (если объект - в составе)
  /// </summary>
  public long Sorting
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.sorting;
    [MethodImpl(MethodImplOptions.AggressiveInlining)] set => this.sorting = value;
  }

  /// <summary>Состояние фильтрации версии</summary>
  public ObjectFiltrationState State
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.state;
    [MethodImpl(MethodImplOptions.AggressiveInlining)] set => this.state = value;
  }

  /// <summary>Номер версии объекта</summary>
  public long Version
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.version;
    [MethodImpl(MethodImplOptions.AggressiveInlining)] set => this.version = value;
  }

  /// <summary>Признак базовой версии</summary>
  public long BaseVersion
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.baseVersion;
    [MethodImpl(MethodImplOptions.AggressiveInlining)] set => this.baseVersion = value;
  }

  /// <summary>Узлы информационной системы</summary>
  public string SiteID
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.siteID;
    [MethodImpl(MethodImplOptions.AggressiveInlining)] set => this.siteID = value;
  }

  /// <summary>
  /// Идентификатор версии родительского объекта (для связи)
  /// </summary>
  public long ProjID
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.projID;
    [MethodImpl(MethodImplOptions.AggressiveInlining)] set => this.projID = value;
  }

  /// <summary>Guid связи</summary>
  public Guid RelGuid
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.relGuid;
    [MethodImpl(MethodImplOptions.AggressiveInlining)] set => this.relGuid = value;
  }

  /// <summary>
  /// Номер группы изменений (не равна 0 - объект принадлежит контексту редактирования)
  /// </summary>
  public long ModificationID
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.modificationID;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)] set => this.modificationID = value;
  }

  public FontStyle fontStyle
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._fontStyle;
    [MethodImpl(MethodImplOptions.AggressiveInlining)] set => this._fontStyle = value;
  }

  /// <summary>
  /// Создать пустые параметры, описывающие узел, связанный с объектом, связью
  /// </summary>
  public CreateObjectNodeParams()
  {
  }

  /// <summary>
  /// Создать параметры, описывающие узел, связанный с объектом, связью
  /// </summary>
  /// <param name="source">Объект-источник</param>
  public CreateObjectNodeParams(object source) => this.Assign(source);

  /// <summary>
  /// Создать параметры, описывающие узел, связанный с объектом, связью
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
  /// <param name="state">Состояние фильтрации версии</param>
  /// <param name="version">Номер версии объекта</param>
  /// <param name="baseVersion">Признак базовой версии</param>
  /// <param name="siteID">Узлы информационной системы</param>
  /// <param name="projID">Идентификатор родительского объекта</param>
  /// <param name="relGuid">Guid связи</param>
  /// <param name="modificationID">Номер группы изменений (не равна 0 - объект принадлежит контексту редактирования)</param>
  /// <param name="fontStyle">Стиль шрифта для ноды</param>
  public CreateObjectNodeParams(
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
    long modificationID,
    FontStyle fontStyle = FontStyle.Regular)
  {
    this.objTypeId = objTypeId;
    this.objId = objId;
    this.id = id;
    this.checkedOutBy = checkedOutBy;
    this.prjLinkId = prjLinkId;
    this.lcStepID = lcStepID;
    this.caption = caption;
    this.relTypeID = relTypeID;
    this.owner = owner;
    this.sorting = sorting;
    this.state = state;
    this.version = version;
    this.baseVersion = baseVersion;
    this.siteID = siteID;
    this.projID = projID;
    this.relGuid = relGuid;
    this.modificationID = modificationID;
    this._fontStyle = fontStyle;
  }

  /// <summary>Очистить поля класса</summary>
  public virtual void Clear()
  {
    this.caption = string.Empty;
    this.checkedOutBy = 0L;
    this.id = 0L;
    this.lcStepID = -1;
    this.objId = 0L;
    this.objTypeId = -1;
    this.owner = 0L;
    this.prjLinkId = 0L;
    this.relTypeID = -1;
    this.sorting = 0L;
    this.state = ObjectFiltrationState.fsNotRequired;
    this.version = 0L;
    this.baseVersion = 0L;
    this.siteID = string.Empty;
    this.modificationID = 0L;
    this._fontStyle = FontStyle.Regular;
  }

  /// <summary>Скопировать в текущий объект поля из другого объекта.</summary>
  /// <param name="source">Объект-источник</param>
  public virtual void Assign(object source)
  {
    if (this == source)
      return;
    this.Clear();
    if (!(source is CreateObjectNodeParams objectNodeParams))
      return;
    this.caption = objectNodeParams.Caption;
    this.checkedOutBy = objectNodeParams.CheckedOutBy;
    this.id = objectNodeParams.ID;
    this.lcStepID = objectNodeParams.LCStepID;
    this.objId = objectNodeParams.ObjectID;
    this.objTypeId = objectNodeParams.ObjectTypeID;
    this.owner = objectNodeParams.Owner;
    this.prjLinkId = objectNodeParams.PrjLinkID;
    this.relTypeID = objectNodeParams.RelationTypeID;
    this.sorting = objectNodeParams.Sorting;
    this.state = objectNodeParams.State;
    this.version = objectNodeParams.Version;
    this.baseVersion = objectNodeParams.BaseVersion;
    this.siteID = objectNodeParams.SiteID;
    this.projID = objectNodeParams.ProjID;
    this.relGuid = objectNodeParams.RelGuid;
    this.modificationID = objectNodeParams.ModificationID;
    this._fontStyle = objectNodeParams._fontStyle;
  }

  /// <summary>Создать точную копию экземпляра класса</summary>
  /// <returns>Точная копия экземпляра класса</returns>
  public virtual object Clone()
  {
    CreateObjectNodeParams instance = Activator.CreateInstance(this.GetType()) as CreateObjectNodeParams;
    instance.Assign((object) this);
    return (object) instance;
  }
}
