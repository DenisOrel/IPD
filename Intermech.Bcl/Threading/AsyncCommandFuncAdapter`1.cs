
// Type: Intermech.Threading.AsyncCommandFuncAdapter`1
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Diagnostics;


namespace Intermech.Threading
{
    /// <summary>
    /// Класс-обертка, позволяющий представить произвольную функцию в виде асинхронной команды с поддержкой прерывания выполнения.
    /// </summary>
    public sealed class AsyncCommandFuncAdapter<T> : AsyncCommand
    {
      private Func<T> function;
      private T returnValue;

      /// <summary>Создает объект.</summary>
      /// <param name="action">Функция, которая должна быть представлена как команда</param>
      /// <exception cref="T:ArgumentNullException">Параметр <paramref name="function" /> не должен быть равен null</exception>
      public AsyncCommandFuncAdapter(Func<T> function)
      {
        this.function = function != null ? function : throw new ArgumentNullException(nameof (function));
      }

      /// <summary>
      /// Возвращает результат последнего выполнения команды.
      /// Значение свойства не определено, если свойство <see cref="P:AsyncCommand.ResultType" /> не равно Completed.
      /// </summary>
      public T ReturnValue
      {
        [DebuggerStepThrough] get => this.returnValue;
      }

      /// <summary>
      /// Очищает внутреннее состояние команды и готовит ее к выполнению.
      /// Метод вызывается перед каждым выполнением команды, он используется для очистки результатов предыдущего выполнения.
      /// </summary>
      protected override void DoReset()
      {
        base.DoReset();
        this.returnValue = default (T);
      }

      /// <summary>Реализует выполенение команды.</summary>
      /// <param name="commandContext">Контекст управления выполнением команды. Используется для прерывания выполнения команды.</param>
      /// <exception cref="T:Exception">В процессе выполнения команды произошло необработанное исключение</exception>
      /// <remarks>
      /// <para>
      /// Команда, получив сигнал прерывания, должна немедленно прекратить выполнение. При этом любые результаты работы команды,
      /// как полные, так и частичные, должны быть отброшены.</para>
      /// <para>Если команда самостоятельно не прекратит выполнение в течение определенного интервала времени,
      /// то она может быть принудительно остановлена с помощью асинхронного исключения.</para>
      /// <see cref="T:System.Threading.ThreadAbortException" />.
      /// </remarks>
      protected override void DoExecute(IAsyncCommandContext commandContext)
      {
        T obj = this.function();
        if (commandContext.CommandAborted)
          return;
        this.returnValue = obj;
      }
    }
}
