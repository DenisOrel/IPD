
// Type: Intermech.UI.Winforms.CodeBehaviors.AutoCloseBehavior
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.ComponentModel;
using System.Windows.Forms;


namespace Intermech.UI.Winforms.CodeBehaviors
{
    /// <summary>
    /// Объект-поведение, обеспечивающий закрытие окна в соответствии с поведением модели вида.
    /// Модель вида должна поддерживать интерфейс <see cref="T:Intermech.UI.ICloseableViewModel" />
    /// </summary>
    public sealed class AutoCloseBehavior : CodeBehavior
    {
      private readonly Form form;
      private readonly INotifyPropertyChanged vm;
      private bool disableVMIsClosedHandler;

      /// <summary>Создает объект.</summary>
      /// <param name="form">Окно</param>
      /// <param name="viewModel">Модель вида</param>
      /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="form" /> содержит null; параметр <paramref name="viewModel" /> содержит null</exception>
      public AutoCloseBehavior(Form form, INotifyPropertyChanged viewModel)
      {
        if (form == null)
          throw new ArgumentNullException(nameof (form));
        if (viewModel == null)
          throw new ArgumentNullException(nameof (viewModel));
        this.form = form;
        this.vm = viewModel;
        this.Attach();
      }

      /// <summary>Подключает текущий объект.</summary>
      protected override void DoAttach()
      {
        base.DoAttach();
        if (!(this.vm is ICloseableViewModel))
          return;
        this.form.Closing += new CancelEventHandler(this.OnWindowClosing);
        this.vm.PropertyChanged += new PropertyChangedEventHandler(this.OnVMIsClosedChanged);
      }

      /// <summary>Отключает текущий объект.</summary>
      protected override void DoDetach()
      {
        base.DoDetach();
        if (!(this.vm is ICloseableViewModel))
          return;
        this.form.Closing -= new CancelEventHandler(this.OnWindowClosing);
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
        this.form.Close();
      }
    }
}
