// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.ArchivesContextMenuProvider
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

using Intermech.Archives.Common;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using System;

#nullable disable
namespace Intermech.Archives;

internal class ArchivesContextMenuProvider : ICommandsProvider
{
  public CommandsInfo GetMergedCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    CommandsInfo commandsInfo = new CommandsInfo();
    return CommandsInfo.Empty;
  }

  public CommandsInfo GetGroupCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    if (((viewServices.GetService(typeof (IViewState)) is IViewState service1 ? (long) service1.ViewState : 0L) & 2L) != 0L || items.Count != 1)
      return CommandsInfo.Empty;
    CommandsInfo groupCommands = new CommandsInfo();
    groupCommands.Add("Create", new CommandInfo(0, new ClickEventHandler(ArchivesCommands.CreateNewCommand)));
    IClipboard service2 = (IClipboard) ServicesManager.GetService(typeof (IClipboard));
    if (service2 != null && service2.GetDataObject() is IDBObjectTypedIDCollection dataObject && dataObject.Count == 1 && MetaDataHelper.IsObjectTypeChildOf(dataObject.GetTypedObjectID(0).ObjectType, ConstsHolder.ArcTypeID))
    {
      ClipboardObject typedObjectId = dataObject.GetTypedObjectID(0) as ClipboardObject;
      if (typedObjectId.Value != 0L && typedObjectId.Value != -1L)
        groupCommands.Add("Paste", new CommandInfo(0, new ClickEventHandler(ArchivesCommands.PasteCommand)));
    }
    return groupCommands;
  }
}
