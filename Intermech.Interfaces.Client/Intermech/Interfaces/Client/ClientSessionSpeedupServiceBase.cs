// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.ClientSessionSpeedupServiceBase
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Threading;
using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Базовый класс для прокси-классов сервисов, являющихся кэшированными вариантами серверных сервисов.
/// Реализация должна быть thread safe.
/// </summary>
internal abstract class ClientSessionSpeedupServiceBase
{
  private object _syncRoot;
  private IClientCache _clientCache;
  private AtomicBoolean _isInitialized;

  /// <summary>Создает объект</summary>
  /// <param name="clientCache">Сервис клиентского кэша метаданных</param>
  /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="clientCache" /> содержит null</exception>
  protected ClientSessionSpeedupServiceBase(IClientCache clientCache)
  {
    if (clientCache == null)
      throw new ArgumentNullException(nameof (clientCache));
    this._syncRoot = new object();
    this._clientCache = clientCache;
    this._clientCache.Cleared += new EventHandler(this.OnClientCacheCleared);
    this._clientCache.Reloaded += new EventHandler<ClientCacheReloadedEventArgs>(this.OnClientCacheReloaded);
    this._isInitialized = new AtomicBoolean(false);
  }

  /// <summary>Возвращает сервис клиентского кэша</summary>
  protected IClientCache ClientCache
  {
    [DebuggerStepThrough] get => this._clientCache;
  }

  /// <summary>
  /// Возвращает признак, что инициализация уже была выполнена.
  /// </summary>
  protected bool IsInitialized
  {
    [DebuggerStepThrough] get => this._isInitialized.Value;
  }

  private void OnClientCacheCleared(object sender, EventArgs e)
  {
    lock (this._syncRoot)
      this.ClearInternal();
  }

  private void OnClientCacheReloaded(object sender, ClientCacheReloadedEventArgs e)
  {
    lock (this._syncRoot)
    {
      this.ClearInternal();
      this.InitializeInternal(e.UserSession);
    }
  }

  private void ClearInternal()
  {
    if (!this._isInitialized.Value)
      return;
    this.DoClear();
    this._isInitialized.Value = false;
  }

  private void InitializeInternal(IUserSession userSession)
  {
    if (this._isInitialized.Value)
      return;
    this.DoInitialize(userSession);
    this._isInitialized.Value = true;
  }

  /// <summary>
  /// Очищает сервис после очистки клиентского кэша метаданных.
  /// Реализация является thread safe.
  /// </summary>
  protected virtual void DoClear()
  {
  }

  /// <summary>
  /// Инициализирует сервис после заполнения клиентского кэша метаданных.
  /// Реализация является thread safe.
  /// </summary>
  /// <param name="userSession">Пользовательская сессия</param>
  protected virtual void DoInitialize(IUserSession userSession)
  {
  }

  /// <summary>Проверяет, инициализирован ли сервис.</summary>
  /// <exception cref="T:System.InvalidOperationException">Сервис не был инициализирован</exception>
  protected void CheckInitialized()
  {
    if (!this._isInitialized.Value)
      throw new InvalidOperationException($"The service '{this.GetType()}' must be initialized first. Check the '{this._clientCache.GetType()}' initialization code.");
  }
}
