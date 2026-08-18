// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Editor.BriefcaseCommandProvider
// Assembly: Intermech.Workflow.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 48E18BC1-AABA-4AA1-97DA-4BBD788BE326
// Assembly location: D:\IPS\Client\Intermech.Workflow.Editor.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Editor.xml

using Intermech.DataFormats;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using Intermech.Workflow.Design;
using System;

#nullable disable
namespace Intermech.Workflow.Editor;

public class BriefcaseCommandProvider : ICommandsProvider
{
  public CommandsInfo GetMergedCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    CommandsInfo mergedCommands = new CommandsInfo();
    mergedCommands.Add("wfExport", new CommandInfo(0, new ClickEventHandler(this.ExportCommand)));
    mergedCommands.Add("Export", new CommandInfo(0));
    return mergedCommands;
  }

  public CommandsInfo GetGroupCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    return new CommandsInfo();
  }

  public void ExportCommand(
    ISelectedItems items,
    IServiceProvider viewServices,
    object additionalInfo)
  {
    for (int index = 0; index < items.Count; ++index)
      WorkflowBriefcase.Export((items.GetItemData(index, typeof (IDBTypedObjectID)) as IDBTypedObjectID).ObjectID);
  }
}
