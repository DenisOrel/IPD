
// Type: Intermech.UI.Wpf.ViewModels.WizardPageVM
// Assembly: Intermech.Bcl.Wpf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 91600B17-2177-4703-BAB9-56FCFFBCBBA2
:\IPS\Client\Intermech.Bcl.Wpf.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.Wpf.xml

using System;
using System.Diagnostics;


namespace Intermech.UI.Wpf.ViewModels;

/// <summary>Класс модели вида для страницы мастера</summary>
public class WizardPageVM : ViewModel
{
  private readonly string displayName;
  private bool isActive;
  private bool isCompleted;

  /// <summary>Создает объект.</summary>
  /// <param name="displayName">Отображаемое имя страницы мастера</param>
  /// <exception cref="T:System.ArgumentException">параметр <paramref name="displayName" /> содержит null или пустую строку</exception>
  public WizardPageVM(string displayName)
  {
    this.displayName = !string.IsNullOrEmpty(displayName) ? displayName : throw new ArgumentException("Не задано отображаемое имя страницы мастера.", nameof (displayName));
  }

  /// <summary>Возвращает отображаемое имя страницы мастера.</summary>
  public string DisplayName
  {
    [DebuggerStepThrough] get => this.displayName;
  }

  /// <summary>
  /// Возвращает признак, что текущая страница является активной страницей мастера.
  /// </summary>
  public bool IsActive
  {
    [DebuggerStepThrough] get => this.isActive;
    private set
    {
      if (this.isActive == value)
        return;
      this.isActive = value;
      this.OnIsActiveChanged();
      this.RaisePropertyChanged(nameof (IsActive));
    }
  }

  /// <summary>
  /// Обработчик смены значения для свойства <see cref="P:Intermech.UI.Wpf.ViewModels.WizardPageVM.IsActive" />.
  /// Базовая реализация метода пуста.
  /// </summary>
  protected virtual void OnIsActiveChanged()
  {
  }

  /// <summary>
  /// Возвращает признак, что работа текущей страницы завершена,
  /// и мастер может перейти к следующей странице.
  /// </summary>
  public bool IsCompleted
  {
    [DebuggerStepThrough] get => this.isCompleted;
    protected set
    {
      if (this.isCompleted == value)
        return;
      this.isCompleted = value;
      this.OnIsCompletedChanged();
      this.RaisePropertyChanged(nameof (IsCompleted));
    }
  }

  /// <summary>
  /// Обработчик смены значения для свойства <see cref="P:Intermech.UI.Wpf.ViewModels.WizardPageVM.IsCompleted" />.
  /// Базовая реализация метода пуста.
  /// </summary>
  protected virtual void OnIsCompletedChanged()
  {
  }

  /// <summary>
  /// Обрабатывает активацию страницы в мастере после перехода с другой страницы
  /// </summary>
  /// <param name="navigationType">Тип перехода между страницами мастера</param>
  /// <param name="previousPage">Предыдущая страница мастера. Может быть не задана, если текущая страница является первой страницей</param>
  protected virtual void DoActivate(
    WizardPageNavigationType navigationType,
    WizardPageVM previousPage)
  {
  }

  /// <summary>
  /// Обрабатывает деактивацию страницы в мастере перед переходом на другую страницу.
  /// </summary>
  /// <param name="navigationType">Тип перехода между страницами мастера</param>
  /// <param name="nextPage">Следующая страница мастера. Может быть не задана, если текущая страница является последней страницей</param>
  protected virtual void DoDeactivate(
    WizardPageNavigationType navigationType,
    WizardPageVM nextPage)
  {
  }

  /// <summary>
  /// Позволяет проверить возможность перехода между страницами мастера, и,
  /// при необходимости, отменить операцию перехода.
  /// </summary>
  /// <param name="e">Аргументы события перехода между страницами мастера</param>
  protected virtual void DoValidateNavigation(WizardPageNavigationEventArgs e)
  {
  }

  internal void Activate(WizardPageNavigationType navigationType, WizardPageVM previousPage)
  {
    this.DoActivate(navigationType, previousPage);
    this.IsActive = true;
  }

  internal void Deactivate(WizardPageNavigationType navigationType, WizardPageVM nextPage)
  {
    this.DoDeactivate(navigationType, nextPage);
    this.IsActive = false;
  }

  internal void ValidateNavigation(WizardPageNavigationEventArgs e) => this.DoValidateNavigation(e);
}
