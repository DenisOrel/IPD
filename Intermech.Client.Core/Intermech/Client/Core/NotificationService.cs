
// Type: Intermech.Client.Core.NotificationService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using ImSSP;
using Intermech.ApplicationModel;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;


namespace Intermech.Client.Core;

/// <summary>Реализует службу рассылки событий обновления.</summary>
public class NotificationService : INotificationService, IDisposable
{
  private Dictionary<string, NotificationEventHandler> activeHandlers;
  private Dictionary<string, NotificationEventHandler> delayedHandlers;
  private volatile NotificationService parent;
  private List<NotificationService> children;
  private volatile bool delaySubscription;
  private volatile bool optimizationEnabled;
  private volatile IOptionalService<IMainFormUpdate> mainFormServiceProvider;
  private const string AnyEventCookie = "{6365BF58-7C3B-4d5a-874E-F342FDA1F8A3}";

  /// <summary>Делегат "дёргается" перед вызовом указанного события</summary>
  public event NotificationEventHandler OnBeforeEvent;

  /// <summary>Делегат "дёргается" после вызова указанного события</summary>
  public event NotificationEventHandler OnAfterEvent;

  /// <summary>Создает службу рассылки событий обновления</summary>
  public NotificationService()
  {
    this.activeHandlers = new Dictionary<string, NotificationEventHandler>();
    this.delayedHandlers = new Dictionary<string, NotificationEventHandler>();
    this.children = new List<NotificationService>();
    this.optimizationEnabled = true;
    this.mainFormServiceProvider = (IOptionalService<IMainFormUpdate>) new MissingService<IMainFormUpdate>();
  }

  /// <summary>
  /// Освобождает используемые службой ресурсы при ее уничтожении.
  /// </summary>
  ~NotificationService() => this.Dispose(false);

  /// <summary>
  /// Устанавливает или возвращает родительскую службу событий обновления.
  /// </summary>
  public NotificationService Parent
  {
    [DebuggerStepThrough] get => this.parent;
    set
    {
      if (this.parent == value)
        return;
      if (this.parent != null)
        this.parent.children.Remove(this);
      this.parent = value;
      if (this.parent == null)
        return;
      this.parent.children.Add(this);
    }
  }

  /// <summary>Разрешена ли оптимизация обработки уведомлений</summary>
  public bool OptimizationEnabled
  {
    [DebuggerStepThrough] get => this.optimizationEnabled;
    [DebuggerStepThrough] set => this.optimizationEnabled = value;
  }

  /// <summary>
  /// Возвращает или задает провайдер сервиса IMainFormUpdate.
  /// </summary>
  /// <exception cref="T:ArgumentNullException">Новое значение свойства не должно быть равно null</exception>
  public IOptionalService<IMainFormUpdate> MainFormServiceProvider
  {
    [DebuggerStepThrough] get => this.mainFormServiceProvider;
    [DebuggerStepThrough] set
    {
      this.mainFormServiceProvider = value != null ? value : throw new ArgumentNullException(nameof (value));
    }
  }

  private IMainFormUpdate TryGetMainFormService()
  {
    IMainFormUpdate mainFormService1 = this.MainFormServiceProvider.TryGet();
    if (mainFormService1 != null)
      return mainFormService1;
    NotificationService parent = this.Parent;
    if (parent != null)
    {
      IMainFormUpdate mainFormService2 = parent.TryGetMainFormService();
      if (mainFormService2 != null)
        return mainFormService2;
    }
    return (IMainFormUpdate) null;
  }

  /// <summary>
  /// Завершает работу службы обновления, исключая ее из дерева служб обновления.
  /// </summary>
  public void Dispose()
  {
    this.Dispose(true);
    GC.SuppressFinalize((object) this);
  }

  private void Dispose(bool disposing)
  {
    if (!disposing)
      return;
    this.Parent = (NotificationService) null;
    foreach (NotificationService notificationService in this.children.ToArray())
      notificationService.Parent = (NotificationService) null;
  }

  /// <summary>Осуществяет подписку на обработку события обновления.</summary>
  /// <param name="eventName">Имя события обновления.</param>
  /// <param name="eventHandler">Делегат обработчика события обновления.</param>
  public void Subscribe(string eventName, NotificationEventHandler eventHandler)
  {
    this.CheckEventName(eventName);
    this.CheckEventHandler(eventHandler);
    Dictionary<string, NotificationEventHandler> handlers = this.delaySubscription ? this.delayedHandlers : this.activeHandlers;
    this.InternalSubscribe(eventName, eventHandler, handlers);
  }

