// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.DataExchange.CaptureChangesDriverContext
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.ControlFlow.Cooperative;
using System;

#nullable disable
namespace Intermech.Tools.DataExchange;

public class CaptureChangesDriverContext
{
  private readonly ICaptureChangesDriver driver;
  private readonly CaptureChangesContext sharedContext;
  private readonly CooperativeScheduler scheduler;

  public CaptureChangesDriverContext(
    ICaptureChangesDriver driver,
    CaptureChangesContext sharedContext)
  {
    if (driver == null)
      throw new ArgumentNullException(nameof (driver));
    if (sharedContext == null)
      throw new ArgumentNullException(nameof (sharedContext));
    this.driver = driver;
    this.sharedContext = sharedContext;
    this.scheduler = new CooperativeScheduler();
  }

  /// <summary>Возвращает драйвер, создавший объект контекста.</summary>
  public ICaptureChangesDriver Driver => this.driver;

  /// <summary>Возвращает базу данных анализатора изменений.</summary>
  public CaptureChangesDatabase Database => this.sharedContext.Database;

  /// <summary>
  /// Возвращает список операций, выполняемых на клиенте при откате транзакции с серверной очередь операций. Используется для удаления заготовок объектов и других полуфабрикатов из базы IPS.
  /// Операции выполняются в порядке, обратном порядку добавления операций в этот список.
  /// </summary>
  public ActionQueue ServerCleanupActions => this.sharedContext.ServerCleanupActions;

  /// <summary>
  /// Возвращает построитель очереди клиентских операций по обновлению пользовательского интерфейса.
  /// </summary>
  public UINotificationsBuilder UINotifications => this.sharedContext.UINotifications;

  /// <summary>
  /// Возвращает планировщик для обработчиков рабочего контекста.
  /// </summary>
  public CooperativeScheduler Scheduler => this.scheduler;
}
