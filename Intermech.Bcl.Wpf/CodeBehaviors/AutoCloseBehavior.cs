
// Type: Intermech.UI.Wpf.CodeBehaviors.AutoCloseBehavior
// Assembly: Intermech.Bcl.Wpf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 91600B17-2177-4703-BAB9-56FCFFBCBBA2
:\IPS\Client\Intermech.Bcl.Wpf.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.Wpf.xml

using System;
using System.ComponentModel;
using System.Windows;


namespace Intermech.UI.Wpf.CodeBehaviors;

/// <summary>
/// Объект-поведение, обеспечивающий закрытие окна в соответствии с поведением модели вида.
/// Модель вида должна поддерживать интерфейс <see cref="T:Intermech.UI.ICloseableViewModel" />
/// </summary>
public sealed class AutoCloseBehavior : CodeBehavior
{
  private readonly Window window;
  private readonly INotifyPropertyChanged vm;
  private bool disableVMIsClosedHandler;

  /// <summary>Создает объект.</summary>
  /// <param name="window">Окно</param>
  /// <param name="viewModel">Модель вида</param>
  /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="window" /> содержит null; параметр <paramref name="viewModel" /> содержит null</exception>
  public AutoCloseBehavior(Window window, INotifyPropertyChanged viewModel)
  {
    if (window == null)
      throw new ArgumentNullException(nameof (window));
    if (viewModel == null)
      throw new ArgumentNullException(nameof (viewModel));
    this.window = window;
    this.vm = viewModel;
    this.Attach();
  }

  /// <summary>Подключает текущий объект.</summary>
  protected override void DoAttach()
  {
    base.DoAttach();
    if (!(this.vm is ICloseableViewModel))
      return;
    this.window.Closing += new CancelEventHandler(this.OnWindowClosing);
    this.vm.PropertyChanged += new PropertyChangedEventHandler(this.OnVMIsClosedChanged);
  }

  /// <summary>Отключает текущий объект.</summary>
  protected override void DoDetach()
  {
    base.DoDetach();
    if (!(this.vm is ICloseableViewModel))
      return;
    this.window.Closing -= new CancelEventHandler(this.OnWindowClosing);
    this.vm.PropertyChanged -= new PropertyChangedEventHandler(this.OnVMIsClosedChanged);
  }

  private void OnWindowClosing(object sender, CancelEventArgs e)
  {
    ICloseableViewModel vm = (ICloseableViewModel) this.vm;
    if (vm.IsClosed)
      return;
    this.disableVMIsClosedHandler = true;
    try
    {
      vm.Close();
      if (vm.IsClosed)
        return;
      e.Cancel = true;
    }
    finally
    {
      this.disableVMIsClosedHandler = false;
    }
  }

  private void OnVMIsClosedChanged(object sender, PropertyChangedEventArgs e)
  {
    if (!(e.PropertyName == "IsClosed") || this.disableVMIsClosedHandler || !((ICloseableViewModel) this.vm).IsClosed)
      return;
    this.window.Close();
  }
}
