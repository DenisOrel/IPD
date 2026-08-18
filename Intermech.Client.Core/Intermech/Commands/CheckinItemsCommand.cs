
// Type: Intermech.Commands.CheckinItemsCommand
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Windows.Forms;


namespace Intermech.Commands;

/// <summary>
/// 
/// </summary>
public class CheckinItemsCommand : ReplaceObjectCopiesCommand
{
  public CheckinItemsCommand()
    : base("CheckinItems", "Checkin", LocalizationHolder.rm.GetString("Client.Core_288"), LocalizationHolder.rm.GetString("Client.Core_287"))
  {
  }

  protected override void DoExecute()
  {
    ServiceContainer serviceContainer = new ServiceContainer(this.ContextServices);
    serviceContainer.AddService(typeof (ObjectCommandsOptionsHolder), (object) new ObjectCommandsOptionsHolder());
    List<WorkCopyCommandOptionsEditor> commandOptionsEditorList = new List<WorkCopyCommandOptionsEditor>();
    ServiceUtils.GetService<IWorkCopyCommandOptions>((object) ServicesManager.ServiceContainer, true).GetCheckinOptions(this.Items, (IServiceContainer) serviceContainer, commandOptionsEditorList);
    if (new CheckinObjectsForm((IServiceContainer) serviceContainer, LocalizationHolder.rm.GetString("Client.Core_281"), this.Items.Count == 1 ? LocalizationHolder.rm.GetString("Client.Core_1254") : LocalizationHolder.rm.GetString("Client.Core_1255"), string.Format(LocalizationHolder.rm.GetString("Client.Core_1256"), (object) this.Items.Count), (ICollection<WorkCopyCommandOptionsEditor>) commandOptionsEditorList)
    {
      ShowPreserveWorkingCopiesBox = true
    }.ShowDialog() != DialogResult.Yes)
      return;
    this.ProcessObjects((System.IServiceProvider) serviceContainer);
  }
}
