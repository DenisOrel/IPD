
// Type: Intermech.Interfaces.Data.Actions.ChangeObjectTypeAction
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.ControlFlow;
using System;


namespace Intermech.Interfaces.Data.Actions;

public sealed class ChangeObjectTypeAction : IAction
{
  private IDBObjectRef objRef;
  private int objectType;

  public ChangeObjectTypeAction(IDBObjectRef objRef, int objectType)
  {
    this.objRef = objRef != null ? objRef : throw new ArgumentNullException();
    this.objectType = objectType;
  }

  public void Perform()
  {
    long objectId = this.objRef.GetObjectId();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      sessionKeeper.Session.GetObject(objectId, true).ObjectType = this.objectType;
  }

  public override string ToString() => "Изменение типа объекта IPS";
}
