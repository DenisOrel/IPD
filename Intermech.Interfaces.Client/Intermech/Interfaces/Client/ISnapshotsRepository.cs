// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.ISnapshotsRepository
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Diagnostics;
using Intermech.Interfaces.Snapshots;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Интерфейс репозитория итераций объектов</summary>
public interface ISnapshotsRepository : IRepository<long, IDBObjectSnapshot>
{
  /// <summary>Конструктор экземляров итераций.</summary>
  /// <param name="snapshotID">Идентификатор итерации</param>
  /// <param name="preLoadAttributes">Список флагов, показывающих какие наборы атрибутов должны быть закэшированы ещё при создании</param>
  /// <param name="failIfNotFound">Если Тrue, то в случае недоступности серверного интерфейса итерации выбросится исключительная ситуация</param>
  /// <returns>Созданная итерация</returns>
  [ContractAnnotation("failIfNotFound:false => CanBeNull; => NotNull")]
  ISnapshot Create(long snapshotID, SnapshotAttributes preLoadAttributes = SnapshotAttributes.Default, bool failIfNotFound = true);

  /// <summary>Конструктор экземпляров итераций.</summary>
  /// <param name="snapshotID">Идентификатор итерации</param>
  /// <param name="snapshot">[out] Созданная итерация</param>
  /// <param name="preLoadAttributes">Список флагов, показывающих какие наборы атрибутов должны быть закэшированы ещё при создании</param>
  /// <returns>True, если создание прошло успешно</returns>
  [ContractAnnotation("=> true, snapshot: notnull; => false, snapshot: null")]
  bool TryCreate(long snapshotID, out ISnapshot snapshot, SnapshotAttributes preLoadAttributes = SnapshotAttributes.Default);
}
