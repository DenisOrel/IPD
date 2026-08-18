// Decompiled with JetBrains decompiler
// Type: Intermech.ECO.Client.RevisionContextProvider
// Assembly: Intermech.ECO.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BF6FF14F-986B-44C3-A04A-31D571D76B17
// Assembly location: D:\IPS\Client\Intermech.ECO.Client.dll

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

#nullable disable
namespace Intermech.ECO.Client;

internal class RevisionContextProvider : ICommandsProvider
{
  CommandsInfo ICommandsProvider.GetMergedCommands(
    ISelectedItems items,
    System.IServiceProvider viewServices)
  {
    return new CommandsInfo();
  }

  CommandsInfo ICommandsProvider.GetGroupCommands(
    ISelectedItems items,
    System.IServiceProvider viewServices)
  {
    CommandsInfo groupCommands = new CommandsInfo();
    groupCommands.Add("LinkToKI", new CommandInfo(0, new ClickEventHandler(RevisionContextProvider.ECO_LinkToKI_Command)));
    groupCommands.Add("AddLinkToKI", new CommandInfo(0, new ClickEventHandler(RevisionContextProvider.ECO_AddLinkToKI_Command)));
    return groupCommands;
  }

  public static void ECO_AddLinkToKI_Command(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    object[] objArray = SelectionWindow.Select("Выбор комплекта извещений", (IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(RevisionComplectClient.RevisionComplect_TypeId), typeof (IDBTypedObjectID), SelectionOptions.SelectObjects | SelectionOptions.DisableMultiselect);
    if (objArray == null || objArray.Length == 0 || !(objArray[0] is IDBTypedObjectID))
      return;
    long objectId = ((IDBTypedObjectID) objArray[0]).ObjectID;
    RevisionContextProvider.AddToKI(items, objectId);
  }

  public static void ECO_LinkToKI_Command(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    long objectByTypeDialog = (ServicesManager.GetService(typeof (IObjectCreatorService)) as IObjectCreatorService).CreateObjectByTypeDialog(RevisionComplectClient.RevisionComplect_TypeId);
    RevisionContextProvider.AddToKI(items, objectByTypeDialog);
  }

  private static void AddToKI(ISelectedItems items, long kiID)
  {
    List<long> longList = new List<long>();
    for (int index = 0; index < items.Count; ++index)
    {
      long objectId = (items.GetItemData(index, typeof (IDBTypedObjectID)) as IDBTypedObjectID).ObjectID;
      longList.Add(objectId);
    }
    long projectID = kiID;
    if (projectID == -1L)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (long num1 in longList)
      {
        try
        {
          IDBRelation dbRelation = sessionKeeper.Session.GetRelationCollection(RevisionComplectClient.RevisionComplectRelation_TypeId).Create(projectID, num1);
          INotificationService service = (INotificationService) ServicesManager.GetService(typeof (INotificationService));
          if (service != null)
          {
            service.FireEvent((object) null, (NotificationEventArgs) new DBRelationsEventArgs("RelationsCreated", dbRelation.RelationID));
            service.FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsChanged", num1));
          }
        }
        catch (Exception ex)
        {
          int num2 = (int) MessageBox.Show(ex.Message, "Ошибка включения извещения в состав комплекта", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
      }
    }
  }
}
