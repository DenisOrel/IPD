
// Type: Intermech.UI.ICloseableViewModel
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System.ComponentModel;


namespace Intermech.UI
{
    /// <summary>
    /// Интерфейс для моделей вида, поддерживающих концепцию завершения работы.
    /// Если модель завершает свою работу, то вид, связанный с моделью, будет
    /// автоматически закрыт.
    /// </summary>
    public interface ICloseableViewModel : INotifyPropertyChanged
    {
      /// <summary>
      /// Возвращает признак, что модель вида завершила работу и не может больше использоваться.
      /// </summary>
      bool IsClosed { get; }

      /// <summary>
      /// Завершает работу модели вида, если это еще не было сделано.
      /// </summary>
      void Close();
    }
}
