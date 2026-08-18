// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Server.DBNotify
// Assembly: Intermech.Workflow.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8228C0CD-1234-4581-9863-2FEE480D176A
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Workflow.Server.dll

using Intermech.Interfaces;
using Intermech.Kernel;
using System;
using System.Data;

#nullable disable
namespace Intermech.Workflow.Server;

public class DBNotify(UserSession uSession, DataTable objectParams) : DBObject(uSession, objectParams)
{
  protected override void DoAfterDeleteAdditionalAttributeValue(
    IDBAttribute attribute,
    AttributeDataTableValue deletedValue)
  {
    base.DoAfterDeleteAdditionalAttributeValue(attribute, deletedValue);
    if (!attribute.AttributeType.GUID.Equals(new Guid("cad00628-306c-11d8-b4e9-00304f19f545")))
      return;
    int inlistId = deletedValue.InlistID;
    bool flag = false;
    IDBObject dbObject;
    if (this.CheckoutBy == 0L && this.ObjectModifyMode == ObjectModifyModes.Checkout)
    {
      dbObject = this.CheckOut();
      flag = true;
    }
    else
      dbObject = (IDBObject) this;
    this.DeleteValue(inlistId, dbObject.GetAttributeByGuid(new Guid("cad0062a-306c-11d8-b4e9-00304f19f545")));
    this.DeleteValue(inlistId, dbObject.GetAttributeByGuid(new Guid("cadd9940-306c-11d8-b4e9-00304f19f545")));
    this.DeleteValue(inlistId, dbObject.GetAttributeByGuid(new Guid("cad0062b-306c-11d8-b4e9-00304f19f545")));
    this.DeleteValue(inlistId, dbObject.GetAttributeByID(wfConsts.AttrGUIDsAttributesID));
    if (!flag)
      return;
    dbObject.CheckIn();
  }

  private void DeleteValue(int index, IDBAttribute attribute)
  {
    if (attribute.ValuesCount < index)
      throw new Exception($"Удаляемое значение (InlistID = {index}) находится вне диапазона значений атрибута {attribute.Name}");
    if (attribute.ValuesCount > 1)
    {
      attribute.Index = index;
      attribute.DeleteValue();
    }
    else
      attribute.ClearValues();
  }
}
