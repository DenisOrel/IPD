// Decompiled with JetBrains decompiler
// Type: Intermech.ECO.Client.ConvertToECOProvider
// Assembly: Intermech.ECO.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BF6FF14F-986B-44C3-A04A-31D571D76B17
// Assembly location: D:\IPS\Client\Intermech.ECO.Client.dll

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Contexts;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using System.Collections.Generic;
using System.Windows.Forms;

#nullable disable
namespace Intermech.ECO.Client;

internal class ConvertToECOProvider : ICommandsProvider
{
  public CommandsInfo GetMergedCommands(ISelectedItems items, System.IServiceProvider viewServices)
  {
    CommandsInfo mergedCommands = new CommandsInfo();
    if (items != null && items.Count == 1 && items.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData && MetaDataHelper.HasObjectTypeGroupingRelTypes(itemData.ObjectType) && !MetaDataHelper.IsObjectTypeChildOf(itemData.ObjectType, MetaDataHelper.GetObjectTypeID("cad00348-306c-11d8-b4e9-00304f19f545")))
      mergedCommands.Add("ECO.ConvertToECO", new CommandInfo(2, new ClickEventHandler(ConvertToECOProvider.ConvertToECO)));
    return mergedCommands;
  }

  public CommandsInfo GetGroupCommands(ISelectedItems items, System.IServiceProvider viewServices)
  {
    return new CommandsInfo();
  }

  internal static void ConvertToECO(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    if (items == null || items.Count != 1 || !(items.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData) || !MetaDataHelper.HasObjectTypeGroupingRelTypes(itemData.ObjectType) || MetaDataHelper.IsObjectTypeChildOf(itemData.ObjectType, MetaDataHelper.GetObjectTypeID("cad00348-306c-11d8-b4e9-00304f19f545")))
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = (IDBObject) null;
      long objectID = -1;
      using (SelIzvType selIzvType = new SelIzvType())
      {
        if (selIzvType.ShowDialog() != DialogResult.OK)
          return;
        objectID = selIzvType.EcoObjectID;
        dbObject = sessionKeeper.Session.GetObject(objectID);
      }
      IDBEditingContextsObject editingContextsObject = dbObject as IDBEditingContextsObject;
      if (ServicesManager.GetService(typeof (INotificationService)) is INotificationService service)
      {
        service.FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsCreated", objectID));
        service.FireEvent((object) null, new NotificationEventArgs("RecentObjectsChanged"));
      }
      if (editingContextsObject == null)
        return;
      sessionKeeper.Session.GetObject(itemData.ObjectID);
      List<long> longList1 = new List<long>();
      List<long> longList2 = new List<long>();
      List<long> longList3 = new List<long>();
    }
  }
}
