// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.MRP.MRPCompositionTaskState
// Assembly: Intermech.Interfaces.MRP, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 450A2767-EF3B-475F-B784-5AB5004E9964
// Assembly location: D:\IPS\Client\Intermech.Interfaces.MRP.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.MRP.xml

using System;

#nullable disable
namespace Intermech.Interfaces.MRP;

/// <summary>Состояние выполняющегося задания MRPComposition*Task</summary>
[Serializable]
public enum MRPCompositionTaskState
{
  /// <summary>Задание прервано из-за ошибки</summary>
  Error = -2, // 0xFFFFFFFE
  /// <summary>Задание прервано пользователем</summary>
  Cancelled = -1, // 0xFFFFFFFF
  /// <summary>Задание ещё не запущено</summary>
  NotStarted = 0,
  /// <summary>Задание работает</summary>
  Working = 1,
  /// <summary>Задание работает вхолостую (в состоянии ожидания)</summary>
  Throttling = 2,
  /// <summary>Задание успешно завершено</summary>
  Completed = 3,
}
