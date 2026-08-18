// Decompiled with JetBrains decompiler
// Type: Intermech.Commands.CommandStack
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.Commands;

/// <summary>
/// Стек выполняющихся команд клиента IPS, поддерживающих централизованное создание и управление.
/// </summary>
public static class CommandStack
{
  [ThreadStatic]
  private static Stack<Command> currentThreadStack;

  /// <summary>
  /// Возвращает команду, выполняющуюся в данный момент на текущем потоке.
  /// Значение свойства может быть равно null, если ни одна команда не выполняется.
  /// </summary>
  public static Command ActiveCommand
  {
    [DebuggerStepThrough] get
    {
      Stack<Command> currentThreadStack = CommandStack.GetCurrentThreadStack();
      return currentThreadStack.Count == 0 ? (Command) null : currentThreadStack.Peek();
    }
  }

  internal static void PushCommand(Command command)
  {
    CommandStack.GetCurrentThreadStack().Push(command);
  }

  internal static Command PopCommand() => CommandStack.GetCurrentThreadStack().Pop();

  private static Stack<Command> GetCurrentThreadStack()
  {
    if (CommandStack.currentThreadStack == null)
      CommandStack.currentThreadStack = new Stack<Command>();
    return CommandStack.currentThreadStack;
  }
}
