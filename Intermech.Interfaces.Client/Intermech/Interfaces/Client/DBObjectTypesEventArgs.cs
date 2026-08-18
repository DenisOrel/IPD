// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.DBObjectTypesEventArgs
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
/// Список идентификаторов типов объектов, с которыми произошло некоторое событие
/// </summary>
[Serializable]
public class DBObjectTypesEventArgs : NotificationEventArgs, IDataMergingSupport
{
  /// <summary>Словарь идентификаторов типов объектов</summary>
  private IList<int> _objectTypeIDs;

  /// <summary>
  /// Подготовить список идентификаторов типов объектов, с которыми произошло некоторое событие
  /// </summary>
  /// <param name="eventName">Наименование события</param>
  /// <param name="objectTypeID">Идентификатор типа объекта</param>
  public DBObjectTypesEventArgs(string eventName, int objectTypeID)
    : this(eventName, (IList<int>) new int[1]
    {
      objectTypeID
    })
  {
  }

  /// <summary>
  /// Подготовить список идентификаторов типов объектов, с которыми произошло некоторое событие
  /// </summary>
  /// <param name="eventName">Наименование события</param>
  /// <param name="objectTypeID">Идентификатор типа объекта</param>
  /// <param name="firePrePostEvents">"Дёргать" дополнительные события "Перед основным событием" и "После основного события" у сервиса событий</param>
  public DBObjectTypesEventArgs(string eventName, int objectTypeID, bool firePrePostEvents)
    : this(eventName, (IList<int>) new int[1]
    {
      objectTypeID
    }, (firePrePostEvents ? 1 : 0) != 0)
  {
  }

  /// <summary>
  /// Создает новый экземпляр объекта с указанными именем события обновления и списком идентификаторов типов объектов
  /// </summary>
  /// <param name="eventName">Имя события обновления</param>
  /// <param name="objectTypeIDs">
  /// Список идентификаторов типов объектов. Может быть любым списком объектов,
  /// поддерживающим интерфейс IList и содержащим значения типа Int32.
  /// </param>
  public DBObjectTypesEventArgs(string eventName, IList<int> objectTypeIDs)
    : base(eventName)
  {
    this._objectTypeIDs = objectTypeIDs;
  }

  /// <summary>
  /// Создает новый экземпляр объекта с указанными именем события обновления и
  /// списком идентификаторов типов объектов.
  /// </summary>
  /// <param name="eventName">Имя события обновления.</param>
  /// <param name="objectTypeIDs">
  /// Список идентификаторов типов объектов. Может быть любым объектов,
  /// поддерживающим интерфейс IList и содержащим значения типа Int32.
  /// </param>
  /// <param name="firePrePostEvents">"Дёргать" дополнительные события "Перед основным событием" и "После основного события" у сервиса событий</param>
  public DBObjectTypesEventArgs(string eventName, IList<int> objectTypeIDs, bool firePrePostEvents)
    : base(eventName, firePrePostEvents)
  {
    this._objectTypeIDs = objectTypeIDs;
  }

  /// <summary>
  /// Возвращает список идентификаторов типов объектов, с которыми произошло событие
  /// </summary>
  public IList<int> ObjectTypeIDs
  {
    [DebuggerStepThrough] get => this._objectTypeIDs;
  }

  /// <summary>
  /// Объединяет данные этого объекта с данными указанного объекта. После успешного объединения другой
  /// объект будет больше не нужен.
  /// </summary>
  /// <param name="obj">Объект, чьи данные должны быть объединены с данными этого объекта</param>
  /// <returns>true, если объединение было успешным, в противном случае - false</returns>
  public bool MergeWith(object obj)
  {
    if (!(obj is DBObjectTypesEventArgs objectTypesEventArgs))
      return false;
    List<int> intList = new List<int>((IEnumerable<int>) this._objectTypeIDs);
    for (int index = 0; index < objectTypesEventArgs._objectTypeIDs.Count; ++index)
    {
      int objectTypeId = objectTypesEventArgs._objectTypeIDs[index];
      if (!intList.Contains(objectTypeId))
        intList.Add(objectTypeId);
    }
    this._objectTypeIDs = (IList<int>) intList;
    return true;
  }

  /// <summary>Количество заданий в аргументах</summary>
  public override int ItemsCount
  {
    get
    {
      int num = 0;
      if (this._objectTypeIDs != null)
        num += this._objectTypeIDs.Count;
      return num <= 0 ? base.ItemsCount : num;
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
