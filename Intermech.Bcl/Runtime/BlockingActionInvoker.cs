
// Type: Intermech.Runtime.BlockingActionInvoker
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Threading;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading;


namespace Intermech.Runtime
{
    public sealed class BlockingActionInvoker : IDisposable
    {
      private ApartmentState apartmentState;
      private int timeout;
      private DedicatedThreadCommandExecutor commandExecutor;
      private bool isDisposed;

      public BlockingActionInvoker()
      {
        this.apartmentState = ApartmentState.MTA;
        this.timeout = 60000;
      }

      public void Dispose()
      {
        if (this.isDisposed)
          return;
        try
        {
          if (this.commandExecutor == null)
            return;
          this.commandExecutor.Dispose();
          this.commandExecutor = (DedicatedThreadCommandExecutor) null;
        }
        finally
        {
          this.isDisposed = true;
        }
      }

      public bool IsDisposed
      {
        [DebuggerStepThrough] get => this.isDisposed;
      }

      private void RequireNotDisposed()
      {
        if (this.isDisposed)
          throw new ObjectDisposedException(this.GetType().FullName);
      }

      public ApartmentState ApartmentState
      {
        [DebuggerStepThrough] get => this.apartmentState;
        [DebuggerStepThrough] set
        {
          this.RequireNotDisposed();
          if (this.apartmentState == value)
            return;
          if (this.commandExecutor != null)
          {
            this.commandExecutor.Dispose();
            this.commandExecutor = (DedicatedThreadCommandExecutor) null;
          }
          this.apartmentState = value;
        }
      }

      public int Timeout
      {
        [DebuggerStepThrough] get => this.timeout;
        [DebuggerStepThrough] set
        {
          if (value < 0)
            throw new ArgumentOutOfRangeException(nameof (value));
          this.RequireNotDisposed();
          this.timeout = value;
        }
      }

      public void InvokeAction(Action action)
      {
        if (action == null)
          throw new ArgumentNullException(nameof (action));
        this.RequireNotDisposed();
        if (this.commandExecutor == null)
          this.commandExecutor = new DedicatedThreadCommandExecutor(this.apartmentState);
        AsyncCommandActionAdapter command = AsyncCommands.FromAction(action);
        this.commandExecutor.BeginCommand((AsyncCommand) command, (Action) null);
        if (this.commandExecutor.CurrentCommandState.WaitHandle.WaitOne(this.Timeout))
        {
          switch (command.ResultStatus)
          {
            case AsyncCommandResultStatus.Completed:
              break;
            case AsyncCommandResultStatus.Failed:
              throw new TargetInvocationException(command.Exception);
            default:
              throw new NotSupportedEnumException((Enum) command.ResultStatus);
          }
        }
        else
        {
          this.commandExecutor.AbortCommand();
          throw new TimeoutException("The action is aborted due to timeout.");
        }
      }

      public TResult InvokeFunction<TResult>(Func<TResult> function)
      {
        if (function == null)
          throw new ArgumentNullException(nameof (function));
        this.RequireNotDisposed();
        if (this.commandExecutor == null)
          this.commandExecutor = new DedicatedThreadCommandExecutor(this.apartmentState);
        AsyncCommandFuncAdapter<TResult> command = AsyncCommands.FromFunction(function);
        this.commandExecutor.BeginCommand((AsyncCommand) command, (Action) null);
        if (this.commandExecutor.CurrentCommandState.WaitHandle.WaitOne(this.Timeout))
        {
          switch (command.ResultStatus)
          {
            case AsyncCommandResultStatus.Completed:
              return command.ReturnValue;
            case AsyncCommandResultStatus.Failed:
              throw new TargetInvocationException(command.Exception);
            default:
              throw new NotSupportedEnumException((Enum) command.ResultStatus);
          }
        }
        else
        {
          this.commandExecutor.AbortCommand();
          throw new TimeoutException("The function is aborted due to timeout.");
        }
      }
    }
}
