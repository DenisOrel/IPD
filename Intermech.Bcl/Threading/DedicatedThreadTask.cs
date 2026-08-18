
// Type: Intermech.Threading.DedicatedThreadTask
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Diagnostics;
using System;
using System.Diagnostics;
using System.Threading;


namespace Intermech.Threading
{
    internal sealed class DedicatedThreadTask : IDisposable, IAsyncCommandState, IAsyncCommandContext
    {
      private AsyncCommand command;
      private Action completedCallback;
      private ManualResetEvent completedWaitEvent;
      private bool isDisposed;
      private object stateSyncRoot;
      private volatile bool isCompleted;
      private volatile bool isAborted;

      public DedicatedThreadTask(AsyncCommand command, Action completedCallback = null)
      {
        this.command = command != null ? command : throw new ArgumentNullException(nameof (command));
        this.completedCallback = completedCallback;
        this.completedWaitEvent = new ManualResetEvent(false);
        this.stateSyncRoot = new object();
      }

      public void Dispose()
      {
        if (this.isDisposed)
          return;
        try
        {
          this.completedWaitEvent.Dispose();
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

      public AsyncCommand Command
      {
        [DebuggerStepThrough] get => this.command;
      }

      public EventWaitHandle WaitHandle
      {
        [DebuggerStepThrough] get => (EventWaitHandle) this.completedWaitEvent;
      }

      public bool IsCompleted
      {
        [DebuggerStepThrough] get => this.isCompleted;
      }

      bool IAsyncCommandContext.CommandAborted
      {
        [DebuggerStepThrough] get => this.isAborted;
      }

      public Action CompletedCallback
      {
        [DebuggerStepThrough] get => this.completedCallback;
      }

      /// <summary>
      /// Помечает, что задание отменено. Метод ни в коем случае не должен бросать исключений.
      /// </summary>
      /// <returns>Признак успешного изменения состояния</returns>
      internal bool SetAbortedState()
      {
        this.RequireNotDisposed();
        lock (this.stateSyncRoot)
        {
          if (this.isCompleted)
            return false;
          this.isAborted = true;
          return true;
        }
      }

      /// <summary>
      /// Помечает, что задание выполнено. Метод ни в коем случае не должен бросать исключений.
      /// </summary>
      /// <returns>Признак успешного изменения состояния</returns>
      internal bool SetCompletedState()
      {
        this.RequireNotDisposed();
        lock (this.stateSyncRoot)
        {
          if (this.isAborted)
            return false;
          this.isCompleted = true;
          this.RaiseCompletedCallbackSilently();
          this.SetCompletedWaitEventSilently();
          return true;
        }
      }

      private void RaiseCompletedCallbackSilently()
      {
        Action completedCallback = this.completedCallback;
        if (completedCallback == null)
          return;
        try
        {
          completedCallback();
        }
        catch (Exception ex)
        {
          SuppressedExceptions.TraceException(ex, "DedicatedThreadTask.RaiseCompletedCallbackSilently()");
        }
      }

      private void SetCompletedWaitEventSilently()
      {
        try
        {
          if (this.completedWaitEvent.SafeWaitHandle.IsClosed)
            return;
          this.completedWaitEvent.Set();
        }
        catch (Exception ex)
        {
          SuppressedExceptions.TraceException(ex, "DedicatedThreadTask.SetCompletedWaitEventSilently()");
        }
      }
    }
}
