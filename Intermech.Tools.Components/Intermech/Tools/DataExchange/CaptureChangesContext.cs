// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.DataExchange.CaptureChangesContext
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using System;

#nullable disable
namespace Intermech.Tools.DataExchange;

public class CaptureChangesContext
{
  private readonly CaptureChangesDatabase database;
  private readonly ActionQueue serverCleanupActions;
  private readonly UINotificationsBuilder uiNotifications;

  public CaptureChangesContext(
    CaptureChangesDatabase database,
    UINotificationsBuilder uiNotifications)
  {
    if (database == null)
      throw new ArgumentNullException(nameof (database));
    if (uiNotifications == null)
      throw new ArgumentNullException(nameof (uiNotifications));
    this.database = database;
    this.serverCleanupActions = new ActionQueue();
    this.uiNotifications = uiNotifications;
  }

  /// <summary>Возвращает базу данных анализатора изменений.</summary>
  public CaptureChangesDatabase Database => this.database;

  /// <summary>
  /// Возвращает список операций, выполняемых на клиенте при откате транзакции с серверной очередь операций. Используется для удаления заготовок объектов и других полуфабрикатов из базы IPS.
  /// Операции выполняются в порядке, обратном порядку добавления операций в этот список.
  /// </summary>
  public ActionQueue ServerCleanupActions => this.serverCleanupActions;

  /// <summary>
  /// Возвращает построитель очереди клиентских операций по обновлению пользовательского интерфейса.
  /// </summary>
  public UINotificationsBuilder UINotifications => this.uiNotifications;
}
