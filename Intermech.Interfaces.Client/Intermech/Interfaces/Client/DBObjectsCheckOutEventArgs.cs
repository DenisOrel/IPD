// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.DBObjectsCheckOutEventArgs
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Событие, возникающее при взятии объекта на изменение</summary>
[Serializable]
public class DBObjectsCheckOutEventArgs : DBObjectsEventArgs, ICriticalEventArgs
{
  /// <summary>Коллекция новых идентификаторов объектов</summary>
  private IList<long> _newObjectIDs;

  /// <summary>
  /// Создает новый экземпляр объекта с указанными именем события обновления и
  /// списком идентификаторов версий объектов.
  /// </summary>
  /// <param name="eventName">Имя события обновления.</param>
  /// <param name="objectIDs">
  /// Список идентификаторов версий объектов. Может быть любым объектом,
  /// поддерживающим интерфейс IList и содержащим значения типа Int64.
  /// </param>
  /// <param name="newObjectIDs">
  /// Список новых идентификаторов версий объектов. Может быть любым объектом,
  /// поддерживающим интерфейс IList и содержащим значения типа Int64.
  /// </param>
  public DBObjectsCheckOutEventArgs(
    string eventName,
    IList<long> objectIDs,
    IList<long> newObjectIDs)
    : base(eventName, objectIDs)
  {
    this._newObjectIDs = newObjectIDs;
  }

  /// <summary>
  /// Создает новый экземпляр объекта с указанными именем события обновления и
  /// списком идентификаторов версий объектов.
  /// </summary>
  /// <param name="eventName">Имя события обновления.</param>
  /// <param name="objectIDs">
  /// Список идентификаторов версий объектов. Может быть любым объектом,
  /// поддерживающим интерфейс IList и содержащим значения типа Int64.
  /// </param>
  /// <param name="newObjectIDs">
  /// Список новых идентификаторов версий объектов. Может быть любым объектом,
  /// поддерживающим интерфейс IList и содержащим значения типа Int64.
  /// </param>
  /// <param name="firePrePostEvents">"Дёргать" дополнительные события "Перед основным событием" и "После основного события" у сервиса событий</param>
  public DBObjectsCheckOutEventArgs(
    string eventName,
    IList<long> objectIDs,
    IList<long> newObjectIDs,
    bool firePrePostEvents)
    : base(eventName, objectIDs, firePrePostEvents)
  {
    this._newObjectIDs = newObjectIDs;
  }

  /// <summary>
  /// Возвращает список идентификаторов новых версий объектов, с которыми произошло событие.
  /// </summary>
  public IList<long> NewObjectIDs => this._newObjectIDs;

  /// <summary>
  /// Объединяет данные этого объекта с данными указанного объекта. После успешного объединения другой
  /// объект будет больше не нужен.
  /// </summary>
  /// <param name="obj">Объект, чьи данные должны быть объединены с данными этого объекта</param>
  /// <returns>true, если объединение было успешным, в противном случае - false</returns>
  public override bool MergeWith(object obj)
  {
    if (!(obj is DBObjectsCheckOutEventArgs checkOutEventArgs))
      return false;
    List<long> longList1 = new List<long>((IEnumerable<long>) this._objectIDs);
    List<long> longList2 = new List<long>((IEnumerable<long>) this._newObjectIDs);
    for (int index1 = 0; index1 < checkOutEventArgs.ObjectIDs.Count; ++index1)
    {
      long objectId = checkOutEventArgs.ObjectIDs[index1];
      long newObjectId = checkOutEventArgs.NewObjectIDs[index1];
      int index2 = longList1.IndexOf(objectId);
      if (index2 >= 0)
      {
        if (longList2[index2] != newObjectId)
          return false;
      }
      else
      {
        longList1.Add(objectId);
        longList2.Add(newObjectId);
      }
    }
    this._objectIDs = (IList<long>) longList1;
    this._newObjectIDs = (IList<long>) longList2;
    return true;
  }

  /// <summary>
  /// Проверить, является ли событие критическим благодаря указанным аргументам
  /// </summary>
  public override bool IsCritical => true;
}
