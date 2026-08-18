// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Imbase.IImbaseRestructuringTablesService
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Imbase;

/// <summary>
/// 
/// </summary>
public interface IImbaseRestructuringTablesService
{
  /// <summary>
  /// Список идентификаторов объектов, которые не удалось синхронизировать.
  /// </summary>
  List<RestructuringTablesExteption> ExceptionInfo { get; }

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

  /// <summary>Переводит сервис в режим паузы.</summary>
  void Pause();

  /// <summary>Запуск сервиса синхронизации.</summary>
  /// <param name="userID">Идентификатор пользователя, производящего реструктуризацию</param>
  /// <param name="objID">Идентификатор выбранного объекта</param>
  /// <param name="settings">Добавляемые атрибуты и их настройки</param>
  void Start(long userID, long objID, List<RestructuringTablesAttrSettings> settings);

  /// <summary>Останавливает сервис преобразования.</summary>
  void Stop();
}
