
// Type: Intermech.UI.ContextMenuViewModel`1
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;


namespace Intermech.UI
{
    /// <summary>
    /// Класс модели вида для динамически создаваемого контекстного меню.
    /// </summary>
    /// <typeparam name="T">Тип моделей вида элементов контекстного меню</typeparam>
    public class ContextMenuViewModel<T> : ViewModel where T : ICommand
    {
      private ObservableCollection<T> items;

      /// <summary>Создает объект.</summary>
      public ContextMenuViewModel() => this.items = new ObservableCollection<T>();

      /// <summary>Возвращает признак, что контекстное меню не пусто.</summary>
      public bool HasItems
      {
        [DebuggerStepThrough] get => this.items.Count != 0;
      }

      /// <summary>Возвращает коллекцию элементов контекстного меню.</summary>
      public ObservableCollection<T> Items
      {
        [DebuggerStepThrough] get => this.items;
      }
    }
}
