// Decompiled with JetBrains decompiler
// Type: Intermech.ECO.Client.CJOpen
// Assembly: Intermech.ECO.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BF6FF14F-986B-44C3-A04A-31D571D76B17
// Assembly location: D:\IPS\Client\Intermech.ECO.Client.dll

using Intermech.DataFormats;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using System;

#nullable disable
namespace Intermech.ECO.Client;

public class CJOpen : ICommandsProvider
{
  public CommandsInfo GetGroupCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    return new CommandsInfo();
  }

  public CommandsInfo GetMergedCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    CommandsInfo mergedCommands = new CommandsInfo();
    if (items != null)
    {
      if (items.Count == 1 && items.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData1 && itemData1.ObjectType == RevHelper.idObjCJRecord)
        mergedCommands.Add("CJRec.OpenCJ", new CommandInfo(2, new ClickEventHandler(ECOPlugin.OpenCJforRecord)));
      bool flag = false;
      for (int index = 0; index < items.Count; ++index)
      {
        if (items.GetItemData(index, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData2 && itemData2.ObjectType == RevHelper.idObjCJRecord)
        {
          flag = true;
          break;
        }
      }
      if (flag)
        mergedCommands.Add("CJRec.ReplaceCJs", new CommandInfo(2, new ClickEventHandler(ECOPlugin.ReplaceCJRecords)));
    }
    return mergedCommands;
  }
}
