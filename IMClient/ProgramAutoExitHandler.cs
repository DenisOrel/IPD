
// Type: IMClient.ProgramAutoExitHandler




using Intermech.Interfaces.Client;
using Intermech.Runtime.ComInterop.LocalServer;
using System;
using System.Windows.Forms;


namespace IMClient
{
    internal sealed class ProgramAutoExitHandler : ComProcessAutoExitHandler
    {
      public ProgramAutoExitHandler(ComServer comServer)
        : base(comServer)
      {
        Application.Idle += new EventHandler(this.OnApplicationIdle);
      }

      protected override void RequestExit()
      {
      }

      private void OnApplicationIdle(object sender, EventArgs e)
      {
        if (!this.IsExitRequested)
          return;
        this.ExitApplication();
      }

      private void ExitApplication()
      {
        this.TraceExitEvent();
        Form openForm = Application.OpenForms["MainForm"];
        if (openForm != null)
        {
          UISettings.AskOnExit = false;
          openForm.Close();
        }
        else
          Application.Exit();
      }
    }
}
