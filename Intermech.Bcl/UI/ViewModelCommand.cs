
// Type: Intermech.UI.ViewModelCommand
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Input;


namespace Intermech.UI
{
    /// <summary>Базовый класс для команд моделей вида.</summary>
    public abstract class ViewModelCommand : ICommand, INotifyPropertyChanged
    {
      private bool enabled;

      /// <summary>Создает объект.</summary>
      protected ViewModelCommand() => this.enabled = true;

      /// <summary>
      /// Возвращает или задает признак, что команда включена и может быть использована.
      /// </summary>
      public bool Enabled
      {
        [DebuggerStepThrough] get => this.enabled;
        set
        {
          if (this.enabled == value)
            return;
          this.enabled = value;
          this.RaisePropertyChanged(nameof (Enabled));
          if (!this.IsHandlerSet())
            return;
          this.RaiseCanExecuteChanged();
        }
      }

      /// <summary>Проверяет, назначен ли обработчик для команды.</summary>
      /// <returns>true - если обработчик назначен; flase - если обработчик не назначен</returns>
      protected abstract bool IsHandlerSet();

      /// <summary>Проверяет, можно ли выполнить команду.</summary>
      /// <param name="parameter">Параметр выполнения команды</param>
      /// <returns>true - если команда может быть выполнена; false - если команда не может быть выполнена</returns>
      public bool CanExecute(object parameter) => this.enabled && this.IsHandlerSet();

      /// <summary>Выполняет команду.</summary>
      /// <param name="parameter">Параметр выполнения команды</param>
      public void Execute(object parameter)
      {
        if (!this.CanExecute(parameter))
          return;
        try
        {
          this.DoExecute(parameter);
        }
        finally
        {
          this.DoCleanup();
        }
      }

      /// <summary>Выполняет команду.</summary>
      /// <param name="parameter">Параметр выполнения команды</param>
      protected abstract void DoExecute(object parameter);

      /// <summary>
      /// Очищает внутреннее состояние команды после выполнения.
      /// Метод вызывается даже в случае падения необработанного исключения при выполнении команды.
      /// </summary>
      protected virtual void DoCleanup()
      {
      }

      /// <summary>Событие изменения доступности команды для выполнения.</summary>
      public event EventHandler CanExecuteChanged;

      /// <summary>
      /// Запускает событие изменения доступности команды для выполнения.
      /// </summary>
      protected void RaiseCanExecuteChanged()
      {
        EventHandler canExecuteChanged = this.CanExecuteChanged;
        if (canExecuteChanged == null)
          return;
        canExecuteChanged((object) this, EventArgs.Empty);
      }

      /// <summary>Запускает событие изменения свойства команды.</summary>
      /// <param name="propertyName">Имя свойства команды</param>
      protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
      {
        if (this.PropertyChanged == null || propertyName == null)
          return;
        this.PropertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
      }

      /// <summary>Событие изменения свойства команды.</summary>
      public event PropertyChangedEventHandler PropertyChanged;
    }
}
