
// Type: Intermech.Data.KeyValueStores.BackupReplicaBackgroundTxProcessor`1
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Diagnostics;
using Intermech.Threading;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;


namespace Intermech.Data.KeyValueStores
{
    internal sealed class BackupReplicaBackgroundTxProcessor<TItem>
    {
      private const int WaitCycleSleepInterval = 25;
      private const int WaitCycleMinCount = 2;
      private int throttlingTimeout;
      private Action<IList<TItem>> processingAction;
      private Action<IList<TItem>, Exception> errorAction;
      private IWaitHandle completedWaitHandle;
      private List<TItem> itemQueue;
      private bool itemQueueIsEmpty;
      private ManualResetEventSlim workerEvent;
      private List<TItem> workerQueue;
      private Thread workerThread;
      private volatile BackgroundItemProcessorState workerState;
      private volatile bool workerCancellationRequested;

      public BackupReplicaBackgroundTxProcessor(
        int throttlingTimeout,
        Action<IList<TItem>> processingAction,
        Action<IList<TItem>, Exception> errorAction = null)
      {
        if (throttlingTimeout < 0)
          throw new ArgumentNullException(nameof (throttlingTimeout));
        if (processingAction == null)
          throw new ArgumentNullException(nameof (processingAction));
        this.throttlingTimeout = throttlingTimeout;
        this.processingAction = processingAction;
        this.errorAction = errorAction;
        this.itemQueue = new List<TItem>(16 /*0x10*/);
        this.itemQueueIsEmpty = true;
        this.workerEvent = new ManualResetEventSlim(false);
        this.workerQueue = new List<TItem>(this.itemQueue.Capacity);
        this.workerState = BackupReplicaBackgroundTxProcessor<TItem>.BackgroundItemProcessorState.NotStarted;
        this.workerThread = new Thread(new ParameterizedThreadStart(this.WorkerRoutine));
        this.workerThread.Name = $"{this.GetType().Name} worker thread";
        this.workerThread.IsBackground = true;
        this.workerThread.Start();
        if (!this.TryStart(1000))
          throw new InvalidOperationException($"Не удалось запустить рабочий поток для {this.GetType().Name}");
        this.completedWaitHandle = (IWaitHandle) new CompletedWaitHandle(this);
      }

      private bool TryStart(int timeout)
      {
        int num = timeout >= 0 ? Math.Max(timeout / 25, 2) : throw new ArgumentOutOfRangeException(nameof (timeout));
        while (this.GetWorkerState() == BackupReplicaBackgroundTxProcessor<TItem>.BackgroundItemProcessorState.NotStarted && num != 0)
        {
          --num;
          Thread.Sleep(25);
        }
        return this.GetWorkerState() != 0;
      }

      public bool TryStop(int timeout)
      {
        if (timeout < 0)
          throw new ArgumentOutOfRangeException(nameof (timeout));
        switch (this.GetWorkerState())
        {
          case BackupReplicaBackgroundTxProcessor<TItem>.BackgroundItemProcessorState.NotStarted:
          case BackupReplicaBackgroundTxProcessor<TItem>.BackgroundItemProcessorState.Stopped:
            return true;
          default:
            lock (this.itemQueue)
            {
              this.itemQueue.Clear();
              this.itemQueueIsEmpty = true;
            }
            this.workerCancellationRequested = true;
            this.workerEvent.Set();
            int num = Math.Max(timeout / 25, 2);
            while (this.GetWorkerState() != BackupReplicaBackgroundTxProcessor<TItem>.BackgroundItemProcessorState.Stopped && num != 0)
            {
              --num;
              Thread.Sleep(25);
            }
            if (this.GetWorkerState() != BackupReplicaBackgroundTxProcessor<TItem>.BackgroundItemProcessorState.Stopped)
              return false;
            this.DisposeResources();
            return true;
        }
      }

      private BackgroundItemProcessorState GetWorkerState()
      {
        return this.workerState;
      }

      private bool TestItemQueueIsEmpty() => Volatile.Read(ref this.itemQueueIsEmpty);

      private void DisposeResources() => this.workerEvent.Dispose();

      public void Add(TItem item)
      {
        this.CheckProcessingIsActive();
        lock (this.itemQueue)
        {
          this.itemQueue.Add(item);
          this.itemQueueIsEmpty = false;
        }
        this.workerEvent.Set();
      }

