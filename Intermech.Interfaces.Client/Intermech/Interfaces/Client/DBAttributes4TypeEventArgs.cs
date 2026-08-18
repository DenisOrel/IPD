// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.DBAttributes4TypeEventArgs
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Список идентификаторов атрибутов, с которыми произошло некоторое событие, для типов объектов/связей
/// </summary>
[Serializable]
public class DBAttributes4TypeEventArgs : NotificationEventArgs
{
  /// <summary>
  /// Тип объекта или связи, с которым (которой) произошло событие
  /// </summary>
  public int CategoryID = -1;
  /// <summary>Список добавленных атрибутов</summary>
  public IList<int> AddedIDs;
  /// <summary>Список изменённых атрибутов</summary>
  public IList<int> ChangedIDs;
  /// <summary>Список удалённых атрибутов</summary>
  public IList<int> RemovedIDs;

  /// <summary>
  /// Подготовить список идентификаторов атрибутов для типа объекта/связи
  /// </summary>
  /// <param name="eventName">Наименование события</param>
  /// <param name="categoryID">Идентификатор типа объекта или связи</param>
  /// <param name="addedIDs">Список добавленных атрибутов</param>
  /// <param name="changedIDs">Список изменённых атрибутов</param>
  /// <param name="removedIDs">Список удалённых атрибутов</param>
  public DBAttributes4TypeEventArgs(
    string eventName,
    int categoryID,
    IList<int> addedIDs,
    IList<int> changedIDs,
    IList<int> removedIDs)
    : base(eventName)
  {
    this.CategoryID = categoryID;
    this.AddedIDs = addedIDs;
    this.ChangedIDs = changedIDs;
    this.RemovedIDs = removedIDs;
  }

  /// <summary>
  /// Подготовить список идентификаторов атрибутов для типа объекта/связи
  /// </summary>
  /// <param name="eventName">Наименование события</param>
  /// <param name="categoryID">Идентификатор типа объекта или связи</param>
  /// <param name="addedIDs">Список добавленных атрибутов</param>
  /// <param name="changedIDs">Список изменённых атрибутов</param>
  /// <param name="removedIDs">Список удалённых атрибутов</param>
  /// <param name="firePrePostEvents">"Дёргать" дополнительные события "Перед основным событием" и "После основного события" у сервиса событий</param>
  public DBAttributes4TypeEventArgs(
    string eventName,
    int categoryID,
    IList<int> addedIDs,
    IList<int> changedIDs,
    IList<int> removedIDs,
    bool firePrePostEvents)
    : base(eventName, firePrePostEvents)
  {
    this.CategoryID = categoryID;
    this.AddedIDs = addedIDs;
    this.ChangedIDs = changedIDs;
    this.RemovedIDs = removedIDs;
  }

  /// <summary>Количество заданий в аргументах</summary>
  public override int ItemsCount
  {
    get
    {
      int num = 0;
      if (this.AddedIDs != null)
        num += this.AddedIDs.Count;
      if (this.ChangedIDs != null)
        num += this.ChangedIDs.Count;
      if (this.RemovedIDs != null)
        num += this.RemovedIDs.Count;
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
