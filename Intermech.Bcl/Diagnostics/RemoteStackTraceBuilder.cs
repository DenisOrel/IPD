
// Type: Intermech.Diagnostics.RemoteStackTraceBuilder
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.Diagnostics
{
    /// <summary>
    /// Реализует ленивое получение стека вызова исключения. Данный класс используется сервером приложений при передаче своих исключений
    /// клиентам.
    /// </summary>
    /// <summary>Создает объект.</summary>
    /// <param name="exception">Объект исключения</param>
    /// <param name="remoteData">Контейнер с техническими сведениями об исключении</param>
    /// <param name="saveFirstFrameOffset">Признак, требуется ли выполнять корректировку смещения для первого кадра в stack trace</param>
    /// <exception cref="T:System.ArgumentNullException">exception или remoteData</exception>
    public sealed class RemoteStackTraceBuilder(Exception exception, RemoteExceptionData remoteData) : 
      RemoteExceptionDataBuilder(exception, remoteData)
    {
      /// <summary>
      /// Заполняет контейнер с техническими сведениями об исключении.
      /// </summary>
      protected override void DoBuild()
      {
        base.DoBuild();
        this.RemoteData.StackTrace = this.GetExceptionStackTraceSafely();
      }

      private string GetExceptionStackTraceSafely()
      {
        try
        {
          return ExceptionServices.GetExtendedStackTrace(this.Exception);
        }
        catch
        {
          return "An error occured during stack trace processing at the server side.";
        }
      }
    }
}
