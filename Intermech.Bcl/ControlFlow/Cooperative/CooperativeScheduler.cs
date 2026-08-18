
// Type: Intermech.ControlFlow.Cooperative.CooperativeScheduler
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Localization;
using Intermech.UI;
using System;
using System.Collections.Generic;


namespace Intermech.ControlFlow.Cooperative
{
    public sealed class CooperativeScheduler
    {
      private readonly Dictionary<object, TaskState> taskStates;
      private readonly LinkedList<IAction> runList;
      private readonly LinkedList<ManualResetEvent> checkpoints;
      private readonly LinkedList<ManualResetEvent> immediateCheckpoints;
      private bool running;

      public CooperativeScheduler()
      {
        this.taskStates = new Dictionary<object, TaskState>();
        this.runList = new LinkedList<IAction>();
        this.checkpoints = new LinkedList<ManualResetEvent>();
        this.immediateCheckpoints = new LinkedList<ManualResetEvent>();
      }

      public void AddTask(IAction task)
      {
        if (task == null)
          throw new ArgumentNullException(nameof (task));
        this.RequireUnscheduledTask(task);
        this.ScheduleRun(task);
      }

      internal void SuspendTask(IAction task, IWaitObject waitObject)
      {
        if (task == null)
          throw new ArgumentNullException(nameof (task));
        if (waitObject == null)
          throw new ArgumentNullException(nameof (waitObject));
        this.RequireUnscheduledTask(task);
        this.taskStates[(object) task] = (TaskState) new WaitState(waitObject);
      }

      internal void ResumeTask(IAction task, IWaitObject waitObject)
      {
        if (task == null)
          throw new ArgumentNullException(nameof (task));
        if (waitObject == null)
          throw new ArgumentNullException(nameof (waitObject));
            TaskState taskState;
        if (!this.taskStates.TryGetValue((object) task, out taskState) || !(taskState is WaitState))
          throw new InvalidOperationException(LocalizationHolder.rm.GetString("SR_1690"));
        if (((WaitState) taskState).WaitObject != waitObject)
          throw new InvalidOperationException(LocalizationHolder.rm.GetString("SR_1689"));
        this.ScheduleRun(task);
      }

      private void RequireUnscheduledTask(IAction task)
      {
        if (this.taskStates.ContainsKey((object) task))
          throw new InvalidOperationException(LocalizationHolder.rm.GetString("SR_1691"));
      }

      private void ScheduleRun(IAction task)
      {
        this.taskStates[(object) task] = (TaskState) CooperativeScheduler.ReadyToRunState.Instance;
        this.runList.AddLast(task);
      }

      public ManualResetEvent CreateImmediateCheckpoint()
      {
        if (!this.running)
          throw new InvalidOperationException(LocalizationHolder.rm.GetString("SR_1692"));
        if (this.immediateCheckpoints.Count == 0)
          this.immediateCheckpoints.AddLast(new ManualResetEvent(this));
        return this.immediateCheckpoints.First.Value;
      }

      public void AppendCheckpoint(ManualResetEvent checkpoint)
      {
        this.ValidateCheckpoint(checkpoint);
        this.checkpoints.AddLast(checkpoint);
      }

      public void AppendCheckpointBefore(
        ManualResetEvent existingCheckpont,
        ManualResetEvent checkpoint)
      {
        if (existingCheckpont == null)
          throw new ArgumentNullException(nameof (existingCheckpont));
        this.ValidateCheckpoint(checkpoint);
        for (LinkedListNode<ManualResetEvent> node = this.checkpoints.First; node != null; node = node.Next)
        {
          if (node.Value == existingCheckpont)
          {
            this.checkpoints.AddBefore(node, checkpoint);
            return;
          }
        }
        throw new InvalidOperationException("Не удалось добавить новую контрольную точку после указанной.");
      }

