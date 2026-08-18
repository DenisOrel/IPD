
// Type: Intermech.Diagnostics.ErrorReporterAdapter
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Collections;
using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace Intermech.Diagnostics
{
    /// <summary>
    /// Реализует адаптер для вывода списков ошибок через систему вывода многострочных сообщений.
    /// </summary>
    public sealed class ErrorReporterAdapter : IErrorReporter
    {
      private readonly IMessageReporter messageReporter;
      private Func<ICollection<ErrorInfo>, string> captionGenerator;

      /// <summary>Создает объект.</summary>
      /// <param name="messageReporter">Объект для вывода многострочных сообщений</param>
      /// <exception cref="T:System.ArgumentNullException">messageReporter</exception>
      public ErrorReporterAdapter(IMessageReporter messageReporter)
      {
        this.messageReporter = messageReporter != null ? messageReporter : throw new ArgumentNullException(nameof (messageReporter));
      }

      /// <summary>
      /// Возвращает или задает метод для формирования заголовка списка ошибок. Может быть не задан.
      /// </summary>
      public Func<ICollection<ErrorInfo>, string> CaptionGenerator
      {
        [DebuggerStepThrough] get => this.captionGenerator;
        [DebuggerStepThrough] set => this.captionGenerator = value;
      }

      /// <summary>
      /// Выводит список ошибок в журнал в форме, пригодной для чтения пользователем.
      /// </summary>
      /// <param name="errors">Коллекция ошибок</param>
      /// <exception cref="T:System.ArgumentNullException">errors</exception>
      public void ReportErrors(ICollection<ErrorInfo> errors)
      {
        if (errors == null)
          throw new ArgumentNullException(nameof (errors));
        if (errors.Count == 0)
          return;
        if (errors.Count == 1)
        {
          if (this.captionGenerator != null)
            this.messageReporter.WriteLine(this.captionGenerator(errors));
          ErrorInfo firstItem = CollectionUtils.GetFirstItem((IEnumerable<ErrorInfo>) errors);
          this.messageReporter.WriteLine(firstItem.Message);
          this.OutputTechnicalInfo(firstItem, 1);
          this.messageReporter.EndMessage();
        }
        else
        {
          if (this.captionGenerator != null)
            this.messageReporter.WriteLine(this.captionGenerator(errors));
          this.messageReporter.WriteLine("------- Список ошибок -------");
          int num = 0;
          foreach (ErrorInfo error in (IEnumerable<ErrorInfo>) errors)
          {
            this.messageReporter.WriteLine(error.Message);
            this.OutputTechnicalInfo(error, 1);
            ++num;
            if (num < errors.Count)
              this.messageReporter.WriteLine(string.Empty);
          }
          this.messageReporter.WriteLine("------- Конец списка ошибок -------");
          this.messageReporter.EndMessage();
        }
      }

      private void OutputTechnicalInfo(ErrorInfo error, int indent)
      {
        string str = new string(' ', indent * 2);
        if (string.IsNullOrEmpty(error.Cause) && string.IsNullOrEmpty(error.Source))
          return;
        this.messageReporter.WriteLine("Дополнительные сведения:");
        if (!string.IsNullOrEmpty(error.Cause))
          this.messageReporter.WriteLine($"{str}*) причина: {error.Cause}");
        if (string.IsNullOrEmpty(error.Source))
          return;
        if (error.Source.Contains(Environment.NewLine))
        {
          this.messageReporter.WriteLine($"{str}*) источник ошибки:");
          this.messageReporter.WriteLine(error.Source);
        }
        else
          this.messageReporter.WriteLine($"{str}*) источник ошибки: {error.Source}");
      }
    }
}
