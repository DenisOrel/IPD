
// Type: Intermech.Mvp.AbstractViewService
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Threading;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;


namespace Intermech.Mvp
{
    /// <summary>
    /// Базовый класс для сервиса видов MVP, реализованных с помощью определенной технологии - Windows Forms, WPF и др.
    /// </summary>
    /// <remarks>
    /// Сервис видов обеспечивает абстрагирование от используемой технологии и решает следующие задачи -
    /// создание видов, отображение видов на экране в модальном и немодальном режиме, и др.
    /// </remarks>
    public abstract class AbstractViewService
    {
      private static readonly Type IViewType = typeof (IView);
      private ReaderWriterLockSlim viewRegistryRwl;
      private Dictionary<Type, Type> viewInterfaceTable;

      /// <summary>Создает объект.</summary>
      protected AbstractViewService()
      {
        this.viewRegistryRwl = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);
        this.viewInterfaceTable = new Dictionary<Type, Type>();
      }

      /// <summary>
      /// Регистрирует реализацию вида MVP (view), связывая реализацию вида с указанным интерфейсом вида.
      /// </summary>
      /// <param name="viewInterface">Интерфейс вида</param>
      /// <param name="viewImplementation">Реализация вида</param>
      /// <exception cref="T:ArgumentNullException">viewInterface || viewImplementation</exception>
      /// <exception cref="T:MvpException">Реализация вида не соответствует интерфейсу вида, либо реализация некорректна, либо отсутствуют необходимые аннотации</exception>
      public void RegisterView(Type viewInterface, Type viewImplementation)
      {
        if (viewInterface == (Type) null)
          throw new ArgumentNullException(nameof (viewInterface));
        if (viewImplementation == (Type) null)
          throw new ArgumentNullException(nameof (viewImplementation));
        this.DoValidateViewType(viewInterface, viewImplementation);
        using (new DataWriteLockSlim(this.viewRegistryRwl))
          this.viewInterfaceTable[viewInterface] = viewImplementation;
      }

      /// <summary>Отменяет регистрацию вида MVP (view).</summary>
      /// <param name="viewInterface">Интерфейс вида</param>
      /// <exception cref="T:ArgumentNullException">viewInterface</exception>
      public void UnregisterView(Type viewInterface)
      {
        if (viewInterface == (Type) null)
          throw new ArgumentNullException(nameof (viewInterface));
        using (new DataWriteLockSlim(this.viewRegistryRwl))
          this.viewInterfaceTable.Remove(viewInterface);
      }

      /// <summary>
      /// Проверяет, соответствие реализации вида его интерфейсу, а также корректность реализации, наличие необходимых аннотаций и так далее.
      /// </summary>
      /// <param name="viewInterface">Интерфейс вида</param>
      /// <param name="viewImplementation">Реализация вида</param>
      /// <exception cref="T:MvpException">Реализация вида не соответствует интерфейсу, либо реализация некорректна, либо отсутствуют необходимые аннотации</exception>
      protected virtual void DoValidateViewType(Type viewInterface, Type viewImplementation)
      {
        this.CheckViewInterfaceBaseType(viewInterface);
        if (!viewInterface.IsAssignableFrom(viewImplementation))
          throw new MvpException($"Вид MVP '{viewImplementation}' не реализует указанный интерфейс вида '{viewInterface}'.");
      }

      /// <summary>
      /// Находит и возвращает зарегистрированный тип реализации вида MVP (view), соответствующий указанному интерфейсу вида.
      /// </summary>
      /// <param name="viewInterface">Интерфейс вида</param>
      /// <returns>Тип реализации вида или null, если тип не был зарегистрирован</returns>
      /// <exception cref="T:ArgumentNullException">viewInterface</exception>
      public Type FindRegisteredViewType(Type viewInterface)
      {
        if (viewInterface == (Type) null)
          throw new ArgumentNullException(nameof (viewInterface));
        using (new DataReadLockSlim(this.viewRegistryRwl))
        {
          Type type;
          return this.viewInterfaceTable.TryGetValue(viewInterface, out type) ? type : (Type) null;
        }
      }

