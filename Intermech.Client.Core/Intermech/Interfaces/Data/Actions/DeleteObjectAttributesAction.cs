
// Type: Intermech.Interfaces.Data.Actions.DeleteObjectAttributesAction
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.ControlFlow;
using System;
using System.Collections.Generic;
using System.Text;


namespace Intermech.Interfaces.Data.Actions;

public class DeleteObjectAttributesAction : IAction
{
  private IDBObjectRef objRef;
  private IList<string> attributeKeys;

  public DeleteObjectAttributesAction(IDBObjectRef objRef, IList<string> attributeKeys)
  {
    if (objRef == null)
      throw new ArgumentNullException(nameof (objRef));
    if (attributeKeys == null)
      throw new ArgumentNullException(nameof (attributeKeys));
    this.objRef = objRef;
    this.attributeKeys = attributeKeys;
  }

  public void Perform()
  {
    if (this.attributeKeys.Count <= 0)
      return;
    long objectId = this.objRef.GetObjectId();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(objectId, true);
      foreach (string attributeKey in (IEnumerable<string>) this.attributeKeys)
        dbObject.GetAttributeByName(attributeKey)?.Delete(0L);
    }
  }

  public override string ToString()
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append("Удаление из базы IPS атрибутов объекта");
    if (this.attributeKeys.Count > 0)
    {
      stringBuilder.Append(' ');
      stringBuilder.Append(DeleteObjectAttributesAction.AttrValuesToString(this.attributeKeys));
    }
    return stringBuilder.ToString();
  }

  private static string AttrValuesToString(IList<string> attributeKeys)
  {
    StringBuilder stringBuilder = new StringBuilder(attributeKeys.Count * 32 /*0x20*/);
    stringBuilder.Append('{');
    if (attributeKeys.Count > 0)
    {
      stringBuilder.Append(attributeKeys[0]);
      for (int index = 1; index < attributeKeys.Count; ++index)
      {
        stringBuilder.Append(',');
        stringBuilder.Append(' ');
        stringBuilder.Append(attributeKeys[index]);
      }
    }
    stringBuilder.Append('}');
    return stringBuilder.ToString();
  }
}
