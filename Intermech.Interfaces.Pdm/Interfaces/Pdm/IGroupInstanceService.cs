// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Pdm.IGroupInstanceService
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Pdm;

/// <summary>
/// Интерфейс на класс для реализации функционала работы с групповыми изделиями
/// </summary>
public interface IGroupInstanceService
{
  /// <summary>Создана версия группового изделия</summary>
  /// <param name="session">Пользовательская сессия</param>
  /// <param name="dbObject">версия группового изделия</param>
  /// <param name="parentObject">версия изделия, от которого создается эта версия</param>
  void ArticleVersionCreated(IUserSession session, IDBObject dbObject, IDBObject parentObject);

  /// <summary>
  /// Блокирует автоматическое создание версии изделия при создании версии спецификации.
  /// Используется при создании версии спецификации в клиенте AVS в диалоговом режиме.
  /// </summary>
  /// <param name="ignoreSessionGuid">Идентификатор сессии, в которой выполняется блокировка</param>
  void AddIgnoreSessionGuid(Guid ignoreSessionGuid);

  /// <summary>
  /// Разблокирует автоматическое создание версии изделия при создании версии спецификации.
  /// Используется после создания версии спецификации в клиенте AVS в диалоговом режиме.
  /// </summary>
  /// <param name="ignoreSessionGuid">Идентификатор сессии, в которой была выполнена блокировка</param>
  void RemoveIgnoreSessionGuid(Guid ignoreSessionGuid);
}