      /// <summary>
      /// Находит и возвращает тип реализации вида MVP (view), соответствующий интерфейсу вида, полученному у посредника MVP.
      /// Если реализация вида не была зарегистрирована, то метод ищет реализацию вида в сборке, содержащей реализацию посредника MVP.
      /// </summary>
      /// <param name="presenter">Посредник MVP</param>
      /// <returns>Реализация вида или null, если реализация вида не была зарегистрирована или найдена в сборке с реализацией посредника</returns>
      /// <exception cref="T:ArgumentNullException">presenter</exception>
      /// <remarks>
      /// Метод используется в тех случаях, когда используется только одна технология создания видов - Windows Forms, WPF и др.
      /// В этом случае можно не выпонять предварительную регистрацию реализаций видов. Вместо этого реализации видов MVP размещаются в
      /// той же сборке, что и посредники MVP, а сервис видов находит их самостоятельно.
      /// </remarks>
      public Type FindSuitableViewType(IPresenter presenter)
      {
        Type viewInterface = presenter != null ? presenter.ViewInterface : throw new ArgumentNullException(nameof (presenter));
        Type viewImplementation = this.FindRegisteredViewType(viewInterface);
        if (viewImplementation == (Type) null)
        {
          viewImplementation = this.FindSuitableViewTypeIsAssembly(viewInterface, presenter.GetType().Assembly);
          if (viewImplementation != (Type) null)
            this.RegisterView(viewInterface, viewImplementation);
        }
        return viewImplementation;
      }

      private Type FindSuitableViewTypeIsAssembly(Type viewInterface, Assembly assembly)
      {
        foreach (Type type in assembly.GetTypes())
        {
          if (!type.IsAbstract && viewInterface.IsAssignableFrom(type) && this.IsSuitableViewType(viewInterface, type))
            return type;
        }
        return (Type) null;
      }

      /// <summary>
      /// Проверяет, может ли указанный тип являться реализацией вида MVP с указанный интерфейсом вида.
      /// Метод используется при автоматическом поиске реализации вида в сборке с реализацией посредника MVP, в случае,
      /// когда реализация вида не была явно зарегистрирована в сервисе видов.
      /// </summary>
      /// <param name="viewInterface">Интерфейс вида</param>
      /// <param name="viewImplementation">Реализация вида</param>
      /// <returns>true - если это подходящая реализация вида; иначе - false</returns>
      protected virtual bool IsSuitableViewType(Type viewInterface, Type viewImplementation) => true;

      private void CheckViewInterfaceBaseType(Type viewInterface)
      {
        if (!AbstractViewService.IViewType.IsAssignableFrom(viewInterface))
          throw new MvpException($"Интерфейс вида MVP '{viewInterface}' должен быть унаследован от базового интерфейса для всех видов MVP '{AbstractViewService.IViewType}'.");
      }

      /// <summary>
      /// Создает и возвращает вид MVP (view) для указанного интерфейса вида.
      /// </summary>
      /// <param name="viewInterface">Интерфейс вида</param>
      /// <returns>Экземпляр вида MVP</returns>
      /// <exception cref="T:ArgumentNullException">viewInterface</exception>
      /// <exception cref="T:MvpException">Невозможно создать вид MVP для указанного интерфейса вида, так как реализация вида не была зарегистрирована</exception>
      public IView CreateView(Type viewInterface)
      {
        Type viewImplementation = !(viewInterface == (Type) null) ? this.FindRegisteredViewType(viewInterface) : throw new ArgumentNullException(nameof (viewInterface));
        return viewImplementation != (Type) null ? this.DoCreateView(viewInterface, viewImplementation) : throw new MvpException($"Невозможно создать вид MVP для интерфейса вида '{viewInterface}', так как реализация вида не была зарегистрирована в сервисе видов.");
      }

      /// <summary>
      /// Создает вида MVP (view) для указанного посредника MVP (presenter) и связывает их друг с другом, если у посредника еще нет вида.
      /// Для поиска реализации вида используется не только зарегистрированные виды, но и все подходящие типы из сборки с реализацией посредника.
      /// </summary>
      /// <param name="presenter">Посредник MVP</param>
      /// <exception cref="T:ArgumentNullException">presenter</exception>
      /// <exception cref="T:MvpException">Невозможно создать вид MVP для указанного посредника, так как не удалось найти реализацию вида</exception>
      public void CreateViewIfMissing(IPresenter presenter)
      {
        if (presenter == null)
          throw new ArgumentNullException(nameof (presenter));
        if (presenter.View != null)
          return;
        Type viewInterface = presenter.ViewInterface;
        Type suitableViewType = this.FindSuitableViewType(presenter);
        IView view = suitableViewType != (Type) null ? this.DoCreateView(viewInterface, suitableViewType) : throw new MvpException($"Невозможно создать вид MVP для посредника '{presenter.GetType()}', так как не удалось найти реализацию вида для интерфейса вида '{viewInterface}'.");
        presenter.View = view;
      }

      /// <summary>Создает экземпляр вида.</summary>
      /// <param name="viewInterface">Интерфейс вида</param>
      /// <param name="viewImplementation">Реализация вида</param>
      /// <returns>Экземпляр вида MVP</returns>
      protected virtual IView DoCreateView(Type viewInterface, Type viewImplementation)
      {
        return (IView) Activator.CreateInstance(viewImplementation);
      }

