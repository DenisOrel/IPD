// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.TablesNodeContext
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Interfaces.Client;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using System;

#nullable disable
namespace Intermech.Imbase;

public class TablesNodeContext : ICommandsProvider
{
  public CommandsInfo GetMergedCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    CommandsInfo mergedCommands = new CommandsInfo();
    mergedCommands.Add("Create", new CommandInfo(0, new ClickEventHandler(TablesNodeContext.Create)));
    return mergedCommands;
  }

  public CommandsInfo GetGroupCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    return CommandsInfo.Empty;
  }

  public static void Create(ISelectedItems items, IServiceProvider viewServices, object addon)
  {
    if (!(ServicesManager.GetService(typeof (IObjectCreatorService)) is IObjectCreatorService service1))
      return;
    long objectByTypeDialog = service1.CreateObjectByTypeDialog(Consts.ImbaseTableTypeGUID);
    if (objectByTypeDialog.Equals(-1L) || !(ServicesManager.GetService(typeof (INotificationService)) is INotificationService service2))
      return;
    service2.FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsCreated", objectByTypeDialog));
  }
}
