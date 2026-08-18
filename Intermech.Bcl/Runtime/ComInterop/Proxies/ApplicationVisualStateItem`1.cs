
// Type: Intermech.Runtime.ComInterop.Proxies.ApplicationVisualStateItem`1
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.Runtime.ComInterop.Proxies
{
    /// <summary>
    /// Базовый класс для элемента сохраненного состояния UI приложения.
    /// </summary>
    /// <remarks>Реализация не должна быть thread safe.</remarks>
    public class ApplicationVisualStateItem<TApplication>
    {
      /// <summary>
      /// Заполняет элемент, сохраняя текущее состояние UI приложения.
      /// </summary>
      /// <param name="application">Объект приложения</param>
      /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="application" /> содержит null</exception>
      public void SaveState(TApplication application)
      {
        if ((object) application == null)
          throw new ArgumentNullException(nameof (application));
        this.DoSaveState(application);
      }

      /// <summary>
      /// Заполняет элемент, сохраняя текущее состояние UI приложения.
      /// </summary>
      /// <param name="application">Объект приложения</param>
      protected virtual void DoSaveState(TApplication application)
      {
      }

      /// <summary>
      /// Восстанавливает элемент, используя сохраненное состояние UI приложения.
      /// </summary>
      /// <param name="application">Объект приложения</param>
      /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="application" /> содержит null</exception>
      public void RestoreState(TApplication application)
      {
        if ((object) application == null)
          throw new ArgumentNullException(nameof (application));
        this.DoRestoreState(application);
      }

      /// <summary>
      /// Восстанавливает элемент, используя сохраненное состояние UI приложения.
      /// </summary>
      /// <param name="application">Объект приложения</param>
      protected virtual void DoRestoreState(TApplication application)
      {
      }
    }
}
