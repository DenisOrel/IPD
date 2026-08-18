// Decompiled with JetBrains decompiler
// Type: Intermech.Commands.CommandHelper
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Commands;

internal static class CommandHelper
{
  public static void CheckCommandType(Type commandType)
  {
    if (commandType == (Type) null)
      throw new ArgumentNullException(nameof (commandType));
    if (!typeof (Command).IsAssignableFrom(commandType))
      throw new ArgumentException($"Для типа команд '{commandType}' базовым типом должен быть '{typeof (Command)}'.");
  }

  public static void CheckCommandName(string name, string parameterName)
  {
    if (string.IsNullOrEmpty(name))
      throw new ArgumentException("Не задано имя команды.", parameterName);
  }
}
