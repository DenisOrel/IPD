// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.Commands.ExtendedSaveCommandProvider
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.DataFormats;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Client.Commands;

internal sealed class ExtendedSaveCommandProvider : ICommandsProvider
{
  private ExtendedSaveHelper extendedSaveHelper;

  public ExtendedSaveCommandProvider(ExtendedSaveHelper extendedSaveHelper)
  {
    this.extendedSaveHelper = extendedSaveHelper;
  }

  public CommandsInfo GetMergedCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    return CommandsInfo.Empty;
  }

  public CommandsInfo GetGroupCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    CommandsInfo groupCommands = new CommandsInfo();
    ICollection<int> supportedObjectTypes = this.extendedSaveHelper.SupportedObjectTypes;
    if (supportedObjectTypes.Count > 0)
    {
      int num = 0;
      for (int index = 0; index < items.Count; ++index)
      {
        if (items.GetItemData(index, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData && itemData.ObjectID < 0L && supportedObjectTypes.Contains(itemData.ObjectType))
          ++num;
      }
      if (num == items.Count)
        groupCommands.Add(MenuConsts.ExtendedSaveCommandName, new CommandInfo(3, new ClickEventHandler(ExtendedSaveCommandProvider.OnExtendedSave)));
    }
    return groupCommands;
  }

  private static void OnExtendedSave(
    ISelectedItems items,
    IServiceProvider viewServices,
    object info)
  {
    if (items == null)
      throw new InvalidOperationException();
    for (int index = 0; index < items.Count; ++index)
    {
      IDBTypedObjectID itemData = (IDBTypedObjectID) items.GetItemData(index, typeof (IDBTypedObjectID));
      if (itemData == null)
        throw new InvalidOperationException();
      ExtendedSaveCommand extendedSaveCommand = new ExtendedSaveCommand();
      extendedSaveCommand.ObjectId = itemData.ObjectID;
      extendedSaveCommand.ObjectTypeId = itemData.ObjectType;
      extendedSaveCommand.ObjectCaption = itemData.Caption;
      extendedSaveCommand.Execute();
    }
  }
}
