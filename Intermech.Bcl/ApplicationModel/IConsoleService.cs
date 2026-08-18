
// Type: Intermech.ApplicationModel.IConsoleService
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.ApplicationModel
{
    /// <summary>
    /// Сервис для работы с консолью сервера приложений. Позволяет выводить текст, а также читать пользовательский ввод.
    /// </summary>
    public interface IConsoleService
    {
      /// <summary>Читает пользовательский ввод из консоли.</summary>
      /// <returns>Введенный пользователем текст</returns>
      string ReadLine();

      /// <summary>
      /// Читает пользовательский ввод из консоли, если он имеется. Иначе возвращает null.
      /// </summary>
      /// <returns>Введенный пользователем текст или null</returns>
      string TryReadLine();

      /// <summary>Выдать в консоль указанный текст.</summary>
      /// <param name="text">Текст</param>
      void Write(string text);

      /// <summary>
      /// Выдать в консоль указанный текст.
      /// Старый цвет текста сохраняется перед выводом, затем восстанавливается
      /// </summary>
      /// <param name="text">Текст</param>
      /// <param name="color">Цвет</param>
      void Write(string text, ConsoleColor color);

      /// <summary>Выдать в консоль указанный текст.</summary>
      /// <param name="text">Текст</param>
      void WriteLine(string text);

      /// <summary>
      /// Выдать в консоль указанный текст.
      /// Старый цвет текста сохраняется перед выводом, затем восстанавливается
      /// </summary>
      /// <param name="text">Текст</param>
      /// <param name="color">Цвет</param>
      void WriteLine(string text, ConsoleColor color);
    }
}
