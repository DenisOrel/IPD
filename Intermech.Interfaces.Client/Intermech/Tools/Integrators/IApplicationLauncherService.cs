// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.IApplicationLauncherService
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Integrators;

/// <summary>
/// Позволяет реализовать сервис интегратора, позволяющий настроить приложение для работы в паре c IPS.
/// </summary>
public interface IApplicationLauncherService
{
  /// <summary>
  /// Возвращает список команд для запуска приложения и настройки его для работы в паре с IPS.
  /// Как правило, список содержит только одну команду, чье название совпадает с названием приложения.
  /// </summary>
  /// <returns>Список команд запуска приложения</returns>
  List<MenuCommand> GetCommands();
}
