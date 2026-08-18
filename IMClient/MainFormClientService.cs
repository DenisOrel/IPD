
// Type: IMClient.MainFormClientService




using Intermech.Search;
using System;
using System.Windows.Forms;


namespace IMClient
{
    internal sealed class MainFormClientService : IMainFormClientService
    {
      private IMClient.MainForm _mainForm;

      public MainFormClientService(IMClient.MainForm mainForm)
      {
        this._mainForm = mainForm != null ? mainForm : throw new ArgumentNullException(nameof (mainForm));
      }

      public Form MainForm => (Form) this._mainForm;
    }
}
