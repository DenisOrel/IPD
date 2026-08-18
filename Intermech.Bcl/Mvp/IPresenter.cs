
// Type: Intermech.Mvp.IPresenter
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.Mvp
{
    /// <summary>
    /// Базовый интерфейс для всех посредников MVP (presenter).
    /// </summary>
    public interface IPresenter
    {
      /// <summary>
      /// Возвращает или задает вид MVP (view), который будет использоваться текущим посредником MVP (presenter).
      /// Подключение посредника к виду будет выполнено при отображении вида на экране, а отключение посредника от вида - при закрытии вида.
      /// Если в момент установки свойства вид отображен на экране, то подключение посредника к виду будет выполнено немедленно.
      /// </summary>
      IView View { get; set; }

      /// <summary>
      /// Возвращает интерфейс вида MVP (view), требуемого этому посреднику MVP (presenter).
      /// </summary>
      Type ViewInterface { get; }
    }
}
