// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.DBObjectsManagedEventArgs
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Список идентификаторов версий объектов, с которыми произошло некоторое управляемое событие
/// </summary>
[Serializable]
public class DBObjectsManagedEventArgs : DBObjectsEventArgs
{
  /// <summary>Допускается ли данное событие к обработке</summary>
  public bool AcceptEvent = true;

  /// <summary>
  /// Подготовить список идентификаторов объектов, с которыми произошло некоторое событие
  /// </summary>
  /// <param name="eventName">Наименование события</param>
  /// <param name="objectID">Идентификатор объекта</param>
  /// <param name="AnAcceptEvent">Допускается ли данное событие к обработке</param>
  public DBObjectsManagedEventArgs(string eventName, long objectID, bool AnAcceptEvent)
    : base(eventName, objectID)
  {
    this.AcceptEvent = AnAcceptEvent;
  }

  /// <summary>
  /// Подготовить список идентификаторов объектов, с которыми произошло некоторое событие
  /// </summary>
  /// <param name="eventName">Наименование события</param>
  /// <param name="objectID">Идентификатор объекта</param>
  /// <param name="objectTypeID">Идентификатор типа объекта</param>
  /// <param name="AnAcceptEvent">Допускается ли данное событие к обработке</param>
  public DBObjectsManagedEventArgs(
    string eventName,
    long objectID,
    int objectTypeID,
    bool AnAcceptEvent)
    : base(eventName, objectID, objectTypeID)
  {
    this.AcceptEvent = AnAcceptEvent;
  }

  /// <summary>
  /// Подготовить список идентификаторов объектов, с которыми произошло некоторое событие
  /// </summary>
  /// <param name="eventName">Наименование события</param>
  /// <param name="objectID">Идентификатор объекта</param>
  /// <param name="objectTypeID">Идентификатор типа объекта</param>
  /// <param name="firePrePostEvents">"Дёргать" дополнительные события "Перед основным событием" и "После основного события" у сервиса событий</param>
  /// <param name="AnAcceptEvent">Допускается ли данное событие к обработке</param>
  public DBObjectsManagedEventArgs(
    string eventName,
    long objectID,
    int objectTypeID,
    bool firePrePostEvents,
    bool AnAcceptEvent)
    : base(eventName, objectID, objectTypeID, firePrePostEvents)
  {
    this.AcceptEvent = AnAcceptEvent;
  }

  /// <summary>
  /// Создает новый экземпляр объекта с указанными именем события обновления и списком идентификаторов версий объектов
  /// </summary>
  /// <param name="eventName">Имя события обновления</param>
  /// <param name="objectIDs">
  /// Список идентификаторов версий объектов. Может быть любым объектов,
  /// поддерживающим интерфейс IList и содержащим значения типа Int64.
  /// </param>
  /// <param name="objectTypeIDs">Список идентификаторов типов объектов</param>
  /// <param name="AnAcceptEvent">Допускается ли данное событие к обработке</param>
  public DBObjectsManagedEventArgs(
    string eventName,
    IList<long> objectIDs,
    IList<int> objectTypeIDs,
    bool AnAcceptEvent)
    : base(eventName, objectIDs, objectTypeIDs)
  {
    this.AcceptEvent = AnAcceptEvent;
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
  /// <param name="objectTypeIDs">Список идентификаторов типов объектов</param>
  /// <param name="firePrePostEvents">"Дёргать" дополнительные события "Перед основным событием" и "После основного события" у сервиса событий</param>
  /// <param name="AnAcceptEvent">Допускается ли данное событие к обработке</param>
  public DBObjectsManagedEventArgs(
    string eventName,
    IList<long> objectIDs,
    IList<int> objectTypeIDs,
    bool firePrePostEvents,
    bool AnAcceptEvent)
    : base(eventName, objectIDs, objectTypeIDs, firePrePostEvents)
  {
    this.AcceptEvent = AnAcceptEvent;
  }

  /// <summary>
  /// Объединяет данные этого объекта с данными указанного объекта. После успешного объединения другой
  /// объект будет больше не нужен.
  /// </summary>
  /// <param name="obj">Объект, чьи данные должны быть объединены с данными этого объекта</param>
  /// <returns>true, если объединение было успешным, в противном случае - false</returns>
  public override bool MergeWith(object obj)
  {
    return obj is DBObjectsManagedEventArgs managedEventArgs && managedEventArgs.AcceptEvent == this.AcceptEvent && base.MergeWith(obj);
  }
}
