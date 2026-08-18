
// Type: Intermech.ControlFlow.Cooperative.Barrier
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.ControlFlow.Cooperative
{
    public sealed class Barrier : WaitObjectBase, IWaitObject
    {
      private int count;
      private int remaining;

      public Barrier(CooperativeScheduler scheduler, int participantCount)
        : this(scheduler, participantCount, (IAction) null)
      {
      }

      public Barrier(CooperativeScheduler scheduler, int participantCount, IAction barrierTarget)
        : base(scheduler)
      {
        this.count = participantCount > 0 ? participantCount : throw new ArgumentOutOfRangeException();
        this.remaining = participantCount;
        if (barrierTarget == null)
          return;
        base.Wait(barrierTarget);
      }

      public void AddParticipants(int count)
      {
        if (count < 0)
          throw new ArgumentOutOfRangeException();
        this.count += count;
        this.remaining += count;
      }

      public override void Wait(IAction waitTarget)
      {
        base.Wait(waitTarget);
        --this.remaining;
        if (this.remaining != 0)
          return;
        this.ResumeTasks();
        this.remaining = this.count;
      }

      public int ParticipantCount => this.count;

      public int ParticipantsRemaining => this.remaining;
    }
}
