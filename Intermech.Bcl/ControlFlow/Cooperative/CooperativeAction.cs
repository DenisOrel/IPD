
// Type: Intermech.ControlFlow.Cooperative.CooperativeAction
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.UI;
using System;
using System.Collections.Generic;


namespace Intermech.ControlFlow.Cooperative
{
    public abstract class CooperativeAction : IAction
    {
      private readonly CooperativeScheduler scheduler;
      private readonly LinkedList<IEnumerator<CooperativeState>> runStates;
      private object uiReportOperationId;
      private static readonly CooperativeState YieldState = (CooperativeState) new CooperativeState.Constant();

      public CooperativeAction(CooperativeScheduler scheduler)
      {
        this.scheduler = scheduler != null ? scheduler : throw new ArgumentNullException(nameof (scheduler));
        this.runStates = new LinkedList<IEnumerator<CooperativeState>>();
      }

      public void Perform()
      {
        if (this.runStates.Count == 0)
        {
          if (UIReport.Enabled)
            this.uiReportOperationId = this.GetUIReportOperationId();
          this.runStates.AddFirst(this.Coroutine().GetEnumerator());
        }
        bool flag;
        do
        {
          flag = false;
          IEnumerator<CooperativeState> runState = this.runStates.First.Value;
          if (this.Step(runState))
          {
            CooperativeState current = runState.Current;
            if (current == CooperativeAction.YieldState)
            {
              this.scheduler.AddTask((IAction) this);
            }
            else
            {
              switch (current)
              {
                case CooperativeState.Wait _:
                  ((CooperativeState.Wait) current).WaitObject.Wait((IAction) this);
                  break;
                case CooperativeState.Call _:
                  this.runStates.AddFirst(((CooperativeState.Call) current).InnerStates.GetEnumerator());
                  flag = true;
                  break;
                default:
                  throw new NotImplementedException();
              }
            }
          }
          else
          {
            this.runStates.RemoveFirst();
            if (this.runStates.Count > 0)
              flag = true;
          }
        }
        while (flag);
      }

      private bool Step(IEnumerator<CooperativeState> runState)
      {
        this.StartUIReportOperation();
        try
        {
          return runState.MoveNext();
        }
        finally
        {
          this.StopUIReportOperation();
        }
      }

      protected virtual object GetUIReportOperationId() => (object) null;

      private void StartUIReportOperation()
      {
        if (this.uiReportOperationId == null || !UIReport.Enabled)
          return;
        UIReport.StartLogicalOperation(this.uiReportOperationId);
      }

      private void StopUIReportOperation()
      {
        if (this.uiReportOperationId == null || !UIReport.Enabled)
          return;
        UIReport.StopLogicalOperation(this.uiReportOperationId);
      }

      protected abstract IEnumerable<CooperativeState> Coroutine();

      protected CooperativeState Yield => CooperativeAction.YieldState;

      protected CooperativeState Wait(IWaitObject waitObject)
      {
        return (CooperativeState) new CooperativeState.Wait(waitObject);
      }

      protected CooperativeState Call(IEnumerable<CooperativeState> innerStates)
      {
        return (CooperativeState) new CooperativeState.Call(innerStates);
      }

      protected CooperativeState Call(Func<IEnumerable<CooperativeState>> method)
      {
        return method != null ? (CooperativeState) new CooperativeState.Call(method()) : throw new ArgumentNullException(nameof (method));
      }

      protected CooperativeState Call<T>(Func<T, IEnumerable<CooperativeState>> method, T arg)
      {
        return method != null ? (CooperativeState) new CooperativeState.Call(method(arg)) : throw new ArgumentNullException(nameof (method));
      }

      protected CooperativeState Call<T1, T2>(
        Func<T1, T2, IEnumerable<CooperativeState>> method,
        T1 arg1,
        T2 arg2)
      {
        if (method == null)
          throw new ArgumentNullException(nameof (method));
        return (CooperativeState) new CooperativeState.Call(method(arg1, arg2));
      }

      protected CooperativeState Call<T1, T2, T3>(
        Func<T1, T2, T3, IEnumerable<CooperativeState>> method,
        T1 arg1,
        T2 arg2,
        T3 arg3)
      {
        if (method == null)
          throw new ArgumentNullException(nameof (method));
        return (CooperativeState) new CooperativeState.Call(method(arg1, arg2, arg3));
      }
    }
}
