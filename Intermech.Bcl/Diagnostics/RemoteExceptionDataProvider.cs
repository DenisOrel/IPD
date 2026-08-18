
// Type: Intermech.Diagnostics.RemoteExceptionDataProvider
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.Diagnostics
{
    /// <summary>
    /// <para>Реализует автоматическое сохранение дополнительных технических сведений об исключении во время его падения
    /// в самом объекте исключения. Этот провайдер используется сервером приложений и собирает сведения,
    /// которые не могут быть получены клиентом самостоятельно в момент обработки исключения.
    /// </para>
    /// <para>Используется для передачи информации о точном месте падения исключения, а также других сведений,
    /// предназначенных для улучшения диагностики ошибок у пользователей.
    /// </para>
    /// </summary>
    public class RemoteExceptionDataProvider : FirstChanceExceptionTrap
    {
      /// <summary>
      /// Обрабатывает исключение в месте его падения.
      /// Метод вызывается в том потоке (thread), где произошло падение исключения. Поэтому реализация метода должна быть thread safe.
      /// Любые исключения в этом методе будут подавлены.
      /// </summary>
      /// <param name="exception">Объект исключения</param>
      protected override void DoProcessException(Exception exception)
      {
        base.DoProcessException(exception);
        this.SaveExceptionData(exception);
      }

      private void SaveExceptionData(Exception exception)
      {
        Func<Exception, bool> saveExceptionData = this.CanSaveExceptionData;
        if (saveExceptionData != null && !saveExceptionData(exception) || RemoteExceptionData.TryGet(exception) != null)
          return;
        RemoteExceptionData remoteExceptionData = new RemoteExceptionData();
        remoteExceptionData.IsUnderConstruction = true;
        this.DoCreateRemoteExceptionDataBuilders(exception, remoteExceptionData);
        RemoteExceptionData.Set(exception, remoteExceptionData);
      }

      /// <summary>
      /// Создает и регистрирует специальные объекты-построители технических сведений об объекте исключения.
      /// Они будут вызваны только перед передачей исключения клиенту.
      /// </summary>
      /// <param name="exception">Объект исключения</param>
      /// <param name="remoteData">Контейнер с техническими сведениями об объекте исключения</param>
      protected virtual void DoCreateRemoteExceptionDataBuilders(
        Exception exception,
        RemoteExceptionData remoteData)
      {
        remoteData.AddBuilder((RemoteExceptionDataBuilder) new RemoteStackTraceBuilder(exception, remoteData));
      }

      /// <summary>
      /// Событие, которое позволяет определить, следует ли собирать сведения об упавшем исключении.
      /// </summary>
      public event Func<Exception, bool> CanSaveExceptionData;
    }
}
