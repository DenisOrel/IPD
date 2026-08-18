
// Type: Intermech.Client.Core.Commands.CommandCache.CommandCacheData
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using Intermech.Interfaces.Plugins;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace Intermech.Client.Core.Commands.CommandCache;

/// <summary>Кеш комманд CommandsTable</summary>
internal class CommandCacheData : IDisposable
{
  /// <summary>Размер кеша CommandsTable по умолчанию</summary>
  private const int DefCacheSize = 5;
  /// <summary>
  /// Время жизни кеша CommandsTable по умолчанию в милисекундах
  /// </summary>
  private const int DefCacheLimeTime = 30000;
  /// <summary>
  /// 
  /// </summary>
  private bool _disposed;
  /// <summary>Данные кеша</summary>
  private readonly List<CommandCacheData.CacheItem> _cacheData;
  /// <summary>Размер кеша</summary>
  protected int _cacheSize;
  /// <summary>Время жизни кеша</summary>
  protected int _cacheLifeTime;

  /// <summary>Инициализация данных кеша</summary>
  protected void InitializeData() => this.RegisterEvents();

  /// <summary>Регистрация событий</summary>
  protected void RegisterEvents()
  {
    if (!(ServicesManager.GetService(typeof (IPluginManager)) is IPluginManager service))
      return;
    service.PluginAdded += new PluginEventHandler(this.pluginManager_PluginAdded);
    service.PluginRemoved += new PluginEventHandler(this.pluginManager_PluginRemoved);
  }

  /// <summary>Разрегистрация событий</summary>
  protected void UnregisterEvents()
  {
    if (!(ServicesManager.GetService(typeof (IPluginManager)) is IPluginManager service))
      return;
    service.PluginAdded -= new PluginEventHandler(this.pluginManager_PluginAdded);
    service.PluginRemoved -= new PluginEventHandler(this.pluginManager_PluginRemoved);
  }

  /// <summary>Конструктор</summary>
  public CommandCacheData()
    : this(5, 30000)
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="cacheSize">Размер кеша</param>
  /// <param name="cacheLifeTime">Время жизни содержимого кеша</param>
  public CommandCacheData(int cacheSize, int cacheLifeTime)
  {
    this._cacheData = new List<CommandCacheData.CacheItem>(cacheSize);
    this._cacheSize = cacheSize;
    this._cacheLifeTime = cacheLifeTime;
  }

  /// <summary>Деструктор</summary>
  ~CommandCacheData()
  {
    if (this._disposed)
      return;
    this.UnregisterEvents();
  }

  /// <summary>
  /// 
  /// </summary>
  public void Dispose()
  {
    if (this._disposed)
      return;
    this.UnregisterEvents();
    this._disposed = true;
  }

  /// <summary>
  /// Возвращает таблицу команд, которые могут быть выполнены для указанных
  /// элементов навигации.
  /// </summary>
  /// <remarks>Вынесен в от</remarks>
  /// <param name="items">Коллекция элементов навигации</param>
  /// <param name="viewServices">Контейнер с дополнительными сервисами</param>
  /// <param name="excludeInvisible">Исключить из списка команд те, которые не должны отображаться в контекстных меню</param>
  /// <returns>Таблица команд</returns>
  public CommandsTable GetCommandsTable(
    ISelectedItems items,
    IServiceProvider viewServices,
    bool excludeInvisible)
  {
    lock (this._cacheData)
    {
      for (int index = this._cacheData.Count - 1; index >= 0; --index)
      {
        if (this._cacheData[index].IsStaled(this._cacheLifeTime))
          this._cacheData.RemoveAt(index);
      }
      ViewStateFlags viewState = viewServices.GetService(typeof (IViewState)) is IViewState service ? service.ViewState : ViewStateFlags.None;
      CommandCacheData.CacheItem cacheItem1 = new CommandCacheData.CacheItem(items, viewState, Convert.ToInt32(excludeInvisible));
      int index1 = this._cacheData.IndexOf(cacheItem1);
      if (index1 != -1)
      {
        CommandCacheData.CacheItem cacheItem2 = this._cacheData[index1];
        cacheItem2.UpdateLastAccess();
        return cacheItem2.Commands;
      }
      cacheItem1.Commands = Intermech.Navigator.ContextMenu.Services.GetCommandsTable(items, viewServices, excludeInvisible);
      cacheItem1.UpdateLastAccess();
      if (this._cacheData.Count >= 5)
        this._cacheData.RemoveAt(0);
      this._cacheData.Add(cacheItem1);
      return cacheItem1.Commands;
    }
  }

