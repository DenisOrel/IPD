
// Type: Intermech.Diagnostics.ExceptionServices
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Pools;
using Intermech.Text;
using System;
using System.Diagnostics;
using System.Text;


namespace Intermech.Diagnostics
{
    /// <summary>
    /// Предоставляет сервисы по упрощению обработки исключений.
    /// </summary>
    public static class ExceptionServices
    {
      private static Func<StackTraceBuilder> stackTraceBuilderFactory = (Func<StackTraceBuilder>) (() => new StackTraceBuilder());

      /// <summary>
      /// Возвращает или задает фабрику объектов для преобразования stack trace в текстовое представление.
      /// </summary>
      /// <exception cref="T:ArgumentNullException">Новое значение свойства не должно быть равно null</exception>
      public static Func<StackTraceBuilder> StackTraceBuilderFactory
      {
        [DebuggerStepThrough] get => ExceptionServices.stackTraceBuilderFactory;
        [DebuggerStepThrough] set
        {
          if (!(ExceptionServices.stackTraceBuilderFactory != value))
            return;
          ExceptionServices.stackTraceBuilderFactory = value != null ? value : throw new ArgumentNullException(nameof (value));
        }
      }

      /// <summary>
      /// Создает объект для преобразования stack trace в текстовое представление.
      /// </summary>
      /// <returns>Объект для преобразования stack trace в текстовое представление</returns>
      public static StackTraceBuilder CreateStackTraceBuilder()
      {
        return ExceptionServices.StackTraceBuilderFactory();
      }

      /// <summary>
      /// Возвращает расширенное текстовое представление для stack trace, которое содержит дополнительные технические сведения,
      /// предоставленные объектом типа StackTraceBuilder.
      /// </summary>
      /// <param name="exception">Объект исключения</param>
      /// <returns>Текстовое представление stack trace</returns>
      /// <exception cref="T:ArgumentNullException">Параметр <paramref name="exception" /> не должен быть равен null</exception>
      public static string GetExtendedStackTrace(Exception exception)
      {
        if (exception == null)
          throw new ArgumentNullException(nameof (exception));
        StackTraceBuilder stackTraceBuilder = ExceptionServices.CreateStackTraceBuilder();
        stackTraceBuilder.AppendException(exception);
        return stackTraceBuilder.ToString();
      }

      /// <summary>
      /// Возвращает расширенное многострочное текстовое представление для исключения, которое содержит текст сообщения, тип исключения и stack trace.
      /// </summary>
      /// <param name="exception">Объект исключения</param>
      /// <param name="preamble">Вступление, которое будет использовано перед основным текстом. Может быть не задано</param>
      /// <returns>Текстовое представление для исключения</returns>
      /// <exception cref="T:ArgumentNullException">Параметр <paramref name="exception" /> не должен быть равен null</exception>
      public static string GetExtendedExceptionText(Exception exception, string preamble = null)
      {
        if (exception == null)
          throw new ArgumentNullException(nameof (exception));
        using (ObjectPoolScope<StringBuilder> objectPoolScope = TextServices.StringBuilderPool.Allocate(2048 /*0x0800*/))
        {
          StringBuilder stringBuilder = objectPoolScope.Object;
          if (!string.IsNullOrEmpty(preamble))
            stringBuilder.AppendLine(preamble);
          stringBuilder.AppendLine(exception.Message);
          stringBuilder.AppendLine($"Type: {exception.GetType()}");
          stringBuilder.AppendLine("Stack trace:");
          stringBuilder.AppendLine(ExceptionServices.GetExtendedStackTrace(exception));
          return stringBuilder.ToString();
        }
      }
    }
}
