
// Type: IMClient.DockControlProxy




using Intermech.Bars;
using Intermech.Docking;
using Intermech.Interfaces.Client;
using System;


namespace IMClient
{
    internal sealed class DockControlProxy : DockControl, ISkipTargetActivate, IOpenAsObjectSupport
    {
      public DockControlProxy(Guid guid, string text, string persistString)
      {
        this.Guid = guid;
        this.Text = text;
        this.PersistString = persistString;
      }

      public bool CanBeOpenedInNewWindowsAsObject => false;

      public void OpenNewInstanceAsObject()
      {
      }
    }
}
