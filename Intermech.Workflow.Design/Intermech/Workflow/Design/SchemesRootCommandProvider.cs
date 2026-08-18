// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.SchemesRootCommandProvider
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Navigator.ContextCommands;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using System;

#nullable disable
namespace Intermech.Workflow.Design;

internal class SchemesRootCommandProvider : ICommandsProvider
{
  public CommandsInfo GetMergedCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    CommandsInfo mergedCommands = new CommandsInfo();
    mergedCommands.Add("CreateSchemeGroup", new CommandInfo(0, new ClickEventHandler(this.CreateSchemeGroup)));
    return mergedCommands;
  }

  public CommandsInfo GetGroupCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    return new CommandsInfo();
  }

  public void CreateSchemeGroup(
    ISelectedItems items,
    IServiceProvider viewServices,
    object additionalInfo)
  {
    ObjectCommands.CreateCommand(wfConsts.SchemeCategoriesID);
  }
}
