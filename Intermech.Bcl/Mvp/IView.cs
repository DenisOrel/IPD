
// Type: Intermech.Mvp.IView
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml


namespace Intermech.Mvp
{
    /// <summary>Базовый интерфейс для всех видов MVP (view).</summary>
    public interface IView
    {
      /// <summary>
      /// Возвращает состояние вида MVP (view). Объект состояния вида используется посредником MVP (presenter) для подключения к виду и отключения от него.
      /// </summary>
      IViewDisplayState DisplayState { get; }
    }
}
