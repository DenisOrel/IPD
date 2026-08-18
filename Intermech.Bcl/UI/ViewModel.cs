
// Type: Intermech.UI.ViewModel
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace Intermech.UI
{
    /// <summary>Базовый класс для всех моделей вида.</summary>
    public abstract class ViewModel : INotifyPropertyChanged
    {
      /// <summary>Запускает событие изменения свойства модели вида.</summary>
      /// <param name="propertyName">Имя свойства модели вида</param>
      protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
      {
        if (this.PropertyChanged == null || propertyName == null)
          return;
        this.PropertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
      }

      /// <summary>Событие изменения свойства модели вида.</summary>
      public event PropertyChangedEventHandler PropertyChanged;
    }
}
