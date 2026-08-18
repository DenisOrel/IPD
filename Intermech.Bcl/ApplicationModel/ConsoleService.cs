using System;


namespace Intermech.ApplicationModel
{
    /// <summary>Обработчик серверных консольных команд</summary>
    public sealed class ConsoleService : IConsoleService
    {
      /// <summary>Читает пользовательский ввод из консоли.</summary>
      /// <returns>Введенный пользователем текст</returns>
      public string ReadLine() => Console.ReadLine();

      /// <summary>
      /// Читает пользовательский ввод из консоли, если он имеется. Иначе возвращает null.
      /// </summary>
      /// <returns>Введенный пользователем текст или null</returns>
      public string TryReadLine()
      {
        return (Console.IsInputRedirected ? (Console.In.Peek() != -1 ? 1 : 0) : (Console.KeyAvailable ? 1 : 0)) != 0 ? Console.ReadLine() : (string) null;
      }

      /// <summary>Выдать в консоль указанный текст.</summary>
      /// <param name="text">Текст</param>
      public void Write(string text) => Console.Write(text);

      /// <summary>
      /// Выдать в консоль указанный текст.
      /// Старый цвет текста сохраняется перед выводом, затем восстанавливается
      /// </summary>
      /// <param name="text">Текст</param>
      /// <param name="color">Цвет</param>
      public void Write(string text, ConsoleColor color)
      {
        int foregroundColor = (int) Console.ForegroundColor;
        Console.ForegroundColor = color;
        Console.Write(text);
        Console.ForegroundColor = (ConsoleColor) foregroundColor;
      }

      /// <summary>Выдать в консоль указанный текст.</summary>
      /// <param name="text">Текст</param>
      public void WriteLine(string text) => Console.WriteLine(text);

      /// <summary>
      /// Выдать в консоль указанный текст.
      /// Старый цвет текста сохраняется перед выводом, затем восстанавливается
      /// </summary>
      /// <param name="text">Текст</param>
      /// <param name="color">Цвет</param>
      public void WriteLine(string text, ConsoleColor color)
      {
        int foregroundColor = (int) Console.ForegroundColor;
        Console.ForegroundColor = color;
        Console.Write(text);
        Console.ForegroundColor = (ConsoleColor) foregroundColor;
        Console.WriteLine();
      }
    }
}
