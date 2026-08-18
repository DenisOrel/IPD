
// Type: Intermech.Interfaces.Data.Actions.CommitBlankObjectAction
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.ControlFlow;
using Intermech.Localization;
using System;


namespace Intermech.Interfaces.Data.Actions;

public sealed class CommitBlankObjectAction : IAction
{
  private IUpdateableDBObjectRef objRef;
  private bool autoCheckout;

  public CommitBlankObjectAction(IUpdateableDBObjectRef objRef, bool autoCheckout = false)
  {
    this.objRef = objRef != null ? objRef : throw new ArgumentNullException();
    this.autoCheckout = autoCheckout;
  }

  public void Perform()
  {
    long objectId = this.objRef.GetObjectId();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(objectId, true);
      if (!dbObject.IsCreationMode)
        return;
      dbObject.CommitCreation(true, this.autoCheckout);
      this.objRef.UpdateObjectId(dbObject.ObjectID);
    }
  }

  public override string ToString() => LocalizationHolder.rm.GetString("SR_1645");
}