      public void AddRange(ICollection<TItem> items)
      {
        if (items == null)
          throw new ArgumentNullException(nameof (items));
        if (items.Count == 0)
          return;
        this.CheckProcessingIsActive();
        lock (this.itemQueue)
        {
          this.itemQueue.AddRange((IEnumerable<TItem>) items);
          this.itemQueueIsEmpty = false;
        }
        this.workerEvent.Set();
      }

      private void CheckProcessingIsActive()
      {
        switch (this.GetWorkerState())
        {
          case BackupReplicaBackgroundTxProcessor<TItem>.BackgroundItemProcessorState.Idling:
            break;
          case BackupReplicaBackgroundTxProcessor<TItem>.BackgroundItemProcessorState.Processing:
            break;
          default:
            throw new InvalidOperationException("Обработчик был остановлен.");
        }
      }

      public IWaitHandle WaitHandle
      {
        [DebuggerStepThrough] get => this.completedWaitHandle;
      }

      private bool WaitForProcessingToComplete(int timeout)
      {
        if (timeout < 0)
          throw new ArgumentOutOfRangeException(nameof (timeout));
        switch (this.GetWorkerState())
        {
          case BackupReplicaBackgroundTxProcessor<TItem>.BackgroundItemProcessorState.NotStarted:
          case BackupReplicaBackgroundTxProcessor<TItem>.BackgroundItemProcessorState.Stopped:
            return true;
          default:
            int num = Math.Max(timeout / 25, 2);
            while (!this.TestProcessingIsComplete() && num != 0)
            {
              --num;
              Thread.Sleep(25);
            }
            return this.TestProcessingIsComplete();
        }
      }

      private bool TestProcessingIsComplete()
      {
            BackgroundItemProcessorState workerState = this.GetWorkerState();
        return this.TestItemQueueIsEmpty() ? workerState != BackupReplicaBackgroundTxProcessor<TItem>.BackgroundItemProcessorState.Processing : workerState == BackupReplicaBackgroundTxProcessor<TItem>.BackgroundItemProcessorState.Stopped;
      }

      private void WorkerRoutine(object state)
      {
        try
        {
          while (!this.workerCancellationRequested)
          {
            this.workerState = BackupReplicaBackgroundTxProcessor<TItem>.BackgroundItemProcessorState.Idling;
            this.workerEvent.Wait();
            this.workerEvent.Reset();
            this.workerState = BackupReplicaBackgroundTxProcessor<TItem>.BackgroundItemProcessorState.Processing;
            if (this.workerCancellationRequested)
              break;
            if (this.throttlingTimeout != 0)
              Thread.Sleep(this.throttlingTimeout);
            lock (this.itemQueue)
            {
              if (!this.itemQueueIsEmpty)
              {
                this.workerQueue.AddRange((IEnumerable<TItem>) this.itemQueue);
                this.itemQueue.Clear();
                this.itemQueueIsEmpty = true;
              }
            }
            if (this.workerQueue.Count != 0)
            {
              try
              {
                this.processingAction((IList<TItem>) this.workerQueue);
              }
              catch (Exception ex)
              {
                if (this.errorAction != null)
                  this.errorAction((IList<TItem>) this.workerQueue, ex);
                else
                  SuppressedExceptions.TraceException(ex, "BackgroundItemProcessor`1.WorkerRoutine()");
              }
              this.workerQueue.Clear();
            }
          }
        }
        catch (ThreadAbortException ex)
        {
          this.workerState = BackupReplicaBackgroundTxProcessor<TItem>.BackgroundItemProcessorState.Stopped;
          throw;
        }
        catch
        {
        }
        finally
        {
          this.workerState = BackupReplicaBackgroundTxProcessor<TItem>.BackgroundItemProcessorState.Stopped;
        }
      }

      private enum BackgroundItemProcessorState
      {
        NotStarted,
        Idling,
        Processing,
        Stopped,
      }

      private sealed class CompletedWaitHandle : IWaitHandle
      {
        private BackupReplicaBackgroundTxProcessor<TItem> processor;

        public CompletedWaitHandle(
          BackupReplicaBackgroundTxProcessor<TItem> processor)
        {
          this.processor = processor;
        }

        public bool Wait(int timeout) => this.processor.WaitForProcessingToComplete(timeout);

        public bool Wait(TimeSpan timeout) => this.Wait((int) Math.Round(timeout.TotalMilliseconds));
      }
    }
}
