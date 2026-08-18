// Decompiled with JetBrains decompiler
// Type: Intermech.Commands.Command
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Interfaces.Client;
using System;
using System.ComponentModel.Design;
using System.Diagnostics;

#nullable disable
namespace Intermech.Commands;

/// <summary>
/// Базовый класс для команд клиента IPS, поддерживающих централизованное создание и управление.
/// </summary>
public abstract class Command
{
  private string name;
  private string displayName;
  private INotificationQueue notifications;
  private bool updateUI;
  private IServiceProvider contextServices;
  private static readonly IServiceProvider emptyContextServices = (IServiceProvider) new ServiceContainer();

  /// <summary>Создает объект.</summary>
  /// <param name="name">Имя команды</param>
  /// <exception cref="T:ArgumentException">Параметр <paramref name="name" /> не должен быть пуст или равен null</exception>
  public Command(string name)
  {
    CommandHelper.CheckCommandName(name, nameof (name));
    this.name = name;
    this.displayName = name;
    this.notifications = (INotificationQueue) new NotificationQueue();
    this.updateUI = true;
    this.contextServices = Command.emptyContextServices;
  }

  /// <summary>Возвращает имя команды.</summary>
  public string Name
  {
    [DebuggerStepThrough] get => this.name;
  }

  /// <summary>
  /// Возвращает или задает название команды, отображаемое в диалоговых окнах.
  /// По умолчанию значение свойства равно имени команды.
  /// </summary>
  /// <exception cref="T:ArgumentNullException">Новое значение свойства не должно быть равно null</exception>
  public string DisplayName
  {
    [DebuggerStepThrough] get => this.displayName;
    [DebuggerStepThrough] set
    {
      this.displayName = value != null ? value : throw new ArgumentNullException(nameof (value));
    }
  }

  /// <summary>
  /// Возвращает или задает очередь событий обновления интерфейса пользователя.
  /// </summary>
  /// <exception cref="T:ArgumentNullException">Новое значение свойства не должно быть равно null</exception>
  public INotificationQueue Notifications
  {
    [DebuggerStepThrough] get => this.notifications;
    [DebuggerStepThrough] set
    {
      this.notifications = value != null ? value : throw new ArgumentNullException(nameof (value));
    }
  }

  /// <summary>
  /// Включает или выключает рассылку событий обновления интерфейса пользователя.
  /// </summary>
  public bool UpdateUI
  {
    [DebuggerStepThrough] get => this.updateUI;
    [DebuggerStepThrough] set => this.updateUI = value;
  }

  /// <summary>
  /// Возвращает или задает контейнер сервисов окружения, в котором команда будет выполнена.
  /// По умолчанию свойство указывает на пустой контейнер.
  /// </summary>
  /// <exception cref="T:ArgumentNullException">Новое значение свойства не должно быть равно null</exception>
  public IServiceProvider ContextServices
  {
    [DebuggerStepThrough] get => this.contextServices;
    [DebuggerStepThrough] set
    {
      this.contextServices = value != null ? value : throw new ArgumentNullException(nameof (value));
    }
  }

  /// <summary>Выполняет команду.</summary>
  /// <param name="flushNotifyQueue">Признак запуска событий обновления интерфейса пользователя после успешного выполнения команды</param>
  public void Execute(bool flushNotifyQueue)
  {
    CommandEvents.RaiseCommandStarted(this);
    try
    {
      this.DoExecute();
      if (!flushNotifyQueue)
        return;
      this.FlushNotificationQuery();
    }
    finally
    {
      CommandEvents.RaiseCommandFinished(this);
    }
  }

  /// <summary>Выполняет команду.</summary>
  public void Execute() => this.Execute(true);

  /// <summary>Выполняет команду.</summary>
  protected abstract void DoExecute();

  /// <summary>Отправка событий гна выполнение</summary>
  protected virtual void FlushNotificationQuery() => this.Notifications.FlushQueue();
}