      public void AppendCheckpointAfter(ManualResetEvent existingCheckpont, ManualResetEvent checkpoint)
      {
        if (existingCheckpont == null)
          throw new ArgumentNullException(nameof (existingCheckpont));
        this.ValidateCheckpoint(checkpoint);
        for (LinkedListNode<ManualResetEvent> node = this.checkpoints.First; node != null; node = node.Next)
        {
          if (node.Value == existingCheckpont)
          {
            this.checkpoints.AddAfter(node, checkpoint);
            return;
          }
        }
        throw new InvalidOperationException("Не удалось добавить новую контрольную точку после указанной.");
      }

      public void AppendCheckpoints(ICollection<ManualResetEvent> checkpoints)
      {
        if (checkpoints == null)
          throw new ArgumentNullException(nameof (checkpoints));
        foreach (ManualResetEvent checkpoint in (IEnumerable<ManualResetEvent>) checkpoints)
          this.AppendCheckpoint(checkpoint);
      }

      private void ValidateCheckpoint(ManualResetEvent checkpoint)
      {
        if (checkpoint == null)
          throw new ArgumentNullException(nameof (checkpoint));
        if (checkpoint.IsSet)
          throw new ArgumentException(LocalizationHolder.rm.GetString("SR_1693"), nameof (checkpoint));
      }

      public CooperativeSchedulerResult Run(IPercentageProgressSink progressSink = null)
      {
        this.running = !this.running ? true : throw new InvalidOperationException(LocalizationHolder.rm.GetString("SR_1694"));
        try
        {
          if (progressSink == null)
            progressSink = ProgressSinks.NullPercentageSink;
          return this.RunCore(progressSink);
        }
        finally
        {
          this.running = false;
        }
      }

      private CooperativeSchedulerResult RunCore(IPercentageProgressSink progressSink)
      {
        double num1 = 0.0;
        double num2 = 11.0;
        bool flag;
        do
        {
          while (this.runList.Count > 0)
          {
            int count = this.taskStates.Count;
            double num3 = (100.0 - num1) / (double) count / num2;
            if (num3 < 0.0)
              num3 = 0.0;
            IAction key = this.runList.First.Value;
            this.runList.RemoveFirst();
            this.taskStates.Remove((object) key);
            key.Perform();
            if (progressSink.IsCancelled)
              return CooperativeSchedulerResult.Cancelled;
            if (this.taskStates.Count != count)
            {
              num1 += num3;
              if (num1 > 100.0 || MathUtils.AlmostEqual(num1, 100.0))
                num1 = 100.0;
              progressSink.SetProgress(num1);
            }
          }
          if (this.immediateCheckpoints.Count > 0)
          {
            foreach (ManualResetEvent immediateCheckpoint in this.immediateCheckpoints)
              immediateCheckpoint.Set();
            this.immediateCheckpoints.Clear();
            flag = true;
          }
          else
          {
            flag = this.checkpoints.Count > 0;
            if (flag)
            {
              LinkedListNode<ManualResetEvent> first = this.checkpoints.First;
              this.checkpoints.RemoveFirst();
              first.Value.Set();
            }
          }
          if (num2 > 1.0)
            num2 /= 3.0;
        }
        while (flag);
        if (this.taskStates.Count > 0)
          throw new InvalidOperationException(LocalizationHolder.rm.GetString("SR_1695"));
        progressSink.SetState(string.Empty);
        progressSink.SetProgress(100.0);
        return CooperativeSchedulerResult.Completed;
      }

      private abstract class TaskState
      {
      }

      private sealed class ReadyToRunState : TaskState
        {
        public static readonly ReadyToRunState Instance = new ReadyToRunState();
      }

      private sealed class WaitState : TaskState
        {
        public readonly IWaitObject WaitObject;

        public WaitState(IWaitObject waitObject)
        {
          this.WaitObject = waitObject != null ? waitObject : throw new ArgumentNullException(nameof (waitObject));
        }
      }
    }
}
