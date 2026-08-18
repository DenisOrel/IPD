
// Type: Intermech.Mvp.Presenter`1
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Localization;
using Intermech.Runtime;
using System;
using System.Diagnostics;
using System.Threading;


namespace Intermech.Mvp
{
    /// <summary>
    /// Базовый класс, облегчающий создание посредников MVP (presenter).
    /// </summary>
    /// <typeparam name="TView">Интерфейс вида MVP, используемый посредником MVP</typeparam>
    public abstract class Presenter<TView> : IPresenter where TView : class, IView
    {
      private SilentActionInvoker silentActions;
      private TView view;
      private SynchronizationContext viewSyncContext;
      private volatile bool isAttachedToView;

      /// <summary>Создает объект.</summary>
      public Presenter() => this.silentActions = SilentActionInvoker.Default;

      /// <summary>
      /// Возвращает или задает вид MVP (view), который будет использоваться текущим посредником MVP (presenter).
      /// Подключение посредника к виду будет выполнено при отображении вида на экране, а отключение посредника от вида - при закрытии вида.
      /// Если в момент установки свойства вид отображен на экране, то подключение посредника к виду будет выполнено немедленно.
      /// </summary>
      public TView View
      {
        [DebuggerStepThrough] get => this.view;
        [DebuggerStepThrough] set
        {
          if ((object) this.view == (object) value)
            return;
          this.ChangeView(value);
        }
      }

      private void ChangeView(TView newView)
      {
        if ((object) this.view != null)
        {
          this.view.DisplayState.ViewShown -= new EventHandler(this.OnViewShown);
          this.view.DisplayState.ViewClosed -= new EventHandler(this.OnViewClosed);
          this.DetachView();
        }
        this.view = newView;
        if ((object) this.view == null)
          return;
        this.view.DisplayState.ViewShown += new EventHandler(this.OnViewShown);
        this.view.DisplayState.ViewClosed += new EventHandler(this.OnViewClosed);
        if (!this.view.DisplayState.IsViewShown)
          return;
        this.OnViewShown((object) this.view, EventArgs.Empty);
      }

      /// <summary>
      /// Возвращает или задает вид MVP (view), который будет использоваться текущим посредником MVP (presenter).
      /// Подключение посредника к виду будет выполнено при отображении вида на экране, а отключение посредника от вида - при закрытии вида.
      /// Если в момент установки свойства вид отображен на экране, то подключение посредника к виду будет выполнено немедленно.
      /// </summary>
      IView IPresenter.View
      {
        [DebuggerStepThrough] get => (IView) this.View;
        [DebuggerStepThrough] set => this.View = (TView) value;
      }

      /// <summary>
      /// Возвращает интерфейс вида MVP (view), требуемого этому посреднику MVP (presenter).
      /// </summary>
      public Type ViewInterface
      {
        [DebuggerStepThrough] get => typeof (TView);
      }

      /// <summary>
      /// Возвращает true, если посредник MVP (presenter) подключен к виду MVP (view).
      /// </summary>
      /// <remarks>
      /// Обычно это свойство используется посредником для проверки возможности обновления вида из
      /// асинхронных обработчиков событий и фоновых потоков, так как в этом случае есть вероятность
      /// отключения посредника от вида до срабатывания обработчика.
      /// </remarks>
      public bool IsAttachedToView
      {
        [DebuggerStepThrough] get => this.isAttachedToView;
      }

      /// <summary>
      /// Возвращает контекст синхронизации доступа к UI-потоку вида MVP (view).
      /// Контекст синхронизации используется посредником MVP (presenter) для обновления вида из фоновых потоков.
      /// Свойство может возвращать null, если посредник не подключен к виду.
      /// </summary>
      public SynchronizationContext SynchronizationContext
      {
        [DebuggerStepThrough] get => this.viewSyncContext;
      }

      private void OnViewShown(object sender, EventArgs e)
      {
        this.DoValidate();
        try
        {
          this.OnAttachView();
          this.isAttachedToView = true;
        }
        catch
        {
          this.OnDetachViewSilently();
          this.isAttachedToView = false;
          throw;
        }
        this.OnAfterAttachView();
      }

      private void OnViewClosed(object sender, EventArgs e) => this.DetachView();

      /// <summary>
      /// Позволяет проверить корректность инициализации посредника MVP (presenter).
      /// Метод вызывается непосредственно перед подключением посредника к виду MVP (view).
      /// Необработанное исключение в этом методе прерывает процесс подключения.
      /// </summary>
      /// <exception cref="T:Intermech.Mvp.PresenterPropertyException">Указанное свойство посредника некорректно</exception>
      /// <exception cref="T:Intermech.Mvp.MvpException">Посредник не был корректно инициализирован</exception>
      protected virtual void DoValidate()
      {
      }