  /// <summary>Очистка кеша</summary>
  public void ClearCache()
  {
    lock (this._cacheData)
      this._cacheData.Clear();
  }

  /// <summary>Размер кеша</summary>
  public int CacheSize
  {
    [DebuggerStepThrough] get => this._cacheSize;
  }

  /// <summary>Время жизни сожержимого кеша</summary>
  public int CacheLifeTime
  {
    [DebuggerStepThrough] get => this._cacheLifeTime;
  }

  /// <summary>Cобытие на загрузку плагина</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void pluginManager_PluginAdded(object sender, PluginEventArgs e) => this.ClearCache();

  /// <summary>Событие на выгрузку плагина</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void pluginManager_PluginRemoved(object sender, PluginEventArgs e) => this.ClearCache();

  /// <summary>Запись / элемент кеша</summary>
  private class CacheItem : IEquatable<CommandCacheData.CacheItem>
  {
    /// <summary>Время последнего обращения к кешу (в тиках)</summary>
    private int _lastAccess;
    /// <summary>
    /// 
    /// </summary>
    private readonly ViewStateFlags _viewState;
    /// <summary>Флаг</summary>
    private readonly int _flags;
    /// <summary>Список комманд</summary>
    private CommandsTable _commands;
    /// <summary>
    /// 
    /// </summary>
    private readonly List<INodeID> _nodeList;

    /// <summary>Конструктор</summary>
    /// <param name="items"></param>
    /// <param name="viewState"></param>
    /// <param name="flags"></param>
    public CacheItem(ISelectedItems items, ViewStateFlags viewState, int flags)
    {
      this._viewState = viewState;
      this._flags = flags;
      if (items == null)
        return;
      this._nodeList = new List<INodeID>(items.Count);
      for (int index = 0; index < items.Count; ++index)
        this._nodeList.Add(items.GetItemID(index));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool Equals(CommandCacheData.CacheItem other)
    {
      return other != null && this.ViewState.Equals((object) other.ViewState) && this.Flags.Equals(other.Flags) && CommandCacheData.CacheItem.Equals(this.NodeList, other.NodeList);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object obj) => this.Equals(obj as CommandCacheData.CacheItem);

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode() => this.ViewState.GetHashCode() ^ this.Flags.GetHashCode();

    /// <summary>Обновление времени последнего обращения к объекту</summary>
    public void UpdateLastAccess() => this._lastAccess = Environment.TickCount;

    /// <summary>
    /// 
    /// </summary>
    public List<INodeID> NodeList
    {
      [DebuggerStepThrough] get => this._nodeList;
    }

    /// <summary>
    /// 
    /// </summary>
    public ViewStateFlags ViewState => this._viewState;

    /// <summary>Флаги</summary>
    public int Flags
    {
      [DebuggerStepThrough] get => this._flags;
    }

    /// <summary>Данные с коммандами</summary>
    public CommandsTable Commands
    {
      [DebuggerStepThrough] get => this._commands;
      [DebuggerStepThrough] set => this._commands = value;
    }

    /// <summary>"Устарел" ли объект со времени последнего обращения</summary>
    public bool IsStaled(int lifeTime) => this._lastAccess + lifeTime < Environment.TickCount;

    /// <summary>Сравнение объектов</summary>
    /// <param name="items1"></param>
    /// <param name="items2"></param>
    /// <returns></returns>
    private static bool Equals(List<INodeID> items1, List<INodeID> items2)
    {
      if (items1 == null || items2 == null)
        return items1 == null && items2 == null;
      bool flag = items1.Count.Equals(items2.Count);
      if (!flag)
        return false;
      for (int index = 0; index < items1.Count; ++index)
      {
        flag = CommandCacheData.CacheItem.Equals(items1[index], items2[index]);
        if (!flag)
          break;
      }
      return flag;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="item1"></param>
    /// <param name="item2"></param>
    /// <returns></returns>
    private static bool Equals(INodeID item1, INodeID item2)
    {
      if (item1 == null || item2 == null)
        return item1 == null && item2 == null;
      if (!item1.GetHashCode().Equals(item2.GetHashCode()) || !item1.Equals((object) item2))
        return false;
      NodeID nodeId1 = item1 as NodeID;
      NodeID nodeId2 = item2 as NodeID;
      return nodeId1 == null || nodeId2 == null || nodeId1.ObjectID.Equals(nodeId2.ObjectID);
    }
  }
}
