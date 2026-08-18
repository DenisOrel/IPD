
// Type: Intermech.Diagnostics.RemoteExceptionDataBuilder
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Diagnostics;


namespace Intermech.Diagnostics
{
    /// <summary>
    /// Базовый класс для объектов, реализующих ленивое заполнение RemoteExceptionData, которое выполняется только в случае передачи
    /// объекта исключения от сервера приложений клиенту.
    /// </summary>
    public class RemoteExceptionDataBuilder
    {
      private Exception exception;
      private RemoteExceptionData remoteData;

      /// <summary>Создает объект.</summary>
      /// <param name="exception">Объект исключения</param>
      /// <param name="remoteData">Контейнер с техническими сведениями об исключении</param>
      /// <exception cref="T:System.ArgumentNullException">exception или remoteData</exception>
      public RemoteExceptionDataBuilder(Exception exception, RemoteExceptionData remoteData)
      {
        if (exception == null)
          throw new ArgumentNullException(nameof (exception));
        if (remoteData == null)
          throw new ArgumentNullException(nameof (remoteData));
        this.exception = exception;
        this.remoteData = remoteData;
      }

      /// <summary>Возвращает объект исключения.</summary>
      public Exception Exception
      {
        [DebuggerStepThrough] get => this.exception;
      }

      /// <summary>
      /// Возвращает контейнер с техническими сведениями об исключении.
      /// </summary>
      public RemoteExceptionData RemoteData
      {
        [DebuggerStepThrough] get => this.remoteData;
      }

      /// <summary>
      /// Заполняет контейнер с техническими сведениями об исключении.
      /// </summary>
      internal void Build()
      {
        try
        {
          this.DoBuild();
        }
        catch
        {
        }
      }

      /// <summary>
      /// Заполняет контейнер с техническими сведениями об исключении.
      /// </summary>
      protected virtual void DoBuild()
      {
      }
    }
}
