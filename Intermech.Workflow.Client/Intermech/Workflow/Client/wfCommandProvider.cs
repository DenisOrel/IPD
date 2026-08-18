// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Client.wfCommandProvider
// Assembly: Intermech.Workflow.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 69C148DA-C200-403A-9CDB-2C809AA0D654
// Assembly location: D:\IPS\Client\Intermech.Workflow.Client.dll

using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using Intermech.Workflow.Design;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Client;

public class wfCommandProvider : ICommandsProvider
{
  public CommandsInfo GetMergedCommands(ISelectedItems items, System.IServiceProvider viewServices)
  {
    CommandsInfo mergedCommands = new CommandsInfo();
    mergedCommands.Add("ViewDocument", new CommandInfo(0, new ClickEventHandler(this.ViewSchemeCommand)));
    foreach (string extraOpenCommand in BaseHolder.ExtraOpenCommands)
      mergedCommands.Add(extraOpenCommand, new CommandInfo(0));
    return mergedCommands;
  }

  public CommandsInfo GetGroupCommands(ISelectedItems items, System.IServiceProvider viewServices)
  {
    return new CommandsInfo();
  }

  public void ViewSchemeCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    Form service = viewServices.GetService(typeof (Form)) as Form;
    wfFunx.OpenProcess(items, false, service != null && service.Modal);
  }
}
