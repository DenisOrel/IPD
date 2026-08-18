
// Type: Intermech.Interfaces.Data.Actions.DeleteFileAction
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.ControlFlow;
using Intermech.Localization;
using System;


namespace Intermech.Interfaces.Data.Actions;

public sealed class DeleteFileAction : IAction
{
  private IDBObjectRef objRef;
  private int valueIndex;
  private string fileName;

  public DeleteFileAction(IDBObjectRef objRef, int valueIndex, string fileName)
  {
    this.objRef = objRef != null ? objRef : throw new ArgumentNullException();
    this.valueIndex = valueIndex;
    this.fileName = fileName;
  }

  public void Perform()
  {
    long objectId = this.objRef.GetObjectId();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(objectId, true);
      IDBAttribute attributeById = dbObject.GetAttributeByID(sessionKeeper.Session.IdentHelper.FileAttributeID);
      if (attributeById == null)
        throw new KernelException(string.Format(LocalizationHolder.rm.GetString("SR_1649"), (object) dbObject.NameInMessages, (object) dbObject.ObjectID)).WithRecoveryActions((ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(dbObject.ObjectID));
      attributeById.Index = this.valueIndex;
      string description = attributeById.Description;
      if (!description.Equals(this.fileName))
        throw new KernelException(string.Format(LocalizationHolder.rm.GetString("SR_1650"), (object) this.fileName, (object) objectId, (object) this.valueIndex, (object) description)).WithRecoveryActions((ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(objectId));
      attributeById.DeleteValue();
    }
  }

  public override string ToString() => LocalizationHolder.rm.GetString("SR_1651");
}
