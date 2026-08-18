// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.DBObjectsEventArgs
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Список идентификаторов версий объектов, с которыми произошло некоторое событие
/// </summary>
[Serializable]
public class DBObjectsEventArgs : NotificationEventArgs, IDataMergingSupport, ICriticalEventArgs
{
  /// <summary>Словарь идентификаторов объектов</summary>
  protected IList<long> _objectIDs;
  /// <summary>Словарь идентификаторов типов созданных объектов</summary>
  protected IList<int> _objectTypeIDs;
  /// <summary>Разновидность записи об объектах</summary>
  protected ObjectRecordKind _verType;

  /// <summary>
  /// Подготовить список идентификаторов объектов, с которыми произошло некоторое событие
  /// </summary>
  /// <param name="eventName">Наименование события</param>
  /// <param name="objectID">Идентификатор объекта</param>
  public DBObjectsEventArgs(string eventName, long objectID)
    : this(eventName, (IList<long>) new long[1]{ objectID }, (IList<int>) new int[1]
    {
      -1
    })
  {
  }

  /// <summary>
  /// Подготовить список идентификаторов объектов, с которыми произошло некоторое событие
  /// </summary>
  /// <param name="eventName">Наименование события</param>
  /// <param name="objectID">Идентификатор объекта</param>
  /// <param name="objectTypeID">Идентификатор типа объекта (если тип неизвестен, можно подсунуть Intermech.Consts.UnknownObjectTypeId)</param>
  public DBObjectsEventArgs(string eventName, long objectID, int objectTypeID)
    : this(eventName, (IList<long>) new long[1]{ objectID }, (IList<int>) new int[1]
    {
      objectTypeID
    })
  {
  }

  /// <summary>
  /// Подготовить список идентификаторов объектов, с которыми произошло некоторое событие
  /// </summary>
  /// <param name="eventName">Наименование события</param>
  /// <param name="objectID">Идентификатор объекта</param>
  /// <param name="firePrePostEvents">"Дёргать" дополнительные события "Перед основным событием" и "После основного события" у сервиса событий</param>
  public DBObjectsEventArgs(string eventName, long objectID, bool firePrePostEvents)
    : this(eventName, (IList<long>) new long[1]{ objectID }, (IList<int>) new int[1]
    {
      -1
    }, (firePrePostEvents ? 1 : 0) != 0)
  {
  }

  /// <summary>
  /// Подготовить список идентификаторов объектов, с которыми произошло некоторое событие
  /// </summary>
  /// <param name="eventName">Наименование события</param>
  /// <param name="objectID">Идентификатор объекта</param>
  /// <param name="objectTypeID">Идентификатор типа объекта (если тип неизвестен, можно подсунуть Intermech.Consts.UnknownObjectTypeId)</param>
  /// <param name="firePrePostEvents">"Дёргать" дополнительные события "Перед основным событием" и "После основного события" у сервиса событий</param>
  public DBObjectsEventArgs(
    string eventName,
    long objectID,
    int objectTypeID,
    bool firePrePostEvents)
    : this(eventName, (IList<long>) new long[1]{ objectID }, (IList<int>) new int[1]
    {
      objectTypeID
    }, (firePrePostEvents ? 1 : 0) != 0)
  {
  }

  /// <summary>
  /// Создает новый экземпляр объекта с указанными именем события обновления и списком идентификаторов версий объектов
  /// </summary>
  /// <param name="eventName">Имя события обновления</param>
  /// <param name="objectIDs">
  /// Список идентификаторов версий объектов. Может быть любым объектов,
  /// поддерживающим интерфейс IList и содержащим значения типа Int64.
  /// </param>
  public DBObjectsEventArgs(string eventName, IList<long> objectIDs)
    : base(eventName)
  {
    this._objectIDs = objectIDs;
    this._objectTypeIDs = this._objectIDs != null ? (IList<int>) new List<int>(this._objectIDs.Count) : (IList<int>) null;
    if (this._objectIDs != null)
    {
      for (int index = 0; index < this._objectIDs.Count; ++index)
        this._objectTypeIDs.Add(-1);
    }
    this.ExcludeDuplicates();
  }

  /// <summary>
  /// Создает новый экземпляр объекта с указанными именем события обновления и списком идентификаторов версий объектов
  /// </summary>
  /// <param name="eventName">Имя события обновления</param>
  /// <param name="objectIDs">
  /// Список идентификаторов версий объектов. Может быть любым объектов,
  /// поддерживающим интерфейс IList и содержащим значения типа Int64.
  /// </param>
  /// <param name="objectTypeIDs">Список идентификаторов типов созданных объектов (если типы неизвестен, можно подсунуть Intermech.Consts.UnknownObjectTypeId)</param>
  public DBObjectsEventArgs(string eventName, IList<long> objectIDs, IList<int> objectTypeIDs)
    : base(eventName)
  {
    this._objectIDs = objectIDs;
    this._objectTypeIDs = objectTypeIDs;
    this.ExcludeDuplicates();
  }

  /// <summary>
  /// Создает новый экземпляр объекта с указанными именем события обновления и
  /// списком идентификаторов версий объектов.
  /// </summary>
  /// <param name="eventName">Имя события обновления.</param>
  /// <param name="objectIDs">
  /// Список идентификаторов версий объектов. Может быть любым объектов,
  /// поддерживающим интерфейс IList и содержащим значения типа Int64.
  /// </param>
  /// <param name="firePrePostEvents">"Дёргать" дополнительные события "Перед основным событием" и "После основного события" у сервиса событий</param>
  public DBObjectsEventArgs(string eventName, IList<long> objectIDs, bool firePrePostEvents)
    : base(eventName, firePrePostEvents)
  {
    this._objectIDs = objectIDs;
    this._objectTypeIDs = this._objectIDs != null ? (IList<int>) new List<int>(this._objectIDs.Count) : (IList<int>) null;
    if (this._objectIDs != null)
    {
      for (int index = 0; index < this._objectIDs.Count; ++index)
        this._objectTypeIDs.Add(-1);
    }
    this.ExcludeDuplicates();
  }

