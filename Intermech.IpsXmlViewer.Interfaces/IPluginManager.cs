// Decompiled with JetBrains decompiler
// Type: Intermech.IpsXmlViewer.Interfaces.IPluginManager
// Assembly: Intermech.IpsXmlViewer.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 35FFF223-7A37-420F-9D15-CF4A93D8C384
// Assembly location: D:\IPS\Client\Intermech.IpsXmlViewer.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.IpsXmlViewer.Interfaces.xml

using System;

#nullable disable
namespace Intermech.IpsXmlViewer.Interfaces;

/// <summary>Интерфейс менеджера модулей расширения</summary>
public interface IPluginManager
{
  /// <summary>
  /// Загрузить все найденные плагины из указанной папки и всех её вложенных папок
  /// </summary>
  /// <param name="path">Путь</param>
  /// <param name="mask">Маска для поиска файлов с плагинами</param>
  /// <returns>Коллекция загруженных плагинов</returns>
  IPluginCollection LoadPlugins(string path, string mask = "*.dll");

  /// <summary>Загрузить модуль расширения из указанной сборки</summary>
  /// <param name="fileName">Полный путь к сборке</param>
  /// <returns>Модуль расширения или null</returns>
  IPlugin Load(string fileName);

  /// <summary>Загрузить модуль расширения из указанной сборки</summary>
  /// <param name="fileName">Полный путь к сборке</param>
  /// <param name="autoReload">Требуется ли автоматическая загрузка указанного плагина</param>
  /// <returns>Модуль расширения или null</returns>
  IPlugin Load(string fileName, bool autoReload);

  /// <summary>Выгрузить модуль расширения</summary>
  /// <param name="plugin">Выгружаемый модуль расширения</param>
  void Unload(IPlugin plugin);

  /// <summary>Выгрузить все модули расширения</summary>
  void UnloadAll();

  /// <summary>Получить коллекцию модулей расширения</summary>
  IPluginCollection Plugins { get; }

  /// <summary>Событие "Загружен модуль расширения"</summary>
  event PluginEventHandler PluginAdded;

  /// <summary>Событие "Выгружен модуль расширения"</summary>
  event PluginEventHandler PluginRemoved;

  /// <summary>Событие "Загружены все модули расширения"</summary>
  event EventHandler LoadComplete;

  /// <summary>
  /// Флаг указывает режим загрузки плагина (ручной или автоматический)
  /// </summary>
  bool AutoLoad { get; }

  /// <summary>Версия приложения</summary>
  string AppVersion { get; }
}
