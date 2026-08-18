// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.ExportReceipt
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.WebPortal;
using System;
using System.Data;


namespace Intermech.Kernel.Services.PortalServices;

internal class ExportReceipt : Receipt
{
  public ExportReceipt(Guid packetGuid)
    : base(ReceiptTypes.Export, false, packetGuid)
  {
    this.content = this.CreateTable();
  }

  protected override DataTable CreateTable()
  {
    DataTable table = base.CreateTable();
    table.Columns.Add(PortalConsts.attributeReasonInfo.ToString(), typeof (string));
    return table;
  }

  public void AddObject(IDBObject obj, PublishCompositionObject pco)
  {
    DataRow row = this.content.NewRow();
    row["CAPTION"] = (object) obj.Caption;
    row["F_FILENAME"] = (object) this.FormingFileNames(obj);
    IDBAttribute attributeByGuid1 = obj.GetAttributeByGuid(new Guid("cad0001f-306c-11d8-b4e9-00304f19f545"), false);
    if (attributeByGuid1 != null && !attributeByGuid1.IsNull)
      row["cad0001f-306c-11d8-b4e9-00304f19f545"] = (object) attributeByGuid1.AsString;
    IDBAttribute attributeByGuid2 = obj.GetAttributeByGuid(new Guid("cad00020-306c-11d8-b4e9-00304f19f545"), false);
    if (attributeByGuid2 != null && !attributeByGuid2.IsNull)
      row["cad00020-306c-11d8-b4e9-00304f19f545"] = (object) attributeByGuid2.AsString;
    row["F_OBJECT_TYPE"] = (object) MetaDataHelper.GetObjectTypeName(obj.ObjectType);
    row["F_VERSION_ID"] = (object) obj.VersionID;
    row["F_GUID"] = (object) obj.ObjectGUID.ToString();
    row["F_OBJ_GUID"] = (object) obj.GUID.ToString();
    IDBAttribute attributeByGuid3 = obj.GetAttributeByGuid(new Guid("cad00770-306c-11d8-b4e9-00304f19f545"), false);
    if (attributeByGuid3 != null && !attributeByGuid3.IsNull)
      row["cad00770-306c-11d8-b4e9-00304f19f545"] = (object) Convert.ToInt32(attributeByGuid3.AsInteger);
    row[PortalConsts.attributeReasonInfo.ToString()] = (object) pco.ReasonInfo;
    this.content.Rows.Add(row);
  }
}
