
// Type: Intermech.Diagnostics.FatalExceptionLogger
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Pools;
using Intermech.Text;
using System;
using System.Text;


namespace Intermech.Diagnostics
{
    /// <summary>
    /// Позволяет сохранить информацию о необработанном исключении в журнал событий приложения перед падением приложения.
    /// </summary>
    public class FatalExceptionLogger
    {
      private IEventLogWriter eventLogWriter;
      private bool isActive;

      /// <summary>Создает объект.</summary>
      /// <param name="eventLogWriter">Писатель в журнал событий приложения</param>
      /// <exception cref="T:ArgumentNullException">Параметр <paramref name="eventLogWriter" /> не должен быть равен null</exception>
      public FatalExceptionLogger(IEventLogWriter eventLogWriter)
      {
        this.eventLogWriter = eventLogWriter != null ? eventLogWriter : throw new ArgumentNullException(nameof (eventLogWriter));
        this.ApplicationName = AppDomain.CurrentDomain.SetupInformation.ApplicationName;
        this.Recommendation = string.Empty;
      }

      /// <summary>
      /// Возвращает или задает имя приложения для вывода в сообщении о необработанном исключении.
      /// Значение свойства может быть не задано. По умолчанию свойство инициализируется именем из текущего AppDomain.
      /// </summary>
      public string ApplicationName { get; set; }

      /// <summary>
      /// Возвращает или задает рекомендацию для пользователя для вывода в сообщении о необработанном исключении.
      /// Значение свойства может быть не задано.
      /// </summary>
      public string Recommendation { get; set; }

      /// <summary>Активирует обработчик.</summary>
      public void Activate()
      {
        if (this.isActive)
          return;
        AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(this.OnUnhandledException);
        this.isActive = true;
      }

      /// <summary>Деактивирует обработчик.</summary>
      public void Deactivate()
      {
        if (!this.isActive)
          return;
        AppDomain.CurrentDomain.UnhandledException -= new UnhandledExceptionEventHandler(this.OnUnhandledException);
        this.isActive = false;
      }

      private void OnUnhandledException(object sender, UnhandledExceptionEventArgs exceptionInfo)
      {
        if (!(exceptionInfo.ExceptionObject is Exception exceptionObject))
          return;
        try
        {
          using (ObjectPoolScope<StringBuilder> objectPoolScope = TextServices.StringBuilderPool.Allocate(2048 /*0x0800*/))
          {
            StringBuilder result = objectPoolScope.Object;
            this.CreateErrorMessage(exceptionInfo, exceptionObject, result);
            this.eventLogWriter.Write(result.ToString(), EventLogItemType.Error);
          }
        }
        catch
        {
        }
      }

      /// <summary>
      /// Формирует текст сообщения для вывода в журнал. Метод может быть вызван одновременно из нескольки потоков,
      /// доступ к полям данных следует либо синхронизировать, либо не использовать вообще.
      /// </summary>
      /// <param name="exceptionInfo">Аргументы события о необработанном исключении</param>
      /// <param name="exception">Объект исключения</param>
      /// <param name="result">Результат работы метода</param>
      protected virtual void CreateErrorMessage(
        UnhandledExceptionEventArgs exceptionInfo,
        Exception exception,
        StringBuilder result)
      {
        string str = string.IsNullOrEmpty(this.ApplicationName) ? "An unhandled error occured in application." : $"An unhandled error occured in {this.ApplicationName}.";
        result.Append(str);
        if (!string.IsNullOrEmpty(this.Recommendation))
        {
          result.Append(" ");
          result.Append(this.Recommendation);
        }
        result.AppendLine();
        result.AppendLine();
        result.AppendLine(exception.Message);
        result.AppendLine();
        StackTraceBuilder stackTraceBuilder = ExceptionServices.CreateStackTraceBuilder();
        stackTraceBuilder.AppendException(exception);
        result.AppendLine($"Type: {exception.GetType()}");
        result.AppendLine("Stack trace:");
        result.Append(stackTraceBuilder.ToString());
      }
    }
}
