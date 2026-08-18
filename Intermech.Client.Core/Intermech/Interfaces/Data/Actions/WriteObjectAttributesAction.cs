
// Type: Intermech.Interfaces.Data.Actions.WriteObjectAttributesAction
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;
using System;
using System.Collections.Generic;


namespace Intermech.Interfaces.Data.Actions;

public sealed class WriteObjectAttributesAction : WriteAttributesActionBase
{
  private readonly IDBObjectRef objRef;

  public WriteObjectAttributesAction(IDBObjectRef objRef, params AttributeValues[] attrValues)
    : base(attrValues)
  {
    this.objRef = objRef != null ? objRef : throw new ArgumentNullException();
  }

  protected override Dictionary<string, Exception> PerformWrite()
  {
    long objectId = this.objRef.GetObjectId();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return sessionKeeper.Session.GetObject(objectId, true).SetAttributesValuesEx(this.attrValues, false, true, false, GetAttributeValuesModes.None);
  }

  protected override string GetActionName() => LocalizationHolder.rm.GetString("SR_1657");
}
