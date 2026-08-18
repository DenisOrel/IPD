
// Type: IMClient.WellKnownWindowsOpenService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using Intermech.Localization;
using System;
using System.Collections.Generic;


namespace IMClient;

/// <summary>
/// Сервис, позволяющий открывать именованные окна Навигатора в главной форме IMClient
/// </summary>
public class WellKnownWindowsOpenService : IWellKnownWindowsOpenService
{
  /// <summary>
  /// Словарик обработчиков для корректного открытия именованных окон Навигатора
  /// </summary>
  private SortedDictionary<string, EventHandler> handlers = new SortedDictionary<string, EventHandler>();

  /// <summary>
  /// Создать и зарегистрировать экземпляр службы IWellKnownWindowsOpenService
  /// </summary>
  /// <returns>Ссылка на зарегистрированную службу IWellKnownWindowsOpenService</returns>
  public static IWellKnownWindowsOpenService Register()
  {
    if (ServicesManager.GetService(typeof (IWellKnownWindowsOpenService)) is IWellKnownWindowsOpenService service)
      return service;
    IWellKnownWindowsOpenService serviceInstance = (IWellKnownWindowsOpenService) new WellKnownWindowsOpenService();
    ServicesManager.AddService(typeof (IWellKnownWindowsOpenService), (object) serviceInstance);
    return serviceInstance;
  }

  /// <summary>
  /// Зарегистрировать (перекрыть регистрацию) именованное окно Навигатора и метод для его корректного открытия
  /// </summary>
  /// <param name="wellKnownName">Уникальное в пределах Навигатора имя окна (WellKnownName)</param>
  /// <param name="handler">Метод, позволяющий открыть указанное именованное окно</param>
  public void RegisterWindowOpeningHandler(string wellKnownName, EventHandler handler)
  {
    if (string.IsNullOrEmpty(wellKnownName))
      throw new ArgumentException(LocalizationHolder.rm.GetString("Client.Core_1599"), nameof (wellKnownName));
    if (handler == null)
      throw new ArgumentNullException(LocalizationHolder.rm.GetString("Client.Core_1600"), nameof (handler));
    lock (this.handlers)
      this.handlers[wellKnownName] = handler;
  }

  /// <summary>
  /// Удалить регистрацию метода для корректного открытия именованного окна Навигатора
  /// </summary>
  /// <param name="wellKnownName">Уникальное в пределах Навигатора имя окна (WellKnownName)</param>
  public void UnregisterWindowOpeningHandler(string wellKnownName)
  {
    lock (this.handlers)
    {
      if (!this.handlers.ContainsKey(wellKnownName))
        return;
      this.handlers.Remove(wellKnownName);
    }
  }

  /// <summary>Открыть именованное окно Навигатора</summary>
  public void OpenWellKnownWindow(string wellKnownName)
  {
    lock (this.handlers)
    {
      if (!this.handlers.ContainsKey(wellKnownName))
        return;
      this.handlers[wellKnownName]((object) this, EventArgs.Empty);
    }
  }
}
