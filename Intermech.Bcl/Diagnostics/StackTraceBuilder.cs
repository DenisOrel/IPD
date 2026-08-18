
// Type: Intermech.Diagnostics.StackTraceBuilder
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Text;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;


namespace Intermech.Diagnostics
{
    /// <summary>
    /// Расширяет стандартный способ формирования stack trace, позволяя включить в него дополнительную техническую инфомацию.
    /// </summary>
    public class StackTraceBuilder
    {
      private static string _delimiter = new string('=', 32 /*0x20*/);
      private readonly StringBuilder textBuilder;

      /// <summary>Создает объект.</summary>
      public StackTraceBuilder() => this.textBuilder = new StringBuilder(512 /*0x0200*/);

      /// <summary>Добавляет в stack trace указанное исключение.</summary>
      /// <param name="exception">Объект исключения</param>
      /// <exception cref="T:System.ArgumentNullException">Объект исключения не указан</exception>
      public void AppendException(Exception exception)
      {
        RemoteExceptionData remoteData = exception != null ? RemoteExceptionData.TryGet(exception) : throw new ArgumentNullException(nameof (exception));
        bool flag = remoteData != null && !remoteData.IsUnderConstruction && !string.IsNullOrEmpty(remoteData.StackTrace);
        if (flag)
          this.AppendRemoteStackTrace(exception, remoteData);
        this.DoAppendException(exception);
        if (exception.InnerException == null || flag)
          return;
        this.AppendAllInnerExceptions(exception);
      }

      private void AppendRemoteStackTrace(Exception exception, RemoteExceptionData remoteData)
      {
        string stackTrace = remoteData.StackTrace;
        this.textBuilder.AppendLine("Server stack trace:");
        this.textBuilder.Append(stackTrace);
        if (!stackTrace.Contains(Environment.NewLine))
          this.textBuilder.AppendLine();
        this.textBuilder.AppendLine();
        this.textBuilder.AppendLine("Client stack trace:");
      }

      /// <summary>Реализует вывод в stack trace указанного исключения.</summary>
      /// <param name="exception">Объект исключения</param>
      /// <exception cref="T:System.ArgumentNullException">Объект исключения не указан</exception>
      protected virtual void DoAppendException(Exception exception)
      {
        this.AppendDefaultStackTrace(exception);
      }

      private void AppendDefaultStackTrace(Exception exception)
      {
        StackTrace stackTrace = new StackTrace(exception, true);
        IList<string> stackTraceLines = this.GetStackTraceLines(this.GetStackTraceText(exception, stackTrace));
        CompressedStackTrace stackTraceFrames = this.GetStackTraceFrames(stackTrace);
        int num = Math.Min(stackTraceFrames.FrameCount, stackTraceLines.Count);
        for (int index = 0; index < num; ++index)
        {
          string textLine = stackTraceLines[index];
          StackLineBuilder lineBuilder = StackLineBuilder.TryParse(textLine);
          if (lineBuilder != null)
          {
            CompressedStackFrame frame = stackTraceFrames.TryGetFrame(index);
            this.DoMakeStackTraceLine(lineBuilder, frame);
            this.textBuilder.AppendLine(lineBuilder.ToString());
          }
          else
            this.textBuilder.AppendLine(textLine);
        }
        for (int index = num; index < stackTraceLines.Count; ++index)
          this.textBuilder.AppendLine(stackTraceLines[index]);
      }

      private string GetStackTraceText(Exception exception, StackTrace stackTrace)
      {
        return stackTrace.FrameCount != 0 ? stackTrace.ToString() : exception.StackTrace ?? string.Empty;
      }

      private IList<string> GetStackTraceLines(string text)
      {
        List<string> stackTraceLines = new List<string>((IEnumerable<string>) text.Split(TextServices.TextLinesSplitPatterns, StringSplitOptions.None));
        while (stackTraceLines.Count > 0 && string.IsNullOrEmpty(stackTraceLines[stackTraceLines.Count - 1]))
          stackTraceLines.RemoveAt(stackTraceLines.Count - 1);
        return (IList<string>) stackTraceLines;
      }

      private CompressedStackTrace GetStackTraceFrames(StackTrace stackTrace)
      {
        return new CompressedStackTrace(stackTrace);
      }

      /// <summary>Реализует формирование строки stack trace.</summary>
      /// <param name="lineBuilder">Построитель строки</param>
      /// <param name="throwLocation">Точка падения исключения. Может быть null, если эти сведения не удалось получить из объекта исключения</param>
      protected virtual void DoMakeStackTraceLine(
        StackLineBuilder lineBuilder,
        CompressedStackFrame throwLocation)
      {
        if (lineBuilder.ThrowLocation != null || throwLocation == null)
          return;
        lineBuilder.ThrowLocation = throwLocation;
      }

      /// <summary>
      /// Добавляет в вывод вложенные исключения для указанного исключения
      /// </summary>
      /// <param name="exception">Объект исключения</param>
      /// <exception cref="T:System.ArgumentNullException">Объект исключения не указан</exception>
      protected void AppendAllInnerExceptions(Exception exception)
      {
        for (Exception innerException = exception.InnerException; innerException != null; innerException = innerException.InnerException)
        {
          if (this.textBuilder.Length > 0)
          {
            this.textBuilder.AppendLine();
            this.AppendDelimiter();
          }
          this.textBuilder.AppendLine(innerException.Message);
          this.AppendException(innerException);
        }
      }

      /// <summary>Добавляет в вывод строку разделителя.</summary>
      protected void AppendDelimiter() => this.textBuilder.AppendLine(StackTraceBuilder._delimiter);

      /// <summary>Возвращает объект построителя текста.</summary>
      protected StringBuilder TextBuilder => this.textBuilder;

      /// <summary>Очищает вывод.</summary>
      public void Clear() => this.textBuilder.Clear();

      /// <summary>Возвращает результат работы в виде строки.</summary>
      /// <returns>Построенный stack trace в виде строки</returns>
      public override string ToString() => this.textBuilder.ToString();
    }
}
