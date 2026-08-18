
// Type: Intermech.Bars.ICommandTargetExtensions
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Diagnostics;
using System.Runtime.CompilerServices;


namespace Intermech.Bars;

public static class ICommandTargetExtensions
{
  /// <summary>Проверка что комманда видима и доступна для вызова</summary>
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool IsCommandEnabled(
    [NotNull] this ICommandTarget commandTarget,
    [NotNull] ICommandManager commandManager,
    [NotNull, NotEmpty] string commandName)
  {
    ICommandState command = commandManager.FindCommand(commandName);
    commandTarget.QueryStatus(command);
    return command.Visible && command.Enabled;
  }

  /// <summary>Получение статуса комманды</summary>
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ICommandState GetCommandState(
    [NotNull] this ICommandTarget commandTarget,
    [NotNull] ICommandManager commandManager,
    [NotNull, NotEmpty] string commandName)
  {
    ICommandState command = commandManager.FindCommand(commandName);
    commandTarget.QueryStatus(command);
    return command;
  }
}
