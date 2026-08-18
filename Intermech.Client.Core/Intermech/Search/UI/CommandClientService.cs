
// Type: Intermech.Search.UI.CommandClientService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections.Generic;


namespace Intermech.Search.UI;

public sealed class CommandClientService : ICommandClientService
{
  private List<string> _usedCommands = new List<string>();

  public bool IsUsedCommand(string commandName)
  {
    return !string.IsNullOrEmpty(commandName) ? this._usedCommands.Contains(commandName) : throw new ArgumentException();
  }

  public void ClearUsedCommands() => this._usedCommands.Clear();

  public void RegisterUsedCommand(string commandName)
  {
    if (string.IsNullOrEmpty(commandName))
      throw new ArgumentException();
    if (this._usedCommands.Contains(commandName))
      return;
    this._usedCommands.Add(commandName);
  }
}
