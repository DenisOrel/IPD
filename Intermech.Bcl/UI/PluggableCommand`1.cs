
// Type: Intermech.UI.PluggableCommand`1
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Diagnostics;


namespace Intermech.UI
{
    /// <summary>
    /// Команда для моделей вида, обработчик котором можно задавать через свойство.
    /// </summary>
    /// <typeparam name="T">Тип параметра команды</typeparam>
    public sealed class PluggableCommand<T> : ViewModelCommand
    {
      private Action<T> handler;

      /// <summary>Создает объект.</summary>
      public PluggableCommand()
      {
      }

      /// <summary>Создает объект.</summary>
      /// <param name="handler">Обработчик команды, не должен быть null</param>
      /// <exception cref="T:ArgumentNullException">Параметр <paramref name="handler" /> не должен быть null</exception>
      public PluggableCommand(Action<T> handler)
      {
        this.Handler = handler != null ? handler : throw new ArgumentNullException(nameof (handler));
      }

      /// <summary>
      /// Возвращает или задает обработчик команды.
      /// Значение свойства может быть не задано и равно null.
      /// </summary>
      public Action<T> Handler
      {
        [DebuggerStepThrough] get => this.handler;
        set
        {
          if (!(this.handler != value))
            return;
          this.handler = value;
          this.RaisePropertyChanged(nameof (Handler));
          if (!this.Enabled)
            return;
          this.RaiseCanExecuteChanged();
        }
      }

      /// <summary>Проверяет, назначен ли обработчик для команды.</summary>
      /// <returns>true - если обработчик назначен; flase - если обработчик не назначен</returns>
      protected override bool IsHandlerSet() => this.handler != null;

      /// <summary>Выполняет команду.</summary>
      /// <param name="parameter">Параметр выполнения команды</param>
      protected override void DoExecute(object parameter)
      {
        Action<T> handler = this.Handler;
        if (handler == null)
          return;
        handler((T) parameter);
      }
    }
}