  /// <summary>
  /// Осуществляет подписку на обработку любых событий обновления.
  /// </summary>
  /// <param name="eventHandler">Делегат обработчика события обновления.</param>
  public void Subscribe(NotificationEventHandler eventHandler)
  {
    this.Subscribe("{6365BF58-7C3B-4d5a-874E-F342FDA1F8A3}", eventHandler);
  }

  /// <summary>Осуществляет отписку от обработки события обновления.</summary>
  /// <param name="eventName">Имя события обновления.</param>
  /// <param name="eventHandler">Делегат обработчика события обновления.</param>
  public void Unsubscribe(string eventName, NotificationEventHandler eventHandler)
  {
    this.CheckEventName(eventName);
    this.CheckEventHandler(eventHandler);
    this.InternalUnsubscribe(eventName, eventHandler, this.activeHandlers);
    this.InternalUnsubscribe(eventName, eventHandler, this.delayedHandlers);
  }

  /// <summary>
  /// Осуществляет отписку от обработки любых событий обновления.
  /// </summary>
  /// <param name="eventHandler">Делегат обработчика события обновления.</param>
  public void Unsubscribe(NotificationEventHandler eventHandler)
  {
    this.Unsubscribe("{6365BF58-7C3B-4d5a-874E-F342FDA1F8A3}", eventHandler);
  }

  /// <summary>Извещает всех подписчиков о произошедшем событии.</summary>
  /// <param name="sender">Объект, рассылающий событие обновления.</param>
  /// <param name="e">Данные для события обновления.</param>
  public void FireEvent(object sender, NotificationEventArgs e) => this.FireEvent(sender, e, true);

  /// <summary>
  /// Извещает всех подписчиков о произошедшем событии, позволяя указать область рассылки сообщения.
  /// </summary>
  /// <param name="sender">Объект, рассылающий событие обновления.</param>
  /// <param name="e">Данные для события обновления.</param>
  /// <param name="redirectToParent">
  /// Если true, то сообщение будет послано всему дереву служб обновления, если false - только
  /// поддереву, начинающемуся с этой службы.
  /// </param>
  public void FireEvent(object sender, NotificationEventArgs e, bool redirectToParent)
  {
    this.delaySubscription = true;
    try
    {
      NotificationService notificationService1 = this;
      if (redirectToParent)
      {
        while (notificationService1.Parent != null)
          notificationService1 = notificationService1.Parent;
      }
      notificationService1.InternalFireEvent(sender, e);
      foreach (NotificationService notificationService2 in this.children.ToArray())
        notificationService2.InternalFireEvent(sender, e);
    }
    finally
    {
      this.delaySubscription = false;
      this.MergeHandlers();
    }
  }

  /// <summary>
  /// Позволяет узнать, есть ли подписчики на указанное событие.
  /// </summary>
  /// <param name="eventName">Имя события</param>
  /// <returns>true - если подписчики есть</returns>
  public bool HasSubscribers(string eventName)
  {
    this.CheckEventName(eventName);
    return this.activeHandlers.ContainsKey(eventName);
  }

  private void CheckEventName(string eventName)
  {
    if (eventName == null)
      throw new ArgumentNullException(sc_4620.ssp_imclient_4621(), LocalizationHolder.rm.GetString("Client.Core_845"));
    if (eventName == string.Empty)
      throw new ArgumentException(LocalizationHolder.rm.GetString(sc_4620.ssp_imclient_4622()), nameof (eventName));
  }

  private void CheckEventHandler(NotificationEventHandler eventHandler)
  {
    if (eventHandler == null)
      throw new ArgumentNullException(sc_4620.ssp_imclient_4623(), LocalizationHolder.rm.GetString("Client.Core_847"));
  }

  private void InternalSubscribe(
    string eventName,
    NotificationEventHandler eventHandler,
    Dictionary<string, NotificationEventHandler> handlers)
  {
    lock (handlers)
    {
      if (handlers.ContainsKey(eventName))
        handlers[eventName] = handlers[eventName] + eventHandler;
      else
        handlers.Add(eventName, eventHandler);
    }
  }

  private void InternalUnsubscribe(
    string eventName,
    NotificationEventHandler eventHandler,
    Dictionary<string, NotificationEventHandler> handlers)
  {
    lock (handlers)
    {
      if (!handlers.ContainsKey(eventName))
        return;
      NotificationEventHandler notificationEventHandler = handlers[eventName] - eventHandler;
      if (notificationEventHandler != null)
        handlers[eventName] = notificationEventHandler;
      else
        handlers.Remove(eventName);
    }
  }

