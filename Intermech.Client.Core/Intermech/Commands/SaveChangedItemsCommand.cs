
// Type: Intermech.Commands.SaveChangedItemsCommand
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator;
using Intermech.Navigator.DBObjects;
using System;
using System.Collections.Generic;


namespace Intermech.Commands;

/// <summary>
/// 
/// </summary>
public class SaveChangedItemsCommand : ExtendedSelectedItemsCommand
{
  public SaveChangedItemsCommand()
    : base("SaveItems")
  {
  }

  protected override void DoExecute()
  {
    List<long> objectIDs = new List<long>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this.DoBeforeProceedItems(sessionKeeper.Session);
      try
      {
        for (int index = 0; index < this.Items.Count; ++index)
        {
          IDBObjectID itemData = (IDBObjectID) this.Items.GetItemData(index, typeof (IDBObjectID));
          if (!objectIDs.Contains(itemData.Value))
          {
            this.DoBeforeProceedItem(index);
            try
            {
              ObjectCommand saveChangesCommand = ObjectCommandFactory.CreateSaveChangesCommand(true);
              saveChangesCommand.ObjectId = itemData.Value;
              saveChangesCommand.UpdateUI = false;
              saveChangesCommand.Execute();
              objectIDs.Add(itemData.Value);
              RecentObjectsNode.MRUObjects.Add(itemData.Value, ObjectAction.SaveChanges, DateTime.UtcNow);
            }
            finally
            {
              this.DoAfterProceedItem(index);
            }
          }
        }
      }
      finally
      {
        if (objectIDs.Count > 0)
        {
          DBObjectsEventArgs e = new DBObjectsEventArgs("ObjectsChanged", (IList<long>) objectIDs);
          Holder.NotificationService.FireEvent((object) null, (NotificationEventArgs) e);
        }
        this.DoAfterProceedItems(sessionKeeper.Session);
      }
    }
  }
}
