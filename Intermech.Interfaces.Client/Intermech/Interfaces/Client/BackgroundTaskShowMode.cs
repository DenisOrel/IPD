// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.BackgroundTaskShowMode
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Режим отображения состояния фоновой задачи</summary>
public enum BackgroundTaskShowMode
{
  /// <summary>В виде индикатора прогресса</summary>
  Progress,
  /// <summary>В виде текстового сообщения</summary>
  Text,
  /// <summary>Индикатор прогресса с подсчетом оставшегося времени</summary>
  TimedProgress,
}
