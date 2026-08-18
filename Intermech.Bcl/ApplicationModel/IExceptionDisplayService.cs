
// Type: Intermech.ApplicationModel.IExceptionDisplayService
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.ApplicationModel
{
    /// <summary>
    /// Интерфейс сервиса для отображения исключительных ситуаций.
    /// </summary>
    public interface IExceptionDisplayService
    {
      /// <summary>
      /// Показывает сообщение пользователю с информацией об исключении.
      /// </summary>
      /// <param name="exception">Объект исключения</param>
      /// <exception cref="T:ArgumentNullException">Параметр <paramref name="exception" /> не должен быть равен null</exception>
      void ShowException(Exception exception);
    }
}
