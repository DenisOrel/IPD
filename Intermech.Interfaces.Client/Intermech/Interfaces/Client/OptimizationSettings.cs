// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.OptimizationSettings
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Оптимизация системы</summary>
public static class OptimizationSettings
{
  /// <summary>
  /// Минимальное количество уведомлений (минимальный предел)
  /// </summary>
  public const int MinEventsQuantity = 10;
  /// <summary>
  /// Максимальное количество уведомлений (максимальный предел)
  /// </summary>
  public const int MaxEventsQuantity = 1000;
  /// <summary>
  /// Значение по-умолчанию для настройки "Скрывать кнопку "Читать все" навигатора"
  /// </summary>
  public const bool HideNavigatorReadAllButtonDefault = true;
  /// <summary>Выполнять фоновую проверку дочерних узлов в деревьях</summary>
  public static bool BackgroundTreeTasks = false;
  /// <summary>Выполнять проверку орфографии</summary>
  private static bool spellCheck;
  /// <summary>
  /// Полная сортировка составов согласно текущему правилу отображения и сортировки составов
  /// </summary>
  public static bool FullCompositionsSorting = false;
  /// <summary>Не сжимать файлы указанных типов</summary>
  public static string FileZipExclusions;
  /// <summary>Режим обработки сообщений службы уведомлений</summary>
  public static NotificationServiceMode NotificationServiceMode;
  /// <summary>
  /// Максимально допустимое количество уведомлений, превышение которого обновляет все окна вместо их обработки
  /// </summary>
  public static int MaxEventsCount;

  public static bool SpellCheck
  {
    get => OptimizationSettings.spellCheck;
    set => OptimizationSettings.spellCheck = value;
  }

  /// <summary>Скрывать кнопку "Читать все" навигатора</summary>
  public static bool HideNavigatorReadAllButton { get; set; }

  /// <summary>Статический конструктор</summary>
  static OptimizationSettings()
  {
    OptimizationSettings.SpellCheck = false;
    OptimizationSettings.FileZipExclusions = string.Empty;
    OptimizationSettings.NotificationServiceMode = NotificationServiceMode.NotifyUser;
    OptimizationSettings.MaxEventsCount = 100;
    OptimizationSettings.HideNavigatorReadAllButton = true;
  }
}
