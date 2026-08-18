
// Type: Intermech.Client.Core.Commands.CommandCache.CommandCacheService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using System;


namespace Intermech.Client.Core.Commands.CommandCache;

/// <summary>Интерфейс кеша комманд для элементов навигации</summary>
internal class CommandCacheService : ICommandCacheService
{
  /// <summary>
  /// 
  /// </summary>
  private CommandCacheData _commandsCache;

  /// <summary>
  /// Возвращает таблицу команд, которые могут быть выполнены для указанных
  /// элементов навигации.
  /// </summary>
  /// <param name="items">Коллекция элементов навигации</param>
  /// <param name="viewServices">Контейнер с дополнительными сервисами</param>
  /// <param name="excludeInvisible">Исключить из списка команд те, которые не должны отображаться в контекстных меню</param>
  /// <returns>Таблица команд</returns>
  CommandsTable ICommandCacheService.GetCommandsTable(
    ISelectedItems items,
    IServiceProvider viewServices,
    bool excludeInvisible)
  {
    if (this._commandsCache == null)
      this._commandsCache = new CommandCacheData();
    return this._commandsCache.GetCommandsTable(items, viewServices, excludeInvisible);
  }

  /// <summary>Регистрация сервиса</summary>
  public static void RegisterService()
  {
    CommandCacheService.RemoveService();
    ServicesManager.AddService(typeof (ICommandCacheService), (object) new CommandCacheService());
  }

  /// <summary>Удаление сервиса</summary>
  public static void RemoveService()
  {
    if (ServicesManager.GetService(typeof (ICommandCacheService)) == null)
      return;
    ServicesManager.RemoveService(typeof (ICommandCacheService));
  }
}
