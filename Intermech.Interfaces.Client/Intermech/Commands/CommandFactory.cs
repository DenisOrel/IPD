// Decompiled with JetBrains decompiler
// Type: Intermech.Commands.CommandFactory
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Commands;

public static class CommandFactory
{
  public static T CreateCommand<T>(string commandName, bool throwIfNotCreated) where T : Command
  {
    CommandHelper.CheckCommandType(typeof (T));
    CommandHelper.CheckCommandName(commandName, nameof (commandName));
    T obj = default (T);
    EventHandler<CreateCommandEventArgs> onCreateCommand = CommandFactory.OnCreateCommand;
    if (onCreateCommand != null)
    {
      CreateCommandEventArgs e = new CreateCommandEventArgs(typeof (T), commandName);
      onCreateCommand((object) null, e);
      if (e.Command != null)
        obj = (T) e.Command;
    }
    return (object) obj != null || !throwIfNotCreated ? obj : throw new Exception($"Фабрика команд не смогла создать команду '{commandName}', так как эта команда неизвестна фабрике.");
  }

  /// <summary>Событие создания команды.</summary>
  public static event EventHandler<CreateCommandEventArgs> OnCreateCommand;
}