  protected virtual void InternalFireEvent(object sender, NotificationEventArgs e)
  {
    if (this.OptimizationEnabled)
    {
      if (sender == this)
        return;
      NotificationServiceMode optimizationMode = this.GetOptimizationMode(e);
      switch (optimizationMode)
      {
        case NotificationServiceMode.RefreshWindows:
        case NotificationServiceMode.NotifyUser:
          IMainFormUpdate fm = this.TryGetMainFormService();
          if (fm != null)
          {
            if (optimizationMode == NotificationServiceMode.NotifyUser)
              fm.MainForm.Invoke((Delegate) (() => fm.AllWindowsRefreshButtonTextVisible = true));
            if (optimizationMode != NotificationServiceMode.RefreshWindows)
              return;
            fm.MainForm.Invoke((Delegate) (() => fm.ReloadAllWindows((object) this)));
            return;
          }
          break;
      }
    }
    if (e.FirePrePostEvents)
    {
      NotificationEventHandler onBeforeEvent = this.OnBeforeEvent;
      if (onBeforeEvent != null)
        this.InvokeHandler(onBeforeEvent, sender, e);
    }
    this.TryInvokeHandler(this.activeHandlers, e.EventName, sender, e);
    this.TryInvokeHandler(this.activeHandlers, "{6365BF58-7C3B-4d5a-874E-F342FDA1F8A3}", sender, e);
    if (!e.FirePrePostEvents)
      return;
    NotificationEventHandler onAfterEvent = this.OnAfterEvent;
    if (onAfterEvent == null)
      return;
    this.InvokeHandler(onAfterEvent, sender, e);
  }

  private void MergeHandlers()
  {
    lock (this.delayedHandlers)
    {
      foreach (KeyValuePair<string, NotificationEventHandler> delayedHandler in this.delayedHandlers)
        this.InternalSubscribe(delayedHandler.Key, delayedHandler.Value, this.activeHandlers);
      this.delayedHandlers.Clear();
    }
  }

  private void InvokeHandler(
    NotificationEventHandler handler,
    object sender,
    NotificationEventArgs e)
  {
    Delegate[] invocationList = handler.GetInvocationList();
    for (int index = 0; index < invocationList.Length; ++index)
    {
      bool flag = true;
      if (invocationList[index].Target is Control)
      {
        Control target = (Control) invocationList[index].Target;
        if (target.InvokeRequired)
        {
          flag = false;
          target.BeginInvoke(invocationList[index], sender, (object) e);
        }
      }
      if (flag)
        ((NotificationEventHandler) invocationList[index])(sender, e);
    }
  }

  private void TryInvokeHandler(
    Dictionary<string, NotificationEventHandler> handlers,
    string eventName,
    object sender,
    NotificationEventArgs e)
  {
    NotificationEventHandler notificationEventHandler1 = (NotificationEventHandler) null;
    lock (handlers)
      handlers.TryGetValue(eventName, out notificationEventHandler1);
    if (notificationEventHandler1 == null)
      return;
    foreach (Delegate invocation in notificationEventHandler1.GetInvocationList())
    {
      NotificationEventHandler notificationEventHandler2 = (NotificationEventHandler) null;
      lock (handlers)
        handlers.TryGetValue(eventName, out notificationEventHandler2);
      if (notificationEventHandler2 != null && ((IEnumerable<Delegate>) notificationEventHandler2.GetInvocationList()).Contains<Delegate>(invocation))
        this.InvokeDelegate(invocation, sender, e);
    }
  }

  private void InvokeDelegate(Delegate @delegate, object sender, NotificationEventArgs e)
  {
    bool flag = true;
    if (@delegate.Target is Control)
    {
      Control target = (Control) @delegate.Target;
      if (target.InvokeRequired)
      {
        flag = false;
        target.BeginInvoke(@delegate, sender, (object) e);
      }
    }
    if (!flag)
      return;
    ((NotificationEventHandler) @delegate)(sender, e);
  }

  /// <summary>
  /// Метод определяет, в каком режиме должно быть обработано указанное сообщение
  /// </summary>
  /// <param name="e">Аргументы сообщения</param>
  /// <returns>Режим обработки указанного сообщения</returns>
  internal NotificationServiceMode GetOptimizationMode(NotificationEventArgs e)
  {
    if (OptimizationSettings.NotificationServiceMode == NotificationServiceMode.Default)
      return OptimizationSettings.NotificationServiceMode;
    IEventArgsItemsCount eventArgsItemsCount = (IEventArgsItemsCount) e;
    IEventArgsOptimizationMode optimizationMode = (IEventArgsOptimizationMode) e;
    if (eventArgsItemsCount == null || optimizationMode == null)
      return NotificationServiceMode.Default;
    return eventArgsItemsCount.ItemsCount > OptimizationSettings.MaxEventsCount ? optimizationMode.GetSupportedOptimization(OptimizationSettings.NotificationServiceMode) : optimizationMode.GetSupportedOptimization(NotificationServiceMode.Default);
  }
}
