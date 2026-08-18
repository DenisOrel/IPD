// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.Import.ObjectAttributesXMLParser
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.WebPortal;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Xml;


namespace Intermech.Kernel.Services.PortalServices.Import;

internal sealed class ObjectAttributesXMLParser : AttributesXMLParser<IDBObjectType, ImportingObject>
{
  private readonly ImportReceipt _receipt;
  private readonly SiteInfo _creatorInfo;

  public ObjectAttributesXMLParser(
    Dictionary<Guid, ImportedInfo> links,
    string path,
    ImportReceipt receipt,
    SiteInfo creatorInfo)
    : base(links, path)
  {
    this._receipt = receipt;
    this._creatorInfo = creatorInfo;
  }

  protected override bool OnReadNode(
    AttributeInfo attrInfo,
    ImportingObject importingObject,
    XmlNode node)
  {
    return this._creatorInfo.SystemType != SystemTypes.Search || !SearchAttributes.HandleAttribute(attrInfo, node, importingObject);
  }

  protected override bool CheckAttribute4Type(
    IDBObjectType parent,
    IDBAttributeType attrType,
    IEventLogHelper eventHelper)
  {
    if (parent.AnyAttributes || parent.Attributes.GetAttributeByID(attrType.AttributeID, false) != null)
      return true;
    string EventStr = string.Format(LocalizationHolder.rm.GetString("Kernel_1105"), (object) parent.ObjectTypeName, (object) attrType.Name);
    eventHelper.AddToTrace(EventStr, Consts.traceAlways, string.Empty);
    return false;
  }

  protected override void OnAfterReadAttributes(
    UserSession session,
    XmlNode rootNode,
    IDBObjectType parent,
    ImportingObject importingObject)
  {
    if (this._creatorInfo.SystemType != SystemTypes.Search)
      return;
    SearchAttributes.Create((IUserSession) session, parent, importingObject, rootNode);
  }

  protected override void OnMessage(
    string message,
    string attributeName,
    ImportingObject importingObject)
  {
    if (this._receipt == null)
      return;
    this._receipt.AddAttributeRecord(importingObject, attributeName, message);
  }

  protected override bool CheckChildNode(string nodeName)
  {
    return nodeName.Equals(PortalConsts.XmlNodeAttribute) || nodeName.Equals(PortalConsts.XmlRootNodeRemark);
  }

  protected override void AddNullAttribute(
    int attributeID,
    ImportingObject importingObject,
    XmlNode node)
  {
    if (node.Name.Equals(PortalConsts.XmlRootNodeRemark))
      return;
    base.AddNullAttribute(attributeID, importingObject, node);
  }

  protected override void AddAttribute(
    AttributeRecord attribute,
    ImportingObject importingObject,
    XmlNode node)
  {
    if (node.Name.Equals(PortalConsts.XmlRootNodeRemark))
    {
      RemarkRecord attribute1 = new RemarkRecord(attribute, node.Attributes["F_SITE_ID"].Value[0], DateTimeHelper.ToDateTime(node.Attributes["F_MODIFY_DATE"].Value));
      importingObject.AddRemark(attribute1);
    }
    else
      base.AddAttribute(attribute, importingObject, node);
  }

  protected override long UnknownAttributableId => 0;
}
