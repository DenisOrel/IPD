
// Type: IMClient.StartupService




using Intermech.Interfaces.Client;
using System;
using System.Diagnostics;


namespace IMClient
{
    internal sealed class StartupService : IStartupService
    {
      private volatile bool isMainFormShown;
      private volatile bool isStartupCompleted;

      public bool IsStartupCompleted
      {
        [DebuggerStepThrough] get => this.isStartupCompleted;
      }

      public event EventHandler MainFormShown;

      public event EventHandler StartupComplete;

      internal void RaiseMainFormShown()
      {
        if (this.isMainFormShown)
          return;
        this.isMainFormShown = true;
        EventHandler mainFormShown = this.MainFormShown;
        if (mainFormShown == null)
          return;
        mainFormShown((object) this, EventArgs.Empty);
      }

      internal void RaiseStartupComplete()
      {
        if (this.isStartupCompleted)
          return;
        this.isStartupCompleted = true;
        EventHandler startupComplete = this.StartupComplete;
        if (startupComplete == null)
          return;
        startupComplete((object) this, EventArgs.Empty);
      }
    }
}
