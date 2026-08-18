// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.ObjectCreatedInfo
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Описание созданного объекта</summary>
[Serializable]
public class ObjectCreatedInfo
{
  /// <summary>Поле для хранения идентификатора созданного объекта</summary>
  private long _objectId = -1;
  /// <summary>Идентификатор типа созданного объекта</summary>
  private int _objectTypeId = -1;
  /// <summary>
  /// Поле для хранения прототипа по которому создается объект
  /// </summary>
  private long _prototypeId = -1;
  /// <summary>
  /// Поле для хранения признака - создается новый объект или его версия
  /// </summary>
  private bool _isVersion;
  /// <summary>Перечень созданных связей</summary>
  private ObjectRelationLink[] _relationLinks;

  /// <summary>Конструктор</summary>
  public ObjectCreatedInfo()
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="objectId"></param>
  /// <param name="objectTypeId"></param>
  public ObjectCreatedInfo(long objectId, int objectTypeId)
    : this(objectId, objectTypeId, -1L, false)
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="objectId"></param>
  /// <param name="objectTypeId"></param>
  /// <param name="prototypeId"></param>
  /// <param name="isVersion"></param>
  public ObjectCreatedInfo(long objectId, int objectTypeId, long prototypeId, bool isVersion)
  {
    this._objectId = objectId;
    this._objectTypeId = objectTypeId;
    this._prototypeId = prototypeId;
    this._isVersion = isVersion;
  }

  /// <summary>Идентификатор созданного объекта</summary>
  public long ObjectId
  {
    get => this._objectId;
    set => this._objectId = value;
  }

  /// <summary>Идентификатор прототипа по которому создается объект</summary>
  public long PrototypeId
  {
    get => this._prototypeId;
    internal set => this._prototypeId = value;
  }

  /// <summary>Признак - создается новый объект или его версия</summary>
  public bool IsVersion
  {
    get => this._isVersion;
    internal set => this._isVersion = value;
  }

  /// <summary>Идентификатор типа созданного объекта</summary>
  public int ObjectTypeId
  {
    get => this._objectTypeId;
    internal set => this._objectTypeId = value;
  }

  /// <summary>Перечень созданных связей</summary>
  public ObjectRelationLink[] RelationLinks
  {
    get => this._relationLinks;
    set => this._relationLinks = value;
  }
}
