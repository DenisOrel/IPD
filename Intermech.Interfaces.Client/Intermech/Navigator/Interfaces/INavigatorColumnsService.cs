// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.INavigatorColumnsService
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>
/// Сервис для хранения настроек видов для различных категорий и типов
/// </summary>
public interface INavigatorColumnsService
{
  event EventHandler<NavigatorColumnsChangedEventArgs> ColumnsChanged;

  /// <summary>Событие "Найти родительскую категорию и тип"</summary>
  event GetCategoryTypeParentEventHandler OnGetCategoryTypeParentEventHandler;

  /// <summary>Создать (перезаписать) настройки вида</summary>
  /// <param name="columns">Новые настройки вида</param>
  /// <returns>true - настройки вида были успешно добавлены в словарик</returns>
  bool CreateNavigatorColumns(NavigatorColumns columns);

  /// <summary>Создать (перезаписать) настройки вида</summary>
  /// <param name="columns">Новые настройки вида</param>
  /// <param name="navStreams">Словарь, в котором хранятся настройки видов</param>
  /// <returns>true - настройки вида были успешно добавлены в словарик</returns>
  bool CreateNavigatorColumns(
    NavigatorColumns columns,
    Dictionary<NavigatorColumnsKey, NavigatorColumns> navStreams);

  /// <summary>
  /// Создать (перезаписать) настройки вида для указанной категории
  /// </summary>
  /// <param name="category">Категория</param>
  /// <returns>Настройки вида для указанной категории</returns>
  NavigatorColumns CreateNavigatorColumns(int category);

  /// <summary>
  /// Создать (перезаписать) настройки вида для указанных категории и типа
  /// </summary>
  /// <param name="category">Категория</param>
  /// <param name="type">Тип</param>
  /// <returns>Настройки вида для указанных категории и типа</returns>
  NavigatorColumns CreateNavigatorColumns(int category, int type);

  /// <summary>
  /// Создать (перезаписать) настройки вида для указанных категории, типа и дополнительного имени
  /// </summary>
  /// <param name="category">Категория</param>
  /// <param name="type">Тип</param>
  /// <param name="suffix">Дополнительное имя</param>
  /// <returns>Настройки вида для указанных категории, типа и дополнительного имени</returns>
  NavigatorColumns CreateNavigatorColumns(int category, int type, string suffix);

  /// <summary>
  /// Создать (перезаписать) настройки вида для указанных категории, типа и дополнительного имени
  /// </summary>
  /// <param name="category">Категория</param>
  /// <param name="type">Тип</param>
  /// <param name="suffix">Дополнительное имя</param>
  /// <param name="navStreams">Словарь, в котором хранятся настройки видов</param>
  /// <returns>Настройки вида для указанных категории, типа и дополнительного имени</returns>
  NavigatorColumns CreateNavigatorColumns(
    int category,
    int type,
    string suffix,
    Dictionary<NavigatorColumnsKey, NavigatorColumns> navStreams);

  /// <summary>
  /// Получить настройки вида для указанной категории. Если поток не
  /// существует, будет возвращен null
  /// </summary>
  /// <param name="category">Категория</param>
  /// <param name="useInheritance">Использовать наследование схем</param>
  /// <returns>Настройки вида для указанной категории или null</returns>
  NavigatorColumns GetNavigatorColumns(int category, bool useInheritance);

  /// <summary>
  /// Получить настройки вида для указанных категории и типа. Если поток не
  /// существует, будет возвращен null
  /// </summary>
  /// <param name="category">Категория</param>
  /// <param name="type">Тип</param>
  /// <param name="useInheritance">Использовать наследование схем</param>
  /// <returns>Настройки вида для указанных категории и типа, или null</returns>
  NavigatorColumns GetNavigatorColumns(int category, int type, bool useInheritance);

  /// <summary>
  /// Получить настройки вида для указанных категории, типа и дополнительного имени. Если поток не
  /// существует, будет возвращен null
  /// </summary>
  /// <param name="category">Категория</param>
  /// <param name="type">Тип</param>
  /// <param name="suffix">Дополнительное имя</param>
  /// <param name="useInheritance">Использовать наследование схем</param>
  /// <returns>Настройки вида для указанных категории, типа и дополнительного имени, или null</returns>
  NavigatorColumns GetNavigatorColumns(int category, int type, string suffix, bool useInheritance);

