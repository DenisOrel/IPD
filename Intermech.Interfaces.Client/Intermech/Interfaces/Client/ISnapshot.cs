// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.ISnapshot
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Diagnostics;
using Intermech.Interfaces.Snapshots;
using System;
using System.Data;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Интерфейс итерации</summary>
public interface ISnapshot : IEntity<long, IDBObjectSnapshot>
{
  /// <summary>Имя</summary>
  [NotNull]
  string Name { get; }

  /// <summary>Дата и время последней модификации</summary>
  DateTime ModifyDate { get; }

  /// <summary>Владелец</summary>
  long OwnerID { get; }

  /// <summary>Идентификатор головного объекта</summary>
  long RootObjectID { get; }

  /// <summary>Идентификатор версии головного объекта</summary>
  long RootObjectVersionID { get; }

  /// <summary>Таблица дополнительных атрибутов корневого объекта</summary>
  DataTable RootObjectAttributes { get; }

  /// <summary>Проверить входит ли версия объекта в итерацию.</summary>
  /// <param name="objectVerID">Идентификатор версии объекта</param>
  /// <returns>True если данный объект входит в итерацию</returns>
  bool ObjectInSnapshot(long objectVerID);

  /// <summary>Получить таблицу дополнительных атрибутов объекта, включённого в итерацию.</summary>
  /// <param name="objectVerID">Идентификатор версии объекта</param>
  /// <param name="failIfNotFound">Выбрасывать ли исключение если объект с переданным идентификатором не включён в итерацию</param>
  /// <returns>Таблица дополнительных атрибутов объекта, включённого в итерацию</returns>
  DataTable GetObjectInSnapshotAttributes(long objectVerID, bool failIfNotFound = true);
}
