
// Type: Intermech.UI.Wpf.UIExceptionHandler
// Assembly: Intermech.Bcl.Wpf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 91600B17-2177-4703-BAB9-56FCFFBCBBA2
:\IPS\Client\Intermech.Bcl.Wpf.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.Wpf.xml

using System;
using System.Windows;
using System.Windows.Threading;


namespace Intermech.UI.Wpf;

/// <summary>
/// Позволяет перехватить все необработанные исключения в UI-потоке приложения и отобразить/записать их с помощью указанного обработчика.
/// Реализация класса не является thread safe.
/// </summary>
public sealed class UIExceptionHandler
{
  private Action<Exception> exceptionHandler;
  private bool isActive;

  /// <summary>Создает объект.</summary>
  /// <param name="exceptionHandler">Обработчик исключений UI-потока приложения</param>
  /// <exception cref="T:ArgumentNullException">Параметр <paramref name="exceptionHandler" /> не должен быть равен null</exception>
  public UIExceptionHandler(Action<Exception> exceptionHandler)
  {
    this.exceptionHandler = exceptionHandler != null ? exceptionHandler : throw new ArgumentNullException(nameof (exceptionHandler));
  }

  /// <summary>Активирует обработчик.</summary>
  public void Activate()
  {
    if (this.isActive)
      return;
    Application.Current.DispatcherUnhandledException += new DispatcherUnhandledExceptionEventHandler(this.OnUIThreadException);
    this.isActive = true;
  }

  /// <summary>Деактивирует обработчик.</summary>
  public void Deactivate()
  {
    if (!this.isActive)
      return;
    Application.Current.DispatcherUnhandledException -= new DispatcherUnhandledExceptionEventHandler(this.OnUIThreadException);
    this.isActive = false;
  }

  private void OnUIThreadException(object sender, DispatcherUnhandledExceptionEventArgs e)
  {
    if (e.Handled)
      return;
    this.exceptionHandler(e.Exception);
    e.Handled = true;
  }
}
