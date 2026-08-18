
// Type: Intermech.Diagnostics.ErrorInfo
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.Diagnostics
{
    /// <summary>
    /// Реализует объект для хранения информации об ошибках. Используется в задачах вывода ошибок в логи, журналы и т.д.
    /// </summary>
    public class ErrorInfo
    {
      private readonly string message;
      private readonly string cause;
      private readonly string source;
      private readonly Exception exception;

      /// <summary>Создает объект.</summary>
      /// <param name="message">Сообщение об ошибке</param>
      /// <exception cref="T:System.ArgumentException">Не задано сообщение об ошибке</exception>
      public ErrorInfo(string message)
        : this(message, (string) null, (string) null)
      {
      }

      /// <summary>Создает объект.</summary>
      /// <param name="message">Сообщение об ошибке</param>
      /// <param name="cause">Причина ошибки. Может быть не задана</param>
      /// <param name="source">Источник ошибки. Может быть не задан</param>
      /// <param name="exception">Объект исключения. Может быть не задан</param>
      /// <exception cref="T:System.ArgumentException">Не задано сообщение об ошибке</exception>
      public ErrorInfo(string message, string cause, string source, Exception exception = null)
      {
        this.message = !string.IsNullOrEmpty(message) ? message : throw new ArgumentException("Не задано сообщение об ошибке.", nameof (message));
        this.cause = cause;
        this.source = source;
        this.exception = exception;
      }

      /// <summary>Создает объект ошибки из объекта исключения.</summary>
      /// <param name="exception">Объект исключения</param>
      /// <param name="message">Сообщение об ошибке. Может быть не задано</param>
      /// <returns>Созданных объект ошибки</returns>
      /// <exception cref="T:System.ArgumentNullException">exception</exception>
      public static ErrorInfo FromException(Exception exception, string message = null)
      {
        if (exception == null)
          throw new ArgumentNullException(nameof (exception));
        string cause;
        if (string.IsNullOrEmpty(message))
        {
          cause = (string) null;
          message = $"Необработанное исключение типа {exception.GetType().Name}. {exception.Message}";
        }
        else
          cause = $"{exception.GetType().Name}: {exception.Message}";
        StackTraceBuilder stackTraceBuilder = ExceptionServices.CreateStackTraceBuilder();
        stackTraceBuilder.AppendException(exception);
        string source = stackTraceBuilder.ToString();
        return new ErrorInfo(message, cause, source, exception);
      }

      /// <summary>Возвращает сообщение об ошибке.</summary>
      public string Message => this.message;

      /// <summary>
      /// Возвращает причину ошибки. Может быть не задана.
      /// Если ошибка создана из исключения, то здесь содержится текст исключения.
      /// </summary>
      public string Cause => this.cause;

      /// <summary>
      /// Возвращает источник ошибки. Может быть не задан.
      /// Если ошибка создана из исключения, то здесь содержится стек вызова.
      /// </summary>
      public string Source => this.source;

      /// <summary>Возвращает объект исключения. Может быть не задан.</summary>
      public Exception Exception => this.exception;

      /// <summary>Возвращает текстовое представление объекта.</summary>
      /// <returns>Сообщение об ошибке</returns>
      public override string ToString() => this.message;
    }
}
