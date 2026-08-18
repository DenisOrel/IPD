// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.DBReceiptCreator
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.WebPortal;
using System;


namespace Intermech.Kernel.Services.PortalServices;

public static class DBReceiptCreator
{
  internal static string contentFileName = "content.tbl";
  internal static string contentFileNote = "Содержание квитанции";

  public static IDBObject Create(
    IUserSession session,
    SiteInfo info,
    string name,
    Guid packetGuid,
    ReceiptTypes receiptType)
  {
    IDBObject receipt = session.GetObjectCollection(PortalConsts.objtypeReceipt).Create();
    DBReceiptCreator.SetReceiptAttribute(receipt, new Guid("cad00020-306c-11d8-b4e9-00304f19f545"), (object) name);
    DBReceiptCreator.SetReceiptAttribute(receipt, PortalConsts.attributeReceiptType, (object) receiptType);
    DBReceiptCreator.SetReceiptAttribute(receipt, PortalConsts.attributePacketGUID, (object) packetGuid);
    DBReceiptCreator.SetReceiptAttribute(receipt, PortalConsts.attributeReceiptCreateDate, (object) DateTime.UtcNow);
    DBReceiptCreator.SetReceiptAttribute(receipt, PortalConsts.attributeReceiptCreator, (object) session.UserName);
    DBReceiptCreator.SetReceiptAttribute(receipt, PortalConsts.attributeFirstPublishSite, (object) info.Code.ToString());
    return receipt;
  }

  public static void SetReceiptAttribute(IDBObject receipt, Guid attributeGuid, object value)
  {
    (receipt.GetAttributeByGuid(attributeGuid) ?? receipt.Attributes.AddAttribute(MetaDataHelper.GetAttributeTypeID(attributeGuid), false)).Value = value;
  }
}
