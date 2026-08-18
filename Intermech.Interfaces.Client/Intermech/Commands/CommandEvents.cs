// Decompiled with JetBrains decompiler
// Type: Intermech.Commands.CommandEvents
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Commands;

/// <summary>
/// Содержит глобальные события для команд клиента IPS, поддерживающих централизованное создание и управление.
/// </summary>
public static class CommandEvents
{
  /// <summary>Событие начала выполнения команды.</summary>
  public static event EventHandler CommandStarted;

  /// <summary>
  /// Событие окончания выполнения команды. Событие срабатывает как при успешном выполнении команды,
  /// так и при необработанном исключении при выполнении команды.
  /// </summary>
  public static event EventHandler CommandFinished;

  internal static void RaiseCommandStarted(Command command)
  {
    CommandStack.PushCommand(command);
    EventHandler commandStarted = CommandEvents.CommandStarted;
    if (commandStarted == null)
      return;
    commandStarted((object) command, EventArgs.Empty);
  }

  internal static void RaiseCommandFinished(Command command)
  {
    EventHandler commandFinished = CommandEvents.CommandFinished;
    if (commandFinished != null)
      commandFinished((object) command, EventArgs.Empty);
    CommandStack.PopCommand();
  }
}
