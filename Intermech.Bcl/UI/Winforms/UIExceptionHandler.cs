
// Type: Intermech.UI.Winforms.UIExceptionHandler
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Threading;
using System.Windows.Forms;


namespace Intermech.UI.Winforms
{
    /// <summary>
    /// Позволяет перехватить все необработанные исключения в UI-потоке приложения и отобразить/записать их с помощью указанного обработчика.
    /// Реализация класса не является thread safe.
    /// </summary>
    public sealed class UIExceptionHandler
    {
      private Action<Exception> exceptionHandler;
      private bool isActive;

      /// <summary>Создает объект.</summary>
      /// <param name="exceptionHandler">Обработчик исключений UI-потока приложения</param>
      /// <exception cref="T:ArgumentNullException">Параметр <paramref name="exceptionHandler" /> не должен быть равен null</exception>
      public UIExceptionHandler(Action<Exception> exceptionHandler)
      {
        this.exceptionHandler = exceptionHandler != null ? exceptionHandler : throw new ArgumentNullException(nameof (exceptionHandler));
      }

      /// <summary>Активирует обработчик.</summary>
      public void Activate()
      {
        if (this.isActive)
          return;
        Application.ThreadException += new ThreadExceptionEventHandler(this.OnUIThreadException);
        this.isActive = true;
      }

      /// <summary>Деактивирует обработчик.</summary>
      public void Deactivate()
      {
        if (!this.isActive)
          return;
        Application.ThreadException -= new ThreadExceptionEventHandler(this.OnUIThreadException);
        this.isActive = false;
      }

      private void OnUIThreadException(object sender, ThreadExceptionEventArgs e)
      {
        this.exceptionHandler(e.Exception);
      }
    }
}
