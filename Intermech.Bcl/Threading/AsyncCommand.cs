
// Type: Intermech.Threading.AsyncCommand
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Diagnostics;
using System.Threading;


namespace Intermech.Threading
{
    /// <summary>
    /// Базовый класс для команд, допускающих асинхронное выполнение и асинхронное прерывание выполения.
    /// </summary>
    public abstract class AsyncCommand
    {
      private AsyncCommandResultStatus resultStatus;
      private Exception exception;

      /// <summary>Создает объект</summary>
      protected AsyncCommand() => this.ResultStatus = AsyncCommandResultStatus.Undefined;

      /// <summary>Возвращает статус последнего выполнения команды.</summary>
      /// <remarks>
      /// Если было использовано асинхронное прерывание выполнения команды, то статус команды не обязательно будет равен <see cref="M:AsyncCommandResultStatus.Aborted" />.
      /// В тот момент, когда было затребовано прерывание выполнения, команда уже могла находиться в коде завершения выполнения или захвата необработанного исключения.
      /// </remarks>
      public AsyncCommandResultStatus ResultStatus
      {
        [DebuggerStepThrough] get => this.resultStatus;
        [DebuggerStepThrough] private set => this.resultStatus = value;
      }

      /// <summary>
      /// Возвращает необработанное исключение при последнем выполнении команды.
      /// </summary>
      public Exception Exception
      {
        [DebuggerStepThrough] get => this.exception;
        [DebuggerStepThrough] private set => this.exception = value;
      }

      public void Execute(IAsyncCommandContext commandContext)
      {
        if (commandContext == null)
          throw new ArgumentNullException(nameof (commandContext));
        this.ResultStatus = AsyncCommandResultStatus.Undefined;
        this.Exception = (Exception) null;
        try
        {
          this.DoReset();
          this.DoExecute(commandContext);
          this.ResultStatus = commandContext.CommandAborted ? AsyncCommandResultStatus.Aborted : AsyncCommandResultStatus.Completed;
        }
        catch (ThreadAbortException ex)
        {
          this.ResultStatus = AsyncCommandResultStatus.Aborted;
          this.Exception = (Exception) null;
          throw;
        }
        catch (Exception ex)
        {
          this.ResultStatus = AsyncCommandResultStatus.Failed;
          this.Exception = ex;
        }
      }

      /// <summary>
      /// Очищает внутреннее состояние команды и готовит ее к выполнению.
      /// Метод вызывается перед каждым выполнением команды, он используется для очистки результатов предыдущего выполнения.
      /// </summary>
      protected virtual void DoReset()
      {
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
      protected abstract void DoExecute(IAsyncCommandContext commandContext);
    }
}
