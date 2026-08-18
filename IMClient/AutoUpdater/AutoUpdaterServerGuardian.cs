
// Type: IMClient.AutoUpdater.AutoUpdaterServerGuardian




using System;
using System.Diagnostics;
using System.Threading;


namespace IMClient.AutoUpdater
{
    internal sealed class AutoUpdaterServerGuardian
    {
      private AutoUpdaterServerConnection connection;
      private int checkPeriod;
      private bool enabled;
      private Timer timer;

      public AutoUpdaterServerGuardian(AutoUpdaterServerConnection connection, int checkPeriod)
      {
        if (connection == null)
          throw new ArgumentNullException(nameof (connection));
        if (checkPeriod <= 0)
          throw new ArgumentOutOfRangeException(nameof (checkPeriod));
        this.connection = connection;
        this.checkPeriod = checkPeriod;
      }

      public bool Enabled
      {
        [DebuggerStepThrough] get => this.enabled;
        set
        {
          if (this.enabled == value)
            return;
          if (this.enabled)
            this.StopGuardian();
          this.enabled = value;
          if (!this.enabled)
            return;
          this.StartGuardian();
        }
      }

      private void StartGuardian()
      {
        this.timer = new Timer(new TimerCallback(this.CheckConnection), (object) null, 10000, this.checkPeriod);
      }

      private void StopGuardian()
      {
        this.timer.Dispose();
        this.timer = (Timer) null;
      }

      private void CheckConnection(object state)
      {
        try
        {
          this.connection.ValidateConnection();
        }
        catch
        {
        }
      }
    }
}
