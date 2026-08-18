// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.PdmConfigurator.PdmCompositionBrowserJobProgress
// Assembly: Intermech.Interfaces.PdmConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 6A3EF664-00FF-4A8A-A8E2-24964457B937
// Assembly location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.xml

using System;

#nullable disable
namespace Intermech.Interfaces.PdmConfigurator;

/// <summary>Индикатор выполнения задания по раскрутке состава</summary>
[Serializable]
public enum PdmCompositionBrowserJobProgress
{
  /// <summary>Задание прервано из-за ошибки</summary>
  Error = -2, // 0xFFFFFFFE
  /// <summary>Задание прервано пользователем</summary>
  Cancelled = -1, // 0xFFFFFFFF
  /// <summary>Задание ещё не запущено</summary>
  NotStarted = 0,
  /// <summary>Задание работает</summary>
  Working = 1,
  /// <summary>Задание успешно завершено</summary>
  Completed = 2,
}
