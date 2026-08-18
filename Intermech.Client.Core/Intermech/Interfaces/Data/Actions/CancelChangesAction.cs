
// Type: Intermech.Interfaces.Data.Actions.CancelChangesAction
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.ControlFlow;
using System;


namespace Intermech.Interfaces.Data.Actions;

public sealed class CancelChangesAction : IAction
{
  private IDBObjectRef objRef;

  public CancelChangesAction(IDBObjectRef objRef)
  {
    this.objRef = objRef != null ? objRef : throw new ArgumentNullException();
  }

  public void Perform()
  {
    long objectId = this.objRef.GetObjectId();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(objectId, true);
      if (objectId >= 0L)
        return;
      dbObject.CancelChanges();
      if (!(this.objRef is IUpdateableDBObjectRef objRef))
        return;
      objRef.UpdateObjectId(-objectId);
    }
  }

  public override string ToString() => "Отмена изменений в объекте";
}
