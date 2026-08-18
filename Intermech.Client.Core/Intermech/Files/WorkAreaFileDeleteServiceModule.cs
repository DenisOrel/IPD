
// Type: Intermech.Files.WorkAreaFileDeleteServiceModule
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.ApplicationModel;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System;
using System.Windows.Forms;


namespace Intermech.Files;

internal sealed class WorkAreaFileDeleteServiceModule : InitializerModule, IWorkAreaFileDeleteService
{
  private IDBConfigurations _configurations;
  private IFileVault _fileVault;
  private Timer _timer;
  private IPropertyPagesService _propertyPagesService;

  public WorkAreaFileDeleteServiceModule(
    IDBConfigurations configurations,
    IFileVault fileVault,
    IPropertyPagesService propertyPagesService)
  {
    this._configurations = configurations;
    this._fileVault = fileVault;
    this._propertyPagesService = propertyPagesService;
  }

  protected override void DoInitialize()
  {
    base.DoInitialize();
    this._propertyPagesService.AddPage("Пользователи\\Текущий пользователь\\Рабочая область", (IPropertyPage) new WorkAreaFileDeletePropertiesControlPage());
    this.StartMonitor();
  }

  protected override void DoShutdown()
  {
    if (this._timer != null)
    {
      this._timer.Stop();
      this._timer.Dispose();
    }
    base.DoShutdown();
  }

  public void StartMonitor()
  {
    if (this._timer != null)
    {
      this._timer.Stop();
      this._timer.Dispose();
    }
    this._timer = new Timer() { Interval = 86400000 };
    this._timer.Tick += new EventHandler(this.Timer_Tick);
    this._timer.Start();
  }

  private void Timer_Tick(object sender, EventArgs e)
  {
    try
    {
      long num1 = this._configurations.ReadInteger("CLIENT", "WORKCLEANER", "CleaningPendingDateCount", 92L, DBConfigMode.UserOnly);
      int num2 = (int) this._configurations.ReadInteger("CLIENT", "WORKCLEANER", "CleaningPendingDateMode", 0L, DBConfigMode.UserOnly);
      TimeSpan timeSpan = TimeSpan.FromDays(92.0);
      switch (num2)
      {
        case 0:
          timeSpan = TimeSpan.FromDays((double) num1);
          break;
        case 1:
          timeSpan = TimeSpan.FromDays((double) (num1 * 7L));
          break;
        case 2:
          timeSpan = TimeSpan.FromDays((double) (num1 * 31L /*0x1F*/));
          break;
        case 3:
          timeSpan = TimeSpan.FromDays((double) (num1 * 365L));
          break;
      }
      foreach (DBObjectState dbObjectState in this._fileVault.WorkArea.GetPublishedObjects(DateTime.UtcNow.Date - timeSpan).FindAll((Predicate<DBObjectState>) (workObj => !workObj.IsEditableState)))
        this._fileVault.WorkArea.Unpublish(dbObjectState.ObjectId);
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }
}
