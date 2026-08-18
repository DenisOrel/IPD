
// Type: Intermech.UI.Wpf.ViewModels.WizardVM
// Assembly: Intermech.Bcl.Wpf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 91600B17-2177-4703-BAB9-56FCFFBCBBA2
:\IPS\Client\Intermech.Bcl.Wpf.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.Wpf.xml

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;


namespace Intermech.UI.Wpf.ViewModels;

/// <summary>
/// Класс модели вида для простого мастера с фиксированным набором страниц.
/// </summary>
public class WizardVM : ViewModel, ICloseableViewModel, INotifyPropertyChanged
{
  private IReadOnlyList<WizardPageVM> pages;
  private WizardVM.PagesGeometry pagesGeometry;
  private int currentPageIndex;
  private WizardPageVM currentPage;
  private WizardRunState runState;
  private PluggableCommand previousPageCommand;
  private PluggableCommand nextPageCommand;
  private PluggableCommand finishCommand;
  private PluggableCommand cancelCommand;
  private static readonly WizardPageVM[] emptyPages = new WizardPageVM[0];

  /// <summary>Создает объект.</summary>
  public WizardVM()
  {
    this.pages = (IReadOnlyList<WizardPageVM>) WizardVM.emptyPages;
    this.pagesGeometry = (WizardVM.PagesGeometry) null;
    this.currentPageIndex = -1;
    this.currentPage = (WizardPageVM) null;
    this.runState = WizardRunState.NotStarted;
    this.previousPageCommand = new PluggableCommand(new Action(this.OnPreviousPageCommand));
    this.nextPageCommand = new PluggableCommand(new Action(this.OnNextPageCommand));
    this.finishCommand = new PluggableCommand(new Action(this.OnFinishCommand));
    this.cancelCommand = new PluggableCommand(new Action(this.OnCancelCommand));
    this.DisableNavigationCommands();
  }

  /// <summary>
  /// Возвращает или задает набор страниц мастера.
  /// Изменение набора страниц при работающем мастере приведет к его перезапуску.
  /// </summary>
  public IReadOnlyList<WizardPageVM> Pages
  {
    [DebuggerStepThrough] get => this.pages;
    set
    {
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      if (value.Count == 0)
        throw new ArgumentException("Коллекция страниц мастера должна содержать хотя бы одну страницу.", nameof (value));
      if (this.pages == value)
        return;
      if (this.runState == WizardRunState.Started)
        this.FinishInternal(WizardPageNavigationType.Cancel);
      this.pages = (IReadOnlyList<WizardPageVM>) new List<WizardPageVM>((IEnumerable<WizardPageVM>) value);
      this.pagesGeometry = new WizardVM.PagesGeometry(this.pages.Count - 1);
      this.RaisePropertyChanged(nameof (Pages));
      this.StartInternal();
    }
  }

  /// <summary>Возвращает текущее состояние мастера</summary>
  public WizardRunState RunState
  {
    [DebuggerStepThrough] get => this.runState;
  }

  /// <summary>Возвращает текущую страницу мастера.</summary>
  public WizardPageVM CurrentPage
  {
    [DebuggerStepThrough] get => this.currentPage;
  }

  /// <summary>
  /// Возвращает команду для перехода на предыдущую страницу мастера.
  /// </summary>
  public PluggableCommand PreviousPageCommand
  {
    [DebuggerStepThrough] get => this.previousPageCommand;
  }

  /// <summary>
  /// Возвращает команду для перехода на следующую страницу мастера.
  /// </summary>
  public PluggableCommand NextPageCommand
  {
    [DebuggerStepThrough] get => this.nextPageCommand;
  }

  /// <summary>Возвращает команду для завершения работы мастера.</summary>
  public PluggableCommand FinishCommand
  {
    [DebuggerStepThrough] get => this.finishCommand;
  }

  /// <summary>Возвращает команду для отмены работы мастера.</summary>
  public PluggableCommand CancelCommand
  {
    [DebuggerStepThrough] get => this.cancelCommand;
  }

  /// <summary>Перезапускает мастер.</summary>
  /// <exception cref="T:System.InvalidOperationException">Невозможно перезапустить мастер, так как он не запущен</exception>
  public void Restart()
  {
    if (this.runState == WizardRunState.NotStarted)
      throw new InvalidOperationException("Мастер не был запущен на выполнение.");
    if (this.runState == WizardRunState.Started)
      this.FinishInternal(WizardPageNavigationType.Cancel);
    this.StartInternal();
  }

  /// <summary>
  /// Предварительное событие изменения текущей страницы мастера.
  /// Подписчики вызываются до начала изменения свойства <see cref="P:Intermech.UI.Wpf.ViewModels.WizardVM.CurrentPage" />.
  /// </summary>
  public event EventHandler<WizardPageChangingEventArgs> CurrentPageChanging;

