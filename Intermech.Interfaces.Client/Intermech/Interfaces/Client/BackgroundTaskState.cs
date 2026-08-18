// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.BackgroundTaskState
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Localization;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Текущее состояние фоновой задачи</summary>
public enum BackgroundTaskState
{
  /// <summary>Выполняется</summary>
  [CustomDescription("Attribute.Interfaces.Client_21")] Running,
  /// <summary>Пауза</summary>
  [CustomDescription("Attribute.Interfaces.Client_22")] Paused,
  /// <summary>Остановлено</summary>
  [CustomDescription("Attribute.Interfaces.Client_23")] Stopped,
  /// <summary>Завершено</summary>
  [CustomDescription("Attribute.Interfaces.Client_24")] Terminated,
  /// <summary>Ошибка</summary>
  [CustomDescription("Attribute.Interfaces.Client_25")] Error,
}
