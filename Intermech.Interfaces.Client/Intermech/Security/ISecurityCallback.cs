// Decompiled with JetBrains decompiler
// Type: Intermech.Security.ISecurityCallback
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Interfaces;
using System;

#nullable disable
namespace Intermech.Security;

/// <summary>
/// Интерфейс получения информации связанной с настройками безопасности
/// Обслуживает, по сути, категорию (типы атрибутов, объектов, объекты).
/// </summary>
public interface ISecurityCallback
{
  /// <summary>Получение интерфейса IDBSecurity по некоторому id</summary>
  /// <param name="session">сессия</param>
  /// <param name="id">идентификатор, достаточный обработчику для выдачи результата IDBSecurity</param>
  /// <returns></returns>
  IDBSecurity GetSecurity(IUserSession session, object id);

  /// <summary>Обслуживаемая категория</summary>
  int MaintainedCategory { get; }

  /// <summary>
  /// Доп информация о применяемости. Обычно null.
  /// Но например для MaintainedCategory = Consts.CategoryLCStep может быть в item1=Consts.CategoryObjectType, item2=(int)objectTypeId -
  /// то есть шаг назначен в схему применительно к типу объекта
  /// </summary>
  Tuple<int, object> Applicability { get; }
}
