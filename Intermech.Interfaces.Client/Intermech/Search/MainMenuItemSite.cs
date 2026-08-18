// Decompiled with JetBrains decompiler
// Type: Intermech.Search.MainMenuItemSite
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Search;

/// <summary>Разрешенные для вставки места в главном меню IPS</summary>
public enum MainMenuItemSite
{
  /// <summary>Меню "Приложения"</summary>
  Applications,
  /// <summary>Меню "Состав"</summary>
  Composition,
  /// <summary>Верхняя группа элементов меню "Настройка"</summary>
  TuningTop,
  /// <summary>Средняя группа элементов меню "Настройка"</summary>
  TuningMiddle,
  /// <summary>Нижняя группа элементов меню "Настройка"</summary>
  TuningBottom,
  /// <summary>Меню "Утилиты администратора"</summary>
  AdministratorUtilities,
  /// <summary>Меню "Экспорт/Импорт"</summary>
  ExportImport,
  /// <summary>Верняя группа элементов меню "Вид"</summary>
  ViewTop,
  /// <summary>Средняя группа элементов меню "Вид"</summary>
  ViewMiddle,
  /// <summary>Нижняя группа элементов меню "Вид"</summary>
  ViewBottom,
}
