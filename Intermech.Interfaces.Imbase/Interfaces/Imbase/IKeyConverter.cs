// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Imbase.IKeyConverter
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Imbase;

/// <summary>
/// Интерфейс для преобразования старых ключей IMBASE в новые в таблицах базы данных.
/// </summary>
public interface IKeyConverter
{
  /// <summary>
  /// Список идентификаторов и наименований объектов, которые не удалось конвертировать.
  /// </summary>
  List<ObjectInfoForExteption> ConvertedInfo { get; }

  /// <summary>
  /// 
  /// </summary>
  bool IsFirstTaskComplete { get; }

  /// <summary>
  /// Состояние сервиса.
  ///  0 - не активен
  /// -1 - пауза
  /// -2 - завершен
  /// &gt; 0 - выполняется
  /// </summary>
  int State { get; }

  /// <summary>Процент выполнения.</summary>
  int Value { get; }

  /// <summary>Запускает сервис преобразования.</summary>
  /// <param name="sessionGuid">Идентификатор сессии</param>
  void Start(Guid sessionGuid);

  /// <summary>Останавливает сервис преобразования.</summary>
  void Stop();

  /// <summary>Переводит сервис в режим паузы.</summary>
  void Pause();

  /// <summary>конвертировать старый ключ в новый</summary>
  /// <param name="session"></param>
  /// <param name="oldKey"></param>
  /// <returns></returns>
  string ConvertOldKey(IUserSession session, string oldKey);
}
