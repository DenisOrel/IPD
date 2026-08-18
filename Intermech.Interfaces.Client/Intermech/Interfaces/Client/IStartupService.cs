// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.IStartupService
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Сервис сообщений о загрузке системы</summary>
public interface IStartupService
{
  /// <summary>
  /// Возвращает признак, что загрузка приложения полностью завершена, и приложение готово к работе.
  /// </summary>
  bool IsStartupCompleted { get; }

  /// <summary>Главное окно отображено</summary>
  event EventHandler MainFormShown;

  /// <summary>Загрузка системы полностью завершена</summary>
  event EventHandler StartupComplete;
}
