
// Type: Intermech.Interfaces.Data.Actions.CheckoutAction
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.ControlFlow;
using Intermech.Localization;
using System;


namespace Intermech.Interfaces.Data.Actions;

public sealed class CheckoutAction : IAction
{
  private IUpdateableDBObjectRef objRef;
  private bool strictMode;

  public CheckoutAction(IUpdateableDBObjectRef objRef, bool strictMode)
  {
    this.objRef = objRef != null ? objRef : throw new ArgumentNullException();
    this.strictMode = strictMode;
  }

  public void Perform()
  {
    long objectId = this.objRef.GetObjectId();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this.objRef.UpdateObjectId(sessionKeeper.Session.GetObject(objectId, true).CheckOut(this.strictMode).ObjectID);
  }

  public override string ToString() => LocalizationHolder.rm.GetString("SR_1644");
}
