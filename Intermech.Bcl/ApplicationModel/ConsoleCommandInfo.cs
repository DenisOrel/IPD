using System;
using System.Diagnostics;


namespace Intermech.ApplicationModel
{
    /// <summary>Описывает команду для консоли сервера приложений.</summary>
    public class ConsoleCommandInfo
    {
      private readonly string name;
      private readonly string shortName;
      private readonly string help;
      private readonly IConsoleCommand handler;

      /// <summary>Создает объект.</summary>
      /// <param name="name">Имя команды</param>
      /// <param name="shortName">Краткое имя команды или пустая строка</param>
      /// <param name="help">Краткая справка по команде или пустая строка</param>
      /// <param name="handler">Обработчик команды</param>
      /// <exception cref="T:System.ArgumentNullException">shortName or help or handler</exception>
      /// <exception cref="T:System.ArgumentException">name - имя команды пусто</exception>
      public ConsoleCommandInfo(string name, string shortName, string help, IConsoleCommand handler)
      {
        if (string.IsNullOrEmpty(name))
          throw new ArgumentException("Имя команды не задано", nameof (name));
        if (shortName == null)
          throw new ArgumentNullException(nameof (shortName));
        if (help == null)
          throw new ArgumentNullException(nameof (help));
        if (handler == null)
          throw new ArgumentNullException(nameof (handler));
        this.name = name;
        this.shortName = shortName;
        this.help = help;
        this.handler = handler;
      }

      /// <summary>Создает объект.</summary>
      /// <param name="name">Имя команды</param>
      /// <param name="shortName">Краткое имя команды или пустая строка</param>
      /// <param name="help">Краткая справка по команде или пустая строка</param>
      /// <param name="methodHandler">Метод-обработчик команды</param>
      /// <exception cref="T:System.ArgumentNullException">shortName or help or methodHandler</exception>
      /// <exception cref="T:System.ArgumentException">name - имя команды пусто</exception>
      public ConsoleCommandInfo(
        string name,
        string shortName,
        string help,
        ConsoleCommandMethod methodHandler)
        : this(name, shortName, help, (IConsoleCommand) new ConsoleCommandMethodAdapter(methodHandler))
      {
      }

      /// <summary>
      /// Возвращает имя команды, с помощью которой она запускается из консоли.
      /// </summary>
      public string Name
      {
        [DebuggerStepThrough] get => this.name;
      }

      /// <summary>
      /// Возвращает краткое имя команды, с помощью которой она запускается из консоли. Например, "?" - краткое имя для команды "help".
      /// Свойство может быть не задано и содержать пустую строку.
      /// </summary>
      public string ShortName
      {
        [DebuggerStepThrough] get => this.shortName;
      }

      /// <summary>
      /// Возвращает краткую справку по использованию команды. Свойство может быть не задано и содержать пустую строку.
      /// </summary>
      public string Help
      {
        [DebuggerStepThrough] get => this.help;
      }

      /// <summary>Возвращает обработчик команды.</summary>
      public IConsoleCommand Handler
      {
        [DebuggerStepThrough] get => this.handler;
      }
    }
}