      /// <summary>
      /// Отображает вид MVP (view) на экране в немодальном режиме.
      /// </summary>
      /// <param name="IView">Вид MVP</param>
      /// <exception cref="T:ArgumentNullException">view</exception>
      public void Show(IView view)
      {
        if (view == null)
          throw new ArgumentNullException(nameof (view));
        this.DoShow(view);
      }

      /// <summary>
      /// Отображает связанный с посредником вид MVP (view) на экране в немодальном режиме.
      /// Если вид еще не создан, то он будет создан автоматически.
      /// </summary>
      /// <param name="presenter">Посредник MVP</param>
      /// <exception cref="T:ArgumentNullException">presenter</exception>
      public void Show(IPresenter presenter)
      {
        if (presenter == null)
          throw new ArgumentNullException(nameof (presenter));
        this.CreateViewIfMissing(presenter);
        this.DoShow(presenter.View, presenter);
      }

      /// <summary>
      /// Отображает вид MVP (view) на экране в немодальном режиме.
      /// </summary>
      /// <param name="view">Вид MVP</param>
      /// <param name="presenter">Посредник MVP. Параметр может быть не задан и равен null</param>
      protected abstract void DoShow(IView view, IPresenter presenter = null);

      /// <summary>
      /// Отображает связанный с посредником вид MVP (view) на экране в модальном режиме.
      /// Если вид еще не создан, то он будет создан автоматически.
      /// </summary>
      /// <param name="presenter">Посредник MVP</param>
      /// <param name="ownerView">Вид-владелец. Параметр может быть не задан и равен null</param>
      /// <exception cref="T:ArgumentNullException">presenter</exception>
      public void ShowModal(IPresenter presenter, object ownerView = null)
      {
        if (presenter == null)
          throw new ArgumentNullException(nameof (presenter));
        this.CreateViewIfMissing(presenter);
        this.DoShowModal(presenter.View, presenter, ownerView);
      }

      /// <summary>
      /// Отображает вид MVP (view) на экране в модальном режиме.
      /// </summary>
      /// <param name="view">Вид MVP</param>
      /// <param name="presenter">Посредник MVP. Параметр может быть не задан и равен null</param>
      /// <param name="ownerView">Вид-владелец. Параметр может быть не задан и равен null</param>
      protected abstract void DoShowModal(IView view, IPresenter presenter = null, object ownerView = null);

      /// <summary>
      /// Запускает цикл обработки сообщений приложения для вида MVP (view).
      /// </summary>
      /// <param name="view">Вид MVP</param>
      /// <exception cref="T:ArgumentNullException">view</exception>
      public void RunApplication(IView view)
      {
        if (view == null)
          throw new ArgumentNullException(nameof (view));
        this.DoRunApplication(view);
      }

      /// <summary>
      /// Запускает цикл обработки сообщений приложения для связанного с посредником вида MVP (view).
      /// Если вид еще не создан, то он будет создан автоматически.
      /// </summary>
      /// <param name="presenter">Посредник MVP</param>
      /// <exception cref="T:ArgumentNullException">presenter</exception>
      public void RunApplication(IPresenter presenter)
      {
        if (presenter == null)
          throw new ArgumentNullException(nameof (presenter));
        this.CreateViewIfMissing(presenter);
        this.DoRunApplication(presenter.View, presenter);
      }

      /// <summary>
      /// Запускает цикл обработки сообщений приложения для вида MVP (view).
      /// </summary>
      /// <param name="view">Вид MVP</param>
      /// <param name="presenter">Посредник MVP. Параметр может быть не задан и равен null</param>
      protected abstract void DoRunApplication(IView view, IPresenter presenter = null);

      /// <summary>
      /// Активирует вид MVP и переводит на него фокус ввода. Вид должен быть отображен на экране.
      /// </summary>
      /// <param name="view">Вид MVP</param>
      /// <exception cref="T:ArgumentNullException">view</exception>
      public void ActivateView(IView view)
      {
        if (view == null)
          throw new ArgumentNullException(nameof (view));
        if (!view.DisplayState.IsViewShown)
          throw new MvpException($"Не удалось активировать вид MVP '{view.GetType()}' и передать ему фокус ввода, так как он не отображен на экране.");
        this.DoActivateView(view);
      }

      /// <summary>Активирует вид MVP и переводит на него фокус ввода.</summary>
      /// <param name="view">Вид MVP</param>
      protected abstract void DoActivateView(IView view);
    }
}