  /// <summary>
  /// Окончательное событие изменения текущей страницы мастера.
  /// Подписчики вызываются после успешного изменения свойства <see cref="P:Intermech.UI.Wpf.ViewModels.WizardVM.CurrentPage" />.
  /// </summary>
  public event EventHandler CurrentPageChanged;

  /// <summary>
  /// Возвращает признак, что мастер завершил работу и не может больше использоваться.
  /// </summary>
  bool ICloseableViewModel.IsClosed
  {
    [DebuggerStepThrough] get
    {
      return this.runState == WizardRunState.Completed || this.runState == WizardRunState.Cancelled;
    }
  }

  /// <summary>Завершает работу мастера.</summary>
  void ICloseableViewModel.Close()
  {
    if (this.FinishCommand.CanExecute((object) null))
    {
      this.FinishCommand.Execute((object) null);
    }
    else
    {
      if (!this.CancelCommand.CanExecute((object) null))
        return;
      this.CancelCommand.Execute((object) null);
    }
  }

  private void OnPreviousPageCommand()
  {
    WizardPageNavigationType navigationType = WizardPageNavigationType.Backward;
    if (!this.CanNavigate(navigationType))
      return;
    this.MoveToPreviousPage(navigationType);
  }

  private void OnNextPageCommand()
  {
    WizardPageNavigationType navigationType = WizardPageNavigationType.Forward;
    if (!this.CanNavigate(navigationType))
      return;
    this.MoveToNextPage(navigationType);
  }

  private void OnFinishCommand()
  {
    WizardPageNavigationType navigationType = WizardPageNavigationType.Finish;
    if (!this.CanNavigate(navigationType))
      return;
    this.FinishInternal(navigationType);
  }

  private void OnCancelCommand()
  {
    WizardPageNavigationType navigationType = WizardPageNavigationType.Cancel;
    if (!this.CanNavigate(navigationType))
      return;
    this.FinishInternal(navigationType);
  }

  private void ActivateCurrentPage(
    WizardPageNavigationType navigationType,
    WizardPageVM previousPage)
  {
    this.currentPage.PropertyChanged += new PropertyChangedEventHandler(this.OnCurrentPageCompleted);
    this.currentPage.Activate(navigationType, previousPage);
  }

  private void DeactivateCurrentPage(WizardPageNavigationType navigationType, WizardPageVM nextPage)
  {
    this.currentPage.PropertyChanged -= new PropertyChangedEventHandler(this.OnCurrentPageCompleted);
    this.currentPage.Deactivate(navigationType, nextPage);
  }

  private void OnCurrentPageCompleted(object sender, PropertyChangedEventArgs e)
  {
    if (!(e.PropertyName == "IsCompleted"))
      return;
    this.UpdateNavigationCommands();
  }

  private void UpdateNavigationCommands()
  {
    bool isCompleted = this.currentPage.IsCompleted;
    this.finishCommand.Enabled = isCompleted && this.currentPageIndex == this.pagesGeometry.LastPageIndex;
    this.cancelCommand.Enabled = true;
    this.nextPageCommand.Enabled = isCompleted && this.currentPageIndex < this.pagesGeometry.LastPageIndex;
    this.previousPageCommand.Enabled = !this.finishCommand.Enabled && this.currentPageIndex > 0;
  }

  private void DisableNavigationCommands()
  {
    this.finishCommand.Enabled = false;
    this.cancelCommand.Enabled = false;
    this.nextPageCommand.Enabled = false;
    this.previousPageCommand.Enabled = false;
  }

  private void MoveToPreviousPage(WizardPageNavigationType navigationType)
  {
    int index = this.currentPageIndex - 1;
    WizardPageVM page = this.pages[index];
    WizardPageVM currentPage = this.currentPage;
    this.RaiseCurrentPageChanging(navigationType, currentPage, page);
    this.DeactivateCurrentPage(navigationType, page);
    this.currentPageIndex = index;
    this.currentPage = page;
    this.RaisePropertyChanged("CurrentPage");
    this.RaiseCurrentPageChanged(navigationType, currentPage, page);
    this.ActivateCurrentPage(navigationType, currentPage);
    this.UpdateNavigationCommands();
  }

  private void MoveToNextPage(WizardPageNavigationType navigationType)
  {
    int index = this.currentPageIndex + 1;
    WizardPageVM page = this.pages[index];
    WizardPageVM currentPage = this.currentPage;
    this.RaiseCurrentPageChanging(navigationType, currentPage, page);
    this.DeactivateCurrentPage(navigationType, page);
    this.currentPageIndex = index;
    this.currentPage = page;
    this.RaisePropertyChanged("CurrentPage");
    this.RaiseCurrentPageChanged(navigationType, currentPage, page);
    this.ActivateCurrentPage(navigationType, currentPage);
    this.UpdateNavigationCommands();
  }

