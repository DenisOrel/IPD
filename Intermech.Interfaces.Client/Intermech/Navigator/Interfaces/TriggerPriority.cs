// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.TriggerPriority
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>Приоритет команд контекстных меню "Навигатора"</summary>
public sealed class TriggerPriority
{
  /// <summary>
  /// Приоритет базовый - команда назначается при наличии выделенных элементов навигации
  /// </summary>
  public const int Basic = 0;
  /// <summary>
  /// Команда назначается после проверки категории выделенных элементов навигации
  /// </summary>
  public const int ItemCategory = 1;
  /// <summary>
  /// Команда назначается после проверки типа выделенных элементов навигации
  /// </summary>
  public const int ItemType = 2;
  /// <summary>
  /// Команда назначается после проверки данных выделенных элементов навигации
  /// </summary>
  public const int ItemData = 4;
  /// <summary>
  /// Команда назначается после проверки категории родительских выделенных элементов навигации
  /// </summary>
  public const int ParentCategory = 8;
  /// <summary>
  /// Команда назначается после проверки типа родительских выделенных элементов навигации
  /// </summary>
  public const int ParentType = 16 /*0x10*/;
  /// <summary>
  /// Команда назначается после проверки данных родительских выделенных элементов навигации
  /// </summary>
  public const int ParentData = 32 /*0x20*/;
  /// <summary>
  /// Команда назначается после проверки контейнера сервисов (контекста выделенных элементов навигации)
  /// </summary>
  public const int ViewServices = 64 /*0x40*/;
}
