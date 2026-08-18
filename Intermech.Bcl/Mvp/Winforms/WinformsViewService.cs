
// Type: Intermech.Mvp.Winforms.WinformsViewService
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Mvp.Components.Dialogs;
using Intermech.Mvp.Native.Windows.Dialogs;
using System;
using System.Diagnostics;
using System.Windows.Forms;


namespace Intermech.Mvp.Winforms
{
    /// <summary>
    /// Реализует сервис видов MVP, созданных на основе Windows Forms.
    /// </summary>
    /// <remarks>
    /// Сервис видов обеспечивает абстрагирование от используемой технологии и решает следующие задачи -
    /// создание видов, отображение видов на экране в модальном и немодальном режиме, и др.
    /// </remarks>
    public sealed class WinformsViewService : AbstractViewService
    {
      private static readonly WinformsViewService defaultInstance = new WinformsViewService();
      private Type formBaseType;
      private Type controlBaseType;
      private Type systemDialogBaseType;

      /// <summary>Создает объект.</summary>
      public WinformsViewService()
      {
        this.formBaseType = typeof (Form);
        this.controlBaseType = typeof (Control);
        this.systemDialogBaseType = typeof (SystemDialogWrapper);
        this.RegisterSystemDialogs();
      }

      private void RegisterSystemDialogs()
      {
        this.RegisterView(typeof (ISimpleMessageView), typeof (SimpleMessageDialogWrapper));
        this.RegisterView(typeof (IYesNoMessageView), typeof (YesNoMessageDialogWrapper));
        this.RegisterView(typeof (IOpenFileView), typeof (OpenFileDialogWrapper));
        this.RegisterView(typeof (ISaveFileView), typeof (SaveFileDialogWrapper));
        this.RegisterView(typeof (IFolderBrowserView), typeof (FolderBrowserDialogWrapper));
      }

      /// <summary>
      /// Проверяет, соответствие реализации вида его интерфейсу, а также корректность реализации, наличие необходимых аннотаций и так далее.
      /// </summary>
      /// <param name="viewInterface">Интерфейс вида</param>
      /// <param name="viewImplementation">Реализация вида</param>
      /// <exception cref="T:MvpException">Реализация вида не соответствует интерфейсу, либо реализация некорректна, либо отсутствуют необходимые аннотации</exception>
      protected override void DoValidateViewType(Type viewInterface, Type viewImplementation)
      {
        base.DoValidateViewType(viewInterface, viewImplementation);
        if (!this.IsWinformsVisual(viewImplementation))
          throw new MvpException($"Указанный тип вида '{viewImplementation.Name}' должен быть унаследован от '{this.formBaseType}' или '{this.controlBaseType}'.");
      }

      /// <summary>
      /// Проверяет, может ли указанный тип являться реализацией вида MVP с указанный интерфейсом вида.
      /// Метод используется при автоматическом поиске реализации вида в сборке с реализацией посредника MVP, в случае,
      /// когда реализация вида не была явно зарегистрирована в сервисе видов.
      /// </summary>
      /// <param name="viewInterface">Интерфейс вида</param>
      /// <param name="viewImplementation">Реализация вида</param>
      /// <returns>true - если это подходящая реализация вида; иначе - false</returns>
      protected override bool IsSuitableViewType(Type viewInterface, Type viewImplementation)
      {
        return base.IsSuitableViewType(viewInterface, viewImplementation) && this.IsWinformsVisual(viewImplementation);
      }

      private bool IsWinformsVisual(Type viewImplementation)
      {
        return this.formBaseType.IsAssignableFrom(viewImplementation) || this.controlBaseType.IsAssignableFrom(viewImplementation) || this.systemDialogBaseType.IsAssignableFrom(viewImplementation);
      }

      /// <summary>
      /// Отображает вид MVP (view) на экране в немодальном режиме.
      /// </summary>
      /// <param name="view">Вид MVP</param>
      /// <param name="presenter">Посредник MVP. Параметр может быть не задан и равен null</param>
      protected override void DoShow(IView view, IPresenter presenter)
      {
        ((Control) view).Show();
        Application.DoEvents();
      }

      /// <summary>
      /// Отображает вид MVP (view) на экране в модальном режиме.
      /// </summary>
      /// <param name="view">Вид MVP</param>
      /// <param name="presenter">Посредник MVP. Параметр может быть не задан и равен null</param>
      /// <param name="ownerView">Вид-владелец. Параметр может быть не задан и равен null</param>
      protected override void DoShowModal(IView view, IPresenter presenter, object ownerView = null)
      {
        if (view is SystemDialogWrapper)
        {
          ((SystemDialogWrapper) view).ShowDialog((IWin32Window) ownerView);
        }
        else
        {
          int num = (int) ((Form) view).ShowDialog((IWin32Window) ownerView);
        }
      }

      /// <summary>
      /// Запускает цикл обработки сообщений приложения для вида MVP (view).
      /// </summary>
      /// <param name="view">Вид MVP</param>
      /// <param name="presenter">Посредник MVP. Параметр может быть не задан и равен null</param>
      protected override void DoRunApplication(IView view, IPresenter presenter)
      {
        Application.Run((Form) view);
      }

      /// <summary>Активирует вид MVP и переводит на него фокус ввода.</summary>
      /// <param name="view">Вид MVP</param>
      protected override void DoActivateView(IView view)
      {
        ((Form) view).Activate();
        Application.DoEvents();
      }

      /// <summary>
      /// Возвращает экземпляр объекта, используемый по умолчанию.
      /// </summary>
      public static WinformsViewService Default
      {
        [DebuggerStepThrough] get => WinformsViewService.defaultInstance;
      }
    }
}
