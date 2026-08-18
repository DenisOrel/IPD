using System.Collections.Generic;


namespace Intermech.ApplicationModel
{
    /// <summary>
    /// Позволяет реализовать обработчик команды для консоли сервера приложений в виде объекта.
    /// </summary>
    public interface IConsoleCommand
    {
      /// <summary>Выполняет команду.</summary>
      /// <param name="consoleService">Сервис консоли сервера приложений</param>
      /// <param name="commandArgs">Аргументы команды</param>
      /// <exception cref="T:System.ArgumentNullException">consoleService or commandArgs</exception>
      void Invoke(IConsoleService consoleService, List<string> commandArgs);
    }
}
