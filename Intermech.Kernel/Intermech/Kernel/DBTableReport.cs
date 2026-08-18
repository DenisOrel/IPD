// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBTableReport
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using System;
using System.Data;


namespace Intermech.Kernel;

public class DBTableReport(UserSession uSession, DataTable objectParams) : DBObject(uSession, objectParams)
{
  protected override void DoBeforeSetAdditionalAttributeValue(
    IDBAttribute attribute,
    object newValue)
  {
    if (attribute.AttributeID == MetaDataHelper.GetAttributeTypeID("cadd920c-306c-11d8-b4e9-00304f19f545") && CompareValuesHelper.NormalizedValue(newValue) != null && this.ObjectType == MetaDataHelper.GetObjectTypeID("cad0028a-306c-11d8-b4e9-00304f19f545") && this.UserSession.GetObjectInfo((long) newValue).ObjectTypeID == MetaDataHelper.GetObjectTypeID("cad00122-306c-11d8-b4e9-00304f19f545"))
      throw new Exception("Нельзя к общей выборке привязать персональный табличный отчет");
  }

  protected override void DoAfterSetAdditionalAttributeValue(IDBAttribute attribute)
  {
    this.UserSession.GetObjectType(this.ObjectType);
    if (attribute.AttributeID != MetaDataHelper.GetAttributeTypeID("cadd920c-306c-11d8-b4e9-00304f19f545"))
      return;
    IAttachedSelectionsServerService service = (IAttachedSelectionsServerService) ServerServices.GetService(typeof (IAttachedSelectionsServerService));
    if (attribute.IsNull || attribute.ValuesCount == 0)
      service.OnDeleteObject(this.ObjectID);
    else
      service.OnSetSelections((IDBObject) this, attribute.Values);
  }

  protected override void DoAfterDeleteAdditionalAttributeValue(
    IDBAttribute attribute,
    AttributeDataTableValue deletedValue)
  {
    if (attribute.AttributeID != MetaDataHelper.GetAttributeTypeID("cadd920c-306c-11d8-b4e9-00304f19f545") || deletedValue.IntegerValue == 0L)
      return;
    ((IAttachedSelectionsServerService) ServerServices.GetService(typeof (IAttachedSelectionsServerService))).OnDeleteSelection(this.ObjectID, deletedValue.IntegerValue);
  }

  public override int Delete(long DeleteMode)
  {
    int num = base.Delete(DeleteMode);
    ((IAttachedSelectionsServerService) ServerServices.GetService(typeof (IAttachedSelectionsServerService))).OnDeleteObject(this.ObjectID);
    return num;
  }
}
