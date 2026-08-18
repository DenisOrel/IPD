// Decompiled with JetBrains decompiler
// Type: Intermech.DatabaseConfigurator.Scripting.CSharp.NavigatorCommandProvider
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.DataFormats;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.DatabaseConfigurator.Scripting.CSharp;

internal sealed class NavigatorCommandProvider : ICommandsProvider
{
  private MenuTemplateNode checkSelectedScriptsStructureNode;
  private Func<CheckScriptStructureUIAction> createCheckScriptStructureUIAction;

  public NavigatorCommandProvider(
    Func<CheckScriptStructureUIAction> createCheckScriptStructureUIAction)
  {
    this.createCheckScriptStructureUIAction = createCheckScriptStructureUIAction != null ? createCheckScriptStructureUIAction : throw new ArgumentNullException(nameof (createCheckScriptStructureUIAction));
  }

  public void AddCommandsToMenuTemplate(MenuTemplate menuTemplate)
  {
    this.checkSelectedScriptsStructureNode = new MenuTemplateNode(ScriptCheckerMenuConsts.CheckScriptStructureCommandName, ScriptCheckerMenuConsts.CheckScriptStructureDisplayName, -1, 300, 0);
    menuTemplate.BeginUpdate();
    try
    {
      menuTemplate.Nodes.Add(this.checkSelectedScriptsStructureNode);
    }
    finally
    {
      menuTemplate.EndUpdate();
    }
  }

  public void RemoveCommandsFromMenuTemplate(MenuTemplate menuTemplate)
  {
    if (this.checkSelectedScriptsStructureNode == null)
      return;
    menuTemplate.BeginUpdate();
    try
    {
      menuTemplate.Nodes.Remove(this.checkSelectedScriptsStructureNode);
      this.checkSelectedScriptsStructureNode = (MenuTemplateNode) null;
    }
    finally
    {
      menuTemplate.EndUpdate();
    }
  }

  public CommandsInfo GetMergedCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    if (items == null)
      throw new ArgumentNullException(nameof (items));
    return CommandsInfo.Empty;
  }

  public CommandsInfo GetGroupCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    if (items == null)
      throw new ArgumentNullException(nameof (items));
    if (items.Count == 0)
      return CommandsInfo.Empty;
    CommandsInfo groupCommands = new CommandsInfo();
    groupCommands.Add(ScriptCheckerMenuConsts.CheckScriptStructureCommandName, new CommandInfo(0, new ClickEventHandler(this.CheckSelectedScriptsStructureHandler)));
    return groupCommands;
  }

  private void CheckSelectedScriptsStructureHandler(
    ISelectedItems items,
    IServiceProvider viewServices,
    object additionalInfo)
  {
    this.createCheckScriptStructureUIAction().Execute(this.CreateScriptInfoList(items));
  }

  private List<ScriptInfo> CreateScriptInfoList(ISelectedItems items)
  {
    List<ScriptInfo> scriptInfoList = new List<ScriptInfo>(items.Count);
    for (int index = 0; index < items.Count; ++index)
    {
      IDBObjectID itemData = (IDBObjectID) items.GetItemData(index, typeof (IDBObjectID));
      long objectId = itemData.Value;
      string caption = string.IsNullOrEmpty(itemData.Caption) ? $"Сценарий #{objectId}" : itemData.Caption;
      scriptInfoList.Add(new ScriptInfo(objectId, caption));
    }
    return scriptInfoList;
  }
}
