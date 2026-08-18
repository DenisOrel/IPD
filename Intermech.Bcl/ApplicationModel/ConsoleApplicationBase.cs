using System;
using System.Collections.Generic;


namespace Intermech.ApplicationModel
{
    /// <summary>Базовый класс для консольных приложений.</summary>
    public class ConsoleApplicationBase : ApplicationBase
    {
      private WindowsConsoleCtrlHandler consoleCtrlHandler;

      /// <summary>Создает объект приложения.</summary>
      /// <param name="arguments">Аргументы приложения</param>
      /// <exception cref="T:ArgumentNullException">Параметр <paramref name="arguments" /> не должен быть равен null</exception>
      public ConsoleApplicationBase(string[] arguments)
        : base((IList<string>) arguments)
      {
        Console.CancelKeyPress += new ConsoleCancelEventHandler(this.OnCancelKeyPress);
        this.CreateConsoleCtrlHandler();
      }

      /// <summary>
      /// Реализует освобождение ресурсов приложения перед завершением работы. Метод вызывается как при нормальном завершении приложения,
      /// так и в случае необработанного исключения в процессе выполнения приложения. Реализация метода должна учитывать, что он
      /// может быть вызван для частично инициализированного приложения.
      /// </summary>
      /// <param name="errorMode">Признак завершения работы приложения из-за необработанного исключения</param>
      protected override void DoCleanup(bool errorMode)
      {
        this.consoleCtrlHandler.Deactivate();
        base.DoCleanup(errorMode);
      }

      private void OnCancelKeyPress(object sender, ConsoleCancelEventArgs e)
      {
        if (e.SpecialKey != ConsoleSpecialKey.ControlC && e.SpecialKey != ConsoleSpecialKey.ControlBreak)
          return;
        e.Cancel = true;
      }

      private void CreateConsoleCtrlHandler()
      {
        this.consoleCtrlHandler = new WindowsConsoleCtrlHandler((object) this);
        this.consoleCtrlHandler.OnCloseConsole += new EventHandler(this.OnCloseConsole);
        this.consoleCtrlHandler.Activate();
      }

      private void OnCloseConsole(object sender, EventArgs e)
      {
        if (!this.IsRunning)
          return;
        this.InvokeSilently(new Action(this.DoEmergencyExit));
      }

      /// <summary>
      /// Обработчик для события аварийного завершения работы консольного приложения. Вызывается по нажатию кнопки "Закрыть" у консоли приложения.
      /// Метод обработчика вызывается из фонового потока, у него есть всего 2 секунды, чтобы обработать событие.
      /// </summary>
      protected virtual void DoEmergencyExit() => this.consoleCtrlHandler.Deactivate(false);
    }
}
