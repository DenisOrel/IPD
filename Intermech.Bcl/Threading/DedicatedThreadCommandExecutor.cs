
// Type: Intermech.Threading.DedicatedThreadCommandExecutor
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
    public sealed class DedicatedThreadCommandExecutor : IDisposable, IAsyncCommandExecutor
    {
      private const int PutCommandTimeout = 10000;
      private const int AbortThreadTimeout = 10000;
      private ApartmentState apartmentState;
      private bool isDisposed;
      private DedicatedThreadTask currentTask;
      private DedicatedThreadControlBlock workerThreadControlBlock;
      private Thread workerThread;

      public DedicatedThreadCommandExecutor(ApartmentState apartmentState = ApartmentState.STA)
      {
        this.apartmentState = apartmentState;
      }

      public void Dispose()
      {
        if (this.isDisposed)
          return;
        try
        {
          this.AbortCommand();
          if (this.currentTask == null)
            return;
          this.currentTask.Dispose();
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
      }

      public void BeginCommand(AsyncCommand command, Action completedCallback = null)
      {
        if (command == null)
          throw new ArgumentNullException(nameof (command));
        this.RequireNotDisposed();
        if (this.currentTask != null && !this.currentTask.IsCompleted)
          throw new InvalidOperationException("The previous command is still in progress.");
        if (this.currentTask != null)
          this.currentTask.Dispose();
        this.currentTask = new DedicatedThreadTask(command, completedCallback);
        try
        {
          this.LazyStartWorkerThread();
          if (!this.workerThreadControlBlock.Mailbox.TryPut(this.currentTask, 10000))
            throw new TimeoutException("Unable to send a command to a worker thread.");
        }
        catch
        {
          this.AbortWorkerThread();
          this.currentTask.Dispose();
          this.currentTask = (DedicatedThreadTask) null;
          throw;
        }
      }

      public void AbortCommand()
      {
        this.RequireNotDisposed();
        if (this.currentTask == null || this.currentTask.IsCompleted || !this.currentTask.SetAbortedState())
          return;
        this.AbortWorkerThread();
        DedicatedThreadTask dedicatedThreadTask = new DedicatedThreadTask(this.currentTask.Command, this.currentTask.CompletedCallback);
        this.currentTask.Dispose();
        this.currentTask = dedicatedThreadTask;
        dedicatedThreadTask.SetCompletedState();
      }

      public IAsyncCommandState CurrentCommandState
      {
        [DebuggerStepThrough] get => (IAsyncCommandState) this.currentTask;
      }

      private void LazyStartWorkerThread()
      {
        if (this.workerThread != null && !this.workerThread.IsAlive)
        {
          this.workerThread = (Thread) null;
          this.workerThreadControlBlock.Dispose();
          this.workerThreadControlBlock = (DedicatedThreadControlBlock) null;
        }
        if (this.workerThread != null)
          return;
        try
        {
          this.workerThreadControlBlock = new DedicatedThreadControlBlock();
          this.workerThread = new Thread(new ParameterizedThreadStart(this.WorkerThreadRoutine));
          this.workerThread.Name = $"DedicatedThreadCommandExecutor thread #{this.workerThread.ManagedThreadId}";
          this.workerThread.IsBackground = true;
          this.workerThread.SetApartmentState(this.apartmentState);
          this.workerThread.Start((object) this.workerThreadControlBlock);
          this.workerThread.Join(20);
        }
        catch
        {
          if (this.workerThread != null)
          {
            if (this.workerThread.IsAlive)
              this.workerThread.Abort();
            this.workerThread = (Thread) null;
          }
          if (this.workerThreadControlBlock != null)
          {
            this.workerThreadControlBlock.Dispose();
            this.workerThreadControlBlock = (DedicatedThreadControlBlock) null;
          }
          throw;
        }
      }

      private bool AbortWorkerThread()
      {
        bool flag = true;
        if (this.workerThread != null)
        {
          if (this.workerThread.IsAlive)
          {
            this.workerThread.Abort();
            if (!this.workerThread.Join(10000))
              flag = false;
          }
          this.workerThread = (Thread) null;
          this.workerThreadControlBlock.Dispose();
          this.workerThreadControlBlock = (DedicatedThreadControlBlock) null;
        }
        return flag;
      }

      private void WorkerThreadRoutine(object sharedState)
      {
        IDedicatedThreadControlBlock controlBlock = (IDedicatedThreadControlBlock) sharedState;
        try
        {
          this.WorkerThreadCommandLook(controlBlock);
        }
        catch (ThreadAbortException ex)
        {
          throw;
        }
        catch (Exception ex)
        {
          SuppressedExceptions.TraceException(ex, "DedicatedThreadCommandExecutor.WorkerThreadRoutine()");
        }
      }

      private void WorkerThreadCommandLook(IDedicatedThreadControlBlock controlBlock)
      {
        while (true)
        {
          DedicatedThreadTask commandContext;
          do
          {
            commandContext = controlBlock.Mailbox.TryGet(600000);
          }
          while (commandContext == null);
          commandContext.Command.Execute((IAsyncCommandContext) commandContext);
          commandContext.SetCompletedState();
        }
      }
    }
}
