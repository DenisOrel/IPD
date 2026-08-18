// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.wfCommandProvider
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.DataFormats;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using System;

#nullable disable
namespace Intermech.Workflow.Design;

/// <summary>Summary description for wfCommandProvider.</summary>
public class wfCommandProvider : ICommandsProvider
{
  public CommandsInfo GetMergedCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    CommandsInfo mergedCommands = new CommandsInfo();
    mergedCommands.Add("EditDocument", new CommandInfo(0, new ClickEventHandler(this.EditSchemeCommand)));
    return mergedCommands;
  }

  public CommandsInfo GetGroupCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    return new CommandsInfo();
  }

  public void EditSchemeCommand(
    ISelectedItems items,
    IServiceProvider viewServices,
    object additionalInfo)
  {
    for (int index = 0; index < items.Count; ++index)
      wfFunx.EditProcess((items.GetItemData(index, typeof (IDBTypedObjectID)) as IDBTypedObjectID).ObjectID);
  }
}
