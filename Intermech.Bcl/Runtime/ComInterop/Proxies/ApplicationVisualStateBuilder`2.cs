
// Type: Intermech.Runtime.ComInterop.Proxies.ApplicationVisualStateBuilder`2
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Collections.Generic;


namespace Intermech.Runtime.ComInterop.Proxies
{
    /// <summary>Построитель для сохраненного состояния UI приложения.</summary>
    /// <remarks>Реализация не является thread safe.</remarks>
    public abstract class ApplicationVisualStateBuilder<TApplication, TFlags> where TFlags : Enum
    {
      private readonly TFlags emptyFlags;
      private static readonly ApplicationVisualState<TApplication> emptyState = new ApplicationVisualState<TApplication>((ICollection<ApplicationVisualStateItem<TApplication>>) new ApplicationVisualStateItem<TApplication>[0]);

      /// <summary>Создает объект.</summary>
      /// <param name="emptyFlags">Пустой набор флагов, определяющих сохраняемые элементы UI</param>
      protected ApplicationVisualStateBuilder(TFlags emptyFlags) => this.emptyFlags = emptyFlags;

      /// <summary>Сохраняет состояние UI приложения.</summary>
      /// <param name="application">Объект приложения</param>
      /// <param name="flags">Набор флагов, определяющих сохраняемые элементы UI</param>
      /// <returns>Контейнер с сохраненным состоянием</returns>
      /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="application" /> содержит null</exception>
      public ApplicationVisualState<TApplication> SaveState(TApplication application, TFlags flags)
      {
        if ((object) application == null)
          throw new ArgumentNullException(nameof (application));
        if (flags.Equals((object) this.emptyFlags))
          return ApplicationVisualStateBuilder<TApplication, TFlags>.emptyState;
        List<ApplicationVisualStateItem<TApplication>> stateItems = new List<ApplicationVisualStateItem<TApplication>>();
        this.DoSaveState(application, flags, stateItems);
        return new ApplicationVisualState<TApplication>((ICollection<ApplicationVisualStateItem<TApplication>>) stateItems);
      }

      /// <summary>Сохраняет состояние UI приложения.</summary>
      /// <param name="application">Объект приложения</param>
      /// <param name="flags">Набор флагов, определяющих сохраняемые элементы UI</param>
      /// <param name="stateItems">Коллекция сохраненных элементов UI</param>
      protected virtual void DoSaveState(
        TApplication application,
        TFlags flags,
        List<ApplicationVisualStateItem<TApplication>> stateItems)
      {
      }
    }
}
