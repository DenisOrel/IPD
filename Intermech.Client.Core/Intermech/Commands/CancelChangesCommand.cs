
// Type: Intermech.Commands.CancelChangesCommand
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using System;


namespace Intermech.Commands;

internal class CancelChangesCommand : ObjectCopyCommand
{
  public CancelChangesCommand()
    : base("CancelChanges", ObjectCommandEvents.CancelChanges)
  {
    this.DisplayName = LocalizationHolder.rm.GetString("Client.Core_1590");
  }

  protected override long DoReplaceObjectCopy(long currObjectId)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(currObjectId, false);
      if (dbObject == null)
      {
        if (currObjectId < 0L)
          dbObject = sessionKeeper.Session.GetObject(Math.Abs(currObjectId), false);
        return dbObject != null ? dbObject.ObjectID : 0L;
      }
      dbObject.CancelChanges();
      long objectId = dbObject.ObjectID;
      if (DBHelper.IsObjectAlive(objectId))
      {
        if (this.UpdateUI)
          this.Notifications.QueueEvent((NotificationEventArgs) new DBObjectsEventArgs("ObjectsChangesCancelled", currObjectId));
        return objectId;
      }
      if (this.UpdateUI)
        this.Notifications.QueueEvent((NotificationEventArgs) new DBObjectsEventArgs("ObjectsRemoved", currObjectId));
      return 0;
    }
  }
}