  private void MoveToFirstPage(WizardPageNavigationType navigationType)
  {
    int index = 0;
    WizardPageVM page = this.pages[index];
    this.RaiseCurrentPageChanging(navigationType, nextPage: page);
    this.currentPageIndex = index;
    this.currentPage = page;
    this.RaisePropertyChanged("CurrentPage");
    this.RaiseCurrentPageChanged(navigationType, nextPage: page);
    this.ActivateCurrentPage(navigationType, (WizardPageVM) null);
    this.UpdateNavigationCommands();
  }

  private void MoveBeyondLastPage(WizardPageNavigationType navigationType)
  {
    WizardPageVM currentPage = this.currentPage;
    this.RaiseCurrentPageChanging(navigationType, currentPage);
    this.DisableNavigationCommands();
    this.DeactivateCurrentPage(navigationType, (WizardPageVM) null);
    this.currentPageIndex = -1;
    this.currentPage = (WizardPageVM) null;
    this.RaisePropertyChanged("CurrentPage");
    this.RaiseCurrentPageChanged(navigationType, currentPage);
  }

  private void StartInternal()
  {
    this.runState = WizardRunState.Started;
    this.MoveToFirstPage(WizardPageNavigationType.Forward);
    this.RaisePropertyChanged("RunState");
    this.RaisePropertyChanged("IsClosed");
  }

  private void FinishInternal(WizardPageNavigationType navigationType)
  {
    bool flag = navigationType == WizardPageNavigationType.Cancel;
    this.MoveBeyondLastPage(navigationType);
    this.runState = flag ? WizardRunState.Cancelled : WizardRunState.Completed;
    this.RaisePropertyChanged("RunState");
    this.RaisePropertyChanged("IsClosed");
  }

  private bool CanNavigate(WizardPageNavigationType navigationType)
  {
    WizardPageNavigationEventArgs e = new WizardPageNavigationEventArgs(navigationType);
    this.DoValidateNavigation(e);
    return !e.Cancel;
  }

  /// <summary>
  /// Позволяет проверить возможность перехода между страницами мастера, и,
  /// при необходимости, отменить операцию перехода.
  /// </summary>
  /// <param name="e">Аргументы события перехода между страницами мастера</param>
  protected virtual void DoValidateNavigation(WizardPageNavigationEventArgs e)
  {
    this.currentPage.ValidateNavigation(e);
  }

  private void RaiseCurrentPageChanging(
    WizardPageNavigationType navigationType,
    WizardPageVM previousPage = null,
    WizardPageVM nextPage = null)
  {
    this.DoCurrentPageChanging(navigationType, previousPage, nextPage);
    EventHandler<WizardPageChangingEventArgs> currentPageChanging = this.CurrentPageChanging;
    if (currentPageChanging == null)
      return;
    currentPageChanging((object) this, new WizardPageChangingEventArgs(previousPage, nextPage));
  }

  private void RaiseCurrentPageChanged(
    WizardPageNavigationType navigationType,
    WizardPageVM previousPage = null,
    WizardPageVM nextPage = null)
  {
    this.DoCurrentPageChanged(navigationType, previousPage, nextPage);
    EventHandler currentPageChanged = this.CurrentPageChanged;
    if (currentPageChanged == null)
      return;
    currentPageChanged((object) this, EventArgs.Empty);
  }

  /// <summary>
  /// Обрабатывает начало перехода между страницами мастера.
  /// </summary>
  /// <param name="navigationType">Тип перехода между страницами мастера</param>
  /// <param name="previousPage">Предыдущая страница мастера. Может быть не задана, если текущая страница является первой страницей</param>
  /// <param name="nextPage">Следующая страница мастера. Может быть не задана, если текущая страница является последней страницей</param>
  protected virtual void DoCurrentPageChanging(
    WizardPageNavigationType navigationType,
    WizardPageVM previousPage,
    WizardPageVM nextPage)
  {
  }

  /// <summary>
  /// Обрабатывает завершение перехода между страницами мастера.
  /// </summary>
  /// <param name="navigationType">Тип перехода между страницами мастера</param>
  /// <param name="previousPage">Предыдущая страница мастера. Может быть не задана, если текущая страница является первой страницей</param>
  /// <param name="nextPage">Следующая страница мастера. Может быть не задана, если текущая страница является последней страницей</param>
  protected virtual void DoCurrentPageChanged(
    WizardPageNavigationType navigationType,
    WizardPageVM previousPage,
    WizardPageVM nextPage)
  {
  }

  private sealed class PagesGeometry
  {
    public PagesGeometry(int lastPageIndex) => this.LastPageIndex = lastPageIndex;

    public int LastPageIndex { get; }
  }
}