  /// <summary>
  /// Получить настройки вида для указанных категории, типа и дополнительного имени. Если поток не
  /// существует, будет возвращен null
  /// </summary>
  /// <param name="category">Категория</param>
  /// <param name="type">Тип</param>
  /// <param name="suffix">Дополнительное имя</param>
  /// <param name="useInheritance">Использовать наследование схем</param>
  /// <param name="navStreams">Словарь, в котором хранятся настройки видов</param>
  /// <returns>Настройки вида для указанных категории, типа и дополнительного имени, или null</returns>
  NavigatorColumns GetNavigatorColumns(
    int category,
    int type,
    string suffix,
    bool useInheritance,
    Dictionary<NavigatorColumnsKey, NavigatorColumns> navStreams);

  /// <summary>Удалить настройки вида для указанной категории</summary>
  /// <param name="category">Категория</param>
  /// <returns>true - настройки вида для указанной категории удалён</returns>
  bool RemoveNavigatorColumns(int category);

  /// <summary>Удалить настройки вида для указанных категории и типа</summary>
  /// <param name="category">Категория</param>
  /// <param name="type">Тип</param>
  /// <returns>true - настройки вида для указанных категории и типа удалён</returns>
  bool RemoveNavigatorColumns(int category, int type);

  /// <summary>
  /// Удалить настройки вида для указанных категории, типа и дополнительного имени
  /// </summary>
  /// <param name="category">Категория</param>
  /// <param name="type">Тип</param>
  /// <param name="suffix">Дополнительное имя</param>
  /// <returns>true - настройки вида для указанных категории, типа и дополнительного имени удалён</returns>
  bool RemoveNavigatorColumns(int category, int type, string suffix);

  /// <summary>
  /// Удалить настройки вида для указанных категории, типа и дополнительного имени
  /// </summary>
  /// <param name="category">Категория</param>
  /// <param name="type">Тип</param>
  /// <param name="suffix">Дополнительное имя</param>
  /// <param name="navStreams">Словарь, в котором хранятся настройки видов</param>
  /// <returns>true - настройки вида для указанных категории, типа и дополнительного имени удалён</returns>
  bool RemoveNavigatorColumns(
    int category,
    int type,
    string suffix,
    Dictionary<NavigatorColumnsKey, NavigatorColumns> navStreams);

  /// <summary>Полностью очистить все настройки отображения</summary>
  void Reset();

  /// <summary>
  /// Загрузить настройки из конфигурации текущего пользователя
  /// </summary>
  void LoadFromUserConfig();

  /// <summary>
  /// Сохранить настройки в конфигурацию текущего пользователя
  /// </summary>
  void SaveToUserConfig();

  /// <summary>
  /// Загрузить настройки видов Навигатора из атрибута указанного объекта
  /// </summary>
  /// <param name="objectID">Идентификатор версии объекта</param>
  /// <param name="attributeID">Идентификатор атрибута</param>
  /// <returns>Настройки видов Навигатора или пустой словарик</returns>
  Dictionary<NavigatorColumnsKey, NavigatorColumns> LoadFromObject(long objectID, int attributeID);

  /// <summary>
  /// Сохранить настройки видов Навигатора в атрибут указанного объекта
  /// </summary>
  /// <param name="objectID">Идентификатор версии объекта</param>
  /// <param name="attributeID">Идентификатор атрибута</param>
  /// <param name="navStreams">Настройки видов Навигатора</param>
  /// <returns>true - сохранение выполнено успешно</returns>
  bool SaveToObject(
    long objectID,
    int attributeID,
    Dictionary<NavigatorColumnsKey, NavigatorColumns> navStreams);

  /// <summary>Загрузить настройки из указанного файла</summary>
  /// <param name="fileName">Файл, в котором находятся настройки</param>
  /// <returns>true - настройки успешно загружены</returns>
  bool LoadFromFile(string fileName);

  /// <summary>Сохранить настройки в указанный файл</summary>
  /// <param name="fileName">Файл, в который будут записаны настройки</param>
  /// <returns>true - настройки успешно сохранены</returns>
  bool SaveToFile(string fileName);
}
