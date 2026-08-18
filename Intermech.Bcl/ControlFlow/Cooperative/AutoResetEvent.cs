
// Type: Intermech.ControlFlow.Cooperative.AutoResetEvent
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml


namespace Intermech.ControlFlow.Cooperative
{
    public sealed class AutoResetEvent(CooperativeScheduler scheduler) : 
      WaitObjectBase(scheduler),
      IWaitEvent,
      IWaitObject
    {
      private bool set;

      public override void Wait(IAction waitTarget)
      {
        base.Wait(waitTarget);
        if (!this.set)
          return;
        this.set = false;
        this.ResumeTasks(1);
      }

      public void Set()
      {
        if (this.TaskCount != 0)
          this.ResumeTasks(1);
        else
          this.set = true;
      }

      public void Reset() => this.set = false;

      public bool IsSet => this.set;
    }
}
