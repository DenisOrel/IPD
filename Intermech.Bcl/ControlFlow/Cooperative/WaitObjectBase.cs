
// Type: Intermech.ControlFlow.Cooperative.WaitObjectBase
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Collections.Generic;


namespace Intermech.ControlFlow.Cooperative
{
    public class WaitObjectBase : IWaitObject
    {
      private readonly CooperativeScheduler scheduler;
      private LinkedList<IAction> waitTargets;

      public WaitObjectBase(CooperativeScheduler scheduler)
      {
        this.scheduler = scheduler != null ? scheduler : throw new ArgumentNullException(nameof (scheduler));
        this.waitTargets = new LinkedList<IAction>();
      }

      public virtual void Wait(IAction waitTarget)
      {
        if (waitTarget == null)
          throw new ArgumentNullException(nameof (waitTarget));
        this.scheduler.SuspendTask(waitTarget, (IWaitObject) this);
        this.waitTargets.AddLast(waitTarget);
      }

      protected int TaskCount => this.waitTargets.Count;

      protected void ResumeTasks()
      {
        if (this.waitTargets.Count <= 0)
          return;
        this.ResumeTasks(this.waitTargets.Count);
      }

      protected void ResumeTasks(int count)
      {
        if (this.waitTargets.Count <= 0 || count <= 0)
          return;
        List<IAction> actionList;
        if (this.waitTargets.Count <= count)
        {
          actionList = new List<IAction>((IEnumerable<IAction>) this.waitTargets);
          this.waitTargets.Clear();
        }
        else
          actionList = this.TakeTasks(count);
        foreach (IAction task in actionList)
          this.scheduler.ResumeTask(task, (IWaitObject) this);
      }

      private List<IAction> TakeTasks(int count)
      {
        List<IAction> tasks = new List<IAction>(count);
        for (int index = 0; index < count; ++index)
        {
          LinkedListNode<IAction> first = this.waitTargets.First;
          this.waitTargets.RemoveFirst();
          tasks.Add(first.Value);
        }
        return tasks;
      }
    }
}
