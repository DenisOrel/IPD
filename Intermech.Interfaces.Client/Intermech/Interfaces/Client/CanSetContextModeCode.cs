// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.CanSetContextModeCode
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Результат проверки, можно ли установить режим автоматического обновления для контекста редактирования
/// </summary>
[Serializable]
public enum CanSetContextModeCode
{
  /// <summary>Контекст взят на изменение другим пользователем</summary>
  CheckedOutByOtherUser = -6, // 0xFFFFFFFA
  /// <summary>Права доступа запрещают редактирование</summary>
  ReadOnlyByAccessRights = -5, // 0xFFFFFFFB
  /// <summary>
  /// Модифицировать контекст можно только через выпуск новой версии
  /// </summary>
  ModifyByCreateVersion = -4, // 0xFFFFFFFC
  /// <summary>Модифицировать объект контекста запрещено</summary>
  CantModifyObject = -3, // 0xFFFFFFFD
  /// <summary>
  /// Запрещена или скрыта панель инструментов "Контекст редактирования"
  /// </summary>
  ContextToolbarDisabled = -2, // 0xFFFFFFFE
  /// <summary>Не задан или не найден контекст редактирования</summary>
  UnknownContext = -1, // 0xFFFFFFFF
  /// <summary>Проверка не выполнялась</summary>
  None = 0,
  /// <summary>
  /// Для данного контекста допустимо включить режим автоматического пополнения
  /// </summary>
  CanSetAutoUpdate = 1,
}
