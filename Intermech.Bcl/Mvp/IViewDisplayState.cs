
// Type: Intermech.Mvp.IViewDisplayState
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.Mvp
{
    /// <summary>
    /// Интерфейс состояния вида MVP (view). Он предоставляет свойства и события изменения состояния вида,
    /// и используется посредником MVP (presenter) для подключения к виду при отображении вида на экране.
    /// </summary>
    public interface IViewDisplayState
    {
      /// <summary>
      /// Возвращает true, если вид MVP (view) отображен на экране.
      /// </summary>
      bool IsViewShown { get; }

      /// <summary>
      /// Событие срабатывает, когда вид MVP (view) появляется на экране. По этому событию происходит
      /// подключение посредника MVP (presenter) к виду в методе <see cref="M:Intermech.Mvp.Presenter.OnAttachView()" />.
      /// </summary>
      event EventHandler ViewShown;

      /// <summary>
      /// Событие срабатывает, когда вид MVP (view) закрывается. По этому событию происходит
      /// отключение посредника MVP (presenter) от вида в методе <see cref="M:Intermech.Mvp.Presenter.OnDetachView()" />.
      /// </summary>
      event EventHandler ViewClosed;
    }
}
