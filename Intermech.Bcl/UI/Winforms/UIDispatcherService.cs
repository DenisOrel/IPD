
// Type: Intermech.UI.Winforms.UIDispatcherService
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Threading;
using System.Windows.Forms;


namespace Intermech.UI.Winforms
{
    public sealed class UIDispatcherService : IUIDispatcherService
    {
      private int uiThreadId;
      private SynchronizationContext uiSynchronizationContext;

      public UIDispatcherService(int uiThreadId, SynchronizationContext uiContext)
      {
        if (uiContext == null)
          throw new ArgumentNullException(nameof (uiContext));
        this.uiThreadId = uiThreadId;
        this.uiSynchronizationContext = uiContext;
      }

      /// <summary>
      /// Создает сервис диспетчера UI-потока, используя текущий поток, который должен быть UI-потоком приложения System.Windows.Forms.
      /// </summary>
      /// <returns>Объект сервиса</returns>
      /// <exception cref="T:InvalidOperationException">Текущий поток не является UI-потоком System.Windows.Forms</exception>
      public static UIDispatcherService FromCurrentUIThread()
      {
        WindowsFormsSynchronizationContext current = (WindowsFormsSynchronizationContext) SynchronizationContext.Current;
        if (current == null)
        {
          new Control().Dispose();
          current = (WindowsFormsSynchronizationContext) SynchronizationContext.Current;
          if (current == null)
            throw new InvalidOperationException("Текущий поток не является UI-потоком System.Windows.Forms");
        }
        return new UIDispatcherService(Thread.CurrentThread.ManagedThreadId, (SynchronizationContext) current);
      }

      public bool IsUIThread() => Thread.CurrentThread.ManagedThreadId == this.uiThreadId;

      public void SendToUIThread(SendOrPostCallback action, object state)
      {
        this.uiSynchronizationContext.Send(action, state);
      }

      public void PostToUIThread(SendOrPostCallback action, object state)
      {
        this.uiSynchronizationContext.Post(action, state);
      }
    }
}
