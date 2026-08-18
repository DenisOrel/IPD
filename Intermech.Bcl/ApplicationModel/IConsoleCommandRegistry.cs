using System.Collections.Generic;


namespace Intermech.ApplicationModel
{
    /// <summary>
    /// Сервис реестра команд, которые можно выполнить в консоли сервера приложений.
    /// </summary>
    public interface IConsoleCommandRegistry
    {
      /// <summary>Добавляет команду в реестр.</summary>
      /// <param name="command">Команда</param>
      /// <exception cref="T:System.ArgumentNullException">command</exception>
      /// <exception cref="T:System.InvalidOperationException">Команда с таким именем уже зарегистрирована</exception>
      void Add(ConsoleCommandInfo command);

      /// <summary>Удаляет команду из реестра.</summary>
      /// <param name="command">Команда</param>
      /// <exception cref="T:System.ArgumentNullException">command</exception>
      void Remove(ConsoleCommandInfo command);

      /// <summary>Возвращает коллекцию зарегистрированных команд.</summary>
      /// <returns>Коллекция зарегистрированных команд</returns>
      ICollection<ConsoleCommandInfo> GetAll();

      /// <summary>Находит команду по имени.</summary>
      /// <param name="commandName">Имя команды</param>
      /// <returns>Найденная команда или null</returns>
      /// <exception cref="T:System.ArgumentNullException">commandName</exception>
      ConsoleCommandInfo FindByName(string commandName);
    }
}
