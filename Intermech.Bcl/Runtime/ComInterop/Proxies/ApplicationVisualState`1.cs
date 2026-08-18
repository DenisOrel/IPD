
// Type: Intermech.Runtime.ComInterop.Proxies.ApplicationVisualState`1
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Collections.Generic;


namespace Intermech.Runtime.ComInterop.Proxies
{
    /// <summary>Контейнер для сохраненного состояния UI приложения.</summary>
    /// <remarks>Реализация не является thread safe.</remarks>
    public class ApplicationVisualState<TApplication>
    {
      private ICollection<ApplicationVisualStateItem<TApplication>> stateItems;

      /// <summary>Создает объект.</summary>
      /// <param name="stateItems">Коллекция сохраненных элементов UI</param>
      /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="stateItems" /> содержит null</exception>
      public ApplicationVisualState(
        ICollection<ApplicationVisualStateItem<TApplication>> stateItems)
      {
        this.stateItems = stateItems != null ? stateItems : throw new ArgumentNullException(nameof (stateItems));
      }

      /// <summary>Восстанавливает сохраненное состояние UI приложения.</summary>
      /// <param name="application">Объект приложения</param>
      /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="application" /> содержит null</exception>
      public void RestoreState(TApplication application)
      {
        if ((object) application == null)
          throw new ArgumentNullException(nameof (application));
        if (this.stateItems.Count == 0)
          return;
        foreach (ApplicationVisualStateItem<TApplication> stateItem in (IEnumerable<ApplicationVisualStateItem<TApplication>>) this.stateItems)
          stateItem.RestoreState(application);
      }
    }
}
