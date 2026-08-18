using System;
using System.Collections.Generic;


namespace Intermech.ApplicationModel
{
    /// <summary>
    /// Сервис реестра команд, которые можно выполнить в консоли сервера приложений.
    /// </summary>
    public sealed class ConsoleCommandRegistry : IConsoleCommandRegistry
    {
      private readonly List<ConsoleCommandInfo> commandList;
      private readonly Dictionary<string, ConsoleCommandInfo> commandTable;
      private readonly object syncRoot;

      /// <summary>Создает объект.</summary>
      public ConsoleCommandRegistry()
      {
        this.commandList = new List<ConsoleCommandInfo>(32 /*0x20*/);
        this.commandTable = new Dictionary<string, ConsoleCommandInfo>(32 /*0x20*/, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
        this.syncRoot = new object();
      }

      /// <summary>Добавляет команду в реестр.</summary>
      /// <param name="command">Команда</param>
      /// <exception cref="T:System.ArgumentNullException">command</exception>
      /// <exception cref="T:System.InvalidOperationException">Команда с таким именем уже зарегистрирована</exception>
      public void Add(ConsoleCommandInfo command)
      {
        if (command == null)
          throw new ArgumentNullException(nameof (command));
        lock (this.syncRoot)
        {
          this.RequireUniqueCommandName(command.Name);
          if (!string.IsNullOrEmpty(command.ShortName))
            this.RequireUniqueCommandName(command.ShortName);
          this.commandList.Add(command);
          this.commandTable.Add(command.Name, command);
          if (string.IsNullOrEmpty(command.ShortName))
            return;
          this.commandTable.Add(command.ShortName, command);
        }
      }

      private void RequireUniqueCommandName(string commandName)
      {
        if (this.commandTable.ContainsKey(commandName))
          throw new InvalidOperationException($"Команда с именем '{commandName}' уже зарегистрирована.");
      }

      /// <summary>Удаляет команду из реестра.</summary>
      /// <param name="command">Команда</param>
      /// <exception cref="T:System.ArgumentNullException">command</exception>
      public void Remove(ConsoleCommandInfo command)
      {
        if (command == null)
          throw new ArgumentNullException(nameof (command));
        lock (this.syncRoot)
        {
          int num = this.commandTable.Remove(command.Name) ? 1 : 0;
          if (!string.IsNullOrEmpty(command.ShortName))
            this.commandTable.Remove(command.ShortName);
          if (num == 0)
            return;
          this.commandList.Remove(command);
        }
      }

      /// <summary>Возвращает коллекцию зарегистрированных команд.</summary>
      /// <returns>Коллекция зарегистрированных команд</returns>
      public ICollection<ConsoleCommandInfo> GetAll()
      {
        lock (this.syncRoot)
          return (ICollection<ConsoleCommandInfo>) new List<ConsoleCommandInfo>((IEnumerable<ConsoleCommandInfo>) this.commandList);
      }

      /// <summary>Находит команду по имени.</summary>
      /// <param name="commandName">Имя команды</param>
      /// <returns>Найденная команда или null</returns>
      /// <exception cref="T:System.ArgumentNullException">commandName</exception>
      public ConsoleCommandInfo FindByName(string commandName)
      {
        if (commandName == null)
          throw new ArgumentNullException(nameof (commandName));
        lock (this.syncRoot)
        {
          ConsoleCommandInfo consoleCommandInfo;
          return this.commandTable.TryGetValue(commandName, out consoleCommandInfo) ? consoleCommandInfo : (ConsoleCommandInfo) null;
        }
      }
    }
}
