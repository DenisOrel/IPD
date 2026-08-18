
// Type: Intermech.Interfaces.Data.Actions.CreateBlankObjectAction
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.ControlFlow;
using Intermech.Localization;
using System;


namespace Intermech.Interfaces.Data.Actions;

public sealed class CreateBlankObjectAction : IAction
{
  private int objectType;
  private IUpdateableDBObjectRef objRef;

  public CreateBlankObjectAction(int objectType, IUpdateableDBObjectRef objRef)
  {
    if (objectType == -1)
      throw new ArgumentException();
    if (objRef == null)
      throw new ArgumentNullException();
    this.objectType = objectType;
    this.objRef = objRef;
  }

  public void Perform()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this.objRef.UpdateObjectId(sessionKeeper.Session.GetObjectCollection(this.objectType).Create().ObjectID);
  }

  public override string ToString() => LocalizationHolder.rm.GetString("SR_1646");
}
