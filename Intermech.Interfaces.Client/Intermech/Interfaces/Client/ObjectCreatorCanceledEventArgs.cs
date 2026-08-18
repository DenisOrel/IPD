// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.ObjectCreatorCanceledEventArgs
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Класс для работы с событием, возникающим при отмене создания объекта,
/// если заготовка была создана.
/// </summary>
public class ObjectCreatorCanceledEventArgs : EventArgs
{
  /// <summary>
  /// Поле для хранения идентификатора ЗАГОТОВКИ созданного объекта
  /// </summary>
  protected long createdZagId = -1;
  /// <summary>
  /// Поле для хранения признака - создается новый объект или его версия
  /// </summary>
  protected bool isVersion;
  /// <summary>Идентификатор типа созданного объекта</summary>
  protected int objectTypeID = -1;

  /// <summary>Конструктор</summary>
  /// <param name="createdObjectId">Идентификатор созданной заготовки объекта</param>
  public ObjectCreatorCanceledEventArgs(long createdObjectId)
    : this(createdObjectId, false, -1)
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="createdObjectId">Идентификатор созданной заготовки объекта</param>
  /// <param name="isVersion">Признак - создавался новый объект или его версия</param>
  public ObjectCreatorCanceledEventArgs(long createdObjectId, bool isVersion)
  {
    this.createdZagId = createdObjectId;
    this.isVersion = isVersion;
  }

  /// <summary>Конструктор</summary>
  /// <param name="createdObjectId">Идентификатор созданной заготовки объекта</param>
  /// <param name="isVersion">Признак - создавался новый объект или его версия</param>
  /// <param name="objectTypeID">Идентификатор типа созданного объекта</param>
  public ObjectCreatorCanceledEventArgs(long createdObjectId, bool isVersion, int objectTypeID)
  {
    this.createdZagId = createdObjectId;
    this.isVersion = isVersion;
    this.objectTypeID = objectTypeID;
  }

  /// <summary>Идентификатор созданной заготовки</summary>
  public long CreatedZagId
  {
    get => this.createdZagId;
    set
    {
      if (this.createdZagId == value)
        return;
      this.createdZagId = value;
    }
  }

  /// <summary>Признак - создается новый объект или его версия</summary>
  public bool IsVersion
  {
    get => this.isVersion;
    set
    {
      if (this.isVersion == value)
        return;
      this.isVersion = value;
    }
  }

  /// <summary>Идентификатор типа созданного объекта</summary>
  public int ObjectTypeID
  {
    get => this.objectTypeID;
    set
    {
      if (this.objectTypeID == value)
        return;
      this.objectTypeID = value;
    }
  }
}
