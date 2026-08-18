// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.IWellKnownWindowsOpenService
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Сервис, позволяющий открывать именованные окна Навигатора
/// </summary>
public interface IWellKnownWindowsOpenService
{
  /// <summary>
  /// Зарегистрировать (перекрыть регистрацию) именованное окно Навигатора и метод для его корректного открытия
  /// </summary>
  /// <param name="wellKnownName">Уникальное в пределах Навигатора имя окна (WellKnownName)</param>
  /// <param name="handler">Метод, позволяющий открыть указанное именованное окно</param>
  void RegisterWindowOpeningHandler(string wellKnownName, EventHandler handler);

  /// <summary>
  /// Удалить регистрацию метода для корректного открытия именованного окна Навигатора
  /// </summary>
  /// <param name="wellKnownName">Уникальное в пределах Навигатора имя окна (WellKnownName)</param>
  void UnregisterWindowOpeningHandler(string wellKnownName);

  /// <summary>Открыть именованное окно Навигатора</summary>
  void OpenWellKnownWindow(string wellKnownName);
}
