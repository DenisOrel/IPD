// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.RedLineXMLFileFormer
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.Server.WebPortal;
using Intermech.Interfaces.WebPortal;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;


namespace Intermech.Kernel.Services.PortalServices;

public class RedLineXMLFileFormer : XMLFileFormer
{
  private readonly ObjectTag _objectTag;
  private readonly IDBObject _object;
  private readonly List<int> _freeChangeAttributes;

  public RedLineXMLFileFormer(
    IUserSession session,
    ExtendedTransferedObject unit,
    IBackupWriter writer,
    IDBObject obj,
    List<int> freeChangeAttributes,
    Attributes4ObjectTag tag,
    ObjectTag objectTag)
    : base(session, unit, writer, (Attributes4Tag) tag)
  {
    this._object = obj;
    this._objectTag = objectTag;
    this._freeChangeAttributes = freeChangeAttributes;
  }

  protected override void WriteRootNode(XmlDocument xmlDocument, XmlNode xmlRootNode)
  {
    XmlNode element = (XmlNode) xmlDocument.CreateElement(PortalConsts.XmlNodeSysAttribute);
    XMLFileHelper.AddAttribute(xmlDocument, element, "F_GUID", this._object.GUID.ToString());
    XMLFileHelper.AddAttribute(xmlDocument, element, "F_OBJECT_GUID", this._object.ObjectGUID.ToString());
    IDBObjectType objectType = this.session.GetObjectType(this._object.ObjectType);
    XMLFileHelper.AddAttribute(xmlDocument, element, "F_OBJTYPE_GUID", (objectType as IDBGuid).GUID.ToString());
    XMLFileHelper.AddAttribute(xmlDocument, element, "F_OBJ_TYPE_NAME", objectType.ObjectTypeName);
    if (this._objectTag != null)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append(this._objectTag.CreatorCode);
      if (this._objectTag.OwnerCode.HasValue)
      {
        stringBuilder.Append(this._objectTag.OwnerCode.Value);
        if (this._objectTag.CompositionOwnerCode.HasValue)
          stringBuilder.Append(this._objectTag.CompositionOwnerCode.Value);
      }
      XMLFileHelper.AddAttribute(xmlDocument, element, "F_SITE_ID", stringBuilder.ToString());
    }
    if (this.tag is Attributes4ObjectTag tag && !string.IsNullOrEmpty(tag.LinkedGuid))
      XMLFileHelper.AddAttribute(xmlDocument, element, "F_LINKED_GUID", tag.LinkedGuid);
    xmlRootNode.AppendChild(element);
    IDBAttribute attributeById = this._object.GetAttributeByID(this.session.IdentHelper.ModifyContentDateID);
    if (attributeById == null)
      return;
    DateTime localDateTime = !attributeById.IsNull ? attributeById.AsDateTime : DateTime.MinValue;
    XmlNode attributeNode = this.CreateAttributeNode(xmlDocument, attributeById.AttributeType, PortalConsts.XmlNodeAttribute, false);
    XmlNode valueNode = XMLFileHelper.CreateValueNode(xmlDocument, 0);
    XMLFileHelper.AddDateTimeAttribute(xmlDocument, valueNode, this.ToUTCDateTime(localDateTime, this.session));
    attributeNode.AppendChild(valueNode);
    xmlRootNode.AppendChild(attributeNode);
  }

  protected override bool IsEnablePublishFile(BlobInformation biFile)
  {
    return biFile.FileType == FileTypes.ftNotContent || biFile.FileType == FileTypes.ftRedlining;
  }

  protected override void AdditionalAttributesForNode(
    XmlDocument xmlDocument,
    XmlNode xmlNode,
    IDBAttributeType attrType)
  {
    ISitesCacheService customService = (ISitesCacheService) this.session.GetCustomService(typeof (ISitesCacheService));
    XMLFileHelper.AddAttribute(xmlDocument, xmlNode, "F_SITE_ID", customService.Info.Code.ToString());
    XMLFileHelper.AddAttribute(xmlDocument, xmlNode, "F_MODIFY_DATE", DateTimeHelper.ToString(DateTime.UtcNow));
  }

  protected override string AttributeNode => PortalConsts.XmlRootNodeRemark;

  protected override List<int> EnableAttributes => this._freeChangeAttributes ?? new List<int>(0);

  protected override IDBAttributeCollection Attributes => this._object.Attributes;
}
