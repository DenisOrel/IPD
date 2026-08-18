// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.DataExchange.ActionQueuePair
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

#nullable disable
namespace Intermech.Tools.DataExchange;

/// <summary>
/// Реализует построитель списков операций, необходимых для захвата изменений в объекте или связи.
/// </summary>
public sealed class ActionQueuePair
{
  private readonly ActionQueue serverActions;
  private readonly ActionQueue clientActions;

  /// <summary>Создает объект.</summary>
  public ActionQueuePair()
  {
    this.serverActions = new ActionQueue();
    this.clientActions = new ActionQueue();
  }

  /// <summary>Коллекция серверных операций.</summary>
  public ActionQueue ServerActions => this.serverActions;

  /// <summary>Коллекция клиентских операций.</summary>
  public ActionQueue ClientActions => this.clientActions;
}
