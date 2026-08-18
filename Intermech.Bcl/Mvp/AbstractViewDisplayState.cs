
// Type: Intermech.Mvp.AbstractViewDisplayState
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Diagnostics;


namespace Intermech.Mvp
{
    /// <summary>Базовый класс для объектов состояния вида MVP (view).</summary>
    public abstract class AbstractViewDisplayState : IViewDisplayState
    {
      protected EventHandler ViewShownHandler;
      protected EventHandler ViewClosedHandler;
      private bool isViewShown;

      /// <summary>Задает начальное состояние объекта.</summary>
      /// <param name="isViewShown">Признак, что вид отображен на экране</param>
      protected void SetInitialState(bool isViewShown) => this.isViewShown = isViewShown;

      /// <summary>
      /// Возвращает true, если вид MVP (view) отображен на экране.
      /// </summary>
      public bool IsViewShown
      {
        [DebuggerStepThrough] get => this.isViewShown;
      }

      /// <summary>
      /// Событие срабатывает, когда вид MVP (view) появляется на экране. По этому событию происходит
      /// подключение посредника MVP (presenter) к виду в методе <see cref="M:Intermech.Mvp.Presenter.OnAttachView()" />.
      /// </summary>
      public event EventHandler ViewShown
      {
        [DebuggerStepThrough] add => this.ViewShownHandler += value;
        [DebuggerStepThrough] remove => this.ViewShownHandler -= value;
      }

      /// <summary>
      /// Событие срабатывает, когда вид MVP (view) закрывается. По этому событию происходит
      /// отключение посредника MVP (presenter) от вида в методе <see cref="M:Intermech.Mvp.Presenter.OnDetachView()" />.
      /// </summary>
      public event EventHandler ViewClosed
      {
        [DebuggerStepThrough] add => this.ViewClosedHandler += value;
        [DebuggerStepThrough] remove => this.ViewClosedHandler -= value;
      }

      protected void RaiseViewShown()
      {
        if (this.isViewShown)
          return;
        this.isViewShown = true;
        if (this.ViewShownHandler == null)
          return;
        this.ViewShownHandler((object) this, EventArgs.Empty);
      }

      protected void RaiseViewClosed()
      {
        if (!this.isViewShown)
          return;
        this.isViewShown = false;
        if (this.ViewClosedHandler == null)
          return;
        this.ViewClosedHandler((object) this, EventArgs.Empty);
      }
    }
}
