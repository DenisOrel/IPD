
// Type: Intermech.ApplicationModel.ExceptionDisplayService
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Diagnostics;
using System;


namespace Intermech.ApplicationModel
{
    /// <summary>
    /// Сервис для отображения исключительных ситуаций.
    /// Реализация сервиса является thread safe.
    /// </summary>
    public sealed class ExceptionDisplayService : IExceptionDisplayService
    {
      private IAlertMessageService alertMessageService;

      /// <summary>Создает объект.</summary>
      /// <param name="alertMessageService">Сервис отображения исключительных событий в приложении</param>
      /// <exception cref="T:ArgumentNullException">Параметр <paramref name="alertMessageService" /> не должен быть равен null</exception>
      public ExceptionDisplayService(IAlertMessageService alertMessageService)
      {
        this.alertMessageService = alertMessageService != null ? alertMessageService : throw new ArgumentNullException(nameof (alertMessageService));
      }

      /// <summary>
      /// Показывает сообщение пользователю с информацией об исключении.
      /// </summary>
      /// <param name="exception">Объект исключения</param>
      /// <exception cref="T:ArgumentNullException">Параметр <paramref name="exception" /> не должен быть равен null</exception>
      public void ShowException(Exception exception)
      {
        if (exception == null)
          throw new ArgumentNullException(nameof (exception));
        this.alertMessageService.ShowMessage("Необработанное исключение", ExceptionServices.GetExtendedExceptionText(exception), AlertMessageType.Error);
      }
    }
}
