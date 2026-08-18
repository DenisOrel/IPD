// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.IRecentObjectsService
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.DataFormats;
using System;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Служба, позволяющая управлять недавними объектами</summary>
public interface IRecentObjectsService
{
  /// <summary>
  /// Добавить/заменить существующий объект
  /// (метод потокобезопасен)
  /// </summary>
  /// <param name="objectID">ID версии объекта</param>
  /// <param name="action">Действие, выполненное над объектом</param>
  /// <param name="date">Дата и время (UTC) выполнения этого действия</param>
  /// <returns>Вновь добавленный или существующий объект</returns>
  void Add(long objectID, ObjectAction action, DateTime date);

  /// <summary>
  /// Добавить/заменить существующие объекты
  /// (метод потокобезопасен)
  /// </summary>
  /// <param name="objectVersionIds">ID версий объектов</param>
  /// <param name="action">Действие, выполненное над объектами</param>
  /// <param name="date">Дата и время (UTC) выполнения этого действия</param>
  void Add(long[] objectID, ObjectAction action, DateTime date);
}
