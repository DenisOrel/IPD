
// Type: Intermech.Commands.CheckoutCommand
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator.DBObjects;
using System;
using System.Collections.Generic;


namespace Intermech.Commands;

internal class CheckoutCommand : ObjectCopyCommand
{
  public CheckoutCommand()
    : base("Checkout", ObjectCommandEvents.Checkout)
  {
    this.DisplayName = LocalizationHolder.rm.GetString("Client.Core_1592");
  }

  protected override long DoReplaceObjectCopy(long currObjectId)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (this.UpdateUI)
        sessionKeeper.Session.StartLogHistory();
      long objectID = sessionKeeper.Session.CheckOutCommand(this.ObjectId);
      if (this.UpdateUI)
        sessionKeeper.Session.StopLogHistory();
      if (this.UpdateUI)
      {
        RecentObjectsNode.MRUObjects.Add(objectID, ObjectAction.CheckOut, DateTime.UtcNow);
        List<long> objectIDs = new List<long>();
        List<long> newObjectIDs = new List<long>();
        List<CategoryValue> modificationsHistoryList = sessionKeeper.Session.GetModificationsHistoryList();
        if (modificationsHistoryList != null)
        {
          for (int index = 0; index < modificationsHistoryList.Count; ++index)
          {
            CategoryValue categoryValue1 = modificationsHistoryList[index];
            if (categoryValue1.ActionID == ActionType.CheckOut && categoryValue1.CategoryType == 1)
            {
              ++index;
              CategoryValue categoryValue2 = modificationsHistoryList[index];
              objectIDs.Add(categoryValue1.CategoryID);
              newObjectIDs.Add(categoryValue2.CategoryID);
            }
          }
        }
        this.Notifications.QueueEvent((NotificationEventArgs) new DBObjectsCheckOutEventArgs("ObjectsCheckedOut", (IList<long>) objectIDs, (IList<long>) newObjectIDs));
      }
      return objectID;
    }
  }
}
