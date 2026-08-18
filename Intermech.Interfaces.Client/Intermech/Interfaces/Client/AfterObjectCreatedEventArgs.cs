// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.AfterObjectCreatedEventArgs
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Аргументы для события, возникающего после успешного завершения создания нового объекта
/// </summary>
public sealed class AfterObjectCreatedEventArgs
{
  /// <summary>Поле для хранения идентификатора созданного объекта</summary>
  protected long objectId = -1;
  /// <summary>
  /// Поле для хранения прототипа по которому создается объект
  /// </summary>
  protected long prototypeId = -1;
  /// <summary>
  /// Поле для хранения признака - создается новый объект или его версия
  /// </summary>
  protected bool isVersion;
  /// <summary>Поле для хранения значения RunEditor</summary>
  protected bool runEditor;
  /// <summary>Идентификатор типа созданного объекта</summary>
  protected int objectTypeID = -1;

  /// <summary>Конструктор</summary>
  /// <param name="objectId">Идентификатор созданного объекта</param>
  /// <param name="runEditor">Признак - нужно ли запускать редактор по завершению создания объекта</param>
  public AfterObjectCreatedEventArgs(long objectId, bool runEditor)
    : this(objectId, runEditor, -1L)
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="objectId">Идентификатор созданного объекта</param>
  /// <param name="runEditor">Признак - нужно ли запускать редактор по завершению создания объекта</param>
  /// <param name="prototypeId">Идентификатор прототипа по которому создается объект</param>
  public AfterObjectCreatedEventArgs(long objectId, bool runEditor, long prototypeId)
    : this(objectId, runEditor, prototypeId, false, -1)
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="objectId">Идентификатор созданного объекта</param>
  /// <param name="runEditor">Признак - нужно ли запускать редактор по завершению создания объекта</param>
  /// <param name="prototypeId">Идентификатор прототипа по которому создается объект</param>
  /// <param name="isVersion">Признак - создается новый объект или его версия</param>
  public AfterObjectCreatedEventArgs(
    long objectId,
    bool runEditor,
    long prototypeId,
    bool isVersion)
    : this(objectId, runEditor, prototypeId, isVersion, -1)
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="objectId">Идентификатор созданного объекта</param>
  /// <param name="runEditor">Признак - нужно ли запускать редактор по завершению создания объекта</param>
  /// <param name="prototypeId">Идентификатор прототипа по которому создается объект</param>
  /// <param name="isVersion">Признак - создается новый объект или его версия</param>
  /// <param name="objectTypeID">Идентификатор типа созданного объекта</param>
  public AfterObjectCreatedEventArgs(
    long objectId,
    bool runEditor,
    long prototypeId,
    bool isVersion,
    int objectTypeID)
  {
    this.objectId = objectId;
    this.runEditor = runEditor;
    this.prototypeId = prototypeId;
    this.isVersion = isVersion;
    this.objectTypeID = objectTypeID;
  }

  /// <summary>Идентификатор созданного объекта</summary>
  public long ObjectID
  {
    get => this.objectId;
    set
    {
      if (this.objectId == value)
        return;
      this.objectId = value;
    }
  }

  /// <summary>Идентификатор прототипа по которому создается объект</summary>
  public long PrototypeId
  {
    get => this.prototypeId;
    set
    {
      if (this.prototypeId == value)
        return;
      this.prototypeId = value;
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

  /// <summary>
  /// Признак - нужно ли запускать редактор по завершению создания объекта
  /// </summary>
  public bool RunEditor
  {
    get => this.runEditor;
    set
    {
      if (this.runEditor == value)
        return;
      this.runEditor = value;
    }
  }

  /// <summary>Идентификатор типа созданного объекта</summary>
  public int ObjectTypeID
  {
    get => this.objectTypeID;
    set => this.objectTypeID = this.objectTypeID != value ? value : this.objectTypeID;
  }
}
