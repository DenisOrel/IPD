
// Type: Intermech.Interfaces.IExceptionHandlerService
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса для обработки исключительных ситуаций с возможностью подключения пользовательских обработчиков.
    /// </summary>
    public interface IExceptionHandlerService
    {
      /// <summary>
      /// Вызывается при возникновении в системе необработанного исключения.
      /// Обработчики вызываются в порядке регистрации до тех пор, пока кто-то из них не установит свойство Handled в true.
      /// </summary>
      event ExceptionHandler HandleException;

      void ShowException(Exception exception);

      [Obsolete("Do not use this method anymore.", true)]
      void NotifyException(Exception exception);

      [Obsolete("Do not use this method anymore.", true)]
      void NotifyException(Exception exception, int timeout);
    }
}
