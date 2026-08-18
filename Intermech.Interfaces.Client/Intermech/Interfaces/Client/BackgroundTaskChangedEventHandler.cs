// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.BackgroundTaskChangedEventHandler
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Делегат, вызываемый при изменении состояния фоновой задачи
/// </summary>
/// <param name="sender">Отправитель события</param>
/// <param name="type">Какие изменения произошли в фоновой задаче</param>
public delegate void BackgroundTaskChangedEventHandler(
  object sender,
  BackgroundTaskChangedType type);
