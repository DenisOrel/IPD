// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.DBAttributesEventArgs
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
/// Список идентификаторов атрибутов, с которыми произошло некоторое событие
/// </summary>
[Serializable]
public class DBAttributesEventArgs : NotificationEventArgs, IDataMergingSupport
{
  /// <summary>Словарь идентификаторов атрибутов</summary>
  private IList<int> _attributeIDs;

  /// <summary>
  /// Подготовить список идентификаторов атрибутов, с которыми произошло некоторое событие
  /// </summary>
  /// <param name="eventName">Наименование события</param>
  /// <param name="attributeID">Идентификатор атрибута</param>
  public DBAttributesEventArgs(string eventName, int attributeID)
    : this(eventName, (IList<int>) new int[1]{ attributeID })
  {
  }

  /// <summary>
  /// Подготовить список идентификаторов атрибутов, с которыми произошло некоторое событие
  /// </summary>
  /// <param name="eventName">Наименование события</param>
  /// <param name="attributeID">Идентификатор атрибута</param>
  /// <param name="firePrePostEvents">"Дёргать" дополнительные события "Перед основным событием" и "После основного события" у сервиса событий</param>
  public DBAttributesEventArgs(string eventName, int attributeID, bool firePrePostEvents)
    : this(eventName, (IList<int>) new int[1]{ attributeID }, (firePrePostEvents ? 1 : 0) != 0)
  {
  }

  /// <summary>
  /// Создает новый экземпляр объекта с указанными именем события обновления и списком идентификаторов атрибутов
  /// </summary>
  /// <param name="eventName">Имя события обновления</param>
  /// <param name="attributeIDs">
  /// Список идентификаторов атрибутов. Может быть любым списком объектов,
  /// поддерживающим интерфейс IList и содержащим значения типа Int32.
  /// </param>
  public DBAttributesEventArgs(string eventName, IList<int> attributeIDs)
    : base(eventName)
  {
    this._attributeIDs = attributeIDs;
  }

  /// <summary>
  /// Создает новый экземпляр объекта с указанными именем события обновления и
  /// списком идентификаторов атрибутов.
  /// </summary>
  /// <param name="eventName">Имя события обновления.</param>
  /// <param name="attributeIDs">
  /// Список идентификаторов атрибутов. Может быть любым объектов,
  /// поддерживающим интерфейс IList и содержащим значения типа Int32.
  /// </param>
  /// <param name="firePrePostEvents">"Дёргать" дополнительные события "Перед основным событием" и "После основного события" у сервиса событий</param>
  public DBAttributesEventArgs(string eventName, IList<int> attributeIDs, bool firePrePostEvents)
    : base(eventName, firePrePostEvents)
  {
    this._attributeIDs = attributeIDs;
  }

  /// <summary>
  /// Возвращает список идентификаторов атрибутов, с которыми произошло событие
  /// </summary>
  public IList<int> AttributeIDs
  {
    [DebuggerStepThrough] get => this._attributeIDs;
  }

  /// <summary>
  /// Объединяет данные этого объекта с данными указанного объекта. После успешного объединения другой
  /// объект будет больше не нужен.
  /// </summary>
  /// <param name="obj">Объект, чьи данные должны быть объединены с данными этого объекта</param>
  /// <returns>true, если объединение было успешным, в противном случае - false</returns>
  public virtual bool MergeWith(object obj)
  {
    if (!(obj is DBAttributesEventArgs attributesEventArgs))
      return false;
    List<int> intList = new List<int>((IEnumerable<int>) this._attributeIDs);
    for (int index = 0; index < attributesEventArgs._attributeIDs.Count; ++index)
    {
      int attributeId = attributesEventArgs._attributeIDs[index];
      if (!intList.Contains(attributeId))
        intList.Add(attributeId);
    }
    this._attributeIDs = (IList<int>) intList;
    return true;
  }

  /// <summary>Количество заданий в аргументах</summary>
  public override int ItemsCount
  {
    get
    {
      int num = 0;
      if (this._attributeIDs != null)
        num += this._attributeIDs.Count;
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
