
// Type: Intermech.Threading.AsyncCommands
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.Threading
{
    /// <summary>
    /// Фабрика асинхронных команд с поддержкой прерывания выполнения.
    /// </summary>
    public static class AsyncCommands
    {
      /// <summary>Создает команду из указанного метода.</summary>
      /// <param name="action">Метод, который должен быть представлен как команда</param>
      /// <returns>Созданная команда</returns>
      /// <exception cref="T:ArgumentNullException">Параметр <paramref name="action" /> не должен быть равен null</exception>
      public static AsyncCommandActionAdapter FromAction(Action action)
      {
        return new AsyncCommandActionAdapter(action);
      }

      /// <summary>Создает команду из указанной функции.</summary>
      /// <param name="action">Функция, которая должна быть представлена как команда</param>
      /// <returns>Созданная команда</returns>
      /// <exception cref="T:ArgumentNullException">Параметр <paramref name="function" /> не должен быть равен null</exception>
      public static AsyncCommandFuncAdapter<TResult> FromFunction<TResult>(Func<TResult> function)
      {
        return new AsyncCommandFuncAdapter<TResult>(function);
      }
    }
}
