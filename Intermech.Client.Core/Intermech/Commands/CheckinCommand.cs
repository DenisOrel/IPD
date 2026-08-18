
// Type: Intermech.Commands.CheckinCommand
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

internal class CheckinCommand : ObjectCopyCommand
{
  public CheckinCommand()
    : base("Checkin", ObjectCommandEvents.Checkin)
  {
    this.DisplayName = LocalizationHolder.rm.GetString("Client.Core_1591");
  }

  protected override long DoReplaceObjectCopy(long currObjectId)
  {
    SaveChangesCommand saveChangesCommand = (SaveChangesCommand) ObjectCommandFactory.CreateSaveChangesCommand(true);
    saveChangesCommand.ObjectId = currObjectId;
    saveChangesCommand.UpdateUI = this.UpdateUI;
    saveChangesCommand.Mode = new SaveChangesMode?(SaveChangesMode.Checkin);
    saveChangesCommand.CommonOptions = this.CommonOptions;
    saveChangesCommand.ContextServices = this.ContextServices;
    saveChangesCommand.Execute();
    ObjectCommandsOptions commonOptions = this.GetCommonOptions();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (this.UpdateUI)
        sessionKeeper.Session.StartLogHistory();
      long objectID = sessionKeeper.Session.CheckInCommand(this.ObjectId, (commonOptions & ObjectCommandsOptions.PreserveWorkingCopies) == ObjectCommandsOptions.PreserveWorkingCopies);
      if (this.UpdateUI)
        sessionKeeper.Session.StopLogHistory();
      if (this.UpdateUI)
      {
        RecentObjectsNode.MRUObjects.Add(objectID, ObjectAction.CheckIn, DateTime.UtcNow);
        List<long> objectIDs = new List<long>();
        List<CategoryValue> modificationsHistoryList = sessionKeeper.Session.GetModificationsHistoryList();
        if (modificationsHistoryList != null)
        {
          for (int index = 0; index < modificationsHistoryList.Count; ++index)
          {
            CategoryValue categoryValue = modificationsHistoryList[index];
            if (categoryValue.ActionID == ActionType.CheckIn && categoryValue.CategoryType == 1)
            {
              objectIDs.Add(categoryValue.CategoryID);
              ++index;
            }
          }
        }
        this.Notifications.QueueEvent((NotificationEventArgs) new DBObjectsEventArgs("ObjectsCheckedIn", (IList<long>) objectIDs));
      }
      return objectID;
    }
  }
}
