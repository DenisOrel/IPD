// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.WellKnownWindowsNames
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Класс содержит имена нескольких стандартных окон Навигатора.
/// Их можно использовать в службе IWellKnownWindowsOpenService
/// </summary>
public static class WellKnownWindowsNames
{
  /// <summary>Уникальное имя стандартного окна "Навигатор"</summary>
  public const string Navigator = "mainNavigator";
  /// <summary>Уникальное имя стандартного окна "Избранное"</summary>
  public const string Favorites = "favoritesNavigator";
  /// <summary>Уникальное имя стандартного окна "Рабочий стол"</summary>
  public const string Desktop = "desktopNavigator";
  /// <summary>Уникальное имя стандартного окна "Недавние объекты"</summary>
  public const string RecentObjects = "desktopRecentObjects";
  /// <summary>
  /// Уникальное имя стандартного окна "Редактор правил автоматической сортировки и отображения составов"
  /// </summary>
  public const string CompositionsAutosortRules = "desktopAutosortWindow";
  /// <summary>
  /// Уникальное имя для стандартного окна "Статистика запросов"
  /// </summary>
  public const string DatabaseStatistics = "databaseStatistics";
  /// <summary>
  /// Уникальное имя для стандартного окна "Администратор базы данных"
  /// </summary>
  public const string DatabaseAdministrator = "SecurityWindow";
  /// <summary>
  /// Уникальное имя для стандартного окна "Конфигуратор базы данных"
  /// </summary>
  public const string DatabaseConfigurator = "databaseConfigurator";
  /// <summary>Уникальное имя для стандартного окна "WEB портал"</summary>
  public const string Portal = "portalWindow";
}
