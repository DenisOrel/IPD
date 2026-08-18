// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Imbase.Filters.ImbaseObjFilterInfo
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.Imbase.Filters;

/// <summary>Класс описание фильтра Imbase для объектов</summary>
[Serializable]
public class ImbaseObjFilterInfo
{
  /// <summary>Ид. версии объекта соответствующего фильтру</summary>
  private readonly long _objectID;
  /// <summary>Ид. типа объекта для которого настроен данный фильтр</summary>
  private int _refObjTypeID;
  /// <summary>Заголок объекта соответсвующего фильтру</summary>
  private string _caption;
  /// <summary>Владелец объекта</summary>
  /// <remarks>Общий, пользователь, роль, предметная область</remarks>
  private string _owner;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="objectId">Ид. версии объекта соответствующего фильтру</param>
  /// <param name="refObjTypeId"></param>
  /// <param name="caption">Заголок объекта соответсвующего фильтру</param>
  public ImbaseObjFilterInfo(long objectId, int refObjTypeId, string caption)
    : this(objectId, refObjTypeId, caption, string.Empty)
  {
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="objectId">Ид. версии объекта соответствующего фильтру</param>
  /// <param name="refObjTypeId"></param>
  /// <param name="caption">Заголок объекта соответсвующего фильтру</param>
  /// <param name="owner">Владелец фильтра</param>
  public ImbaseObjFilterInfo(long objectId, int refObjTypeId, string caption, string owner)
  {
    this._objectID = objectId;
    this._refObjTypeID = refObjTypeId;
    this._caption = caption;
    this._owner = owner;
  }

  /// <summary>Ид. версии объекта соответствующего фильтру</summary>
  public long ObjectID
  {
    [DebuggerStepThrough] get => this._objectID;
  }

  /// <summary>Ид. типа объекта для которого настроен данный фильтр</summary>
  public int RefObjTypeID
  {
    [DebuggerStepThrough] get => this._refObjTypeID;
    set => this._refObjTypeID = value;
  }

  /// <summary>Заголок объекта соответствующего фильтру</summary>
  public string Caption
  {
    [DebuggerStepThrough] get => this._caption;
    set => this._caption = value;
  }

  /// <summary>Владелец фильтра</summary>
  public string Owner
  {
    [DebuggerStepThrough] get => this._owner;
    set => this._owner = value;
  }
}