      /// <summary>
      /// Позволяет обработать событие подключения посредника MVP (presenter) к виду MVP (view).
      /// Посредник должен заполнить свой вид исходными данными и подписаться на события вида.
      /// Необработанное исключение в этом методе прерывает процесс подключения и запускает процесс отключения.
      /// </summary>
      protected virtual void OnAttachView()
      {
        this.viewSyncContext = SynchronizationContext.Current ?? throw new MvpException(LocalizationHolder.rm.GetString("SR_1685"));
      }

      /// <summary>
      /// Позволяет обработать событие успешного подключения посредника MVP (presenter) к виду MVP (view).
      /// Необработанное исключение в этом методе не приводит к отключению посредника от своего вида.
      /// </summary>
      protected virtual void OnAfterAttachView()
      {
      }

      /// <summary>
      /// Позволяет обработать событие, предваряющее отключение посредника MVP (presenter) от вида MVP (view).
      /// Метод вызывается только в том случае, если ранее посредник был успешно подключен к виду.
      /// Необработанное исключение в этом методе не прерывает процесс отключения посредника от своего вида,
      /// исключение будет подавлено, а сведения о нем будут выведены в журнал трассировки приложения.
      /// </summary>
      protected virtual void OnBeforeDetachView()
      {
      }

      /// <summary>
      /// Позволяет обработать событие отключения посредника MVP (presenter) от вида MVP (view).
      /// Посредник должен очистить вид и отписаться от всех событий вида.
      /// Метод вызывается как при закрытии вида, так и в случае ошибки подключения к виду.
      /// </summary>
      protected virtual void OnDetachView() => this.viewSyncContext = (SynchronizationContext) null;

      /// <summary>
      /// Отключает посредника MVP (presenter) от вида MVP (view), если ранее этот посредник был успешно подключен к виду.
      /// Метод автоматически вызывается при закрытии вида и при его удалении с экрана.
      /// </summary>
      public void DetachView()
      {
        if (!this.isAttachedToView)
          return;
        this.OnBeforeDetachViewSilently();
        this.OnDetachViewSilently();
        this.isAttachedToView = false;
      }

      private void OnBeforeDetachViewSilently()
      {
        this.InvokeSilently(new Action(this.OnBeforeDetachView), "OnBeforeDetachView()");
      }

      private void OnDetachViewSilently()
      {
        this.InvokeSilently(new Action(this.OnDetachView), "OnDetachView()");
      }

      /// <summary>
      /// Выполняет указанный метод или блок кода с контролем необработанных исключений. Если при выполнении произойдет необработанное исключение,
      /// оно будет подавлено, и, если требуется, информация об этом событии будет записана в журнал трассировки.
      /// </summary>
      /// <param name="action">Выполняемый метод или блок кода</param>
      /// <param name="exceptionLocation">Описание места падения исключения, используется только в случае падения исключения. Значение параметра может быть равно null, в этом случае место падения будет вычислено автоматически</param>
      /// <exception cref="T:ArgumentNullException">Параметр <paramref name="action" /> не должен быть равен null</exception>
      private void InvokeSilently(Action action, string exceptionLocation = null)
      {
        this.silentActions.Invoke(action, exceptionLocation);
      }

      /// <summary>
      /// Позволяет проверить, можно ли изменить значение свойства посредника MVP (presenter).
      /// Как правило, значения свойств посредника можно изменять, если он не подключен к виду.
      /// </summary>
      protected void CheckAllowPropertyChange()
      {
        if (this.IsAttachedToView)
          throw new MvpException("Нельзя изменять свойства посредника MVP (presenter), когда он уже подключен к виду MVP (view).");
      }

      /// <summary>
      /// Позволяет посреднику MVP (presenter) обновить свой вид MVP (view) из фонового потока посредника.
      /// Это синхронный метод, поэтому фоновый поток будет приостановлен, пока вид не будет обновлен.
      /// </summary>
      /// <param name="method">Метод для обновления вида</param>
      protected void SendToViewThread(Action method)
      {
        if (method == null)
          throw new ArgumentNullException(nameof (method));
        if (!this.IsAttachedToView)
          return;
        this.SynchronizationContext?.Send((SendOrPostCallback) (state0 =>
        {
          if (!this.IsAttachedToView)
            return;
          method();
        }), (object) null);
      }

      /// <summary>
      /// Позволяет посреднику MVP (presenter) обновить свой вид MVP (view) из фонового потока посредника.
      /// Это асинхронный метод, поэтому фоновый поток посредника не будет приостановлен.
      /// </summary>
      /// <param name="method">Метод для обновления вида</param>
      protected void PostToViewThread(Action method)
      {
        if (method == null)
          throw new ArgumentNullException(nameof (method));
        if (!this.IsAttachedToView)
          return;
        this.SynchronizationContext?.Post((SendOrPostCallback) (state0 =>
        {
          if (!this.IsAttachedToView)
            return;
          method();
        }), (object) null);
      }
    }
}
