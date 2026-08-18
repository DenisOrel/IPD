// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.ContextMenu.CommandsInfo
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Navigator.Interfaces;

#nullable disable
namespace Intermech.Navigator.ContextMenu;

/// <summary>
/// Класс CommandsInfo является контейнером сведений о допустимых и подавляемых командах контекстных меню.
/// Экземпляры данного класса возвращаются в методах классов, реализующих интерфейс ICommandsProvider.
/// </summary>
public class CommandsInfo : ProviderInfo
{
  /// <summary>
  /// Если провайдер команд контекстных меню должен вернуть пустую таблицу с командами,
  /// он может вернуть значение статического свойства CommandsInfo.Empty, вместо того, чтобы
  /// создавать и возвращать пустые экземпляры данного класса.
  /// </summary>
  private static readonly CommandsInfo _empty = new CommandsInfo();

  /// <summary>
  /// Если провайдер команд контекстных меню должен вернуть пустую таблицу с командами,
  /// он может вернуть значение статического свойства CommandsInfo.Empty, вместо того, чтобы
  /// создавать и возвращать пустые экземпляры данного класса.
  /// </summary>
  public static CommandsInfo Empty => CommandsInfo._empty;

  /// <summary>
  /// Возвращает массив имен допустимых команд. Если в контейнер не было
  /// добавлено ни одной такой команды, то результатом будет null.
  /// </summary>
  public string[] CommandNames => this.PossibleItems;

  /// <summary>
  /// Добавляет в контейнер команду и соответствующий ей метод,
  /// появление которой в меню допустимо для данного контекста
  /// </summary>
  /// <param name="commandName">Имя добавляемой команды.</param>
  /// <param name="commandInfo">Информация о методе, реализующем выполнение команды.</param>
  public CommandsInfo Add(string commandName, CommandInfo commandInfo)
  {
    this.AddPossibleItem(commandName, (object) commandInfo);
    return this;
  }

  /// <summary>
  /// Добавляет в контейнер команду, появление которой должно быть подавлено.
  /// </summary>
  /// <param name="commandName">Имя подавляемой команды.</param>
  /// <param name="priority">Приоритет команды.</param>
  public void Suppress(string commandName, int priority)
  {
    this.AddPossibleItem(commandName, (object) new CommandInfo(priority));
  }

  /// <summary>Удаляет из сведения о команде с заданным именем.</summary>
  /// <param name="commandName">Имя команды.</param>
  public void Remove(string commandName) => this.RemovePossibleItem(commandName);

  /// <summary>
  /// Возвращает информацию о методе, реализующем выполнение допустимой команды с указанным именем
  /// </summary>
  /// <param name="commandName">Имя допустимой команды.</param>
  /// <returns>Информация о методе, реализующем выполнение команды.</returns>
  public CommandInfo GetInfo(string commandName) => (CommandInfo) this.GetPossibleItem(commandName);
}
