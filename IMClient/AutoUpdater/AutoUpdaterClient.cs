using Intermech.ApplicationModel;
using Intermech.AutoUpdater;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using IPSAutoUpdater.Interfaces;
using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;


namespace IMClient.AutoUpdater
{
    internal sealed class AutoUpdaterClient : LongLifeObject, IAutoUpdaterClient
    {
      private IApplicationEventLogService eventLogService;
      private Lazy<IMainFormUpdate> mainFormService;
      private Guid id;
      private string executableDirPath;
      private int processId;

      public AutoUpdaterClient(
        IApplicationEventLogService eventLogService,
        Lazy<IMainFormUpdate> mainFormService)
      {
        if (eventLogService == null)
          throw new ArgumentNullException(nameof (eventLogService));
        if (mainFormService == null)
          throw new ArgumentNullException(nameof (mainFormService));
        this.eventLogService = eventLogService;
        this.mainFormService = mainFormService;
        this.id = Guid.NewGuid();
        this.executableDirPath = AppDomain.CurrentDomain.BaseDirectory;
        this.processId = this.GetCurrentProcessId();
      }

      private int GetCurrentProcessId()
      {
        using (Process currentProcess = Process.GetCurrentProcess())
          return currentProcess.Id;
      }

      public Guid ID
      {
        [DebuggerStepThrough] get => this.id;
      }

      public string Path
      {
        [DebuggerStepThrough] get => this.executableDirPath;
      }

      public int ProcessId
      {
        [DebuggerStepThrough] get => this.processId;
      }

      public bool PrepareForAutoUpdate(string reason)
      {
        if (reason == null)
          reason = string.Empty;
        this.eventLogService.DefaultLog.Write($"Получен запрос на обновление от службы обновления ПО ИНТЕРМЕХ (причина обновления: '{reason}'). В ближайшее время приложение будет завершено.");
        ThreadPool.QueueUserWorkItem(new WaitCallback(this.ExitApplication), (object) reason);
        return true;
      }

      private void ExitApplication(object arg)
      {
        using (AutoUpdaterMessageDialog updaterMessageDialog = new AutoUpdaterMessageDialog())
        {
          updaterMessageDialog.Text = "Автообновление IPS";
          updaterMessageDialog.MessageText = "Для клиента IPS доступно обновление. Приложение будет автоматически выгружено и обновлено.";
          updaterMessageDialog.AutoCloseMode = true;
          int num = (int) updaterMessageDialog.ShowDialog();
        }
        Form mainForm = this.mainFormService.Value.MainForm;
        Action method = (Action) (() =>
        {
          UISettings.AskOnExit = false;
          mainForm.Close();
        });
        mainForm.BeginInvoke((Delegate) method);
      }
    }
}
