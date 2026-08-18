// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.Commands.CommandsModule
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.ApplicationModel;
using Intermech.Commands;
using Intermech.Files;
using Intermech.Localization;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using Intermech.Tools.LaunchActions;
using System;

#nullable disable
namespace Intermech.Tools.Client.Commands;

internal sealed class CommandsModule : InitializerModule
{
  private IFactory navigatorFactory;
  private ReplaceObjectReflector checkoutReflector;
  private ReplaceObjectReflector checkinReflector;
  private ReplaceObjectReflector cancelChangesReflector;
  private DBObjectAttributesToFileWriter dbObjectAttributesToFileWriter;
  private DBObjectPrototypeFixup dbObjectPrototypeFixup;
  private ArticleAttributesLockHandler articleAttributesLockHandler;
  private ExtendedSaveCommandProvider extendedSaveCommandsProvider;
  private MenuTemplateNode extendedSaveTemplateNode;
  private CheckinCommandOptionsProvider checkinOptionsProvider;

  public CommandsModule(IFactory navigatorFactory, ICommandsModuleFactory moduleFactory)
  {
    this.navigatorFactory = navigatorFactory;
    this.dbObjectAttributesToFileWriter = moduleFactory.CreateDBObjectAttributesToFileWriter();
    this.dbObjectPrototypeFixup = moduleFactory.CreateDBObjectPrototypeFixup();
    this.articleAttributesLockHandler = moduleFactory.CreateArticleAttributesLockHandler();
    this.checkinOptionsProvider = moduleFactory.CreateCheckinOptionsProvider();
    this.extendedSaveCommandsProvider = moduleFactory.CreateExtendedSaveCommandProvider();
  }

  protected override void DoInitialize()
  {
    base.DoInitialize();
    CommandFactory.OnCreateCommand += new EventHandler<CreateCommandEventArgs>(this.CreateObjectCommand);
    this.checkoutReflector = new ReplaceObjectReflector(ObjectCommandEvents.Checkout, (IReplaceFilePolicy) new PreserveAnyChanges());
    this.checkinReflector = new ReplaceObjectReflector(ObjectCommandEvents.Checkin, (IReplaceFilePolicy) new PreserveAnyChanges());
    this.cancelChangesReflector = new ReplaceObjectReflector(ObjectCommandEvents.CancelChanges, (IReplaceFilePolicy) new ForceRefresh());
    this.dbObjectAttributesToFileWriter.Enabled = true;
    this.dbObjectPrototypeFixup.Enabled = true;
    this.articleAttributesLockHandler.Enabled = true;
    this.checkinOptionsProvider.Enabled = true;
    this.EnableExtendedSaveCommand();
  }

  protected override void DoShutdown()
  {
    this.DisableExtendedSaveCommand();
    this.checkinOptionsProvider.Enabled = false;
    this.articleAttributesLockHandler.Enabled = false;
    this.dbObjectPrototypeFixup.Enabled = false;
    this.dbObjectAttributesToFileWriter.Enabled = false;
    this.checkoutReflector.Dispose();
    this.checkinReflector.Dispose();
    this.cancelChangesReflector.Dispose();
    CommandFactory.OnCreateCommand -= new EventHandler<CreateCommandEventArgs>(this.CreateObjectCommand);
    base.DoShutdown();
  }

  private void CreateObjectCommand(object sender, CreateCommandEventArgs e)
  {
    if (!(e.CommandType == typeof (ObjectCommand)) || e.Command != null)
      return;
    switch (e.CommandName)
    {
      case "Open":
        e.Command = (Command) new LauncherCommand("Edit", LaunchType.Edit, false);
        break;
      case "Edit":
        e.Command = (Command) new LauncherCommand("Edit", LaunchType.Edit, true);
        break;
      case "View":
        e.Command = (Command) new LauncherCommand("View", LaunchType.View, false);
        break;
      case "Print":
        e.Command = (Command) new LauncherCommand("Print", LaunchType.Print, false);
        break;
      case "OpenWith":
        e.Command = (Command) new OpenWithCommand();
        break;
    }
  }

  private void EnableExtendedSaveCommand()
  {
    MenuTemplateNode menuTemplateNode = this.navigatorFactory.ContextMenuTemplate[MenuConsts.SaveChangesCommandName];
    if (menuTemplateNode == null)
      return;
    this.extendedSaveTemplateNode = new MenuTemplateNode(MenuConsts.ExtendedSaveCommandName, LocalizationHolder.rm.GetString("Tools.Client_112"), menuTemplateNode.ImageIndex, menuTemplateNode.GroupID, menuTemplateNode.OrderID + 1);
    this.navigatorFactory.ContextMenuTemplate.BeginUpdate();
    try
    {
      this.navigatorFactory.ContextMenuTemplate.Nodes.Add(this.extendedSaveTemplateNode);
    }
    finally
    {
      this.navigatorFactory.ContextMenuTemplate.EndUpdate();
    }
    this.navigatorFactory.AddCommandsProvider(1, (ICommandsProvider) this.extendedSaveCommandsProvider);
  }

  private void DisableExtendedSaveCommand()
  {
    this.navigatorFactory.RemoveCommandsProvider(1, (ICommandsProvider) this.extendedSaveCommandsProvider);
    if (this.extendedSaveTemplateNode == null)
      return;
    this.navigatorFactory.ContextMenuTemplate.Nodes.Remove(this.extendedSaveTemplateNode);
    this.extendedSaveTemplateNode = (MenuTemplateNode) null;
  }
}
