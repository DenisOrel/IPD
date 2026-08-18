using System;
using System.Collections.Generic;


namespace Intermech.ApplicationModel
{
    /// <summary>
    /// Реализует базовый класс для команд консоли сервера приложений.
    /// </summary>
    public abstract class AbstractConsoleCommand : IConsoleCommand
    {
      /// <summary>Выполняет команду.</summary>
      /// <param name="consoleService">Сервис консоли сервера приложений</param>
      /// <param name="commandArgs">Аргументы команды</param>
      /// <exception cref="T:System.ArgumentNullException">consoleService or commandArgs</exception>
      public void Invoke(IConsoleService consoleService, List<string> commandArgs)
      {
        if (consoleService == null)
          throw new ArgumentNullException(nameof (consoleService));
        if (commandArgs == null)
          throw new ArgumentNullException(nameof (commandArgs));
        this.DoInvoke(consoleService, commandArgs);
      }

      /// <summary>Выполняет команду.</summary>
      /// <param name="consoleService">Сервис консоли сервера приложений</param>
      /// <param name="commandArgs">Аргументы команды</param>
      protected abstract void DoInvoke(IConsoleService consoleService, List<string> commandArgs);
    }
}