  /// <summary>
  /// Создает новый экземпляр объекта с указанными именем события обновления и
  /// списком идентификаторов версий объектов.
  /// </summary>
  /// <param name="eventName">Имя события обновления.</param>
  /// <param name="objectIDs">
  /// Список идентификаторов версий объектов. Может быть любым объектов,
  /// поддерживающим интерфейс IList и содержащим значения типа Int64.
  /// </param>
  /// <param name="objectTypeIDs">Список идентификаторов типов созданных объектов (если типы неизвестен, можно подсунуть Intermech.Consts.UnknownObjectTypeId)</param>
  /// <param name="firePrePostEvents">"Дёргать" дополнительные события "Перед основным событием" и "После основного события" у сервиса событий</param>
  public DBObjectsEventArgs(
    string eventName,
    IList<long> objectIDs,
    IList<int> objectTypeIDs,
    bool firePrePostEvents)
    : base(eventName, firePrePostEvents)
  {
    this._objectIDs = objectIDs;
    this._objectTypeIDs = objectTypeIDs;
    this.ExcludeDuplicates();
  }

  /// <summary>Разновидность записи об объектах</summary>
  public ObjectRecordKind VerType
  {
    [DebuggerStepThrough] get => this._verType;
    set => this._verType = value;
  }

  /// <summary>
  /// Возвращает список идентификаторов версий объектов, с которыми произошло событие
  /// </summary>
  public IList<long> ObjectIDs
  {
    [DebuggerStepThrough] get => this._objectIDs;
  }

  /// <summary>
  /// Список идентификаторов типов созданных объектов (если типы неизвестен, можно подсунуть Intermech.Consts.UnknownObjectTypeId)
  /// </summary>
  public IList<int> ObjectTypeIDs
  {
    [DebuggerStepThrough] get => this._objectTypeIDs;
  }

  /// <summary>Количество заданий в аргументах</summary>
  public override int ItemsCount
  {
    get
    {
      int num = 0;
      if (this._objectIDs != null)
        num += this._objectIDs.Count;
      return num <= 0 ? base.ItemsCount : num;
    }
  }

  /// <summary>
  /// Объединяет данные этого объекта с данными указанного объекта. После успешного объединения другой
  /// объект будет больше не нужен.
  /// </summary>
  /// <param name="obj">Объект, чьи данные должны быть объединены с данными этого объекта</param>
  /// <returns>true, если объединение было успешным, в противном случае - false</returns>
  public virtual bool MergeWith(object obj)
  {
    if (!(obj is DBObjectsEventArgs objectsEventArgs))
      return false;
    List<long> longList = new List<long>((IEnumerable<long>) this._objectIDs);
    List<int> intList = new List<int>((IEnumerable<int>) this._objectTypeIDs);
    for (int index = 0; index < objectsEventArgs._objectIDs.Count; ++index)
    {
      long objectId = objectsEventArgs._objectIDs[index];
      int objectTypeId = objectsEventArgs._objectTypeIDs[index];
      if (!longList.Contains(objectId))
      {
        longList.Add(objectId);
        intList.Add(objectTypeId);
      }
    }
    this._objectIDs = (IList<long>) longList;
    this._objectTypeIDs = (IList<int>) intList;
    this.ExcludeDuplicates();
    return true;
  }

  /// <summary>Исключить дублирующиеся идентификаторы из списка</summary>
  protected void ExcludeDuplicates()
  {
    if (this._objectIDs == null || this._objectIDs.Count == 0)
      return;
    List<long> longList = new List<long>();
    List<int> intList = new List<int>();
    for (int index = 0; index < this._objectIDs.Count; ++index)
    {
      if (!longList.Contains(this._objectIDs[index]))
      {
        longList.Add(this._objectIDs[index]);
        intList.Add(this._objectTypeIDs[index]);
      }
    }
    this._objectIDs = (IList<long>) longList;
    this._objectTypeIDs = (IList<int>) intList;
  }

  /// <summary>
  /// Проверить, является ли событие критическим благодаря указанным аргументам
  /// </summary>
  public virtual bool IsCritical
  {
    get
    {
      if (UISettings.AutoupdateNonActiveWindows && this.EventName == "ObjectsCreated")
        return true;
      if (this._objectIDs == null || this._objectTypeIDs == null || this._objectIDs.Count == 0 || this._objectIDs.Count != this._objectTypeIDs.Count)
        return false;
      for (int index = 0; index < this._objectTypeIDs.Count; ++index)
      {
        if (this._objectTypeIDs[index] == -1)
          return false;
      }
      return true;
    }
  }

  /// <summary>
  /// Проверить, поддерживается ли указанный режим оптимизации аргументами события и,
  /// в случае необходимости, вернуть максимальный уровень поддерживаемой оптимизации
  /// </summary>
  /// <param name="mode">Запрашиваемый режим оптимизации</param>
  /// <returns>Допустимый режим оптимизации</returns>
  public override NotificationServiceMode GetSupportedOptimization(NotificationServiceMode mode)
  {
    return mode;
  }
}
